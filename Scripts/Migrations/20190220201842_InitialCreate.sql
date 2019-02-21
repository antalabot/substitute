CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

CREATE TYPE e_access_level AS ENUM ('owner', 'administrator', 'moderator', 'user');
CREATE TYPE e_role AS ENUM ('owner', 'user');

CREATE TABLE "Users" (
    "Id" numeric(20,0) NOT NULL,
    "Name" text NOT NULL,
    "Role" e_role NOT NULL,
    "Discriminator" text NOT NULL,
    "DiscriminatorValue" integer NOT NULL,
    "IconUrl" text NULL,
    CONSTRAINT "PK_Users" PRIMARY KEY ("Id")
);

CREATE TABLE "Guilds" (
    "Id" numeric(20,0) NOT NULL,
    "Name" text NOT NULL,
    "IconUrl" text NULL,
    "OwnerId" numeric(20,0) NOT NULL,
    CONSTRAINT "PK_Guilds" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Guilds_Users_OwnerId" FOREIGN KEY ("OwnerId") REFERENCES "Users" ("Id") ON DELETE CASCADE
);

CREATE TABLE "GuildRoles" (
    "Id" numeric(20,0) NOT NULL,
    "GuildId" numeric(20,0) NOT NULL,
    "Name" text NOT NULL,
    "AccessLevel" e_access_level NOT NULL,
    CONSTRAINT "PK_GuildRoles" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_GuildRoles_Guilds_GuildId" FOREIGN KEY ("GuildId") REFERENCES "Guilds" ("Id") ON DELETE CASCADE
);

CREATE TABLE "ImageReponses" (
    "Id" numeric(20,0) NOT NULL,
    "Command" text NOT NULL,
    "GuildId" numeric(20,0) NULL,
    CONSTRAINT "PK_ImageReponses" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_ImageReponses_Guilds_GuildId" FOREIGN KEY ("GuildId") REFERENCES "Guilds" ("Id") ON DELETE RESTRICT
);

CREATE INDEX "IX_GuildRoles_GuildId" ON "GuildRoles" ("GuildId");

CREATE INDEX "IX_Guilds_OwnerId" ON "Guilds" ("OwnerId");

CREATE INDEX "IX_ImageReponses_GuildId" ON "ImageReponses" ("GuildId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20190220201842_InitialCreate', '2.2.2-servicing-10034');

