CREATE TABLE [dbo].[Usuarios] (
    [Id]       UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Nome]     VARCHAR (100)    NOT NULL,
    [Email]    VARCHAR (50)     NOT NULL,
    [Senha]    VARCHAR (100)    NOT NULL,
    [Telefone] VARCHAR (15)     NOT NULL,
    [Endereco] VARCHAR (100)    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

