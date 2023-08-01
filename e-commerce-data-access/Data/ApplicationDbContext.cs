using e_commerce_application_web.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_application_web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, DisplayOrder = 0, Name = "Category A"},
                new Category { Id = 2, DisplayOrder = 1, Name = "Category B" },
                new Category { Id = 3, DisplayOrder = 2, Name = "Category C" }
                );
        }
    }

}
