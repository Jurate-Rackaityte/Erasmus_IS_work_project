CREATE TABLE [dbo].[Message] (
    [ID]     INT  NOT NULL IDENTITY,
    [Text]     NVARCHAR (MAX) NOT NULL,
    [Username] NVARCHAR (450) NOT NULL,
    CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED ([ID] ASC), 
    CONSTRAINT [FK_Message_User] FOREIGN KEY ([Username]) REFERENCES [User]([Username])
);

