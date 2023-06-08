using Microsoft.AspNetCore.Identity;
using ShoeperStar.Models;

namespace ShoeperStar
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                SeedBrands(context);
                SeedCategories(context);
                SeedGenders(context);
            }
        }

        public static void SeedBrands(AppDbContext dbContext)
        {
            if (!dbContext.Brands.Any())
            {
                dbContext.Brands.AddRange(new List<Brand>()
                {
                    new Brand()
                    {
                        Name = "Nike"
                    },
                    new Brand()
                    {
                        Name = "Adidas"
                    },
                    new Brand()
                    {
                        Name = "Under Armour"
                    },
                    new Brand()
                    {
                        Name = "Converse"
                    },
                    new Brand()
                    {
                        Name = "New Balance"
                    },
                });

                dbContext.SaveChanges();
            }
        }

        public static void SeedCategories(AppDbContext dbContext)
        {
            if (!dbContext.Catergories.Any())
            {
                dbContext.Catergories.AddRange(new List<Category>()
                {
                    new Category()
                    {
                        Name = "Basketball Shoes"
                    },
                    new Category()
                    {
                        Name = "Boots"
                    },
                    new Category()
                    {
                        Name = "Running Shoes"
                    },
                    new Category()
                    {
                        Name = "Sneakers"
                    },
                    new Category()
                    {
                        Name = "Loafers"
                    },
                    new Category()
                    {
                        Name = "Sandals"
                    },
                });

                dbContext.SaveChanges();
            }
        }

        public static void SeedGenders(AppDbContext dbContext)
        {
            if (!dbContext.Genders.Any())
            {
                dbContext.Genders.AddRange(new List<Gender>()
                {
                    new Gender()
                    {
                        Name = "Men"
                    },
                    new Gender()
                    {
                        Name = "Women"
                    },
                    new Gender()
                    {
                        Name = "Unisex"
                    },
                    new Gender()
                    {
                        Name = "Kids"
                    }
                });

                dbContext.SaveChanges();
            }
        }
    }
}
