using AppForSEII2526.API.Models;
using Microsoft.EntityFrameworkCore;
namespace AppForSEII2526.API.Data
{
   
        public static class SeedData
        {
        public static void Initialize(ApplicationDbContext dbContext, IServiceProvider serviceProvider, ILogger logger)
        {
            List<string> rolesNames = new List<string> { "Administrator", "Employee", "Customer" };

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            try
            {
                SeedRoles(roleManager, rolesNames);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred seeding the roles in the Database.");
            }


            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();



            try
            {
                SeedUsers(userManager, rolesNames);
            }


            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred seeding the Users in the Database.");
            }



            try
            {
                Seeddbo(dbContext);
            }
            catch (Exception ex)
            {
                Console.WriteLine("SEED ERROR: " + ex.ToString());
                logger.LogError(ex, "An error occurred seeding the dB data in the Database.");
            }
        }

            public static void SeedRoles(RoleManager<IdentityRole> roleManager, List<string> roles)
            {
                foreach (string roleName in roles)
                {

                    //it checks such role does not exist in the database 


                    if (!roleManager.RoleExistsAsync(roleName).Result)
                    {


                        IdentityRole role = new IdentityRole();


                        role.Name = roleName;


                        role.NormalizedName = roleName;


                        IdentityResult roleResult = roleManager.CreateAsync(role).Result;


                    }


                }


            }

            public static void SeedUsers(UserManager<ApplicationUser> userManager, List<string> roles)
            {



                //first, it checks the user does not already exist in the DB


                if (userManager.FindByNameAsync("elena@uclm.es").Result == null)
                {


                    ApplicationUser user = new ApplicationUser("1", "Elena", "Pretel", "elena@uclm.es", "Avenida España, 2","611");
                    user.EmailConfirmed = true;
                    var result = userManager.CreateAsync(user, "Password1234%");

                    result.Wait();


                    if (result.IsCompletedSuccessfully)
                    {


                        //administrator role


                        userManager.AddToRoleAsync(user, roles[0]).Wait();


                    }


                }

                if (userManager.FindByNameAsync("pablo.ramon@uclm.es").Result == null)
                {


                    ApplicationUser user = new ApplicationUser("2", "Pablo", "Ramón", "pablo.ramon@uclm.es", "Val General, 12","622");
                    user.EmailConfirmed = true;

                    var result = userManager.CreateAsync(user, "APassword1234%");


                    result.Wait();

                    if (result.IsCompletedSuccessfully)
                    {

                        //employee role

                        userManager.AddToRoleAsync(user, roles[1]).Wait();

                    }


                }

                if (userManager.FindByNameAsync("pablo.ballestero@uclm.es").Result == null)
                {


                    //A customer class has been defined because it has different attributes (purchase, rental, etc.)


                    ApplicationUser user = new ApplicationUser("3", "Pablo", "Ballestero", "pablo.ballestero@uclm.es", "Paseo Cervantes,8","633");
                    user.EmailConfirmed = true;

                    var result = userManager.CreateAsync(user, "OtherPass12$");

                    result.Wait();

                    if (result.IsCompletedSuccessfully)
                    {
                        //customer role
                        userManager.AddToRoleAsync(user, roles[2]).Wait();
                    }


                }

            if (userManager.FindByNameAsync("tomas.gonzalez@uclm.es").Result == null)
            {


                //A customer class has been defined because it has different attributes (purchase, rental, etc.)


                ApplicationUser user = new ApplicationUser("4", "Tomás", "González", "tomas.gonzalez@uclm.es", "Blasco Ibáñez,4", "644");
                user.EmailConfirmed = true;

                var result = userManager.CreateAsync(user, "OtherPass12$");

                result.Wait();

                if (result.IsCompletedSuccessfully)
                {
                    //customer role
                    userManager.AddToRoleAsync(user, roles[2]).Wait();
                }


            }



        }


        public static void Seeddbo(ApplicationDbContext dbcontext)
        {



           

            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Models] ([Name]) VALUES (N'Coupe')");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Models] ([Name]) VALUES (N'Hatchback')");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Models] ([Name]) VALUES (N'Pickup Truck')");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Models] ([Name]) VALUES (N'Sedan')");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Models] ([Name]) VALUES (N'SUV')");

            

            dbcontext.SaveChanges();

           

            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Cars] ([CarClass], [Color], [Description], [Manufacturer], [PurchasingPrice], [QuantityForPurchasing], [QuantityForRenting], [RentingPrice], [ReviewItems], [EngDisplacement], [Fueltype], [MaintenanceTypes], [RimSize], [ModelId]) VALUES (N'Standard', N'Red', N'Compact family sedan', N'Toyota', CAST(2500000.00 AS Decimal(10, 2)), 5, 8, CAST(12000.00 AS Decimal(18, 2)), CAST(4.50 AS Decimal(18, 2)), CAST(1.80 AS Decimal(18, 2)), N'Gasoline', N'Oil change, tire rotation', CAST(16.00 AS Decimal(18, 2)), 1)");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Cars] ([CarClass], [Color], [Description], [Manufacturer], [PurchasingPrice], [QuantityForPurchasing], [QuantityForRenting], [RentingPrice], [ReviewItems], [EngDisplacement], [Fueltype], [MaintenanceTypes], [RimSize], [ModelId]) VALUES (N'Premium', N'Black', N'Luxury business sedan', N'Mercedes-Benz', CAST(4800000.00 AS Decimal(10, 2)), 4, 9, CAST(20000.00 AS Decimal(18, 2)), CAST(4.80 AS Decimal(18, 2)), CAST(2.00 AS Decimal(18, 2)), N'Hybrid', N'Oil change, battery check', CAST(17.00 AS Decimal(18, 2)), 1)");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Cars] ([CarClass], [Color], [Description], [Manufacturer], [PurchasingPrice], [QuantityForPurchasing], [QuantityForRenting], [RentingPrice], [ReviewItems], [EngDisplacement], [Fueltype], [MaintenanceTypes], [RimSize], [ModelId]) VALUES (N'Standard', N'Blue', N'Family SUV with ample space', N'Toyota', CAST(3600000.00 AS Decimal(10, 2)), 6, 9, CAST(15000.00 AS Decimal(18, 2)), CAST(4.60 AS Decimal(18, 2)), CAST(2.40 AS Decimal(18, 2)), N'Diesel', N'Tire rotation, oil change', CAST(18.00 AS Decimal(18, 2)), 2)");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Cars] ([CarClass], [Color], [Description], [Manufacturer], [PurchasingPrice], [QuantityForPurchasing], [QuantityForRenting], [RentingPrice], [ReviewItems], [EngDisplacement], [Fueltype], [MaintenanceTypes], [RimSize], [ModelId]) VALUES (N'Compact', N'White', N'Fuel-efficient city hatchback', N'Volkswagen', CAST(1900000.00 AS Decimal(10, 2)), 10, 15, CAST(8000.00 AS Decimal(18, 2)), CAST(4.30 AS Decimal(18, 2)), CAST(1.60 AS Decimal(18, 2)), N'Gasoline', N'Oil change, tire replacement', CAST(15.00 AS Decimal(18, 2)), 3)");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Cars] ([CarClass], [Color], [Description], [Manufacturer], [PurchasingPrice], [QuantityForPurchasing], [QuantityForRenting], [RentingPrice], [ReviewItems], [EngDisplacement], [Fueltype], [MaintenanceTypes], [RimSize], [ModelId]) VALUES (N'Sport', N'Red', N'High-performance two-door coupe', N'Audi', CAST(6200000.00 AS Decimal(10, 2)), 2, 10, CAST(28000.00 AS Decimal(18, 2)), CAST(4.90 AS Decimal(18, 2)), CAST(3.20 AS Decimal(18, 2)), N'Gasoline', N'Oil change, brake inspection', CAST(19.00 AS Decimal(18, 2)), 4)");
            
            
            dbcontext.SaveChanges();

            

            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Purchases] ([PaymentMethod], [PurchasingDate], [PurchasingPrice], [DeliveryCarDealer], [ApplicationUserId]) VALUES (1, N'2025-01-15 00:00:00', CAST(2500000.00 AS Decimal(10, 2)), 1, N'1')");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Purchases] ([PaymentMethod], [PurchasingDate], [PurchasingPrice], [DeliveryCarDealer], [ApplicationUserId]) VALUES (1, N'2025-02-12 00:00:00', CAST(4800000.00 AS Decimal(10, 2)), 1, N'2')");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Purchases] ([PaymentMethod], [PurchasingDate], [PurchasingPrice], [DeliveryCarDealer], [ApplicationUserId]) VALUES (2, N'2025-03-10 00:00:00', CAST(3600000.00 AS Decimal(10, 2)), 2, N'3')");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Purchases] ([PaymentMethod], [PurchasingDate], [PurchasingPrice], [DeliveryCarDealer], [ApplicationUserId]) VALUES (3, N'2025-04-05 00:00:00', CAST(1900000.00 AS Decimal(10, 2)), 3, N'4')");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Purchases] ([PaymentMethod], [PurchasingDate], [PurchasingPrice], [DeliveryCarDealer], [ApplicationUserId]) VALUES (1, N'2025-05-15 00:00:00', CAST(6200000.00 AS Decimal(10, 2)), 1, N'1')");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Purchases] ([PaymentMethod], [PurchasingDate], [PurchasingPrice], [DeliveryCarDealer], [ApplicationUserId]) VALUES (0, N'2025-11-11 00:00:00', CAST(5000000.00 AS Decimal(10, 2)), 2, N'4')");
            
            

            dbcontext.SaveChanges();

            
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[PurchaseItems] ([CarId], [PurchaseId], [Quantity]) VALUES (1, 1, 3)");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[PurchaseItems] ([CarId], [PurchaseId], [Quantity]) VALUES (1, 6, 2)");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[PurchaseItems] ([CarId], [PurchaseId], [Quantity]) VALUES (2, 2, 4)");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[PurchaseItems] ([CarId], [PurchaseId], [Quantity]) VALUES (3, 3, 1)");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[PurchaseItems] ([CarId], [PurchaseId], [Quantity]) VALUES (4, 4, 1)");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[PurchaseItems] ([CarId], [PurchaseId], [Quantity]) VALUES (5, 5, 1)");
            

            dbcontext.SaveChanges();

            

            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Rentals] ([PaymentMethod], [RentalDate], [TotalPrice], [DeliveryCarDealer], [RentalDateFrom], [RentalDateTo], [ApplicationUserId]) VALUES (1, '2025-10-01 09:30:00', 150.00, 0, '2025-10-02 08:00:00', '2025-10-05 10:00:00', N'1')");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Rentals] ([PaymentMethod], [RentalDate], [TotalPrice], [DeliveryCarDealer], [RentalDateFrom], [RentalDateTo], [ApplicationUserId]) VALUES (2, '2025-10-03 14:15:00', 320.50, 1, '2025-10-04 09:00:00', '2025-10-09 18:00:00', N'2')");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Rentals] ([PaymentMethod], [RentalDate], [TotalPrice], [DeliveryCarDealer], [RentalDateFrom], [RentalDateTo], [ApplicationUserId]) VALUES (3, '2025-09-28 11:45:00', 450.75, 0, '2025-09-29 08:00:00', '2025-10-05 08:00:00', N'3')");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Rentals] ([PaymentMethod], [RentalDate], [TotalPrice], [DeliveryCarDealer], [RentalDateFrom], [RentalDateTo], [ApplicationUserId]) VALUES (1, '2025-10-10 16:00:00', 210.00, 1, '2025-10-11 09:00:00', '2025-10-14 09:00:00', N'4')");
            
            

            dbcontext.SaveChanges();

            
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[RentalItems] ([CarId], [RentalId], [Quantity]) VALUES (1, 1, 1)");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[RentalItems] ([CarId], [RentalId], [Quantity]) VALUES (2, 2, 1)");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[RentalItems] ([CarId], [RentalId], [Quantity]) VALUES (3, 3, 1)");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[RentalItems] ([CarId], [RentalId], [Quantity]) VALUES (4, 4, 1)");
            

            dbcontext.SaveChanges();

            

            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[MaintenanceTypes] ([Type]) VALUES (N'Preventive')");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[MaintenanceTypes] ([Type]) VALUES (N'Corrective')");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[MaintenanceTypes] ([Type]) VALUES (N'Emergency')");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[MaintenanceTypes] ([Type]) VALUES (N'Calibration')");
            
            

            dbcontext.SaveChanges();

            

            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Maintenances] ([Name], [NumberOfDays], [Price], [MaintenanceTypeId]) VALUES (N'Air filter cleaning', 3, CAST(80.00 AS Decimal(10, 2)), 1)");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Maintenances] ([Name], [NumberOfDays], [Price], [MaintenanceTypeId]) VALUES (N'Oil level check', 1, CAST(50.00 AS Decimal(10, 2)), 1)");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Maintenances] ([Name], [NumberOfDays], [Price], [MaintenanceTypeId]) VALUES (N'Repair ventilation motor', 5, CAST(300.00 AS Decimal(10, 2)), 2)");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Maintenances] ([Name], [NumberOfDays], [Price], [MaintenanceTypeId]) VALUES (N'Restore cooling system operation', 7, CAST(550.00 AS Decimal(10, 2)), 3)");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Maintenances] ([Name], [NumberOfDays], [Price], [MaintenanceTypeId]) VALUES (N'Scale calibration', 4, CAST(180.00 AS Decimal(10, 2)), 4)");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Maintenances] ([Name], [NumberOfDays], [Price], [MaintenanceTypeId]) VALUES (N'Fix leaking valve', 2, CAST(150.00 AS Decimal(10, 2)), 2)");
            
            

            dbcontext.SaveChanges();

            

            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Bookings] ([Date], [PaymentMethod], [ApplicationUserId], [TotalPrice], [TotalNumberOfDays]) VALUES (N'2025-10-01 09:30:00', 1, N'1',CAST(80.00 AS Decimal(10, 2)),3)");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Bookings] ([Date], [PaymentMethod], [ApplicationUserId], [TotalPrice], [TotalNumberOfDays]) VALUES (N'2025-10-03 14:15:00', 2, N'2',CAST(50.00 AS Decimal(10, 2)),1)");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Bookings] ([Date], [PaymentMethod], [ApplicationUserId], [TotalPrice], [TotalNumberOfDays]) VALUES (N'2025-09-28 11:45:00', 3, N'2',CAST(300.00 AS Decimal(10, 2)),5)");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Bookings] ([Date], [PaymentMethod], [ApplicationUserId], [TotalPrice], [TotalNumberOfDays]) VALUES (N'2025-10-10 16:00:00', 4, N'3',CAST(550.00 AS Decimal(10, 2)),7)");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Bookings] ([Date], [PaymentMethod], [ApplicationUserId], [TotalPrice], [TotalNumberOfDays]) VALUES (N'2025-05-15 00:00:00', 5, N'4',CAST(180.00 AS Decimal(10, 2)),4)");
            
            

            dbcontext.SaveChanges();

            
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[BookingItems] ([BookingId], [MaintenanceId], [Comment]) VALUES (1, 1, N'Air filter cleaned, dust and debris removed')");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[BookingItems] ([BookingId], [MaintenanceId], [Comment]) VALUES (2, 2, N'Oil level checked, within normal parameters')");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[BookingItems] ([BookingId], [MaintenanceId], [Comment]) VALUES (3, 3, N'Ventilation system repaired, main fan replaced')");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[BookingItems] ([BookingId], [MaintenanceId], [Comment]) VALUES (4, 4, N'Cooling system restored, compressor replaced')");
            dbcontext.Database.ExecuteSqlRaw("INSERT INTO [dbo].[BookingItems] ([BookingId], [MaintenanceId], [Comment]) VALUES (5, 5, N'Equipment calibrated accurately according to ISO standard')");
            

            dbcontext.SaveChanges();

        }


    }


    }
