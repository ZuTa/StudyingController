--Add functions here
IF OBJECT_ID('dbo.ABBREVIATION') IS NOT NULL
  DROP FUNCTION [ABBREVIATION]
GO

CREATE FUNCTION dbo.[ABBREVIATION] ( @str NVARCHAR(4000) )
RETURNS NVARCHAR(2000)
AS
BEGIN
    DECLARE @retval NVARCHAR(2000);

    SET @str=RTRIM(LTRIM(@str));
    SET @retval=LEFT(@str,1);

    WHILE CHARINDEX(' ',@str,1)>0 BEGIN
        SET @str=LTRIM(RIGHT(@str,LEN(@str)-CHARINDEX(' ',@str,1)));
		IF(CHARINDEX(' ',@str,1) > 3 OR CHARINDEX(' ',@str,1) <= 0)
			SET @retval+=UPPER(LEFT(@str,1));
    END

    RETURN @retval;
END
GO
--Default user
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
	(1, N'Панас', N'Адмінич', 'panas@admin.com')
