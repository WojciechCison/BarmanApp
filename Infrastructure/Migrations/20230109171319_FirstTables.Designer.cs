﻿// <auto-generated />
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(CoctailsContext))]
    [Migration("20230109171319_FirstTables")]
    partial class FirstTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.CoctailEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Coctails");
                });

            modelBuilder.Entity("Domain.Entities.CoctailIngridientEntity", b =>
                {
                    b.Property<int>("CoctailId")
                        .HasColumnType("int");

                    b.Property<int>("IngridientId")
                        .HasColumnType("int");

                    b.Property<double>("Dose")
                        .HasColumnType("float");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CoctailId", "IngridientId");

                    b.HasIndex("IngridientId");

                    b.ToTable("CoctailIngridients");
                });

            modelBuilder.Entity("Domain.Entities.IngridientEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Ingridients");
                });

            modelBuilder.Entity("Domain.Entities.UserCoctailEntity", b =>
                {
                    b.Property<int>("CoctailId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CoctailId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserCoctails");
                });

            modelBuilder.Entity("Domain.Entities.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Entities.CoctailIngridientEntity", b =>
                {
                    b.HasOne("Domain.Entities.CoctailEntity", "Coctail")
                        .WithMany("CoctailIngridients")
                        .HasForeignKey("CoctailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.IngridientEntity", "Ingridient")
                        .WithMany("CoctailIngridients")
                        .HasForeignKey("IngridientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coctail");

                    b.Navigation("Ingridient");
                });

            modelBuilder.Entity("Domain.Entities.UserCoctailEntity", b =>
                {
                    b.HasOne("Domain.Entities.CoctailEntity", "Coctail")
                        .WithMany("UserCoctails")
                        .HasForeignKey("CoctailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.UserEntity", "User")
                        .WithMany("FavoriteCoctailsList")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coctail");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.CoctailEntity", b =>
                {
                    b.Navigation("CoctailIngridients");

                    b.Navigation("UserCoctails");
                });

            modelBuilder.Entity("Domain.Entities.IngridientEntity", b =>
                {
                    b.Navigation("CoctailIngridients");
                });

            modelBuilder.Entity("Domain.Entities.UserEntity", b =>
                {
                    b.Navigation("FavoriteCoctailsList");
                });
#pragma warning restore 612, 618
        }
    }
}
