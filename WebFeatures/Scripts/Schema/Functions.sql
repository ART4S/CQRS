DO $functions$
BEGIN
CREATE FUNCTION get_products_list()
RETURNS TABLE
(
	Id UUID,
	Name VARCHAR,
	Price DECIMAL
) AS $$
BEGIN
	RETURN QUERY 
	SELECT
		p.id,
		p.name,
		p.price
	FROM
		public.Products p;

END; $$ LANGUAGE 'plpgsql';

CREATE FUNCTION get_product_comments(product_id UUID)
RETURNS TABLE
(
	Id UUID,
	Body VARCHAR,
	CreateDate TIMESTAMP WITHOUT TIME ZONE,
	AuthorId UUID,
	AuthorName VARCHAR,
	ParentCommentId UUID
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
		public.ProductComments c
	JOIN 
		public.Users u ON u.id = c.authorid
	WHERE 
		c.productid = product_id;

END; $$ LANGUAGE 'plpgsql';

CREATE FUNCTION get_product_reviews(product_id UUID)
RETURNS TABLE
(
	Id UUID,
	Title VARCHAR,
	Comment VARCHAR,
	CreateDate TIMESTAMP WITHOUT TIME ZONE,
	Rating INT,
	AuthorId UUID,
	AuthorName VARCHAR,
	ProductId UUID
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
		ProductReviews r
	JOIN 
		Users u ON u.id = r.authorid
	WHERE 
		r.productid = product_id;

END; $$ LANGUAGE 'plpgsql';

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

END $functions$