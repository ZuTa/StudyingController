USE [UniversityDB] 
GO
----------------------------------------------------
--Main administrators

INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'admin2', convert(binary,''),1, N'Іван', N'admin2', N'Адмін', N'admin@admin.com' )
GO
------------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'admin3', convert(binary,''),1, N'Петро', N'admin3', N'Адмін', N'admin@admin.com' )
GO
------------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'admin4', convert(binary,''),1, N'Василь', N'admin4', N'Адмін', N'admin@admin.com' )
GO
-----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'admin5', convert(binary,''),1, N'Олег', N'admin5', N'Адмін', N'admin@admin.com' )
GO
----------------------------------------------------
--Main secretaries

INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'secretary1', convert(binary,''),8, N'Ольга', N'secretary1', N'Секретар', N'secretary@admin.com' )
GO
------------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'secretary2', convert(binary,''),8, N'Олена', N'secretary2', N'Секретар', N'secretary@admin.com' )
GO
------------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'secretary3', convert(binary,''),8, N'Катерина', N'secretary3', N'Секретар', N'secretary@admin.com' )
GO
-----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'secretary4', convert(binary,''),8, N'Анастасія', N'secretary4', N'Секретар', N'secretary@admin.com' )
GO
----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'secretary5', convert(binary,''),8, N'Інна', N'secretary5', N'Секретар', N'secretary@admin.com' )
GO
----------------------------------------------------
--Institute administrators

INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'admininst1', convert(binary,''),2, N'Антон', N'admininst1', N'Адмін', N'admin@admin.com' )
GO
INSERT INTO [InstituteAdmin]
  ([SystemUserID], [InstituteID])
VALUES 
	(11, 1)
GO
----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'admininst2', convert(binary,''),2, N'Андрій', N'admininst2', N'Адмін', N'admin@admin.com' )
GO
INSERT INTO [InstituteAdmin]
  ([SystemUserID], [InstituteID])
VALUES 
	(12, 2)
GO
----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'admininst3', convert(binary,''),2, N'Петро', N'admininst3', N'Адмін', N'admin@admin.com' )
GO
INSERT INTO [InstituteAdmin]
  ([SystemUserID], [InstituteID])
VALUES 
	(13, 3)
GO
------------------------------------------------------
----Institute secretaries

--INSERT INTO [SystemUser]
--  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
--VALUES 
--	( 'secretaryinst1', convert(binary,''),16)
--GO
--INSERT INTO [UserInformation]
--  ([SystemUserID], [FirstName], [LastName], [Email])
--VALUES 
--	(14, N'Вікторія', N'Секретар', 'panas@admin.com')
--GO
--INSERT INTO [InstituteSecretary]
--  ([SystemUserID], [InstituteID])
--VALUES 
--	(14, 1, N'', N'admin', N'Адмін', N'admin@admin.com' )
--GO
------------------------------------------------------
--INSERT INTO [SystemUser]
--  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
--VALUES 
--	( 'secretaryinst2', convert(binary,''),16)
--GO
--INSERT INTO [UserInformation]
--  ([SystemUserID], [FirstName], [LastName], [Email])
--VALUES 
--	(15, N'Алла', N'Секретар', 'panas@admin.com')
--GO
--INSERT INTO [InstituteSecretary]
--  ([SystemUserID], [InstituteID])
--VALUES 
--	(15, 1, N'', N'admin', N'Адмін', N'admin@admin.com' )
--GO
------------------------------------------------------
--INSERT INTO [SystemUser]
--  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
--VALUES 
--	( 'secretaryinst3', convert(binary,''),16)
--GO
--INSERT INTO [UserInformation]
--  ([SystemUserID], [FirstName], [LastName], [Email])
--VALUES 
--	(16, N'Євгенія', N'Секретар', 'panas@admin.com')
--GO
--INSERT INTO [InstituteSecretary]
--  ([SystemUserID], [InstituteID])
--VALUES 
--	(16, 1, N'', N'admin', N'Адмін', N'admin@admin.com' )
--GO
------------------------------------------------------
----Faculty administrators

--INSERT INTO [SystemUser]
--  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
--VALUES 
--	( 'adminfacult1', convert(binary,''),4)
--GO
--INSERT INTO [UserInformation]
--  ([SystemUserID], [FirstName], [LastName], [Email])
--VALUES 
--	(17, N'Максим', N'Адмін', 'panas@admin.com')
--GO
--INSERT INTO [FacultyAdmin]
--  ([SystemUserID], [FacultyID])
--VALUES 
--	(17, 1, N'', N'admin', N'Адмін', N'admin@admin.com' )
--GO
------------------------------------------------------
--INSERT INTO [SystemUser]
--  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
--VALUES 
--	( 'adminfacult2', convert(binary,''),4)
--GO
--INSERT INTO [UserInformation]
--  ([SystemUserID], [FirstName], [LastName], [Email])
--VALUES 
--	(18, N'Артем', N'Адмін', 'panas@admin.com')
--GO
--INSERT INTO [FacultyAdmin]
--  ([SystemUserID], [FacultyID])
--VALUES 
--	(18, 2, N'', N'admininst', N'Адмін', N'admin@admin.com' )
--GO
------------------------------------------------------
--INSERT INTO [SystemUser]
--  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
--VALUES 
--	( 'adminfacult3', convert(binary,''),4)
--GO
--INSERT INTO [UserInformation]
--  ([SystemUserID], [FirstName], [LastName], [Email])
--VALUES 
--	(19, N'Тарас', N'Адмін', 'panas@admin.com')
--GO
--INSERT INTO [FacultyAdmin]
--  ([SystemUserID], [FacultyID])
--VALUES 
--	(19, 3)
--GO
------------------------------------------------------
--INSERT INTO [SystemUser]
--  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
--VALUES 
--	( 'adminfacult4', convert(binary,''),4)
--GO
--INSERT INTO [UserInformation]
--  ([SystemUserID], [FirstName], [LastName], [Email])
--VALUES 
--	(20, N'Юрій', N'Адмін', 'panas@admin.com')
--GO
--INSERT INTO [FacultyAdmin]
--  ([SystemUserID], [FacultyID])
--VALUES 
--	(20, 4)
--GO
------------------------------------------------------
--INSERT INTO [SystemUser]
--  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
--VALUES 
--	( 'adminfacult5', convert(binary,''),4)
--GO
--INSERT INTO [UserInformation]
--  ([SystemUserID], [FirstName], [LastName], [Email])
--VALUES 
--	(21, N'Ярослав', N'Адмін', 'panas@admin.com')
--GO
--INSERT INTO [FacultyAdmin]
--  ([SystemUserID], [FacultyID])
--VALUES 
--	(21, 5)
--GO
------------------------------------------------------
--INSERT INTO [SystemUser]
--  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
--VALUES 
--	( 'adminfacult6', convert(binary,''),4)
--GO
--INSERT INTO [UserInformation]
--  ([SystemUserID], [FirstName], [LastName], [Email])
--VALUES 
--	(22, N'Микола', N'Адмін', 'panas@admin.com')
--GO
--INSERT INTO [FacultyAdmin]
--  ([SystemUserID], [FacultyID])
--VALUES 
--	(22, 6)
--GO
------------------------------------------------------
--INSERT INTO [SystemUser]
--  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
--VALUES 
--	( 'adminfacult7', convert(binary,''),4)
--GO
--INSERT INTO [UserInformation]
--  ([SystemUserID], [FirstName], [LastName], [Email])
--VALUES 
--	(23, N'Роман', N'Адмін', 'panas@admin.com')
--GO
--INSERT INTO [FacultyAdmin]
--  ([SystemUserID], [FacultyID])
--VALUES 
--	(23, 7)
--GO
------------------------------------------------------
--INSERT INTO [SystemUser]
--  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
--VALUES 
--	( 'adminfacult8', convert(binary,''),4)
--GO
--INSERT INTO [UserInformation]
--  ([SystemUserID], [FirstName], [LastName], [Email])
--VALUES 
--	(24, N'Олександр', N'Адмін', 'panas@admin.com')
--GO
--INSERT INTO [FacultyAdmin]
--  ([SystemUserID], [FacultyID])
--VALUES 
--	(24, 8, N'', N'secretary', N'Секретар', N'secretary@admin.com' )
--GO
------------------------------------------------------
--INSERT INTO [SystemUser]
--  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
--VALUES 
--	( 'adminfacult9', convert(binary,''),4)
--GO
--INSERT INTO [UserInformation]
--  ([SystemUserID], [FirstName], [LastName], [Email])
--VALUES 
--	(25, N'Валентин', N'Адмін', 'panas@admin.com')
--GO
--INSERT INTO [FacultyAdmin]
--  ([SystemUserID], [FacultyID])
--VALUES 
--	(25, 9)
--GO
------------------------------------------------------
--INSERT INTO [SystemUser]
--  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
--VALUES 
--	( 'adminfacult10', convert(binary,''),4)
--GO
--INSERT INTO [UserInformation]
--  ([SystemUserID], [FirstName], [LastName], [Email])
--VALUES 
--	(26, N'Валерій', N'Адмін', 'panas@admin.com')
--GO
--INSERT INTO [FacultyAdmin]
--  ([SystemUserID], [FacultyID])
--VALUES 
--	(26, 10)
--GO
------------------------------------------------------
--INSERT INTO [SystemUser]
--  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
--VALUES 
--	( 'adminfacult11', convert(binary,''),4)
--GO
--INSERT INTO [UserInformation]
--  ([SystemUserID], [FirstName], [LastName], [Email])
--VALUES 
--	(27, N'Віктор', N'Адмін', 'panas@admin.com')
--GO
--INSERT INTO [FacultyAdmin]
--  ([SystemUserID], [FacultyID])
--VALUES 
--	(27, 11, N'', N'admin', N'Адмін', N'admin@admin.com' )
--GO
------------------------------------------------------
--INSERT INTO [SystemUser]
--  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
--VALUES 
--	( 'adminfacult12', convert(binary,''),4)
--GO
--INSERT INTO [UserInformation]
--  ([SystemUserID], [FirstName], [LastName], [Email])
--VALUES 
--	(28, N'Вірослав', N'Адмін', 'panas@admin.com')
--GO
--INSERT INTO [FacultyAdmin]
--  ([SystemUserID], [FacultyID])
--VALUES 
--	(28, 12, N'', N'admininst', N'Адмін', N'admin@admin.com' )
--GO
------------------------------------------------------
--INSERT INTO [SystemUser]
--  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
--VALUES 
--	( 'adminfacult13', convert(binary,''),4)
--GO
--INSERT INTO [UserInformation]
--  ([SystemUserID], [FirstName], [LastName], [Email])
--VALUES 
--	(29, N'Віталій', N'Адмін', 'panas@admin.com')
--GO
--INSERT INTO [FacultyAdmin]
--  ([SystemUserID], [FacultyID])
--VALUES 
--	(29, 13)
--GO
------------------------------------------------------
--INSERT INTO [SystemUser]
--  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
--VALUES 
--	( 'adminfacult14', convert(binary,''),4)
--GO
--INSERT INTO [UserInformation]
--  ([SystemUserID], [FirstName], [LastName], [Email])
--VALUES 
--	(30, N'Влад', N'Адмін', 'panas@admin.com')
--GO
--INSERT INTO [FacultyAdmin]
--  ([SystemUserID], [FacultyID])
--VALUES 
--	(30, 14)
--GO
------------------------------------------------------
----Faculty secretaries

--INSERT INTO [SystemUser]
--  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
--VALUES 
--	( 'secretaryfacult1', convert(binary,''),32, N'', N'admininst', N'Адмін', N'admin@admin.com' )
--GO
--INSERT INTO [UserInformation]
--  ([SystemUserID], [FirstName], [LastName], [Email])
--VALUES 
--	(31, N'Аврора', N'Секретар', 'panas@admin.com')
--GO
--INSERT INTO [FacultySecretary]
--  ([SystemUserID], [FacultyID])
--VALUES 
--	(31, 1, N'', N'admin', N'Адмін', N'admin@admin.com' )
--GO
------------------------------------------------------
--INSERT INTO [SystemUser]
--  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
--VALUES 
--	( 'secretaryfacult2', convert(binary,''),32, N'', N'admininst', N'Адмін', N'admin@admin.com' )
--GO
--INSERT INTO [UserInformation]
--  ([SystemUserID], [FirstName], [LastName], [Email])
--VALUES 
--	(32, N'Агата', N'Секретар', 'panas@admin.com')
--GO
--INSERT INTO [FacultySecretary]
--  ([SystemUserID], [FacultyID])
--VALUES 
--	(32, 2, N'', N'admininst', N'Адмін', N'admin@admin.com' )
--GO
------------------------------------------------------
--INSERT INTO [SystemUser]
--  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
--VALUES 
--	( 'secretaryfacult3', convert(binary,''),32, N'', N'admininst', N'Адмін', N'admin@admin.com' )
--GO
--INSERT INTO [UserInformation]
--  ([SystemUserID], [FirstName], [LastName], [Email])
--VALUES 
--	(33, N'Ада', N'Секретар', 'panas@admin.com')
--GO
--INSERT INTO [FacultySecretary]
--  ([SystemUserID], [FacultyID])
--VALUES 
--	(33, 3)
--GO
------------------------------------------------------
--INSERT INTO [SystemUser]
--  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
--VALUES 
--	( 'secretaryfacult4', convert(binary,''),32, N'', N'admininst', N'Адмін', N'admin@admin.com' )
--GO
--INSERT INTO [UserInformation]
--  ([SystemUserID], [FirstName], [LastName], [Email])
--VALUES 
--	(34, N'Алевтина', N'Секретар', 'panas@admin.com')
--GO
--INSERT INTO [FacultySecretary]
--  ([SystemUserID], [FacultyID])
--VALUES 
--	(34, 4)
--GO
------------------------------------------------------
--INSERT INTO [SystemUser]
--  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
--VALUES 
--	( 'secretaryfacult5', convert(binary,''),32, N'', N'admininst', N'Адмін', N'admin@admin.com' )
--GO
--INSERT INTO [UserInformation]
--  ([SystemUserID], [FirstName], [LastName], [Email])
--VALUES 
--	(35, N'Аліна', N'Секретар', 'panas@admin.com')
--GO
--INSERT INTO [FacultySecretary]
--  ([SystemUserID], [FacultyID])
--VALUES 
--	(35, 5)
--GO
------------------------------------------------------
--INSERT INTO [SystemUser]
--  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
--VALUES 
--	( 'secretaryfacult6', convert(binary,''),32, N'', N'admininst', N'Адмін', N'admin@admin.com' )
--GO
--INSERT INTO [UserInformation]
--  ([SystemUserID], [FirstName], [LastName], [Email])
--VALUES 
--	(36, N'Аліса', N'Секретар', 'panas@admin.com')
--GO
--INSERT INTO [FacultySecretary]
--  ([SystemUserID], [FacultyID])
--VALUES 
--	(36, 6)
--GO
------------------------------------------------------
--INSERT INTO [SystemUser]
--  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
--VALUES 
--	( 'secretaryfacult7', convert(binary,''),32, N'', N'admininst', N'Адмін', N'admin@admin.com' )
--GO
--INSERT INTO [UserInformation]
--  ([SystemUserID], [FirstName], [LastName], [Email])
--VALUES 
--	(37, N'Альбіна', N'Секретар', 'panas@admin.com')
--GO
--INSERT INTO [FacultySecretary]
--  ([SystemUserID], [FacultyID])
--VALUES 
--	(37, 7)
--GO
------------------------------------------------------
--INSERT INTO [SystemUser]
--  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
--VALUES 
--	( 'secretaryfacult8', convert(binary,''),32, N'', N'admininst', N'Адмін', N'admin@admin.com' )
--GO
--INSERT INTO [UserInformation]
--  ([SystemUserID], [FirstName], [LastName], [Email])
--VALUES 
--	(38, N'Анатолія', N'Секретар', 'panas@admin.com')
--GO
--INSERT INTO [FacultySecretary]
--  ([SystemUserID], [FacultyID])
--VALUES 
--	(38, 8, N'', N'secretary', N'Секретар', N'secretary@admin.com' )
--GO
------------------------------------------------------
--INSERT INTO [SystemUser]
--  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
--VALUES 
--	( 'secretaryfacult9', convert(binary,''),32, N'', N'admininst', N'Адмін', N'admin@admin.com' )
--GO
--INSERT INTO [UserInformation]
--  ([SystemUserID], [FirstName], [LastName], [Email])
--VALUES 
--	(39, N'Ангеліна', N'Секретар', 'panas@admin.com')
--GO
--INSERT INTO [FacultySecretary]
--  ([SystemUserID], [FacultyID])
--VALUES 
--	(39, 9)
--GO
------------------------------------------------------
--INSERT INTO [SystemUser]
--  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
--VALUES 
--	( 'secretaryfacult10', convert(binary,''),32, N'', N'admininst', N'Адмін', N'admin@admin.com' )
--GO
--INSERT INTO [UserInformation]
--  ([SystemUserID], [FirstName], [LastName], [Email])
--VALUES 
--	(40, N'Анжела', N'Секретар', 'panas@admin.com')
--GO
--INSERT INTO [FacultySecretary]
--  ([SystemUserID], [FacultyID])
--VALUES 
--	(40, 10)
--GO
------------------------------------------------------
--INSERT INTO [SystemUser]
--  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
--VALUES 
--	( 'secretaryfacult11', convert(binary,''),32, N'', N'admininst', N'Адмін', N'admin@admin.com' )
--GO
--INSERT INTO [UserInformation]
--  ([SystemUserID], [FirstName], [LastName], [Email])
--VALUES 
--	(41, N'Антоніна', N'Секретар', 'panas@admin.com')
--GO
--INSERT INTO [FacultySecretary]
--  ([SystemUserID], [FacultyID])
--VALUES 
--	(41, 11, N'', N'admin', N'Адмін', N'admin@admin.com' )
--GO
------------------------------------------------------
--INSERT INTO [SystemUser]
--  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
--VALUES 
--	( 'secretaryfacult12', convert(binary,''),32, N'', N'admininst', N'Адмін', N'admin@admin.com' )
--GO
--INSERT INTO [UserInformation]
--  ([SystemUserID], [FirstName], [LastName], [Email])
--VALUES 
--	(42, N'Анфіса', N'Секретар', 'panas@admin.com')
--GO
--INSERT INTO [FacultySecretary]
--  ([SystemUserID], [FacultyID])
--VALUES 
--	(42, 12, N'', N'admininst', N'Адмін', N'admin@admin.com' )
--GO
------------------------------------------------------
--INSERT INTO [SystemUser]
--  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
--VALUES 
--	( 'secretaryfacult13', convert(binary,''),32, N'', N'admininst', N'Адмін', N'admin@admin.com' )
--GO
--INSERT INTO [UserInformation]
--  ([SystemUserID], [FirstName], [LastName], [Email])
--VALUES 
--	(43, N'Аполлонія', N'Секретар', 'panas@admin.com')
--GO
--INSERT INTO [FacultySecretary]
--  ([SystemUserID], [FacultyID])
--VALUES 
--	(43, 13)
--GO
------------------------------------------------------
--INSERT INTO [SystemUser]
--  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
--VALUES 
--	( 'secretaryfacult14', convert(binary,''),32, N'', N'admininst', N'Адмін', N'admin@admin.com' )
--GO
--INSERT INTO [UserInformation]
--  ([SystemUserID], [FirstName], [LastName], [Email])
--VALUES 
--	(44, N'Афіна', N'Секретар', 'panas@admin.com')
--GO
--INSERT INTO [FacultySecretary]
--  ([SystemUserID], [FacultyID])
--VALUES 
--	(44, 14)
--GO
------------------------------------------------------
--Teachers

INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher1', convert(binary,''), 64, N'Віктор', N'teacher1', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(14, 1)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher2', convert(binary,''), 64, N'Володимир', N'teacher2', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(15, 1)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher3', convert(binary,''), 64, N'Степан', N'teacher3', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(16, 1)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher4', convert(binary,''), 64, N'Милослава', N'teacher4', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(17, 1)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher5', convert(binary,''), 64, N'Назарій', N'teacher5', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(18, 1)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher6', convert(binary,''), 64, N'Олесь', N'teacher6', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(19, 2)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher7', convert(binary,''), 64, N'Софія', N'teacher7', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(20, 2)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher8', convert(binary,''), 64, N'Катерина', N'teacher8', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(21, 2)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher9', convert(binary,''), 64, N'Варвара', N'teacher9', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(22, 2)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher10', convert(binary,''), 64, N'Маркіян', N'teacher10', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(23, 2)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher11', convert(binary,''), 64, N'Роман', N'teacher11', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(24, 3)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher12', convert(binary,''), 64, N'Владислав', N'teacher12', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(25, 3)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher13', convert(binary,''), 64, N'Тамара', N'teacher13', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(26, 3)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher14', convert(binary,''), 64, N'Олена', N'teacher14', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(27, 3)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher15', convert(binary,''), 64, N'Любомир', N'teacher15', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(28, 3)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher16', convert(binary,''), 64, N'Милослава', N'teacher16', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(29, 4)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher17', convert(binary,''), 64, N'Мирон', N'teacher17', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(30, 4)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher18', convert(binary,''), 64, N'Потап', N'teacher18', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(31, 4)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher19', convert(binary,''), 64, N'Сергій', N'teacher19', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(32, 4)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher20', convert(binary,''), 64, N'Орест', N'teacher20', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(33, 4)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher21', convert(binary,''), 64, N'Орест', N'teacher21', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(34, 5)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher22', convert(binary,''), 64, N'Галина', N'teacher22', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(35, 5)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher23', convert(binary,''), 64, N'Софія', N'teacher23', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(36, 5)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher24', convert(binary,''), 64, N'Владислава', N'teacher24', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(37, 5)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher25', convert(binary,''), 64, N'Анжела', N'teacher25', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(38, 5)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher26', convert(binary,''), 64, N'Уляна', N'teacher26', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(39, 6)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher27', convert(binary,''), 64, N'Валентина', N'teacher27', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(40, 6)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher28', convert(binary,''), 64, N'Варвара', N'teacher28', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(41, 6)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher29', convert(binary,''), 64, N'Зінаїда', N'teacher29', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(42, 6)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher30', convert(binary,''), 64, N'Святослав', N'teacher30', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(43, 6)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher31', convert(binary,''), 64, N'Віра', N'teacher31', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(44, 7)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher32', convert(binary,''), 64, N'Надія', N'teacher32', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(45, 7)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher33', convert(binary,''), 64, N'Вікторія', N'teacher33', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(46, 7)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher34', convert(binary,''), 64, N'Аліса', N'teacher34', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(47, 7)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher35', convert(binary,''), 64, N'Милослава', N'teacher35', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(48, 7)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher36', convert(binary,''), 64, N'Віолетта', N'teacher36', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(49, 8)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher37', convert(binary,''), 64, N'Наталія', N'teacher37', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(50, 8)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher38', convert(binary,''), 64, N'Петро', N'teacher38', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(51, 8)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher39', convert(binary,''), 64, N'Віталіна', N'teacher39', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(52, 8)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher40', convert(binary,''), 64, N'Микола', N'teacher40', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(53, 8)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher41', convert(binary,''), 64, N'Ігор', N'teacher41', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(54, 9)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher42', convert(binary,''), 64, N'Валентин', N'teacher42', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(55, 9)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher43', convert(binary,''), 64, N'Валентин', N'teacher43', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(56, 9)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher44', convert(binary,''), 64, N'Ярема', N'teacher44', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(57, 9)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher45', convert(binary,''), 64, N'Любов', N'teacher45', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(58, 9)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher46', convert(binary,''), 64, N'Владислав', N'teacher46', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(59, 10)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher47', convert(binary,''), 64, N'Георгій', N'teacher47', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(60, 10)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher48', convert(binary,''), 64, N'Лукян', N'teacher48', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(61, 10)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher49', convert(binary,''), 64, N'Зінаїда', N'teacher49', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(62, 10)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher50', convert(binary,''), 64, N'Микола', N'teacher50', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(63, 10)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher51', convert(binary,''), 64, N'Жанна', N'teacher51', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(64, 11)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher52', convert(binary,''), 64, N'Надія', N'teacher52', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(65, 11)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher53', convert(binary,''), 64, N'Віктор', N'teacher53', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(66, 11)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher54', convert(binary,''), 64, N'Ольга', N'teacher54', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(67, 11)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher55', convert(binary,''), 64, N'Лукян', N'teacher55', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(68, 11)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher56', convert(binary,''), 64, N'Микита', N'teacher56', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(69, 12)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher57', convert(binary,''), 64, N'Георгій', N'teacher57', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(70, 12)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher58', convert(binary,''), 64, N'Іванна', N'teacher58', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(71, 12)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher59', convert(binary,''), 64, N'Лев', N'teacher59', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(72, 12)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher60', convert(binary,''), 64, N'Олег', N'teacher60', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(73, 12)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher61', convert(binary,''), 64, N'Зоя', N'teacher61', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(74, 13)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher62', convert(binary,''), 64, N'Микола', N'teacher62', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(75, 13)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher63', convert(binary,''), 64, N'Віталій', N'teacher63', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(76, 13)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher64', convert(binary,''), 64, N'Денис', N'teacher64', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(77, 13)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher65', convert(binary,''), 64, N'Богдана', N'teacher65', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(78, 13)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher66', convert(binary,''), 64, N'Аліса', N'teacher66', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(79, 14)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher67', convert(binary,''), 64, N'Петро', N'teacher67', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(80, 14)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher68', convert(binary,''), 64, N'Олесь', N'teacher68', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(81, 14)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher69', convert(binary,''), 64, N'Вячеслав', N'teacher69', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(82, 14)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher70', convert(binary,''), 64, N'Анжела', N'teacher70', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(83, 14)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher71', convert(binary,''), 64, N'Світлана', N'teacher71', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(84, 15)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher72', convert(binary,''), 64, N'Анастасія', N'teacher72', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(85, 15)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher73', convert(binary,''), 64, N'Зінаїда', N'teacher73', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(86, 15)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher74', convert(binary,''), 64, N'Жадан', N'teacher74', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(87, 15)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher75', convert(binary,''), 64, N'Захар', N'teacher75', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(88, 15)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher76', convert(binary,''), 64, N'Олексій', N'teacher76', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(89, 16)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher77', convert(binary,''), 64, N'Влада', N'teacher77', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(90, 16)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher78', convert(binary,''), 64, N'Орест', N'teacher78', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(91, 16)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher79', convert(binary,''), 64, N'Милослава', N'teacher79', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(92, 16)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher80', convert(binary,''), 64, N'Сергій', N'teacher80', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(93, 16)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher81', convert(binary,''), 64, N'Милослава', N'teacher81', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(94, 17)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher82', convert(binary,''), 64, N'Михайло', N'teacher82', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(95, 17)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher83', convert(binary,''), 64, N'Милослава', N'teacher83', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(96, 17)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher84', convert(binary,''), 64, N'Петро', N'teacher84', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(97, 17)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher85', convert(binary,''), 64, N'Дана', N'teacher85', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(98, 17)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher86', convert(binary,''), 64, N'Зоя', N'teacher86', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(99, 18)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher87', convert(binary,''), 64, N'Леся', N'teacher87', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(100, 18)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher88', convert(binary,''), 64, N'Влада', N'teacher88', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(101, 18)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher89', convert(binary,''), 64, N'Максим', N'teacher89', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(102, 18)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher90', convert(binary,''), 64, N'Ян', N'teacher90', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(103, 18)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher91', convert(binary,''), 64, N'Ярослав', N'teacher91', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(104, 19)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher92', convert(binary,''), 64, N'Георгій', N'teacher92', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(105, 19)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher93', convert(binary,''), 64, N'Жадан', N'teacher93', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(106, 19)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher94', convert(binary,''), 64, N'Микита', N'teacher94', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(107, 19)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher95', convert(binary,''), 64, N'Ян', N'teacher95', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(108, 19)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher96', convert(binary,''), 64, N'Наталія', N'teacher96', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(109, 20)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher97', convert(binary,''), 64, N'Софія', N'teacher97', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(110, 20)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher98', convert(binary,''), 64, N'Милослава', N'teacher98', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(111, 20)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher99', convert(binary,''), 64, N'Олеся', N'teacher99', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(112, 20)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher100', convert(binary,''), 64, N'Левко', N'teacher100', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(113, 20)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher101', convert(binary,''), 64, N'Вікторія', N'teacher101', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(114, 21)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher102', convert(binary,''), 64, N'Владислава', N'teacher102', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(115, 21)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher103', convert(binary,''), 64, N'Зіновій', N'teacher103', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(116, 21)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher104', convert(binary,''), 64, N'Дмитро', N'teacher104', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(117, 21)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher105', convert(binary,''), 64, N'Назар', N'teacher105', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(118, 21)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher106', convert(binary,''), 64, N'Марта', N'teacher106', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(119, 22)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher107', convert(binary,''), 64, N'Георгій', N'teacher107', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(120, 22)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher108', convert(binary,''), 64, N'Милослава', N'teacher108', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(121, 22)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher109', convert(binary,''), 64, N'Жанна', N'teacher109', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(122, 22)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher110', convert(binary,''), 64, N'Маряна', N'teacher110', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(123, 22)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher111', convert(binary,''), 64, N'Богдана', N'teacher111', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(124, 23)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher112', convert(binary,''), 64, N'Влада', N'teacher112', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(125, 23)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher113', convert(binary,''), 64, N'Оксана', N'teacher113', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(126, 23)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher114', convert(binary,''), 64, N'Дарина', N'teacher114', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(127, 23)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher115', convert(binary,''), 64, N'Сергій', N'teacher115', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(128, 23)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher116', convert(binary,''), 64, N'Остап', N'teacher116', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(129, 24)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher117', convert(binary,''), 64, N'Пилип', N'teacher117', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(130, 24)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher118', convert(binary,''), 64, N'Анжела', N'teacher118', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(131, 24)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher119', convert(binary,''), 64, N'Орест', N'teacher119', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(132, 24)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher120', convert(binary,''), 64, N'Ксенія', N'teacher120', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(133, 24)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher121', convert(binary,''), 64, N'Дана', N'teacher121', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(134, 25)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher122', convert(binary,''), 64, N'Анна', N'teacher122', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(135, 25)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher123', convert(binary,''), 64, N'Владислав', N'teacher123', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(136, 25)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher124', convert(binary,''), 64, N'Степан', N'teacher124', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(137, 25)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher125', convert(binary,''), 64, N'Віктор', N'teacher125', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(138, 25)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher126', convert(binary,''), 64, N'Любов', N'teacher126', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(139, 26)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher127', convert(binary,''), 64, N'Владислава', N'teacher127', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(140, 26)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher128', convert(binary,''), 64, N'Пилип', N'teacher128', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(141, 26)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher129', convert(binary,''), 64, N'Валентина', N'teacher129', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(142, 26)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher130', convert(binary,''), 64, N'Любомир', N'teacher130', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(143, 26)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher131', convert(binary,''), 64, N'Павло', N'teacher131', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(144, 27)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher132', convert(binary,''), 64, N'Леся', N'teacher132', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(145, 27)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher133', convert(binary,''), 64, N'Ростислав', N'teacher133', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(146, 27)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher134', convert(binary,''), 64, N'Анжела', N'teacher134', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(147, 27)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher135', convert(binary,''), 64, N'Любов', N'teacher135', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(148, 27)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher136', convert(binary,''), 64, N'Дмитро', N'teacher136', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(149, 28)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher137', convert(binary,''), 64, N'Данило', N'teacher137', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(150, 28)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher138', convert(binary,''), 64, N'Владислава', N'teacher138', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(151, 28)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher139', convert(binary,''), 64, N'Володимир', N'teacher139', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(152, 28)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher140', convert(binary,''), 64, N'Милослава', N'teacher140', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(153, 28)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher141', convert(binary,''), 64, N'Вікторія', N'teacher141', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(154, 29)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher142', convert(binary,''), 64, N'Світлана', N'teacher142', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(155, 29)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher143', convert(binary,''), 64, N'Ольга', N'teacher143', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(156, 29)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher144', convert(binary,''), 64, N'Остап', N'teacher144', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(157, 29)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher145', convert(binary,''), 64, N'Олена', N'teacher145', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(158, 29)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher146', convert(binary,''), 64, N'Людмила', N'teacher146', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(159, 30)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher147', convert(binary,''), 64, N'Зеновій', N'teacher147', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(160, 30)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher148', convert(binary,''), 64, N'Павло', N'teacher148', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(161, 30)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher149', convert(binary,''), 64, N'Ірина', N'teacher149', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(162, 30)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher150', convert(binary,''), 64, N'Герасим', N'teacher150', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(163, 30)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher151', convert(binary,''), 64, N'Пилип', N'teacher151', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(164, 31)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher152', convert(binary,''), 64, N'Михайло', N'teacher152', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(165, 31)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher153', convert(binary,''), 64, N'Потап', N'teacher153', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(166, 31)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher154', convert(binary,''), 64, N'Генадій', N'teacher154', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(167, 31)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher155', convert(binary,''), 64, N'Тарас', N'teacher155', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(168, 31)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher156', convert(binary,''), 64, N'Олена', N'teacher156', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(169, 32)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher157', convert(binary,''), 64, N'Анжела', N'teacher157', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(170, 32)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher158', convert(binary,''), 64, N'Віта', N'teacher158', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(171, 32)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher159', convert(binary,''), 64, N'Георгій', N'teacher159', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(172, 32)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher160', convert(binary,''), 64, N'Степан', N'teacher160', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(173, 32)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher161', convert(binary,''), 64, N'Данило', N'teacher161', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(174, 33)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher162', convert(binary,''), 64, N'Віра', N'teacher162', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(175, 33)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher163', convert(binary,''), 64, N'Кузьма', N'teacher163', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(176, 33)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher164', convert(binary,''), 64, N'Роман', N'teacher164', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(177, 33)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher165', convert(binary,''), 64, N'Петро', N'teacher165', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(178, 33)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher166', convert(binary,''), 64, N'Зоряна', N'teacher166', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(179, 34)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher167', convert(binary,''), 64, N'Ігор', N'teacher167', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(180, 34)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher168', convert(binary,''), 64, N'Марян', N'teacher168', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(181, 34)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher169', convert(binary,''), 64, N'Наталія', N'teacher169', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(182, 34)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher170', convert(binary,''), 64, N'Мирон', N'teacher170', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(183, 34)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher171', convert(binary,''), 64, N'Любомир', N'teacher171', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(184, 35)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher172', convert(binary,''), 64, N'Дана', N'teacher172', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(185, 35)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher173', convert(binary,''), 64, N'Тарас', N'teacher173', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(186, 35)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher174', convert(binary,''), 64, N'Юрій', N'teacher174', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(187, 35)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher175', convert(binary,''), 64, N'Дмитро', N'teacher175', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(188, 35)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher176', convert(binary,''), 64, N'Зоряна', N'teacher176', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(189, 36)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher177', convert(binary,''), 64, N'Зоя', N'teacher177', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(190, 36)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher178', convert(binary,''), 64, N'Віра', N'teacher178', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(191, 36)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher179', convert(binary,''), 64, N'Аліна', N'teacher179', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(192, 36)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher180', convert(binary,''), 64, N'Захар', N'teacher180', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(193, 36)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher181', convert(binary,''), 64, N'Вероніка', N'teacher181', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(194, 37)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher182', convert(binary,''), 64, N'Любомир', N'teacher182', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(195, 37)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher183', convert(binary,''), 64, N'Валерія', N'teacher183', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(196, 37)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher184', convert(binary,''), 64, N'Сергій', N'teacher184', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(197, 37)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher185', convert(binary,''), 64, N'Леся', N'teacher185', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(198, 37)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher186', convert(binary,''), 64, N'Всеволод', N'teacher186', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(199, 38)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher187', convert(binary,''), 64, N'Олег', N'teacher187', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(200, 38)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher188', convert(binary,''), 64, N'Назар', N'teacher188', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(201, 38)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher189', convert(binary,''), 64, N'Володимир', N'teacher189', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(202, 38)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher190', convert(binary,''), 64, N'Наталія', N'teacher190', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(203, 38)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher191', convert(binary,''), 64, N'Ярема', N'teacher191', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(204, 39)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher192', convert(binary,''), 64, N'Пилип', N'teacher192', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(205, 39)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher193', convert(binary,''), 64, N'Анастасія', N'teacher193', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(206, 39)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher194', convert(binary,''), 64, N'Аліна', N'teacher194', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(207, 39)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher195', convert(binary,''), 64, N'Галина', N'teacher195', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(208, 39)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher196', convert(binary,''), 64, N'Уляна', N'teacher196', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(209, 40)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher197', convert(binary,''), 64, N'Владислава', N'teacher197', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(210, 40)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher198', convert(binary,''), 64, N'Ростислав', N'teacher198', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(211, 40)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher199', convert(binary,''), 64, N'Тамара', N'teacher199', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(212, 40)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher200', convert(binary,''), 64, N'Денис', N'teacher200', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(213, 40)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher201', convert(binary,''), 64, N'Валентина', N'teacher201', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(214, 41)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher202', convert(binary,''), 64, N'Маряна', N'teacher202', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(215, 41)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher203', convert(binary,''), 64, N'Тарас', N'teacher203', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(216, 41)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher204', convert(binary,''), 64, N'Ростислав', N'teacher204', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(217, 41)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher205', convert(binary,''), 64, N'Любов', N'teacher205', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(218, 41)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher206', convert(binary,''), 64, N'Владислава', N'teacher206', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(219, 42)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher207', convert(binary,''), 64, N'Ксенія', N'teacher207', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(220, 42)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher208', convert(binary,''), 64, N'Ніна', N'teacher208', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(221, 42)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher209', convert(binary,''), 64, N'Ольга', N'teacher209', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(222, 42)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher210', convert(binary,''), 64, N'Дана', N'teacher210', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(223, 42)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher211', convert(binary,''), 64, N'Денис', N'teacher211', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(224, 43)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher212', convert(binary,''), 64, N'Олеся', N'teacher212', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(225, 43)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher213', convert(binary,''), 64, N'Георгій', N'teacher213', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(226, 43)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher214', convert(binary,''), 64, N'Оксана', N'teacher214', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(227, 43)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher215', convert(binary,''), 64, N'Людмила', N'teacher215', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(228, 43)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher216', convert(binary,''), 64, N'Марта', N'teacher216', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(229, 44)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher217', convert(binary,''), 64, N'Василь', N'teacher217', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(230, 44)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher218', convert(binary,''), 64, N'Дарина', N'teacher218', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(231, 44)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher219', convert(binary,''), 64, N'Валерія', N'teacher219', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(232, 44)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher220', convert(binary,''), 64, N'Назар', N'teacher220', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(233, 44)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher221', convert(binary,''), 64, N'Олексій', N'teacher221', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(234, 45)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher222', convert(binary,''), 64, N'Милослава', N'teacher222', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(235, 45)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher223', convert(binary,''), 64, N'Ігор', N'teacher223', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(236, 45)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher224', convert(binary,''), 64, N'Богдан', N'teacher224', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(237, 45)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher225', convert(binary,''), 64, N'Зоя', N'teacher225', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(238, 45)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher226', convert(binary,''), 64, N'Ксенія', N'teacher226', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(239, 46)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher227', convert(binary,''), 64, N'Данило', N'teacher227', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(240, 46)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher228', convert(binary,''), 64, N'Ольга', N'teacher228', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(241, 46)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher229', convert(binary,''), 64, N'Ірина', N'teacher229', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(242, 46)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher230', convert(binary,''), 64, N'Євген', N'teacher230', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(243, 46)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher231', convert(binary,''), 64, N'Вячеслав', N'teacher231', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(244, 47)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher232', convert(binary,''), 64, N'Марян', N'teacher232', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(245, 47)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher233', convert(binary,''), 64, N'Віта', N'teacher233', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(246, 47)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher234', convert(binary,''), 64, N'Остап', N'teacher234', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(247, 47)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher235', convert(binary,''), 64, N'Ярослав', N'teacher235', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(248, 47)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher236', convert(binary,''), 64, N'Зоряна', N'teacher236', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(249, 48)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher237', convert(binary,''), 64, N'Жанна', N'teacher237', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(250, 48)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher238', convert(binary,''), 64, N'Денис', N'teacher238', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(251, 48)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher239', convert(binary,''), 64, N'Вікторія', N'teacher239', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(252, 48)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher240', convert(binary,''), 64, N'Герасим', N'teacher240', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(253, 48)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher241', convert(binary,''), 64, N'Іван', N'teacher241', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(254, 49)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher242', convert(binary,''), 64, N'Потап', N'teacher242', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(255, 49)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher243', convert(binary,''), 64, N'Орест', N'teacher243', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(256, 49)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher244', convert(binary,''), 64, N'Мирон', N'teacher244', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(257, 49)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher245', convert(binary,''), 64, N'Жанна', N'teacher245', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(258, 49)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher246', convert(binary,''), 64, N'Богдана', N'teacher246', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(259, 50)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher247', convert(binary,''), 64, N'Мирон', N'teacher247', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(260, 50)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher248', convert(binary,''), 64, N'Вікторія', N'teacher248', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(261, 50)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher249', convert(binary,''), 64, N'Владислава', N'teacher249', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(262, 50)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher250', convert(binary,''), 64, N'Людмила', N'teacher250', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(263, 50)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher251', convert(binary,''), 64, N'Ольга', N'teacher251', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(264, 51)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher252', convert(binary,''), 64, N'Світлана', N'teacher252', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(265, 51)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher253', convert(binary,''), 64, N'Леся', N'teacher253', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(266, 51)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher254', convert(binary,''), 64, N'Богдана', N'teacher254', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(267, 51)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher255', convert(binary,''), 64, N'Жанна', N'teacher255', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(268, 51)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher256', convert(binary,''), 64, N'Жанна', N'teacher256', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(269, 52)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher257', convert(binary,''), 64, N'Вероніка', N'teacher257', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(270, 52)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher258', convert(binary,''), 64, N'Тарас', N'teacher258', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(271, 52)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher259', convert(binary,''), 64, N'Віталіна', N'teacher259', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(272, 52)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole], [FirstName], [MiddleName], [LastName], [Email])
VALUES 
	( 'teacher260', convert(binary,''), 64, N'Борис', N'teacher260', N'Викладач', N'teacher@tech.ua')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(273, 52)
GO
---------------------------------------------------
