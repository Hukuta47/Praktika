CREATE DATABASE RepairServiceDB
GO
USE RepairServiceDB
GO
CREATE TABLE LoginData
(
	[LoginDataID] INT IDENTITY PRIMARY KEY,
	[Username] VARCHAR(20) NOT NULL,
	[Password] VARCHAR(20) NOT NULL
)
CREATE TABLE Techs 
(
	[TechID] INT IDENTITY PRIMARY KEY,
	[TechTypeName] VARCHAR(20) NOT NULL,
	[Manufacturer] VARCHAR(20) NOT NULL,
	[Model] VARCHAR(20) NOT NULL,
	[Description] NVARCHAR(MAX) NULL
)
CREATE TABLE OrdersStatus 
(
	[OrderStatusID] INT IDENTITY PRIMARY KEY,
	[Name] NVARCHAR(20) NOT NULL,
	[Description] NVARCHAR(MAX) NULL
)
CREATE TABLE RepairParts
(
	[RepairPartID] INT IDENTITY PRIMARY KEY,
	[Name] VARCHAR(50) NOT NULL,
	[Description] NVARCHAR(MAX) NULL
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
	[LoginDataID] INT NOT NULL REFERENCES LoginData(LoginDataID),
	[UserTypeID] INT NOT NULL REFERENCES UserTypes(UserTypeID)
)
CREATE TABLE Orders
(
	[OrderID]          INT IDENTITY PRIMARY KEY,
	[CustomerID]       INT NOT NULL REFERENCES Users(UserID),
	[MasterEmployeeID] INT NULL REFERENCES Users(UserID),
	[OrderDate]        DATE NOT NULL DEFAULT GETDATE(),
	[TechID]           INT NOT NULL REFERENCES Techs(TechID),
	[Description]      NVARCHAR(MAX) NOT NULL,
	[OrderStatusID]    INT NOT NULL REFERENCES OrdersStatus(OrderStatusID),
	[CompletionDate]   DATE NULL,
	[RepairPartsID]    INT NULL REFERENCES RepairParts(RepairPartID)
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
        WHERE UserID = @CustomerID AND UserTypeID <> (SELECT UserTypeID FROM UserTypes WHERE Name = 'Заказчик')
    )
    BEGIN
        RAISERROR('CustomerID должно быть "Заказчик".', 16, 1)
        ROLLBACK
        RETURN
    END

    IF EXISTS (
        SELECT 1
        FROM Users
        WHERE UserID = @MasterEmployeeID AND UserTypeID <> (SELECT UserTypeID FROM UserTypes WHERE Name = 'Мастер')
    )
    BEGIN
        RAISERROR('MasterEmployeeID должно быть "Мастер".', 16, 1)
        ROLLBACK
        RETURN
    END
END
GO



CREATE PROCEDURE CreateRepairRequest
    @CustomerID INT,
    @TechID INT,
    @Description NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO Orders (CustomerID, TechID, Description, OrderStatusID)
    VALUES (@CustomerID, @TechID, @Description, (SELECT OrderStatusID FROM OrdersStatus WHERE Name = 'Новая заявка'))
END
GO

CREATE PROCEDURE EditRepairRequest
    @OrderID INT,
    @TechID INT,
    @Description NVARCHAR(MAX)
AS
BEGIN
    UPDATE Orders
    SET TechID = @TechID,
        Description = @Description
    WHERE OrderID = @OrderID
END
GO
-- 1 --

CREATE PROCEDURE RegisterRepairRequest
    @OrderID INT
AS
BEGIN
    UPDATE Orders
    SET OrderStatusID = (SELECT OrderStatusID FROM OrdersStatus WHERE Name = 'Зарегистрирована')
    WHERE OrderID = @OrderID
END

GO
-- 2 --

CREATE PROCEDURE AssignMasterToRequest
    @OrderID INT,
    @MasterEmployeeID INT
AS
BEGIN
    UPDATE Orders
    SET MasterEmployeeID = @MasterEmployeeID,
        OrderStatusID = (SELECT OrderStatusID FROM OrdersStatus WHERE Name = 'В процессе ремонта')
    WHERE OrderID = @OrderID
END

GO
-- 3 --

CREATE PROCEDURE CompleteRepairRequest
    @OrderID INT,
    @RepairPartsID INT
AS
BEGIN
    UPDATE Orders
    SET RepairPartsID = @RepairPartsID,
        OrderStatusID = (SELECT OrderStatusID FROM OrdersStatus WHERE Name = 'Готова к выдаче')
    WHERE OrderID = @OrderID
END
GO
-- 4 --

CREATE PROCEDURE GenerateRepairReport
    @OrderID INT
AS
BEGIN
    SELECT
        o.OrderID,
        o.CustomerID,
        o.MasterEmployeeID,
        o.OrderDate,
        o.TechID,
        o.Description,
        o.OrderStatusID,
        o.CompletionDate,
        o.RepairPartsID,
        uc.FirstName AS CustomerFirstName,
        uc.LastName AS CustomerLastName,
        uc.Phone AS CustomerPhone,
        um.FirstName AS MasterFirstName,
        um.LastName AS MasterLastName,
        um.Phone AS MasterPhone,
        t.TechTypeName,
        t.Manufacturer,
        t.Model,
        rp.Name AS RepairPartName,
        rp.Description AS RepairPartDescription
    FROM Orders o
    JOIN Users uc ON o.CustomerID = uc.UserID
    LEFT JOIN Users um ON o.MasterEmployeeID = um.UserID
    JOIN Techs t ON o.TechID = t.TechID
    LEFT JOIN RepairParts rp ON o.RepairPartsID = rp.RepairPartID
    WHERE o.OrderID = @OrderID
END
GO
-- 5 --

CREATE PROCEDURE AnalyzeRequestProcessingTime
AS
BEGIN
    SELECT
        o.OrderID,
        o.OrderDate,
        o.CompletionDate,
        DATEDIFF(DAY, o.OrderDate, o.CompletionDate) AS ProcessingTimeInDays
    FROM Orders o
    WHERE o.CompletionDate IS NOT NULL
END
GO
-- 6 --

CREATE PROCEDURE GetOrdersByCustomer
    @CustomerID INT
AS
BEGIN
    SELECT
        O.OrderID AS OrderID,
        O.OrderDate AS OrderDate,
        (SELECT CONCAT(M.FirstName, ' ', M.LastName, ' ', M.Patronymic)
         FROM Users M
         WHERE M.UserID = O.MasterEmployeeID) AS FullName,
        O.Description AS DescriptionProbles,
        (SELECT OS.Name
         FROM OrdersStatus OS
         WHERE OS.OrderStatusID = O.OrderStatusID) AS OrderStatus,
        O.CompletionDate AS CompletionDate
    FROM
        Orders O
    WHERE
        O.CustomerID = @CustomerID;
END;

-- 7 --