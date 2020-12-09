CREATE TABLE [dbo].[Annokset]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Nimi] VARCHAR(50) NULL, 
    [Kuvaus] VARCHAR(400) NULL, 
    [Hinta] FLOAT NULL, 
    [KategoriaId] INT NULL, 
    [AllergeeniId] CHAR(10) NULL, 
    [AnnosTyyppiId] INT NULL,  
    CONSTRAINT [FK_Annokset_AnnosTyyppi] FOREIGN KEY ([AnnosTyyppiId]) REFERENCES [AnnosTyyppi]([Id])
)
