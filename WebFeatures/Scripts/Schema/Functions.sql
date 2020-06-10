DO 
$functions$ BEGIN

CREATE FUNCTION get_product(product_id UUID)
RETURNS TABLE
(
	Id UUID,
	Name VARCHAR,
	Price DECIMAL,
	Description VARCHAR,
	ManufacturerId UUID,
	CategoryId UUID,
	BrandId UUID
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
		p.brandid
	FROM
		public.products p
	WHERE 
		p.id = product_id;

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

END $functions$;