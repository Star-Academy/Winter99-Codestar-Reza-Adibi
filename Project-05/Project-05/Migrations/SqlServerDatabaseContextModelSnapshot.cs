﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Project_05;
using System.Diagnostics.CodeAnalysis;

namespace Project05.Migrations {
    [ExcludeFromCodeCoverage]
    [DbContext(typeof(SqlDatabaseContext))]
    partial class SqlServerDatabaseContextModelSnapshot : ModelSnapshot {
        protected override void BuildModel(ModelBuilder modelBuilder) {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DocumentToken", b => {
                b.Property<int>("DocumentsID")
                    .HasColumnType("int");

                b.Property<int>("TokensID")
                    .HasColumnType("int");

                b.HasKey("DocumentsID", "TokensID");

                b.HasIndex("TokensID");

                b.ToTable("DocumentToken");
            });

            modelBuilder.Entity("Project_05.Document", b => {
                b.Property<int>("ID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("DocumentPath")
                    .HasColumnType("nvarchar(450)");

                b.HasKey("ID");

                b.HasIndex("DocumentPath")
                    .IsUnique()
                    .HasFilter("[DocumentPath] IS NOT NULL");

                b.ToTable("Documents");
            });

            modelBuilder.Entity("Project_05.Token", b => {
                b.Property<int>("ID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("TokenText")
                    .HasColumnType("nvarchar(450)");

                b.HasKey("ID");

                b.HasIndex("TokenText")
                    .IsUnique()
                    .HasFilter("[TokenText] IS NOT NULL");

                b.ToTable("Tokens");
            });

            modelBuilder.Entity("DocumentToken", b => {
                b.HasOne("Project_05.Document", null)
                    .WithMany()
                    .HasForeignKey("DocumentsID")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("Project_05.Token", null)
                    .WithMany()
                    .HasForeignKey("TokensID")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });
#pragma warning restore 612, 618
        }
    }
}
