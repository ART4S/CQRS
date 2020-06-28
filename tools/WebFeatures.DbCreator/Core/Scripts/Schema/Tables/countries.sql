CREATE TABLE countries
(
	id UUID NOT NULL,
	name VARCHAR NOT NULL,
	continent VARCHAR NOT NULL,
	
	CONSTRAINT pk_countries PRIMARY KEY (id)
);