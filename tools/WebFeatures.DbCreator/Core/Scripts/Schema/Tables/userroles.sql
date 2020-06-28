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