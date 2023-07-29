CREATE TABLE [dbo].[Message]
(
	[UserId] INT NOT NULL, 
    [ContactId] INT NOT NULL, 
    [SendTime] DATETIME NOT NULL, 
    [DeliveryTime] DATETIME NULL, 
    [Content] NVARCHAR(MAX) NOT NULL, 
    CONSTRAINT [FK_MessageUserId_UserId] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]),
    CONSTRAINT [FK_MessageContactId_UserId] FOREIGN KEY ([ContactId]) REFERENCES [User]([Id])
)
