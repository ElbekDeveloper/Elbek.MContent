MERGE INTO [dbo].[Authors] AS [Target]
	USING (VALUES 
		('a01e0a65-dee9-401e-865a-6778f160a47a', 'Elvina Thurnham'),
		('4728e207-9a41-4faf-95fc-ffcdbf8665cd', 'Teirtza Heinel'),
		('9b86aba3-4cd6-4e6d-96cb-0bf4424706c4', 'Alejandrina Twinterman'),
		('8827a36a-f0cb-4c67-afd0-bf961e097125', 'Brew Kingsworth'),
		('1569d652-0cf9-4893-8df8-80a9f78bb963', 'Teddi Gullivan'),
		('d53efed0-ede9-46fb-917a-d1a22bdc909c', 'Temple Strang'),
		('45d75eaf-bea3-4322-b370-d316c1bfca34', 'Katey Hurlin'),
		('0d9d6096-d506-47f4-8097-3fbe81b65667', 'Ellary Dwire'),
		('818ff237-5d02-4167-b490-43785a4dc83c', 'Maia Es'),
		('65f9b826-0697-4e36-a1ca-c20535ba6381', 'Selestina O''Docherty')
	) 
AS [Source] ([Id], [Name])
	ON [Target].[Id] = [Source].[Id] 
WHEN MATCHED THEN
	UPDATE SET 
		[Name] = [Source].[Name]
WHEN NOT MATCHED BY TARGET THEN 
	INSERT ([Id], [Name])
	VALUES ([Source].[Id], [Source].[Name]);