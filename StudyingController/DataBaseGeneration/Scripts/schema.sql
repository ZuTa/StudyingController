USE [UniversityDB]
GO
/****** Object:  Table [dbo].[StudyRange]    Script Date: 11/24/2013 23:34:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudyRange](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Year] [int] NOT NULL,
	[Part] [int] NOT NULL,
 CONSTRAINT [PK_StudyRange] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemUser]    Script Date: 11/24/2013 23:34:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SystemUser](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](50) NOT NULL,
	[Password] [binary](32) NOT NULL,
	[UserRole] [int] NOT NULL,
 CONSTRAINT [PK_SystemUser] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Control]    Script Date: 11/24/2013 23:34:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Control](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[MaxMark] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_Control] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Institute]    Script Date: 11/24/2013 23:34:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Institute](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Institute] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Faculty]    Script Date: 11/24/2013 23:34:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Faculty](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InstituteID] [int] NULL,
	[Name] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Faculty] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_Faculty_Institute] ON [dbo].[Faculty] 
(
	[InstituteID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ControlMessage]    Script Date: 11/24/2013 23:34:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ControlMessage](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Text] [nvarchar](max) NULL,
	[Date] [datetime] NOT NULL,
	[ControlID] [int] NOT NULL,
	[SystemUserID] [int] NOT NULL,
 CONSTRAINT [PK_ControlMessage] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_ControlMessage_Control] ON [dbo].[ControlMessage] 
(
	[ControlID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_ControlMessage_SystemUser] ON [dbo].[ControlMessage] 
(
	[SystemUserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 11/24/2013 23:34:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](500) NOT NULL,
	[UserID] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InstituteSecretary]    Script Date: 11/24/2013 23:34:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InstituteSecretary](
	[InstituteID] [int] NOT NULL,
	[SystemUserID] [int] NOT NULL,
 CONSTRAINT [PK_SystemUser_InstituteSecretary] PRIMARY KEY CLUSTERED 
(
	[SystemUserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_InstituteSecretary_Institute] ON [dbo].[InstituteSecretary] 
(
	[InstituteID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InstituteAdmin]    Script Date: 11/24/2013 23:34:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InstituteAdmin](
	[InstituteID] [int] NOT NULL,
	[SystemUserID] [int] NOT NULL,
 CONSTRAINT [PK_SystemUser_InstituteAdmin] PRIMARY KEY CLUSTERED 
(
	[SystemUserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_InstituteAdmin_Institute] ON [dbo].[InstituteAdmin] 
(
	[InstituteID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserInformation]    Script Date: 11/24/2013 23:34:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInformation](
	[SystemUserID] [int] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserInformation] PRIMARY KEY CLUSTERED 
(
	[SystemUserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[System_Configuration]    Script Date: 11/24/2013 23:34:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[System_Configuration](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StudyRangeID] [int] NOT NULL,
 CONSTRAINT [PK_SystemConfiguration] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_System_Configuration_StudyRange] ON [dbo].[System_Configuration] 
(
	[StudyRangeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 11/24/2013 23:34:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[SystemUserID] [int] NOT NULL,
 CONSTRAINT [PK_SystemUser_Student] PRIMARY KEY CLUSTERED 
(
	[SystemUserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Specialization]    Script Date: 11/24/2013 23:34:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Specialization](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[FacultyID] [int] NOT NULL,
 CONSTRAINT [PK_Specialization] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_Specialization_Faculty] ON [dbo].[Specialization] 
(
	[FacultyID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FacultySecretary]    Script Date: 11/24/2013 23:34:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FacultySecretary](
	[FacultyID] [int] NOT NULL,
	[SystemUserID] [int] NOT NULL,
 CONSTRAINT [PK_SystemUser_FacultySecretary] PRIMARY KEY CLUSTERED 
(
	[SystemUserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_FacultySecretary_Faculty] ON [dbo].[FacultySecretary] 
(
	[FacultyID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FacultyAdmin]    Script Date: 11/24/2013 23:34:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FacultyAdmin](
	[FacultyID] [int] NOT NULL,
	[SystemUserID] [int] NOT NULL,
 CONSTRAINT [PK_SystemUser_FacultyAdmin] PRIMARY KEY CLUSTERED 
(
	[SystemUserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_FacultyAdmin_Faculty] ON [dbo].[FacultyAdmin] 
(
	[FacultyID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mark]    Script Date: 11/24/2013 23:34:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mark](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StudentID] [int] NOT NULL,
	[Mark] [decimal](18, 0) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Mark] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_Mark_Student] ON [dbo].[Mark] 
(
	[StudentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cathedra]    Script Date: 11/24/2013 23:34:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cathedra](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FacultyID] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Cathedra] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_Cathedra_Faculty] ON [dbo].[Cathedra] 
(
	[FacultyID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 11/24/2013 23:34:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[SpecializationID] [int] NOT NULL,
	[CathedraID] [int] NOT NULL,
	[StudyRangeID] [int] NOT NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_Group_Cathedra] ON [dbo].[Group] 
(
	[CathedraID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_Group_Specialization] ON [dbo].[Group] 
(
	[SpecializationID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_Group_StudyRange] ON [dbo].[Group] 
(
	[StudyRangeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teacher]    Script Date: 11/24/2013 23:34:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teacher](
	[CathedraID] [int] NOT NULL,
	[SystemUserID] [int] NOT NULL,
 CONSTRAINT [PK_SystemUser_Teacher] PRIMARY KEY CLUSTERED 
(
	[SystemUserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_Teacher_Cathedra] ON [dbo].[Teacher] 
(
	[CathedraID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subject]    Script Date: 11/24/2013 23:34:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CathedraID] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Rate] [int] NOT NULL,
 CONSTRAINT [PK_Subject] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_Subject_Cathedra] ON [dbo].[Subject] 
(
	[CathedraID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student_Group]    Script Date: 11/24/2013 23:34:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student_Group](
	[GroupID] [int] NOT NULL,
	[StudentID] [int] NOT NULL,
 CONSTRAINT [PK_Student_Group] PRIMARY KEY NONCLUSTERED 
(
	[GroupID] ASC,
	[StudentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_Student_Group_Student] ON [dbo].[Student_Group] 
(
	[StudentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Practice]    Script Date: 11/24/2013 23:34:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Practice](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SubjectID] [int] NOT NULL,
 CONSTRAINT [PK_Practice] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_Practice_Subject] ON [dbo].[Practice] 
(
	[SubjectID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lecture]    Script Date: 11/24/2013 23:34:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lecture](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TeacherID] [int] NOT NULL,
	[SubjectID] [int] NOT NULL,
 CONSTRAINT [PK_Lecture] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_Lecture_Subject] ON [dbo].[Lecture] 
(
	[SubjectID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_Lecture_Teacher] ON [dbo].[Lecture] 
(
	[TeacherID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Attachment]    Script Date: 11/24/2013 23:34:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Attachment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[DateAdded] [datetime] NOT NULL,
	[Data] [varbinary](max) NOT NULL,
	[TeacherID] [int] NOT NULL,
 CONSTRAINT [PK_Attachment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IX_FK_Attachment_Teacher] ON [dbo].[Attachment] 
(
	[TeacherID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Control_Attachment]    Script Date: 11/24/2013 23:34:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Control_Attachment](
	[Attachment_ID] [int] NOT NULL,
	[Control_ID] [int] NOT NULL,
 CONSTRAINT [PK_Control_Attachment] PRIMARY KEY NONCLUSTERED 
(
	[Attachment_ID] ASC,
	[Control_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_Control_Attachment_Control] ON [dbo].[Control_Attachment] 
(
	[Control_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LectureControl]    Script Date: 11/24/2013 23:34:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LectureControl](
	[LectureID] [int] NOT NULL,
	[ControlID] [int] NOT NULL,
 CONSTRAINT [PK_Control_LectureControl] PRIMARY KEY CLUSTERED 
(
	[ControlID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_LectureControl_Lecture] ON [dbo].[LectureControl] 
(
	[LectureID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lecture_Group]    Script Date: 11/24/2013 23:34:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lecture_Group](
	[GroupID] [int] NOT NULL,
	[LectureID] [int] NOT NULL,
 CONSTRAINT [PK_Lecture_Group] PRIMARY KEY NONCLUSTERED 
(
	[GroupID] ASC,
	[LectureID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_Lecture_Group_Lecture] ON [dbo].[Lecture_Group] 
(
	[LectureID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Practice_Teacher]    Script Date: 11/24/2013 23:34:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Practice_Teacher](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PracticeID] [int] NOT NULL,
	[TeacherID] [int] NOT NULL,
 CONSTRAINT [PK_PracticeTeacher] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_Practice_Teacher_Practice] ON [dbo].[Practice_Teacher] 
(
	[PracticeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_Practice_Teacher_Teacher] ON [dbo].[Practice_Teacher] 
(
	[TeacherID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LectureControlMark]    Script Date: 11/24/2013 23:34:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LectureControlMark](
	[LectureControlID] [int] NOT NULL,
	[MarkID] [int] NOT NULL,
 CONSTRAINT [PK_Mark_LectureControlMark] PRIMARY KEY CLUSTERED 
(
	[MarkID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_LectureControlLectureControlMark] ON [dbo].[LectureControlMark] 
(
	[LectureControlID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PracticeControl]    Script Date: 11/24/2013 23:34:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PracticeControl](
	[PracticeID] [int] NOT NULL,
	[ControlID] [int] NOT NULL,
 CONSTRAINT [PK_Control_PracticeControl] PRIMARY KEY CLUSTERED 
(
	[ControlID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_PracticeControl_Practice] ON [dbo].[PracticeControl] 
(
	[PracticeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_PracticeControl_Practice_Teacher] ON [dbo].[PracticeControl] 
(
	[PracticeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Practice_Teacher_Student]    Script Date: 11/24/2013 23:34:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Practice_Teacher_Student](
	[Practice_TeacherID] [int] NOT NULL,
	[StudentID] [int] NOT NULL,
 CONSTRAINT [PK_Practice_Teacher_Student] PRIMARY KEY NONCLUSTERED 
(
	[Practice_TeacherID] ASC,
	[StudentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_Practice_Teacher_Student_Student] ON [dbo].[Practice_Teacher_Student] 
(
	[StudentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PracticeControlMark]    Script Date: 11/24/2013 23:34:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PracticeControlMark](
	[PracticeControlID] [int] NOT NULL,
	[MarkID] [int] NOT NULL,
 CONSTRAINT [PK_Mark_PracticeControlMark] PRIMARY KEY CLUSTERED 
(
	[MarkID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_PracticeControlPracticeControlMark] ON [dbo].[PracticeControlMark] 
(
	[PracticeControlID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_Faculty_Institute]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[Faculty]  WITH CHECK ADD  CONSTRAINT [FK_Faculty_Institute] FOREIGN KEY([InstituteID])
REFERENCES [dbo].[Institute] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Faculty] CHECK CONSTRAINT [FK_Faculty_Institute]
GO
/****** Object:  ForeignKey [FK_ControlMessage_Control]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[ControlMessage]  WITH CHECK ADD  CONSTRAINT [FK_ControlMessage_Control] FOREIGN KEY([ControlID])
REFERENCES [dbo].[Control] ([ID])
GO
ALTER TABLE [dbo].[ControlMessage] CHECK CONSTRAINT [FK_ControlMessage_Control]
GO
/****** Object:  ForeignKey [FK_ControlMessage_SystemUser]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[ControlMessage]  WITH CHECK ADD  CONSTRAINT [FK_ControlMessage_SystemUser] FOREIGN KEY([SystemUserID])
REFERENCES [dbo].[SystemUser] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ControlMessage] CHECK CONSTRAINT [FK_ControlMessage_SystemUser]
GO
/****** Object:  ForeignKey [FK_Notification_SystemUser]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_SystemUser] FOREIGN KEY([UserID])
REFERENCES [dbo].[SystemUser] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_SystemUser]
GO
/****** Object:  ForeignKey [FK_InstituteSecretary_inherits_SystemUser]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[InstituteSecretary]  WITH CHECK ADD  CONSTRAINT [FK_InstituteSecretary_inherits_SystemUser] FOREIGN KEY([SystemUserID])
REFERENCES [dbo].[SystemUser] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[InstituteSecretary] CHECK CONSTRAINT [FK_InstituteSecretary_inherits_SystemUser]
GO
/****** Object:  ForeignKey [FK_InstituteSecretary_Institute]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[InstituteSecretary]  WITH CHECK ADD  CONSTRAINT [FK_InstituteSecretary_Institute] FOREIGN KEY([InstituteID])
REFERENCES [dbo].[Institute] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[InstituteSecretary] CHECK CONSTRAINT [FK_InstituteSecretary_Institute]
GO
/****** Object:  ForeignKey [FK_InstituteAdmin_inherits_SystemUser]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[InstituteAdmin]  WITH CHECK ADD  CONSTRAINT [FK_InstituteAdmin_inherits_SystemUser] FOREIGN KEY([SystemUserID])
REFERENCES [dbo].[SystemUser] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[InstituteAdmin] CHECK CONSTRAINT [FK_InstituteAdmin_inherits_SystemUser]
GO
/****** Object:  ForeignKey [FK_InstituteAdmin_Institute]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[InstituteAdmin]  WITH CHECK ADD  CONSTRAINT [FK_InstituteAdmin_Institute] FOREIGN KEY([InstituteID])
REFERENCES [dbo].[Institute] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[InstituteAdmin] CHECK CONSTRAINT [FK_InstituteAdmin_Institute]
GO
/****** Object:  ForeignKey [FK_UserInformation_SystemUser]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[UserInformation]  WITH CHECK ADD  CONSTRAINT [FK_UserInformation_SystemUser] FOREIGN KEY([SystemUserID])
REFERENCES [dbo].[SystemUser] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserInformation] CHECK CONSTRAINT [FK_UserInformation_SystemUser]
GO
/****** Object:  ForeignKey [FK_System_Configuration_StudyRange]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[System_Configuration]  WITH CHECK ADD  CONSTRAINT [FK_System_Configuration_StudyRange] FOREIGN KEY([StudyRangeID])
REFERENCES [dbo].[StudyRange] ([ID])
GO
ALTER TABLE [dbo].[System_Configuration] CHECK CONSTRAINT [FK_System_Configuration_StudyRange]
GO
/****** Object:  ForeignKey [FK_Student_inherits_SystemUser]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_inherits_SystemUser] FOREIGN KEY([SystemUserID])
REFERENCES [dbo].[SystemUser] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_inherits_SystemUser]
GO
/****** Object:  ForeignKey [FK_Specialization_Faculty]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[Specialization]  WITH CHECK ADD  CONSTRAINT [FK_Specialization_Faculty] FOREIGN KEY([FacultyID])
REFERENCES [dbo].[Faculty] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Specialization] CHECK CONSTRAINT [FK_Specialization_Faculty]
GO
/****** Object:  ForeignKey [FK_FacultySecretary_Faculty]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[FacultySecretary]  WITH CHECK ADD  CONSTRAINT [FK_FacultySecretary_Faculty] FOREIGN KEY([FacultyID])
REFERENCES [dbo].[Faculty] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FacultySecretary] CHECK CONSTRAINT [FK_FacultySecretary_Faculty]
GO
/****** Object:  ForeignKey [FK_FacultySecretary_inherits_SystemUser]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[FacultySecretary]  WITH CHECK ADD  CONSTRAINT [FK_FacultySecretary_inherits_SystemUser] FOREIGN KEY([SystemUserID])
REFERENCES [dbo].[SystemUser] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FacultySecretary] CHECK CONSTRAINT [FK_FacultySecretary_inherits_SystemUser]
GO
/****** Object:  ForeignKey [FK_FacultyAdmin_Faculty]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[FacultyAdmin]  WITH CHECK ADD  CONSTRAINT [FK_FacultyAdmin_Faculty] FOREIGN KEY([FacultyID])
REFERENCES [dbo].[Faculty] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FacultyAdmin] CHECK CONSTRAINT [FK_FacultyAdmin_Faculty]
GO
/****** Object:  ForeignKey [FK_FacultyAdmin_inherits_SystemUser]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[FacultyAdmin]  WITH CHECK ADD  CONSTRAINT [FK_FacultyAdmin_inherits_SystemUser] FOREIGN KEY([SystemUserID])
REFERENCES [dbo].[SystemUser] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FacultyAdmin] CHECK CONSTRAINT [FK_FacultyAdmin_inherits_SystemUser]
GO
/****** Object:  ForeignKey [FK_Mark_Student]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[Mark]  WITH CHECK ADD  CONSTRAINT [FK_Mark_Student] FOREIGN KEY([StudentID])
REFERENCES [dbo].[Student] ([SystemUserID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Mark] CHECK CONSTRAINT [FK_Mark_Student]
GO
/****** Object:  ForeignKey [FK_Cathedra_Faculty]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[Cathedra]  WITH CHECK ADD  CONSTRAINT [FK_Cathedra_Faculty] FOREIGN KEY([FacultyID])
REFERENCES [dbo].[Faculty] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Cathedra] CHECK CONSTRAINT [FK_Cathedra_Faculty]
GO
/****** Object:  ForeignKey [FK_Group_Cathedra]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[Group]  WITH CHECK ADD  CONSTRAINT [FK_Group_Cathedra] FOREIGN KEY([CathedraID])
REFERENCES [dbo].[Cathedra] ([ID])
GO
ALTER TABLE [dbo].[Group] CHECK CONSTRAINT [FK_Group_Cathedra]
GO
/****** Object:  ForeignKey [FK_Group_Specialization]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[Group]  WITH CHECK ADD  CONSTRAINT [FK_Group_Specialization] FOREIGN KEY([SpecializationID])
REFERENCES [dbo].[Specialization] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Group] CHECK CONSTRAINT [FK_Group_Specialization]
GO
/****** Object:  ForeignKey [FK_Group_StudyRange]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[Group]  WITH CHECK ADD  CONSTRAINT [FK_Group_StudyRange] FOREIGN KEY([StudyRangeID])
REFERENCES [dbo].[StudyRange] ([ID])
GO
ALTER TABLE [dbo].[Group] CHECK CONSTRAINT [FK_Group_StudyRange]
GO
/****** Object:  ForeignKey [FK_Teacher_Cathedra]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[Teacher]  WITH CHECK ADD  CONSTRAINT [FK_Teacher_Cathedra] FOREIGN KEY([CathedraID])
REFERENCES [dbo].[Cathedra] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Teacher] CHECK CONSTRAINT [FK_Teacher_Cathedra]
GO
/****** Object:  ForeignKey [FK_Teacher_inherits_SystemUser]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[Teacher]  WITH CHECK ADD  CONSTRAINT [FK_Teacher_inherits_SystemUser] FOREIGN KEY([SystemUserID])
REFERENCES [dbo].[SystemUser] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Teacher] CHECK CONSTRAINT [FK_Teacher_inherits_SystemUser]
GO
/****** Object:  ForeignKey [FK_Subject_Cathedra]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[Subject]  WITH CHECK ADD  CONSTRAINT [FK_Subject_Cathedra] FOREIGN KEY([CathedraID])
REFERENCES [dbo].[Cathedra] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Subject] CHECK CONSTRAINT [FK_Subject_Cathedra]
GO
/****** Object:  ForeignKey [FK_Student_Group_Group]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[Student_Group]  WITH CHECK ADD  CONSTRAINT [FK_Student_Group_Group] FOREIGN KEY([GroupID])
REFERENCES [dbo].[Group] ([ID])
GO
ALTER TABLE [dbo].[Student_Group] CHECK CONSTRAINT [FK_Student_Group_Group]
GO
/****** Object:  ForeignKey [FK_Student_Group_Student]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[Student_Group]  WITH CHECK ADD  CONSTRAINT [FK_Student_Group_Student] FOREIGN KEY([StudentID])
REFERENCES [dbo].[Student] ([SystemUserID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Student_Group] CHECK CONSTRAINT [FK_Student_Group_Student]
GO
/****** Object:  ForeignKey [FK_Practice_Subject]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[Practice]  WITH CHECK ADD  CONSTRAINT [FK_Practice_Subject] FOREIGN KEY([SubjectID])
REFERENCES [dbo].[Subject] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Practice] CHECK CONSTRAINT [FK_Practice_Subject]
GO
/****** Object:  ForeignKey [FK_Lecture_Subject]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[Lecture]  WITH CHECK ADD  CONSTRAINT [FK_Lecture_Subject] FOREIGN KEY([SubjectID])
REFERENCES [dbo].[Subject] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Lecture] CHECK CONSTRAINT [FK_Lecture_Subject]
GO
/****** Object:  ForeignKey [FK_Lecture_Teacher]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[Lecture]  WITH CHECK ADD  CONSTRAINT [FK_Lecture_Teacher] FOREIGN KEY([TeacherID])
REFERENCES [dbo].[Teacher] ([SystemUserID])
GO
ALTER TABLE [dbo].[Lecture] CHECK CONSTRAINT [FK_Lecture_Teacher]
GO
/****** Object:  ForeignKey [FK_Attachment_Teacher]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[Attachment]  WITH CHECK ADD  CONSTRAINT [FK_Attachment_Teacher] FOREIGN KEY([TeacherID])
REFERENCES [dbo].[Teacher] ([SystemUserID])
GO
ALTER TABLE [dbo].[Attachment] CHECK CONSTRAINT [FK_Attachment_Teacher]
GO
/****** Object:  ForeignKey [FK_Control_Attachment_Attachment]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[Control_Attachment]  WITH CHECK ADD  CONSTRAINT [FK_Control_Attachment_Attachment] FOREIGN KEY([Attachment_ID])
REFERENCES [dbo].[Attachment] ([ID])
GO
ALTER TABLE [dbo].[Control_Attachment] CHECK CONSTRAINT [FK_Control_Attachment_Attachment]
GO
/****** Object:  ForeignKey [FK_Control_Attachment_Control]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[Control_Attachment]  WITH CHECK ADD  CONSTRAINT [FK_Control_Attachment_Control] FOREIGN KEY([Control_ID])
REFERENCES [dbo].[Control] ([ID])
GO
ALTER TABLE [dbo].[Control_Attachment] CHECK CONSTRAINT [FK_Control_Attachment_Control]
GO
/****** Object:  ForeignKey [FK_LectureControl_inherits_Control]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[LectureControl]  WITH CHECK ADD  CONSTRAINT [FK_LectureControl_inherits_Control] FOREIGN KEY([ControlID])
REFERENCES [dbo].[Control] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LectureControl] CHECK CONSTRAINT [FK_LectureControl_inherits_Control]
GO
/****** Object:  ForeignKey [FK_LectureControl_Lecture]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[LectureControl]  WITH CHECK ADD  CONSTRAINT [FK_LectureControl_Lecture] FOREIGN KEY([LectureID])
REFERENCES [dbo].[Lecture] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LectureControl] CHECK CONSTRAINT [FK_LectureControl_Lecture]
GO
/****** Object:  ForeignKey [FK_Lecture_Group_Group]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[Lecture_Group]  WITH CHECK ADD  CONSTRAINT [FK_Lecture_Group_Group] FOREIGN KEY([GroupID])
REFERENCES [dbo].[Group] ([ID])
GO
ALTER TABLE [dbo].[Lecture_Group] CHECK CONSTRAINT [FK_Lecture_Group_Group]
GO
/****** Object:  ForeignKey [FK_Lecture_Group_Lecture]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[Lecture_Group]  WITH CHECK ADD  CONSTRAINT [FK_Lecture_Group_Lecture] FOREIGN KEY([LectureID])
REFERENCES [dbo].[Lecture] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Lecture_Group] CHECK CONSTRAINT [FK_Lecture_Group_Lecture]
GO
/****** Object:  ForeignKey [FK_Practice_Teacher_Practice]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[Practice_Teacher]  WITH CHECK ADD  CONSTRAINT [FK_Practice_Teacher_Practice] FOREIGN KEY([PracticeID])
REFERENCES [dbo].[Practice] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Practice_Teacher] CHECK CONSTRAINT [FK_Practice_Teacher_Practice]
GO
/****** Object:  ForeignKey [FK_Practice_Teacher_Teacher]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[Practice_Teacher]  WITH CHECK ADD  CONSTRAINT [FK_Practice_Teacher_Teacher] FOREIGN KEY([TeacherID])
REFERENCES [dbo].[Teacher] ([SystemUserID])
GO
ALTER TABLE [dbo].[Practice_Teacher] CHECK CONSTRAINT [FK_Practice_Teacher_Teacher]
GO
/****** Object:  ForeignKey [FK_LectureControlLectureControlMark]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[LectureControlMark]  WITH CHECK ADD  CONSTRAINT [FK_LectureControlLectureControlMark] FOREIGN KEY([LectureControlID])
REFERENCES [dbo].[LectureControl] ([ControlID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LectureControlMark] CHECK CONSTRAINT [FK_LectureControlLectureControlMark]
GO
/****** Object:  ForeignKey [FK_LectureControlMark_inherits_Mark]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[LectureControlMark]  WITH CHECK ADD  CONSTRAINT [FK_LectureControlMark_inherits_Mark] FOREIGN KEY([MarkID])
REFERENCES [dbo].[Mark] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LectureControlMark] CHECK CONSTRAINT [FK_LectureControlMark_inherits_Mark]
GO
/****** Object:  ForeignKey [FK_PracticeControl_inherits_Control]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[PracticeControl]  WITH CHECK ADD  CONSTRAINT [FK_PracticeControl_inherits_Control] FOREIGN KEY([ControlID])
REFERENCES [dbo].[Control] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PracticeControl] CHECK CONSTRAINT [FK_PracticeControl_inherits_Control]
GO
/****** Object:  ForeignKey [FK_PracticeControl_Practice]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[PracticeControl]  WITH CHECK ADD  CONSTRAINT [FK_PracticeControl_Practice] FOREIGN KEY([PracticeID])
REFERENCES [dbo].[Practice] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PracticeControl] CHECK CONSTRAINT [FK_PracticeControl_Practice]
GO
/****** Object:  ForeignKey [FK_PracticeControl_Practice_Teacher]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[PracticeControl]  WITH CHECK ADD  CONSTRAINT [FK_PracticeControl_Practice_Teacher] FOREIGN KEY([PracticeID])
REFERENCES [dbo].[Practice_Teacher] ([ID])
GO
ALTER TABLE [dbo].[PracticeControl] CHECK CONSTRAINT [FK_PracticeControl_Practice_Teacher]
GO
/****** Object:  ForeignKey [FK_Practice_Teacher_Student_Practice_Teacher]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[Practice_Teacher_Student]  WITH CHECK ADD  CONSTRAINT [FK_Practice_Teacher_Student_Practice_Teacher] FOREIGN KEY([Practice_TeacherID])
REFERENCES [dbo].[Practice_Teacher] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Practice_Teacher_Student] CHECK CONSTRAINT [FK_Practice_Teacher_Student_Practice_Teacher]
GO
/****** Object:  ForeignKey [FK_Practice_Teacher_Student_Student]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[Practice_Teacher_Student]  WITH CHECK ADD  CONSTRAINT [FK_Practice_Teacher_Student_Student] FOREIGN KEY([StudentID])
REFERENCES [dbo].[Student] ([SystemUserID])
GO
ALTER TABLE [dbo].[Practice_Teacher_Student] CHECK CONSTRAINT [FK_Practice_Teacher_Student_Student]
GO
/****** Object:  ForeignKey [FK_PracticeControlMark_inherits_Mark]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[PracticeControlMark]  WITH CHECK ADD  CONSTRAINT [FK_PracticeControlMark_inherits_Mark] FOREIGN KEY([MarkID])
REFERENCES [dbo].[Mark] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PracticeControlMark] CHECK CONSTRAINT [FK_PracticeControlMark_inherits_Mark]
GO
/****** Object:  ForeignKey [FK_PracticeControlPracticeControlMark]    Script Date: 11/24/2013 23:34:59 ******/
ALTER TABLE [dbo].[PracticeControlMark]  WITH CHECK ADD  CONSTRAINT [FK_PracticeControlPracticeControlMark] FOREIGN KEY([PracticeControlID])
REFERENCES [dbo].[PracticeControl] ([ControlID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PracticeControlMark] CHECK CONSTRAINT [FK_PracticeControlPracticeControlMark]
GO
