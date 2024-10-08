USE [master]
GO
/****** Object:  Database [SchoolRules]    Script Date: 8/28/2024 3:34:33 PM ******/
CREATE DATABASE [SchoolRules]
GO
ALTER DATABASE [SchoolRules] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SchoolRules].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SchoolRules] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SchoolRules] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SchoolRules] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SchoolRules] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SchoolRules] SET ARITHABORT OFF 
GO
ALTER DATABASE [SchoolRules] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SchoolRules] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SchoolRules] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SchoolRules] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SchoolRules] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SchoolRules] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SchoolRules] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SchoolRules] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SchoolRules] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SchoolRules] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SchoolRules] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SchoolRules] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SchoolRules] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SchoolRules] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SchoolRules] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SchoolRules] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SchoolRules] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SchoolRules] SET RECOVERY FULL 
GO
ALTER DATABASE [SchoolRules] SET  MULTI_USER 
GO
ALTER DATABASE [SchoolRules] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SchoolRules] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SchoolRules] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SchoolRules] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SchoolRules] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SchoolRules] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'SchoolRules', N'ON'
GO
ALTER DATABASE [SchoolRules] SET QUERY_STORE = OFF
GO
USE [SchoolRules]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 8/28/2024 3:34:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin](
	[AdminID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[Phone] [nvarchar](20) NULL,
	[RoleID] [tinyint] NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[AdminID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Class]    Script Date: 8/28/2024 3:34:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Class](
	[ClassID] [int] IDENTITY(1,1) NOT NULL,
	[SchoolYearID] [int] NOT NULL,
	[ClassGroupID] [int] NOT NULL,
	[TeacherID] [int] NULL,
	[Code] [nvarchar](50) NULL,
	[Grade] [int] NOT NULL,
	[Name] [nvarchar](70) NOT NULL,
	[TotalPoint] [int] NOT NULL,
	[Status] [nvarchar](50) NULL,
 CONSTRAINT [PK_Class] PRIMARY KEY CLUSTERED 
(
	[ClassID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClassGroup]    Script Date: 8/28/2024 3:34:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassGroup](
	[ClassGroupID] [int] IDENTITY(1,1) NOT NULL,
	[SchoolID] [int] NOT NULL,
	[TeacherID] [int] NULL,
	[Name] [nvarchar](70) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ClassGroup] PRIMARY KEY CLUSTERED 
(
	[ClassGroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Discipline]    Script Date: 8/28/2024 3:34:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Discipline](
	[DisciplineID] [int] IDENTITY(1,1) NOT NULL,
	[ViolationID] [int] NOT NULL,
	[PennaltyID] [int] NULL,
	[Description] [nvarchar](255) NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[Status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Discipline] PRIMARY KEY CLUSTERED 
(
	[DisciplineID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Evaluation]    Script Date: 8/28/2024 3:34:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Evaluation](
	[EvaluationID] [int] IDENTITY(1,1) NOT NULL,
	[ClassID] [int] NULL,
	[Description] [nvarchar](500) NULL,
	[From] [date] NOT NULL,
	[To] [date] NOT NULL,
	[Points] [int] NULL,
	[Status] [nvarchar](50) NULL,
 CONSTRAINT [PK_Evaluation] PRIMARY KEY CLUSTERED 
(
	[EvaluationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HighSchool]    Script Date: 8/28/2024 3:34:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HighSchool](
	[SchoolID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NULL,
	[Name] [nvarchar](100) NOT NULL,
	[City] [nvarchar](50) NULL,
	[Address] [nvarchar](300) NULL,
	[Phone] [nvarchar](12) NULL,
	[WebURL] [nvarchar](500) NULL,
 CONSTRAINT [PK_HighSchool] PRIMARY KEY CLUSTERED 
(
	[SchoolID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ImageURL]    Script Date: 8/28/2024 3:34:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImageURL](
	[ImageURLID] [int] IDENTITY(1,1) NOT NULL,
	[ViolationID] [int] NOT NULL,
	[PublicID] [nvarchar](50) NULL,
	[URL] [nvarchar](500) NULL,
	[Name] [nchar](100) NULL,
	[Description] [nvarchar](200) NULL,
 CONSTRAINT [PK_ImageURL] PRIMARY KEY CLUSTERED 
(
	[ImageURLID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 8/28/2024 3:34:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[PackageID] [int] NOT NULL,
	[OrderCode] [int] NOT NULL,
	[Description] [nvarchar](200) NULL,
	[Total] [int] NOT NULL,
	[AmountPaid] [int] NULL,
	[AmountRemaining] [int] NULL,
	[CounterAccountBankName] [nvarchar](100) NULL,
	[CounterAccountNumber] [nvarchar](50) NULL,
	[CounterAccountName] [nvarchar](100) NULL,
	[Date] [datetime2](7) NULL,
	[Status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Package]    Script Date: 8/28/2024 3:34:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Package](
	[PackageID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[Price] [int] NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Package] PRIMARY KEY CLUSTERED 
(
	[PackageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PatrolSchedule]    Script Date: 8/28/2024 3:34:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatrolSchedule](
	[ScheduleID] [int] IDENTITY(1,1) NOT NULL,
	[ClassID] [int] NOT NULL,
	[UserID] [int] NULL,
	[SupervisorID] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Slot] [int] NULL,
	[Time] [time](0) NULL,
	[From] [date] NOT NULL,
	[To] [date] NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PatrolSchedule] PRIMARY KEY CLUSTERED 
(
	[ScheduleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Penalty]    Script Date: 8/28/2024 3:34:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Penalty](
	[PenaltyID] [int] IDENTITY(1,1) NOT NULL,
	[SchoolID] [int] NOT NULL,
	[Code] [nvarchar](50) NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[Level] [int] NULL,
	[Status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ActivityType] PRIMARY KEY CLUSTERED 
(
	[PenaltyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RegisteredSchool]    Script Date: 8/28/2024 3:34:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegisteredSchool](
	[RegisteredID] [int] IDENTITY(1,1) NOT NULL,
	[SchoolID] [int] NOT NULL,
	[RegisteredDate] [date] NOT NULL,
	[Description] [nvarchar](255) NULL,
	[Status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_RegisteredSchool] PRIMARY KEY CLUSTERED 
(
	[RegisteredID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 8/28/2024 3:34:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleID] [tinyint] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SchoolYear]    Script Date: 8/28/2024 3:34:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SchoolYear](
	[SchoolYearID] [int] IDENTITY(1,1) NOT NULL,
	[SchoolID] [int] NOT NULL,
	[Year] [smallint] NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_SchoolYear] PRIMARY KEY CLUSTERED 
(
	[SchoolYearID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Semester]    Script Date: 8/28/2024 3:34:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Semester](
	[SemesterID] [int] IDENTITY(1,1) NOT NULL,
	[SchoolYearID] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
 CONSTRAINT [PK_Semester] PRIMARY KEY CLUSTERED 
(
	[SemesterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 8/28/2024 3:34:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[StudentID] [int] IDENTITY(1,1) NOT NULL,
	[SchoolID] [int] NOT NULL,
	[Code] [nvarchar](50) NULL,
	[Name] [nvarchar](50) NULL,
	[Sex] [bit] NULL,
	[Birthday] [date] NULL,
	[Address] [nvarchar](255) NULL,
	[Phone] [nvarchar](12) NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[StudentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentInClass]    Script Date: 8/28/2024 3:34:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentInClass](
	[StudentInClassID] [int] IDENTITY(1,1) NOT NULL,
	[ClassID] [int] NOT NULL,
	[StudentID] [int] NOT NULL,
	[EnrollDate] [date] NULL,
	[IsSupervisor] [bit] NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[NumberOfViolation] [int] NULL,
	[Status] [nvarchar](50) NULL,
 CONSTRAINT [PK_StudentInClass] PRIMARY KEY CLUSTERED 
(
	[StudentInClassID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentSupervisor]    Script Date: 8/28/2024 3:34:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentSupervisor](
	[StudentSupervisorID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[StudentInClassID] [int] NOT NULL,
	[Description] [nvarchar](100) NULL,
 CONSTRAINT [PK_StudentSupervisor] PRIMARY KEY CLUSTERED 
(
	[StudentSupervisorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teacher]    Script Date: 8/28/2024 3:34:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teacher](
	[TeacherID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[SchoolID] [int] NOT NULL,
	[Sex] [bit] NOT NULL,
 CONSTRAINT [PK_Teacher] PRIMARY KEY CLUSTERED 
(
	[TeacherID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 8/28/2024 3:34:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[RoleID] [tinyint] NOT NULL,
	[SchoolID] [int] NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](70) NOT NULL,
	[Phone] [nvarchar](20) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[Address] [nvarchar](255) NULL,
	[Status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Entity] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Violation]    Script Date: 8/28/2024 3:34:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Violation](
	[ViolationID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[ClassID] [int] NOT NULL,
	[ViolationTypeID] [int] NOT NULL,
	[StudentInClassID] [int] NULL,
	[ScheduleID] [int] NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[Date] [datetime] NOT NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
	[Status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Violation] PRIMARY KEY CLUSTERED 
(
	[ViolationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ViolationConfig]    Script Date: 8/28/2024 3:34:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ViolationConfig](
	[ViolationConfigID] [int] IDENTITY(1,1) NOT NULL,
	[ViolationTypeID] [int] NOT NULL,
	[MinusPoints] [int] NULL,
	[Description] [nvarchar](200) NULL,
	[Status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ViolationConfig] PRIMARY KEY CLUSTERED 
(
	[ViolationConfigID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[ViolationTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ViolationGroup]    Script Date: 8/28/2024 3:34:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ViolationGroup](
	[ViolationGroupID] [int] IDENTITY(1,1) NOT NULL,
	[SchoolID] [int] NULL,
	[Code] [nvarchar](50) NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[Status] [nvarchar](50) NULL,
 CONSTRAINT [PK_ViolationGroup] PRIMARY KEY CLUSTERED 
(
	[ViolationGroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ViolationType]    Script Date: 8/28/2024 3:34:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ViolationType](
	[ViolationTypeID] [int] IDENTITY(1,1) NOT NULL,
	[ViolationGroupID] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[IsSupervisorOnly] [bit] NOT NULL,
	[Description] [nvarchar](500) NULL,
	[Status] [nvarchar](50) NULL,
 CONSTRAINT [PK_ViolationType] PRIMARY KEY CLUSTERED 
(
	[ViolationTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[YearPackage]    Script Date: 8/28/2024 3:34:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[YearPackage](
	[YearPackageID] [int] IDENTITY(1,1) NOT NULL,
	[SchoolYearID] [int] NOT NULL,
	[PackageID] [int] NOT NULL,
	[Status] [nvarchar](50) NULL,
 CONSTRAINT [PK_YearPackage] PRIMARY KEY CLUSTERED 
(
	[YearPackageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Admin]  WITH CHECK ADD  CONSTRAINT [FK_Admin_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([RoleID])
GO
ALTER TABLE [dbo].[Admin] CHECK CONSTRAINT [FK_Admin_Role]
GO
ALTER TABLE [dbo].[Class]  WITH CHECK ADD  CONSTRAINT [FK_Class_ClassGroup] FOREIGN KEY([ClassGroupID])
REFERENCES [dbo].[ClassGroup] ([ClassGroupID])
GO
ALTER TABLE [dbo].[Class] CHECK CONSTRAINT [FK_Class_ClassGroup]
GO
ALTER TABLE [dbo].[Class]  WITH CHECK ADD  CONSTRAINT [FK_Class_SchoolYear] FOREIGN KEY([SchoolYearID])
REFERENCES [dbo].[SchoolYear] ([SchoolYearID])
GO
ALTER TABLE [dbo].[Class] CHECK CONSTRAINT [FK_Class_SchoolYear]
GO
ALTER TABLE [dbo].[ClassGroup]  WITH CHECK ADD  CONSTRAINT [FK_ClassGroup_HighSchool] FOREIGN KEY([SchoolID])
REFERENCES [dbo].[HighSchool] ([SchoolID])
GO
ALTER TABLE [dbo].[ClassGroup] CHECK CONSTRAINT [FK_ClassGroup_HighSchool]
GO
ALTER TABLE [dbo].[ClassGroup]  WITH CHECK ADD  CONSTRAINT [FK_ClassGroup_Teacher] FOREIGN KEY([TeacherID])
REFERENCES [dbo].[Teacher] ([TeacherID])
GO
ALTER TABLE [dbo].[ClassGroup] CHECK CONSTRAINT [FK_ClassGroup_Teacher]
GO
ALTER TABLE [dbo].[Discipline]  WITH CHECK ADD  CONSTRAINT [FK_Discipline_Penalty] FOREIGN KEY([PennaltyID])
REFERENCES [dbo].[Penalty] ([PenaltyID])
GO
ALTER TABLE [dbo].[Discipline] CHECK CONSTRAINT [FK_Discipline_Penalty]
GO
ALTER TABLE [dbo].[Discipline]  WITH CHECK ADD  CONSTRAINT [FK_Discipline_Violation] FOREIGN KEY([ViolationID])
REFERENCES [dbo].[Violation] ([ViolationID])
GO
ALTER TABLE [dbo].[Discipline] CHECK CONSTRAINT [FK_Discipline_Violation]
GO
ALTER TABLE [dbo].[Evaluation]  WITH CHECK ADD  CONSTRAINT [FK_Evaluation_Class] FOREIGN KEY([ClassID])
REFERENCES [dbo].[Class] ([ClassID])
GO
ALTER TABLE [dbo].[Evaluation] CHECK CONSTRAINT [FK_Evaluation_Class]
GO
ALTER TABLE [dbo].[ImageURL]  WITH CHECK ADD  CONSTRAINT [FK_ImageURL_Violation] FOREIGN KEY([ViolationID])
REFERENCES [dbo].[Violation] ([ViolationID])
GO
ALTER TABLE [dbo].[ImageURL] CHECK CONSTRAINT [FK_ImageURL_Violation]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Package] FOREIGN KEY([PackageID])
REFERENCES [dbo].[Package] ([PackageID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Package]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_User]
GO
ALTER TABLE [dbo].[PatrolSchedule]  WITH CHECK ADD  CONSTRAINT [FK_PatrolSchedule_Class] FOREIGN KEY([ClassID])
REFERENCES [dbo].[Class] ([ClassID])
GO
ALTER TABLE [dbo].[PatrolSchedule] CHECK CONSTRAINT [FK_PatrolSchedule_Class]
GO
ALTER TABLE [dbo].[PatrolSchedule]  WITH CHECK ADD  CONSTRAINT [FK_PatrolSchedule_StudentSupervisor1] FOREIGN KEY([SupervisorID])
REFERENCES [dbo].[StudentSupervisor] ([StudentSupervisorID])
GO
ALTER TABLE [dbo].[PatrolSchedule] CHECK CONSTRAINT [FK_PatrolSchedule_StudentSupervisor1]
GO
ALTER TABLE [dbo].[PatrolSchedule]  WITH CHECK ADD  CONSTRAINT [FK_PatrolSchedule_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[PatrolSchedule] CHECK CONSTRAINT [FK_PatrolSchedule_User]
GO
ALTER TABLE [dbo].[Penalty]  WITH CHECK ADD  CONSTRAINT [FK_Penalty_HighSchool] FOREIGN KEY([SchoolID])
REFERENCES [dbo].[HighSchool] ([SchoolID])
GO
ALTER TABLE [dbo].[Penalty] CHECK CONSTRAINT [FK_Penalty_HighSchool]
GO
ALTER TABLE [dbo].[RegisteredSchool]  WITH CHECK ADD  CONSTRAINT [FK_RegisteredSchool_HighSchool] FOREIGN KEY([SchoolID])
REFERENCES [dbo].[HighSchool] ([SchoolID])
GO
ALTER TABLE [dbo].[RegisteredSchool] CHECK CONSTRAINT [FK_RegisteredSchool_HighSchool]
GO
ALTER TABLE [dbo].[SchoolYear]  WITH CHECK ADD  CONSTRAINT [FK_SchoolYear_HighSchool] FOREIGN KEY([SchoolID])
REFERENCES [dbo].[HighSchool] ([SchoolID])
GO
ALTER TABLE [dbo].[SchoolYear] CHECK CONSTRAINT [FK_SchoolYear_HighSchool]
GO
ALTER TABLE [dbo].[Semester]  WITH CHECK ADD  CONSTRAINT [FK_Semester_SchoolYear] FOREIGN KEY([SchoolYearID])
REFERENCES [dbo].[SchoolYear] ([SchoolYearID])
GO
ALTER TABLE [dbo].[Semester] CHECK CONSTRAINT [FK_Semester_SchoolYear]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_HighSchool] FOREIGN KEY([SchoolID])
REFERENCES [dbo].[HighSchool] ([SchoolID])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_HighSchool]
GO
ALTER TABLE [dbo].[StudentInClass]  WITH CHECK ADD  CONSTRAINT [FK_StudentInClass_Class] FOREIGN KEY([ClassID])
REFERENCES [dbo].[Class] ([ClassID])
GO
ALTER TABLE [dbo].[StudentInClass] CHECK CONSTRAINT [FK_StudentInClass_Class]
GO
ALTER TABLE [dbo].[StudentInClass]  WITH CHECK ADD  CONSTRAINT [FK_StudentInClass_Student] FOREIGN KEY([StudentID])
REFERENCES [dbo].[Student] ([StudentID])
GO
ALTER TABLE [dbo].[StudentInClass] CHECK CONSTRAINT [FK_StudentInClass_Student]
GO
ALTER TABLE [dbo].[StudentSupervisor]  WITH CHECK ADD  CONSTRAINT [FK_StudentSupervisor_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[StudentSupervisor] CHECK CONSTRAINT [FK_StudentSupervisor_User]
GO
ALTER TABLE [dbo].[Teacher]  WITH CHECK ADD  CONSTRAINT [FK_Teacher_HighSchool] FOREIGN KEY([SchoolID])
REFERENCES [dbo].[HighSchool] ([SchoolID])
GO
ALTER TABLE [dbo].[Teacher] CHECK CONSTRAINT [FK_Teacher_HighSchool]
GO
ALTER TABLE [dbo].[Teacher]  WITH CHECK ADD  CONSTRAINT [FK_Teacher_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Teacher] CHECK CONSTRAINT [FK_Teacher_User]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_HighSchool] FOREIGN KEY([SchoolID])
REFERENCES [dbo].[HighSchool] ([SchoolID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_HighSchool]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([RoleID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
ALTER TABLE [dbo].[Violation]  WITH CHECK ADD  CONSTRAINT [FK_Violation_Class] FOREIGN KEY([ClassID])
REFERENCES [dbo].[Class] ([ClassID])
GO
ALTER TABLE [dbo].[Violation] CHECK CONSTRAINT [FK_Violation_Class]
GO
ALTER TABLE [dbo].[Violation]  WITH CHECK ADD  CONSTRAINT [FK_Violation_PatrolSchedule] FOREIGN KEY([ScheduleID])
REFERENCES [dbo].[PatrolSchedule] ([ScheduleID])
GO
ALTER TABLE [dbo].[Violation] CHECK CONSTRAINT [FK_Violation_PatrolSchedule]
GO
ALTER TABLE [dbo].[Violation]  WITH CHECK ADD  CONSTRAINT [FK_Violation_StudentInClass] FOREIGN KEY([StudentInClassID])
REFERENCES [dbo].[StudentInClass] ([StudentInClassID])
GO
ALTER TABLE [dbo].[Violation] CHECK CONSTRAINT [FK_Violation_StudentInClass]
GO
ALTER TABLE [dbo].[Violation]  WITH CHECK ADD  CONSTRAINT [FK_Violation_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Violation] CHECK CONSTRAINT [FK_Violation_User]
GO
ALTER TABLE [dbo].[Violation]  WITH CHECK ADD  CONSTRAINT [FK_Violation_ViolationType] FOREIGN KEY([ViolationTypeID])
REFERENCES [dbo].[ViolationType] ([ViolationTypeID])
GO
ALTER TABLE [dbo].[Violation] CHECK CONSTRAINT [FK_Violation_ViolationType]
GO
ALTER TABLE [dbo].[ViolationConfig]  WITH CHECK ADD  CONSTRAINT [FK_ViolationConfig_ViolationType] FOREIGN KEY([ViolationTypeID])
REFERENCES [dbo].[ViolationType] ([ViolationTypeID])
GO
ALTER TABLE [dbo].[ViolationConfig] CHECK CONSTRAINT [FK_ViolationConfig_ViolationType]
GO
ALTER TABLE [dbo].[ViolationGroup]  WITH CHECK ADD  CONSTRAINT [FK_ViolationGroup_HighSchool] FOREIGN KEY([SchoolID])
REFERENCES [dbo].[HighSchool] ([SchoolID])
GO
ALTER TABLE [dbo].[ViolationGroup] CHECK CONSTRAINT [FK_ViolationGroup_HighSchool]
GO
ALTER TABLE [dbo].[ViolationType]  WITH CHECK ADD  CONSTRAINT [FK_ViolationType_ViolationGroup] FOREIGN KEY([ViolationGroupID])
REFERENCES [dbo].[ViolationGroup] ([ViolationGroupID])
GO
ALTER TABLE [dbo].[ViolationType] CHECK CONSTRAINT [FK_ViolationType_ViolationGroup]
GO
ALTER TABLE [dbo].[YearPackage]  WITH CHECK ADD  CONSTRAINT [FK_YearPackage_Package] FOREIGN KEY([PackageID])
REFERENCES [dbo].[Package] ([PackageID])
GO
ALTER TABLE [dbo].[YearPackage] CHECK CONSTRAINT [FK_YearPackage_Package]
GO
ALTER TABLE [dbo].[YearPackage]  WITH CHECK ADD  CONSTRAINT [FK_YearPackage_SchoolYear] FOREIGN KEY([SchoolYearID])
REFERENCES [dbo].[SchoolYear] ([SchoolYearID])
GO
ALTER TABLE [dbo].[YearPackage] CHECK CONSTRAINT [FK_YearPackage_SchoolYear]
GO
USE [master]
GO
ALTER DATABASE [SchoolRules] SET  READ_WRITE 
GO
