CREATE TABLE [dbo].[Contact]
(
	[UserId] INT NOT NULL, 
    [ContactId] INT NOT NULL, 
    [LastUpdateTime] DATETIME NULL, 
    CONSTRAINT [PK_Contact] PRIMARY KEY ([UserId], [ContactId]),
    CONSTRAINT [FK_ContactUserId_UserId] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]),
    CONSTRAINT [FK_ContactContactId_UserId] FOREIGN KEY ([ContactId]) REFERENCES [User]([Id])
)
