CREATE TABLE files
(
	id UUID NOT NULL,
	name VARCHAR NOT NULL,
	contenttype VARCHAR NOT NULL,
	checksum VARCHAR NOT NULL,
	content bytea NOT NULL,
	
	CONSTRAINT pk_files PRIMARY KEY (id)
);