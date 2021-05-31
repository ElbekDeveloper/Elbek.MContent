MERGE INTO [dbo].[Advert] AS [Target]
	USING (VALUES 
			('c177d3a0-e002-498a-a285-126558cfa520', '6461ad0c-78aa-4214-aa27-b37bf83e61cc', 1, 1, '6/11/2020', '9/19/2020'),
			('b428c5ce-290c-42bb-a11f-e340b3548439', 'b7efb5d6-77bf-4f5d-8df3-06d8fa85c538', 1, 1, '1/30/2021', null),
			('0c796261-9325-4a2b-bfd1-e425c2f0176c', 'f0facb70-4878-4a63-a9e9-2a44432928d8', 1, 0, '3/30/2021', null),
			('64f20cfb-1895-4a94-bc90-429d05f35c94', '87ad0c26-536e-4dd2-b9aa-f594ef7366ec', 0, 0, '5/27/2021', '6/22/2020'),
			('439e9b2f-4cc4-4724-a7d5-d8c414321cfd', '4f126a83-430f-4eeb-b2bc-101578755e79', 0, 1, '2/14/2021', null),
			('a33a2621-092b-48ea-a9df-494049b9cd16', '8987c1e4-eab0-47d5-8fa4-40f64ddc1de1', 0, 1, '1/16/2021', null)
) 
AS [Source] ([Id],
					[ContentId],
					[IsActive],
					[IsDeleted],
					[PublishDate],
					[UpdateDate])
	ON [Target].[Id] = [Source].[Id] 
WHEN MATCHED THEN
	UPDATE SET 
					[ContentId]= [Source].[ContentId],
					[IsActive]= [Source].[IsActive],
					[IsDeleted]= [Source].[IsDeleted],
					[PublishDate]= [Source].[PublishDate],
					[UpdateDate]= [Source].[UpdateDate]
WHEN NOT MATCHED BY TARGET THEN 
	INSERT (	[Id],
					[ContentId],
					[IsActive],
					[IsDeleted],
					[PublishDate],
					[UpdateDate] )
	VALUES ( [Source].[Id],
					[Source].[ContentId],
					[Source].[IsActive],
					[Source].[IsDeleted],
					[Source].[PublishDate],
					[Source].[UpdateDate] );