CREATE TABLE [dbo].[Estabelecimento] (
    [Id]             UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Nome]           VARCHAR (50)     NOT NULL,
    [Endereco]       VARCHAR (50)     NOT NULL,
    [HoraFechamento] NCHAR (5)        NOT NULL,
    [HoraAbertura]   NCHAR (5)        NOT NULL
);

