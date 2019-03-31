ALTER TABLE "GuildRoles" DROP CONSTRAINT "FK_GuildRoles_Guilds_GuildId";

ALTER TABLE "ImageReponses" DROP CONSTRAINT "FK_ImageReponses_Guilds_GuildId";

DROP TABLE "Guilds";

DROP INDEX "IX_ImageReponses_GuildId";

DROP INDEX "IX_GuildRoles_GuildId";

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20190331110209_GuildsRemoval', '2.2.2-servicing-10034');

