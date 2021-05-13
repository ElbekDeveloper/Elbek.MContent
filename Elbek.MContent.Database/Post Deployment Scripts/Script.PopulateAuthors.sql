/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

/*
   Clear out the existing data
*/

-- TODO 1
-- это файл с BuildAction = PostDeploy, но у тебя будет много разных постдеплой скриптов, 
-- поэтому правильне будет вынести скрипты в отдельные sql файлы и эти файлы(скрипты) вызывать в этом файле.
-- 1) создать папку Scrupts
-- 2) создать в ней sql файл с кодом на для заполнения авторов.

-- TODO 2
-- скрипт заполнения авторов переделать в MERGE (create/update). Загугли MERGE script to fill initial data


delete from Authors;

insert into Authors (id, Name) values ('9d6d2319-5373-4889-965b-27719e148ea5', 'Tomasina Corriea');
insert into Authors (id, Name) values ('f5e7bd9e-3f6d-4cd3-9d43-41244ced4b28', 'Daphne Matteoni');
insert into Authors (id, Name) values ('df2aa010-503d-4bdb-98b6-e2e2f3ce23bf', 'Abramo Domino');
insert into Authors (id, Name) values ('7dcd4aa8-ac6d-4686-a041-34e5edb4c743', 'Reilly Hopkynson');
insert into Authors (id, Name) values ('f656411d-bfcc-45f6-bdd3-2bbc33d8ebc0', 'Laurice Padginton');
insert into Authors (id, Name) values ('45a49c0b-08c1-4dab-ab76-a18f4cd26343', 'Mandy Orfeur');
insert into Authors (id, Name) values ('39182dcc-25dd-45f0-95d1-26ae8dae3a2c', 'Murray Temblett');
insert into Authors (id, Name) values ('b4c0c023-4768-4d92-aa98-42370091cfe1', 'Stacy Sturch');
insert into Authors (id, Name) values ('388c2935-1eed-477a-a1ef-6f237bfa3460', 'Joaquin Cussons');
insert into Authors (id, Name) values ('fa52b418-2d96-4885-ac78-1c68944a890d', 'Timmy Mougenel');
insert into Authors (id, Name) values ('0d1d772d-3d08-4655-8801-09574b329f7d', 'Shirlee Childerhouse');
insert into Authors (id, Name) values ('059715dc-c618-41a4-879b-27bfefbb3d40', 'Barry Fechnie');
insert into Authors (id, Name) values ('7ec7b4e6-a067-44b6-bfea-5a8fdb97981e', 'Shannah Giovannazzi');
insert into Authors (id, Name) values ('a50b5ff6-d1de-44b1-8e09-90745a96dfe8', 'Thane Cuesta');
insert into Authors (id, Name) values ('b6390657-692f-454e-9288-68b6a97286c9', 'Mano Amar');
insert into Authors (id, Name) values ('4a136902-ef8d-44b6-bb25-7c265e3eafd7', 'Janela Urvoy');
insert into Authors (id, Name) values ('7a09b60e-83a3-480c-b38d-53e433151482', 'Steffen Clowton');
insert into Authors (id, Name) values ('beb331c1-e877-4998-8280-11da42c94ffa', 'Frank Mourant');
insert into Authors (id, Name) values ('b79896ec-c56e-4bed-9bb8-5bfc7e2f7478', 'Justinian Shorte');
insert into Authors (id, Name) values ('edd040f8-c5f1-4f42-a275-5f7e6cd073b3', 'Jilly Burkart');
insert into Authors (id, Name) values ('ebe5e1fe-6a45-4245-959d-e57cc68de60a', 'Dru Sowman');
insert into Authors (id, Name) values ('ae856fa8-95c9-4316-a8ba-fcbbc1083054', 'Nickie Lowseley');
insert into Authors (id, Name) values ('166c4b39-8552-4619-884b-7ced0613c34a', 'Skye Memory');
insert into Authors (id, Name) values ('185b0cb6-b9a4-4e2e-9069-9cb1722189de', 'Taddeo Adhams');
insert into Authors (id, Name) values ('23051923-45b7-499b-9576-f78668591382', 'Olly McCrory');
insert into Authors (id, Name) values ('6ba135d9-5699-461e-a037-02cd0dad5fcb', 'Evin Hinckesman');
insert into Authors (id, Name) values ('9ba58889-4f46-491f-a631-dc9270892c18', 'Arv Bodesson');
insert into Authors (id, Name) values ('7ec5592a-1136-42d8-9a91-aa8dc8eb5019', 'Hillel Seadon');
insert into Authors (id, Name) values ('eb414a95-01c4-4468-83e5-f342f5101865', 'Rube Hansie');
insert into Authors (id, Name) values ('c91c09c0-300d-4883-b9dc-620a86812347', 'Edythe Talby');
insert into Authors (id, Name) values ('baf21c85-c417-41cf-8410-a7961277ccd4', 'Agathe Agates');
insert into Authors (id, Name) values ('7bb504bf-fe42-4c73-b185-63436d87d1dd', 'Natalee Chesshyre');
insert into Authors (id, Name) values ('79f40d7e-20f3-49bc-ae4b-d9dc91f1e6b8', 'Dimitri Jerrolt');
insert into Authors (id, Name) values ('0b603ece-5e53-4d04-87d5-a503fb5027bf', 'Hildegarde Skune');
insert into Authors (id, Name) values ('4926fabc-7762-4be0-8ca8-f43335150654', 'Justine Ladlow');
insert into Authors (id, Name) values ('9b936add-4d12-4886-bb27-0c4f241b5200', 'Katrine Bum');
insert into Authors (id, Name) values ('d911061d-a457-44aa-8701-09a2ba7f93d4', 'Jack Bisseker');
insert into Authors (id, Name) values ('dea904bd-51e7-4d98-babb-a4f7f888b393', 'Lemar Sheppey');
insert into Authors (id, Name) values ('61044f42-8105-4611-96a6-95e90f8a9a2a', 'Igor Rainard');
insert into Authors (id, Name) values ('8e08d949-ac7e-4268-8683-1c4645fd3241', 'Billie McCarrison');
insert into Authors (id, Name) values ('77543e73-0573-43b0-8cd7-134702bb3d92', 'Cammy McNeigh');
insert into Authors (id, Name) values ('fe112449-f77e-443e-91e7-f55e70ae0343', 'Jarvis Balchin');
insert into Authors (id, Name) values ('4494ecfc-80cb-446f-a37c-ecf7fc7dac60', 'Tallie Pfeifer');
insert into Authors (id, Name) values ('d0ad844b-1f9c-4742-9495-9e5a8cfe9c83', 'Siobhan Botterman');
insert into Authors (id, Name) values ('df9e9a01-dffa-4e18-ab99-84de2916e112', 'Rhona McSpirron');
insert into Authors (id, Name) values ('552618cf-d29a-4020-9040-b617f19d2159', 'Alice Beccero');
insert into Authors (id, Name) values ('8b8beaa6-2f70-47c0-af22-1f0226e3f172', 'Quill Denerley');
insert into Authors (id, Name) values ('369b9549-59a1-4288-bcb2-a51125859dba', 'Bobbie Bettinson');
insert into Authors (id, Name) values ('ab17d824-0fcb-45e6-8fe6-e56cb3d9dd17', 'Burlie Gascone');
insert into Authors (id, Name) values ('58efb072-fd9b-4b7e-963f-534999221860', 'Sherlock Verralls');
insert into Authors (id, Name) values ('d5e4e50e-96b8-47db-9468-a6cf9e407bb9', 'Corrinne Bendix');
insert into Authors (id, Name) values ('cb105199-2ca3-4861-b999-203b5dbe2e73', 'Kirsteni Down');
insert into Authors (id, Name) values ('8d91d205-0f82-4842-b115-69557e19b06c', 'Rosita Heinonen');
insert into Authors (id, Name) values ('691d1da8-aa95-47db-80e7-0cadcf87a98a', 'Ashby McIlvenny');
insert into Authors (id, Name) values ('87beaa8f-fd7d-4afd-aedf-c6d4e062fa02', 'Marten Venmore');
insert into Authors (id, Name) values ('98323b48-34d4-4d19-9199-b17e131209c9', 'Gilbertine MacNeachtain');
insert into Authors (id, Name) values ('7e326b3c-2a09-48a7-867d-625e54c14485', 'Andriana Puttock');
insert into Authors (id, Name) values ('14cf202e-fcf2-47f1-94c7-3d0369c2cbcf', 'Lucho Deavin');
insert into Authors (id, Name) values ('955df71b-9160-4047-84a2-3a2b20c5898c', 'Evangelin Woodage');
insert into Authors (id, Name) values ('323a7057-04cc-4212-8f52-ee10ead1f0dc', 'Yancy McCaughey');
insert into Authors (id, Name) values ('ee5fe81a-3d94-4906-a58c-a4cb44b7f46d', 'Genna Phippard');
insert into Authors (id, Name) values ('2e0b23fe-f669-45b7-9054-3cee5748edcc', 'Sharline Barmby');
insert into Authors (id, Name) values ('6e032b11-a708-403c-9591-a42eda1d264d', 'Hagan Ells');
insert into Authors (id, Name) values ('73144b49-5e89-4bf8-855b-038392504d28', 'Cindelyn Monnoyer');
insert into Authors (id, Name) values ('4742bb51-7004-47a9-aedd-14d51c632925', 'Daryl Castilla');
insert into Authors (id, Name) values ('e6ccf9c6-f906-47a2-9bd2-2b9ea4c09d31', 'Charmain Hurne');
insert into Authors (id, Name) values ('b89f742e-ca33-4a48-ad69-779a42cad041', 'Kathryne Espinheira');
insert into Authors (id, Name) values ('bdf745be-5ac5-4b4f-aca9-e8436af5c4a9', 'Brenden Ussher');
insert into Authors (id, Name) values ('a1d908a9-b934-48e9-abba-5ac43fef557a', 'Melodie Ebsworth');
insert into Authors (id, Name) values ('ce422495-0b25-4e20-b3a7-de1241874657', 'Shandra Roadknight');
insert into Authors (id, Name) values ('48870308-638c-4e00-9bd6-22f170cfaf08', 'Brad Bang');
insert into Authors (id, Name) values ('7d88e018-8e83-4f49-9bd5-1dfcfa2c3bd2', 'Edeline Dowdam');
insert into Authors (id, Name) values ('7ca37caa-8ef4-4f49-9bf0-dda36354cc30', 'Giffie Grandin');
insert into Authors (id, Name) values ('2801826e-2f78-4fec-af20-4b2c85422172', 'Cara Sikorsky');
insert into Authors (id, Name) values ('f98b793a-5893-4f4a-9cd4-99f4e4b09a64', 'Theda Paradin');
insert into Authors (id, Name) values ('327de5e9-ed46-4514-baa5-e31e3b8fd3dc', 'Christine Mourant');
insert into Authors (id, Name) values ('5fc84934-be5f-4dec-af5b-dd915179947c', 'Margeaux O''Dea');
insert into Authors (id, Name) values ('54120b8f-ee9c-4c52-b449-3ccbe42fcf0b', 'Barnabe Stables');
insert into Authors (id, Name) values ('f151391e-680f-4c92-9729-6f20434e237e', 'Cal Spinage');
insert into Authors (id, Name) values ('338d2e6c-30bd-4794-9cf1-77969c79740c', 'Miriam Rootes');
insert into Authors (id, Name) values ('06475c27-284e-4a39-848c-687a06a7574b', 'Gerhard Brodie');
insert into Authors (id, Name) values ('5d62e687-ff02-4393-b680-9fcca89028ef', 'Mariele Stannas');
insert into Authors (id, Name) values ('2e071a40-b1a0-4880-80ad-f4319fe84a89', 'Alaster Hynde');
insert into Authors (id, Name) values ('f75eaa75-1bdd-43e3-89ec-893eea0ccd2f', 'Phyllis O''Quin');
insert into Authors (id, Name) values ('5f1fa188-2d71-43f8-aa9e-7cdddafeac48', 'Josephine Stallion');
insert into Authors (id, Name) values ('64697abb-2409-48b6-83e3-254c1e7159a9', 'Siffre Bezarra');
insert into Authors (id, Name) values ('2ff52a2f-30fc-4d28-9eac-e08fbb29a557', 'Annabell McDermot');
insert into Authors (id, Name) values ('ce2a37ea-2202-4db4-b048-c17a69b8c976', 'Danya Dalley');
insert into Authors (id, Name) values ('c2a1e45b-6adf-46b4-a00c-039c23ab6361', 'Roxane Boatwright');
insert into Authors (id, Name) values ('6d07f246-ef21-411c-9a9d-fad00735fac9', 'Micheline Hawton');
insert into Authors (id, Name) values ('ca9225c7-034a-4510-b2c9-803bc493d303', 'Vern Skellern');
insert into Authors (id, Name) values ('44ebd890-09f6-47ac-914c-e3c15d02b0c4', 'Isiahi Poulsum');
insert into Authors (id, Name) values ('97aed1e3-1038-4427-9e2a-8db05944babd', 'Gilda Haythorne');
insert into Authors (id, Name) values ('026592d3-dd70-46ff-8dfa-7cfcfd5195d4', 'Marcy Godard');
insert into Authors (id, Name) values ('4e03e78d-955d-4dd4-961a-69efb3a9d71d', 'Raven McLese');
insert into Authors (id, Name) values ('89455bd5-5959-404c-ad6f-5d2e632804ce', 'Pavlov Dumbreck');
insert into Authors (id, Name) values ('5638a2eb-3262-4eca-80c9-79db102a7a29', 'Worth Letcher');
insert into Authors (id, Name) values ('8b4b1f89-673f-4bf9-b9a6-df004a5a4e40', 'Gustavo Bromilow');
insert into Authors (id, Name) values ('c658c43a-8679-443c-9cfe-d48029d06b87', 'Newton de Merida');
insert into Authors (id, Name) values ('d00ba10c-b707-40d4-adea-342f42b3b3f3', 'Dov Souch');