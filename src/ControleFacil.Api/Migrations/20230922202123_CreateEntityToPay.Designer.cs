﻿// <auto-generated />
using System;
using ControleFacil.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ControleFacil.Api.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20230922202123_CreateEntityToPay")]
    partial class CreateEntityToPay
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ControleFacil.Api.Domain.Models.NatureRelease", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Details")
                        .HasColumnType("VARCHAR");

                    b.Property<DateTime?>("InactivationDate")
                        .HasColumnType("timestamp");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("timestamp");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("naturerelease", (string)null);
                });

            modelBuilder.Entity("ControleFacil.Api.Domain.Models.ToPay", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Details")
                        .HasColumnType("VARCHAR");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("timestamp");

                    b.Property<DateTime?>("InactivationDate")
                        .HasColumnType("timestamp");

                    b.Property<Guid>("NatureReleaseId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("OriginalValue")
                        .HasColumnType("decimal");

                    b.Property<DateTime?>("PaidDate")
                        .HasColumnType("timestamp");

                    b.Property<decimal>("PaidValue")
                        .HasColumnType("decimal");

                    b.Property<DateTime?>("ReferenceDate")
                        .HasColumnType("timestamp");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("timestamp");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("NatureReleaseId");

                    b.HasIndex("UserId");

                    b.ToTable("toPay", (string)null);
                });

            modelBuilder.Entity("ControleFacil.Api.Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.Property<DateTime?>("InactivationDate")
                        .HasColumnType("timestamp");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("timestamp");

                    b.HasKey("Id");

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("ControleFacil.Api.Domain.Models.NatureRelease", b =>
                {
                    b.HasOne("ControleFacil.Api.Domain.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ControleFacil.Api.Domain.Models.ToPay", b =>
                {
                    b.HasOne("ControleFacil.Api.Domain.Models.NatureRelease", "NatureRelease")
                        .WithMany()
                        .HasForeignKey("NatureReleaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ControleFacil.Api.Domain.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NatureRelease");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
