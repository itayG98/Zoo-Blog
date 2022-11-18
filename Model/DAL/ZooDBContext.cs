using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using Model.Services;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using static System.Net.Mime.MediaTypeNames;

namespace Model.DAL
{
    public class ZooDBContext : DbContext
    {
        public ZooDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Category> categories = new();
            foreach (string categotyName in Enum.GetNames(typeof(CategoriesEnum)))
            {
                categories.Add(new Category() { Name = categotyName, CategoryID = Guid.NewGuid() });
            }

            Category Mammal = categories.Where(c => c.Name == "Mammal").First();
            Category Avian = categories.Where(c => c.Name == "Avian").First();
            Category Aquadic = categories.Where(c => c.Name == "Aquadic").First();
            Category Insect = categories.Where(c => c.Name == "Insect").First();
            Category Reptile = categories.Where(c => c.Name == "Reptile").First();

            modelBuilder.Entity<Category>().HasData(categories);

            Animel Elephant = new() { ID = Guid.NewGuid(), Name = "Elephent", BirthDate = new DateTime(2002, 6, 12), Description = "Test", CategoryID = Mammal.CategoryID, ImageRawData = ImagesFormater.ImageToBytesArrayFromLocalPath("InitImages/Elephant.jpg") };
            Animel Eagel = new() { ID = Guid.NewGuid(), Name = "Eagel", BirthDate = new DateTime(2009, 12, 12), Description = "Test", CategoryID = Avian.CategoryID, ImageRawData = ImagesFormater.ImageToBytesArrayFromLocalPath("InitImages/Eagle.webp") };
            Animel Squirl = new() { ID = Guid.NewGuid(), Name = "Squirrel", BirthDate = new DateTime(2009, 12, 12), Description = "Test", CategoryID = Mammal.CategoryID, ImageRawData = ImagesFormater.ImageToBytesArrayFromLocalPath("InitImages/SQIRL.jpg") };
            Animel Monkey = new() { ID = Guid.NewGuid(), Name = "Monkey", BirthDate = new DateTime(2011, 12, 12), Description = "Test", CategoryID = Mammal.CategoryID, ImageRawData = ImagesFormater.ImageToBytesArrayFromLocalPath("InitImages/Monkey.jpg") };
            Animel Cow = new() { ID = Guid.NewGuid(), Name = "Cow", BirthDate = new DateTime(2011, 12, 12), Description = "Test", CategoryID = Mammal.CategoryID, ImageRawData = ImagesFormater.ImageToBytesArrayFromLocalPath("InitImages/Cow.jpg") };
            Animel Dolphin = new() { ID = Guid.NewGuid(), Name = "Dolphin", BirthDate = new DateTime(2011, 12, 12), Description = "Test", CategoryID = Mammal.CategoryID, ImageRawData = ImagesFormater.ImageToBytesArrayFromLocalPath("InitImages/Dolphin.jpg") };
            Animel Lion = new() { ID = Guid.NewGuid(), Name = "Lion", BirthDate = new DateTime(2011, 12, 12), Description = "Test", CategoryID = Mammal.CategoryID, ImageRawData = ImagesFormater.ImageToBytesArrayFromLocalPath("InitImages/Lion.jpg") };
            Animel Lizard = new() { ID = Guid.NewGuid(), Name = "Lizard", BirthDate = new DateTime(2011, 12, 12), Description = "Test", CategoryID = Reptile.CategoryID, ImageRawData = ImagesFormater.ImageToBytesArrayFromLocalPath("InitImages/Lizard.jpg") };
            Animel Owl = new() { ID = Guid.NewGuid(), Name = "Owl", BirthDate = new DateTime(2011, 12, 12), Description = "Test", CategoryID = Avian.CategoryID, ImageRawData = ImagesFormater.ImageToBytesArrayFromLocalPath("InitImages/Owl.jpg") };
            Animel Shark = new() { ID = Guid.NewGuid(), Name = "Shark", BirthDate = new DateTime(2011, 12, 12), Description = "Test", CategoryID = Aquadic.CategoryID, ImageRawData = ImagesFormater.ImageToBytesArrayFromLocalPath("InitImages/Shark.webp") };
            Animel Snake = new() { ID = Guid.NewGuid(), Name = "Snake", BirthDate = new DateTime(2011, 12, 12), Description = "Test", CategoryID = Reptile.CategoryID, ImageRawData = ImagesFormater.ImageToBytesArrayFromLocalPath("InitImages/Snake.jpg") };
            Animel Spider = new() { ID = Guid.NewGuid(), Name = "Spider", BirthDate = new DateTime(2011, 12, 12), Description = "Test", CategoryID = Insect.CategoryID, ImageRawData = ImagesFormater.ImageToBytesArrayFromLocalPath("InitImages/Spider.webp") };
            Animel wagtail = new() { ID = Guid.NewGuid(), Name = "Wagtail", BirthDate = new DateTime(2011, 12, 12), Description = "Test", CategoryID = Avian.CategoryID, ImageRawData = ImagesFormater.ImageToBytesArrayFromLocalPath("InitImages/wagtail.jpg") };
            Animel bee = new() { ID = Guid.NewGuid(), Name = "Bee", BirthDate = new DateTime(2011, 12, 12), Description = "Test", CategoryID = Insect.CategoryID, ImageRawData = ImagesFormater.ImageToBytesArrayFromLocalPath("InitImages/Bee.png") };

            modelBuilder.Entity<Animel>().HasData(Elephant, Eagel, Squirl, Monkey, Cow, Dolphin, Lion, Lizard, Owl, Shark, Snake, Spider, wagtail,bee);

            modelBuilder.Entity<Comment>().HasData(
                new Comment() { AnimelID = Eagel.ID, Content = "First init comment", CommentId = Guid.NewGuid() },
                new Comment() { AnimelID = Eagel.ID, Content = "second init comment", CommentId = Guid.NewGuid() },
                new Comment() { AnimelID = Eagel.ID, Content = "Third init comment", CommentId = Guid.NewGuid() },
                new Comment() { AnimelID = Elephant.ID, Content = "Initial comment", CommentId = Guid.NewGuid() }
                );

        }

    }
}
