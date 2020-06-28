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