INSERT INTO [dbo].[AspNetUsers] ([Id], [Name], [Surname], [Address], [PhoneNumber], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'1', N'Elena', N'Pretel', N'Avenida España, 2', NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, 1, 1, NULL, 1, 1)
INSERT INTO [dbo].[AspNetUsers] ([Id], [Name], [Surname], [Address], [PhoneNumber], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'2', N'Pablo', N'Ramón', N'Val General, 12', NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, 1, 1, NULL, 1, 2)
INSERT INTO [dbo].[AspNetUsers] ([Id], [Name], [Surname], [Address], [PhoneNumber], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'3', N'Pablo', N'Ballestero', N'Paseo Cervantes,8', NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, 1, 1, NULL, 1, 3)
INSERT INTO [dbo].[AspNetUsers] ([Id], [Name], [Surname], [Address], [PhoneNumber], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'4', N'Tomás', N'González', N'Blasco Ibáñez,4', NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, 1, 1, NULL, 1, 4)

SET IDENTITY_INSERT [dbo].[Models] ON
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (4, N'Coupe')
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (3, N'Hatchback')
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (5, N'Pickup Truck')
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (1, N'Sedan')
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (2, N'SUV')
SET IDENTITY_INSERT [dbo].[Models] OFF

SET IDENTITY_INSERT [dbo].[Cars] ON
INSERT INTO [dbo].[Cars] ([Id], [CarClass], [Color], [Description], [Manufacturer], [PurchasingPrice], [QuantityForPurchasing], [QuantityForRenting], [RentingPrice], [ReviewItems], [EngDisplacement], [Fueltype], [MaintenanceTypes], [RimSize], [ModelId]) VALUES (1, N'Standard', N'Red', N'Compact family sedan', N'Toyota', CAST(2500000.00 AS Decimal(10, 2)), 5, 2, CAST(12000.00 AS Decimal(18, 2)), CAST(4.50 AS Decimal(18, 2)), CAST(1.80 AS Decimal(18, 2)), N'Gasoline', N'Oil change, tire rotation', CAST(16.00 AS Decimal(18, 2)), 1)
INSERT INTO [dbo].[Cars] ([Id], [CarClass], [Color], [Description], [Manufacturer], [PurchasingPrice], [QuantityForPurchasing], [QuantityForRenting], [RentingPrice], [ReviewItems], [EngDisplacement], [Fueltype], [MaintenanceTypes], [RimSize], [ModelId]) VALUES (2, N'Premium', N'Black', N'Luxury business sedan', N'Mercedes-Benz', CAST(4800000.00 AS Decimal(10, 2)), 4, 2, CAST(20000.00 AS Decimal(18, 2)), CAST(4.80 AS Decimal(18, 2)), CAST(2.00 AS Decimal(18, 2)), N'Hybrid', N'Oil change, battery check', CAST(17.00 AS Decimal(18, 2)), 1)
INSERT INTO [dbo].[Cars] ([Id], [CarClass], [Color], [Description], [Manufacturer], [PurchasingPrice], [QuantityForPurchasing], [QuantityForRenting], [RentingPrice], [ReviewItems], [EngDisplacement], [Fueltype], [MaintenanceTypes], [RimSize], [ModelId]) VALUES (3, N'Standard', N'Blue', N'Family SUV with ample space', N'Toyota', CAST(3600000.00 AS Decimal(10, 2)), 6, 3, CAST(15000.00 AS Decimal(18, 2)), CAST(4.60 AS Decimal(18, 2)), CAST(2.40 AS Decimal(18, 2)), N'Diesel', N'Tire rotation, oil change', CAST(18.00 AS Decimal(18, 2)), 2)
INSERT INTO [dbo].[Cars] ([Id], [CarClass], [Color], [Description], [Manufacturer], [PurchasingPrice], [QuantityForPurchasing], [QuantityForRenting], [RentingPrice], [ReviewItems], [EngDisplacement], [Fueltype], [MaintenanceTypes], [RimSize], [ModelId]) VALUES (4, N'Compact', N'White', N'Fuel-efficient city hatchback', N'Volkswagen', CAST(1900000.00 AS Decimal(10, 2)), 10, 5, CAST(8000.00 AS Decimal(18, 2)), CAST(4.30 AS Decimal(18, 2)), CAST(1.60 AS Decimal(18, 2)), N'Gasoline', N'Oil change, tire replacement', CAST(15.00 AS Decimal(18, 2)), 3)
INSERT INTO [dbo].[Cars] ([Id], [CarClass], [Color], [Description], [Manufacturer], [PurchasingPrice], [QuantityForPurchasing], [QuantityForRenting], [RentingPrice], [ReviewItems], [EngDisplacement], [Fueltype], [MaintenanceTypes], [RimSize], [ModelId]) VALUES (5, N'Sport', N'Red', N'High-performance two-door coupe', N'Audi', CAST(6200000.00 AS Decimal(10, 2)), 2, 1, CAST(28000.00 AS Decimal(18, 2)), CAST(4.90 AS Decimal(18, 2)), CAST(3.20 AS Decimal(18, 2)), N'Gasoline', N'Oil change, brake inspection', CAST(19.00 AS Decimal(18, 2)), 4)
SET IDENTITY_INSERT [dbo].[Cars] OFF

SET IDENTITY_INSERT [dbo].[Purchases] ON
INSERT INTO [dbo].[Purchases] ([Id], [PaymentMethod], [PurchasingDate], [PurchasingPrice], [DeliveryCarDealer], [ApplicationUserId]) VALUES (1, 1, N'2025-01-15 00:00:00', CAST(2500000.00 AS Decimal(10, 2)), 1, N'1')
INSERT INTO [dbo].[Purchases] ([Id], [PaymentMethod], [PurchasingDate], [PurchasingPrice], [DeliveryCarDealer], [ApplicationUserId]) VALUES (2, 1, N'2025-02-12 00:00:00', CAST(4800000.00 AS Decimal(10, 2)), 1, N'2')
INSERT INTO [dbo].[Purchases] ([Id], [PaymentMethod], [PurchasingDate], [PurchasingPrice], [DeliveryCarDealer], [ApplicationUserId]) VALUES (3, 2, N'2025-03-10 00:00:00', CAST(3600000.00 AS Decimal(10, 2)), 2, N'3')
INSERT INTO [dbo].[Purchases] ([Id], [PaymentMethod], [PurchasingDate], [PurchasingPrice], [DeliveryCarDealer], [ApplicationUserId]) VALUES (4, 3, N'2025-04-05 00:00:00', CAST(1900000.00 AS Decimal(10, 2)), 3, N'4')
INSERT INTO [dbo].[Purchases] ([Id], [PaymentMethod], [PurchasingDate], [PurchasingPrice], [DeliveryCarDealer], [ApplicationUserId]) VALUES (5, 1, N'2025-05-15 00:00:00', CAST(6200000.00 AS Decimal(10, 2)), 1, N'1')
SET IDENTITY_INSERT [dbo].[Purchases] OFF


INSERT INTO [dbo].[PurchaseItems] ([CarId], [PurchaseId], [Quantity]) VALUES (1, 1, 1) 
INSERT INTO [dbo].[PurchaseItems] ([CarId], [PurchaseId], [Quantity]) VALUES (2, 2, 1) 
INSERT INTO [dbo].[PurchaseItems] ([CarId], [PurchaseId], [Quantity]) VALUES (3, 3, 1) 
INSERT INTO [dbo].[PurchaseItems] ([CarId], [PurchaseId], [Quantity]) VALUES (4, 4, 1)

SET IDENTITY_INSERT [dbo].[Rentals] ON
INSERT INTO [dbo].[Rentals] ([Id], [PaymentMethod], [RentingDate], [TotalPrice], [DeliveryCarDealer], [Startdate], [Enddate], [ApplicationUserId]) VALUES (1, 1, '2025-10-01 09:30:00', 150.00, 0, '2025-10-02 08:00:00', '2025-10-05 10:00:00', N'1')
INSERT INTO [dbo].[Rentals] ([Id], [PaymentMethod], [RentingDate], [TotalPrice], [DeliveryCarDealer], [Startdate], [Enddate], [ApplicationUserId]) VALUES (2, 2, '2025-10-03 14:15:00', 320.50, 1, '2025-10-04 09:00:00', '2025-10-09 18:00:00', N'2')
INSERT INTO [dbo].[Rentals] ([Id], [PaymentMethod], [RentingDate], [TotalPrice], [DeliveryCarDealer], [Startdate], [Enddate], [ApplicationUserId]) VALUES (3, 3, '2025-09-28 11:45:00', 450.75, 0, '2025-09-29 08:00:00', '2025-10-05 08:00:00', N'3')
INSERT INTO [dbo].[Rentals] ([Id], [PaymentMethod], [RentingDate], [TotalPrice], [DeliveryCarDealer], [Startdate], [Enddate], [ApplicationUserId]) VALUES (4, 1, '2025-10-10 16:00:00', 210.00, 1, '2025-10-11 09:00:00', '2025-10-14 09:00:00', N'4')
SET IDENTITY_INSERT [dbo].[Rentals] OFF

INSERT INTO [dbo].[RentalItems] ([CarId], [RentalId], [Quantity]) VALUES (1, 1, 1) 
INSERT INTO [dbo].[RentalItems] ([CarId], [RentalId], [Quantity]) VALUES (2, 2, 1) 
INSERT INTO [dbo].[RentalItems] ([CarId], [RentalId], [Quantity]) VALUES (3, 3, 1) 
INSERT INTO [dbo].[RentalItems] ([CarId], [RentalId], [Quantity]) VALUES (4, 4, 1)

SET IDENTITY_INSERT [dbo].[MaintenanceTypes] ON
INSERT INTO [dbo].[MaintenanceTypes] ([Id], [Type]) VALUES (1, N'Preventive')
INSERT INTO [dbo].[MaintenanceTypes] ([Id], [Type]) VALUES (2, N'Corrective')
INSERT INTO [dbo].[MaintenanceTypes] ([Id], [Type]) VALUES (3, N'Emergency')
INSERT INTO [dbo].[MaintenanceTypes] ([Id], [Type]) VALUES (4, N'Calibration')
SET IDENTITY_INSERT [dbo].[MaintenanceTypes] OFF

SET IDENTITY_INSERT [dbo].[Maintenances] ON
INSERT INTO [dbo].[Maintenances] ([Id], [Name], [NumberOfDays], [Price], [MaintenanceTypeId]) VALUES (1, N'Air filter cleaning', 3, CAST(80.00 AS Decimal(10, 2)), 1)
INSERT INTO [dbo].[Maintenances] ([Id], [Name], [NumberOfDays], [Price], [MaintenanceTypeId]) VALUES (2, N'Oil level check', 1, CAST(50.00 AS Decimal(10, 2)), 1)
INSERT INTO [dbo].[Maintenances] ([Id], [Name], [NumberOfDays], [Price], [MaintenanceTypeId]) VALUES (3, N'Repair ventilation motor', 5, CAST(300.00 AS Decimal(10, 2)), 2)
INSERT INTO [dbo].[Maintenances] ([Id], [Name], [NumberOfDays], [Price], [MaintenanceTypeId]) VALUES (4, N'Restore cooling system operation', 7, CAST(550.00 AS Decimal(10, 2)), 3)
INSERT INTO [dbo].[Maintenances] ([Id], [Name], [NumberOfDays], [Price], [MaintenanceTypeId]) VALUES (5, N'Scale calibration', 4, CAST(180.00 AS Decimal(10, 2)), 4)
INSERT INTO [dbo].[Maintenances] ([Id], [Name], [NumberOfDays], [Price], [MaintenanceTypeId]) VALUES (6, N'Fix leaking valve', 2, CAST(150.00 AS Decimal(10, 2)), 2)
SET IDENTITY_INSERT [dbo].[Maintenances] OFF

SET IDENTITY_INSERT [dbo].[Bookings] ON
INSERT INTO [dbo].[Bookings] ([Id], [Date], [PaymentMethod], [ApplicationUserId]) VALUES (1, N'2025-10-01 09:30:00', 1, N'1')
INSERT INTO [dbo].[Bookings] ([Id], [Date], [PaymentMethod], [ApplicationUserId]) VALUES (2, N'2025-10-03 14:15:00', 2, N'2')
INSERT INTO [dbo].[Bookings] ([Id], [Date], [PaymentMethod], [ApplicationUserId]) VALUES (3, N'2025-09-28 11:45:00', 3, N'2')
INSERT INTO [dbo].[Bookings] ([Id], [Date], [PaymentMethod], [ApplicationUserId]) VALUES (4, N'2025-10-10 16:00:00', 4, N'3')
INSERT INTO [dbo].[Bookings] ([Id], [Date], [PaymentMethod], [ApplicationUserId]) VALUES (5, N'2025-05-15 00:00:00', 5, N'4')
SET IDENTITY_INSERT [dbo].[Bookings] OFF

INSERT INTO [dbo].[BookingItems] ([BookingId], [MaintenanceId], [Comment]) VALUES (1, 1, N'Air filter cleaned, dust and debris removed')
INSERT INTO [dbo].[BookingItems] ([BookingId], [MaintenanceId], [Comment]) VALUES (2, 2, N'Oil level checked, within normal parameters')
INSERT INTO [dbo].[BookingItems] ([BookingId], [MaintenanceId], [Comment]) VALUES (3, 3, N'Ventilation system repaired, main fan replaced')
INSERT INTO [dbo].[BookingItems] ([BookingId], [MaintenanceId], [Comment]) VALUES (4, 4, N'Cooling system restored, compressor replaced')
INSERT INTO [dbo].[BookingItems] ([BookingId], [MaintenanceId], [Comment]) VALUES (5, 5, N'Equipment calibrated accurately according to ISO standard')

