CREATE TABLE roles
(
	id UUID NOT NULL,
	name VARCHAR NOT NULL,
	description VARCHAR,
	
	CONSTRAINT pk_roles PRIMARY KEY (id)
);