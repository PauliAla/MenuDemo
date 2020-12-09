CREATE TABLE [dbo].[Kategoriat]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Nimi] VARCHAR(50) NULL, 
    [Kuvaus] VARCHAR(400) NULL, 
    [RuokalistaId] INT NULL, 
    CONSTRAINT [FK_Kategoriat_ToTable] FOREIGN KEY ([RuokalistaId]) REFERENCES [Ruokalistat](Id)
)
