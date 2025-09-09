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
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250907172126_InitialCreate'
)
BEGIN
    CREATE TABLE [UrlMaps] (
        [Id] bigint NOT NULL IDENTITY,
        [OriginalUrl] nvarchar(max) NOT NULL,
        [ShortUrl] nvarchar(20) NOT NULL,
        CONSTRAINT [PK_UrlMaps] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250907172126_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_UrlMaps_ShortUrl] ON [UrlMaps] ([ShortUrl]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250907172126_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250907172126_InitialCreate', N'9.0.8');
END;

COMMIT;
GO

