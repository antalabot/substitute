ALTER TABLE "Users" DROP COLUMN "Discriminator";

ALTER TABLE "Users" DROP COLUMN "DiscriminatorValue";

ALTER TABLE "Users" DROP COLUMN "IconUrl";

ALTER TABLE "Users" DROP COLUMN "Name";

ALTER TABLE "ImageReponses" ADD "Fielename" text NOT NULL DEFAULT '';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20190326190723_DatabaseCleanup', '2.2.2-servicing-10034');

