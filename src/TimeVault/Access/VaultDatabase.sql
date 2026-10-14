PRAGMA foreign_keys=OFF;
BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Exclusion" (
	"Id"	INTEGER NOT NULL,
	"Directory"	INTEGER NOT NULL CHECK("Directory" IN (0, 1)),
	"Pattern"	TEXT NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Selection" (
	"Id"	INTEGER NOT NULL,
	"Directory"	INTEGER NOT NULL CHECK("Directory" IN (0, 1)),
	"Path"	TEXT NOT NULL,
	"Selected"	INTEGER NOT NULL CHECK("Selected" IN (0, 1)),
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Files" (
	"Id"	INTEGER NOT NULL,
	"Path"	TEXT NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "FileVersions" (
	"Id"	INTEGER NOT NULL,
	"FileId"	INTEGER NOT NULL,
	"Number"	INTEGER NOT NULL,
	"LastWrite"	INTEGER NOT NULL,
	"Stored"	INTEGER NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT),
	CONSTRAINT "FK_Versions_Files" FOREIGN KEY("FileId") REFERENCES "Files"("Id") ON DELETE CASCADE
);
PRAGMA writable_schema=ON;
CREATE TABLE IF NOT EXISTS sqlite_sequence(name,seq);
DELETE FROM sqlite_sequence;
INSERT INTO sqlite_sequence VALUES('Exclusion',0);
INSERT INTO sqlite_sequence VALUES('Selection',0);
INSERT INTO sqlite_sequence VALUES('Files',0);
INSERT INTO sqlite_sequence VALUES('FileVersions',0);
CREATE UNIQUE INDEX "IDX_Files_Path" ON "Files" (
	"Path"
);
CREATE INDEX "IDX_FileVersions_Latest" ON "FileVersions" (
	"FileId",
	"Number"	DESC,
	"LastWrite"	DESC
);
CREATE INDEX "IDX_FileVersions_Stored" ON "FileVersions" (
	"Stored"
);
PRAGMA writable_schema=OFF;
COMMIT;
