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
                        EmailConfirmed = true
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
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
