CREATE FUNCTION get_file(file_id UUID)
RETURNS TABLE
(
	name VARCHAR,
	contenttype VARCHAR,
	checksum VARCHAR,
	content BYTEA
) AS $$
BEGIN
	RETURN QUERY 
	SELECT 
		f.name,
		f.contenttype,
		f.checksum,
		f.content
	FROM
		public.files f
	WHERE 
		f.id = file_id;

END; $$ LANGUAGE 'plpgsql';