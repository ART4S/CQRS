DO $functions$
BEGIN

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
		productReviews r
	JOIN 
		users u ON u.id = r.authorid
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

END $functions$;

DO 
$views$ BEGIN

CREATE MATERIALIZED VIEW get_products_list AS
	SELECT
		p.id as Id,
		p.name as Name,
		p.price as Price
	FROM
		public.products p

WITH NO DATA;
	
CREATE UNIQUE INDEX IX_get_products_list_Id ON get_products_list (Id);

CREATE MATERIALIZED VIEW get_product_comments AS
	SELECT
		pc.id as Id,
		pc.body as Body,
		pc.createdate as CreateDate,
		pc.productid as ProductId,
		pc.authorid as AuthorId,
		u.name as AuthorName,
		pc.parentcommentid as ParentCommentId
	FROM
		public.productComments pc
	JOIN 
		public.users u ON u.id = pc.authorid

WITH NO DATA;

CREATE UNIQUE INDEX IX_get_product_comments_Id ON get_product_comments (Id);

END $views$;