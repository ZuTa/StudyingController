USE [UniversityDB] 
GO
----------------------------------------------------
--Main administrators

INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'admin2', convert(binary,''),1)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(2, 'Іван', 'Адмін', 'panas@admin.com')
GO
------------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'admin3', convert(binary,''),1)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(3, 'Петро', 'Адмін', 'panas@admin.com')
GO
------------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'admin4', convert(binary,''),1)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(4, 'Василь', 'Адмін', 'panas@admin.com')
GO
-----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'admin5', convert(binary,''),1)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(5, 'Олег', 'Адмін', 'panas@admin.com')
GO
----------------------------------------------------
--Main secretaries

INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'secretary1', convert(binary,''),8)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(6, 'Ольга', 'Секретар', 'panas@admin.com')
GO
------------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'secretary2', convert(binary,''),8)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(7, 'Олена', 'Секретар', 'panas@admin.com')
GO
------------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'secretary3', convert(binary,''),8)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(8, 'Катерина', 'Секретар', 'panas@admin.com')
GO
-----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'secretary4', convert(binary,''),8)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(9, 'Анастасія', 'Секретар', 'panas@admin.com')
GO
----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'secretary5', convert(binary,''),8)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(10, 'Інна', 'Секретар', 'panas@admin.com')
GO
----------------------------------------------------
--Institute administrators

INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'admininst1', convert(binary,''),2)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(11, 'Антон', 'Адмін', 'panas@admin.com')
GO
INSERT INTO [InstituteAdmin]
  ([SystemUserID], [InstituteID])
VALUES 
	(11, 1)
GO
----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'admininst2', convert(binary,''),2)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(12, 'Андрій', 'Адмін', 'panas@admin.com')
GO
INSERT INTO [InstituteAdmin]
  ([SystemUserID], [InstituteID])
VALUES 
	(12, 2)
GO
----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'admininst3', convert(binary,''),2)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(13, 'Педро', 'Адмін', 'panas@admin.com')
GO
INSERT INTO [InstituteAdmin]
  ([SystemUserID], [InstituteID])
VALUES 
	(13, 3)
GO
----------------------------------------------------
--Institute secretaries

INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'secretaryinst1', convert(binary,''),16)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(14, 'Вікторія', 'Секретар', 'panas@admin.com')
GO
INSERT INTO [InstituteSecretary]
  ([SystemUserID], [InstituteID])
VALUES 
	(14, 1)
GO
----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'secretaryinst2', convert(binary,''),16)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(15, 'Алла', 'Секретар', 'panas@admin.com')
GO
INSERT INTO [InstituteSecretary]
  ([SystemUserID], [InstituteID])
VALUES 
	(15, 1)
GO
----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'secretaryinst3', convert(binary,''),16)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(16, 'Євгенія', 'Секретар', 'panas@admin.com')
GO
INSERT INTO [InstituteSecretary]
  ([SystemUserID], [InstituteID])
VALUES 
	(16, 1)
GO
----------------------------------------------------
--Faculty administrators

INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'adminfacult1', convert(binary,''),4)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(17, 'Максим', 'Адмін', 'panas@admin.com')
GO
INSERT INTO [FacultyAdmin]
  ([SystemUserID], [FacultyID])
VALUES 
	(17, 1)
GO
----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'adminfacult2', convert(binary,''),4)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(18, 'Артем', 'Адмін', 'panas@admin.com')
GO
INSERT INTO [FacultyAdmin]
  ([SystemUserID], [FacultyID])
VALUES 
	(18, 2)
GO
----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'adminfacult3', convert(binary,''),4)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(19, 'Тарас', 'Адмін', 'panas@admin.com')
GO
INSERT INTO [FacultyAdmin]
  ([SystemUserID], [FacultyID])
VALUES 
	(19, 3)
GO
----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'adminfacult4', convert(binary,''),4)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(20, 'Юрій', 'Адмін', 'panas@admin.com')
GO
INSERT INTO [FacultyAdmin]
  ([SystemUserID], [FacultyID])
VALUES 
	(20, 4)
GO
----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'adminfacult5', convert(binary,''),4)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(21, 'Ярослав', 'Адмін', 'panas@admin.com')
GO
INSERT INTO [FacultyAdmin]
  ([SystemUserID], [FacultyID])
VALUES 
	(21, 5)
GO
----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'adminfacult6', convert(binary,''),4)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(22, 'Микола', 'Адмін', 'panas@admin.com')
GO
INSERT INTO [FacultyAdmin]
  ([SystemUserID], [FacultyID])
VALUES 
	(22, 6)
GO
----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'adminfacult7', convert(binary,''),4)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(23, 'Роман', 'Адмін', 'panas@admin.com')
GO
INSERT INTO [FacultyAdmin]
  ([SystemUserID], [FacultyID])
VALUES 
	(23, 7)
GO
----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'adminfacult8', convert(binary,''),4)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(24, 'Олександр', 'Адмін', 'panas@admin.com')
GO
INSERT INTO [FacultyAdmin]
  ([SystemUserID], [FacultyID])
VALUES 
	(24, 8)
GO
----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'adminfacult9', convert(binary,''),4)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(25, 'Валентин', 'Адмін', 'panas@admin.com')
GO
INSERT INTO [FacultyAdmin]
  ([SystemUserID], [FacultyID])
VALUES 
	(25, 9)
GO
----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'adminfacult10', convert(binary,''),4)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(26, 'Валерій', 'Адмін', 'panas@admin.com')
GO
INSERT INTO [FacultyAdmin]
  ([SystemUserID], [FacultyID])
VALUES 
	(26, 10)
GO
----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'adminfacult11', convert(binary,''),4)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(27, 'Віктор', 'Адмін', 'panas@admin.com')
GO
INSERT INTO [FacultyAdmin]
  ([SystemUserID], [FacultyID])
VALUES 
	(27, 11)
GO
----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'adminfacult12', convert(binary,''),4)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(28, 'Вірослав', 'Адмін', 'panas@admin.com')
GO
INSERT INTO [FacultyAdmin]
  ([SystemUserID], [FacultyID])
VALUES 
	(28, 12)
GO
----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'adminfacult13', convert(binary,''),4)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(29, 'Віталій', 'Адмін', 'panas@admin.com')
GO
INSERT INTO [FacultyAdmin]
  ([SystemUserID], [FacultyID])
VALUES 
	(29, 13)
GO
----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'adminfacult14', convert(binary,''),4)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(30, 'Влад', 'Адмін', 'panas@admin.com')
GO
INSERT INTO [FacultyAdmin]
  ([SystemUserID], [FacultyID])
VALUES 
	(30, 14)
GO
----------------------------------------------------
--Faculty secretaries

INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'secretaryfacult1', convert(binary,''),32)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(31, 'Аврора', 'Секретар', 'panas@admin.com')
GO
INSERT INTO [FacultySecretary]
  ([SystemUserID], [FacultyID])
VALUES 
	(31, 1)
GO
----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'secretaryfacult2', convert(binary,''),32)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(32, 'Агата', 'Секретар', 'panas@admin.com')
GO
INSERT INTO [FacultySecretary]
  ([SystemUserID], [FacultyID])
VALUES 
	(32, 2)
GO
----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'secretaryfacult3', convert(binary,''),32)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(33, 'Ада', 'Секретар', 'panas@admin.com')
GO
INSERT INTO [FacultySecretary]
  ([SystemUserID], [FacultyID])
VALUES 
	(33, 3)
GO
----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'secretaryfacult4', convert(binary,''),32)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(34, 'Алевтина', 'Секретар', 'panas@admin.com')
GO
INSERT INTO [FacultySecretary]
  ([SystemUserID], [FacultyID])
VALUES 
	(34, 4)
GO
----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'secretaryfacult5', convert(binary,''),32)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(35, 'Аліна', 'Секретар', 'panas@admin.com')
GO
INSERT INTO [FacultySecretary]
  ([SystemUserID], [FacultyID])
VALUES 
	(35, 5)
GO
----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'secretaryfacult6', convert(binary,''),32)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(36, 'Аліса', 'Секретар', 'panas@admin.com')
GO
INSERT INTO [FacultySecretary]
  ([SystemUserID], [FacultyID])
VALUES 
	(36, 6)
GO
----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'secretaryfacult7', convert(binary,''),32)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(37, 'Альбіна', 'Секретар', 'panas@admin.com')
GO
INSERT INTO [FacultySecretary]
  ([SystemUserID], [FacultyID])
VALUES 
	(37, 7)
GO
----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'secretaryfacult8', convert(binary,''),32)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(38, 'Анатолія', 'Секретар', 'panas@admin.com')
GO
INSERT INTO [FacultySecretary]
  ([SystemUserID], [FacultyID])
VALUES 
	(38, 8)
GO
----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'secretaryfacult9', convert(binary,''),32)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(39, 'Ангеліна', 'Секретар', 'panas@admin.com')
GO
INSERT INTO [FacultySecretary]
  ([SystemUserID], [FacultyID])
VALUES 
	(39, 9)
GO
----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'secretaryfacult10', convert(binary,''),32)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(40, 'Анжела', 'Секретар', 'panas@admin.com')
GO
INSERT INTO [FacultySecretary]
  ([SystemUserID], [FacultyID])
VALUES 
	(40, 10)
GO
----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'secretaryfacult11', convert(binary,''),32)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(41, 'Антоніна', 'Секретар', 'panas@admin.com')
GO
INSERT INTO [FacultySecretary]
  ([SystemUserID], [FacultyID])
VALUES 
	(41, 11)
GO
----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'secretaryfacult12', convert(binary,''),32)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(42, 'Анфіса', 'Секретар', 'panas@admin.com')
GO
INSERT INTO [FacultySecretary]
  ([SystemUserID], [FacultyID])
VALUES 
	(42, 12)
GO
----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'secretaryfacult13', convert(binary,''),32)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(43, 'Аполлонія', 'Секретар', 'panas@admin.com')
GO
INSERT INTO [FacultySecretary]
  ([SystemUserID], [FacultyID])
VALUES 
	(43, 13)
GO
----------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'secretaryfacult14', convert(binary,''),32)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(44, 'Афіна', 'Секретар', 'panas@admin.com')
GO
INSERT INTO [FacultySecretary]
  ([SystemUserID], [FacultyID])
VALUES 
	(44, 14)
GO
----------------------------------------------------
--Teachers

INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher1', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(45, 'Раїса', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(45, 1)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher2', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(46, 'Ярослав', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(46, 1)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher3', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(47, 'Ізяслав', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(47, 1)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher4', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(48, 'Златомир', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(48, 1)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher5', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(49, 'Княжослав', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(49, 1)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher6', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(50, 'Анфіса', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(50, 2)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher7', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(51, 'Стефаній', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(51, 2)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher8', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(52, 'Любомир', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(52, 2)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher9', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(53, 'Добряна', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(53, 2)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher10', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(54, 'Назарій', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(54, 2)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher11', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(55, 'Агата', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(55, 3)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher12', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(56, 'Олелько', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(56, 3)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher13', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(57, 'Двалін', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(57, 3)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher14', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(58, 'Олена', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(58, 3)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher15', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(59, 'Двалін', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(59, 3)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher16', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(60, 'Назар', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(60, 4)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher17', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(61, 'Володимир', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(61, 4)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher18', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(62, 'Євстафій', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(62, 4)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher19', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(63, 'Ізяслав', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(63, 4)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher20', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(64, 'Омелян', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(64, 4)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher21', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(65, 'Орест', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(65, 5)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher22', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(66, 'Олесь', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(66, 5)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher23', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(67, 'Святослав', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(67, 5)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher24', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(68, 'Віта', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(68, 5)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher25', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(69, 'Божан', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(69, 5)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher26', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(70, 'Пилип', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(70, 6)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher27', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(71, 'Богдана', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(71, 6)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher28', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(72, 'Ярило', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(72, 6)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher29', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(73, 'Уляна', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(73, 6)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher30', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(74, 'Борислав', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(74, 6)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher31', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(75, 'Валерія', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(75, 7)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher32', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(76, 'Оксана', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(76, 7)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher33', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(77, 'Сіріус', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(77, 7)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher34', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(78, 'Максим', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(78, 7)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher35', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(79, 'Сварг', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(79, 7)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher36', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(80, 'Остап', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(80, 8)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher37', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(81, 'Ітринатор', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(81, 8)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher38', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(82, 'Роксолана', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(82, 8)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher39', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(83, 'Вербан', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(83, 8)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher40', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(84, 'Арагог', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(84, 8)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher41', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(85, 'Аврелія', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(85, 9)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher42', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(86, 'Томас', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(86, 9)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher43', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(87, 'Родослав', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(87, 9)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher44', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(88, 'Ніна', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(88, 9)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher45', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(89, 'Анжела', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(89, 9)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher46', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(90, 'Злотодан', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(90, 10)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher47', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(91, 'Осмомисл', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(91, 10)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher48', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(92, 'Братислав', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(92, 10)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher49', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(93, 'Григорій', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(93, 10)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher50', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(94, 'Квай-Гон', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(94, 10)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher51', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(95, 'Назар', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(95, 11)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher52', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(96, 'Злотодан', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(96, 11)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher53', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(97, 'Ілля', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(97, 11)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher54', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(98, 'Леголас', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(98, 11)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher55', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(99, 'Милодух', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(99, 11)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher56', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(100, 'Мирон', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(100, 12)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher57', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(101, 'Норі', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(101, 12)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher58', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(102, 'Потап', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(102, 12)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher59', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(103, 'Зіновій', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(103, 12)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher60', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(104, 'Забава', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(104, 12)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher61', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(105, 'Світлана', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(105, 13)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher62', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(106, 'Ольга', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(106, 13)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher63', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(107, 'Любомир', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(107, 13)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher64', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(108, 'Ада', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(108, 13)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher65', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(109, 'Антонія', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(109, 13)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher66', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(110, 'Богуслава', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(110, 14)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher67', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(111, 'Людмила', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(111, 14)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher68', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(112, 'Томас', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(112, 14)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher69', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(113, 'Ібрагім', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(113, 14)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher70', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(114, 'Петро', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(114, 14)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher71', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(115, 'Квай-Гон', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(115, 15)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher72', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(116, 'Гостомисл', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(116, 15)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher73', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(117, 'Том', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(117, 15)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher74', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(118, 'Захар', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(118, 15)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher75', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(119, 'Дана', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(119, 15)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher76', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(120, 'Леся', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(120, 16)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher77', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(121, 'Оксана', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(121, 16)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher78', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(122, 'Ярема', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(122, 16)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher79', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(123, 'Орі', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(123, 16)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher80', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(124, 'Ясен', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(124, 16)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher81', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(125, 'Рон', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(125, 17)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher82', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(126, 'Сонцедар', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(126, 17)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher83', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(127, 'Маркіян', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(127, 17)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher84', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(128, 'Либідь', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(128, 17)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher85', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(129, 'Щек', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(129, 17)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher86', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(130, 'Максим', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(130, 18)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher87', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(131, 'Світослав', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(131, 18)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher88', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(132, 'Осмомисл', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(132, 18)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher89', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(133, 'Раїса', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(133, 18)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher90', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(134, 'Квай-Гон', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(134, 18)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher91', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(135, 'Микола', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(135, 19)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher92', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(136, 'Дана', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(136, 19)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher93', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(137, 'Устим', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(137, 19)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher94', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(138, 'Зиновій', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(138, 19)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher95', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(139, 'Балін', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(139, 19)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher96', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(140, 'Афіна', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(140, 20)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher97', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(141, 'Гордій', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(141, 20)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher98', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(142, 'Іванна', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(142, 20)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher99', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(143, 'Гордій', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(143, 20)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher100', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(144, 'Руслана', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(144, 20)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher101', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(145, 'Любослав', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(145, 21)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher102', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(146, 'Любава', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(146, 21)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher103', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(147, 'Вероніка', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(147, 21)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher104', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(148, 'Ада', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(148, 21)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher105', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(149, 'Богдан', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(149, 21)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher106', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(150, 'Денис', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(150, 22)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher107', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(151, 'Георгій', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(151, 22)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher108', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(152, 'Арагорн', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(152, 22)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher109', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(153, 'Доброслав', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(153, 22)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher110', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(154, 'Руслан', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(154, 22)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher111', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(155, 'Остромир', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(155, 23)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher112', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(156, 'Лукян', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(156, 23)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher113', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(157, 'Назар', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(157, 23)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher114', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(158, 'Тарас', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(158, 23)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher115', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(159, 'Дана', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(159, 23)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher116', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(160, 'Ібрагім', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(160, 24)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher117', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(161, 'Остап', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(161, 24)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher118', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(162, 'Острозор', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(162, 24)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher119', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(163, 'Арагорн', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(163, 24)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher120', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(164, 'Жадан', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(164, 24)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher121', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(165, 'Віктор', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(165, 25)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher122', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(166, 'Кий', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(166, 25)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher123', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(167, 'Юрій', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(167, 25)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher124', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(168, 'Дарій', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(168, 25)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher125', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(169, 'Люк', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(169, 25)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher126', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(170, 'Жадан', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(170, 26)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher127', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(171, 'Сварг', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(171, 26)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher128', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(172, 'Анна', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(172, 26)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher129', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(173, 'Гостомисл', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(173, 26)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher130', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(174, 'Леголас', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(174, 26)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher131', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(175, 'Ярополк', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(175, 27)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher132', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(176, 'Норі', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(176, 27)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher133', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(177, 'Милован', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(177, 27)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher134', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(178, 'Енакін', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(178, 27)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher135', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(179, 'Денис', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(179, 27)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher136', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(180, 'Братислав', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(180, 28)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher137', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(181, 'Забава', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(181, 28)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher138', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(182, 'Аріадна', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(182, 28)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher139', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(183, 'Леся', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(183, 28)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher140', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(184, 'Корній', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(184, 28)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher141', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(185, 'Градимир', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(185, 29)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher142', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(186, 'Петро', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(186, 29)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher143', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(187, 'Дарій', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(187, 29)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher144', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(188, 'Корній', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(188, 29)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher145', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(189, 'Сварг', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(189, 29)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher146', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(190, 'Піппін', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(190, 30)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher147', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(191, 'Зорян', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(191, 30)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher148', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(192, 'Гаррі', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(192, 30)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher149', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(193, 'Олексій', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(193, 30)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher150', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(194, 'Аліса', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(194, 30)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher151', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(195, 'Гордій', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(195, 31)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher152', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(196, 'Аврелія', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(196, 31)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher153', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(197, 'Гертруда', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(197, 31)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher154', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(198, 'Маряна', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(198, 31)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher155', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(199, 'Галина', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(199, 31)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher156', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(200, 'Арагорн', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(200, 32)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher157', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(201, 'Градислав', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(201, 32)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher158', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(202, 'Осмомисл', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(202, 32)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher159', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(203, 'Сіріус', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(203, 32)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher160', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(204, 'Анастасія', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(204, 32)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher161', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(205, 'Ярополк', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(205, 33)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher162', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(206, 'Киян', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(206, 33)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher163', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(207, 'Дана', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(207, 33)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher164', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(208, 'Влад', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(208, 33)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher165', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(209, 'Людмила', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(209, 33)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher166', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(210, 'Милован', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(210, 34)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher167', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(211, 'Роксолана', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(211, 34)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher168', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(212, 'Києслав', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(212, 34)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher169', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(213, 'Івантослав', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(213, 34)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher170', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(214, 'Аврелія', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(214, 34)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher171', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(215, 'Осмомисл', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(215, 35)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher172', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(216, 'Віта', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(216, 35)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher173', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(217, 'Турбог', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(217, 35)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher174', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(218, 'Юрій', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(218, 35)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher175', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(219, 'Кілі', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(219, 35)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher176', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(220, 'Стефаній', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(220, 36)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher177', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(221, 'Том', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(221, 36)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher178', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(222, 'Дивозір', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(222, 36)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher179', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(223, 'Ярило', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(223, 36)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher180', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(224, 'Кілі', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(224, 36)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher181', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(225, 'Ольга', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(225, 37)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher182', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(226, 'Ратимир', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(226, 37)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher183', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(227, 'Злат', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(227, 37)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher184', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(228, 'Остап', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(228, 37)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher185', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(229, 'Ірина', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(229, 37)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher186', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(230, 'Тома', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(230, 38)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher187', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(231, 'Дмитро', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(231, 38)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher188', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(232, 'Остромир', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(232, 38)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher189', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(233, 'Захар', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(233, 38)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher190', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(234, 'Гліб', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(234, 38)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher191', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(235, 'Любов', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(235, 39)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher192', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(236, 'Златомир', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(236, 39)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher193', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(237, 'Том', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(237, 39)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher194', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(238, 'Всеволод', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(238, 39)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher195', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(239, 'Антонія', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(239, 39)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher196', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(240, 'Зінаїда', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(240, 40)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher197', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(241, 'Антонія', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(241, 40)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher198', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(242, 'Балін', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(242, 40)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher199', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(243, 'Олександр', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(243, 40)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher200', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(244, 'Том', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(244, 40)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher201', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(245, 'Гендальф', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(245, 41)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher202', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(246, 'Русана', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(246, 41)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher203', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(247, 'Юрій', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(247, 41)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher204', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(248, 'Анна', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(248, 41)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher205', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(249, 'Влад', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(249, 41)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher206', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(250, 'Любомудр', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(250, 42)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher207', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(251, 'Арагог', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(251, 42)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher208', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(252, 'Вербан', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(252, 42)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher209', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(253, 'Інга', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(253, 42)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher210', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(254, 'Владислав', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(254, 42)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher211', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(255, 'Маряна', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(255, 43)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher212', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(256, 'Світлана', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(256, 43)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher213', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(257, 'Злата', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(257, 43)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher214', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(258, 'Пилип', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(258, 43)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher215', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(259, 'Валерія', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(259, 43)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher216', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(260, 'Обі', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(260, 44)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher217', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(261, 'Балін', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(261, 44)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher218', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(262, 'Захар', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(262, 44)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher219', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(263, 'Зиновій', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(263, 44)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher220', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(264, 'Меланія', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(264, 44)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher221', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(265, 'Гаврило', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(265, 45)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher222', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(266, 'Любослав', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(266, 45)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher223', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(267, 'Норі', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(267, 45)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher224', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(268, 'Вербан', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(268, 45)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher225', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(269, 'Віолетта', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(269, 45)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher226', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(270, 'Георгій', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(270, 46)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher227', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(271, 'Ян', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(271, 46)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher228', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(272, 'Милослав', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(272, 46)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher229', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(273, 'Ніна', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(273, 46)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher230', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(274, 'Гордій', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(274, 46)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher231', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(275, 'Любов', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(275, 47)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher232', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(276, 'Філі', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(276, 47)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher233', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(277, 'Варвара', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(277, 47)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher234', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(278, 'Пилип', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(278, 47)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher235', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(279, 'Софія', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(279, 47)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher236', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(280, 'Тамара', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(280, 48)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher237', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(281, 'Ляна', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(281, 48)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher238', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(282, 'Олелько', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(282, 48)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher239', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(283, 'Зорепад', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(283, 48)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher240', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(284, 'Микола', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(284, 48)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher241', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(285, 'Олена', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(285, 49)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher242', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(286, 'Падме', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(286, 49)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher243', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(287, 'Ярема', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(287, 49)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher244', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(288, 'Квай-Гон', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(288, 49)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher245', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(289, 'Жадан', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(289, 49)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher246', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(290, 'Маряна', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(290, 50)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher247', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(291, 'Мирослав', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(291, 50)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher248', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(292, 'Сварг', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(292, 50)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher249', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(293, 'Любомисл', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(293, 50)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher250', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(294, 'Сіріус', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(294, 50)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher251', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(295, 'Вікторія', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(295, 51)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher252', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(296, 'Любомудр', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(296, 51)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher253', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(297, 'Богуслава', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(297, 51)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher254', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(298, 'Сварг', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(298, 51)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher255', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(299, 'Ніна', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(299, 51)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher256', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(300, 'Юрій', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(300, 52)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher257', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(301, 'Либідь', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(301, 52)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher258', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(302, 'Олексій', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(302, 52)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher259', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(303, 'Рон', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(303, 52)
GO
---------------------------------------------------
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'teacher260', convert(binary,''),64)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(304, 'Тамара', 'Викладач', 'teacher@admin.com')
GO
INSERT INTO [Teacher]
  ([SystemUserID], [CathedraID])
VALUES 
	(304, 52)
GO
---------------------------------------------------
