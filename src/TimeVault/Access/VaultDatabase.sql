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
PRAGMA writable_schema=ON;
CREATE TABLE IF NOT EXISTS sqlite_sequence(name,seq);
DELETE FROM sqlite_sequence;
INSERT INTO sqlite_sequence VALUES('Exclusion',0);
INSERT INTO sqlite_sequence VALUES('Selection',0);
CREATE UNIQUE INDEX "Exclusion_PK" ON "Exclusion" (
	"Id"
);
CREATE UNIQUE INDEX "Selection_PK" ON "Selection" (
	"Id"
);
PRAGMA writable_schema=OFF;
COMMIT;
