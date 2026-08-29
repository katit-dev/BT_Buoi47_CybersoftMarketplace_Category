USE CybersoftMarketplace;

ALTER TABLE UserRoles
ADD [desc] NVARCHAR(255) NULL;

SELECT TOP (1000) [Id]
      ,[Name]
      ,[Alias]
      ,[AdditionalData]
      ,[Deleted]
      ,[ShopId]
  FROM [CybersoftMarketplace].[dbo].[Categories]
  WHERE [ShopId] = 102

  -- Xóa cứng (Hard Delete) category có tên "Gaming Gear"
DELETE FROM [CybersoftMarketplace].[dbo].[Categories]
WHERE [Name] = 'string'