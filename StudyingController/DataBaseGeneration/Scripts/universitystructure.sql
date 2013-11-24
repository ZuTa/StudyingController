USE [UniversityDB] 
GO

--Institutes

--ID = 1
INSERT INTO [Institute]
  ( [Name])
VALUES 
	('Військовий інститут')
GO
----------------------------------------

--ID = 2
INSERT INTO [Institute]
  ( [Name])
VALUES 
	('Інститут філології')
GO
----------------------------------------

--ID = 3
INSERT INTO [Institute]
  ( [Name])
VALUES 
	('Інститут журналістики')
GO
----------------------------------------
----------------------------------------
--Faculties without institute

--ID = 1
INSERT INTO [Faculty]
  ( [Name])
VALUES 
	('Кібернетики')
GO
----------------------------------------

--ID = 2
INSERT INTO [Faculty]
  ( [Name])
VALUES 
	('Механіко-математичний')
GO
----------------------------------------

--ID = 3
INSERT INTO [Faculty]
  ( [Name])
VALUES 
	('Радіофізичний')
GO
----------------------------------------

--ID = 4
INSERT INTO [Faculty]
  ( [Name])
VALUES 
	('Фізичний')
GO

----------------------------------------
----------------------------------------
--Faculties with institute

--ID = 5
INSERT INTO [Faculty]
  ( [Name], [InstituteID])
VALUES 
	('Військово-гуманітарний', 1)
GO
----------------------------------------

--ID = 6
INSERT INTO [Faculty]
  ( [Name], [InstituteID])
VALUES 
	('Військовий фінансів і права', 1)
GO
----------------------------------------

--ID = 7
INSERT INTO [Faculty]
  ( [Name], [InstituteID])
VALUES 
	('Військової підготовки', 1)
GO
----------------------------------------

--ID = 8
INSERT INTO [Faculty]
  ( [Name], [InstituteID])
VALUES 
	('Соціальних комунікацій', 2)
GO
----------------------------------------

--ID = 9
INSERT INTO [Faculty]
  ( [Name], [InstituteID])
VALUES 
	('Видавничої справи та редагування', 2)
GO
----------------------------------------

--ID = 10
INSERT INTO [Faculty]
  ( [Name], [InstituteID])
VALUES 
	('Періодичної преси', 2)
GO
----------------------------------------

--ID = 11
INSERT INTO [Faculty]
  ( [Name], [InstituteID])
VALUES 
	('Сучасної української мови, історії та стилістики української мови', 3)
GO
----------------------------------------

--ID = 12
INSERT INTO [Faculty]
  ( [Name], [InstituteID])
VALUES 
	('Історії української літератури та шевченкознавства', 3)
GO
----------------------------------------

--ID = 13
INSERT INTO [Faculty]
  ( [Name], [InstituteID])
VALUES 
	('Новітньої української літератури', 3)
GO
----------------------------------------

--ID = 14
INSERT INTO [Faculty]
  ( [Name], [InstituteID])
VALUES 
	('Факультет іноземних мов та військового перекладу', 1)
GO

----------------------------------------
----------------------------------------
--Cathedras

--ID = 1
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Обчислювальної математики', 1)
GO
----------------------------------------

--ID = 2
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Моделювання складних систем', 1)
GO
----------------------------------------

--ID = 3
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Дослідження операцій', 1)
GO
----------------------------------------

--ID = 4
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Теоретичної кібернетики', 1)
GO
----------------------------------------

--ID = 5
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Теорії та технології програмування', 1)
GO
----------------------------------------

--ID = 6
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Математичної інформатики', 1)
GO
----------------------------------------

--ID = 7
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Системного аналізу і теорії прийняття рішень', 1)
GO
----------------------------------------

--ID = 8
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Інформаційних систем', 1)
GO
----------------------------------------

--ID = 9
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Прикладної статистики', 1)
GO
----------------------------------------

--ID = 10
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Соціальних комунікацій', 8)
GO
----------------------------------------

--ID = 11
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Видавничої справи та редагування', 9)
GO
----------------------------------------

--ID = 12
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Періодичної преси', 10)
GO
----------------------------------------

--ID = 13
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Сучасної української мови, історії та стилістики української мови', 11)
GO
----------------------------------------

--ID = 14
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Історії української літератури та шевченкознавства', 12)
GO
----------------------------------------

--ID = 15
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Новітньої української літератури', 13)
GO
----------------------------------------

--ID = 16
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Алгебри та математичної логіки', 2)
GO
----------------------------------------

--ID = 17
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Геометрії', 2)
GO
----------------------------------------

--ID = 18
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Інтегральних та диференціальних рівнянь', 2)
GO
----------------------------------------

--ID = 19
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Загальної математики, математичного аналізу', 2)
GO
----------------------------------------

--ID = 20
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Математичної фізики', 2)
GO

--ID = 21
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Механіки суцільних середовищ', 2)
GO

--ID = 22
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Теоретичної та прикладної механіки', 2)
GO
----------------------------------------

--ID = 23
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Теорії ймовірностей', 2)
GO
----------------------------------------

--ID = 24
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Статистики та актуарної математики', 2)
GO
----------------------------------------

--ID = 25
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Математики і теоретичної радіофізики', 3)
GO
----------------------------------------

--ID = 26
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Електрофізики', 3)
GO
----------------------------------------

--ID = 27
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Фізичної електроніки', 3)
GO
----------------------------------------


--ID = 28
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Квантової радіофізики', 3)
GO
----------------------------------------

--ID = 29
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Компютерної інженерії', 3)
GO
----------------------------------------

--ID = 30
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Нанофізики і наноелектроніки', 3)
GO
----------------------------------------

--ID = 31
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Медичної радіофізики', 3)
GO
----------------------------------------

--ID = 32
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Радіотехніки і електронних систем', 3)
GO
----------------------------------------

--ID = 33
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Астрономії і фізики космосу', 4)
GO
----------------------------------------

--ID = 34
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Експериментальної фізики', 4)
GO
----------------------------------------

--ID = 35
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Загальної фізики', 4)
GO
----------------------------------------

--ID = 36
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Квантової теорії поля', 4)
GO
----------------------------------------

--ID = 37
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Молекулярної фізики', 4)
GO
----------------------------------------

--ID = 38
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Оптики', 4)
GO
----------------------------------------

--ID = 39
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Теоретичної фізики', 4)
GO
----------------------------------------

--ID = 40
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Фізики металів', 4)
GO
----------------------------------------

--ID = 41
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Фізики функціональних матеріалів', 4)
GO
----------------------------------------

--ID = 42
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Ядерної фізики', 4)
GO
----------------------------------------

--ID = 43
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Кафедра військової преси та інформації', 14)
GO
----------------------------------------

--ID = 44
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Кафедра військового перекладу та спеціальної мовної підготовки', 14)
GO
----------------------------------------

--ID = 45
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Кафедра топогеодезичного та навігаційного забезпечення військ', 14)
GO
----------------------------------------

--ID = 46
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Кафедра військової педагогіки та психології', 5)
GO
----------------------------------------

--ID = 47
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Кафедра загальновійськових дисциплін', 5)
GO
----------------------------------------

--ID = 48
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Кафедра тактики', 5)
GO
----------------------------------------

--ID = 49
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Кафедра фінансів збройних сил', 6)
GO
----------------------------------------

--ID = 50
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Кафедра бухгалтерcького обліку, звітності та контрольно-ревізійної роботи', 6)
GO
----------------------------------------

--ID = 51
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Кафедра військового права', 6)
GO
----------------------------------------

--ID = 52
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
	('Кафедра математичного та програмного забезпечення АСУ', 7)
GO
----------------------------------------


----------------------------------------
----------------------------------------