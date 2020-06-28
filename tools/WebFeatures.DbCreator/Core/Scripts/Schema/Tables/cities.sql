CREATE TABLE cities
(
	id UUID NOT NULL,
	name VARCHAR NOT NULL,
	countryid UUID NOT NULL,
	
	CONSTRAINT pk_cities PRIMARY KEY (id),
	CONSTRAINT fk_cities_countries_countryid FOREIGN KEY (countryid) REFERENCES countries (id)
);

CREATE INDEX idx_cities_countryid ON cities (countryid);