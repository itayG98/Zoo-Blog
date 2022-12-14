// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Model.DAL;

#nullable disable

namespace Model.Migrations
{
    [DbContext(typeof(ZooDBContext))]
    [Migration("20221109101510_SecondInit")]
    partial class SecondInit
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Model.Models.Animel", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CategoryID")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Animel");

                    b.HasData(
                        new
                        {
                            ID = new Guid("490d7fe2-4f84-4b9f-b0af-5514918c7ed0"),
                            BirthDate = new DateTime(2002, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CategoryID = new Guid("454cc411-e44d-4f97-b6d2-1b6d8a0f403b"),
                            Description = "",
                            Image = "~/Images/Defult.jpg",
                            Name = "Elephent"
                        },
                        new
                        {
                            ID = new Guid("a5522b3b-dedb-46d0-869a-fc393a9ba597"),
                            BirthDate = new DateTime(2009, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CategoryID = new Guid("1201df5a-7224-4d09-b24b-c6bf54410272"),
                            Description = "",
                            Image = "~/Images/Defult.jpg",
                            Name = "Eagel"
                        });
                });

            modelBuilder.Entity("Model.Models.Category", b =>
                {
                    b.Property<Guid>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryID");

                    b.ToTable("Category");

                    b.HasData(
                        new
                        {
                            CategoryID = new Guid("454cc411-e44d-4f97-b6d2-1b6d8a0f403b"),
                            Name = "Mammal"
                        },
                        new
                        {
                            CategoryID = new Guid("1201df5a-7224-4d09-b24b-c6bf54410272"),
                            Name = "Avian"
                        });
                });

            modelBuilder.Entity("Model.Models.Comment", b =>
                {
                    b.Property<Guid>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AnimelID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CommentId");

                    b.HasIndex("AnimelID");

                    b.ToTable("Comment");

                    b.HasData(
                        new
                        {
                            CommentId = new Guid("f6551e2a-3811-4695-99fd-db2340e5fc9d"),
                            AnimelID = new Guid("490d7fe2-4f84-4b9f-b0af-5514918c7ed0"),
                            Content = "Content"
                        },
                        new
                        {
                            CommentId = new Guid("aba04f9e-7b86-46bb-8d9a-5dcf7c17dfd2"),
                            AnimelID = new Guid("a5522b3b-dedb-46d0-869a-fc393a9ba597"),
                            Content = "Content"
                        });
                });

            modelBuilder.Entity("Model.Models.Animel", b =>
                {
                    b.HasOne("Model.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Model.Models.Comment", b =>
                {
                    b.HasOne("Model.Models.Animel", "Animel")
                        .WithMany("Comments")
                        .HasForeignKey("AnimelID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animel");
                });

            modelBuilder.Entity("Model.Models.Animel", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
