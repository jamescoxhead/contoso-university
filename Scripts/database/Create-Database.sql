IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'ContsoUniversity')
BEGIN
    CREATE DATABASE [ContosoUniversity];
END;
GO
