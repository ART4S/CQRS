DO 
$views$ BEGIN

CREATE MATERIALIZED VIEW get_products_list AS
	SELECT
		p.id,
		p.name,
		p.price
	FROM
		public.products p

WITH NO DATA;
	
CREATE UNIQUE INDEX IX_get_products_list_id ON get_products_list (id);

CREATE MATERIALIZED VIEW get_product_comments AS
	SELECT
		pc.id,
		pc.body,
		pc.createdate,
		pc.productid,
		pc.authorid,
		u.name as AuthorName,
		pc.parentcommentid
	FROM
		public.productComments pc
	JOIN 
		public.users u ON u.id = pc.authorid

WITH NO DATA;

CREATE UNIQUE INDEX IX_get_product_comments_id ON get_product_comments (id);

CREATE MATERIALIZED VIEW get_product_reviews AS
	SELECT
		r.id,
		r.title,
		r.comment,
		r.createdate,
		r.rating,
		r.authorid,
		u.name as AuthorName,
		r.productid
	FROM
		productReviews r
	JOIN 
		users u ON u.id = r.authorid

WITH NO DATA;

CREATE UNIQUE INDEX IX_get_product_reviews_id ON get_product_reviews (id);

END $views$;