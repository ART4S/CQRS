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