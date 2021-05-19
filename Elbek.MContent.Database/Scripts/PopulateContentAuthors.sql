MERGE INTO [dbo].[ContentAuthors] AS [Target]
	USING (VALUES
		('4cbdeed9-8ff4-4106-ba71-b9bb7b38325a', 'a01e0a65-dee9-401e-865a-6778f160a47a', 'bb8cd20b-7590-487e-a9b5-a00db9127ad2'),
		('587b272b-87c0-4a6c-b355-155b0cbea5da', '4728e207-9a41-4faf-95fc-ffcdbf8665cd', '6461ad0c-78aa-4214-aa27-b37bf83e61cc'),
		('da1018ad-645a-425a-b2ba-b423f58039f9', '8827a36a-f0cb-4c67-afd0-bf961e097125', 'b7efb5d6-77bf-4f5d-8df3-06d8fa85c538'),
		('0c2b9e9d-b25c-465a-99ea-e79da0d4bef2', '1569d652-0cf9-4893-8df8-80a9f78bb963', 'c8a5d760-7106-4012-a772-74ece852b7a0'),
		('628bd1cb-edf8-4836-8161-b6ac355a25e1', 'd53efed0-ede9-46fb-917a-d1a22bdc909c', 'f0facb70-4878-4a63-a9e9-2a44432928d8'),
		('52a3b7cc-42b3-4b61-adb0-b92709028af7', '45d75eaf-bea3-4322-b370-d316c1bfca34', '7da56ff4-b308-47c5-aa2e-d0a9b9cbb72c'),
		('6eaeb995-2267-4599-87c9-cfd982946d7c', '0d9d6096-d506-47f4-8097-3fbe81b65667', '87ad0c26-536e-4dd2-b9aa-f594ef7366ec'),
		('0fd1ae8f-bd1d-4d1d-8bc8-39569944e3af', '818ff237-5d02-4167-b490-43785a4dc83c', '4f126a83-430f-4eeb-b2bc-101578755e79'),
		('bc3d1f41-33ed-4058-babd-a4ff81f59599', '65f9b826-0697-4e36-a1ca-c20535ba6381', '8987c1e4-eab0-47d5-8fa4-40f64ddc1de1')
 	) 
AS [Source] ([Id], [AuthorId], [ContentId])
	ON [Target].[Id] = [Source].[Id] 
WHEN MATCHED THEN
	UPDATE SET 
		[AuthorId] = [Source].[AuthorId],
		[ContentId] = [Source].[ContentId] 
WHEN NOT MATCHED BY TARGET THEN 
	INSERT ([Id], [AuthorId], [ContentId])
	VALUES ([Source].[Id], [Source].[AuthorId],[Source].[ContentId]);