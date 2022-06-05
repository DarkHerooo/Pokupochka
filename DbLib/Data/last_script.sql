USE [PokupochkaDB]
GO
INSERT [dbo].[Roles] ([Id], [Title], [Description], [Image]) VALUES (1, N'Администратор', NULL, N'admin.png')
INSERT [dbo].[Roles] ([Id], [Title], [Description], [Image]) VALUES (2, N'Агент', NULL, N'agent.png')
INSERT [dbo].[Roles] ([Id], [Title], [Description], [Image]) VALUES (3, N'Поставщик', NULL, N'supplier.png')
INSERT [dbo].[Roles] ([Id], [Title], [Description], [Image]) VALUES (4, N'Клиент', NULL, N'client.png')
GO
INSERT [dbo].[Status] ([Id], [Title], [Color]) VALUES (1, N'В обработке', N'Orange')
INSERT [dbo].[Status] ([Id], [Title], [Color]) VALUES (2, N'Активный', N'Green')
INSERT [dbo].[Status] ([Id], [Title], [Color]) VALUES (3, N'Отклонён', N'Red')
INSERT [dbo].[Status] ([Id], [Title], [Color]) VALUES (4, N'Истёк', N'Red')
INSERT [dbo].[Status] ([Id], [Title], [Color]) VALUES (5, N'Приостановлен', N'Orange')
INSERT [dbo].[Status] ([Id], [Title], [Color]) VALUES (6, N'В пути', N'Blue')
INSERT [dbo].[Status] ([Id], [Title], [Color]) VALUES (7, N'Доставлено', N'Green')
GO
SET IDENTITY_INSERT [dbo].[Workers] ON 

INSERT [dbo].[Workers] ([Id], [SecondName], [FirstName], [Patronymic], [Phone], [Email], [UserId]) VALUES (1, N'Админов', N'Админ', N'Админович', N'79500195255', N'korshun.Herooo@yandex.ru', 1)
INSERT [dbo].[Workers] ([Id], [SecondName], [FirstName], [Patronymic], [Phone], [Email], [UserId]) VALUES (2, N'Агентов', N'Агент', N'Агентович', N'+7 (777) 777 77 77', N'agent007@yandex.ru', 2)
SET IDENTITY_INSERT [dbo].[Workers] OFF
GO
