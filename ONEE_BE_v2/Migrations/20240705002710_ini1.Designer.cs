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
    [Migration("20240705002710_ini1")]
    partial class ini1
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

                    b.Property<string>("CINE")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Emploiprecedent")
                        .HasColumnType("longtext");

                    b.Property<string>("Nom")
                        .HasColumnType("longtext");

                    b.Property<int>("OffreId")
                        .HasColumnType("int");

                    b.Property<string>("Prenom")
                        .HasColumnType("longtext");

                    b.Property<string>("Spécialite")
                        .HasColumnType("longtext");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("TELEphone")
                        .HasColumnType("int");

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

                    b.HasIndex("OffreId");

                    b.ToTable("Candidatures");
                });

            modelBuilder.Entity("ONEE_BE_v2.Models.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CandidatureId")
                        .HasColumnType("int");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CandidatureId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("ONEE_BE_v2.Models.Offre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("CentreConcours")
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Diplome")
                        .HasColumnType("longtext");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Specialite")
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

            modelBuilder.Entity("ONEE_BE_v2.Models.Candidature", b =>
                {
                    b.HasOne("ONEE_BE_v2.Models.Offre", "Offre")
                        .WithMany("Candidatures")
                        .HasForeignKey("OffreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Offre");
                });

            modelBuilder.Entity("ONEE_BE_v2.Models.Document", b =>
                {
                    b.HasOne("ONEE_BE_v2.Models.Candidature", "Candidature")
                        .WithMany("Documents")
                        .HasForeignKey("CandidatureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Candidature");
                });

            modelBuilder.Entity("ONEE_BE_v2.Models.Candidature", b =>
                {
                    b.Navigation("Documents");
                });

            modelBuilder.Entity("ONEE_BE_v2.Models.Offre", b =>
                {
                    b.Navigation("Candidatures");
                });
#pragma warning restore 612, 618
        }
    }
}