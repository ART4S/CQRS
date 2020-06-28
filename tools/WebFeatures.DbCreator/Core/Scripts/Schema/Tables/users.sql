CREATE TABLE users
(
	id UUID NOT NULL,
	name VARCHAR NOT NULL,
	email VARCHAR NOT NULL UNIQUE,
	passwordhash VARCHAR NOT NULL,
	pictureid UUID,
	
	CONSTRAINT pk_users PRIMARY KEY (id),
	CONSTRAINT fk_users_files_pictureid FOREIGN KEY (pictureid) REFERENCES files (id),
	CONSTRAINT ck_users_email_not_empty CHECK (email <> ''),
	CONSTRAINT ck_users_passwordhash_not_empty CHECK (passwordhash <> '')
);

CREATE INDEX idx_users_email ON users (email);