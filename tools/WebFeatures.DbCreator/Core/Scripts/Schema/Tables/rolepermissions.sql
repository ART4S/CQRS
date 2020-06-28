CREATE TABLE rolepermissions
(
	id UUID NOT NULL,
	roleid UUID NOT NULL,
	name VARCHAR NOT NULL,

	CONSTRAINT pk_rolepermissions PRIMARY KEY (id),
	CONSTRAINT fk_rolepermissions_roles_roleid FOREIGN KEY (roleid) REFERENCES roles (id) ON DELETE CASCADE
);

CREATE INDEX idx_rolepermissions_roleid ON rolepermissions (roleid);