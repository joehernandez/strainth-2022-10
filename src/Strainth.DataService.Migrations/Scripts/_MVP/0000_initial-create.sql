IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221008051408_Initial-Create')
BEGIN
    CREATE TABLE [Categories] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221008051408_Initial-Create')
BEGIN
    CREATE TABLE [ProgramSplits] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [StartDate] datetime2 NOT NULL,
        [EndDate] datetime2 NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_ProgramSplits] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221008051408_Initial-Create')
BEGIN
    CREATE TABLE [Exercises] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [CategoryId] int NOT NULL,
        CONSTRAINT [PK_Exercises] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Exercises_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221008051408_Initial-Create')
BEGIN
    CREATE TABLE [ProgramDetails] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [DayNumber] int NOT NULL,
        [ProgramSplitId] int NOT NULL,
        CONSTRAINT [PK_ProgramDetails] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProgramDetails_ProgramSplits_ProgramSplitId] FOREIGN KEY ([ProgramSplitId]) REFERENCES [ProgramSplits] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221008051408_Initial-Create')
BEGIN
    CREATE TABLE [ProgramExcercises] (
        [Id] int NOT NULL IDENTITY,
        [RepsRangeLower] int NOT NULL,
        [RepsRangeUpper] int NOT NULL,
        [SetsRangeLower] int NOT NULL,
        [SetsRangeUpper] int NOT NULL,
        [RepsThreshold] int NOT NULL,
        [WeightIncrement] decimal(18,2) NOT NULL,
        [ExerciseId] int NOT NULL,
        [ProgramDetailId] int NOT NULL,
        CONSTRAINT [PK_ProgramExcercises] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProgramExcercises_Exercises_ExerciseId] FOREIGN KEY ([ExerciseId]) REFERENCES [Exercises] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ProgramExcercises_ProgramDetails_ProgramDetailId] FOREIGN KEY ([ProgramDetailId]) REFERENCES [ProgramDetails] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221008051408_Initial-Create')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Categories]'))
        SET IDENTITY_INSERT [Categories] ON;
    EXEC(N'INSERT INTO [Categories] ([Id], [Name])
    VALUES (1, N''Abs''),
    (2, N''Calves''),
    (3, N''Curl''),
    (4, N''Extend''),
    (5, N''Hinge''),
    (6, N''Press''),
    (7, N''Pull''),
    (8, N''Push''),
    (9, N''Row''),
    (10, N''Shoulders''),
    (11, N''Squat'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Categories]'))
        SET IDENTITY_INSERT [Categories] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221008051408_Initial-Create')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CategoryId', N'Name') AND [object_id] = OBJECT_ID(N'[Exercises]'))
        SET IDENTITY_INSERT [Exercises] ON;
    EXEC(N'INSERT INTO [Exercises] ([Id], [CategoryId], [Name])
    VALUES (1, 1, N''Ab Wheel''),
    (2, 1, N''Bicycle Crunch''),
    (3, 2, N''Seated Barbell Calf Raise''),
    (4, 2, N''Standing Calf Raise''),
    (5, 3, N''Incline Dumbbell Curl''),
    (6, 3, N''Dumbbell Spider Curl''),
    (7, 4, N''Rope Pushdown''),
    (8, 4, N''Standing OH Cable Extension''),
    (9, 5, N''Deadlift - Sumo''),
    (10, 5, N''Back Extension''),
    (11, 5, N''Floor Hamstring Curl''),
    (12, 5, N''Pull Through''),
    (13, 6, N''Seated OH Press''),
    (14, 6, N''Arnold Press''),
    (15, 7, N''Chinup''),
    (16, 7, N''Neutral Grip Pulldown''),
    (17, 7, N''Underhand Cable Pullover''),
    (18, 8, N''Incline Bench Press''),
    (19, 8, N''Incline Dumbbell Press Fly''),
    (20, 8, N''Close Grip Bench Press''),
    (21, 9, N''Chest-supported Dumbbell Row''),
    (22, 9, N''Cable Upright Row''),
    (23, 10, N''Lateral Raise''),
    (24, 10, N''Prone Rear Delt Raise''),
    (25, 10, N''Skiers''),
    (26, 11, N''Slantboard Front Squat''),
    (27, 11, N''ATG Split Squat'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CategoryId', N'Name') AND [object_id] = OBJECT_ID(N'[Exercises]'))
        SET IDENTITY_INSERT [Exercises] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221008051408_Initial-Create')
BEGIN
    CREATE INDEX [IX_Exercises_CategoryId] ON [Exercises] ([CategoryId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221008051408_Initial-Create')
BEGIN
    CREATE INDEX [IX_ProgramDetails_ProgramSplitId] ON [ProgramDetails] ([ProgramSplitId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221008051408_Initial-Create')
BEGIN
    CREATE INDEX [IX_ProgramExcercises_ExerciseId] ON [ProgramExcercises] ([ExerciseId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221008051408_Initial-Create')
BEGIN
    CREATE INDEX [IX_ProgramExcercises_ProgramDetailId] ON [ProgramExcercises] ([ProgramDetailId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221008051408_Initial-Create')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20221008051408_Initial-Create', N'6.0.9');
END;
GO

COMMIT;
GO

