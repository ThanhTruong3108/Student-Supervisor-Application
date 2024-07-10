-- Chèn 6 bản ghi mẫu vào bảng Role
INSERT INTO [SchoolRules].[dbo].[Role]([RoleName])
VALUES
    ('ADMIN'),
    ('SCHOOL_ADMIN'),
    ('PRINCIPAL'),
    ('SUPERVISOR'),
    ('TEACHER'),
    ('STUDENT_SUPERVISOR');

-- Chèn 1 bản ghi mẫu vào bảng Admin
INSERT INTO [SchoolRules].[dbo].[Admin] ([Name], [Email], [Password], [Phone], [RoleID], [Status])
VALUES
    ('Admin', 'admin@gmail.com', 'password123', '8438522344', 1, 'ACTIVE');


-- Chèn 2 bản ghi mẫu vào bảng HighSchool
INSERT INTO [SchoolRules].[dbo].[HighSchool] ([Code], [Name], [City], [Address], [Phone], [WebURL], [Status])
VALUES
	('HS001', N'THPT Bình Thắng', N'TP.HCM', N'Dĩ An', '8487654321', 'http://www.thptbt.edu.vn', 'ACTIVE');


-- Chèn 6 bản ghi mẫu vào bảng User
INSERT INTO [SchoolRules].[dbo].[User] ([RoleID], [SchoolID], [Code], [Name], [Phone], [Password], [Address], [Status])
VALUES
    (2, 1, 'BT001', 'School_Admin', '8412367890', 'password123', N'123 Đường A, Quận B, Hà Nội', 'ACTIVE'),
    (3, 1, 'BT002', 'Principal', '8412367891', 'password123', N'456 Đường C, Quận D, Hà Nội', 'ACTIVE'),
    (4, 1, 'BT003', 'Supervisor', '8412367892', 'password123', N'789 Đường E, Quận F, Hà Nội', 'ACTIVE'),
    (5, 1, 'BT004', 'Teacher', '8412367893', 'password123', N'321 Đường G, Quận H, TP.HCM', 'ACTIVE'),
    (6, 1, 'BT005', 'Stu_Supervisor', '8412367894', 'password123', N'654 Đường I, Quận J, TP.HCM', 'ACTIVE'),
    (6, 1, 'BT006', 'Stu_Supervisor2', '8412345678', 'password123', N'Hồ Chí Minh', 'ACTIVE');


-- Chèn 6 bản ghi mẫu vào bảng Teacher
INSERT INTO [SchoolRules].[dbo].[Teacher] ([UserID], [SchoolID], [Sex])
VALUES
(3, 1, 'False'),
(4, 1, 'True');


-- Chèn 6 bản ghi mẫu vào bảng StudentSupervisor
INSERT INTO [SchoolRules].[dbo].[StudentSupervisor] ([UserID], [StudentInClassID], [Description])
VALUES
(5, 1, 'Supervisor for StudentInClass 1'),
(6, 2, 'Supervisor for StudentInClass 2');


-- Chèn 4 bản ghi mẫu vào bảng SchoolYear
INSERT INTO [dbo].[SchoolYear] ([SchoolID], [Year], [StartDate], [EndDate], [Status])
VALUES 
    (1, 2021, '2021-09-01', '2022-06-30', 'ACTIVE'),
    (1, 2022, '2022-09-01', '2023-06-30', 'ACTIVE'),
    (1, 2023, '2023-09-01', '2024-06-30', 'ACTIVE'),
    (1, 2024, '2024-09-01', '2025-06-30', 'ACTIVE');


-- Chèn 3 bản ghi mẫu vào bảng ClassGroup
INSERT INTO [SchoolRules].[dbo].[ClassGroup] ([SchoolID], [ClassGroupName], [Hall], [Slot], [Time], [Status])
VALUES
    (1, N'Khối 10', N'Hội trường A', '1', '07:00:00', 'ACTIVE'),
    (1, N'Khối 11', N'Hội trường B', '1', '07:00:00', 'ACTIVE'),
    (1, N'Khối 12', N'Hội trường C', '1', '07:00:00', 'ACTIVE');


-- Chèn 20 bản ghi mẫu vào bảng Student
INSERT INTO [dbo].[Student] ([SchoolID], [Code], [Name], [Sex], [Birthday], [Address], [Phone])
VALUES
(1, 'BT001', 'Nguyen Van A', 1, '2005-01-15', '123 Nguyen Trai, Ha Noi', '8412345678'),
(1, 'BT002', 'Tran Thi B', 0, '2006-03-22', '456 Le Loi, Ha Noi', '8412345679'),
(1, 'BT003', 'Le Van C', 1, '2005-05-10', '789 Hoang Hoa Tham, Ha Noi', '8412345680'),
(1, 'BT004', 'Pham Thi D', 0, '2006-07-18', '101 Tran Phu, Ha Noi', '8412345681'),
(1, 'BT005', 'Hoang Van E', 1, '2005-09-25', '202 Nguyen Hue, Ha Noi', '8412345682'),
(1, 'BT006', 'Vu Thi F', 0, '2006-11-30', '303 Le Thanh Tong, Ha Noi', '8412345683'),
(1, 'BT007', 'Pham Van G', 1, '2005-12-05', '404 Ba Trieu, Ha Noi', '8412345684'),
(1, 'BT008', 'Ngo Thi H', 0, '2006-01-19', '505 Hai Ba Trung, Ha Noi', '8412345685'),
(1, 'BT009', 'Dao Van I', 1, '2005-04-15', '606 Ly Thuong Kiet, Ha Noi', '8412345686'),
(1, 'BT010', 'Do Thi J', 0, '2006-06-12', '707 Hang Bai, Ha Noi', '8412345687'),
(1, 'BT011', 'Phan Van K', 1, '2005-08-20', '808 Tran Hung Dao, Ha Noi', '8412345688'),
(1, 'BT012', 'Dinh Thi L', 0, '2006-10-22', '909 Ba Dinh, Ha Noi', '8412345689'),
(1, 'BT013', 'Nguyen Van M', 1, '2005-03-15', '123 Cau Giay, Ha Noi', '8412345690'),
(1, 'BT014', 'Tran Thi N', 0, '2006-05-25', '456 Thanh Xuan, Ha Noi', '8412345691'),
(1, 'BT015', 'Le Van O', 1, '2005-07-10', '789 Tay Ho, Ha Noi', '8412345692'),
(1, 'BT016', 'Pham Thi P', 0, '2006-09-19', '101 Dong Da, Ha Noi', '8412345693'),
(1, 'BT017', 'Hoang Van Q', 1, '2005-11-30', '202 Long Bien, Ha Noi', '8412345694'),
(1, 'BT018', 'Vu Thi R', 0, '2006-02-12', '303 Hoan Kiem, Ha Noi', '8412345695'),
(1, 'BT019', 'Pham Van S', 1, '2005-05-18', '404 Hoang Mai, Ha Noi', '8412345696'),
(1, 'BT020', 'Ngo Thi T', 0, '2006-07-25', '505 Gia Lam, Ha Noi', '8412345697');


-- Chèn 6 bản ghi mẫu vào bảng Class 
INSERT INTO [SchoolRules].[dbo].[Class] (SchoolYearID, ClassGroupID, Code, Name, TotalPoint)
VALUES 
	(1, 1, 'TE001', '10A1', 100),
	(1, 2, 'EL001', '11A1', 100),
	(1, 3, 'TW001', '12A1', 100),
	(2, 1, 'TE002', '10A1', 100),
	(2, 2, 'EL002', '11A1', 100),
	(2, 3, 'TW002', '12A1', 100);




-- Chèn 20 bản ghi mẫu vào bảng StudentInClass
INSERT INTO [dbo].[StudentInClass] ([ClassID], [StudentID], [EnrollDate], [IsSupervisor], [StartDate], [EndDate], [NumberOfViolation], [Status])
VALUES
(1, 1, '2023-09-01', 1, '2023-09-01', '2024-05-31', 0, 'Active'),
(2, 2, '2023-09-01', 1, '2023-09-01', '2024-05-31', 0, 'Active'),
(3, 3, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'Active'),
(4, 4, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'Active'),
(1, 5, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'Active'),
(2, 6, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'Active'),
(3, 7, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'Active'),
(4, 8, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'Active'),
(1, 9, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'Active'),
(2, 10, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'Active'),
(3, 11, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'Active'),
(4, 12, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'Active'),
(1, 13, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'Active'),
(2, 14, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'Active'),
(3, 15, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'Active'),
(4, 16, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'Active'),
(1, 17, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'Active'),
(2, 18, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'Active'),
(3, 19, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'Active'),
(4, 20, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'Active');

-- Chèn 6 bản ghi mẫu vào bảng ViolationGroup
INSERT INTO [dbo].[ViolationGroup] ([SchoolID], [Name], [Description], [Status])
VALUES
(1, N'Vi phạm chuyên cần', N'Không tuân thủ quy định về sự hiện diện và tham gia hoạt động của nhà trường.', 'ACTIVE'),
(1, N'Vi phạm nề nếp', N'Không tuân thủ quy định chung về trật tự, nề nếp trong trường học.', 'ACTIVE'),
(1, N'Vi phạm học tập - thi cử', N'Gian lận hoặc không trung thực trong quá trình học tập và thi cử.', 'ACTIVE'),
(1, N'Vi phạm đạo đức', N'Hành vi không đúng mực, trái với đạo đức và giá trị của nhà trường.', 'ACTIVE'),
(1, N'Vi phạm môi trường và tài sản chung', N'Gây hại hoặc làm mất trật tự môi trường học tập và tài sản chung.', 'ACTIVE'),
(1, N'Vi phạm tác phong', N'Không tuân thủ quy định về tác phong, lối sống trong trường học.', 'ACTIVE');

-- Chèn 58 bản ghi mẫu vào bảng ViolationType
INSERT INTO [dbo].[ViolationType] ([ViolationGroupID], [Name], [Description], [Status])
VALUES
-- Vi phạm chuyên cần
(1, N'Nghỉ học có phép/không phép', N'Không tuân thủ quy định về sự hiện diện.', 'ACTIVE'),
(1, N'Đi học trễ', N'Không tuân thủ quy định về sự hiện diện.', 'ACTIVE'),
(1, N'Bỏ tiết/trốn tiết', N'Không tuân thủ quy định về sự hiện diện.', 'ACTIVE'),
(1, N'Nghỉ buổi lao động có phép/không phép', N'Không tuân thủ quy định về sự hiện diện.', 'ACTIVE'),

-- Vi phạm nề nếp
(2, N'Đi lại trên hành lang, ngoài sân trong giờ học', N'Không tuân thủ quy định về trật tự, nề nếp.', 'ACTIVE'),
(2, N'Gây ồn ào, mất trật tự', N'Không tuân thủ quy định về trật tự, nề nếp.', 'ACTIVE'),
(2, N'Leo rào, trèo tường', N'Không tuân thủ quy định về trật tự, nề nếp.', 'ACTIVE'),
(2, N'Đi vệ sinh sai nơi quy định', N'Không tuân thủ quy định về trật tự, nề nếp.', 'ACTIVE'),
(2, N'Để xe sai quy định', N'Không tuân thủ quy định về trật tự, nề nếp.', 'ACTIVE'),
(2, N'Đưa người lạ mặt vào trường', N'Không tuân thủ quy định về trật tự, nề nếp.', 'ACTIVE'),
(2, N'Mang điện thoại, tư trang quý vào trường', N'Không tuân thủ quy định về trật tự, nề nếp.', 'ACTIVE'),
(2, N'Vi phạm luật giao thông', N'Không tuân thủ quy định về trật tự, nề nếp.', 'ACTIVE'),
(2, N'Uống rượu, hút thuốc, sử dụng chất kích thích gây nghiện', N'Không tuân thủ quy định về trật tự, nề nếp.', 'ACTIVE'),
(2, N'Cờ bạc', N'Không tuân thủ quy định về trật tự, nề nếp.', 'ACTIVE'),

-- Vi phạm học tập - thi cử
(3, N'Không mang đủ sách vở, dụng cụ học tập', N'Gian lận hoặc không trung thực trong quá trình học tập và thi cử.', 'ACTIVE'),
(3, N'Không chú ý nghe giảng', N'Gian lận hoặc không trung thực trong quá trình học tập và thi cử.', 'ACTIVE'),
(3, N'Không chép bài đầy đủ', N'Gian lận hoặc không trung thực trong quá trình học tập và thi cử.', 'ACTIVE'),
(3, N'Không làm bài tập về nhà', N'Gian lận hoặc không trung thực trong quá trình học tập và thi cử.', 'ACTIVE'),
(3, N'Nói chuyện, làm việc riêng trong giờ học', N'Gian lận hoặc không trung thực trong quá trình học tập và thi cử.', 'ACTIVE'),
(3, N'Ngồi không đúng vị trí trong lớp', N'Gian lận hoặc không trung thực trong quá trình học tập và thi cử.', 'ACTIVE'),
(3, N'Tự ý ra khỏi lớp trong giờ học', N'Gian lận hoặc không trung thực trong quá trình học tập và thi cử.', 'ACTIVE'),
(3, N'Quay cóp trong giờ kiểm tra', N'Gian lận hoặc không trung thực trong quá trình học tập và thi cử.', 'ACTIVE'),
(3, N'Sử dụng tài liệu trong lúc thi', N'Gian lận hoặc không trung thực trong quá trình học tập và thi cử.', 'ACTIVE'),

-- Vi phạm đạo đức
(4, N'Vô lễ với thầy cô giáo', N'Hành vi không đúng mực, trái với đạo đức và giá trị của nhà trường.', 'ACTIVE'),
(4, N'Nói tục, chửi thề', N'Hành vi không đúng mực, trái với đạo đức và giá trị của nhà trường.', 'ACTIVE'),
(4, N'Nói dối, gian lận', N'Hành vi không đúng mực, trái với đạo đức và giá trị của nhà trường.', 'ACTIVE'),
(4, N'Ăn vạ, ăn cắp', N'Hành vi không đúng mực, trái với đạo đức và giá trị của nhà trường.', 'ACTIVE'),
(4, N'Bắt nạt, ngược đãi bạn học', N'Hành vi không đúng mực, trái với đạo đức và giá trị của nhà trường.', 'ACTIVE'),
(4, N'Gây sự, đánh nhau', N'Hành vi không đúng mực, trái với đạo đức và giá trị của nhà trường.', 'ACTIVE'),
(4, N'Ăn mặc phản cảm', N'Hành vi không đúng mực, trái với đạo đức và giá trị của nhà trường.', 'ACTIVE'),
(4, N'Lưu hành văn hóa phẩm đồi trụy', N'Hành vi không đúng mực, trái với đạo đức và giá trị của nhà trường.', 'ACTIVE'),
(4, N'Sử dụng mạng xã hội một cách tiêu cực', N'Hành vi không đúng mực, trái với đạo đức và giá trị của nhà trường.', 'ACTIVE'),
(4, N'Không tham gia hoạt động tình nguyện', N'Hành vi không đúng mực, trái với đạo đức và giá trị của nhà trường.', 'ACTIVE'),

-- Vi phạm môi trường và tài sản chung
(5, N'Xả rác bừa bãi', N'Gây hại hoặc làm mất trật tự môi trường học tập và tài sản chung.', 'ACTIVE'),
(5, N'Mang đồ ăn vào trong lớp', N'Gây hại hoặc làm mất trật tự môi trường học tập và tài sản chung.', 'ACTIVE'),
(5, N'Viết bậy lên mặt bàn/tường', N'Gây hại hoặc làm mất trật tự môi trường học tập và tài sản chung.', 'ACTIVE'),
(5, N'Bẻ hoa, cây cảnh trong khuôn viên trường', N'Gây hại hoặc làm mất trật tự môi trường học tập và tài sản chung.', 'ACTIVE'),
(5, N'Phung phí điện nước của trường', N'Gây hại hoặc làm mất trật tự môi trường học tập và tài sản chung.', 'ACTIVE'),
(5, N'Phá hoại/làm mất tài sản của trường học', N'Gây hại hoặc làm mất trật tự môi trường học tập và tài sản chung.', 'ACTIVE'),
(5, N'Trộm cắp tài sản của bạn học, nhà trường', N'Gây hại hoặc làm mất trật tự môi trường học tập và tài sản chung.', 'ACTIVE'),
(5, N'Chiếm đoạt hoặc sử dụng trái phép tài sản', N'Gây hại hoặc làm mất trật tự môi trường học tập và tài sản chung.', 'ACTIVE'),

-- Vi phạm tác phong
(6, N'Không mặc đồng phục', N'Không tuân thủ quy định về tác phong.', 'ACTIVE'),
(6, N'Mặc đồng phục không chuẩn', N'Không tuân thủ quy định về tác phong.', 'ACTIVE'),
(6, N'Thay đổi về đồng phục', N'Không tuân thủ quy định về tác phong.', 'ACTIVE'),
(6, N'Đeo phụ kiện trái phép', N'Không tuân thủ quy định về tác phong.', 'ACTIVE'),
(6, N'Mặc đồng phục bẩn hoặc hư hỏng', N'Không tuân thủ quy định về tác phong.', 'ACTIVE'),
(6, N'Mặc quá lố hoặc lòe loẹt', N'Không tuân thủ quy định về tác phong.', 'ACTIVE'),
(6, N'Mang giày bẩn hoặc nhếch nhác', N'Không tuân thủ quy định về tác phong.', 'ACTIVE'),
(6, N'Mang giày hư', N'Không tuân thủ quy định về tác phong.', 'ACTIVE'),
(6, N'Mang giày không an toàn', N'Không tuân thủ quy định về tác phong.', 'ACTIVE'),
(6, N'Mang giày không phù hợp với hoạt động', N'Không tuân thủ quy định về tác phong.', 'ACTIVE'),
(6, N'Mang giày không liên quan đến môi trường học tập', N'Không tuân thủ quy định về tác phong.', 'ACTIVE'),
(6, N'Kiểu tóc không phù hợp', N'Không tuân thủ quy định về tác phong.', 'ACTIVE'),
(6, N'Màu tóc không phù hợp', N'Không tuân thủ quy định về tác phong.', 'ACTIVE'),
(6, N'Phụ kiện tóc trái phép', N'Không tuân thủ quy định về tác phong.', 'ACTIVE'),
(6, N'Tóc bẩn hoặc bù xù', N'Không tuân thủ quy định về tác phong.', 'ACTIVE'),
(6, N'Son móng tay', N'Không tuân thủ quy định về tác phong.', 'ACTIVE'),
(6, N'Trang điểm quá lố', N'Không tuân thủ quy định về tác phong.', 'ACTIVE');

-- Chèn 40 bản ghi mẫu vào bảng Violation
INSERT INTO [dbo].[Violation] ([ClassID], [ViolationTypeID], [StudentInClassID], [TeacherID], [Name], [Description], [Date], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [Status])
VALUES
-- Class 1 Violations
(1, 2, 1, 1, N'Nói chuyện riêng', N'Học sinh nói chuyện không xin phép trong giờ học.', '2021-01-10 10:30:00', '2021-01-10', 1, NULL, NULL, 'PENDING'),
(1, 3, 2, 1, N'Ngôn ngữ không phù hợp', N'Học sinh sử dụng ngôn ngữ không phù hợp trong lớp học.', '2021-02-15 11:00:00', '2021-02-15', 1, NULL, NULL, 'PENDING'),
(1, 4, 3, 1, N'Không tuân theo chỉ dẫn', N'Học sinh không tuân theo chỉ dẫn của giáo viên.', '2021-03-01 09:00:00', '2021-03-01', 1, NULL, NULL, 'PENDING'),
(1, 1, 4, 1, N'Nghỉ học không phép', N'Học sinh vắng mặt không có lý do.', '2021-03-05 09:00:00', '2021-03-05', 1, NULL, NULL, 'PENDING'),
(1, 1, 5, 1, N'Đi học trễ', N'Học sinh đi học trễ nhiều lần.', '2021-03-10 08:45:00', '2021-03-10', 1, NULL, NULL, 'PENDING'),
(1, 1, 1, 1, N'Bỏ tiết/trốn tiết', N'Học sinh cố tình bỏ tiết học.', '2021-03-15 10:00:00', '2021-03-15', 1, NULL, NULL, 'PENDING'),
(1, 6, 2, 1, N'Mặc không đúng quy định', N'Học sinh không tuân thủ quy định về trang phục.', '2021-04-20 08:00:00', '2021-04-20', 1, NULL, NULL, 'PENDING'),
(1, 6, 3, 1, N'Phụ kiện không phù hợp', N'Học sinh đeo phụ kiện không được phép bởi nhà trường.', '2021-04-22 08:15:00', '2021-04-22', 1, NULL, NULL, 'PENDING'),
(1, 3, 4, 1, N'Quay cóp', N'Học sinh bị bắt quay cóp trong kỳ thi.', '2021-05-05 10:00:00', '2021-05-05', 1, NULL, NULL, 'PENDING'),
(1, 3, 5, 1, N'Đạo văn', N'Học sinh nộp bài có nội dung đạo văn.', '2021-05-07 14:00:00', '2021-05-07', 1, NULL, NULL, 'PENDING'),

-- Class 2 Violations
(2, 2, 6, 1, N'Nói chuyện riêng', N'Học sinh nói chuyện không xin phép trong giờ học.', '2021-06-01 12:00:00', '2021-06-01', 1, NULL, NULL, 'PENDING'),
(2, 3, 7, 1, N'Ngôn ngữ không phù hợp', N'Học sinh sử dụng ngôn ngữ không phù hợp trong lớp học.', '2021-06-10 11:00:00', '2021-06-10', 1, NULL, NULL, 'PENDING'),
(2, 4, 8, 1, N'Không tuân theo chỉ dẫn', N'Học sinh không tuân theo chỉ dẫn của giáo viên.', '2021-07-01 09:00:00', '2021-07-01', 1, NULL, NULL, 'PENDING'),
(2, 1, 9, 1, N'Nghỉ học không phép', N'Học sinh vắng mặt không có lý do.', '2021-07-05 09:00:00', '2021-07-05', 1, NULL, NULL, 'PENDING'),
(2, 1, 10, 1, N'Đi học trễ', N'Học sinh đi học trễ nhiều lần.', '2021-07-10 08:45:00', '2021-07-10', 1, NULL, NULL, 'PENDING'),
(2, 1, 6, 1, N'Bỏ tiết/trốn tiết', N'Học sinh cố tình bỏ tiết học.', '2021-07-15 10:00:00', '2021-07-15', 1, NULL, NULL, 'PENDING'),
(2, 6, 7, 1, N'Mặc không đúng quy định', N'Học sinh không tuân thủ quy định về trang phục.', '2021-07-20 08:00:00', '2021-07-20', 1, NULL, NULL, 'PENDING'),
(2, 6, 8, 1, N'Phụ kiện không phù hợp', N'Học sinh đeo phụ kiện không được phép bởi nhà trường.', '2021-07-25 08:15:00', '2021-07-25', 1, NULL, NULL, 'PENDING'),
(2, 3, 9, 1, N'Quay cóp', N'Học sinh bị bắt quay cóp trong kỳ thi.', '2021-08-01 10:00:00', '2021-08-01', 1, NULL, NULL, 'PENDING'),
(2, 3, 10, 1, N'Đạo văn', N'Học sinh nộp bài có nội dung đạo văn.', '2021-08-05 14:00:00', '2021-08-05', 1, NULL, NULL, 'PENDING'),

-- Class 3 Violations
(3, 2, 11, 1, N'Nói chuyện riêng', N'Học sinh nói chuyện không xin phép trong giờ học.', '2021-08-10 10:30:00', '2021-08-10', 1, NULL, NULL, 'PENDING'),
(3, 3, 12, 1, N'Ngôn ngữ không phù hợp', N'Học sinh sử dụng ngôn ngữ không phù hợp trong lớp học.', '2021-08-15 11:00:00', '2021-08-15', 1, NULL, NULL, 'PENDING'),
(3, 4, 13, 1, N'Không tuân theo chỉ dẫn', N'Học sinh không tuân theo chỉ dẫn của giáo viên.', '2021-08-20 09:00:00', '2021-08-20', 1, NULL, NULL, 'PENDING'),
(3, 1, 14, 1, N'Nghỉ học không phép', N'Học sinh vắng mặt không có lý do.', '2021-08-25 09:00:00', '2021-08-25', 1, NULL, NULL, 'PENDING'),
(3, 1, 15, 1, N'Đi học trễ', N'Học sinh đi học trễ nhiều lần.', '2021-08-30 08:45:00', '2021-08-30', 1, NULL, NULL, 'PENDING'),
(3, 1, 11, 1, N'Bỏ tiết/trốn tiết', N'Học sinh cố tình bỏ tiết học.', '2021-09-05 10:00:00', '2021-09-05', 1, NULL, NULL, 'PENDING'),
(3, 6, 12, 1, N'Mặc không đúng quy định', N'Học sinh không tuân thủ quy định về trang phục.', '2021-09-10 08:00:00', '2021-09-10', 1, NULL, NULL, 'PENDING'),
(3, 6, 13, 1, N'Phụ kiện không phù hợp', N'Học sinh đeo phụ kiện không được phép bởi nhà trường.', '2021-09-15 08:15:00', '2021-09-15', 1, NULL, NULL, 'PENDING'),
(3, 3, 14, 1, N'Quay cóp', N'Học sinh bị bắt quay cóp trong kỳ thi.', '2021-09-20 10:00:00', '2021-09-20', 1, NULL, NULL, 'PENDING'),
(3, 3, 15, 1, N'Đạo văn', N'Học sinh nộp bài có nội dung đạo văn.', '2021-09-25 14:00:00', '2021-09-25', 1, NULL, NULL, 'PENDING'),

-- Class 4 Violations
(4, 2, 16, 1, N'Nói chuyện riêng', N'Học sinh nói chuyện không xin phép trong giờ học.', '2022-10-01 12:00:00', '2022-10-01', 1, NULL, NULL, 'PENDING'),
(4, 3, 17, 1, N'Ngôn ngữ không phù hợp', N'Học sinh sử dụng ngôn ngữ không phù hợp trong lớp học.', '2022-10-05 11:00:00', '2022-10-05', 1, NULL, NULL, 'PENDING'),
(4, 4, 18, 1, N'Không tuân theo chỉ dẫn', N'Học sinh không tuân theo chỉ dẫn của giáo viên.', '2022-10-10 09:00:00', '2022-10-10', 1, NULL, NULL, 'PENDING'),
(4, 1, 19, 1, N'Nghỉ học không phép', N'Học sinh vắng mặt không có lý do.', '2022-10-15 09:00:00', '2022-10-15', 1, NULL, NULL, 'PENDING'),
(4, 1, 20, 1, N'Đi học trễ', N'Học sinh đi học trễ nhiều lần.', '2022-10-20 08:45:00', '2022-10-20', 1, NULL, NULL, 'PENDING'),
(4, 1, 16, 1, N'Bỏ tiết/trốn tiết', N'Học sinh cố tình bỏ tiết học.', '2022-10-25 10:00:00', '2022-10-25', 1, NULL, NULL, 'PENDING'),
(4, 6, 17, 1, N'Mặc không đúng quy định', N'Học sinh không tuân thủ quy định về trang phục.', '2022-10-30 08:00:00', '2022-10-30', 1, NULL, NULL, 'PENDING'),
(4, 6, 18, 1, N'Phụ kiện không phù hợp', N'Học sinh đeo phụ kiện không được phép bởi nhà trường.', '2022-11-01 08:15:00', '2022-11-01', 1, NULL, NULL, 'PENDING'),
(4, 3, 19, 1, N'Quay cóp', N'Học sinh bị bắt quay cóp trong kỳ thi.', '2022-11-05 10:00:00', '2022-11-05', 1, NULL, NULL, 'PENDING'),
(4, 3, 20, 1, N'Đạo văn', N'Học sinh nộp bài có nội dung đạo văn.', '2022-11-10 14:00:00', '2022-11-10', 1, NULL, NULL, 'PENDING');



-- Chèn 40 bản ghi mẫu vào bảng Penalty
INSERT INTO [SchoolRules].[dbo].[Penalty] ([SchoolID], [Name], [Description], [Status])
VALUES
(1, N'Cảnh cáo', N'Cảnh cáo bằng lời hoặc văn bản.', 'ACTIVE'),
(1, N'Phạt lao động', N'Yêu cầu học sinh tham gia các hoạt động lao động công ích.', 'ACTIVE'),
(1, N'Phạt viết bài kiểm điểm', N'Yêu cầu học sinh viết bài kiểm điểm.', 'ACTIVE'),
(1, N'Phạt đuổi học', N'Đuổi học tạm thời hoặc vĩnh viễn.', 'ACTIVE'),
(1, N'Phạt đình chỉ', N'Đình chỉ học tập trong một khoảng thời gian.', 'ACTIVE');



-- Chèn 40 bản ghi mẫu vào bảng Discipline
INSERT INTO [SchoolRules].[dbo].[Discipline] ([ViolationID], [PennaltyID], [Description], [StartDate], [EndDate], [Status])
VALUES
-- ViolationID 1 to 10 from Class 1
(1, 1, N'Cảnh cáo bằng lời hoặc văn bản.', '2024-01-11', '2024-01-11', 'ACTIVE'),
(2, 2, N'Yêu cầu học sinh tham gia các hoạt động lao động công ích.', '2024-02-16', '2024-02-16', 'ACTIVE'),
(3, 3, N'Yêu cầu học sinh viết bài kiểm điểm.', '2024-03-02', '2024-03-02', 'ACTIVE'),
(4, 4, N'Đuổi học tạm thời hoặc vĩnh viễn.', '2024-03-06', '2024-03-06', 'ACTIVE'),
(5, 5, N'Đình chỉ học tập trong một khoảng thời gian.', '2024-03-11', '2024-03-11', 'ACTIVE'),

-- ViolationID 11 to 20 from Class 2
(11, 1, N'Cảnh cáo bằng lời hoặc văn bản.', '2024-06-02', '2024-06-02', 'ACTIVE'),
(12, 2, N'Yêu cầu học sinh tham gia các hoạt động lao động công ích.', '2024-06-11', '2024-06-11', 'ACTIVE'),
(13, 3, N'Yêu cầu học sinh viết bài kiểm điểm.', '2024-07-02', '2024-07-02', 'ACTIVE'),
(14, 4, N'Đuổi học tạm thời hoặc vĩnh viễn.', '2024-07-06', '2024-07-06', 'ACTIVE'),
(15, 5, N'Đình chỉ học tập trong một khoảng thời gian.', '2024-07-11', '2024-07-11', 'ACTIVE'),

-- ViolationID 21 to 30 from Class 3
(21, 1, N'Cảnh cáo bằng lời hoặc văn bản.', '2024-08-11', '2024-08-11', 'ACTIVE'),
(22, 2, N'Yêu cầu học sinh tham gia các hoạt động lao động công ích.', '2024-08-16', '2024-08-16', 'ACTIVE'),
(23, 3, N'Yêu cầu học sinh viết bài kiểm điểm.', '2024-08-21', '2024-08-21', 'ACTIVE'),
(24, 4, N'Đuổi học tạm thời hoặc vĩnh viễn.', '2024-08-26', '2024-08-26', 'ACTIVE'),
(25, 5, N'Đình chỉ học tập trong một khoảng thời gian.', '2024-08-31', '2024-08-31', 'ACTIVE'),

-- ViolationID 31 to 40 from Class 4
(31, 1, N'Cảnh cáo bằng lời hoặc văn bản.', '2024-10-02', '2024-10-02', 'ACTIVE'),
(32, 2, N'Yêu cầu học sinh tham gia các hoạt động lao động công ích.', '2024-10-06', '2024-10-06', 'ACTIVE'),
(33, 3, N'Yêu cầu học sinh viết bài kiểm điểm.', '2024-10-11', '2024-10-11', 'ACTIVE'),
(34, 4, N'Đuổi học tạm thời hoặc vĩnh viễn.', '2024-10-16', '2024-10-16', 'ACTIVE'),
(35, 5, N'Đình chỉ học tập trong một khoảng thời gian.', '2024-10-21', '2024-10-21', 'ACTIVE');


-- Chèn 40 bản ghi mẫu vào bảng ViolationConfig
INSERT INTO [SchoolRules].[dbo].[ViolationConfig] ([ViolationTypeID], [MinusPoints], [Description], [Status])
VALUES
-- Disruptive Behavior
(1, 5, N'Nói chuyện không đúng lúc trong lớp học', 'ACTIVE'),
(2, 10, N'Sử dụng ngôn ngữ không phù hợp', 'ACTIVE'),
(3, 15, N'Không tuân theo hướng dẫn của giáo viên hoặc nhân viên', 'ACTIVE'),

-- Attendance Issues
(4, 20, N'Vắng học không phép', 'ACTIVE'),
(5, 10, N'Đi học muộn nhiều lần', 'ACTIVE'),
(6, 15, N'Trốn học', 'ACTIVE'),

-- Dress Code Violations
(7, 5, N'Mặc quần áo không đúng quy định', 'ACTIVE'),
(8, 5, N'Dùng phụ kiện không được phép', 'ACTIVE'),

-- Academic Dishonesty
(9, 25, N'Gian lận trong thi cử', 'ACTIVE'),
(10, 20, N'Đạo văn', 'ACTIVE'),

-- Bullying
(11, 20, N'Bắt nạt bằng lời nói', 'ACTIVE'),
(12, 25, N'Bắt nạt bằng hành động', 'ACTIVE'),
(13, 20, N'Bắt nạt qua mạng', 'ACTIVE');

-- Chèn 2 bản ghi mẫu vào bảng PackageType
INSERT INTO [SchoolRules].[dbo].[PackageType] ([Name], [Description], [Status])
VALUES
(N'Gói Tiêu Chuẩn', N'Gói tiêu chuẩn gồm 2 loại: Thường và Vip', 'ACTIVE'),
(N'Gói Bổ Sung', N'Gói bổ sung được sử dụng trong trường hợp gói tiêu chuẩn không đủ đáp ứng nhu cầu theo quy mô của nhà trường', 'ACTIVE');


-- Chèn 2 bản ghi mẫu vào bảng Package
INSERT INTO [SchoolRules].[dbo].[Package] ([PackageTypeID], [Name], [Description], [TotalStudents], [TotalViolations], [Price], [Status])
VALUES
(1, N'Gói Thường', N'Gói thường phù hợp cho các trường quy mô vừa và nhỏ, số lượng vi phạm và học sinh nằm ở mức tiêu chuẩn, nếu quy mô nhà trường có thể mở rộng trong tương lai hãy cân nhắc đăng ký gói Vip', 500, 1000, 2500000, 'ACTIVE'),
(1, N'Gói Vip', N'Gói Vip với giới hạn học sinh và vi phạm cao hơn gói thường, phù hợp cho các trường có quy mô lớn và có khả năng mở rộng trong tương lai', 900, 18000, 4000000, 'ACTIVE'),
(2, N'Gói Bổ Sung Violation', N'Gói bổ sung cho số lượng Violation', 0, 200, 400000, 'ACTIVE'),
(2, N'Gói Bổ Sung Student', N'Gói bổ sung cho số lượng Student', 100, 0, 400000, 'ACTIVE');


-- Chèn 3 bản ghi mẫu vào bảng RegisteredSchool
INSERT INTO [SchoolRules].[dbo].[RegisteredSchool] ([SchoolID], [RegisteredDate], [Description], [Status])
VALUES
(1, '2023-01-10', N'Trường đã đăng ký tham gia hệ thống quản lý vi phạm.', 'ACTIVE'),
(1, '2023-03-01', N'Trường đã hoàn tất quy trình đăng ký và bắt đầu sử dụng hệ thống.', 'INACTIVE'),
(1, '2023-04-05', N'Trường đang trong giai đoạn thử nghiệm các tính năng mới.', 'ACTIVE');


-- Chèn 16 bản ghi mẫu vào bảng YearPackage
INSERT INTO [SchoolRules].[dbo].[YearPackage] ([SchoolYearID], [PackageID], [NumberOfStudent], [Status])
VALUES
    -- Dữ liệu cho năm học 2021
    (1, 1, 450, 'ACTIVE'),
    (1, 2, 850, 'ACTIVE'),
    (1, 3, 0, 'ACTIVE'),
    (1, 4, 90, 'ACTIVE'),

    -- Dữ liệu cho năm học 2022
    (2, 1, 460, 'ACTIVE'),
    (2, 2, 880, 'ACTIVE'),
    (2, 3, 0, 'ACTIVE'),
    (2, 4, 95, 'ACTIVE'),

    -- Dữ liệu cho năm học 2023
    (3, 1, 470, 'ACTIVE'),
    (3, 2, 890, 'ACTIVE'),
    (3, 3, 0, 'ACTIVE'),
    (3, 4, 100, 'ACTIVE'),

    -- Dữ liệu cho năm học 2024
    (4, 1, 480, 'ACTIVE'),
    (4, 2, 900, 'ACTIVE'),
    (4, 3, 0, 'ACTIVE'),
    (4, 4, 110, 'ACTIVE');
-- Chèn 12 bản ghi mẫu vào bảng PatrolSchedule
INSERT INTO [SchoolRules].[dbo].[PatrolSchedule] ([ClassID], [SupervisorID], [TeacherID], [From], [To], [Status])
VALUES
    -- Lịch tuần tra cho lớp 10A1 năm học 2021
    (1, 1, 1, '2021-09-01', '2022-06-30', 'ACTIVE'),
    (1, 2, 1, '2021-09-01', '2022-06-30', 'ACTIVE'),

    -- Lịch tuần tra cho lớp 11A1 năm học 2021
    (2, 1, 1, '2021-09-01', '2022-06-30', 'ACTIVE'),
    (2, 2, 1, '2021-09-01', '2022-06-30', 'ACTIVE'),

    -- Lịch tuần tra cho lớp 12A1 năm học 2021
    (3, 1, 1, '2021-09-01', '2022-06-30', 'ACTIVE'),
    (3, 2, 1, '2021-09-01', '2022-06-30', 'ACTIVE'),

    -- Lịch tuần tra cho lớp 10A1 năm học 2022
    (4, 1, 1, '2022-09-01', '2023-06-30', 'ACTIVE'),
    (4, 2, 1, '2022-09-01', '2023-06-30', 'ACTIVE'),

    -- Lịch tuần tra cho lớp 11A1 năm học 2022
    (5, 1, 1, '2022-09-01', '2023-06-30', 'ACTIVE'),
    (5, 2, 1, '2022-09-01', '2023-06-30', 'ACTIVE'),

    -- Lịch tuần tra cho lớp 12A1 năm học 2022
    (6, 1, 1, '2022-09-01', '2023-06-30', 'ACTIVE'),
    (6, 2, 1, '2022-09-01', '2023-06-30', 'ACTIVE');

-- Chèn 12 bản ghi mẫu vào bảng ImageURL
INSERT INTO [SchoolRules].[dbo].[ImageURL] ([ViolationID], [URL], [Name], [Description])
VALUES
    -- Liên kết ảnh cho các vi phạm của lớp 1
    (1, 'http://example.com/images/violation1.jpg', N'Ảnh vi phạm Nói chuyện riêng', N'Ảnh minh họa vi phạm Nói chuyện riêng của học sinh.'),
    (2, 'http://example.com/images/violation2.jpg', N'Ảnh vi phạm Ngôn ngữ không phù hợp', N'Ảnh minh họa vi phạm Ngôn ngữ không phù hợp của học sinh.'),
    (3, 'http://example.com/images/violation3.jpg', N'Ảnh vi phạm Không tuân theo chỉ dẫn', N'Ảnh minh họa vi phạm Không tuân theo chỉ dẫn của học sinh.'),
    (4, 'http://example.com/images/violation4.jpg', N'Ảnh vi phạm Nghỉ học không phép', N'Ảnh minh họa vi phạm Nghỉ học không phép của học sinh.'),
    (5, 'http://example.com/images/violation5.jpg', N'Ảnh vi phạm Đi học trễ', N'Ảnh minh họa vi phạm Đi học trễ của học sinh.'),
    (6, 'http://example.com/images/violation6.jpg', N'Ảnh vi phạm Bỏ tiết/trốn tiết', N'Ảnh minh họa vi phạm Bỏ tiết/trốn tiết của học sinh.'),
    (7, 'http://example.com/images/violation7.jpg', N'Ảnh vi phạm Mặc không đúng quy định', N'Ảnh minh họa vi phạm Mặc không đúng quy định của học sinh.'),
    (8, 'http://example.com/images/violation8.jpg', N'Ảnh vi phạm Phụ kiện không phù hợp', N'Ảnh minh họa vi phạm Phụ kiện không phù hợp của học sinh.'),
    (9, 'http://example.com/images/violation9.jpg', N'Ảnh vi phạm Quay cóp', N'Ảnh minh họa vi phạm Quay cóp của học sinh.'),
    (10, 'http://example.com/images/violation10.jpg', N'Ảnh vi phạm Đạo văn', N'Ảnh minh họa vi phạm Đạo văn của học sinh.'),

    -- Liên kết ảnh cho các vi phạm của lớp 2
    (11, 'http://example.com/images/violation11.jpg', N'Ảnh vi phạm Nói chuyện riêng', N'Ảnh minh họa vi phạm Nói chuyện riêng của học sinh.'),
    (12, 'http://example.com/images/violation12.jpg', N'Ảnh vi phạm Ngôn ngữ không phù hợp', N'Ảnh minh họa vi phạm Ngôn ngữ không phù hợp của học sinh.'),
    (13, 'http://example.com/images/violation13.jpg', N'Ảnh vi phạm Không tuân theo chỉ dẫn', N'Ảnh minh họa vi phạm Không tuân theo chỉ dẫn của học sinh.'),
    (14, 'http://example.com/images/violation14.jpg', N'Ảnh vi phạm Nghỉ học không phép', N'Ảnh minh họa vi phạm Nghỉ học không phép của học sinh.'),
    (15, 'http://example.com/images/violation15.jpg', N'Ảnh vi phạm Đi học trễ', N'Ảnh minh họa vi phạm Đi học trễ của học sinh.'),
    (16, 'http://example.com/images/violation16.jpg', N'Ảnh vi phạm Bỏ tiết/trốn tiết', N'Ảnh minh họa vi phạm Bỏ tiết/trốn tiết của học sinh.'),
    (17, 'http://example.com/images/violation17.jpg', N'Ảnh vi phạm Mặc không đúng quy định', N'Ảnh minh họa vi phạm Mặc không đúng quy định của học sinh.'),
    (18, 'http://example.com/images/violation18.jpg', N'Ảnh vi phạm Phụ kiện không phù hợp', N'Ảnh minh họa vi phạm Phụ kiện không phù hợp của học sinh.'),
    (19, 'http://example.com/images/violation19.jpg', N'Ảnh vi phạm Quay cóp', N'Ảnh minh họa vi phạm Quay cóp của học sinh.'),
    (20, 'http://example.com/images/violation20.jpg', N'Ảnh vi phạm Đạo văn', N'Ảnh minh họa vi phạm Đạo văn của học sinh.'),

    -- Liên kết ảnh cho các vi phạm của lớp 3
    (21, 'http://example.com/images/violation21.jpg', N'Ảnh vi phạm Nói chuyện riêng', N'Ảnh minh họa vi phạm Nói chuyện riêng của học sinh.'),
    (22, 'http://example.com/images/violation22.jpg', N'Ảnh vi phạm Ngôn ngữ không phù hợp', N'Ảnh minh họa vi phạm Ngôn ngữ không phù hợp của học sinh.'),
    (23, 'http://example.com/images/violation23.jpg', N'Ảnh vi phạm Không tuân theo chỉ dẫn', N'Ảnh minh họa vi phạm Không tuân theo chỉ dẫn của học sinh.'),
    (24, 'http://example.com/images/violation24.jpg', N'Ảnh vi phạm Nghỉ học không phép', N'Ảnh minh họa vi phạm Nghỉ học không phép của học sinh.'),
    (25, 'http://example.com/images/violation25.jpg', N'Ảnh vi phạm Đi học trễ', N'Ảnh minh họa vi phạm Đi học trễ của học sinh.'),
    (26, 'http://example.com/images/violation26.jpg', N'Ảnh vi phạm Bỏ tiết/trốn tiết', N'Ảnh minh họa vi phạm Bỏ tiết/trốn tiết của học sinh.'),
    (27, 'http://example.com/images/violation27.jpg', N'Ảnh vi phạm Mặc không đúng quy định', N'Ảnh minh họa vi phạm Mặc không đúng quy định của học sinh.'),
    (28, 'http://example.com/images/violation28.jpg', N'Ảnh vi phạm Phụ kiện không phù hợp', N'Ảnh minh họa vi phạm Phụ kiện không phù hợp của học sinh.'),
    (29, 'http://example.com/images/violation29.jpg', N'Ảnh vi phạm Quay cóp', N'Ảnh minh họa vi phạm Quay cóp của học sinh.'),
    (30, 'http://example.com/images/violation30.jpg', N'Ảnh vi phạm Đạo văn', N'Ảnh minh họa vi phạm Đạo văn của học sinh.'),

    -- Liên kết ảnh cho các vi phạm của lớp 4
    (31, 'http://example.com/images/violation31.jpg', N'Ảnh vi phạm Nói chuyện riêng', N'Ảnh minh họa vi phạm Nói chuyện riêng của học sinh.'),
    (32, 'http://example.com/images/violation32.jpg', N'Ảnh vi phạm Ngôn ngữ không phù hợp', N'Ảnh minh họa vi phạm Ngôn ngữ không phù hợp của học sinh.'),
    (33, 'http://example.com/images/violation33.jpg', N'Ảnh vi phạm Không tuân theo chỉ dẫn', N'Ảnh minh họa vi phạm Không tuân theo chỉ dẫn của học sinh.'),
    (34, 'http://example.com/images/violation34.jpg', N'Ảnh vi phạm Nghỉ học không phép', N'Ảnh minh họa vi phạm Nghỉ học không phép của học sinh.'),
    (35, 'http://example.com/images/violation35.jpg', N'Ảnh vi phạm Đi học trễ', N'Ảnh minh họa vi phạm Đi học trễ của học sinh.'),
    (36, 'http://example.com/images/violation36.jpg', N'Ảnh vi phạm Bỏ tiết/trốn tiết', N'Ảnh minh họa vi phạm Bỏ tiết/trốn tiết của học sinh.'),
    (37, 'http://example.com/images/violation37.jpg', N'Ảnh vi phạm Mặc không đúng quy định', N'Ảnh minh họa vi phạm Mặc không đúng quy định của học sinh.'),
    (38, 'http://example.com/images/violation38.jpg', N'Ảnh vi phạm Phụ kiện không phù hợp', N'Ảnh minh họa vi phạm Phụ kiện không phù hợp của học sinh.'),
    (39, 'http://example.com/images/violation39.jpg', N'Ảnh vi phạm Quay cóp', N'Ảnh minh họa vi phạm Quay cóp của học sinh.'),
    (40, 'http://example.com/images/violation40.jpg', N'Ảnh vi phạm Đạo văn', N'Ảnh minh họa vi phạm Đạo văn của học sinh.');


-- Chèn 12 bản ghi mẫu vào bảng Evaluation
INSERT INTO [SchoolRules].[dbo].[Evaluation] ([SchoolYearID], [ViolationConfigID], [Description], [From], [To], [Point])
VALUES
-- Class 1 Evaluations
(1, 2, N'Nói chuyện riêng', '2021-09-01', '2022-06-30', -10),
(1, 3, N'Ngôn ngữ không phù hợp', '2021-09-01', '2022-06-30', -15),
(1, 4, N'Không tuân theo chỉ dẫn', '2021-09-01', '2022-06-30', -20),
(1, 1, N'Nghỉ học không phép', '2021-09-01', '2022-06-30', -5),
(1, 1, N'Đi học trễ', '2021-09-01', '2022-06-30', -5),
(1, 1, N'Bỏ tiết/trốn tiết', '2021-09-01', '2022-06-30', -5),
(1, 6, N'Mặc không đúng quy định', '2021-09-01', '2022-06-30', -5),
(1, 6, N'Phụ kiện không phù hợp', '2021-09-01', '2022-06-30', -5),
(1, 3, N'Quay cóp', '2021-09-01', '2022-06-30', -15),
(1, 3, N'Đạo văn', '2021-09-01', '2022-06-30', -20),

-- Class 2 Evaluations
(2, 2, N'Nói chuyện riêng', '2021-09-01', '2022-06-30', -10),
(2, 3, N'Ngôn ngữ không phù hợp', '2021-09-01','2022-06-30', -15),
(2, 4, N'Không tuân theo chỉ dẫn', '2021-09-01', '2022-06-30', -20),
(2, 1, N'Nghỉ học không phép', '2021-09-01', '2022-06-30', -5),
(2, 1, N'Đi học trễ', '2021-09-01', '2022-06-30', -5),
(2, 1, N'Bỏ tiết/trốn tiết', '2021-09-01', '2022-06-30', -5),
(2, 6, N'Mặc không đúng quy định', '2021-09-01', '2022-06-30', -5),
(2, 6, N'Phụ kiện không phù hợp', '2021-09-01', '2022-06-30', -5),
(2, 3, N'Quay cóp', '2021-09-01', '2022-06-30', -15),
(2, 3, N'Đạo văn', '2021-09-01', '2022-06-30', -20),

-- Class 3 Evaluations
(3, 2, N'Nói chuyện riêng', '2021-09-01', '2022-06-30', -10),
(3, 3, N'Ngôn ngữ không phù hợp', '2021-09-01', '2022-06-30', -15),
(3, 4, N'Không tuân theo chỉ dẫn', '2021-09-01', '2022-06-30', -20),
(3, 1, N'Nghỉ học không phép', '2021-09-01', '2022-06-30', -5),
(3, 1, N'Đi học trễ', '2021-09-01', '2022-06-30', -5),
(3, 1, N'Bỏ tiết/trốn tiết', '2021-09-01', '2022-06-30', -5),
(3, 6, N'Mặc không đúng quy định', '2021-09-01', '2022-06-30', -5),
(3, 6, N'Phụ kiện không phù hợp', '2021-09-01', '2022-06-30', -5),
(3, 3, N'Quay cóp', '2021-09-01', '2022-06-30', -15),
(3, 3, N'Đạo văn', '2021-09-01', '2022-06-30', -20),

-- Class 4 Evaluations
(4, 2, N'Nói chuyện riêng', '2022-09-01', '2023-06-30', -10),
(4, 3, N'Ngôn ngữ không phù hợp', '2022-09-01', '2023-06-30', -15),
(4, 4, N'Không tuân theo chỉ dẫn', '2022-09-01', '2023-06-30', -20),
(4, 1, N'Nghỉ học không phép', '2022-09-01', '2023-06-30', -5),
(4, 1, N'Đi học trễ', '2022-09-01', '2023-06-30', -5),
(4, 1, N'Bỏ tiết/trốn tiết', '2022-09-01', '2023-06-30', -5),
(4, 6, N'Mặc không đúng quy định', '2022-09-01', '2023-06-30', -5),
(4, 6, N'Phụ kiện không phù hợp', '2022-09-01', '2023-06-30', -5),
(4, 3, N'Quay cóp', '2022-09-01', '2023-06-30', -15),
(4, 3, N'Đạo văn', '2022-09-01', '2023-06-30', -20);

--Chèn 40 bản ghi mẫu vào bảng Evaluation
INSERT INTO EvaluationDetail (ClassID, EvaluationID, Status)
VALUES 
    (1, 1, 'ACTIVE'),
    (1, 2, 'ACTIVE'),
    (1, 3, 'ACTIVE'),
    (1, 4, 'ACTIVE'),
    (1, 5, 'ACTIVE'),
    (1, 6, 'ACTIVE'), 
    (1, 7, 'ACTIVE'), 
    (1, 8, 'ACTIVE'), 
    (1, 9, 'ACTIVE'), 
    (1, 10, 'ACTIVE'), 
    (2, 11, 'ACTIVE'), 
    (2, 12, 'ACTIVE'), 
    (2, 13, 'ACTIVE'), 
    (2, 14, 'ACTIVE'), 
    (2, 15, 'ACTIVE'), 
    (2, 16, 'ACTIVE'), 
    (2, 17, 'ACTIVE'), 
    (2, 18, 'ACTIVE'), 
    (2, 19, 'ACTIVE'), 
    (2, 20, 'ACTIVE'), 
    (3, 21, 'ACTIVE'), 
    (3, 22, 'ACTIVE'), 
    (3, 23, 'ACTIVE'), 
    (3, 24, 'ACTIVE'), 
    (3, 25, 'ACTIVE'), 
    (3, 26, 'ACTIVE'), 
    (3, 27, 'ACTIVE'), 
    (3, 28, 'ACTIVE'), 
    (3, 29, 'ACTIVE'), 
    (3, 30, 'ACTIVE'), 
    (4, 31, 'ACTIVE'), 
    (4, 32, 'ACTIVE'), 
    (4, 33, 'ACTIVE'), 
    (4, 34, 'ACTIVE'), 
    (4, 35, 'ACTIVE'), 
    (4, 36, 'ACTIVE'), 
    (4, 37, 'ACTIVE'), 
    (4, 38, 'ACTIVE'), 
    (4, 39, 'ACTIVE'), 
    (4, 40, 'ACTIVE');



