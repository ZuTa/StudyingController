USE [UniversityDB] 
GO
INSERT INTO [SystemUser]
  ( [Login], [Password], [UserRole])
VALUES 
	( 'admin', convert(binary,''),1)
GO
INSERT INTO [UserInformation]
  ([SystemUserID], [FirstName], [LastName], [Email])
VALUES 
	(1, 'Панас', 'Адмінич', 'panas@admin.com')
