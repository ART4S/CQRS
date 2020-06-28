CREATE TABLE products
(
	id UUID NOT NULL,
	name VARCHAR NOT NULL,
	price DECIMAL,
	description VARCHAR,
	createdate TIMESTAMP WITHOUT TIME ZONE NOT NULL,
	mainpictureid UUID,
	manufacturerid UUID NOT NULL,
	categoryid UUID,
	brandid UUID NOT NULL,
	
	CONSTRAINT pk_products PRIMARY KEY (id),
	CONSTRAINT FK_products_files_mainpictureid FOREIGN KEY (mainpictureid) REFERENCES files (id),
	CONSTRAINT FK_products_manufacturers_manufacturerid FOREIGN KEY (manufacturerid) REFERENCES manufacturers (id),
	CONSTRAINT FK_products_categories_categoryid FOREIGN KEY (categoryid) REFERENCES categories (id) ON DELETE SET NULL,
	CONSTRAINT FK_products_brands_brandid FOREIGN KEY (brandid) REFERENCES brands (id)
);

CREATE INDEX idx_products_manufacturerid ON products (manufacturerid);
CREATE INDEX idx_products_categoryid ON products (categoryid);
CREATE INDEX idx_products_brandid ON products (brandid);