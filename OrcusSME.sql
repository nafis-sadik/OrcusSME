USE [master]
GO
/****** Object:  Database [OrcusSME]    Script Date: 2/2/2022 12:43:47 AM ******/
CREATE DATABASE [OrcusSME]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'OrcusSME', FILENAME = N'E:\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\OrcusSME.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'OrcusSME_log', FILENAME = N'E:\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\OrcusSME_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [OrcusSME] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OrcusSME].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OrcusSME] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OrcusSME] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OrcusSME] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OrcusSME] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OrcusSME] SET ARITHABORT OFF 
GO
ALTER DATABASE [OrcusSME] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [OrcusSME] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OrcusSME] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OrcusSME] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OrcusSME] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OrcusSME] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OrcusSME] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OrcusSME] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OrcusSME] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OrcusSME] SET  ENABLE_BROKER 
GO
ALTER DATABASE [OrcusSME] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OrcusSME] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OrcusSME] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OrcusSME] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OrcusSME] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OrcusSME] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [OrcusSME] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OrcusSME] SET RECOVERY FULL 
GO
ALTER DATABASE [OrcusSME] SET  MULTI_USER 
GO
ALTER DATABASE [OrcusSME] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OrcusSME] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OrcusSME] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OrcusSME] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [OrcusSME] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [OrcusSME] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'OrcusSME', N'ON'
GO
ALTER DATABASE [OrcusSME] SET QUERY_STORE = OFF
GO
USE [OrcusSME]
GO
/****** Object:  Table [dbo].[ActivityTypes]    Script Date: 2/2/2022 12:43:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivityTypes](
	[ActivityTypeId] [decimal](18, 0) NOT NULL,
	[ActivityName] [varchar](50) NULL,
 CONSTRAINT [PK_ActivityTypes] PRIMARY KEY CLUSTERED 
(
	[ActivityTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Addresses]    Script Date: 2/2/2022 12:43:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Addresses](
	[AddressId] [int] NOT NULL,
	[UserId] [varchar](50) NOT NULL,
	[StreetAddress] [varchar](50) NULL,
	[GoogleMapsLocation] [text] NULL,
	[LocationLabel] [varchar](50) NULL,
	[Status] [char](1) NOT NULL,
 CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED 
(
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 2/2/2022 12:43:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] NOT NULL,
	[CategoryName] [varchar](50) NULL,
	[ParentCategoryId] [int] NULL,
	[OutletId] [decimal](18, 0) NULL,
	[Status] [char](1) NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CommonCodes]    Script Date: 2/2/2022 12:43:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommonCodes](
	[CommonCodeId] [int] NOT NULL,
	[CommonCodeName] [nchar](10) NULL,
 CONSTRAINT [PK_CommonCodes] PRIMARY KEY CLUSTERED 
(
	[CommonCodeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContactNumbers]    Script Date: 2/2/2022 12:43:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactNumbers](
	[UserId] [varchar](50) NOT NULL,
	[Number] [varchar](20) NOT NULL,
	[IsBkash] [char](1) NULL,
	[IsNagad] [char](1) NULL,
	[IsRocket] [char](1) NULL,
	[Status] [char](1) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Crashlog]    Script Date: 2/2/2022 12:43:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Crashlog](
	[CrashLogId] [int] NOT NULL,
	[ClassName] [varchar](20) NOT NULL,
	[MethodName] [varchar](20) NOT NULL,
	[ErrorMessage] [text] NULL,
	[ErrorInner] [text] NULL,
	[Data] [text] NULL,
	[TimeStamp] [datetime] NOT NULL,
 CONSTRAINT [PK_Crashlog] PRIMARY KEY CLUSTERED 
(
	[CrashLogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmailAddresses]    Script Date: 2/2/2022 12:43:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailAddresses](
	[EMailPk] [int] NOT NULL,
	[UserId] [varchar](50) NOT NULL,
	[IsPrimaryMail] [char](1) NULL,
	[Status] [char](1) NULL,
	[EmailAddress] [nvarchar](50) NULL,
 CONSTRAINT [PK__EmailAdd__41EF875392D05F62] PRIMARY KEY CLUSTERED 
(
	[EMailPk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Images]    Script Date: 2/2/2022 12:43:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Images](
	[ImageId] [int] NOT NULL,
	[TableName] [nvarchar](max) NOT NULL,
	[FK_Id] [int] NOT NULL,
	[StorageLocation] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED 
(
	[ImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InventoryLog]    Script Date: 2/2/2022 12:43:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InventoryLog](
	[InventoryLogId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[InventoryUpdateType] [nvarchar](50) NOT NULL,
	[ActivityDate] [datetime] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] [float] NOT NULL,
 CONSTRAINT [PK_InventoryLog] PRIMARY KEY CLUSTERED 
(
	[InventoryLogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Outlets]    Script Date: 2/2/2022 12:43:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Outlets](
	[OutletId] [decimal](18, 0) NOT NULL,
	[OutletName] [varchar](50) NULL,
	[OutletAddresss] [text] NULL,
	[EComURL] [text] NULL,
	[UserId] [varchar](50) NOT NULL,
	[RequestSite] [tinyint] NULL,
	[SiteUrl] [text] NULL,
	[Status] [char](1) NOT NULL,
 CONSTRAINT [PK_Outlets] PRIMARY KEY CLUSTERED 
(
	[OutletId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductAttributes]    Script Date: 2/2/2022 12:43:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductAttributes](
	[AttributeId] [int] NOT NULL,
	[AttributeValues] [nchar](10) NOT NULL,
	[AttributeTypes] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
 CONSTRAINT [PK_ProductAttributes] PRIMARY KEY CLUSTERED 
(
	[AttributeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 2/2/2022 12:43:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] NOT NULL,
	[ProductName] [nvarchar](120) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[RetailPrice] [float] NOT NULL,
	[ShortDescription] [nchar](10) NULL,
	[Specifications] [nvarchar](max) NULL,
	[Quantity] [int] NULL,
	[UnitTypeId] [int] NOT NULL,
	[Status] [char](1) NULL,
	[PurchasingPrice] [int] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductUnitTypes]    Script Date: 2/2/2022 12:43:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductUnitTypes](
	[UnitTypeIds] [int] NOT NULL,
	[UnitTypeNames] [char](200) NULL,
	[Status] [char](1) NULL,
 CONSTRAINT [PK__ProductU__5085C454884EB7EB] PRIMARY KEY CLUSTERED 
(
	[UnitTypeIds] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Services]    Script Date: 2/2/2022 12:43:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Services](
	[SubscriptionId] [decimal](18, 0) NOT NULL,
	[SubscriptionName] [char](50) NULL,
	[SubscriptionPrice] [decimal](18, 0) NULL,
	[DurationMonths] [decimal](18, 0) NULL,
 CONSTRAINT [PK__Services__9A2B249DF26AE971] PRIMARY KEY CLUSTERED 
(
	[SubscriptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubscriptionLog]    Script Date: 2/2/2022 12:43:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubscriptionLog](
	[Subscription] [char](10) NULL,
	[UserId] [varchar](50) NULL,
	[SubscriptionId] [decimal](18, 0) NULL,
	[SubscriptionDate] [datetime] NULL,
	[ExpirationDate] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserActivityLog]    Script Date: 2/2/2022 12:43:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserActivityLog](
	[ActivityLogIn] [int] NOT NULL,
	[UserId] [varchar](50) NOT NULL,
	[ActivityTypeId] [decimal](18, 0) NULL,
	[Remarks] [varchar](50) NULL,
	[ActivityDate] [datetime] NULL,
	[IPAddress] [char](15) NULL,
	[Browser] [char](10) NULL,
	[OS] [char](10) NULL,
	[Misc] [text] NULL,
 CONSTRAINT [PK__UserActi__19A9B7B9B06B38FA] PRIMARY KEY CLUSTERED 
(
	[ActivityLogIn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2/2/2022 12:43:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [varchar](50) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[MiddleName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[ProfilePicLoc] [text] NULL,
	[Status] [char](1) NOT NULL,
	[Password] [text] NOT NULL,
	[AccountBalance] [decimal](18, 0) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Addresses_UserId]    Script Date: 2/2/2022 12:43:47 AM ******/
CREATE NONCLUSTERED INDEX [IX_Addresses_UserId] ON [dbo].[Addresses]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Categories_OutletId]    Script Date: 2/2/2022 12:43:47 AM ******/
CREATE NONCLUSTERED INDEX [IX_Categories_OutletId] ON [dbo].[Categories]
(
	[OutletId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ContactNumbers_UserId]    Script Date: 2/2/2022 12:43:47 AM ******/
CREATE NONCLUSTERED INDEX [IX_ContactNumbers_UserId] ON [dbo].[ContactNumbers]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_EmailAddresses_UserId]    Script Date: 2/2/2022 12:43:47 AM ******/
CREATE NONCLUSTERED INDEX [IX_EmailAddresses_UserId] ON [dbo].[EmailAddresses]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Outlets_UserId]    Script Date: 2/2/2022 12:43:47 AM ******/
CREATE NONCLUSTERED INDEX [IX_Outlets_UserId] ON [dbo].[Outlets]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductAttributes_AttributeTypes]    Script Date: 2/2/2022 12:43:47 AM ******/
CREATE NONCLUSTERED INDEX [IX_ProductAttributes_AttributeTypes] ON [dbo].[ProductAttributes]
(
	[AttributeTypes] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductAttributes_ProductId]    Script Date: 2/2/2022 12:43:47 AM ******/
CREATE NONCLUSTERED INDEX [IX_ProductAttributes_ProductId] ON [dbo].[ProductAttributes]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Products_CategoryId]    Script Date: 2/2/2022 12:43:47 AM ******/
CREATE NONCLUSTERED INDEX [IX_Products_CategoryId] ON [dbo].[Products]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SubscriptionLog_SubscriptionId]    Script Date: 2/2/2022 12:43:47 AM ******/
CREATE NONCLUSTERED INDEX [IX_SubscriptionLog_SubscriptionId] ON [dbo].[SubscriptionLog]
(
	[SubscriptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_SubscriptionLog_UserId]    Script Date: 2/2/2022 12:43:47 AM ******/
CREATE NONCLUSTERED INDEX [IX_SubscriptionLog_UserId] ON [dbo].[SubscriptionLog]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserActivityLog_ActivityTypeId]    Script Date: 2/2/2022 12:43:47 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserActivityLog_ActivityTypeId] ON [dbo].[UserActivityLog]
(
	[ActivityTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserActivityLog_UserId]    Script Date: 2/2/2022 12:43:47 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserActivityLog_UserId] ON [dbo].[UserActivityLog]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [FK_Addresses_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [FK_Addresses_User]
GO
ALTER TABLE [dbo].[Categories]  WITH CHECK ADD  CONSTRAINT [FK_Categories_Outlets] FOREIGN KEY([OutletId])
REFERENCES [dbo].[Outlets] ([OutletId])
GO
ALTER TABLE [dbo].[Categories] CHECK CONSTRAINT [FK_Categories_Outlets]
GO
ALTER TABLE [dbo].[ContactNumbers]  WITH CHECK ADD  CONSTRAINT [FK_Numbers_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[ContactNumbers] CHECK CONSTRAINT [FK_Numbers_User]
GO
ALTER TABLE [dbo].[EmailAddresses]  WITH CHECK ADD  CONSTRAINT [FK_EmailId_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[EmailAddresses] CHECK CONSTRAINT [FK_EmailId_User]
GO
ALTER TABLE [dbo].[InventoryLog]  WITH CHECK ADD  CONSTRAINT [FK_InventoryLog_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[InventoryLog] CHECK CONSTRAINT [FK_InventoryLog_Products]
GO
ALTER TABLE [dbo].[Outlets]  WITH CHECK ADD  CONSTRAINT [FK_Outlets_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Outlets] CHECK CONSTRAINT [FK_Outlets_User]
GO
ALTER TABLE [dbo].[ProductAttributes]  WITH CHECK ADD  CONSTRAINT [FK_ProductAttributes_CommonCodes] FOREIGN KEY([AttributeTypes])
REFERENCES [dbo].[CommonCodes] ([CommonCodeId])
GO
ALTER TABLE [dbo].[ProductAttributes] CHECK CONSTRAINT [FK_ProductAttributes_CommonCodes]
GO
ALTER TABLE [dbo].[ProductAttributes]  WITH CHECK ADD  CONSTRAINT [FK_ProductAttributes_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[ProductAttributes] CHECK CONSTRAINT [FK_ProductAttributes_Products]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_ProductUnitTypes] FOREIGN KEY([UnitTypeId])
REFERENCES [dbo].[ProductUnitTypes] ([UnitTypeIds])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_ProductUnitTypes]
GO
ALTER TABLE [dbo].[SubscriptionLog]  WITH CHECK ADD  CONSTRAINT [FK_SubscriptionLog_Subscriptions1] FOREIGN KEY([SubscriptionId])
REFERENCES [dbo].[Services] ([SubscriptionId])
GO
ALTER TABLE [dbo].[SubscriptionLog] CHECK CONSTRAINT [FK_SubscriptionLog_Subscriptions1]
GO
ALTER TABLE [dbo].[SubscriptionLog]  WITH CHECK ADD  CONSTRAINT [FK_SubscriptionLog_User1] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[SubscriptionLog] CHECK CONSTRAINT [FK_SubscriptionLog_User1]
GO
ALTER TABLE [dbo].[UserActivityLog]  WITH CHECK ADD  CONSTRAINT [FK_UserActivityLog_ActivityTypes] FOREIGN KEY([ActivityTypeId])
REFERENCES [dbo].[ActivityTypes] ([ActivityTypeId])
GO
ALTER TABLE [dbo].[UserActivityLog] CHECK CONSTRAINT [FK_UserActivityLog_ActivityTypes]
GO
ALTER TABLE [dbo].[UserActivityLog]  WITH CHECK ADD  CONSTRAINT [FK_UserActivityLog_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserActivityLog] CHECK CONSTRAINT [FK_UserActivityLog_User]
GO
USE [master]
GO
ALTER DATABASE [OrcusSME] SET  READ_WRITE 
GO
