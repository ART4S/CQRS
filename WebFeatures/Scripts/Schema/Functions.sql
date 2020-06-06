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

END $functions$;