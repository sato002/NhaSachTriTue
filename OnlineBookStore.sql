-- =============================================
-- Create database template
-- =============================================
USE master
GO

-- Drop the database if it already exists
IF  EXISTS (
	SELECT name 
		FROM sys.databases 
		WHERE name = N'OnlineBookStore'
)
DROP DATABASE OnlineBookStore
GO

CREATE DATABASE OnlineBookStore
GO

USE OnlineBookStore
GO

CREATE TABLE [User] (
	[UserID] bigint identity(1,1),
	[UserName] varchar(50) not null,
	[Password] varchar(32) not null,
	[Name] nvarchar(50),
	[Phone] nvarchar(20),
	[Email] nvarchar(50),
	[Address] nvarchar(250),
	[CreatedDate] datetime default(getdate()),
	[RoleID] int default(1) not null,
	[Status] bit default(1) not null
	CONSTRAINT pk_Customer PRIMARY KEY([UserID])
)
GO

CREATE TABLE [Role] (
	[RoleID] int identity(1,1),
	[RoleName] nvarchar(30) not null
	CONSTRAINT pk_Role PRIMARY KEY(RoleID)
)
GO

CREATE TABLE Product (
	[ProductID] bigint identity(1,1),
	[CategoryID] bigint not null,
	[ProductName] nvarchar(250) not null,
	[ProductCode] varchar(50),
	[Author] nvarchar(250),
	[Publisher] nvarchar(250),
	[MetaTitle] varchar(250) not null,
	[Description] nvarchar(2000),
	[Image] nvarchar(250) not null,
	[Price] decimal(18,0) default(0),
	[PromotionPrice] decimal(18,0) default(null),
	[Quanlity] int default(1),
	[Detail] ntext,
	[ViewCount] bigint default(0),
	[CreatedDate] datetime default(getdate()),
	[Status] bit default(1) not null
	CONSTRAINT pk_Product PRIMARY KEY(ProductID)
)
GO


CREATE TABLE ProductCategory(
	[CategoryID] bigint identity(1,1),
	[CategoryName] nvarchar(250) not null,
	[MetaTitle] varchar(250) not null,
	[CreatedDate] datetime default(getdate()),
	[Status] bit default(1) not null
	CONSTRAINT pk_ProductCategory PRIMARY KEY([CategoryID])
)
GO

CREATE TABLE MenuType(
	[MenuTypeID] int identity(1,1),
	[MenuName] nvarchar(50) not null
	CONSTRAINT pk_MenuType PRIMARY KEY([MenuTypeID])
)
GO

CREATE TABLE Menu (
	[MenuID] int identity(1,1),
	[Text] nvarchar(50) not null,
	[Link] varchar(50) not null,
	[DisplayOrder] int,
	[Target] varchar(50),
	[Status] bit default(1),
	[MenuTypeID] int not null
	CONSTRAINT pk_Menu PRIMARY KEY([MenuID])
)
GO

Create table [Order](
	OrderID bigint identity(1,1),
	CreatedDate datetime not null default(getdate()),
	shipName nvarchar(50),
	shipPhone varchar(50),
	shipAddress nvarchar(50),
	shipEmail nvarchar(50),
	[Status] bit not null default(0)
	CONSTRAINT pk_order PRIMARY KEY(OrderID)
)
GO

INSERT INTO dbo.[Order](shipName,shipPhone,shipEmail,shipAddress) VALUES
(N'BVHau','01211212','ghfjhgdj@gmail.com',N'Hà Nội')

CREATE TABLE [OrderDetail](
	[OrderDetailID] bigint identity(1,1),
	ProductID bigint,
	OrderID bigint,
	Quanlity int default(1),
	Price decimal(18,0) default(0)
	CONSTRAINT pk_orderdetail PRIMARY KEY([OrderDetailID])
)
GO


INSERT INTO dbo.[Role](RoleName) VALUES
('ADMIN')
INSERT INTO dbo.[Role](RoleName) VALUES
('USER')
GO



INSERT INTO dbo.[User](UserName,[Password],Name,[Phone],Email,[Address],[RoleID],[Status]) VALUES
('admin','21232f297a57a5a743894a0e4a801fc3','admin',012345678,'admin@gmail.com','admin street',1,1)
GO

INSERT INTO dbo.ProductCategory(CategoryName,[MetaTitle],[Status]) VALUES (N'Sách Giáo Dục','sach-giao-duc',1)
INSERT INTO dbo.ProductCategory(CategoryName,[MetaTitle],[Status]) VALUES (N'Văn Học','van-hoc',1)
INSERT INTO dbo.ProductCategory(CategoryName,[MetaTitle],[Status]) VALUES (N'Khoa Học Tự Nhiên','khoa-hoc-tu-nhien',1)
INSERT INTO dbo.ProductCategory(CategoryName,[MetaTitle],[Status]) VALUES (N'Khoa Học Xã Hội','khoa-hoc-xa-hoi',1)
INSERT INTO dbo.ProductCategory(CategoryName,[MetaTitle],[Status]) VALUES (N'Khoa Học Kĩ Thuật','khoa-hoc-ki-thuat',1)
INSERT INTO dbo.ProductCategory(CategoryName,[MetaTitle],[Status]) VALUES (N'Sách Thiếu Nhi','sach-thieu-nhi',1)
INSERT INTO dbo.ProductCategory(CategoryName,[MetaTitle],[Status]) VALUES (N'Sách Nghệ Thuật','sach-nghe-thuat',1)
INSERT INTO dbo.ProductCategory(CategoryName,[MetaTitle],[Status]) VALUES (N'Sách Y Học','sach-y-hoc',1)

INSERT INTO dbo.MenuType(MenuName) VALUES(N'Menu chính')
GO
INSERT INTO dbo.MenuType(MenuName) VALUES(N'Menu phụ')
GO

INSERT INTO dbo.Menu([Text],[Link],[DisplayOrder],[Target],[Status],[MenuTypeID]) VALUES (N'Giới thiệu','/gioi-thieu',1,'_self',1,1)
INSERT INTO dbo.Menu([Text],[Link],[DisplayOrder],[Target],[Status],[MenuTypeID]) VALUES (N'Chính sách','/chinh-sach',1,'_self',1,1)
INSERT INTO dbo.Menu([Text],[Link],[DisplayOrder],[Target],[Status],[MenuTypeID]) VALUES (N'Sản phẩm','/san-pham',1,'_self',1,1)
INSERT INTO dbo.Menu([Text],[Link],[DisplayOrder],[Target],[Status],[MenuTypeID]) VALUES (N'Liên hệ','/lien-he',1,'_self',1,1)


