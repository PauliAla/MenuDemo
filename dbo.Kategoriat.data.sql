SET IDENTITY_INSERT [dbo].[Kategoriat] ON
INSERT INTO [dbo].[Kategoriat] ([Id], [Nimi], [Kuvaus], [RuokalistaId]) VALUES (1, N'Alkuruoat', N'Pientä purtavaa ennen Pääruokaa', 1)
INSERT INTO [dbo].[Kategoriat] ([Id], [Nimi], [Kuvaus], [RuokalistaId]) VALUES (2, N'Pääruoat', N'Aterian Pääruoka', 1)
INSERT INTO [dbo].[Kategoriat] ([Id], [Nimi], [Kuvaus], [RuokalistaId]) VALUES (3, N'Jälkiruoat', N'Kenties makeaakin', 1)
INSERT INTO [dbo].[Kategoriat] ([Id], [Nimi], [Kuvaus], [RuokalistaId]) VALUES (4, N'Juomat', N'Juomaa, alkoholitonta tai alkoholillista', 1)
SET IDENTITY_INSERT [dbo].[Kategoriat] OFF
