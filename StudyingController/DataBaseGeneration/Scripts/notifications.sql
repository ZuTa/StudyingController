﻿USE UniversityDB;
GO
INSERT INTO [Notification]([Message], [UserID], [Date])
SELECT N'Вітаємо в системі!', [SU].[ID], GETDATE() FROM [SystemUser] AS [SU];
GO
INSERT INTO [Notification]([Message], [UserID], [Date])
SELECT N'До нового року залишилось ' + CAST(DATEDIFF(DAY, GETDATE(), '2015-01-01') AS nvarchar) + N' днів.', [SU].[ID], DATEADD(minute, 1, GETDATE()) FROM [SystemUser] AS [SU];
