CREATE TABLE productreviews
(
	id UUID NOT NULL,
	title VARCHAR NOT NULL,
	comment VARCHAR NOT NULL,
	createdate TIMESTAMP WITHOUT TIME ZONE NOT NULL,
	rating INT NOT NULL,
	authorid UUID NOT NULL,
	productid UUID NOT NULL,
	
	CONSTRAINT pk_productreviews PRIMARY KEY (id),
	CONSTRAINT fk_productreviews_users_authorid FOREIGN KEY (authorid) REFERENCES users (id) ON DELETE CASCADE,
	CONSTRAINT fk_productreviews_products_productid FOREIGN KEY (productid) REFERENCES products (id) ON DELETE CASCADE
);

CREATE INDEX idx_productreviews_productid ON productreviews (productid);