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