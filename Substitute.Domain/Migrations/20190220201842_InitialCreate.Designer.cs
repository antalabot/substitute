﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Substitute.Domain.DataStore.Impl;
using Substitute.Domain.Enums;

namespace Substitute.Domain.Migrations
{
    [DbContext(typeof(PgContext))]
    [Migration("20190220201842_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:Enum:e_access_level", "owner,administrator,moderator,user")
                .HasAnnotation("Npgsql:Enum:e_role", "owner,user")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Substitute.Domain.Data.Entities.Guild", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<decimal, decimal>(v => default(decimal), v => default(decimal), new ConverterMappingHints(precision: 20, scale: 0)));

                    b.Property<string>("IconUrl");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<decimal>("OwnerId")
                        .HasConversion(new ValueConverter<decimal, decimal>(v => default(decimal), v => default(decimal), new ConverterMappingHints(precision: 20, scale: 0)));

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Guilds");
                });

            modelBuilder.Entity("Substitute.Domain.Data.Entities.GuildRole", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<decimal, decimal>(v => default(decimal), v => default(decimal), new ConverterMappingHints(precision: 20, scale: 0)));

                    b.Property<EAccessLevel>("AccessLevel");

                    b.Property<decimal>("GuildId")
                        .HasConversion(new ValueConverter<decimal, decimal>(v => default(decimal), v => default(decimal), new ConverterMappingHints(precision: 20, scale: 0)));

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("GuildId");

                    b.ToTable("GuildRoles");
                });

            modelBuilder.Entity("Substitute.Domain.Data.Entities.ImageResponse", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<decimal, decimal>(v => default(decimal), v => default(decimal), new ConverterMappingHints(precision: 20, scale: 0)));

                    b.Property<string>("Command")
                        .IsRequired();

                    b.Property<decimal?>("GuildId")
                        .HasConversion(new ValueConverter<decimal, decimal>(v => default(decimal), v => default(decimal), new ConverterMappingHints(precision: 20, scale: 0)));

                    b.HasKey("Id");

                    b.HasIndex("GuildId");

                    b.ToTable("ImageReponses");
                });

            modelBuilder.Entity("Substitute.Domain.Data.Entities.User", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<decimal, decimal>(v => default(decimal), v => default(decimal), new ConverterMappingHints(precision: 20, scale: 0)));

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<int>("DiscriminatorValue");

                    b.Property<string>("IconUrl");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<ERole>("Role");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Substitute.Domain.Data.Entities.Guild", b =>
                {
                    b.HasOne("Substitute.Domain.Data.Entities.User", "Owner")
                        .WithMany("OwnedGuilds")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Substitute.Domain.Data.Entities.GuildRole", b =>
                {
                    b.HasOne("Substitute.Domain.Data.Entities.Guild", "Guild")
                        .WithMany("GuildRoles")
                        .HasForeignKey("GuildId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Substitute.Domain.Data.Entities.ImageResponse", b =>
                {
                    b.HasOne("Substitute.Domain.Data.Entities.Guild", "Guild")
                        .WithMany("ImageResponses")
                        .HasForeignKey("GuildId");
                });
#pragma warning restore 612, 618
        }
    }
}
