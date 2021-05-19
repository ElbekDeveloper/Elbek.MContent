MERGE INTO [dbo].[Contents] AS [Target]
	USING (VALUES
		('bb8cd20b-7590-487e-a9b5-a00db9127ad2', 'Sed ante. Vivamus tortor. Duis mattis egestas metus.', 2),
		('6461ad0c-78aa-4214-aa27-b37bf83e61cc', 'Duis bibendum. Morbi non quam nec dui luctus rutrum. Nulla tellus.', 1),
		('b7efb5d6-77bf-4f5d-8df3-06d8fa85c538', 'Morbi porttitor lorem id ligula. Suspendisse ornare consequat lectus. In est risus, auctor sed, tristique in, tempus sit amet, sem.', 2),
		('c8a5d760-7106-4012-a772-74ece852b7a0', 'Proin eu mi. Nulla ac enim. In tempor, turpis nec euismod sceleitae mattis nibh ligula nec sem.', 0),
		('f0facb70-4878-4a63-a9e9-2a44432928d8', 'Duis consequat dui nec nisi volutpat eleifend. Donec ut dolor. Morbi vel lectus in quam fringilla rhoncus.', 3),
		('7da56ff4-b308-47c5-aa2e-d0a9b9cbb72c', 'Sed ante. Vivamus tortor. Duis mattis egestas metus.', 0),
		('87ad0c26-536e-4dd2-b9aa-f594ef7366ec', 'Phasellus sit amet erat. Nulla tempus. Vivamus in felis eu sapien cursus vestibulum.', 3),
		('4f126a83-430f-4eeb-b2bc-101578755e79', 'Quisque id justo sit amet sapien dignissim vestibulum. Vestibulum ante ipsum pri eros.', 2),
		('8987c1e4-eab0-47d5-8fa4-40f64ddc1de1', 'Maecenas leo odio, condimentum id, luctus nec, molestie sed, justo. Pellentesque viverra pede ac diam. Cras pellentesque volutpat dui.', 1)
 	) 
AS [Source] ([Id], [Title], [Type])
	ON [Target].[Id] = [Source].[Id] 
WHEN MATCHED THEN
	UPDATE SET 
		[Title] = [Source].[Title],
		[Type] = [Source].[Type] 
WHEN NOT MATCHED BY TARGET THEN 
	INSERT ([Id], [Title], [Type])
	VALUES ([Source].[Id], [Source].[Title],[Source].[Type]);