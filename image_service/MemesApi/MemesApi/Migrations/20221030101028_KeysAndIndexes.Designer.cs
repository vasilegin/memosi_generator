﻿// <auto-generated />
using System;
using MemesApi.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MemesApi.Migrations
{
    [DbContext(typeof(MemeContext))]
    [Migration("20221030101028_KeysAndIndexes")]
    partial class KeysAndIndexes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.10");

            modelBuilder.Entity("MemesApi.Db.Models.Estimate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("FileId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Score")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("FileId");

                    b.ToTable("Estimates");
                });

            modelBuilder.Entity("MemesApi.Db.Models.FileMeta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Format")
                        .HasColumnType("TEXT");

                    b.Property<int>("TotalEstimates")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Metas");
                });

            modelBuilder.Entity("MemesApi.Db.Models.MemeFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("MetaId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("MetaId")
                        .IsUnique();

                    b.ToTable("Files");
                });

            modelBuilder.Entity("MemesApi.Db.Models.Estimate", b =>
                {
                    b.HasOne("MemesApi.Db.Models.MemeFile", "File")
                        .WithMany("Estimates")
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("File");
                });

            modelBuilder.Entity("MemesApi.Db.Models.MemeFile", b =>
                {
                    b.HasOne("MemesApi.Db.Models.FileMeta", "Meta")
                        .WithOne()
                        .HasForeignKey("MemesApi.Db.Models.MemeFile", "MetaId");

                    b.Navigation("Meta");
                });

            modelBuilder.Entity("MemesApi.Db.Models.MemeFile", b =>
                {
                    b.Navigation("Estimates");
                });
#pragma warning restore 612, 618
        }
    }
}
