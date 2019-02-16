CREATE TABLE files (
	Id       INT PRIMARY KEY,
	Name     NVARCHAR,
	Revision INTEGER,
	Contents BLOB
);