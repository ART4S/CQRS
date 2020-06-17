DO 
$functions$ BEGIN

CREATE FUNCTION get_product(product_id UUID)
RETURNS TABLE
(
	id UUID,
	name VARCHAR,
	price DECIMAL,
	description VARCHAR,
	manufacturerid UUID,
	categoryid UUID,
	brandid UUID,
	averagerating INT
) AS $$
BEGIN
	RETURN QUERY 
	SELECT
		p.id,
		p.name,
		p.price,
		p.description,
		p.manufacturerid,
		p.categoryid,
		p.brandid,
		AVG(r.rating)::INT
	FROM
		public.products p
	LEFT JOIN
		public.productreviews r ON r.productid = p.id
	WHERE
		p.id = product_id
	GROUP BY
		p.id,
		p.name,
		p.price,
		p.description,
		p.manufacturerid,
		p.categoryid,
		p.brandid;

END; $$ LANGUAGE 'plpgsql';

CREATE FUNCTION get_product_comments(product_id UUID)
RETURNS TABLE
(
	id UUID,
	body VARCHAR,
	createdate TIMESTAMP WITHOUT TIME ZONE,
	authorid UUID,
	authorname VARCHAR,
	parentcommentid UUID
) AS $$
BEGIN
	RETURN QUERY 
	SELECT
		c.id,
		c.body,
		c.createdate,
		c.authorid,
		u.name,
		c.parentcommentid
	FROM
		public.productcomments c
	JOIN 
		public.users u ON u.id = c.authorid
	WHERE 
		c.productid = product_id;

END; $$ LANGUAGE 'plpgsql';

CREATE FUNCTION get_product_reviews(product_id UUID)
RETURNS TABLE
(
	id UUID,
	title VARCHAR,
	comment VARCHAR,
	createdate TIMESTAMP WITHOUT TIME ZONE,
	rating INT,
	authorid UUID,
	authorname VARCHAR,
	productid UUID
) AS $$
BEGIN
	RETURN QUERY 
	SELECT
		r.id,
		r.title,
		r.comment,
		r.createdate,
		r.rating,
		r.authorid,
		u.name,
		r.productid
	FROM
		productReviews r
	JOIN 
		users u ON u.id = r.authorid
	WHERE 
		r.productid = product_id;

END; $$ LANGUAGE 'plpgsql';

CREATE FUNCTION user_has_permission(user_id UUID, permission VARCHAR)
RETURNS BOOLEAN AS $$
BEGIN
	RETURN EXISTS (
		SELECT * 
		FROM 
			public.rolepermissions p 
		JOIN 
			public.roles r ON r.id = p.roleid 
		JOIN 
			public.userroles ur ON ur.roleid = r.id 
		WHERE 
			ur.userid = user_id AND p.name = permission);
END; $$ LANGUAGE 'plpgsql';

END $functions$;