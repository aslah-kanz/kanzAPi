IF NOT EXISTS (SELECT 1 FROM [Product].[Attribute] a WHERE a.NameEn = 'Document')
BEGIN

	DECLARE @maxSort INT
	SET @maxSort = (SELECT TOP 1 a.SortOrder from [Product].[Attribute] a ORDER BY SortOrder DESC)

	SET @maxSort = @maxSort + 1

	INSERT INTO [Product].[Attribute]
			([SortOrder]
			,[GroupOrder]
			,[CreatedAt]
			,[CreatedBy]
			,[UpdatedAt]
			,[UpdatedBy]
			,[GroupEn]
			,[Code]
			,[GroupAr]
			,[NameAr]
			,[NameEn]
			,[Unit1Ar]
			,[Unit1En]
			,[Unit2Ar]
			,[Unit2En]
			,[Unit3Ar]
			,[Unit3En])
		VALUES
			(@maxSort
			,0
			,GETDATE()
			,-3
			,GETDATE()
			,-3
			,'NoGroup'
			,NULL
			,N'ما في'
			,NULL
			,'Document'
			,NULL
			,NULL
			,NULL
			,NULL
			,NULL
			,NULL)

END

IF NOT EXISTS (SELECT 1 FROM [Product].[Property] a WHERE a.[Type] = 'Document')
BEGIN
	DECLARE @maxPropertySort INT
	
	SET @maxPropertySort = (SELECT TOP 1 a.SortOrder from [Product].[Property] a ORDER BY SortOrder DESC)
	SET @maxPropertySort = @maxPropertySort + 1

	INSERT INTO [Product].[Property]
           ([Code]
           ,[NameEn]
           ,[NameAr]
           ,[FieldsEn]
           ,[FieldsAr]
           ,[Type]
           ,[SortOrder]
           ,[CreatedAt]
           ,[CreatedBy]
           ,[UpdatedAt]
           ,[UpdatedBy])
     VALUES
           (NULL
           ,'Document'
           ,NULL
           ,'["Attribute", "Value"]'
           ,N'["وصف", "قيمة"]'
           ,'Document'
           ,@maxPropertySort
           ,GETDATE()
           ,-3
           ,GETDATE()
           ,-3)
END
