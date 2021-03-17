﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZhuoYue.Components.Application.EntityFrameworkCore;

namespace ZhuoYue.Components.Application.EntityFrameworkCore.SqlServer.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ZhuoYue.Components.Application.EntityFrameworkCore.AppEntity", b =>
                {
                    b.Property<string>("AppId")
                        .HasColumnType("nvarchar(36)")
                        .HasMaxLength(36);

                    b.Property<string>("AppName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("AppRemarks")
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(4096);

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<DateTime?>("LastUpdatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastUpdatedUserId")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("AppId");

                    b.ToTable("App");
                });
#pragma warning restore 612, 618
        }
    }
}
