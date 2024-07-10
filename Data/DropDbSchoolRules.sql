USE master;
GO
-- Chuyển cơ sở dữ liệu sang chế độ đơn người dùng và ngắt kết nối
ALTER DATABASE [SchoolRules] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
GO

-- Thả cơ sở dữ liệu
DROP DATABASE [SchoolRules];
GO
