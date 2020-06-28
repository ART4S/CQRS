CREATE FUNCTION user_has_permission(user_id UUID, permission VARCHAR)
RETURNS BOOLEAN AS $$
BEGIN
	RETURN EXISTS (
		SELECT * 
		FROM 
			public.rolepermissions p 
		JOIN 
			public.roles r ON r.id = p.roleid 
		JOIN 
			public.userroles ur ON ur.roleid = r.id 
		WHERE 
			ur.userid = user_id AND p.name = permission);

END; $$ LANGUAGE 'plpgsql';