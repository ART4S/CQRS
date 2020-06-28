CREATE TABLE productcomments
(
	id UUID NOT NULL,
	body VARCHAR NOT NULL,
	createdate TIMESTAMP WITHOUT TIME ZONE NOT NULL,
	productid UUID NOT NULL,
	authorid UUID NOT NULL,
	parentcommentid UUID,
	
	CONSTRAINT pk_productcomments PRIMARY KEY (id),
	CONSTRAINT fk_productcomments_products_productid FOREIGN KEY (productid) REFERENCES products (id) ON DELETE CASCADE,
	CONSTRAINT fk_productcomments_users_AuthorId FOREIGN KEY (authorid) REFERENCES users (id) ON DELETE CASCADE,
	CONSTRAINT fk_productcomments_productcomments_parentcommentid FOREIGN KEY (parentcommentid) REFERENCES productcomments (id) ON DELETE CASCADE
);

CREATE INDEX idx_productcomments_productid ON productcomments (productid);