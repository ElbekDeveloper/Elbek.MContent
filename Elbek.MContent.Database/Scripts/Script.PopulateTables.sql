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
-- скрипт заполнения авторов переделать в MERGE (create/update). Загугли MERGE script to fill initial data.
-- можно сделать меньше данных, штук 10 хватит

:r .\PopulateAuthors.sql