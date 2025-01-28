CREATE DATABASE RepairServiceDB
GO
USE RepairServiceDB
GO
CREATE TABLE LoginData 
(
	[LoginDataID] INT IDENTITY PRIMARY KEY,
	[Login] VARCHAR(20) NOT NULL,
	[Password] VARCHAR(20) NOT NULL
)
CREATE TABLE Techs 
(
	[TechID] INT IDENTITY PRIMARY KEY,
	[TechTypeName] VARCHAR(20) NOT NULL,
	[Manufacturer] VARCHAR(20) NOT NULL,
	[Mode] VARCHAR(20) NOT NULL
)
CREATE TABLE OrdersStatus 
(
	[OrderStatusID] INT IDENTITY PRIMARY KEY,
	[Name] NVARCHAR(20) NOT NULL,
	[Description] NVARCHAR(MAX) NOT NULL
)
CREATE TABLE RepairParts
(
	[RepairPartID] INT IDENTITY PRIMARY KEY,
	[Name] VARCHAR(50) NOT NULL,
	[Description] VARCHAR(50) NOT NULL
)
CREATE TABLE UserTypes 
(
	[UserTypeID] INT IDENTITY PRIMARY KEY,
	[Name] NVARCHAR(20) NOT NULL,
	[Description] NVARCHAR(MAX) NULL
)
CREATE TABLE Users
(
	[UserID] INT IDENTITY PRIMARY KEY,
	[FirstName] NVARCHAR(20) NOT NULL,
	[LastName] NVARCHAR(20) NOT NULL,
	[Patronymic] NVARCHAR(20) NULL,
	[Phone] NVARCHAR(12) NOT NULL,
	[LoginDateID] INT NOT NULL REFERENCES LoginData(LoginDataID),
	[UserTypeID] INT NOT NULL REFERENCES UserTypes(UserTypeID)
)
CREATE TABLE Orders
(
	[OrderID]          INT IDENTITY PRIMARY KEY,
	[CustomerID]       INT REFERENCES Users(UserID) NOT NULL,
	[MasterEmployeeID] INT REFERENCES Users(UserID) NULL,
	[OrderDate]        DATE NOT NULL,
	[TechID]           INT NOT NULL REFERENCES Techs(TechID),
	[Description]      NVARCHAR(MAX) NOT NULL,
	[OrderStatusID]    INT NOT NULL REFERENCES OrdersStatus(OrderStatusID),
	[CompletionDate]   DATE NULL,
	[RepairPartsID]    INT REFERENCES RepairParts(RepairPartID) NULL
)
CREATE TABLE Comments 
(
	[CommentID] INT IDENTITY PRIMARY KEY,
	[Message] NVARCHAR(MAX) NOT NULL,
	[MasterEmployeeID] INT NOT NULL REFERENCES Users(UserID),
	[OrderID] INT NOT NULL REFERENCES Orders(OrderID)
);
GO
CREATE TRIGGER trg_CheckUserTypes_Orders
ON Orders
FOR INSERT, UPDATE
AS
BEGIN
    DECLARE @CustomerID INT, @MasterEmployeeID INT

    SELECT @CustomerID = CustomerID, @MasterEmployeeID = MasterEmployeeID
    FROM inserted

    IF EXISTS (
        SELECT 1
        FROM Users
        WHERE UserID = @CustomerID AND UserTypeID <> (SELECT UserTypeID FROM UserTypes WHERE Name = N'Заказчик')
    )
    BEGIN
        RAISERROR('CustomerID должно быть "Заказчик".', 16, 1)
        ROLLBACK
        RETURN
    END

    IF EXISTS (
        SELECT 1
        FROM Users
        WHERE UserID = @MasterEmployeeID AND UserTypeID <> (SELECT UserTypeID FROM UserTypes WHERE Name = N'Мастер')
    )
    BEGIN
        RAISERROR('MasterEmployeeID должно быть "Мастер".', 16, 1)
        ROLLBACK
        RETURN
    END
END
GO
