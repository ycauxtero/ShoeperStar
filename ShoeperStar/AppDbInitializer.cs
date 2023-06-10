using Microsoft.AspNetCore.Identity;
using ShoeperStar.Data.References;
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
                SeedUsersAndRolesAsync(applicationBuilder).GetAwaiter().GetResult();
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

        public static void SeedShoes(AppDbContext dbContext)
        {
            if (!dbContext.Shoes.Any())
            {
                dbContext.Shoes.AddRange(new List<Shoe>()
                {
                    new Shoe()
                    {
                        BrandId = 1,
                        CategoryId = 1,
                        GenderId = 1,
                        Name = "Kobe 1 Protro",
                        Price = 10000,
                        ImageURL = "https://i.pinimg.com/originals/7d/7d/de/7d7dde45e84deed15c79aa357f013b23.jpg",
                        Variants = new List<Variant>()
                        {
                           new Variant()
                           {
                               Color = "Purple",
                               ColorHex = "#A020F0",
                               Sizes = new List<Size>()
                               {
                                   new Size()
                                   {
                                       Quantity = 2,
                                       Value = "US-10",
                                       SKU = "NK-KB1P-PUR-US10"
                                   }
                               }
                           } 
                        }
                    },

                    new Shoe()
                    {
                        BrandId = 2,
                        CategoryId = 3,
                        GenderId = 2,
                        Name = "Ultraboost Light",
                        Price = 6000,
                        ImageURL = "https://assets.adidas.com/videos/w_600,f_auto,q_auto/2537110c6e0342aaa2a8afb600beaff9_d98c/Ultraboost_Light_Running_Shoes_Black_HQ6339_video.jpg",
                        Variants = new List<Variant>()
                        {
                           new Variant()
                           {
                               Color = "Black",
                               ColorHex = "#000000",
                               Sizes = new List<Size>()
                               {
                                   new Size()
                                   {
                                       Quantity = 3,
                                       Value = "US-8",
                                       SKU = "ADI-ULL-BLK-US8"
                                   },
                                   new Size()
                                   {
                                       Quantity = 1,
                                       Value = "US-7",
                                       SKU = "ADI-ULL-BLK-US7"
                                   }
                               }
                           },
                           new Variant()
                           {
                               Color = "White",
                               ColorHex = "#FFFFFF",
                               Sizes = new List<Size>()
                               {
                                   new Size()
                                   {
                                       Quantity = 3,
                                       Value = "US-8",
                                       SKU = "ADI-ULL-WHT-US8"
                                   },
                                   new Size()
                                   {
                                       Quantity = 1,
                                       Value = "US-7",
                                       SKU = "ADI-ULL-WHT-US7"
                                   }
                               }
                           }
                        }
                    },

                    new Shoe()
                    {
                        BrandId = 3,
                        CategoryId = 6,
                        GenderId = 3,
                        Name = "Project Rock 3 Slides",
                        Price = 3500,
                        ImageURL = "https://cdna.lystit.com/520/650/n/photos/underarmour/b3314b80/under-armour-black-Unisex-project-rock-3-slides.jpeg",
                        Variants = new List<Variant>()
                        {
                           new Variant()
                           {
                               Color = "Black",
                               ColorHex = "#000000",
                               Sizes = new List<Size>()
                               {
                                   new Size()
                                   {
                                       Quantity = 2,
                                       Value = "US-8",
                                       SKU = "UA-PR3S-BLK-US8"
                                   },
                                   new Size()
                                   {
                                       Quantity = 5,
                                       Value = "US-7",
                                       SKU = "UA-PR3S-BLK-US7"
                                   }
                               }
                           }
                        }
                    },

                    new Shoe()
                    {
                        BrandId = 4,
                        CategoryId = 4,
                        GenderId = 4,
                        Name = "Chuck Taylor All Star Street",
                        Price = 6000,
                        ImageURL = "https://www.conversesandton.co.za/images/large/conversesandton/Lt_Armory_Blue_Blue_White_Kids_Converse-59204-DLQN_ZOOM.jpg",
                        Variants = new List<Variant>()
                        {
                           new Variant()
                           {
                               Color = "Morning Glory",
                               ColorHex = "#97DDE4",
                               Sizes = new List<Size>()
                               {
                                   new Size()
                                   {
                                       Quantity = 3,
                                       Value = "US-4",
                                       SKU = "CNV-CTAS-BLU-US4"
                                   }
                               }
                           }
                        }
                    }
                });

                dbContext.SaveChanges();
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "admin@shoeperstar.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        FirstName = "Shoperstar",
                        LastName = "Admin",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        DeliveryAddress = "Pusok, Lapu-lapu City, Cebu"
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }


                string appUserEmail = "user@shoeperstar.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        FirstName = "Shoperstar",
                        LastName = "User",
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        DeliveryAddress = "Pusok, Lapu-lapu City, Cebu"
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
