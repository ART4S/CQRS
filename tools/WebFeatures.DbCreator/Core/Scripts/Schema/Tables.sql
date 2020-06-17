DO 
$tables$ BEGIN

CREATE TABLE files
(
	id UUID NOT NULL,
	name VARCHAR NOT NULL,
	contenttype VARCHAR NOT NULL,
	checksum VARCHAR NOT NULL,
	content bytea NOT NULL,
	
	CONSTRAINT pk_files PRIMARY KEY (id)
);

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

CREATE TABLE roles
(
	id UUID NOT NULL,
	name VARCHAR NOT NULL,
	description VARCHAR,
	
	CONSTRAINT pk_roles PRIMARY KEY (id)
);

CREATE TABLE userroles
(
	id UUID NOT NULL,
	userid UUID NOT NULL,
	roleid UUID NOT NULL,
	
	CONSTRAINT pk_userroles PRIMARY KEY (id),
	CONSTRAINT fk_userroles_users_userid FOREIGN KEY (userid) REFERENCES users (id) ON DELETE CASCADE,
	CONSTRAINT fk_userroles_roles_roleid FOREIGN KEY (roleid) REFERENCES roles (id) ON DELETE CASCADE
);

CREATE INDEX idx_userroles_userid ON userroles (userid);

CREATE TABLE rolepermissions
(
	id UUID NOT NULL,
	roleid UUID NOT NULL,
	name VARCHAR NOT NULL,

	CONSTRAINT pk_rolepermissions PRIMARY KEY (id),
	CONSTRAINT fk_rolepermissions_roles_roleid FOREIGN KEY (roleid) REFERENCES roles (id) ON DELETE CASCADE
);

CREATE INDEX idx_rolepermissions_roleid ON rolepermissions (roleid);

CREATE TABLE countries
(
	id UUID NOT NULL,
	name VARCHAR NOT NULL,
	continent VARCHAR NOT NULL,
	
	CONSTRAINT pk_countries PRIMARY KEY (id)
);

CREATE TABLE cities
(
	id UUID NOT NULL,
	name VARCHAR NOT NULL,
	countryid UUID NOT NULL,
	
	CONSTRAINT pk_cities PRIMARY KEY (id),
	CONSTRAINT fk_cities_countries_countryid FOREIGN KEY (countryid) REFERENCES countries (id)
);

CREATE INDEX idx_cities_countryid ON cities (countryid);

CREATE TABLE manufacturers
(
	id UUID NOT NULL,
	organizationname VARCHAR NOT NULL,
	homepageurl VARCHAR,
	streetaddress_streetname VARCHAR NOT NULL,
	streetaddress_postalcode VARCHAR NOT NULL,
	streetaddress_cityid UUID NOT NULL,
	
	CONSTRAINT pk_manufacturers PRIMARY KEY (id),
	CONSTRAINT fk_manufacturers_cities_streetaddress_cityid FOREIGN KEY (streetaddress_cityid) REFERENCES cities (id)
);

CREATE INDEX idx_manufacturers_streetaddress_cityid ON manufacturers (streetaddress_cityid);

CREATE TABLE categories
(
	id UUID NOT NULL,
	name VARCHAR NOT NULL,
	
	CONSTRAINT pk_categories PRIMARY KEY (id)
);

CREATE TABLE brands
(
	id UUID NOT NULL,
	name VARCHAR NOT NULL,
	
	CONSTRAINT pk_brands PRIMARY KEY (id)
);

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

CREATE TABLE shippers
(
	id UUID NOT NULL,
	organizationname VARCHAR NOT NULL,
	contactphone VARCHAR,
	headoffice_streetname VARCHAR NOT NULL,
	headoffice_postalcode VARCHAR NOT NULL,
	headoffice_cityid UUID NOT NULL,
	
	CONSTRAINT pk_shippers PRIMARY KEY (id),
	CONSTRAINT pk_shippers_cities_headoffice_cityid FOREIGN KEY (headoffice_cityid) REFERENCES cities (id)
);

CREATE INDEX idx_shippers_headoffice_cityid ON shippers (headoffice_cityid);

END $tables$;