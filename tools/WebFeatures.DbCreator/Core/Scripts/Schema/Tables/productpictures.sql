CREATE TABLE productpictures
(
	id UUID NOT NULL,
	productid UUID NOT NULL,
	fileid UUID NOT NULL,
	
	CONSTRAINT pk_productpictures PRIMARY KEY (id),
	CONSTRAINT fk_productpictures_products_productid FOREIGN KEY (productid) REFERENCES products (id) ON DELETE CASCADE,
	CONSTRAINT fk_productpictures_files_fileid FOREIGN KEY (fileid) REFERENCES files (id) ON DELETE CASCADE
);

CREATE INDEX idx_productpictures_productid ON productpictures (productid);