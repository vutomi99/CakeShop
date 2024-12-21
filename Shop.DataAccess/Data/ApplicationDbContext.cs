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
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 7, Name="Electronics",DisplayOrder= 1 },
                new Category { Id = 8, Name = "Cars", DisplayOrder = 2 },
                new Category { Id = 9, Name = "Home Deco", DisplayOrder = 3 }
                );

            modelBuilder.Entity<Product>().HasData(
                
                new Product
                {
                    Id = 1,
                    Name= "Njk Spark Plugs",
                    Description= "Powerful and reliable for any Engine",
                    ProductId = 00254,
                    supplier="GoldWagen",
                    ListPrice= 90,
                    CategoryId = 7,
                    ImageUrl=""
                },
                  new Product
                  {
                      Id = 2,
                      Name = "Grand Piano",
                      Description = "Powerful 1975 Piano",
                      ProductId = 04,
                      supplier = "Takealot",
                      ListPrice = 47000,
                      CategoryId = 8,
                      ImageUrl = ""
                  },
                    new Product
                    {
                        Id = 3,
                        Name = "Office Table",
                        Description = "12cm,14cm office table with chair",
                        ProductId = 054,
                        supplier = "Hassan and Hassan",
                        ListPrice = 90,
                        CategoryId = 9,
                        ImageUrl = ""
                    }


                );
        }
    }
}
