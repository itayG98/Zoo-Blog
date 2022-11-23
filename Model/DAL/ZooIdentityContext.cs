using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Model.DAL
{
    public class ZooIdentityContext : IdentityDbContext<IdentityUser>
    {
        public ZooIdentityContext(DbContextOptions<ZooIdentityContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(new List<IdentityRole>
            {
                new IdentityRole() {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Admin",
                    NormalizedName = "ADMIN"
            },
                new IdentityRole() {
                    Id = Guid.NewGuid().ToString(),
                    Name = "User",
                    NormalizedName = "USER"
                  },
                });
            IdentityRole adminRole = new IdentityRole() { Name = "Admin" };
            IdentityUser admin = new IdentityUser()
            {
                UserName = "Itay",
                Email = "Stam@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "1234567890",
                PhoneNumberConfirmed = true
            };
        }
    }
}
