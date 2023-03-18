﻿// <auto-generated />

using Duende.AdminPanel.Admin.EntityFramework.Shared.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Duende.AdminPanel.Admin.EntityFramework.MySql.Migrations.IdentityServerGrants
{
  [DbContext(typeof(IdentityServerPersistedGrantDbContext))]
  [Migration("20201108173143_UpdateIdentityServerToVersion4")]
  partial class UpdateIdentityServerToVersion4
  {
    protected override void BuildTargetModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
      modelBuilder
          .HasAnnotation("ProductVersion", "3.1.9")
          .HasAnnotation("Relational:MaxIdentifierLength", 64);

      modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.DeviceFlowCodes", b =>
          {
            b.Property<string>("UserCode")
                      .HasColumnType("varchar(200) CHARACTER SET utf8mb4")
                      .HasMaxLength(200);

            b.Property<string>("ClientId")
                      .IsRequired()
                      .HasColumnType("varchar(200) CHARACTER SET utf8mb4")
                      .HasMaxLength(200);

            b.Property<DateTime>("CreationTime")
                      .HasColumnType("datetime(6)");

            b.Property<string>("Data")
                      .IsRequired()
                      .HasColumnType("longtext CHARACTER SET utf8mb4")
                      .HasMaxLength(50000);

            b.Property<string>("Description")
                      .HasColumnType("varchar(200) CHARACTER SET utf8mb4")
                      .HasMaxLength(200);

            b.Property<string>("DeviceCode")
                      .IsRequired()
                      .HasColumnType("varchar(200) CHARACTER SET utf8mb4")
                      .HasMaxLength(200);

            b.Property<DateTime?>("Expiration")
                      .IsRequired()
                      .HasColumnType("datetime(6)");

            b.Property<string>("SessionId")
                      .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                      .HasMaxLength(100);

            b.Property<string>("SubjectId")
                      .HasColumnType("varchar(200) CHARACTER SET utf8mb4")
                      .HasMaxLength(200);

            b.HasKey("UserCode");

            b.HasIndex("DeviceCode")
                      .IsUnique();

            b.HasIndex("Expiration");

            b.ToTable("DeviceCodes");
          });

      modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.PersistedGrant", b =>
          {
            b.Property<string>("Key")
                      .HasColumnType("varchar(200) CHARACTER SET utf8mb4")
                      .HasMaxLength(200);

            b.Property<string>("ClientId")
                      .IsRequired()
                      .HasColumnType("varchar(200) CHARACTER SET utf8mb4")
                      .HasMaxLength(200);

            b.Property<DateTime?>("ConsumedTime")
                      .HasColumnType("datetime(6)");

            b.Property<DateTime>("CreationTime")
                      .HasColumnType("datetime(6)");

            b.Property<string>("Data")
                      .IsRequired()
                      .HasColumnType("longtext CHARACTER SET utf8mb4")
                      .HasMaxLength(50000);

            b.Property<string>("Description")
                      .HasColumnType("varchar(200) CHARACTER SET utf8mb4")
                      .HasMaxLength(200);

            b.Property<DateTime?>("Expiration")
                      .HasColumnType("datetime(6)");

            b.Property<string>("SessionId")
                      .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                      .HasMaxLength(100);

            b.Property<string>("SubjectId")
                      .HasColumnType("varchar(200) CHARACTER SET utf8mb4")
                      .HasMaxLength(200);

            b.Property<string>("Type")
                      .IsRequired()
                      .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                      .HasMaxLength(50);

            b.HasKey("Key");

            b.HasIndex("Expiration");

            b.HasIndex("SubjectId", "ClientId", "Type");

            b.HasIndex("SubjectId", "SessionId", "Type");

            b.ToTable("PersistedGrants");
          });
#pragma warning restore 612, 618
    }
  }
}








