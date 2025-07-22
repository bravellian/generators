-- Test schema to validate ingestion
CREATE TABLE dbo.Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    CreatedDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    IsActive BIT NOT NULL DEFAULT 1
);

CREATE TABLE dbo.Orders (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    OrderDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    Total DECIMAL(10,2) NOT NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT 'Pending',
    
    CONSTRAINT FK_Orders_Users FOREIGN KEY (UserId) REFERENCES dbo.Users(Id)
);

CREATE UNIQUE INDEX IX_Users_Username ON dbo.Users(Username);
CREATE INDEX IX_Orders_UserId ON dbo.Orders(UserId);
CREATE INDEX IX_Orders_OrderDate ON dbo.Orders(OrderDate);

CREATE VIEW dbo.UserOrderSummary AS
SELECT 
    u.Id,
    u.Username,
    u.Email,
    COUNT(o.Id) as OrderCount,
    SUM(o.Total) as TotalAmount
FROM dbo.Users u
LEFT JOIN dbo.Orders o ON u.Id = o.UserId
GROUP BY u.Id, u.Username, u.Email;
