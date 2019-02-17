CREATE TABLE files (
	Id       NVARCHAR(36) PRIMARY KEY,
	Name     NVARCHAR,
	Revision INTEGER,
	Contents BLOB
);