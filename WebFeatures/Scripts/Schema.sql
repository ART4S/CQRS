CREATE TABLE Files
(
	Id UUID NOT NULL,
	Content bytea,
	
	CONSTRAINT PK_Files PRIMARY KEY (Id)
);

CREATE TABLE Users
(
	Id UUID NOT NULL,
	Name VARCHAR,
	Email VARCHAR,
	PasswordHash VARCHAR,
	PictureId uuid,
	
	CONSTRAINT PK_Users PRIMARY KEY (Id),
	CONSTRAINT FK_Users_Files_PictureId FOREIGN KEY (PictureId) REFERENCES Files (Id)
);

CREATE TABLE Roles
(
	Id UUID NOT NULL,
	Name VARCHAR NOT NULL,
	Description VARCHAR,
	
	CONSTRAINT PK_Roles PRIMARY KEY (Id)
);

CREATE TABLE UserRoles
(
	UserId UUID NOT NULL,
	RoleId UUID NOT NULL,
	
	CONSTRAINT PK_UserRoles PRIMARY KEY (UserId, RoleId),
	CONSTRAINT FK_UserRoles_Users_UserId FOREIGN KEY (UserId) REFERENCES Users (Id) ON DELETE CASCADE,
	CONSTRAINT FK_UserRoles_Roles_RoleId FOREIGN KEY (RoleId) REFERENCES Roles (Id) ON DELETE CASCADE
);

CREATE TABLE Countries
(
	Id UUID NOT NULL,
	Name VARCHAR,
	Continent VARCHAR,
	
	CONSTRAINT PK_Countries PRIMARY KEY (Id)
);

CREATE TABLE Cities
(
	Id UUID NOT NULL,
	Name VARCHAR,
	CountryId UUID NOT NULL,
	
	CONSTRAINT PK_Cities PRIMARY KEY (Id),
	CONSTRAINT FK_Cities_Countries_CountryId FOREIGN KEY (CountryId) REFERENCES Countries (Id)
);

CREATE TABLE Manufacturers
(
	Id UUID NOT NULL,
	OrganizationName VARCHAR,
	HomePageUrl VARCHAR,
	StreetAddress_StreetName VARCHAR NOT NULL,
	StreetAddress_PostalCode VARCHAR NOT NULL,
	StreetAddress_CityId UUID NOT NULL,
	
	CONSTRAINT PK_Manufacturers PRIMARY KEY(Id),
	CONSTRAINT FK_Manufacturers_Cities_StreetAddress_CityId FOREIGN KEY (StreetAddress_CityId) REFERENCES Cities (Id)
);

CREATE TABLE Categories
(
	Id UUID NOT NULL,
	Name VARCHAR,
	
	CONSTRAINT PK_Categories PRIMARY KEY (Id)
);

CREATE TABLE Brands
(
	Id UUID NOT NULL,
	Name VARCHAR,
	
	CONSTRAINT PK_Brands PRIMARY KEY (Id)
);

CREATE TABLE Products
(
	Id UUID NOT NULL,
	Name VARCHAR,
	Price DECIMAL,
	Description VARCHAR,
	AverageRating INT,
	ReviewsCount INT NOT NULL DEFAULT 0,
	CreateDate TIMESTAMP WITHOUT TIME ZONE NOT NULL,
	PictureId UUID,
	ManufacturerId UUID NOT NULL,
	CategoryId UUID,
	BrandId UUID NOT NULL,
	
	CONSTRAINT PK_Products PRIMARY KEY (Id),
	CONSTRAINT FK_Products_Files_PictureId FOREIGN KEY (PictureId) REFERENCES Files (Id) ON DELETE SET NULL,
	CONSTRAINT FK_Products_Manufacturers_ManufacturerId FOREIGN KEY (ManufacturerId) REFERENCES Manufacturers (Id),
	CONSTRAINT FK_Products_Categories_CategoryId FOREIGN KEY (CategoryId) REFERENCES Categories (Id) ON DELETE SET NULL,
	CONSTRAINT FK_Products_Brands_BrandId FOREIGN KEY (BrandId) REFERENCES Brands (Id),
	CONSTRAINT Products_AverageRating_Check CHECK (AverageRating IS NULL OR AverageRating BETWEEN 1 AND 5)
);

CREATE TABLE ProductComments
(
	Id UUID NOT NULL,
	Body VARCHAR NOT NULL,
	CreateDate TIMESTAMP WITHOUT TIME ZONE NOT NULL,
	ProductId UUID NOT NULL,
	AuthorId UUID NOT NULL,
	ParentCommentId UUID,
	
	CONSTRAINT PK_ProductComments PRIMARY KEY (Id),
	CONSTRAINT FK_ProductComments_Products_ProductId FOREIGN KEY (ProductId) REFERENCES Products (Id) ON DELETE CASCADE,
	CONSTRAINT FK_ProductComments_Users_AuthorId FOREIGN KEY (AuthorId) REFERENCES Users (Id) ON DELETE CASCADE,
	CONSTRAINT FK_ProductComments_ProductComments_ParentCommentId FOREIGN KEY (ParentCommentId) REFERENCES ProductComments (Id) ON DELETE CASCADE
);

CREATE TABLE ProductReviews
(
	Id UUID NOT NULL,
	Title VARCHAR NOT NULL,
	Comment VARCHAR NOT NULL,
	CreateDate TIMESTAMP WITHOUT TIME ZONE NOT NULL,
	Rating INT NOT NULL,
	AuthorId UUID NOT NULL,
	ProductId UUID NOT NULL,
	
	CONSTRAINT PK_ProductReviews PRIMARY KEY (Id),
	CONSTRAINT FK_ProductReviews_Users_AuthorId FOREIGN KEY (AuthorId) REFERENCES Users (Id) ON DELETE CASCADE,
	CONSTRAINT FK_ProductReviews_Products_ProductId FOREIGN KEY (ProductId) REFERENCES Products (Id) ON DELETE CASCADE,
	CONSTRAINT ProductReviews_Rating_Check CHECK (Rating BETWEEN 1 AND 5)
);

CREATE TABLE Shippers
(
	Id UUID NOT NULL,
	OrganizationName VARCHAR NOT NULL,
	ContactPhone VARCHAR,
	HeadOffice_StreetName VARCHAR NOT NULL,
	HeadOffice_PostalCode VARCHAR NOT NULL,
	HeadOffice_CityId UUID NOT NULL,
	
	CONSTRAINT PK_Shippers PRIMARY KEY(Id),
	CONSTRAINT PK_Shippers_Cities_HeadOffice_CityId FOREIGN KEY(HeadOffice_CityId) REFERENCES Cities(Id)
);