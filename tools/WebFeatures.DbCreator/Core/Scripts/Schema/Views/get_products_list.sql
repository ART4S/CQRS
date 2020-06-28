CREATE MATERIALIZED VIEW get_products_list AS
	SELECT
		p.id,
		p.name,
		p.price
	FROM
		public.products p

WITH NO DATA;
	
CREATE UNIQUE INDEX IX_get_products_list_id ON get_products_list (id);