using CakeShop.Models;
using Microsoft.EntityFrameworkCore;

namespace CakeShop.DataAccess.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options )
        {
                
        }

        //DB Set 

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name="Fondant",DisplayOrder= 1 },
                new Category { Id = 2, Name = "buiscuits", DisplayOrder = 2 },
                 new Category { Id = 3, Name = "Buns", DisplayOrder = 3 }
                );
        }
    }
}
