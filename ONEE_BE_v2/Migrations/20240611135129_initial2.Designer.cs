﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ONEE_BE_v2.Context;

#nullable disable

namespace ONEE_BE_v2.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240611135129_initial2")]
    partial class initial2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("ONEE_BE_v2.Models.Candidature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adresse")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Emploiprecedent")
                        .HasColumnType("longtext");

                    b.Property<string>("Nom")
                        .HasColumnType("longtext");

                    b.Property<string>("Prenom")
                        .HasColumnType("longtext");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("datenaissance")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("datepostulation")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("description")
                        .HasColumnType("longtext");

                    b.Property<string>("motdepasse")
                        .HasColumnType("longtext");

                    b.Property<string>("nommere")
                        .HasColumnType("longtext");

                    b.Property<string>("nompere")
                        .HasColumnType("longtext");

                    b.Property<string>("statusfamiliale")
                        .HasColumnType("longtext");

                    b.Property<string>("ville")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Candidatures");
                });

            modelBuilder.Entity("ONEE_BE_v2.Models.Offre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Titre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("dateDebut")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("dateFin")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("nbr_places")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Offres");
                });

            modelBuilder.Entity("ONEE_BE_v2.Models.Recruteur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Matricule")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MotDePasse")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Recruteurs");
                });
#pragma warning restore 612, 618
        }
    }
}
