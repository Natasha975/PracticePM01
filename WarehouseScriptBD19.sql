USE [master]
GO
/****** Object:  Database [Warehouse]    Script Date: 26.02.2025 13:37:49 ******/
CREATE DATABASE [Warehouse]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Warehouse', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\Warehouse.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Warehouse_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\Warehouse_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Warehouse] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Warehouse].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Warehouse] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Warehouse] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Warehouse] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Warehouse] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Warehouse] SET ARITHABORT OFF 
GO
ALTER DATABASE [Warehouse] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Warehouse] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Warehouse] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Warehouse] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Warehouse] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Warehouse] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Warehouse] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Warehouse] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Warehouse] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Warehouse] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Warehouse] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Warehouse] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Warehouse] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Warehouse] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Warehouse] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Warehouse] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Warehouse] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Warehouse] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Warehouse] SET  MULTI_USER 
GO
ALTER DATABASE [Warehouse] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Warehouse] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Warehouse] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Warehouse] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Warehouse] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Warehouse] SET QUERY_STORE = OFF
GO
USE [Warehouse]
GO
/****** Object:  Table [dbo].[Адрес]    Script Date: 26.02.2025 13:37:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Адрес](
	[Номер] [int] IDENTITY(1,1) NOT NULL,
	[Страна] [nvarchar](50) NOT NULL,
	[Субъект] [nvarchar](max) NULL,
	[Город] [nvarchar](max) NOT NULL,
	[Улица] [nvarchar](max) NOT NULL,
	[Дом] [int] NOT NULL,
 CONSTRAINT [PK_Адрес] PRIMARY KEY CLUSTERED 
(
	[Номер] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ЗоныХранения]    Script Date: 26.02.2025 13:37:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ЗоныХранения](
	[Номер] [int] IDENTITY(1,1) NOT NULL,
	[Наименование] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_ЗоныХранения] PRIMARY KEY CLUSTERED 
(
	[Номер] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Инвентаризация]    Script Date: 26.02.2025 13:37:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Инвентаризация](
	[Номер] [int] IDENTITY(1,1) NOT NULL,
	[ДатаПроведения] [date] NOT NULL,
	[Ответственный] [int] NOT NULL,
	[Результаты] [nvarchar](max) NULL,
 CONSTRAINT [PK_Инвентаризация] PRIMARY KEY CLUSTERED 
(
	[Номер] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Клиент]    Script Date: 26.02.2025 13:37:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Клиент](
	[Номер] [int] IDENTITY(1,1) NOT NULL,
	[Название] [nvarchar](max) NOT NULL,
	[Tелефон] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Адрес] [int] NOT NULL,
 CONSTRAINT [PK_Клиент] PRIMARY KEY CLUSTERED 
(
	[Номер] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Пользователь]    Script Date: 26.02.2025 13:37:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Пользователь](
	[Номер] [int] IDENTITY(1,1) NOT NULL,
	[Фамилия] [nvarchar](max) NOT NULL,
	[Имя] [nvarchar](50) NOT NULL,
	[Отчество] [nvarchar](max) NULL,
	[Роль] [int] NOT NULL,
	[Логин] [nvarchar](max) NOT NULL,
	[Пароль] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Пользователь] PRIMARY KEY CLUSTERED 
(
	[Номер] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Поставщики]    Script Date: 26.02.2025 13:37:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Поставщики](
	[Номер] [int] IDENTITY(1,1) NOT NULL,
	[Название] [nvarchar](max) NOT NULL,
	[ИННКПП] [char](12) NOT NULL,
	[Tелефон] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Адрес] [int] NOT NULL,
 CONSTRAINT [PK_Поставщики] PRIMARY KEY CLUSTERED 
(
	[Номер] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ПриходнаяНакладная]    Script Date: 26.02.2025 13:37:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ПриходнаяНакладная](
	[IDНакладной] [int] IDENTITY(1,1) NOT NULL,
	[НомерНакладной] [nvarchar](max) NOT NULL,
	[ДатаПоступления] [date] NOT NULL,
	[Поставщик] [int] NOT NULL,
	[СписокТоваров] [int] NOT NULL,
	[ОбщаяСумма] [money] NOT NULL,
 CONSTRAINT [PK_ПриходнаяНакладная] PRIMARY KEY CLUSTERED 
(
	[IDНакладной] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[РасходнаяНакладная]    Script Date: 26.02.2025 13:37:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[РасходнаяНакладная](
	[IDНакладной] [int] IDENTITY(1,1) NOT NULL,
	[НомерНакладной] [nvarchar](max) NOT NULL,
	[ДатаОтгрузки] [date] NOT NULL,
	[Клиент] [int] NOT NULL,
	[СписокТоваров] [int] NOT NULL,
	[ОбщаяСумма] [money] NOT NULL,
 CONSTRAINT [PK_РасходнаяНакладная] PRIMARY KEY CLUSTERED 
(
	[IDНакладной] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Роль]    Script Date: 26.02.2025 13:37:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Роль](
	[Номер] [int] IDENTITY(1,1) NOT NULL,
	[Наименование] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Роль] PRIMARY KEY CLUSTERED 
(
	[Номер] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Склад]    Script Date: 26.02.2025 13:37:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Склад](
	[Номер] [int] IDENTITY(1,1) NOT NULL,
	[НазваниеСклада] [nvarchar](max) NOT NULL,
	[Адрес] [int] NOT NULL,
	[ТипСклада] [int] NOT NULL,
	[ЗоныХранения] [int] NOT NULL,
 CONSTRAINT [PK_Склад] PRIMARY KEY CLUSTERED 
(
	[Номер] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ТипCклад]    Script Date: 26.02.2025 13:37:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ТипCклад](
	[Номер] [int] IDENTITY(1,1) NOT NULL,
	[Наименование] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_ТипCклад] PRIMARY KEY CLUSTERED 
(
	[Номер] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Товар]    Script Date: 26.02.2025 13:37:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Товар](
	[Номер] [int] IDENTITY(1,1) NOT NULL,
	[Название] [nvarchar](50) NOT NULL,
	[Артикул] [nvarchar](50) NOT NULL,
	[Штрихкод] [nvarchar](max) NOT NULL,
	[Категория] [nvarchar](max) NOT NULL,
	[ЕдиницаИзмерения] [nvarchar](50) NOT NULL,
	[ЦенаЗаЕдиницу] [money] NOT NULL,
	[СерийныйНомер] [int] NULL,
	[МинимальныйОстаток] [int] NOT NULL,
 CONSTRAINT [PK_Товар] PRIMARY KEY CLUSTERED 
(
	[Номер] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ТоварВНакладной]    Script Date: 26.02.2025 13:37:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ТоварВНакладной](
	[Номер] [int] IDENTITY(1,1) NOT NULL,
	[НомерТовара] [int] NOT NULL,
	[Количество] [int] NOT NULL,
	[Цена] [money] NOT NULL,
 CONSTRAINT [PK_ТоварыВПриходнойНакладной] PRIMARY KEY CLUSTERED 
(
	[Номер] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ТоварНаСкладе]    Script Date: 26.02.2025 13:37:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ТоварНаСкладе](
	[НомерЗаписи] [int] IDENTITY(1,1) NOT NULL,
	[НомерТовара] [int] NOT NULL,
	[НомерСклада] [int] NOT NULL,
	[Количество] [int] NOT NULL,
 CONSTRAINT [PK_ТоварНаСкладе] PRIMARY KEY CLUSTERED 
(
	[НомерЗаписи] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Инвентаризация]  WITH CHECK ADD  CONSTRAINT [FK_Инвентаризация_Пользователь] FOREIGN KEY([Ответственный])
REFERENCES [dbo].[Пользователь] ([Номер])
GO
ALTER TABLE [dbo].[Инвентаризация] CHECK CONSTRAINT [FK_Инвентаризация_Пользователь]
GO
ALTER TABLE [dbo].[Клиент]  WITH CHECK ADD  CONSTRAINT [FK_Клиент_Адрес] FOREIGN KEY([Адрес])
REFERENCES [dbo].[Адрес] ([Номер])
GO
ALTER TABLE [dbo].[Клиент] CHECK CONSTRAINT [FK_Клиент_Адрес]
GO
ALTER TABLE [dbo].[Пользователь]  WITH CHECK ADD  CONSTRAINT [FK_Пользователь_Роль] FOREIGN KEY([Роль])
REFERENCES [dbo].[Роль] ([Номер])
GO
ALTER TABLE [dbo].[Пользователь] CHECK CONSTRAINT [FK_Пользователь_Роль]
GO
ALTER TABLE [dbo].[Поставщики]  WITH CHECK ADD  CONSTRAINT [FK_Поставщики_Адрес] FOREIGN KEY([Адрес])
REFERENCES [dbo].[Адрес] ([Номер])
GO
ALTER TABLE [dbo].[Поставщики] CHECK CONSTRAINT [FK_Поставщики_Адрес]
GO
ALTER TABLE [dbo].[ПриходнаяНакладная]  WITH CHECK ADD  CONSTRAINT [FK_ПриходнаяНакладная_Поставщики] FOREIGN KEY([Поставщик])
REFERENCES [dbo].[Поставщики] ([Номер])
GO
ALTER TABLE [dbo].[ПриходнаяНакладная] CHECK CONSTRAINT [FK_ПриходнаяНакладная_Поставщики]
GO
ALTER TABLE [dbo].[ПриходнаяНакладная]  WITH CHECK ADD  CONSTRAINT [FK_ПриходнаяНакладная_ТоварВНакладной] FOREIGN KEY([СписокТоваров])
REFERENCES [dbo].[ТоварВНакладной] ([Номер])
GO
ALTER TABLE [dbo].[ПриходнаяНакладная] CHECK CONSTRAINT [FK_ПриходнаяНакладная_ТоварВНакладной]
GO
ALTER TABLE [dbo].[РасходнаяНакладная]  WITH CHECK ADD  CONSTRAINT [FK_РасходнаяНакладная_ТоварВНакладной] FOREIGN KEY([СписокТоваров])
REFERENCES [dbo].[ТоварВНакладной] ([Номер])
GO
ALTER TABLE [dbo].[РасходнаяНакладная] CHECK CONSTRAINT [FK_РасходнаяНакладная_ТоварВНакладной]
GO
ALTER TABLE [dbo].[Склад]  WITH CHECK ADD  CONSTRAINT [FK_Склад_Адрес] FOREIGN KEY([Адрес])
REFERENCES [dbo].[Адрес] ([Номер])
GO
ALTER TABLE [dbo].[Склад] CHECK CONSTRAINT [FK_Склад_Адрес]
GO
ALTER TABLE [dbo].[Склад]  WITH CHECK ADD  CONSTRAINT [FK_Склад_ЗоныХранения] FOREIGN KEY([ЗоныХранения])
REFERENCES [dbo].[ЗоныХранения] ([Номер])
GO
ALTER TABLE [dbo].[Склад] CHECK CONSTRAINT [FK_Склад_ЗоныХранения]
GO
ALTER TABLE [dbo].[Склад]  WITH CHECK ADD  CONSTRAINT [FK_Склад_ТипCклад] FOREIGN KEY([ТипСклада])
REFERENCES [dbo].[ТипCклад] ([Номер])
GO
ALTER TABLE [dbo].[Склад] CHECK CONSTRAINT [FK_Склад_ТипCклад]
GO
ALTER TABLE [dbo].[ТоварВНакладной]  WITH CHECK ADD  CONSTRAINT [FK_ТоварВНакладной_Товар] FOREIGN KEY([НомерТовара])
REFERENCES [dbo].[Товар] ([Номер])
GO
ALTER TABLE [dbo].[ТоварВНакладной] CHECK CONSTRAINT [FK_ТоварВНакладной_Товар]
GO
ALTER TABLE [dbo].[ТоварНаСкладе]  WITH CHECK ADD  CONSTRAINT [FK_ТоварНаСкладе_Склад] FOREIGN KEY([НомерСклада])
REFERENCES [dbo].[Склад] ([Номер])
GO
ALTER TABLE [dbo].[ТоварНаСкладе] CHECK CONSTRAINT [FK_ТоварНаСкладе_Склад]
GO
ALTER TABLE [dbo].[ТоварНаСкладе]  WITH CHECK ADD  CONSTRAINT [FK_ТоварНаСкладе_Товар] FOREIGN KEY([НомерТовара])
REFERENCES [dbo].[Товар] ([Номер])
GO
ALTER TABLE [dbo].[ТоварНаСкладе] CHECK CONSTRAINT [FK_ТоварНаСкладе_Товар]
GO
USE [master]
GO
ALTER DATABASE [Warehouse] SET  READ_WRITE 
GO
