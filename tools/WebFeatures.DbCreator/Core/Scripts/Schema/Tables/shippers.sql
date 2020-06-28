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