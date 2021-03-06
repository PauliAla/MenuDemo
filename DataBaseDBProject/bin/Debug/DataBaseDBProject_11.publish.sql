﻿/*
Deployment script for MenuDemoV2

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "MenuDemoV2"
:setvar DefaultFilePrefix "MenuDemoV2"
:setvar DefaultDataPath "C:\Users\Pauli\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"
:setvar DefaultLogPath "C:\Users\Pauli\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'The following operation was generated from a refactoring log file f8ae778b-3ea0-41d2-a403-6e43d3d842f8';

PRINT N'Rename [dbo].[Ravintolalista].[Kruunuhaka] to Nimi';


GO
EXECUTE sp_rename @objname = N'[dbo].[Ravintolalista].[Kruunuhaka]', @newname = N'Nimi', @objtype = N'COLUMN';


GO
-- Refactoring step to update target server with deployed transaction logs
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'f8ae778b-3ea0-41d2-a403-6e43d3d842f8')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('f8ae778b-3ea0-41d2-a403-6e43d3d842f8')

GO

GO
PRINT N'Update complete.';


GO
