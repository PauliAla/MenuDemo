CREATE TABLE [dbo].[Ruokalistat]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Nimi] VARCHAR(50) NULL, 
    [Kuvaus] VARCHAR(400) NULL, 
    [RavintolaId] INT NULL, 
    CONSTRAINT [FK_Ruokalistat_Ravintolalista] FOREIGN KEY ([RavintolaId]) REFERENCES [Ravintolalista]([Id])
)
