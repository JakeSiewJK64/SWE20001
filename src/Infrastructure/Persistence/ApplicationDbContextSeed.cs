using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultRole(RoleManager<ApplicationRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                var adminRole = new ApplicationRole { Name = "Administrator", Description = "Highest level access" };
                var supportRole = new ApplicationRole { Name = "Support", Description = "Support level access" };
                var userRole = new ApplicationRole { Name = "User", Description = "for branch access" };
                await roleManager.CreateAsync(adminRole);
                await roleManager.CreateAsync(supportRole);
                await roleManager.CreateAsync(userRole);
            }
        }

        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager)
        {
            var defaultUser = new ApplicationUser { UserName = "jakeAdminLocal@gmail.com", Email = "jakeAdminLocal@gmail.com" };

            if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
            {
                await userManager.CreateAsync(defaultUser, "welcome123");
                await userManager.AddToRoleAsync(defaultUser, "Administrator");
            }
        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            if (!context.TodoLists.Any())
            {
                context.TodoLists.Add(new TodoList
                {
                    Title = "Shopping",
                    Items =
                    {
                        new TodoItem { Title = "Apples", Done = true },
                        new TodoItem { Title = "Milk", Done = true },
                        new TodoItem { Title = "Bread", Done = true },
                        new TodoItem { Title = "Toilet paper" },
                        new TodoItem { Title = "Pasta" },
                        new TodoItem { Title = "Tissues" },
                        new TodoItem { Title = "Tuna" },
                        new TodoItem { Title = "Water" }
                    }
                });

                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedSampleItems(ApplicationDbContext context)
        {
            if (!context.Items.Any())
            {
                context.Items.Add(new Item { Quantity = 100, ItemName = "Acetaminophen (Tylenol)", Manufacturer_Id = 1, IsDeleted = false, ManufacturerName = "Rhodes Island Pharmaceuticals Ltd.", CostPrice = 10.99f, SellPrice = 12.99f, Status = Domain.Enums.Status.Full });
                context.Items.Add(new Item { Quantity = 90, ItemName = "Aspirin", Manufacturer_Id = 1, IsDeleted = false, ManufacturerName = "Rhodes Island Pharmaceuticals Ltd.", CostPrice = 10.99f, SellPrice = 12.99f, Status = Domain.Enums.Status.Full });
                context.Items.Add(new Item { Quantity = 90, ItemName = "naproxen", Manufacturer_Id = 1, IsDeleted = false, ManufacturerName = "Rhodes Island Pharmaceuticals Ltd.", CostPrice = 10.99f, SellPrice = 12.99f, Status = Domain.Enums.Status.Full });
                context.Items.Add(new Item { Quantity = 70, ItemName = "ibuprofen", Manufacturer_Id = 1, IsDeleted = false, ManufacturerName = "Rhodes Island Pharmaceuticals Ltd.", CostPrice = 10.99f, SellPrice = 12.99f, Status = Domain.Enums.Status.Full });
                context.Items.Add(new Item { Quantity = 70, ItemName = "Folic Acid", Manufacturer_Id = 1, IsDeleted = false, ManufacturerName = "Rhodes Island Pharmaceuticals Ltd.", CostPrice = 10.99f, SellPrice = 12.99f, Status = Domain.Enums.Status.Full });
                context.Items.Add(new Item { Quantity = 50, ItemName = "Iron Suppliments", Manufacturer_Id = 1, IsDeleted = false, ManufacturerName = "Rhodes Island Pharmaceuticals Ltd.", CostPrice = 10.99f, SellPrice = 12.99f, Status = Domain.Enums.Status.Full });
                context.Items.Add(new Item { Quantity = 40, ItemName = "Alprazolam", Manufacturer_Id = 1, IsDeleted = false, ManufacturerName = "Rhodes Island Pharmaceuticals Ltd.", CostPrice = 10.99f, SellPrice = 12.99f, Status = Domain.Enums.Status.Full });
                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedSampleSales(ApplicationDbContext context)
        {
            if (!context.SalesRecord.Any())
            {
                context.SalesRecord.Add(new SalesRecord { Date = new DateTime(2021, 2, 22, 0, 0, 0).ToString("yyyy/MM/dd HH:mm:ss"), Items = "[{\"itemId\":1,\"quantity\":10},{\"itemId\":2,\"quantity\":20}]" });
                context.SalesRecord.Add(new SalesRecord { Date = new DateTime(2021, 3, 2, 0, 0, 0).ToString("yyyy/MM/dd HH:mm:ss"), Items = "[{\"itemId\":2,\"quantity\":1},{\"itemId\":3,\"quantity\":1}]" });
                context.SalesRecord.Add(new SalesRecord { Date = new DateTime(2021, 2, 13, 0, 0, 0).ToString("yyyy/MM/dd HH:mm:ss"), Items = "[{\"itemId\":3,\"quantity\":5},{\"itemId\":5,\"quantity\":2}]" });
                context.SalesRecord.Add(new SalesRecord { Date = new DateTime(2021, 10, 20, 0, 0, 0).ToString("yyyy/MM/dd HH:mm:ss"), Items = "[{\"itemId\":4,\"quantity\":3},{\"itemId\":1,\"quantity\":3}]" });
                context.SalesRecord.Add(new SalesRecord { Date = new DateTime(2021, 6, 10, 0, 0, 0).ToString("yyyy/MM/dd HH:mm:ss"), Items = "[{\"itemId\":5,\"quantity\":2},{\"itemId\":3,\"quantity\":1}]" });
                await context.SaveChangesAsync();
            }
        }
    }
}
