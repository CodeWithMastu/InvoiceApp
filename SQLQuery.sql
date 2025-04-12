SET NOCOUNT ON;
DECLARE @i INT = 15;

-- Insert a few fixed sample records (matching your screenshot)
INSERT INTO Invoices (Number, Status, IssueDate, DueDate, Service, UnitPrice, Quantity, ClientName, Email, Phone, Address)
VALUES
('100001', 'Paid', '2024-12-25', '2024-12-31', 'Proofreading', 10.00, 30, 'John', 'john@gmail.com', '9988888', 'New York'),
('100002', 'Pending', '2024-12-26', NULL, 'Proofreading', 12.00, 50, 'Garrett Winters', 'garrett@gmail.com', '9988888', 'New York'),
('100003', 'Paid', '2024-12-26', NULL, 'Proofreading', 12.00, 12, 'Ashton Cox', 'Ashton.Cox@gmail.com', '9988888', 'New York'),
('100004', 'Pending', '2024-12-26', '2025-01-26', 'Proofreading', 12.00, 12, 'Cedric Kelly', 'Cedric.Kelly@gmail.com', '9988888', 'New York'),
('100005', 'Paid', '2024-12-26', NULL, 'Proofreading', 12.00, 14, 'Brielle Williamson', 'Brielle.Williamson@gmail.com', '9988888', 'New York');

-- Insert additional random records dynamically
WHILE @i <= 100
BEGIN
    INSERT INTO Invoices (Number, Status, IssueDate, DueDate, Service, UnitPrice, Quantity, ClientName, Email, Phone, Address)
    VALUES (
        '1000' + CAST(@i AS VARCHAR(3)),  -- Invoice Number
        CASE WHEN RAND() > 0.5 THEN 'Paid' ELSE 'Pending' END,  -- Randomly select 'Paid' or 'Pending'
        DATEADD(DAY, -ABS(CHECKSUM(NEWID()) % 365), GETDATE()), -- Random past date for IssueDate
        CASE WHEN RAND() > 0.5 THEN DATEADD(DAY, ABS(CHECKSUM(NEWID()) % 30), GETDATE()) ELSE NULL END, -- Random future date or NULL for DueDate
        'Proofreading',  -- Keeping service type constant
        ROUND((RAND() * 5 + 10), 2),  -- Random price between 10.00 and 15.00
        ABS(CHECKSUM(NEWID()) % 50) + 1,  -- Random quantity between 1 and 50
        'Client ' + CAST(@i AS VARCHAR(3)),  -- Client name
        'client' + CAST(@i AS VARCHAR(3)) + '@gmail.com',  -- Email
        '9988888',  -- Static phone number (same as your screenshot)
        'New York'  -- Static address (same as your screenshot)
    );

    SET @i = @i + 1;
END;


-- Check the data in the invoice table
Select * from Invoices;