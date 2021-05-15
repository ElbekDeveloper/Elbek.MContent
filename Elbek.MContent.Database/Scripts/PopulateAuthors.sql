IF EXISTS (SELECT 1 FROM SYS.TABLES where name ='Authors_Sample')
BEGIN 
DROP TABLE Authors_Sample
END
 
IF EXISTS (SELECT 1 FROM SYS.TABLES where name ='Authors')
BEGIN 
DROP TABLE Authors
END
 
CREATE TABLE [dbo].[Authors]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(55)  NOT NULL
)
GO
 
 
 
CREATE TABLE [dbo].[Authors_Sample]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(55)  NOT NULL
)
GO
 
 
insert into Authors_Sample (id, name) values ('a01e0a65-dee9-401e-865a-6778f160a47a', 'Elvina Thurnham');
insert into Authors_Sample (id, name) values ('4728e207-9a41-4faf-95fc-ffcdbf8665cd', 'Teirtza Heinel');
insert into Authors_Sample (id, name) values ('9b86aba3-4cd6-4e6d-96cb-0bf4424706c4', 'Alejandrina Twinterman');
insert into Authors_Sample (id, name) values ('8827a36a-f0cb-4c67-afd0-bf961e097125', 'Brew Kingsworth');
insert into Authors_Sample (id, name) values ('1569d652-0cf9-4893-8df8-80a9f78bb963', 'Teddi Gullivan');
insert into Authors_Sample (id, name) values ('d53efed0-ede9-46fb-917a-d1a22bdc909c', 'Temple Strang');
insert into Authors_Sample (id, name) values ('45d75eaf-bea3-4322-b370-d316c1bfca34', 'Katey Hurlin');
insert into Authors_Sample (id, name) values ('0d9d6096-d506-47f4-8097-3fbe81b65667', 'Ellary Dwire');
insert into Authors_Sample (id, name) values ('818ff237-5d02-4167-b490-43785a4dc83c', 'Maia Essex');
insert into Authors_Sample (id, name) values ('65f9b826-0697-4e36-a1ca-c20535ba6381', 'Selestina O''Docherty');
 
 
MERGE Authors T
USING Authors_Sample S ON T.id =S.id
WHEN NOT MATCHED BY TARGET 
THEN 
INSERT (id,name)
VALUES (S.id,S.name);

DROP TABLE Authors_Sample