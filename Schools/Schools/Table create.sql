CREATE TABLE [dbo].[School] (
    [Id]       INT         IDENTITY (1, 1) NOT NULL,
    [Number]   NCHAR (10)  NULL,
    [FullName] NCHAR (200) NULL,
    [City]     NCHAR (100) NULL,
    [Adress]   NCHAR (200) NOT NULL,
    [Phone]    NCHAR (10)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Class] (
    [Id]       INT        IDENTITY (1, 1) NOT NULL,
    [Number]   INT        NOT NULL,
    [Group]    NCHAR (10) NOT NULL,
    [SchoolId] INT        NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Class_ToSchool] FOREIGN KEY ([SchoolId]) REFERENCES [dbo].[School] ([Id])
);

CREATE TABLE [dbo].[Student] (
    [Id]         INT        IDENTITY (1, 1) NOT NULL,
    [FirstName]  NCHAR (50) NOT NULL,
    [LastName]   NCHAR (50) NOT NULL,
    [MiddleName] NCHAR (50) NOT NULL,
    [SchoolId]   INT        NOT NULL,
    [ClassId]    INT        NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Student_ToSchool] FOREIGN KEY ([SchoolId]) REFERENCES [dbo].[School] ([Id]),
    CONSTRAINT [FK_Student_ToClass] FOREIGN KEY ([ClassId]) REFERENCES [dbo].[Class] ([Id])
);

