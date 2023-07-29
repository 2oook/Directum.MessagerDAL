CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(200) NOT NULL, 
    [Password] NVARCHAR(20) NOT NULL, 
    [State] INT NOT NULL
)
