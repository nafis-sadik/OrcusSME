/*
   Wednesday, February 16, 20221:10:58 AM
   User: 
   Server: WORKSTATION
   Database: OrcusSME
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Orders SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Orders', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Orders', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Orders', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.Products SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Products', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Products', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Products', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.InventoryLog ADD
	OrderId int NULL
GO
ALTER TABLE dbo.InventoryLog ADD CONSTRAINT
	FK_InventoryLog_Products FOREIGN KEY
	(
	ProductId
	) REFERENCES dbo.Products
	(
	ProductId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.InventoryLog ADD CONSTRAINT
	FK_InventoryLog_Orders FOREIGN KEY
	(
	OrderId
	) REFERENCES dbo.Orders
	(
	OrderId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.InventoryLog SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.InventoryLog', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.InventoryLog', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.InventoryLog', 'Object', 'CONTROL') as Contr_Per 