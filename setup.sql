USE [master]
GO

/****** Object:  Database [PaymentServiceProvider]    Script Date: 11/5/2023 7:20:11 PM ******/
CREATE DATABASE [PaymentServiceProvider]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PaymentServiceProvider', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.RICHTER\MSSQL\DATA\PaymentServiceProvider.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PaymentServiceProvider_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.RICHTER\MSSQL\DATA\PaymentServiceProvider_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

ALTER DATABASE [PaymentServiceProvider] SET COMPATIBILITY_LEVEL = 140
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PaymentServiceProvider].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [PaymentServiceProvider] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [PaymentServiceProvider] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [PaymentServiceProvider] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [PaymentServiceProvider] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [PaymentServiceProvider] SET ARITHABORT OFF 
GO

ALTER DATABASE [PaymentServiceProvider] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [PaymentServiceProvider] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [PaymentServiceProvider] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [PaymentServiceProvider] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [PaymentServiceProvider] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [PaymentServiceProvider] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [PaymentServiceProvider] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [PaymentServiceProvider] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [PaymentServiceProvider] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [PaymentServiceProvider] SET  DISABLE_BROKER 
GO

ALTER DATABASE [PaymentServiceProvider] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [PaymentServiceProvider] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [PaymentServiceProvider] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [PaymentServiceProvider] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [PaymentServiceProvider] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [PaymentServiceProvider] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [PaymentServiceProvider] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [PaymentServiceProvider] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [PaymentServiceProvider] SET  MULTI_USER 
GO

ALTER DATABASE [PaymentServiceProvider] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [PaymentServiceProvider] SET DB_CHAINING OFF 
GO

ALTER DATABASE [PaymentServiceProvider] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [PaymentServiceProvider] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [PaymentServiceProvider] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [PaymentServiceProvider] SET QUERY_STORE = OFF
GO

ALTER DATABASE [PaymentServiceProvider] SET  READ_WRITE 
GO

USE [PaymentServiceProvider]
GO

/****** Object:  Table [dbo].[Payables]    Script Date: 11/5/2023 7:20:46 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Payables](
	[Id] [uniqueidentifier] NOT NULL,
	[Value] [decimal](18, 2) NOT NULL,
	[Status] [tinyint] NOT NULL,
	[PaymentDate] [datetime] NOT NULL,
	[TransactionId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Payables] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Payables]  WITH CHECK ADD  CONSTRAINT [FK_Payables_Transactions] FOREIGN KEY([TransactionId])
REFERENCES [dbo].[Transactions] ([Id])
GO

ALTER TABLE [dbo].[Payables] CHECK CONSTRAINT [FK_Payables_Transactions]
GO


USE [PaymentServiceProvider]
GO

/****** Object:  Table [dbo].[Transactions]    Script Date: 11/5/2023 7:21:07 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Transactions](
	[Id] [uniqueidentifier] NOT NULL,
	[Value] [decimal](18, 2) NOT NULL,
	[ClientId] [uniqueidentifier] NOT NULL,
	[Description] [varchar](100) NOT NULL,
	[PaymentMethodCode] [tinyint] NOT NULL,
	[CardNumber] [varchar](4) NOT NULL,
	[CardHolder] [varchar](255) NOT NULL,
	[CardVerificationCode] [varchar](4) NOT NULL,
	[CardExpirationDate] [date] NOT NULL,
	[CreateAt] [datetime] NOT NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


