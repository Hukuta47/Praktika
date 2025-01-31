CREATE PROCEDURE CreateRepairRequest
    @CustomerID INT,
    @TechID INT,
    @Description NVARCHAR(MAX),
    @OrderDate DATE
AS
BEGIN
    INSERT INTO Orders (CustomerID, TechID, Description, OrderDate, OrderStatusID)
    VALUES (@CustomerID, @TechID, @Description, @OrderDate, (SELECT OrderStatusID FROM OrdersStatus WHERE Name = N'Новая'))
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
    SET OrderStatusID = (SELECT OrderStatusID FROM OrdersStatus WHERE Name = N'Зарегистрирована')
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
        OrderStatusID = (SELECT OrderStatusID FROM OrdersStatus WHERE Name = N'В работе')
    WHERE OrderID = @OrderID
END

GO
-- 3 --

CREATE PROCEDURE CompleteRepairRequest
    @OrderID INT,
    @CompletionDate DATE,
    @RepairPartsID INT
AS
BEGIN
    UPDATE Orders
    SET CompletionDate = @CompletionDate,
        RepairPartsID = @RepairPartsID,
        OrderStatusID = (SELECT OrderStatusID FROM OrdersStatus WHERE Name = N'Завершена')
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
        t.Mode,
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
        CONCAT(M.FirstName, ' ', M.LastName, ' ', M.Patronymic) AS FullName,
        O.Description AS DescriptionProbles,
        OS.Name AS OrderStatus,
        O.CompletionDate AS CompletionDate
    FROM
        Orders O
    JOIN
        Users M ON O.MasterEmployeeID = M.UserID
    JOIN
        OrdersStatus OS ON O.OrderStatusID = OS.OrderStatusID
    WHERE
        O.CustomerID = 8;
END;

-- 7 --