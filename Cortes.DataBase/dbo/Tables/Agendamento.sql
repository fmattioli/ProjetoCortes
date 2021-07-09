CREATE TABLE [dbo].[Agendamento] (
    [Id]            UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [NomeCliente]   VARCHAR (50)     NOT NULL,
    [Endereco]      VARCHAR (50)     NULL,
    [DiasSemana_Id] UNIQUEIDENTIFIER NOT NULL,
    [Horario]       NCHAR (5)        NOT NULL,
    [Usuario_Id]    UNIQUEIDENTIFIER NOT NULL,
    [Preco]         MONEY            NOT NULL,
    [DataCorte]     DATETIME         NOT NULL,
    [Compareu]      SMALLINT         DEFAULT ((0)) NOT NULL,
    FOREIGN KEY ([DiasSemana_Id]) REFERENCES [dbo].[DiasSemana] ([Id]),
    FOREIGN KEY ([Usuario_Id]) REFERENCES [dbo].[Usuarios] ([Id])
);

