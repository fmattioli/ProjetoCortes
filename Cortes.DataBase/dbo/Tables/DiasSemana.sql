CREATE TABLE [dbo].[DiasSemana] (
    [Id]     UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Codigo] INT              NOT NULL,
    [Dia]    VARCHAR (15)     NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

