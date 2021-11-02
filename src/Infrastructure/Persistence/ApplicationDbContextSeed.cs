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
            var defaultNormalUser = new ApplicationUser { UserName = "jakeUserLocal@gmail.com", Email = "jakeUserLocal@gmail.com" };

            if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
            {
                await userManager.CreateAsync(defaultUser, "welcome123");
                await userManager.AddToRoleAsync(defaultUser, "Administrator");
            }
            
            if (userManager.Users.All(u => u.UserName != defaultNormalUser.UserName))
            {
                await userManager.CreateAsync(defaultNormalUser, "welcome123");
                await userManager.AddToRoleAsync(defaultNormalUser, "User");
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
                context.Items.Add(new Item { Quantity = 100, ItemName = "Acetaminophen (Tylenol)", ImageUrl = "https://raw.githubusercontent.com/JakeSiewJK64/ImageWarehouse/main/Pharmacy/Acetaminophe%20(tylenol).jfif", Manufacturer_Id = 1, IsDeleted = false, ManufacturerName = "Rhodes Island Pharmaceuticals Ltd.", CostPrice = 10.99f, SellPrice = 12.99f, Status = Domain.Enums.Status.Full, ItemCategory = Domain.Enums.ItemCategory.Amphetamines });
                context.Items.Add(new Item { Quantity = 90, ItemName = "Aspirin", ImageUrl= "https://raw.githubusercontent.com/JakeSiewJK64/ImageWarehouse/main/Pharmacy/aspirin.png", Manufacturer_Id = 1, IsDeleted = false, ManufacturerName = "Rhodes Island Pharmaceuticals Ltd.", CostPrice = 10.99f, SellPrice = 12.99f, Status = Domain.Enums.Status.Full, ItemCategory = Domain.Enums.ItemCategory.Analgesics });
                context.Items.Add(new Item { Quantity = 90, ItemName = "naproxen", ImageUrl= "https://raw.githubusercontent.com/JakeSiewJK64/ImageWarehouse/main/Pharmacy/naproxen.jpg", Manufacturer_Id = 1, IsDeleted = false, ManufacturerName = "Rhodes Island Pharmaceuticals Ltd.", CostPrice = 10.99f, SellPrice = 12.99f, Status = Domain.Enums.Status.Full, ItemCategory = Domain.Enums.ItemCategory.Anesthetics });
                context.Items.Add(new Item { Quantity = 70, ItemName = "ibuprofen", ImageUrl= "https://raw.githubusercontent.com/JakeSiewJK64/ImageWarehouse/main/Pharmacy/ibuprofen.jpg", Manufacturer_Id = 1, IsDeleted = false, ManufacturerName = "Rhodes Island Pharmaceuticals Ltd.", CostPrice = 10.99f, SellPrice = 12.99f, Status = Domain.Enums.Status.Full, ItemCategory = Domain.Enums.ItemCategory.Antacids });
                context.Items.Add(new Item { Quantity = 70, ItemName = "Folic Acid", ImageUrl= "https://raw.githubusercontent.com/JakeSiewJK64/ImageWarehouse/main/Pharmacy/folic_acid.jpg", Manufacturer_Id = 1, IsDeleted = false, ManufacturerName = "Rhodes Island Pharmaceuticals Ltd.", CostPrice = 10.99f, SellPrice = 12.99f, Status = Domain.Enums.Status.Full, ItemCategory = Domain.Enums.ItemCategory.Antibacterial });
                context.Items.Add(new Item { Quantity = 50, ItemName = "Iron Suppliments", ImageUrl= "https://raw.githubusercontent.com/JakeSiewJK64/ImageWarehouse/main/Pharmacy/iron_supplements.png", Manufacturer_Id = 1, IsDeleted = false, ManufacturerName = "Rhodes Island Pharmaceuticals Ltd.", CostPrice = 10.99f, SellPrice = 12.99f, Status = Domain.Enums.Status.Full, ItemCategory = Domain.Enums.ItemCategory.AntiEpileptic });
                context.Items.Add(new Item { Quantity = 40, ItemName = "Alprazolam", ImageUrl= "https://raw.githubusercontent.com/JakeSiewJK64/ImageWarehouse/main/Pharmacy/Alprazolam.jpg", Manufacturer_Id = 1, IsDeleted = false, ManufacturerName = "Rhodes Island Pharmaceuticals Ltd.", CostPrice = 10.99f, SellPrice = 12.99f, Status = Domain.Enums.Status.Full, ItemCategory = Domain.Enums.ItemCategory.AntiHistamine });
                context.Items.Add(new Item { Quantity = 40, ItemName = "Bandage", ImageUrl= "https://raw.githubusercontent.com/JakeSiewJK64/ImageWarehouse/main/Pharmacy/bandage.jpg", Manufacturer_Id = 12, IsDeleted = false, ManufacturerName = "Rhodes Island Pharmaceuticals Ltd.", CostPrice = 10.99f, SellPrice = 12.99f, Status = Domain.Enums.Status.Full, ItemCategory = Domain.Enums.ItemCategory.Bandages });
                context.Items.Add(new Item { Quantity = 40, ItemName = "Champs Vitamin C Supplements", ImageUrl= "https://raw.githubusercontent.com/JakeSiewJK64/ImageWarehouse/main/Pharmacy/champs_vc_supplements.jpg", Manufacturer_Id = 122, IsDeleted = false, ManufacturerName = "Rhodes Island Pharmaceuticals Ltd.", CostPrice = 10.99f, SellPrice = 12.99f, Status = Domain.Enums.Status.Full, ItemCategory = Domain.Enums.ItemCategory.Supplements });
                context.Items.Add(new Item { Quantity = 40, ItemName = "Cod Olive Oil", ImageUrl= "https://raw.githubusercontent.com/JakeSiewJK64/ImageWarehouse/main/Pharmacy/cod_liver_oil.jpg", Manufacturer_Id = 12, IsDeleted = false, ManufacturerName = "Rhodes Island Pharmaceuticals Ltd.", CostPrice = 10.99f, SellPrice = 12.99f, Status = Domain.Enums.Status.Full, ItemCategory = Domain.Enums.ItemCategory.Supplements });
                context.Items.Add(new Item { Quantity = 40, ItemName = "Cough Syrup", ImageUrl= "https://raw.githubusercontent.com/JakeSiewJK64/ImageWarehouse/main/Pharmacy/cough_syrup.jpg", Manufacturer_Id = 21, IsDeleted = false, ManufacturerName = "Rhodes Island Pharmaceuticals Ltd.", CostPrice = 10.99f, SellPrice = 12.99f, Status = Domain.Enums.Status.Full, ItemCategory = Domain.Enums.ItemCategory.Antiviral });
                context.Items.Add(new Item { Quantity = 40, ItemName = "Dettol Sanitizer", ImageUrl= "https://raw.githubusercontent.com/JakeSiewJK64/ImageWarehouse/main/Pharmacy/dettol_sanitizer.jpg", Manufacturer_Id = 1, IsDeleted = false, ManufacturerName = "Rhodes Island Pharmaceuticals Ltd.", CostPrice = 10.99f, SellPrice = 12.99f, Status = Domain.Enums.Status.Full, ItemCategory = Domain.Enums.ItemCategory.SkinCare });
                context.Items.Add(new Item { Quantity = 40, ItemName = "Durex XL Size", ImageUrl= "https://raw.githubusercontent.com/JakeSiewJK64/ImageWarehouse/main/Pharmacy/durex_xl.jpg", Manufacturer_Id = 111, IsDeleted = false, ManufacturerName = "Rhodes Island Pharmaceuticals Ltd.", CostPrice = 10.99f, SellPrice = 12.99f, Status = Domain.Enums.Status.Full, ItemCategory = Domain.Enums.ItemCategory.Antiviral });
                context.Items.Add(new Item { Quantity = 40, ItemName = "Kordel Vitamin C Zinc", ImageUrl= "https://raw.githubusercontent.com/JakeSiewJK64/ImageWarehouse/main/Pharmacy/kordel_vitaminc_zinc.jfif", Manufacturer_Id = 1, IsDeleted = false, ManufacturerName = "Rhodes Island Pharmaceuticals Ltd.", CostPrice = 10.99f, SellPrice = 12.99f, Status = Domain.Enums.Status.Full, ItemCategory = Domain.Enums.ItemCategory.Supplements });
                context.Items.Add(new Item { Quantity = 40, ItemName = "Nivea Body Lotion", ImageUrl= "https://raw.githubusercontent.com/JakeSiewJK64/ImageWarehouse/main/Pharmacy/lotion.jpg", Manufacturer_Id = 331, IsDeleted = false, ManufacturerName = "Rhodes Island Pharmaceuticals Ltd.", CostPrice = 10.99f, SellPrice = 12.99f, Status = Domain.Enums.Status.Full, ItemCategory = Domain.Enums.ItemCategory.SkinCare });
                context.Items.Add(new Item { Quantity = 40, ItemName = "N-95 Mask", ImageUrl= "https://raw.githubusercontent.com/JakeSiewJK64/ImageWarehouse/main/Pharmacy/mask.jpg", Manufacturer_Id = 421, IsDeleted = false, ManufacturerName = "Rhodes Island Pharmaceuticals Ltd.", CostPrice = 10.99f, SellPrice = 12.99f, Status = Domain.Enums.Status.Full, ItemCategory = Domain.Enums.ItemCategory.Antiviral });
                context.Items.Add(new Item { Quantity = 40, ItemName = "Band-Aid Plaster", ImageUrl= "https://raw.githubusercontent.com/JakeSiewJK64/ImageWarehouse/main/Pharmacy/plaster.jpg", Manufacturer_Id = 121, IsDeleted = false, ManufacturerName = "Rhodes Island Pharmaceuticals Ltd.", CostPrice = 10.99f, SellPrice = 12.99f, Status = Domain.Enums.Status.Full, ItemCategory = Domain.Enums.ItemCategory.Bandages });
                context.Items.Add(new Item { Quantity = 40, ItemName = "Post-OP Size L", ImageUrl= "https://raw.githubusercontent.com/JakeSiewJK64/ImageWarehouse/main/Pharmacy/post_op_L.jpg", Manufacturer_Id = 411, IsDeleted = false, ManufacturerName = "Rhodes Island Pharmaceuticals Ltd.", CostPrice = 10.99f, SellPrice = 12.99f, Status = Domain.Enums.Status.Full, ItemCategory = Domain.Enums.ItemCategory.Bandages });
                context.Items.Add(new Item { Quantity = 40, ItemName = "Post-OP Size M", ImageUrl= "https://raw.githubusercontent.com/JakeSiewJK64/ImageWarehouse/main/Pharmacy/post_op_M.jpg", Manufacturer_Id = 125, IsDeleted = false, ManufacturerName = "Rhodes Island Pharmaceuticals Ltd.", CostPrice = 10.99f, SellPrice = 12.99f, Status = Domain.Enums.Status.Full, ItemCategory = Domain.Enums.ItemCategory.Bandages });
                context.Items.Add(new Item { Quantity = 40, ItemName = "Hand Sanitizer", ImageUrl= "https://raw.githubusercontent.com/JakeSiewJK64/ImageWarehouse/main/Pharmacy/sanitizer_gel.jpg", Manufacturer_Id = 261, IsDeleted = false, ManufacturerName = "Rhodes Island Pharmaceuticals Ltd.", CostPrice = 10.99f, SellPrice = 12.99f, Status = Domain.Enums.Status.Full, ItemCategory = Domain.Enums.ItemCategory.Antiviral });
                context.Items.Add(new Item { Quantity = 40, ItemName = "Surgical Mask 25 Pieces", ImageUrl= "https://raw.githubusercontent.com/JakeSiewJK64/ImageWarehouse/main/Pharmacy/surgical_mask.png", Manufacturer_Id = 11, IsDeleted = false, ManufacturerName = "Rhodes Island Pharmaceuticals Ltd.", CostPrice = 10.99f, SellPrice = 12.99f, Status = Domain.Enums.Status.Full, ItemCategory = Domain.Enums.ItemCategory.Antiviral });
                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedSampleSales(ApplicationDbContext context)
        {
            if (!context.SalesRecord.Any())
            {
                context.SalesRecord.Add(new SalesRecord { SalesDate = new DateTime(2021, 2, 22), Items = "[{\"itemId\":1,\"quantity\":10},{\"itemId\":2,\"quantity\":20}]" });
                context.SalesRecord.Add(new SalesRecord { SalesDate = new DateTime(2021, 3, 2), Items = "[{\"itemId\":2,\"quantity\":1},{\"itemId\":3,\"quantity\":1}]" });
                context.SalesRecord.Add(new SalesRecord { SalesDate = new DateTime(2021, 2, 13), Items = "[{\"itemId\":3,\"quantity\":5},{\"itemId\":5,\"quantity\":2}]" });
                context.SalesRecord.Add(new SalesRecord { SalesDate = new DateTime(2021, 10, 20), Items = "[{\"itemId\":4,\"quantity\":3},{\"itemId\":1,\"quantity\":3}]" });
                context.SalesRecord.Add(new SalesRecord { SalesDate = new DateTime(2021, 6, 10), Items = "[{\"itemId\":5,\"quantity\":2},{\"itemId\":3,\"quantity\":1}]" });
                await context.SaveChangesAsync();
            }
        }
    }
}
