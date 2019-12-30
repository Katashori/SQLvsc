﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SQLvcs.Models;

namespace SQLvcs.Migrations
{
    [DbContext(typeof(SQLvcsContext))]
    partial class SQLvcsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0");

            modelBuilder.Entity("SQLvcs.Models.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ClientId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("SQLvcs.Models.Dacpac", b =>
                {
                    b.Property<int>("DacpacId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DacpacName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DacpacPath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("DatabaseId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Uploaded")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("DacpacId");

                    b.HasIndex("DatabaseId");

                    b.HasIndex("UserId");

                    b.ToTable("Dacpacs");
                });

            modelBuilder.Entity("SQLvcs.Models.Database", b =>
                {
                    b.Property<int>("DatabaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DatabaseName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("InstanceId")
                        .HasColumnType("INTEGER");

                    b.HasKey("DatabaseId");

                    b.HasIndex("InstanceId");

                    b.ToTable("Databases");
                });

            modelBuilder.Entity("SQLvcs.Models.Instance", b =>
                {
                    b.Property<int>("InstanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("InstanceName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ProjectId")
                        .HasColumnType("INTEGER");

                    b.HasKey("InstanceId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Instances");
                });

            modelBuilder.Entity("SQLvcs.Models.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClientId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ProjectId");

                    b.HasIndex("ClientId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("SQLvcs.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SQLvcs.Models.Dacpac", b =>
                {
                    b.HasOne("SQLvcs.Models.Database", "Databases")
                        .WithMany()
                        .HasForeignKey("DatabaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SQLvcs.Models.User", "Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SQLvcs.Models.Database", b =>
                {
                    b.HasOne("SQLvcs.Models.Instance", "Instances")
                        .WithMany()
                        .HasForeignKey("InstanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SQLvcs.Models.Instance", b =>
                {
                    b.HasOne("SQLvcs.Models.Project", "Projects")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SQLvcs.Models.Project", b =>
                {
                    b.HasOne("SQLvcs.Models.Client", "Clients")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
