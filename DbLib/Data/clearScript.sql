USE [PokupochkaDB]
GO
INSERT [dbo].[Status] ([Id], [Title], [Color]) VALUES (1, N'В обработке', N'Orange')
INSERT [dbo].[Status] ([Id], [Title], [Color]) VALUES (2, N'Активный', N'LightGreen')
INSERT [dbo].[Status] ([Id], [Title], [Color]) VALUES (3, N'Отклонён', N'Red')
INSERT [dbo].[Status] ([Id], [Title], [Color]) VALUES (4, N'Истёк', N'Red')
INSERT [dbo].[Status] ([Id], [Title], [Color]) VALUES (5, N'Приостановлен', N'Orange')
GO
INSERT [dbo].[Roles] ([Id], [Title], [Description], [Image]) VALUES (1, N'Администратор', NULL, N'admin.png')
INSERT [dbo].[Roles] ([Id], [Title], [Description], [Image]) VALUES (2, N'Агент', NULL, N'agent.png')
INSERT [dbo].[Roles] ([Id], [Title], [Description], [Image]) VALUES (3, N'Поставщик', NULL, N'supplier.png')
INSERT [dbo].[Roles] ([Id], [Title], [Description], [Image]) VALUES (4, N'Клиент', NULL, N'client.png')
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Login], [Password], [RoleId]) VALUES (1, N'ad', N'ad', 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[Workers] ON 

INSERT [dbo].[Workers] ([Id], [SecondName], [FirstName], [Patronymic], [Phone], [Email], [UserId]) VALUES (1, N'Админов', N'Админ', N'Админович', N'79500195255', N'korshun.Herooo@yandex.ru', 1)
SET IDENTITY_INSERT [dbo].[Workers] OFF
GO
