USE [UniversityDB] 
GO

--Institutes

--ID = 1
INSERT INTO [Institute]
  ( [Name])
VALUES 
        (N'Військовий інститут')
GO
----------------------------------------

--ID = 2
INSERT INTO [Institute]
  ( [Name])
VALUES 
        (N'Інститут філології')
GO
----------------------------------------

--ID = 3
INSERT INTO [Institute]
  ( [Name])
VALUES 
        (N'Інститут журналістики')
GO
----------------------------------------
----------------------------------------
--Faculties without institute

--ID = 1
INSERT INTO [Faculty]
  ( [Name])
VALUES 
        (N'Кібернетики')
GO
----------------------------------------

--ID = 2
INSERT INTO [Faculty]
  ( [Name])
VALUES 
        (N'Механіко-математичний')
GO
----------------------------------------

--ID = 3
INSERT INTO [Faculty]
  ( [Name])
VALUES 
        (N'Радіофізичний')
GO
----------------------------------------

--ID = 4
INSERT INTO [Faculty]
  ( [Name])
VALUES 
        (N'Фізичний')
GO

----------------------------------------
----------------------------------------
--Faculties with institute

--ID = 5
INSERT INTO [Faculty]
  ( [Name], [InstituteID])
VALUES 
        (N'Військово-гуманітарний', 1)
GO
----------------------------------------

--ID = 6
INSERT INTO [Faculty]
  ( [Name], [InstituteID])
VALUES 
        (N'Військовий фінансів і права', 1)
GO
----------------------------------------

--ID = 7
INSERT INTO [Faculty]
  ( [Name], [InstituteID])
VALUES 
        (N'Військової підготовки', 1)
GO
----------------------------------------

--ID = 8
INSERT INTO [Faculty]
  ( [Name], [InstituteID])
VALUES 
        (N'Соціальних комунікацій', 2)
GO
----------------------------------------

--ID = 9
INSERT INTO [Faculty]
  ( [Name], [InstituteID])
VALUES 
        (N'Видавничої справи та редагування', 2)
GO
----------------------------------------

--ID = 10
INSERT INTO [Faculty]
  ( [Name], [InstituteID])
VALUES 
        (N'Періодичної преси', 2)
GO
----------------------------------------

--ID = 11
INSERT INTO [Faculty]
  ( [Name], [InstituteID])
VALUES 
        (N'Сучасної української мови, історії та стилістики української мови', 3)
GO
----------------------------------------

--ID = 12
INSERT INTO [Faculty]
  ( [Name], [InstituteID])
VALUES 
        (N'Історії української літератури та шевченкознавства', 3)
GO
----------------------------------------

--ID = 13
INSERT INTO [Faculty]
  ( [Name], [InstituteID])
VALUES 
        (N'Новітньої української літератури', 3)
GO
----------------------------------------

--ID = 14
INSERT INTO [Faculty]
  ( [Name], [InstituteID])
VALUES 
        (N'Факультет іноземних мов та військового перекладу', 1)
GO
----------------------------------------
----------------------------------------
--Specializations

--ID = 1
INSERT INTO [Specialization]
  ( [Name], [FacultyID])
VALUES 
        (N'Прикладна математика', 1)
GO
----------------------------------------

--ID = 2
INSERT INTO [Specialization]
  ( [Name], [FacultyID])
VALUES 
        (N'Інформатика', 1)
GO
----------------------------------------

--ID = 3
INSERT INTO [Specialization]
  ( [Name], [FacultyID])
VALUES 
        (N'Соціальна інформатика', 1)
GO
----------------------------------------

--ID = 4
INSERT INTO [Specialization]
  ( [Name], [FacultyID])
VALUES 
        (N'Програмна інженерія', 1)
GO
----------------------------------------

--ID = 5
INSERT INTO [Specialization]
  ( [Name], [FacultyID])
VALUES 
        (N'Системний аналіз', 1)
GO
----------------------------------------

--ID = 6
INSERT INTO [Specialization]
  ( [Name], [FacultyID])
VALUES 
        (N'Математика', 2)
GO
----------------------------------------

--ID = 7
INSERT INTO [Specialization]
  ( [Name], [FacultyID])
VALUES 
        (N'Статистика', 2)
GO
----------------------------------------

--ID = 8
INSERT INTO [Specialization]
  ( [Name], [FacultyID])
VALUES 
        (N'Актуарна та фінансова математика', 2)
GO
----------------------------------------

--ID = 9
INSERT INTO [Specialization]
  ( [Name], [FacultyID])
VALUES 
        (N'Теоретична та прикладна механіка', 2)
GO
----------------------------------------

--ID = 10
INSERT INTO [Specialization]
  ( [Name], [FacultyID])
VALUES 
        (N'Радіофізика', 3)
GO
----------------------------------------

--ID = 11
INSERT INTO [Specialization]
  ( [Name], [FacultyID])
VALUES 
        (N'Компютерна інженерія', 3)
GO
----------------------------------------

--ID = 12
INSERT INTO [Specialization]
  ( [Name], [FacultyID])
VALUES 
        (N'Радіотехніка', 3)
GO
----------------------------------------

--ID = 13
INSERT INTO [Specialization]
  ( [Name], [FacultyID])
VALUES 
        (N'Фізика', 4)
GO
----------------------------------------

--ID = 14
INSERT INTO [Specialization]
  ( [Name], [FacultyID])
VALUES 
        (N'Оптотехніка', 4)
GO
----------------------------------------

--ID = 15
INSERT INTO [Specialization]
  ( [Name], [FacultyID])
VALUES 
        (N'Астрономія', 4)
GO
----------------------------------------

--ID = 16
INSERT INTO [Specialization]
  ( [Name], [FacultyID])
VALUES 
        (N'Лазерна та оптоелектронна техніка', 4)
GO
----------------------------------------

--ID = 17
INSERT INTO [Specialization]
  ( [Name], [FacultyID])
VALUES 
        (N'Фізика конденсованого стану', 4)
GO
----------------------------------------

--ID = 18
INSERT INTO [Specialization]
  ( [Name], [FacultyID])
VALUES 
        (N'Фізика ядра та фізика високих енергій', 4)
GO
----------------------------------------
----------------------------------------
----------------------------------------
--Cathedras

--ID = 1
INSERT INTO [Cathedra]
  ( [Name], [FacultyID], [DefaultSpecializationID])
VALUES 
        (N'Обчислювальної математики', 1, 1)
GO
----------------------------------------

--ID = 2
INSERT INTO [Cathedra]
  ( [Name], [FacultyID], [DefaultSpecializationID])
VALUES 
        (N'Моделювання складних систем', 1, 1)
GO
----------------------------------------

--ID = 3
INSERT INTO [Cathedra]
  ( [Name], [FacultyID], [DefaultSpecializationID])
VALUES 
        (N'Дослідження операцій', 1, 1)
GO
----------------------------------------

--ID = 4
INSERT INTO [Cathedra]
  ( [Name], [FacultyID], [DefaultSpecializationID])
VALUES 
        (N'Теоретичної кібернетики', 1,2)
GO
----------------------------------------

--ID = 5
INSERT INTO [Cathedra]
  ( [Name], [FacultyID], [DefaultSpecializationID])
VALUES 
        (N'Теорії та технології програмування', 1,2)
GO
----------------------------------------

--ID = 6
INSERT INTO [Cathedra]
  ( [Name], [FacultyID], [DefaultSpecializationID])
VALUES 
        (N'Математичної інформатики', 1,2)
GO
----------------------------------------

--ID = 7
INSERT INTO [Cathedra]
  ( [Name], [FacultyID], [DefaultSpecializationID])
VALUES 
        (N'Системного аналізу і теорії прийняття рішень', 1,5)
GO
----------------------------------------

--ID = 8
INSERT INTO [Cathedra]
  ( [Name], [FacultyID], [DefaultSpecializationID])
VALUES 
        (N'Інформаційних систем', 1,4)
GO
----------------------------------------

--ID = 9
INSERT INTO [Cathedra]
  ( [Name], [FacultyID], [DefaultSpecializationID])
VALUES 
        (N'Прикладної статистики', 1,5)
GO
----------------------------------------

--ID = 10
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Соціальних комунікацій', 8)
GO
----------------------------------------

--ID = 11
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Видавничої справи та редагування', 9)
GO
----------------------------------------

--ID = 12
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Періодичної преси', 10)
GO
----------------------------------------

--ID = 13
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Сучасної української мови, історії та стилістики української мови', 11)
GO
----------------------------------------

--ID = 14
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Історії української літератури та шевченкознавства', 12)
GO
----------------------------------------

--ID = 15
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Новітньої української літератури', 13)
GO
----------------------------------------

--ID = 16
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Алгебри та математичної логіки', 2)
GO
----------------------------------------

--ID = 17
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Геометрії', 2)
GO
----------------------------------------

--ID = 18
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Інтегральних та диференціальних рівнянь', 2)
GO
----------------------------------------

--ID = 19
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Загальної математики, математичного аналізу', 2)
GO
----------------------------------------

--ID = 20
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Математичної фізики', 2)
GO

--ID = 21
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Механіки суцільних середовищ', 2)
GO

--ID = 22
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Теоретичної та прикладної механіки', 2)
GO
----------------------------------------

--ID = 23
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Теорії ймовірностей', 2)
GO
----------------------------------------

--ID = 24
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Статистики та актуарної математики', 2)
GO
----------------------------------------

--ID = 25
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Математики і теоретичної радіофізики', 3)
GO
----------------------------------------

--ID = 26
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Електрофізики', 3)
GO
----------------------------------------

--ID = 27
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Фізичної електроніки', 3)
GO
----------------------------------------


--ID = 28
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Квантової радіофізики', 3)
GO
----------------------------------------

--ID = 29
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Компютерної інженерії', 3)
GO
----------------------------------------

--ID = 30
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Нанофізики і наноелектроніки', 3)
GO
----------------------------------------

--ID = 31
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Медичної радіофізики', 3)
GO
----------------------------------------

--ID = 32
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Радіотехніки і електронних систем', 3)
GO
----------------------------------------

--ID = 33
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Астрономії і фізики космосу', 4)
GO
----------------------------------------

--ID = 34
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Експериментальної фізики', 4)
GO
----------------------------------------

--ID = 35
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Загальної фізики', 4)
GO
----------------------------------------

--ID = 36
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Квантової теорії поля', 4)
GO
----------------------------------------

--ID = 37
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Молекулярної фізики', 4)
GO
----------------------------------------

--ID = 38
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Оптики', 4)
GO
----------------------------------------

--ID = 39
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Теоретичної фізики', 4)
GO
----------------------------------------

--ID = 40
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Фізики металів', 4)
GO
----------------------------------------

--ID = 41
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Фізики функціональних матеріалів', 4)
GO
----------------------------------------

--ID = 42
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Ядерної фізики', 4)
GO
----------------------------------------

--ID = 43
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Кафедра військової преси та інформації', 14)
GO
----------------------------------------

--ID = 44
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Кафедра військового перекладу та спеціальної мовної підготовки', 14)
GO
----------------------------------------

--ID = 45
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Кафедра топогеодезичного та навігаційного забезпечення військ', 14)
GO
----------------------------------------

--ID = 46
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Кафедра військової педагогіки та психології', 5)
GO
----------------------------------------

--ID = 47
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Кафедра загальновійськових дисциплін', 5)
GO
----------------------------------------

--ID = 48
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Кафедра тактики', 5)
GO
----------------------------------------

--ID = 49
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Кафедра фінансів збройних сил', 6)
GO
----------------------------------------

--ID = 50
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Кафедра бухгалтерcького обліку, звітності та контрольно-ревізійної роботи', 6)
GO
----------------------------------------

--ID = 51
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Кафедра військового права', 6)
GO
----------------------------------------

--ID = 52
INSERT INTO [Cathedra]
  ( [Name], [FacultyID])
VALUES 
        (N'Кафедра математичного та програмного забезпечення АСУ', 7)
GO

----------------------------------------
----------------------------------------
--Study ranges

--ID = 1
INSERT INTO [StudyRange]
  ( [Year],[Part])
VALUES 
        (2013, 1)
GO

----------------------------------------
----------------------------------------
--Groups

INSERT INTO [Group]
        ([Name], [SpecializationID], [CathedraID], [StudyRangeID])
        SELECT dbo.ABBREVIATION([C].Name) + '-1', [C].[DefaultSpecializationID], [C].[ID], 1
        FROM [Cathedra] AS [C]
        WHERE [C].[DefaultSpecializationID] IS NOT NULL
GO
INSERT INTO [Group]
        ([Name], [SpecializationID], [CathedraID], [StudyRangeID])
        SELECT dbo.ABBREVIATION([C].Name) + '-2', [C].[DefaultSpecializationID], [C].[ID], 1
        FROM [Cathedra] AS [C]
        WHERE [C].[DefaultSpecializationID] IS NOT NULL
GO
INSERT INTO [Group]
        ([Name], [SpecializationID], [CathedraID], [StudyRangeID])
        SELECT dbo.ABBREVIATION([C].Name) + '-3', [C].[DefaultSpecializationID], [C].[ID], 1
        FROM [Cathedra] AS [C]
        WHERE [C].[DefaultSpecializationID] IS NOT NULL
GO
INSERT INTO [Group]
        ([Name], [SpecializationID], [CathedraID], [StudyRangeID])
        SELECT dbo.ABBREVIATION([C].Name) + '-4', [C].[DefaultSpecializationID], [C].[ID], 1
        FROM [Cathedra] AS [C]
        WHERE [C].[DefaultSpecializationID] IS NOT NULL
GO
