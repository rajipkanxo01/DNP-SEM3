﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAPI.Data;

#nullable disable

namespace WebAPI.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230612202707_ChangedDescription")]
    partial class ChangedDescription
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("Domains.Album", b =>
                {
                    b.Property<string>("Title")
                        .HasMaxLength(25)
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.HasKey("Title");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("Domains.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AlbumTitle")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("TEXT");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AlbumTitle");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Domains.Image", b =>
                {
                    b.HasOne("Domains.Album", null)
                        .WithMany("AllImages")
                        .HasForeignKey("AlbumTitle");
                });

            modelBuilder.Entity("Domains.Album", b =>
                {
                    b.Navigation("AllImages");
                });
#pragma warning restore 612, 618
        }
    }
}
