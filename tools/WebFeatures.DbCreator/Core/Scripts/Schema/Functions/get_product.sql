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