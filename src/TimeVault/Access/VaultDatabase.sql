PRAGMA foreign_keys=OFF;
BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Folder" (
	"Id"	INTEGER NOT NULL,
	"Path"	TEXT NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Exclusion" (
	"Id"	INTEGER NOT NULL,
	"Directory"	INTEGER NOT NULL CHECK("Directory" IN (0, 1)),
	"Pattern"	TEXT NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
PRAGMA writable_schema=ON;
CREATE TABLE IF NOT EXISTS sqlite_sequence(name,seq);
DELETE FROM sqlite_sequence;
INSERT INTO sqlite_sequence VALUES('Exclusion',0);
PRAGMA writable_schema=OFF;
COMMIT;
