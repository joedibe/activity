﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineActivity.EFCore.Infra;

namespace OnlineActivity.EFCore.Infra.Migrations
{
    [DbContext(typeof(OnlineActivityDbContext))]
    partial class OnlineActivityDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OnlineActivity.EFCore.Domain.Models.Category", b =>
                {
                    b.Property<Guid>("CategoryID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryName")
                        .HasMaxLength(100);

                    b.Property<string>("Description")
                        .HasMaxLength(300);

                    b.HasKey("CategoryID");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("OnlineActivity.EFCore.Domain.Models.Document", b =>
                {
                    b.Property<Guid>("DocumentID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("DocumentName")
                        .IsRequired();

                    b.Property<bool>("IsActive");

                    b.Property<decimal>("Price");

                    b.HasKey("DocumentID");

                    b.ToTable("Document");
                });
#pragma warning restore 612, 618
        }
    }
}
