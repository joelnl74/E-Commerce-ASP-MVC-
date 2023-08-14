using e_commerce_application_web.Models;
using e_commerce_data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_application_web.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, DisplayOrder = 0, Name = "Category A"},
                new Category { Id = 2, DisplayOrder = 1, Name = "Category B" },
                new Category { Id = 3, DisplayOrder = 2, Name = "Category C" }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Price = 0, Title = "Title A", Author = "A", ISBN = "12345", ImageUrl = "", CategoryId = 1, Description = "Description A" },
                new Product { Id = 2, Price = 1, Title = "Title B", Author = "B", ISBN = "23451", ImageUrl = "",  CategoryId = 2, Description = "Description B" },
                new Product { Id = 3, Price = 2, Title = "Title C", Author = "B", ISBN = "51235", ImageUrl = "", CategoryId = 3, Description = "Description C" }
                );
        }
    }

}
