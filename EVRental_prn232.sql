-- Tạo database mới
CREATE DATABASE FA25_PRN232_SE1717_G6_EVRental;
GO

USE FA25_PRN232_SE1717_G6_EVRental;
GO

-----------------------------------------------------
-- Bảng phụ: ReturnCondition
-----------------------------------------------------
CREATE TABLE ReturnCondition (
    ReturnConditionID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),                 -- tên tình trạng xe
    SeverityLevel INT,                  -- mức độ hỏng hóc (1-5)
    RepairCost DECIMAL(10,2) DEFAULT 0, -- chi phí sửa chữa dự kiến
    IsResolved BIT DEFAULT 0            -- đã khắc phục chưa
);

-----------------------------------------------------
-- Bảng chính: CheckOutQuanNH
-----------------------------------------------------
CREATE TABLE CheckOutQuanNH (
    CheckOutQuanNHID INT PRIMARY KEY IDENTITY,
    CheckOutTime DATETIME DEFAULT GETDATE(), -- thời điểm trả xe
    ReturnDate DATE,                         -- ngày trả
    ExtraCost DECIMAL(10,2) DEFAULT 0,       -- chi phí phát sinh
    TotalCost DECIMAL(10,2) DEFAULT 0,       -- tổng chi phí
    LateFee DECIMAL(10,2) DEFAULT 0,         -- phí trễ hạn (nếu có)
    IsPaid BIT DEFAULT 0,                    -- đã thanh toán chưa
    IsDamageReported BIT DEFAULT 0,          -- có báo hỏng hóc không
    Notes VARCHAR(255),                      -- ghi chú của nhân viên
    CustomerFeedback VARCHAR(255),           -- phản hồi từ khách hàng
    PaymentMethod VARCHAR(50),               -- phương thức thanh toán
    StaffSignature VARCHAR(100),             -- chữ ký nhân viên xác nhận
    CustomerSignature VARCHAR(100),          -- chữ ký khách hàng xác nhận
    ReturnConditionID INT FOREIGN KEY REFERENCES ReturnCondition(ReturnConditionID)
    -- BỎ UNIQUE constraint để nhiều checkout có thể dùng cùng ReturnConditionID
);

-----------------------------------------------------
-- Bảng người dùng: System_UserAccount
-----------------------------------------------------
CREATE TABLE System_UserAccount (
    UserAccountID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    UserName NVARCHAR(50) NOT NULL,
    Password NVARCHAR(100) NOT NULL,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) NOT NULL,
    Phone NVARCHAR(50) NOT NULL,
    EmployeeCode NVARCHAR(50) NOT NULL,
    RoleId INT NOT NULL,
    RequestCode NVARCHAR(50) NULL,
    CreatedDate DATETIME NULL,
    ApplicationCode NVARCHAR(50) NULL,
    CreatedBy NVARCHAR(50) NULL,
    ModifiedDate DATETIME NULL,
    ModifiedBy NVARCHAR(50) NULL,
    IsActive BIT NOT NULL
);

-----------------------------------------------------
-- Insert dữ liệu mẫu
-----------------------------------------------------

-- Dữ liệu cho UserAccount
SET IDENTITY_INSERT System_UserAccount ON;
INSERT INTO System_UserAccount 
(UserAccountID, UserName, Password, FullName, Email, Phone, EmployeeCode, RoleId, RequestCode, CreatedDate, ApplicationCode, CreatedBy, ModifiedDate, ModifiedBy, IsActive) 
VALUES 
(1, N'acc', N'@a', N'Accountant', N'Accountant@', N'0913652742', N'000001', 2, NULL, NULL, NULL, NULL, NULL, NULL, 1),
(2, N'auditor', N'@a', N'Internal Auditor', N'InternalAuditor@', N'0972224568', N'000002', 3, NULL, NULL, NULL, NULL, NULL, NULL, 1),
(3, N'chiefacc', N'@a', N'Chief Accountant', N'ChiefAccountant@', N'0902927373', N'000003', 1, NULL, NULL, NULL, NULL, NULL, NULL, 1);
SET IDENTITY_INSERT System_UserAccount OFF;

-- Dữ liệu cho ReturnCondition (3 bản ghi)
INSERT INTO ReturnCondition (Name, SeverityLevel, RepairCost, IsResolved) VALUES
(N'Không hỏng hóc', 0, 0, 1),
(N'Trầy xước nhẹ', 2, 150000, 0),
(N'Hư hỏng nặng', 5, 5000000, 0);

-- Dữ liệu cho CheckOutQuanNH (2 bản ghi, tham chiếu tới ReturnConditionID = 1 và 2)
INSERT INTO CheckOutQuanNH (ReturnDate, ExtraCost, TotalCost, LateFee, IsPaid, IsDamageReported, Notes, CustomerFeedback, PaymentMethod, StaffSignature, CustomerSignature, ReturnConditionID)
VALUES
('2025-09-12', 50000, 350000, 0, 1, 0, 'Xe sạch sẽ, không vấn đề', 'Khách hàng hài lòng', 'Tiền mặt', 'StaffA', 'CustomerA', 1),
('2025-09-12', 100000, 600000, 50000, 0, 1, 'Xe bị trầy ở cửa sau', 'Khách hàng không hài lòng', 'Thẻ tín dụng', 'StaffB', 'CustomerB', 2);
