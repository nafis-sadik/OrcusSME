/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [OutletId]
      ,[OutletName]
      ,[OutletAddresss]
      ,[EComURL]
      ,[UserId]
      ,[RequestSite]
      ,[SiteUrl]
      ,[Status]
  FROM [OrcusSME].[dbo].[Outlets]

  UPDATE [OrcusSME].[dbo].[Outlets]
  SET [Status] = 'D'
  WHERE OutletId = 2;