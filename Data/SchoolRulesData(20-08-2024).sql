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
    ('Admin', 'admin@gmail.com', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', '8438522344', 1, 'ACTIVE');


-- Chèn 2 bản ghi mẫu vào bảng HighSchool
INSERT INTO [SchoolRules].[dbo].[HighSchool] ([Code], [Name], [City], [Address], [Phone], [WebURL])
VALUES
	('HS001', N'THPT Bình Thắng', N'TP.HCM', N'Dĩ An', '8487654321', 'http://www.thptbt.edu.vn'),
	('HS002', N'THPT Bình An', N'Hà Nội', N'Hồ Gươm', '8487654322', 'http://www.thptba.edu.vn');



-- Chèn 6 bản ghi mẫu vào bảng User
INSERT INTO [SchoolRules].[dbo].[User] ([RoleID], [SchoolID], [Code], [Name], [Phone], [Password], [Address], [Status])
VALUES
    (2, 1, 'BT001', 'School_AdminBT1', '8412367890', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'123 Đường A, Quận B, TP.HCM', 'ACTIVE'),
    (3, 1, 'BT002', N'Ban Giám hiệu', '8412367891', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'456 Đường C, Quận D, TP.HCM', 'ACTIVE'),
    (4, 1, 'BT003', N'Giám thị BT1', '8412367892', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'789 Đường E, Quận F, TP.HCM', 'ACTIVE'),
    (5, 1, 'BT006', N'Giáo viên BT1', '8412367893', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'321 Đường G, Quận H, TP.HCM', 'ACTIVE'),
    (5, 1, 'BT007', N'Giáo viên BT2', '8412367888', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'322 Đường A, Quận C, TP.HCM', 'ACTIVE'),
    (5, 1, 'BT008', N'Giáo viên BT3', '8412367877', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'323 Đường B, Quận A, TP.HCM', 'ACTIVE'),
    (5, 1, 'BT009', N'Giáo viên BT4', '8412367866', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'324 Đường M, Quận N, TP.HCM', 'ACTIVE'),
    (5, 1, 'BT010', N'Giáo viên BT5', '8412367855', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'324 Đường I, Quận K, TP.HCM', 'ACTIVE'),
    (5, 1, 'BT011', N'Giáo viên BT6', '8412367844', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'324 Đường E, Quận J, TP.HCM', 'ACTIVE'),
    (5, 1, 'BT012', N'Giáo viên BT7', '841267100', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'324 Đường A, Quận Y, TP.HCM', 'ACTIVE'),
    (5, 1, 'BT013', N'Giáo viên BT8', '840011223', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'324 Đường B, Quận O, TP.HCM', 'ACTIVE'),
    (5, 1, 'BT014', N'Giáo viên BT9', '841200161', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'324 Đường C, Quận Q, TP.HCM', 'ACTIVE'),
    (6, 1, 'BT015', N'Sao đỏ BT1', '8412367894', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'245 Đường I, Quận K, TP.HCM', 'INACTIVE'),
    (6, 1, 'BT016', N'Sao đỏ BT2', '8417645678', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'269 Đường I, Quận P, TP.HCM', 'INACTIVE'),
	(6, 1, 'BT017', N'Sao đỏ BT3', '8412367849', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'654 Đường I, Quận Q, TP.HCM', 'INACTIVE'),
    (6, 1, 'BT018', N'Sao đỏ BT4', '8412382678', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'623 Đường I, Quận A, TP.HCM', 'INACTIVE'),
	(6, 1, 'BT019', N'Sao đỏ BT5', '8412367472', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'782 Đường I, Quận B, TP.HCM', 'INACTIVE'),
    (6, 1, 'BT020', N'Sao đỏ BT6', '8416232353', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'10 Đường I, Quận C, TP.HCM', 'INACTIVE'),
	(6, 1, 'BT021', N'Sao đỏ BT7', '8412324678', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'19 Đường I, Quận O, TP.HCM', 'INACTIVE'),
	(6, 1, 'BT022', N'Sao đỏ BT8', '8412767472', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'20 Đường I, Quận J, TP.HCM', 'INACTIVE'),
    (6, 1, 'BT023', N'Sao đỏ BT9', '8416772353', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'67 Đường I, Quận O, TP.HCM', 'INACTIVE'),
	(6, 1, 'BT024', N'Sao đỏ BT10', '8411824813', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'96 Đường J, Quận K, TP.HCM', 'ACTIVE'),
    (6, 1, 'BT025', N'Sao đỏ BT11', '8498346346', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'102 Đường G, Quận P, TP.HCM', 'ACTIVE'),
	(6, 1, 'BT026', N'Sao đỏ BT12', '8463543653', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'140 Đường H, Quận Q, TP.HCM', 'ACTIVE'),
    (6, 1, 'BT027', N'Sao đỏ BT13', '8412388762', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'146 Đường E, Quận A, TP.HCM', 'ACTIVE'),
	(6, 1, 'BT028', N'Sao đỏ BT14', '8417283322', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'31 Đường T, Quận B, TP.HCM', 'ACTIVE'),
    (6, 1, 'BT029', N'Sao đỏ BT15', '8416922222', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'8 Đường V, Quận C, TP.HCM', 'ACTIVE'),
	(6, 1, 'BT030', N'Sao đỏ BT16', '8417134672', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'234 Đường S, Quận O, TP.HCM', 'ACTIVE'),
	(6, 1, 'BT031', N'Sao đỏ BT17', '8493284273', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'101 Đường O, Quận J, TP.HCM', 'ACTIVE'),
    (6, 1, 'BT032', N'Sao đỏ BT18', '8413478346', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'9 Đường L, Quận O, TP.HCM', 'ACTIVE'),

	(2, 2, 'BA001', 'School_AdminBA1', '8497654321', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'123 Đường A, Quận B, Hà Nội', 'ACTIVE'),
    (3, 2, 'BA002', 'PrincipalBA1', '8498765432', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'456 Đường C, Quận D, Hà Nội', 'ACTIVE'),
    (4, 2, 'BA003', 'SupervisorBA1', '8498765431', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'789 Đường E, Quận F, Hà Nội', 'ACTIVE'),
    (5, 2, 'BA006', 'TeacherBA1', '8498765433', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'321 Đường G, Quận H, Hà Nội', 'ACTIVE'),
    (5, 2, 'BA007', 'TeacherBA2', '8498765434', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'322 Đường A, Quận C, Hà Nội', 'ACTIVE'),
    (5, 2, 'BA008', 'TeacherBA3', '8498765435', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'323 Đường B, Quận A, Hà Nội', 'ACTIVE'),
    (5, 2, 'BA009', 'TeacherBA4', '8498765436', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'324 Đường M, Quận N, Hà Nội', 'ACTIVE'),
    (5, 2, 'BA010', 'TeacherBA5', '8498765437', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'324 Đường I, Quận K, Hà Nội', 'ACTIVE'),
    (5, 2, 'BA011', 'TeacherBA6', '8401315438', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'324 Đường E, Quận D, Hà Nội', 'ACTIVE'),
    (5, 2, 'BA012', 'TeacherBA7', '8490997738', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'324 Đường O, Quận A, Hà Nội', 'ACTIVE'),
    (5, 2, 'BA013', 'TeacherBA8', '8498000118', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'324 Đường Z, Quận B, Hà Nội', 'ACTIVE'),
    (5, 2, 'BA014', 'TeacherBA9', '8492223338', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'324 Đường E, Quận J, Hà Nội', 'ACTIVE'),
    (6, 2, 'BA015', 'Stu_SupervisorBA1', '8498765439', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'654 Đường I, Quận J, Hà Nội', 'ACTIVE'),
    (6, 2, 'BA016', 'Stu_SupervisorBA2', '8498765440', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'652 Đường I, Quận O, Hà Nội', 'ACTIVE'),
	(6, 2, 'BA017', 'Stu_SupervisorBA3', '8498777777', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'654 Đường I, Quận N, Hà Nội', 'ACTIVE'),
    (6, 2, 'BA018', 'Stu_SupervisorBA4', '8496666666', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'652 Đường I, Quận M, Hà Nội', 'ACTIVE'),
	(6, 2, 'BA019', 'Stu_SupervisorBA5', '8498752411', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'654 Đường I, Quận A, Hà Nội', 'ACTIVE'),
    (6, 2, 'BA020', 'Stu_SupervisorBA6', '8498216152', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'652 Đường I, Quận D, Hà Nội', 'ACTIVE'),
	(6, 2, 'BA021', 'Stu_SupervisorBA7', '8482527583', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'654 Đường I, Quận K, Hà Nội', 'ACTIVE'),
    (6, 2, 'BA022', 'Stu_SupervisorBA8', '8498252892', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'652 Đường I, Quận T, Hà Nội', 'ACTIVE'),
    (6, 2, 'BA023', 'Stu_SupervisorBA9', '8483648453', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'652 Đường I, Quận V, Hà Nội', 'ACTIVE'),
    (6, 2, 'BA024', 'Stu_SupervisorBA10', '8483363663', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'652 Đường I, Quận V, Hà Nội', 'ACTIVE'),
    (6, 2, 'BA025', 'Stu_SupervisorBA11', '8489819253', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'652 Đường I, Quận V, Hà Nội', 'ACTIVE'),
    (6, 2, 'BA026', 'Stu_SupervisorBA12', '8488726253', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'652 Đường I, Quận V, Hà Nội', 'ACTIVE'),
    (6, 2, 'BA027', 'Stu_SupervisorBA13', '8480828222', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'652 Đường I, Quận V, Hà Nội', 'ACTIVE'),
    (6, 2, 'BA028', 'Stu_SupervisorBA14', '8483111111', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'652 Đường I, Quận V, Hà Nội', 'ACTIVE'),
    (6, 2, 'BA029', 'Stu_SupervisorBA15', '8498364011', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'652 Đường I, Quận V, Hà Nội', 'ACTIVE'),
    (6, 2, 'BA030', 'Stu_SupervisorBA16', '8480061363', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'652 Đường I, Quận V, Hà Nội', 'ACTIVE'),
    (6, 2, 'BA031', 'Stu_SupervisorBA17', '8480238623', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'652 Đường I, Quận V, Hà Nội', 'ACTIVE'),
    (6, 2, 'BA032', 'Stu_SupervisorBA18', '8482385275', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'652 Đường I, Quận V, Hà Nội', 'ACTIVE'),
	(4, 1, 'BT004', N'Giám thị BT2', '8410011292', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'766 Đường N, Quận A, TP.HCM', 'ACTIVE'),
	(4, 1, 'BT005', N'Giám thị BT3', '8419151092', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'711 Đường F, Quận B, TP.HCM', 'ACTIVE'),
	(4, 2, 'BA004', 'SupervisorBA2', '8410976257', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'122 Đường V, Quận U, Hà Nội', 'ACTIVE'),
	(4, 2, 'BA005', 'SupervisorBA3', '8400975892', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'334 Đường J, Quận Q, Hà Nội', 'ACTIVE'),
	(5, 1, 'BT033', N'Giáo viên BT10', '841211166', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'33 Đường N2, Quận K, TP.HCM', 'ACTIVE'),
	(5, 1, 'BT034', N'Giáo viên BT11', '841264169', '+WTDfF6F6PThEBMzbqo/Lw==;ArcVHs1WgZM7a2sEyFMTeLCfxazpU8GEq+lAA6bAZzE=', N'16 Đường 19, Quận A, TP.HCM', 'ACTIVE');


-- Chèn 6 bản ghi mẫu vào bảng Teacher
INSERT INTO [SchoolRules].[dbo].[Teacher] ([UserID], [SchoolID], [Sex])
VALUES
(3, 1, 'False'),
(4, 1, 'True'),
(5, 1, 'False'),
(6, 1, 'True'),
(7, 1, 'False'),
(8, 1, 'True'),
(9, 1, 'False'),
(10, 1, 'True'),
(11, 1, 'False'),
(12, 1, 'True'),

(33, 2, 'True'),
(34, 2, 'False'),
(35, 2, 'True'),
(36, 2, 'False'),
(37, 2, 'True'),
(38, 2, 'False'),
(39, 2, 'True'),
(40, 2, 'False'),
(41, 2, 'True'),
(42, 2, 'False'),

(61, 1, 'True'),
(62, 1, 'True'),
(63, 2, 'False'),
(64, 2, 'False'),
(65, 1, 'True'),
(66, 1, 'False');


-- Chèn 6 bản ghi mẫu vào bảng StudentSupervisor
INSERT INTO [SchoolRules].[dbo].[StudentSupervisor] ([UserID], [StudentInClassID], [Description])
VALUES
(13, 1, N'Sao đỏ lớp 10A1'),
(14, 11, N'Sao đỏ lớp 10A2'),
(15, 21, N'Sao đỏ lớp 10A3'),
(16, 31, N'Sao đỏ lớp 11A1'),
(17, 41, N'Sao đỏ lớp 11A2'),
(18, 51, N'Sao đỏ lớp 11A3'),
(19, 61, N'Sao đỏ lớp 12A1'),
(20, 71, N'Sao đỏ lớp 12A2'),
(21, 81, N'Sao đỏ lớp 12A3'),
(22, 91, N'Sao đỏ lớp 10A1'),
(23, 101, N'Sao đỏ lớp 10A2'),
(24, 111, N'Sao đỏ lớp 10A3'),
(25, 121, N'Sao đỏ lớp 11A2'),
(26, 131, N'Sao đỏ lớp 11A3'),
(27, 141, N'Sao đỏ lớp 11A3'),
(28, 151, N'Sao đỏ lớp 12A1'),
(29, 161, N'Sao đỏ lớp 12A2'),
(30, 171, N'Sao đỏ lớp 12A3'),

(43, 181, N'Sao đỏ lớp 10B1'),
(44, 191, N'Sao đỏ lớp 10B2'),
(45, 201, N'Sao đỏ lớp 10B3'),
(46, 211, N'Sao đỏ lớp 11B1'),
(47, 221, N'Sao đỏ lớp 11B2'),
(48, 231, N'Sao đỏ lớp 11B3'),
(49, 241, N'Sao đỏ lớp 12B1'),
(50, 251, N'Sao đỏ lớp 12B2'),
(51, 261, N'Sao đỏ lớp 12B3'),
(52, 271, N'Sao đỏ lớp 10B1'),
(53, 281, N'Sao đỏ lớp 10B2'),
(54, 291, N'Sao đỏ lớp 10B3'),
(55, 301, N'Sao đỏ lớp 11B2'),
(56, 311, N'Sao đỏ lớp 11B3'),
(57, 321, N'Sao đỏ lớp 11B3'),
(58, 331, N'Sao đỏ lớp 12B1'),
(59, 341, N'Sao đỏ lớp 12B2'),
(60, 351, N'Sao đỏ lớp 12B3');

-- Chèn 4 bản ghi mẫu vào bảng SchoolYear
INSERT INTO [SchoolRules].[dbo].[SchoolYear] ([SchoolID], [Year], [StartDate], [EndDate], [Status])
VALUES 
    (1, 2023, '2023-09-01', '2024-06-30', 'FINISHED'),
    (1, 2024, '2024-08-05', '2025-05-29', 'ONGOING'),

    (2, 2023, '2023-09-01', '2024-06-30', 'FINISHED'),
    (2, 2024, '2024-08-01', '2025-05-31', 'ONGOING');


-- Chèn 3 bản ghi mẫu vào bảng ClassGroup
INSERT INTO [SchoolRules].[dbo].[ClassGroup] ([SchoolID], [TeacherID], [Name], [Status])
VALUES
    (1, 1, N'Khối 10', 'ACTIVE'),
    (1, 21, N'Khối 11', 'ACTIVE'),
    (1, 22, N'Khối 12', 'ACTIVE'),

	(2, 11, N'Khối 10', 'ACTIVE'),
    (2, 23, N'Khối 11', 'ACTIVE'),
    (2, 24, N'Khối 12', 'ACTIVE');


-- Chèn 20 bản ghi mẫu vào bảng Student
INSERT INTO [SchoolRules].[dbo].[Student] ([SchoolID], [Code], [Name], [Sex], [Birthday], [Address], [Phone])
VALUES
(1, 'BT001', N'Nguyễn Văn An', 1, '2005-01-15', N'123 Nguyễn Trãi, TP.HCM', '8412345678'),
(1, 'BT002', N'Trần Thị Tuyết', 0, '2006-03-22', N'456 lê Lợi, TP.HCM', '8412345679'),
(1, 'BT003', N'Lê Văn Bá', 1, '2005-05-10', N'789 Hoàng Hoa Thám, TP.HCM', '8412345680'),
(1, 'BT004', N'Phạm Thị Dung', 0, '2006-07-18', N'101 Trần Phú, TP.HCM', '8412345681'),
(1, 'BT005', N'Hoàng Văn Bảo', 1, '2005-09-25', N'202 Nguyễn Huệ, TP.HCM', '8412345682'),
(1, 'BT006', N'Vũ Thị Tuyết Nhi', 0, '2006-11-30', N'303 Lê Thánh Tông, TP.HCM', '8412345683'),
(1, 'BT007', N'Phạm Văn Tài', 1, '2005-12-05', N'404 Bà Triệu, TP.HCM', '8412345684'),
(1, 'BT008', N'Ngô Việt Hoàng', 0, '2006-01-19', N'505 Hai Bà Trưng, TP.HCM', '8412345685'),
(1, 'BT009', N'Đào Văn Thụ', 1, '2005-04-15', N'606 Lý Thường Kiệt, TP.HCM', '8412345686'),
(1, 'BT010', N'Đỗ Thị My', 0, '2006-06-12', N'707 Hàng Bài, TP.HCM', '8412345687'),
(1, 'BT011', N'Phan Văn Khánh', 1, '2005-08-20', N'808 Trần Hưng Đạo, TP.HCM', '8412345688'),
(1, 'BT012', N'Đinh Thị Linh', 0, '2006-10-22', N'909 Ba Đình, TP.HCM', '8412345689'),
(1, 'BT013', N'Nguyễn Văn Minh', 1, '2005-03-15', N'123 Cầu Giấy, TP.HCM', '8412345690'),
(1, 'BT014', N'Trần Thị Nữ', 0, '2006-05-25', N'456 Thanh Xuân, TP.HCM', '8412345691'),
(1, 'BT015', N'Lê Văn Oanh', 1, '2005-07-10', N'789 Tây Hồ, TP.HCM', '8412345692'),
(1, 'BT016', N'Phạm Thị Phương', 0, '2006-09-19', N'101 Đống Đa, TP.HCM', '8412345693'),
(1, 'BT017', N'Hoàng Văn Quỳnh', 1, '2005-11-30', N'202 Long Biên, TP.HCM', '8412345694'),
(1, 'BT018', N'Vũ Thị Duyên', 0, '2006-02-12', N'303 Hoàn Kiếm, TP.HCM', '8412345695'),
(1, 'BT019', N'Phạm Văn Sáng', 1, '2005-05-18', N'404 Hoàng Mai, TP.HCM', '8412345696'),
(1, 'BT020', N'Ngô Thị Thư', 0, '2006-07-25', N'505 Gia Lâm, TP.HCM', '8412345697'),
(1, 'BT021', N'Đào Văn Uyên', 1, '2005-09-15', N'606 Hà Đông, TP.HCM', '8412345698'),
(1, 'BT022', N'Đỗ Thị Vy', 0, '2006-11-12', N'707 Nam Từ Liêm, TP.HCM', '8412345699'),
(1, 'BT023', N'Phan Văn Quyền', 1, '2005-01-20', N'808 Hải Dương, TP.HCM', '8412345700'),
(1, 'BT024', N'Đinh Thị Xuyến', 0, '2006-03-22', N'909 Hải Phòng, TP.HCM', '8412345701'),
(1, 'BT025', N'Nguyễn Văn Yến', 1, '2005-05-10', N'123 Thái Bình, TP.HCM', '8412345702'),
(1, 'BT026', N'Trần Thị Dung', 0, '2006-07-18', N'456 Quảng Ninh, TP.HCM', '8412345703'),
(1, 'BT027', N'Lê Văn Sánh', 1, '2005-09-25', N'789 Hải Phòng, TP.HCM', '8412345704'),
(1, 'BT028', N'Phạm Thị Tố Nữ', 0, '2006-11-30', N'101 Thái Nguyên, TP.HCM', '8412345705'),
(1, 'BT029', N'Hoàng Văn Tiến', 1, '2005-12-05', N'202 Ninh Bình, TP.HCM', '8412345706'),
(1, 'BT030', N'Vũ Thị Tuyết Nhi', 0, '2006-01-19', N'303 Bắc Giang, TP.HCM', '8412345707'),
(1, 'BT031', N'Phạm Văn Yến', 1, '2005-04-15', N'404 Bắc Ninh, TP.HCM', '8412345708'),
(1, 'BT032', N'Ngô Thị Dung', 0, '2006-06-12', N'505 Hà Nam, TP.HCM', '8412345709'),
(1, 'BT033', N'Đào Văn Đạt', 1, '2005-08-20', N'606 Phú Thọ, TP.HCM', '8412345710'),
(1, 'BT034', N'Đỗ Thị Huyền', 0, '2006-10-22', N'707 Tuyên Quang, TP.HCM', '8412345711'),
(1, 'BT035', N'Phan Văn Tình', 1, '2005-03-15', N'808 Vĩnh Phúc, TP.HCM', '8412345712'),
(1, 'BT036', N'Đinh Thị Trúc Linh', 0, '2006-05-25', N'909 Yên Bái, TP.HCM', '8412345713'),
(1, 'BT037', N'Nguyễn Văn Tú', 1, '2005-07-10', N'123 Lào Cai, TP.HCM', '8412345714'),
(1, 'BT038', N'Trần Thị Tú', 0, '2006-09-19', N'456 Lạng Sơn, TP.HCM', '8412345715'),
(1, 'BT039', N'Lê Văn Khang', 1, '2005-11-30', N'789 Cao Bằng, TP.HCM', '8412345716'),
(1, 'BT040', N'Phạm Thị Giang', 0, '2006-02-12', N'101 Điện Biên, TP.HCM', '8412345717'),
(1, 'BT041', N'Hoàng Văn Phát', 1, '2005-04-18', N'202 Sơn La, TP.HCM', '8412345718'),
(1, 'BT042', N'Vũ Thị Phượng', 0, '2006-06-15', N'303 Lai Châu, TP.HCM', '8412345719'),
(1, 'BT043', N'Phạm Văn Hiếu', 1, '2005-08-22', N'404 Điện Biên, TP.HCM', '8412345720'),
(1, 'BT044', N'Ngô Thị Tình', 0, '2006-10-25', N'505 Lào Cai, TP.HCM', '8412345721'),
(1, 'BT045', N'Đào Văn Tín', 1, '2005-12-28', N'606 Bắc Kạn, TP.HCM', '8412345722'),
(1, 'BT046', N'Đỗ Thị Thảo', 0, '2006-02-28', N'707 Tuyên Quang, TP.HCM', '8412345723'),
(1, 'BT047', N'Phan Văn Dũng', 1, '2005-05-02', N'808 Vĩnh Phúc, TP.HCM', '8412345724'),
(1, 'BT048', N'Đinh Thị Mến', 0, '2006-07-05', N'909 Yên Bái, TP.HCM', '8412345725'),
(1, 'BT049', N'Nguyễn Văn Đạt', 1, '2005-09-08', N'123 Lào Cai, TP.HCM', '8412345726'),
(1, 'BT050', N'Trần Thị Xuyến', 0, '2006-11-10', N'456 Lạng Sơn, TP.HCM', '8412345727'),
(1, 'BT051', N'Lê Văn Bảo', 1, '2005-01-15', N'789 Cao Bằng, TP.HCM', '8412345728'),
(1, 'BT052', N'Phạm Thị Vi', 0, '2006-03-22', N'101 Điện Biên, TP.HCM', '8412345729'),
(1, 'BT053', N'Hoàng Văn Thám', 1, '2005-05-10', N'202 Sơn La, TP.HCM', '8412345730'),
(1, 'BT054', N'Vũ Thị Hằng', 0, '2006-07-18', N'303 Lai Châu, TP.HCM', '8412345731'),
(1, 'BT055', N'Phạm Văn Đạt', 1, '2005-09-25', N'404 Điện Biên, TP.HCM', '8412345732'),
(1, 'BT056', N'Ngô Thị Dung', 0, '2006-11-30', N'505 Lào Cai, TP.HCM', '8412345733'),
(1, 'BT057', N'Đào Văn Tiến', 1, '2005-12-05', N'606 Bắc Kạn, TP.HCM', '8412345734'),
(1, 'BT058', N'Đỗ Thị Trúc', 0, '2006-01-19', N'707 Tuyên Quang, TP.HCM', '8412345735'),
(1, 'BT059', N'Phan Văn Phát', 1, '2005-04-15', N'808 Vĩnh Phúc, TP.HCM', '8412345736'),
(1, 'BT060', N'Đinh Thị Trúc', 0, '2006-06-12', N'909 Yên Bái, TP.HCM', '8412345737'),
(1, 'BT061', N'Nguyễn Văn Tài', 1, '2005-08-20', N'123 Lào Cai, TP.HCM', '8412345738'),
(1, 'BT062', N'Trần Thị Thi', 0, '2006-10-22', N'456 Lạng Sơn, TP.HCM', '8412345739'),
(1, 'BT063', N'Lê Văn Cao', 1, '2005-03-15', N'789 Cao Bằng, TP.HCM', '8412345740'),
(1, 'BT064', N'Phạm Thị Linh', 0, '2006-05-25', N'101 Điện Biên, TP.HCM', '8412345741'),
(1, 'BT065', N'Hoàng Văn Minh', 1, '2005-07-10', N'202 Sơn La, TP.HCM', '8412345742'),
(1, 'BT066', N'Vũ Thị Nở', 0, '2006-09-19', N'303 Lai Châu, TP.HCM', '8412345743'),
(1, 'BT067', N'Phạm Văn Ánh', 1, '2005-11-30', N'404 Điện Biên, TP.HCM', '8412345744'),
(1, 'BT068', N'Ngô Thị Phương', 0, '2006-02-12', N'505 Lào Cai, TP.HCM', '8412345745'),
(1, 'BT069', N'Đào Văn Quyến', 1, '2005-04-18', N'606 Bắc Kạn, TP.HCM', '8412345746'),
(1, 'BT070', N'Đỗ Thị Rạng', 0, '2006-06-15', N'707 Tuyên Quang, TP.HCM', '8412345747'),
(1, 'BT071', N'Trần Thị Sữa', 0, '2006-06-18', N'456 Điện Biên, TP.HCM', '8412345748'),
(1, 'BT072', N'Lê Văn Khang', 1, '2005-08-22', N'789 Sơn La, TP.HCM', '8412345749'),
(1, 'BT073', N'Lê Anh Duy', 0, '2006-10-10', N'101 Lai Châu, TP.HCM', '8412345750'),
(1, 'BT074', N'Hoàng Văn Huy', 1, '2005-03-08', N'202 Điện Biên, TP.HCM', '8412345751'),
(1, 'BT075', N'Vũ Thị Tuyết Trinh', 0, '2006-05-29', N'303 Lào Cai, TP.HCM', '8412345752'),
(1, 'BT076', N'Đoàn Văn Anh Tú', 1, '2005-07-15', N'404 Bắc Kạn, TP.HCM', '8412345753'),
(1, 'BT077', N'Huỳnh Đức Khang', 0, '2006-09-17', N'505 Tuyên Quang, TP.HCM', '8412345754'),
(1, 'BT078', N'Nguyễn Bá Trương Phát', 1, '2005-11-18', N'606 Vĩnh Phúc, TP.HCM', '8412345755'),
(1, 'BT079', N'Võ Lê Yến', 0, '2006-01-12', N'707 Yên Bái, TP.HCM', '8412345756'),
(1, 'BT080', N'Nguyễn Bá Quân', 1, '2005-04-20', N'808 Lào Cai, TP.HCM', '8412345757'),
(1, 'BT081', N'Mai Thị Linh', 0, '2006-06-21', N'909 Lạng Sơn, TP.HCM', '8412345758'),
(1, 'BT082', N'Thái Thành Đạt', 1, '2005-08-28', N'123 Cao Bằng, TP.HCM', '8412345759'),
(1, 'BT083', N'Phạm Tuyết Nhung', 0, '2006-11-05', N'456 Điện Biên, TP.HCM', '8412345760'),
(1, 'BT084', N'Nguyễn Huy Hoàng', 1, '2005-01-11', N'789 Sơn La, TP.HCM', '8412345761'),
(1, 'BT085', N'Phan Văn Sáng', 0, '2006-03-14', N'101 Lai Châu, TP.HCM', '8412345762'),
(1, 'BT086', N'Đỗ Văn Hảo', 1, '2005-05-19', N'202 Điện Biên, TP.HCM', '8412345763'),
(1, 'BT087', N'Đinh Thị Thanh Thảo', 0, '2006-07-21', N'303 Lào Cai, TP.HCM', '8412345764'),
(1, 'BT088', N'Phan Van J3', 1, '2005-09-23', N'404 Bắc Cạn, TP.HCM', '8412345765'),
(1, 'BT089', N'Đinh Thị Anh', 0, '2006-11-29', N'505 Tuyên Quang, TP.HCM', '8412345766'),
(1, 'BT090', N'Nguyễn Văn Tú', 1, '2005-03-03', N'606 Vĩnh Phúc, TP.HCM', '8412345767'),
(1, 'BT091', N'Trần Tuyết Minh', 0, '2006-05-15', N'707 Yen Bai, TP.HCM', '8412345768'),
(1, 'BT092', N'Lê Văn Phúc', 1, '2005-07-18', N'808 Lào Cai, TP.HCM', '8412345769'),
(1, 'BT093', N'Phạm Thị Tuyết Mai', 0, '2006-09-22', N'909 Lạng Sơn, TP.HCM', '8412345770'),
(1, 'BT094', N'Hồ Thanh Thảo', 1, '2005-11-24', N'123 Cao Bằng, TP.HCM', '8412345771'),
(1, 'BT095', N'Nguyễn Hoài Thương', 0, '2006-01-11', N'456 Điện Biên, TP.HCM', '8412345772'),
(1, 'BT096', N'Đào Văn Hảo', 1, '2005-03-18', N'789 Sơn La, TP.HCM', '8412345773'),
(1, 'BT097', N'Đỗ Thị Sung', 0, '2006-05-19', N'101 Lai Châu, TP.HCM', '8412345774'),
(1, 'BT098', N'Phan Văn Tài Ma', 1, '2005-07-25', N'202 Điện Biên, TP.HCM', '8412345775'),
(1, 'BT099', N'Đinh Thị Trinh', 0, '2006-09-28', N'303 Lào Cai, TP.HCM', '8412345776'),
(1, 'BT100', N'Nguyễn Văn Vinh', 1, '2005-11-05', N'404 Bắc Cạn, TP.HCM', '8412345777'),
(1, 'BT101', N'Trần Thị Tố Nhi', 0, '2006-01-19', N'505 Tuyên Quang, TP.HCM', '8412345778'),
(1, 'BT102', N'Lê Xuân Tiến', 1, '2005-04-15', N'606 Vĩnh Phúc, TP.HCM', '8412345779'),
(1, 'BT103', N'Phạm Văn Duy', 0, '2006-06-12', N'707 Yên Bái, TP.HCM', '8412345780'),
(1, 'BT104', N'Lê Tuyêt Trinh', 1, '2005-08-20', N'808 Lao Cai, TP.HCM', '8412345781'),
(1, 'BT105', N'Vũ Thị Trinh', 0, '2006-10-22', N'909 Lạng Sơn, TP.HCM', '8412345782'),
(1, 'BT106', N'Đào Văn Phúc', 1, '2005-03-15', N'123 Cao bằng, TP.HCM', '8412345783'),
(1, 'BT107', N'Đỗ Thị Mộng', 0, '2006-05-25', N'456 Điện Biên, TP.HCM', '8412345784'),
(1, 'BT108', N'Phan Văn Duy', 1, '2005-07-10', N'789 Sơn La, TP.HCM', '8412345785'),
(1, 'BT109', N'Đinh Thị Mai', 0, '2006-09-19', N'101 Lai Châu, TP.HCM', '8412345786'),
(1, 'BT110', N'Nguyên Văn Phúc', 1, '2005-11-30', N'202 Điện Biên, TP.HCM', '8412345787'),
(1, 'BT111', N'Trần Thị Nam', 0, '2006-02-12', N'303 Lào Cai, TP.HCM', '8412345788'),
(1, 'BT112', N'Lê Văn Hảo', 1, '2005-05-18', N'404 Bắc Cạn, TP.HCM', '8412345789'),
(1, 'BT113', N'Phạm Hương Giang', 0, '2006-07-25', N'505 Tuyên Quang, TP.HCM', '8412345790'),
(1, 'BT114', N'Hoàng Văn Bình', 1, '2005-09-15', N'606 Vĩnh Phúc, TP.HCM', '8412345791'),
(1, 'BT115', N'Vu Thi K4', 0, '2006-11-12', N'707 Yên Bái, TP.HCM', '8412345792'),
(1, 'BT116', N'Đào Văn Táo', 1, '2005-01-20', N'808 Lào Cai, TP.HCM', '8412345793'),
(1, 'BT117', N'Đỗ Thị Mơ', 0, '2006-03-22', N'909 Lạng Sơn, TP.HCM', '8412345794'),
(1, 'BT118', N'Lê Hữu Nam', 1, '2005-05-10', N'123 Cao Bằng, TP.HCM', '8412345795'),
(1, 'BT119', N'Đinh Thanh Thảo', 0, '2006-07-18', N'456 Điện Biên, TP.HCM', '8412345796'),
(1, 'BT120', N'Trịnh Văn Quyết', 1, '2005-09-25', N'789 Sơn La, TP.HCM', '8412345797'),



(2, 'BA001', 'Nguyen Van An', 1, '2005-01-15', '123 Nguyen Trai, Ha Noi', '8412345678'),
(2, 'BA002', 'Tran Thi Ba', 0, '2006-03-22', '456 Le Loi, Ha Noi', '8412345679'),
(2, 'BA003', 'Le Van Ca', 1, '2005-05-10', '789 Hoang Hoa Tham, Ha Noi', '8412345680'),
(2, 'BA004', 'Pham Thi Dung', 0, '2006-07-18', '101 Tran Phu, Ha Noi', '8412345681'),
(2, 'BA005', 'Hoang Van Yen', 1, '2005-09-25', '202 Nguyen Hue, Ha Noi', '8412345682'),
(2, 'BA006', 'Vu Thi Thu', 0, '2006-11-30', '303 Le Thanh Tong, Ha Noi', '8412345683'),
(2, 'BA007', 'Pham Van Giang', 1, '2005-12-05', '404 Ba Trieu, Ha Noi', '8412345684'),
(2, 'BA008', 'Ngo Thi Hoang', 0, '2006-01-19', '505 Hai Ba Trung, Ha Noi', '8412345685'),
(2, 'BA009', 'Dao Van Yen', 1, '2005-04-15', '606 Ly Thuong Kiet, Ha Noi', '8412345686'),
(2, 'BA010', 'Do Thi Dung', 0, '2006-06-12', '707 Hang Bai, Ha Noi', '8412345687'),
(2, 'BA011', 'Phan Van Khanh', 1, '2005-08-20', '808 Tran Hung Dao, Ha Noi', '8412345688'),
(2, 'BA012', 'Dinh Thi Linh', 0, '2006-10-22', '909 Ba Dinh, Ha Noi', '8412345689'),
(2, 'BA013', 'Nguyen Van Minh', 1, '2005-03-15', '123 Cau Giay, Ha Noi', '8412345690'),
(2, 'BA014', 'Tran Thi Nam', 0, '2006-05-25', '456 Thanh Xuan, Ha Noi', '8412345691'),
(2, 'BA015', 'Le Van Oanh', 1, '2005-07-10', '789 Tay Ho, Ha Noi', '8412345692'),
(2, 'BA016', 'Pham Thi Puong', 0, '2006-09-19', '101 Dong Da, Ha Noi', '8412345693'),
(2, 'BA017', 'Hoang Van Quynh', 1, '2005-11-30', '202 Long Bien, Ha Noi', '8412345694'),
(2, 'BA018', 'Vu Thi Sua', 0, '2006-02-12', '303 Hoan Kiem, Ha Noi', '8412345695'),
(2, 'BA019', 'Pham Van Sang', 1, '2005-05-18', '404 Hoang Mai, Ha Noi', '8412345696'),
(2, 'BA020', 'Ngo Thi Thu', 0, '2006-07-25', '505 Gia Lam, Ha Noi', '8412345697'),
(2, 'BA021', 'Dao Van Uyen', 1, '2005-09-15', '606 Ha Dong, Ha Noi', '8412345698'),
(2, 'BA022', 'Do Thi Vy', 0, '2006-11-12', '707 Nam Tu Liem, Ha Noi', '8412345699'),
(2, 'BA023', 'Phan Van Quynh', 1, '2005-01-20', '808 Hai Duong, Ha Noi', '8412345700'),
(2, 'BA024', 'Dinh Thi XUyen', 0, '2006-03-22', '909 Hai Phong, Ha Noi', '8412345701'),
(2, 'BA025', 'Nguyen Van Yen', 1, '2005-05-10', '123 Thai Binh, Ha Noi', '8412345702'),
(2, 'BA026', 'Tran Thi Giang', 0, '2006-07-18', '456 Quang Ninh, Ha Noi', '8412345703'),
(2, 'BA027', 'Le Van Anh', 1, '2005-09-25', '789 Hai Phong, Ha Noi', '8412345704'),
(2, 'BA028', 'Pham Thi Bang', 0, '2006-11-30', '101 Thai Nguyen, Ha Noi', '8412345705'),
(2, 'BA029', 'Hoang Van Cao', 1, '2005-12-05', '202 Ninh Binh, Ha Noi', '8412345706'),
(2, 'BA030', 'Vu Thi Duy', 0, '2006-01-19', '303 Bac Giang, Ha Noi', '8412345707'),
(2, 'BA031', 'Pham Van Tien', 1, '2005-04-15', '404 Bac Ninh, Ha Noi', '8412345708'),
(2, 'BA032', 'Ngo Thi Thanh', 0, '2006-06-12', '505 Ha Nam, Ha Noi', '8412345709'),
(2, 'BA033', 'Dao Van Gao', 1, '2005-08-20', '606 Phu Tho, Ha Noi', '8412345710'),
(2, 'BA034', 'Do Thi Huy', 0, '2006-10-22', '707 Tuyen Quang, Ha Noi', '8412345711'),
(2, 'BA035', 'Phan Van Sang', 1, '2005-03-15', '808 Vinh Phuc, Ha Noi', '8412345712'),
(2, 'BA036', 'Dinh Thi Linh', 0, '2006-05-25', '909 Yen Bai, Ha Noi', '8412345713'),
(2, 'BA037', 'Nguyen Van Khai', 1, '2005-07-10', '123 Lao Cai, Ha Noi', '8412345714'),
(2, 'BA038', 'Tran Thi Linh', 0, '2006-09-19', '456 Lang Son, Ha Noi', '8412345715'),
(2, 'BA039', 'Le Van My', 1, '2005-11-30', '789 Cao Bang, Ha Noi', '8412345716'),
(2, 'BA040', 'Pham Thi Nam', 0, '2006-02-12', '101 Dien Bien, Ha Noi', '8412345717'),
(2, 'BA041', 'Hoang Van Mi', 1, '2005-04-18', '202 Son La, Ha Noi', '8412345718'),
(2, 'BA042', 'Vu Thi Phu', 0, '2006-06-15', '303 Lai Chau, Ha Noi', '8412345719'),
(2, 'BA043', 'Pham Van Quynh Nhu', 1, '2005-08-22', '404 Dien Bien, Ha Noi', '8412345720'),
(2, 'BA044', 'Ngo Thi Diem', 0, '2006-10-25', '505 Lao Cai, Ha Noi', '8412345721'),
(2, 'BA045', 'Dao Van Thanh', 1, '2005-12-28', '606 Bac Kan, Ha Noi', '8412345722'),
(2, 'BA046', 'Do Thi Tu', 0, '2006-02-28', '707 Tuyen Quang, Ha Noi', '8412345723'),
(2, 'BA047', 'Phan Van Tung', 1, '2005-05-02', '808 Vinh Phuc, Ha Noi', '8412345724'),
(2, 'BA048', 'Dinh Thi Khang', 0, '2006-07-05', '909 Yen Bai, Ha Noi', '8412345725'),
(2, 'BA049', 'Nguyen Van Tien', 1, '2005-09-08', '123 Lao Cai, Ha Noi', '8412345726'),
(2, 'BA050', 'Tran Thi Thu', 0, '2006-11-10', '456 Lang Son, Ha Noi', '8412345727'),
(2, 'BA051', 'Le Van Trinh', 1, '2005-01-15', '789 Cao Bang, Ha Noi', '8412345728'),
(2, 'BA052', 'Pham Thi Ngo', 0, '2006-03-22', '101 Dien Bien, Ha Noi', '8412345729'),
(2, 'BA053', 'Hoang Van Thu', 1, '2005-05-10', '202 Son La, Ha Noi', '8412345730'),
(2, 'BA054', 'Vu Thi Tuyet Trinh', 0, '2006-07-18', '303 Lai Chau, Ha Noi', '8412345731'),
(2, 'BA055', 'Pham Van Cao', 1, '2005-09-25', '404 Dien Bien, Ha Noi', '8412345732'),
(2, 'BA056', 'Ngo Thi Duyen', 0, '2006-11-30', '505 Lao Cai, Ha Noi', '8412345733'),
(2, 'BA057', 'Dao Van Hao', 1, '2005-12-05', '606 Bac Kan, Ha Noi', '8412345734'),
(2, 'BA058', 'Do Thi Duyen', 0, '2006-01-19', '707 Tuyen Quang, Ha Noi', '8412345735'),
(2, 'BA059', 'Phan Van Huan', 1, '2005-04-15', '808 Vinh Phuc, Ha Noi', '8412345736'),
(2, 'BA060', 'Dinh Thi Tuan', 0, '2006-06-12', '909 Yen Bai, Ha Noi', '8412345737');




-- Chèn 6 bản ghi mẫu vào bảng Class 
INSERT INTO [SchoolRules].[dbo].[Class] (SchoolYearID, ClassGroupID, TeacherID, Code, Grade, Name, TotalPoint, Status)
VALUES 
	(1, 1, 2, 'TBT001', 10, '10A1', 100, 'INACTIVE'),
	(1, 1, 3, 'TBT002', 10, '10A2', 100, 'INACTIVE'),
	(1, 1, 4, 'TBT003', 10, '10A3', 100, 'INACTIVE'),
	(1, 2, 5, 'EBT001', 11, '11A1', 100, 'INACTIVE'),
	(1, 2, 6, 'EBT002', 11, '11A2', 100, 'INACTIVE'),
	(1, 2, 7, 'EBT003', 11, '11A3', 100, 'INACTIVE'),
	(1, 3, 8, 'OBT001', 12, '12A1', 100, 'INACTIVE'),
	(1, 3, 9, 'OBT002', 12, '12A2', 100, 'INACTIVE'),
	(1, 3, 10, 'OBT003', 12, '12A3', 100, 'INACTIVE'),
	(2, 1, 10, 'TBT004', 10, '10A1', 90, 'ACTIVE'),
	(2, 1, 9, 'TBT005', 10, '10A2', 95, 'ACTIVE'),
	(2, 1, 8, 'TBT006', 10, '10A3', 100, 'ACTIVE'),
	(2, 2, 7, 'EBT004', 11, '11A1', 100, 'ACTIVE'),
	(2, 2, 6, 'EBT005', 11, '11A2', 95, 'ACTIVE'),
	(2, 2, 5, 'EBT006', 11, '11A3', 100, 'ACTIVE'),
	(2, 3, 4, 'OBT004', 12, '12A1', 90, 'ACTIVE'),
	(2, 3, 3, 'OBT005', 12, '12A2', 100, 'ACTIVE'),
	(2, 3, 2, 'OBT006', 12, '12A3', 100, 'ACTIVE'),

	(3, 4, 12, 'TBA001', 10, '10B1', 100, 'INACTIVE'),
	(3, 4, 13, 'TBA002', 10, '10B2', 100, 'INACTIVE'),
	(3, 4, 14, 'TBA003', 10, '10B3', 100, 'INACTIVE'),
	(3, 5, 15, 'EBA001', 11, '11B1', 100, 'INACTIVE'),
	(3, 5, 16, 'EBA002', 11, '11B2', 100, 'INACTIVE'),
	(3, 5, 17, 'EBA003', 11, '11B3', 100, 'INACTIVE'),
	(3, 6, 18, 'OBA001', 12, '12B1', 100, 'INACTIVE'),
	(3, 6, 19, 'OBA002', 12, '12B1', 100, 'INACTIVE'),
	(3, 6, 20, 'OBA003', 12, '12B1', 100, 'INACTIVE'),

	(4, 4, 12, 'TBA004', 10, '10B1', 100, 'ACTIVE'),
	(4, 4, 13, 'TBA005', 10, '10B2', 100, 'ACTIVE'),
	(4, 4, 14, 'TBA006', 10, '10B3', 100, 'ACTIVE'),
	(4, 5, 15, 'EBA004', 11, '11B1', 100, 'ACTIVE'),
	(4, 5, 16, 'EBA005', 11, '11B2', 100, 'ACTIVE'),
	(4, 5, 17, 'EBA006', 11, '11B3', 100, 'ACTIVE'),
	(4, 6, 18, 'OBA004', 12, '12B1', 100, 'ACTIVE'),
	(4, 6, 19, 'OBA005', 12, '12B1', 100, 'ACTIVE'),
	(4, 6, 20, 'OBA006', 12, '12B1', 100, 'ACTIVE');




-- Chèn 20 bản ghi mẫu vào bảng StudentInClass
INSERT INTO [SchoolRules].[dbo].[StudentInClass] ([ClassID], [StudentID], [EnrollDate], [IsSupervisor], [StartDate], [EndDate], [NumberOfViolation], [Status])
VALUES
(1, 1, '2023-09-01', 1, '2023-09-01', '2024-05-31', 2, 'ENROLLED'),
(1, 2, '2023-09-01', 0, '2023-09-01', '2024-05-31', 2, 'ENROLLED'),
(1, 3, '2023-09-01', 0, '2023-09-01', '2024-05-31', 2, 'ENROLLED'),
(1, 4, '2023-09-01', 0, '2023-09-01', '2024-05-31', 2, 'ENROLLED'),
(1, 5, '2023-09-01', 0, '2023-09-01', '2024-05-31', 2, 'ENROLLED'),
(1, 6, '2023-09-01', 0, '2023-09-01', '2024-05-31', 2, 'ENROLLED'),
(1, 7, '2023-09-01', 0, '2023-09-01', '2024-05-31', 2, 'ENROLLED'),
(1, 8, '2023-09-01', 0, '2023-09-01', '2024-05-31', 2, 'ENROLLED'),
(1, 9, '2023-09-01', 0, '2023-09-01', '2024-05-31', 2, 'ENROLLED'),
(1, 10, '2023-09-01', 0, '2023-09-01', '2024-05-31', 2, 'ENROLLED'),
(2, 11, '2023-09-01', 1, '2023-09-01', '2024-05-31', 2, 'ENROLLED'),
(2, 12, '2023-09-01', 0, '2023-09-01', '2024-05-31', 2, 'ENROLLED'),
(2, 13, '2023-09-01', 0, '2023-09-01', '2024-05-31', 2, 'ENROLLED'),
(2, 14, '2023-09-01', 0, '2023-09-01', '2024-05-31', 2, 'ENROLLED'),
(2, 15, '2023-09-01', 0, '2023-09-01', '2024-05-31', 2, 'ENROLLED'),
(2, 16, '2023-09-01', 0, '2023-09-01', '2024-05-31', 2, 'ENROLLED'),
(2, 17, '2023-09-01', 0, '2023-09-01', '2024-05-31', 2, 'ENROLLED'),
(2, 18, '2023-09-01', 0, '2023-09-01', '2024-05-31', 2, 'ENROLLED'),
(2, 19, '2023-09-01', 0, '2023-09-01', '2024-05-31', 2, 'ENROLLED'),
(2, 20, '2023-09-01', 0, '2023-09-01', '2024-05-31', 2, 'ENROLLED'),
(3, 21, '2023-09-01', 1, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(3, 22, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(3, 23, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(3, 24, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(3, 25, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(3, 26, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(3, 27, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(3, 28, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(3, 29, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(3, 30, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(4, 31, '2023-09-01', 1, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(4, 32, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(4, 33, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(4, 34, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(4, 35, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(4, 36, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(4, 37, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(4, 38, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(4, 39, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(4, 40, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(5, 41, '2023-09-01', 1, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(5, 42, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(5, 43, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(5, 44, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(5, 45, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(5, 46, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(5, 47, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(5, 48, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(5, 49, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(5, 50, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(6, 51, '2023-09-01', 1, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(6, 52, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(6, 53, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(6, 54, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(6, 55, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(6, 56, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(6, 57, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(6, 58, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(6, 59, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(6, 60, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(7, 61, '2023-09-01', 1, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(7, 62, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(7, 63, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(7, 64, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(7, 65, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(7, 66, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(7, 67, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(7, 68, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(7, 69, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(7, 70, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(8, 71, '2023-09-01', 1, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(8, 72, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(8, 73, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(8, 74, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(8, 75, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(8, 76, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(8, 77, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(8, 78, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(8, 79, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(8, 80, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(9, 81, '2023-09-01', 1, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(9, 82, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(9, 83, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(9, 84, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(9, 85, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(9, 86, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(9, 87, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(9, 88, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(9, 89, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),
(9, 90, '2023-09-01', 0, '2023-09-01', '2024-05-31', 0, 'ENROLLED'),

(10, 91, '2024-08-01', 1, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(10, 92, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(10, 93, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(10, 94, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(10, 95, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(10, 96, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(10, 97, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(10, 98, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(10, 99, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(10, 100, '2024-08-01', 0, '2024-08-01', '2005-05-31', 0, 'ENROLLED'),
(11, 101, '2024-08-01', 1, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(11, 102, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(11, 103, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(11, 104, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(11, 105, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(11, 106, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(11, 107, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(11, 108, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(11, 109, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(11, 110, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(12, 111, '2024-08-01', 1, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(12, 112, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(12, 113, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(12, 114, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(12, 115, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(12, 116, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(12, 117, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(12, 118, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(12, 119, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(12, 120, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(13, 1, '2024-08-01', 1, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(13, 2, '2024-08-01', 1, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(13, 3, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(13, 4, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(13, 5, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(13, 6, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(13, 7, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(13, 8, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(13, 9, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(13, 10, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(14, 11, '2024-08-01', 1, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(14, 12, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(14, 13, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(14, 14, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(14, 15, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(14, 16, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(14, 17, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(14, 18, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(14, 19, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(14, 20, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(15, 21, '2024-08-01', 1, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(15, 22, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(15, 23, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(15, 24, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(15, 25, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(15, 26, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(15, 27, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(15, 28, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(15, 29, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(15, 30, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(16, 31, '2024-08-01', 1, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(16, 32, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(16, 33, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(16, 34, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(16, 35, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(16, 36, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(16, 37, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(16, 38, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(16, 39, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(16, 40, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(17, 41, '2024-08-01', 1, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(17, 42, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(17, 43, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(17, 44, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(17, 45, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(17, 46, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(17, 47, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(17, 48, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(17, 49, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(17, 50, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(18, 51, '2024-08-01', 1, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(18, 52, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(18, 53, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(18, 54, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(18, 55, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(18, 56, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(18, 57, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(18, 58, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(18, 59, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),
(18, 60, '2024-08-01', 0, '2024-08-01', '2025-05-31', 0, 'ENROLLED'),

(19, 121, '2023-09-01', 1, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(19, 122, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(19, 123, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(19, 124, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(19, 125, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(19, 126, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(19, 127, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(19, 128, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(19, 129, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(19, 130, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(20, 131, '2023-09-01', 1, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(20, 132, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(20, 133, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(20, 134, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(20, 135, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(20, 136, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(20, 137, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(20, 138, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(20, 139, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(20, 140, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(21, 141, '2023-09-01', 1, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(21, 142, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(21, 143, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(21, 144, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(21, 145, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(21, 146, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(21, 147, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(21, 148, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(21, 149, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(21, 150, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(22, 151, '2023-09-01', 1, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(22, 152, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(22, 153, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(22, 154, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(22, 155, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(22, 156, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(22, 157, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(22, 158, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(22, 159, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(22, 160, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(23, 161, '2023-09-01', 1, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(23, 162, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(23, 163, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(23, 164, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(23, 165, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(23, 166, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(23, 167, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(23, 168, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(23, 169, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(23, 170, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(24, 171, '2023-09-01', 1, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(24, 172, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(24, 173, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(24, 174, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(24, 175, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(24, 176, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(24, 177, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(24, 178, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(24, 179, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED'),
(24, 180, '2023-09-01', 0, '2023-09-01', '2024-05-31', 1, 'ENROLLED');




-- Chèn 6 bản ghi mẫu vào bảng ViolationGroup
INSERT INTO [SchoolRules].[dbo].[ViolationGroup] ([SchoolID], [Code], [Name] ,[Description], [Status])
VALUES
(1, 'VG001', N'Vi phạm chuyên cần' , N'Không tuân thủ quy định về sự hiện diện và tham gia hoạt động của nhà trường.', 'ACTIVE'),
(1, 'VG002', N'Vi phạm nề nếp' , N'Không tuân thủ quy định chung về trật tự, nề nếp trong trường học.', 'ACTIVE'),
(1, 'VG003', N'Vi phạm học tập - thi cử' , N'Gian lận hoặc không trung thực trong quá trình học tập và thi cử.', 'ACTIVE'),
(1, 'VG004', N'Vi phạm đạo đức' , N'Hành vi không đúng mực, trái với đạo đức và giá trị của nhà trường.', 'ACTIVE'),
(1, 'VG005', N'Vi phạm môi trường và tài sản chung' ,N'Gây hại hoặc làm mất trật tự môi trường học tập và tài sản chung.', 'ACTIVE'),
(1, 'VG006', N'Vi phạm tác phong' , N'Không tuân thủ quy định về tác phong, lối sống trong trường học.', 'ACTIVE'),

(2, 'VGBA001', N'Vi phạm quy định về sự hiện diện', N'Không tuân thủ quy định về sự có mặt và tham gia hoạt động của nhà trường.', 'ACTIVE'),
(2, 'VGBA002', N'Vi phạm quy định về trật tự', N'Không tuân thủ quy định chung về trật tự và nề nếp trong trường học.', 'ACTIVE'),
(2, 'VGBA003', N'Vi phạm quy định học tập và thi cử', N'Gian lận hoặc không trung thực trong quá trình học tập và thi cử.', 'ACTIVE'),
(2, 'VGBA004', N'Vi phạm chuẩn mực đạo đức', N'Hành vi không đúng mực, trái với đạo đức và giá trị của nhà trường.', 'ACTIVE'),
(2, 'VGBA005', N'Vi phạm quy định về môi trường và tài sản chung', N'Gây hại hoặc làm mất trật tự môi trường học tập và tài sản chung.', 'ACTIVE'),
(2, 'VGBA006', N'Vi phạm quy định về tác phong', N'Không tuân thủ quy định về tác phong và lối sống trong trường học.', 'ACTIVE');

-- Chèn 58 bản ghi mẫu vào bảng ViolationType
INSERT INTO [SchoolRules].[dbo].[ViolationType] ([ViolationGroupID], [Name], [IsSupervisorOnly] , [Description], [Status])
VALUES
-- Vi phạm chuyên cần
(1, N'Nghỉ học có phép/không phép', 0, N'Không tuân thủ quy định về sự hiện diện', 'ACTIVE'),
(1, N'Đi học trễ', 1, N'Không tuân thủ quy định về sự hiện diện', 'ACTIVE'),
(1, N'Bỏ tiết/trốn tiết', 0, N'Không tuân thủ quy định về sự hiện diện', 'ACTIVE'),
(1, N'Nghỉ buổi lao động có phép/không phép', 1, N'Không tuân thủ quy định về sự hiện diện', 'ACTIVE'),
-- Vi phạm nề nếp
(2, N'Đi lại trên hành lang, ngoài sân trong giờ học', 0, N'Không tuân thủ quy định về trật tự, nề nếp', 'ACTIVE'),
(2, N'Gây ồn ào, mất trật tự', 1, N'Không tuân thủ quy định về trật tự, nề nếp', 'ACTIVE'),
(2, N'Leo rào, trèo tường', 1, N'Không tuân thủ quy định về trật tự, nề nếp', 'ACTIVE'),
(2, N'Đi vệ sinh sai nơi quy định', 1, N'Không tuân thủ quy định về trật tự, nề nếp', 'ACTIVE'),
(2, N'Để xe sai quy định', 1, N'Không tuân thủ quy định về trật tự, nề nếp', 'ACTIVE'),
(2, N'Đưa người lạ mặt vào trường', 0, N'Không tuân thủ quy định về trật tự, nề nếp', 'ACTIVE'),
(2, N'Mang điện thoại, tư trang quý vào trường', 1, N'Không tuân thủ quy định về trật tự, nề nếp', 'ACTIVE'),
(2, N'Vi phạm luật giao thông', 0, N'Không tuân thủ quy định về trật tự, nề nếp', 'ACTIVE'),
(2, N'Uống rượu, hút thuốc, sử dụng chất kích thích gây nghiện', 1, N'Không tuân thủ quy định về trật tự, nề nếp', 'ACTIVE'),
(2, N'Cờ bạc', 1, N'Không tuân thủ quy định về trật tự, nề nếp', 'ACTIVE'),
(2, N'Không mang đủ sách vở, dụng cụ học tập', 0, N'Không tuân thủ quy định về trật tự, nề nếp', 'ACTIVE'),
(2, N'Không chú ý nghe giảng', 0, N'Không tuân thủ quy định về trật tự, nề nếp', 'ACTIVE'),
(2, N'Nói chuyện, làm việc riêng trong giờ học', 0, N'Không tuân thủ quy định về trật tự, nề nếp', 'ACTIVE'),
(2, N'Tự ý ra khỏi lớp trong giờ học', 0, N'Không tuân thủ quy định về trật tự, nề nếp', 'ACTIVE'),
-- Vi phạm học tập - thi cử
(3, N'Không chép bài đầy đủ', 1, N'Gian lận hoặc không trung thực trong quá trình học tập và thi cử', 'ACTIVE'),
(3, N'Không làm bài tập về nhà', 1, N'Gian lận hoặc không trung thực trong quá trình học tập và thi cử', 'ACTIVE'),
(3, N'Ngồi không đúng vị trí trong lớp', 0, N'Gian lận hoặc không trung thực trong quá trình học tập và thi cử', 'ACTIVE'),
(3, N'Quay cóp trong giờ kiểm tra', 0, N'Gian lận hoặc không trung thực trong quá trình học tập và thi cử', 'ACTIVE'),
(3, N'Sử dụng tài liệu trong lúc thi', 0, N'Gian lận hoặc không trung thực trong quá trình học tập và thi cử', 'ACTIVE'),
-- Vi phạm đạo đức
(4, N'Vô lễ với thầy cô giáo', 1, N'Hành vi không đúng mực, trái với đạo đức và giá trị của nhà trường', 'ACTIVE'),
(4, N'Nói tục, chửi thề', 1, N'Hành vi không đúng mực, trái với đạo đức và giá trị của nhà trường', 'ACTIVE'),
(4, N'Nói dối, gian lận', 0, N'Hành vi không đúng mực, trái với đạo đức và giá trị của nhà trường', 'ACTIVE'),
(4, N'Ăn vạ, ăn cắp', 1, N'Hành vi không đúng mực, trái với đạo đức và giá trị của nhà trường', 'ACTIVE'),
(4, N'Bắt nạt, ngược đãi bạn học', 1, N'Hành vi không đúng mực, trái với đạo đức và giá trị của nhà trường', 'ACTIVE'),
(4, N'Gây sự, đánh nhau', 1, N'Hành vi không đúng mực, trái với đạo đức và giá trị của nhà trường', 'ACTIVE'),
(4, N'Ăn mặc phản cảm', 1, N'Hành vi không đúng mực, trái với đạo đức và giá trị của nhà trường', 'ACTIVE'),
(4, N'Lưu hành văn hóa phẩm đồi trụy', 1, N'Hành vi không đúng mực, trái với đạo đức và giá trị của nhà trường', 'ACTIVE'),
(4, N'Sử dụng mạng xã hội một cách tiêu cực', 0, N'Hành vi không đúng mực, trái với đạo đức và giá trị của nhà trường', 'ACTIVE'),
(4, N'Không tham gia hoạt động tình nguyện', 0, N'Hành vi không đúng mực, trái với đạo đức và giá trị của nhà trường', 'ACTIVE'),
-- Vi phạm môi trường và tài sản chung
(5, N'Xả rác bừa bãi', 1, N'Gây hại hoặc làm mất trật tự môi trường học tập và tài sản chung', 'ACTIVE'),
(5, N'Mang đồ ăn vào trong lớp', 1, N'Gây hại hoặc làm mất trật tự môi trường học tập và tài sản chung', 'ACTIVE'),
(5, N'Viết bậy lên mặt bàn/tường', 1, N'Gây hại hoặc làm mất trật tự môi trường học tập và tài sản chung', 'ACTIVE'),
(5, N'Bẻ hoa, cây cảnh trong khuôn viên trường', 1, N'Gây hại hoặc làm mất trật tự môi trường học tập và tài sản chung', 'ACTIVE'),
(5, N'Phung phí điện nước của trường', 1, N'Gây hại hoặc làm mất trật tự môi trường học tập và tài sản chung', 'ACTIVE'),
(5, N'Phá hoại/làm mất tài sản của trường học', 1, N'Gây hại hoặc làm mất trật tự môi trường học tập và tài sản chung', 'ACTIVE'),
(5, N'Trộm cắp tài sản của bạn học, nhà trường', 0, N'Gây hại hoặc làm mất trật tự môi trường học tập và tài sản chung', 'ACTIVE'),
(5, N'Chiếm đoạt hoặc sử dụng trái phép tài sản', 0, N'Gây hại hoặc làm mất trật tự môi trường học tập và tài sản chung', 'ACTIVE'),
-- Vi phạm tác phong
(6, N'Không mặc đồng phục', 1, N'Không tuân thủ quy định về tác phong', 'ACTIVE'),
(6, N'Mặc đồng phục không chuẩn', 1, N'Không tuân thủ quy định về tác phong', 'ACTIVE'),
(6, N'Thay đổi về đồng phục', 1, N'Không tuân thủ quy định về tác phong', 'ACTIVE'),
(6, N'Đeo phụ kiện trái phép', 1, N'Không tuân thủ quy định về tác phong', 'ACTIVE'),
(6, N'Mặc đồng phục bẩn hoặc hư hỏng', 1, N'Không tuân thủ quy định về tác phong', 'ACTIVE'),
(6, N'Mặc quá lố hoặc lòe loẹt', 1, N'Không tuân thủ quy định về tác phong', 'ACTIVE'),
(6, N'Mang giày bẩn hoặc nhếch nhác', 1, N'Không tuân thủ quy định về tác phong', 'ACTIVE'),
(6, N'Mang giày hư', 1, N'Không tuân thủ quy định về tác phong', 'ACTIVE'),
(6, N'Mang giày không an toàn', 1, N'Không tuân thủ quy định về tác phong', 'ACTIVE'),
(6, N'Mang giày không phù hợp với hoạt động', 1, N'Không tuân thủ quy định về tác phong', 'ACTIVE'),
(6, N'Mang giày không liên quan đến môi trường học tập', 1, N'Không tuân thủ quy định về tác phong', 'ACTIVE'),
(6, N'Kiểu tóc không phù hợp', 1, N'Không tuân thủ quy định về tác phong', 'ACTIVE'),
(6, N'Màu tóc không phù hợp', 1, N'Không tuân thủ quy định về tác phong', 'ACTIVE'),
(6, N'Phụ kiện tóc trái phép', 1, N'Không tuân thủ quy định về tác phong', 'ACTIVE'),
(6, N'Tóc bẩn hoặc bù xù', 1, N'Không tuân thủ quy định về tác phong', 'ACTIVE'),
(6, N'Son móng tay', 1, N'Không tuân thủ quy định về tác phong', 'ACTIVE'),
(6, N'Trang điểm quá lố', 1, N'Không tuân thủ quy định về tác phong', 'ACTIVE'),

-- Violation Types for ViolationGroup 'VGBA001'
(7, N'Không tham gia buổi chào cờ', 1, N'Không có mặt trong buổi chào cờ hàng tuần', 'ACTIVE'),
(7, N'Vắng mặt không phép', 0, N'Vắng mặt không có lý do chính đáng hoặc không có sự cho phép', 'ACTIVE'),
(7, N'Đi trễ', 1, N'Đến lớp hoặc các hoạt động của trường muộn', 'ACTIVE'),
(7, N'Không tham gia sinh hoạt ngoại khóa', 0, N'Không tham gia các hoạt động ngoại khóa bắt buộc', 'ACTIVE'),
(7, N'Không có mặt trong giờ học buổi chiều', 0, N'Vắng mặt không có lý do trong giờ học buổi chiều', 'ACTIVE'),
(7, N'Vắng mặt trong giờ thể dục', 0, N'Không tham gia giờ thể dục mà không có lý do chính đáng', 'ACTIVE'),
-- Violation Types for ViolationGroup 'VGBA002'
(8, N'Gây ồn ào trong lớp học', 1, N'Làm ồn, gây mất trật tự trong giờ học', 'ACTIVE'),
(8, N'Đánh nhau', 1, N'Tham gia vào các vụ ẩu đả, bạo lực', 'ACTIVE'),
(8, N'Không xếp hàng', 1, N'Không tuân thủ quy định xếp hàng trong các hoạt động chung', 'ACTIVE'),
(8, N'Chơi điện thoại trong lớp', 1, N'Sử dụng điện thoại trong giờ học', 'ACTIVE'),
(8, N'Gây rối trong buổi lễ', 1, N'Gây mất trật tự trong các buổi lễ của trường', 'ACTIVE'),
(8, N'Gây rối trong thư viện', 1, N'Làm ồn hoặc gây mất trật tự trong thư viện', 'ACTIVE'),
-- Violation Types for ViolationGroup 'VGBA003'
(9, N'Gian lận trong thi cử', 0, N'Sử dụng tài liệu, thiết bị gian lận trong kỳ thi', 'ACTIVE'),
(9, N'Copy bài của bạn', 0, N'Chép bài từ bạn trong các bài kiểm tra, bài thi', 'ACTIVE'),
(9, N'Không làm bài tập về nhà', 1, N'Không hoàn thành bài tập về nhà được giao', 'ACTIVE'),
(9, N'Truất bài kiểm tra', 1, N'Bị truất bài trong kỳ thi vì hành vi không đúng mực', 'ACTIVE'),
(9, N'Truy cập trang web không phù hợp', 1, N'Truy cập vào các trang web không liên quan đến học tập', 'ACTIVE'),
(9, N'Không hoàn thành bài tập nhóm', 1, N'Không đóng góp hoặc hoàn thành phần bài tập nhóm', 'ACTIVE'),
-- Violation Types for ViolationGroup 'VGBA004'
(10, N'Sử dụng ngôn từ không phù hợp', 1, N'Dùng từ ngữ thô tục, xúc phạm', 'ACTIVE'),
(10, N'Gian lận trong hoạt động đoàn thể', 0, N'Không trung thực trong các hoạt động của đoàn thể', 'ACTIVE'),
(10, N'Treo bảng đen quảng cáo', 0, N'Treo bảng quảng cáo không đúng nơi quy định', 'ACTIVE'),
(10, N'Gian lận trong xét điểm', 0, N'Không trung thực trong việc xin điểm cao', 'ACTIVE'),
(10, N'Làm bài tập hộ bạn', 1, N'Làm bài tập hoặc kiểm tra hộ bạn', 'ACTIVE'),
(10, N'Vi phạm quy định về tư cách đạo đức', 0, N'Có hành vi trái với đạo đức và quy chuẩn của nhà trường', 'ACTIVE'),
-- Violation Types for ViolationGroup 'VGBA005'
(11, N'Xả rác bừa bãi', 1, N'Không vứt rác đúng nơi quy định', 'ACTIVE'),
(11, N'Phá hoại tài sản công', 1, N'Gây hư hỏng tài sản của trường', 'ACTIVE'),
(11, N'Sử dụng tài sản không đúng mục đích', 0, N'Sử dụng tài sản của trường không đúng mục đích', 'ACTIVE'),
(11, N'Không bảo vệ môi trường', 1, N'Không tham gia vào các hoạt động bảo vệ môi trường của trường', 'ACTIVE'),
(11, N'Sử dụng tài nguyên trái phép', 0, N'Sử dụng tài nguyên của trường mà không được phép', 'ACTIVE'),
(11, N'Gian lận trong hoạt động xã hội', 0, N'Không trung thực trong các hoạt động xã hội', 'ACTIVE'),
-- Violation Types for ViolationGroup 'VGBA006'
(12, N'Mặc đồng phục không đúng quy định', 1, N'Không tuân thủ quy định về đồng phục', 'ACTIVE'),
(12, N'Không đeo bảng tên', 1, N'Không đeo bảng tên khi đến trường', 'ACTIVE'),
(12, N'Sử dụng trang phục không phù hợp', 1, N'Mặc trang phục không phù hợp trong trường', 'ACTIVE'),
(12, N'Không tuân thủ quy định về trang phục thể dục', 1, N'Không mặc đúng quy định trang phục thể dục', 'ACTIVE'),
(12, N'Thiếu tôn trọng giáo viên', 0, N'Hành vi thiếu tôn trọng đối với giáo viên', 'ACTIVE'),
(12, N'Không đội mũ bảo hiểm', 1, N'Không đội mũ bảo hiểm khi đi xe máy hoặc xe đạp điện', 'ACTIVE');

-- Chèn 12 bản ghi mẫu vào bảng PatrolSchedule
INSERT INTO [SchoolRules].[dbo].[PatrolSchedule] ([ClassID], [UserID], [SupervisorID], [Name], [Slot], [Time], [From], [To], [Status])
VALUES
    -- Lịch tuần tra cho lớp 10A1 năm học 2023
    (1, 3, 1, N'Lịch tuần tra lớp 10A1', 1, '7:00:00', '2023-09-01', '2023-09-15', 'FINISHED'),
    (1, 3, 1, N'Lịch tuần tra lớp 10A1', 1, '7:00:00', '2023-09-16', '2023-09-30', 'FINISHED'),
    -- Lịch tuần tra cho lớp 10A2 năm học 2023
    (2, 3, 2, N'Lịch tuần tra lớp 10A2', 1, '7:00:00', '2023-09-01', '2023-09-15', 'FINISHED'),
    (2, 3, 2, N'Lịch tuần tra lớp 10A2', 1, '7:00:00', '2023-09-16', '2023-09-30', 'FINISHED'),
    -- Lịch tuần tra cho lớp 10A3 năm học 2023
    (3, 3, 3, N'Lịch tuần tra lớp 10A3', 1, '7:00:00', '2023-09-01', '2023-09-15', 'FINISHED'),
    (3, 3, 3, N'Lịch tuần tra lớp 10A3', 1, '7:00:00', '2023-09-16', '2023-09-30', 'FINISHED'),
	-- Lịch tuần tra cho lớp 11A1 năm học 2023
    (4, 61, 4, N'Lịch tuần tra lớp 11A1', 1, '7:00:00', '2023-09-01', '2023-09-15', 'FINISHED'),
    (4, 61, 4, N'Lịch tuần tra lớp 11A1', 1, '7:00:00', '2023-09-16', '2023-09-30', 'FINISHED'),
    -- Lịch tuần tra cho lớp 11A2 năm học 2023
    (5, 61, 5, N'Lịch tuần tra lớp 11A2', 1, '7:00:00', '2023-09-01', '2023-09-15', 'FINISHED'),
    (5, 61, 5, N'Lịch tuần tra lớp 11A2', 1, '7:00:00', '2023-09-16', '2023-09-30', 'FINISHED'),
    -- Lịch tuần tra cho lớp 11A3 năm học 2023
    (6, 61, 6, N'Lịch tuần tra lớp 11A3', 1, '7:00:00', '2023-09-01', '2023-09-15', 'FINISHED'),
    (6, 61, 6, N'Lịch tuần tra lớp 11A3', 1, '7:00:00', '2023-09-16', '2023-09-30', 'FINISHED'),
	-- Lịch tuần tra cho lớp 12A1 năm học 2023
    (7, 62, 7, N'Lịch tuần tra lớp 12A1', 1, '7:00:00', '2023-09-01', '2023-09-15', 'FINISHED'),
    (7, 62, 7, N'Lịch tuần tra lớp 12A1', 1, '7:00:00', '2023-09-16', '2023-09-30', 'FINISHED'),
    -- Lịch tuần tra cho lớp 12A2 năm học 2023
    (8, 62, 8, N'Lịch tuần tra lớp 12A2', 1, '7:00:00', '2023-09-01', '2023-09-15', 'FINISHED'),
    (8, 62, 8, N'Lịch tuần tra lớp 12A2', 1, '7:00:00', '2023-09-16', '2023-09-30', 'FINISHED'),
    -- Lịch tuần tra cho lớp 12A3 năm học 2023
    (9, 62, 9, N'Lịch tuần tra lớp 12A3', 1, '7:00:00', '2023-09-01', '2023-09-15', 'FINISHED'),
    (9, 62, 9, N'Lịch tuần tra lớp 12A3', 1, '7:00:00', '2023-09-16', '2023-09-30', 'FINISHED'),

    -- Lịch tuần tra cho lớp 10A1 năm học 2024
    (10, 3, 10, N'Lịch tuần tra lớp 10A1', 1, '10:30:00', '2024-08-01', '2024-08-15', 'FINISHED'),
    (10, 3, 10, N'Lịch tuần tra lớp 10A1', 1, '10:30:00', '2024-08-15', '2024-08-31', 'ONGOING'),
    -- Lịch tuần tra cho lớp 10A2 năm học 2024
    (11, 3, 11, N'Lịch tuần tra lớp 10A2', 1, '10:30:00', '2024-08-01', '2024-08-15', 'FINISHED'),
    (11, 3, 11, N'Lịch tuần tra lớp 10A2', 1, '10:30:00', '2024-08-15', '2024-08-31', 'ONGOING'),
    -- Lịch tuần tra cho lớp 10A3 năm học 2024
    (12, 3, 12, N'Lịch tuần tra lớp 10A3', 1, '10:30:00', '2024-08-01', '2024-08-15', 'FINISHED'),
    (12, 3, 12, N'Lịch tuần tra lớp 10A3', 1, '10:30:00', '2024-08-15', '2024-08-31', 'ONGOING'),
	-- Lịch tuần tra cho lớp 11A1 năm học 2024
    (13, 61, 13, N'Lịch tuần tra lớp 11A1', 1, '10:30:00', '2024-08-01', '2024-08-15', 'FINISHED'),
    (13, 61, 13, N'Lịch tuần tra lớp 11A1', 1, '10:30:00', '2024-08-15', '2024-08-31', 'ONGOING'),
    -- Lịch tuần tra cho lớp 11A2 năm học 2024
    (14, 61, 14, N'Lịch tuần tra lớp 11A2', 1, '10:30:00', '2024-08-01', '2024-08-15', 'FINISHED'),
    (14, 61, 14, N'Lịch tuần tra lớp 11A2', 1, '10:30:00', '2024-08-15', '2024-08-31', 'ONGOING'),
    -- Lịch tuần tra cho lớp 11A3 năm học 2024
    (15, 61, 15, N'Lịch tuần tra lớp 11A3', 1, '10:30:00', '2024-08-01', '2024-08-15', 'FINISHED'),
    (15, 61, 15, N'Lịch tuần tra lớp 11A3', 1, '10:30:00', '2024-08-01', '2024-08-31', 'ONGOING'),
	-- Lịch tuần tra cho lớp 12A1 năm học 2024
    (16, 62, 16, N'Lịch tuần tra lớp 12A1', 1, '10:30:00', '2024-08-01', '2024-08-15', 'FINISHED'),
    (16, 62, 16, N'Lịch tuần tra lớp 12A1', 1, '10:30:00', '2024-08-15', '2024-08-31', 'ONGOING'),
    -- Lịch tuần tra cho lớp 12A2 năm học 2024
    (17, 62, 17, N'Lịch tuần tra lớp 12A2', 1, '10:30:00', '2024-08-01', '2024-08-15', 'FINISHED'),
    (17, 62, 17, N'Lịch tuần tra lớp 12A2', 1, '10:30:00', '2024-08-15', '2024-08-31', 'ONGOING'),
    -- Lịch tuần tra cho lớp 12A3 năm học 2024
    (18, 62, 18, N'Lịch tuần tra lớp 12A3', 1, '10:30:00', '2024-08-01', '2024-08-15', 'FINISHED'),
    (18, 62, 18, N'Lịch tuần tra lớp 12A3', 1, '10:30:00', '2024-08-15', '2024-08-31', 'ONGOING'),


	-- Lịch tuần tra cho lớp 10A1 năm học 2023
    (19, 33, 19, N'Lịch tuần tra lớp 10A1', 1, '7:30:00', '2023-09-01', '2023-09-15', 'FINISHED'),
    (19, 33, 19, N'Lịch tuần tra lớp 10A1', 1, '7:30:00', '2023-09-16', '2023-09-30', 'FINISHED'),
    -- Lịch tuần tra cho lớp 10A2 năm học 2023
    (20, 33, 20, N'Lịch tuần tra lớp 10A2', 1, '7:30:00', '2023-09-01', '2023-09-15', 'FINISHED'),
    (20, 33, 20, N'Lịch tuần tra lớp 10A2', 1, '7:30:00', '2023-09-16', '2023-09-30', 'FINISHED'),
    -- Lịch tuần tra cho lớp 10A3 năm học 2023
    (21, 33, 21, N'Lịch tuần tra lớp 10A3', 1, '7:30:00', '2023-09-01', '2023-09-15', 'FINISHED'),
    (21, 33, 21, N'Lịch tuần tra lớp 10A3', 1, '7:30:00', '2023-09-16', '2023-09-30', 'FINISHED'),
	-- Lịch tuần tra cho lớp 11A1 năm học 2023
    (22, 63, 22, N'Lịch tuần tra lớp 11A1', 1, '7:30:00', '2023-09-01', '2023-09-15', 'FINISHED'),
    (22, 63, 22, N'Lịch tuần tra lớp 11A1', 1, '7:30:00', '2023-09-16', '2023-09-30', 'FINISHED'),
    -- Lịch tuần tra cho lớp 11A2 năm học 2023
    (23, 63, 23, N'Lịch tuần tra lớp 11A2', 1, '7:30:00', '2023-09-01', '2023-09-15', 'FINISHED'),
    (23, 63, 23, N'Lịch tuần tra lớp 11A2', 1, '7:30:00', '2023-09-16', '2023-09-30', 'FINISHED'),
    -- Lịch tuần tra cho lớp 11A3 năm học 2023
    (24, 63, 24, N'Lịch tuần tra lớp 11A3', 1, '7:30:00', '2023-09-01', '2023-09-15', 'FINISHED'),
    (24, 63, 24, N'Lịch tuần tra lớp 11A3', 1, '7:30:00', '2023-09-16', '2023-09-30', 'FINISHED'),
	-- Lịch tuần tra cho lớp 12A1 năm học 2023
    (25, 64, 25, N'Lịch tuần tra lớp 12A1', 1, '7:30:00', '2023-09-01', '2023-09-15', 'FINISHED'),
    (25, 64, 25, N'Lịch tuần tra lớp 12A1', 1, '7:30:00', '2023-09-16', '2023-09-30', 'FINISHED'),
    -- Lịch tuần tra cho lớp 12A2 năm học 2023
    (26, 64, 26, N'Lịch tuần tra lớp 12A2', 1, '7:30:00', '2023-09-01', '2023-09-15', 'FINISHED'),
    (26, 64, 26, N'Lịch tuần tra lớp 12A2', 1, '7:30:00', '2023-09-16', '2023-09-30', 'FINISHED'),
	-- Lịch tuần tra cho lớp 12A3 năm học 2023
    (27, 64, 27, N'Lịch tuần tra lớp 12A3', 1, '7:30:00', '2023-09-01', '2023-09-15', 'FINISHED'),
    (27, 64, 27, N'Lịch tuần tra lớp 12A3', 1, '7:30:00', '2023-09-16', '2023-09-30', 'FINISHED');


-- Chèn 40 bản ghi mẫu vào bảng Violation
INSERT INTO [SchoolRules].[dbo].[Violation] ([UserID], [ClassID], [ViolationTypeID], [StudentInClassID], [ScheduleID], [Name], [Description], [Date], [CreatedAt], [UpdatedAt], [Status])
VALUES
-- Class 1 Violations
(13, 1, 6, 1, 1, N'Nói chuyện riêng', N'Học sinh nói chuyện trong giờ học', '2023-09-10 10:30:00', '2023-09-10', NULL, 'APPROVED'),
(3, 1, 24, 3, NULL,  N'Ngôn ngữ không phù hợp', N'Học sinh sử dụng ngôn ngữ không phù hợp', '2023-09-15 11:00:00', '2023-09-15', NULL, 'APPROVED'),
(13, 1, 4, 5, 1, N'Không tuân theo lịch sinh hoạt chung', N'Học sinh không tuân theo lịch sinh hoạt chung của nhà trường', '2023-09-10 09:00:00', '2023-09-10', NULL, 'APPROVED'),
(13, 1, 1, 4, 1, N'Nghỉ học không phép', N'Học sinh vắng mặt không có lý do', '2023-09-05 09:00:00', '2023-09-05', NULL, 'APPROVED'),
(13, 1, 2, 5, 1, N'Đi học trễ', N'Học sinh đi học trễ.', '2023-09-10 08:45:00', '2023-09-10', NULL, 'APPROVED'),
(3, 1, 3, 2, NULL, N'Bỏ tiết/trốn tiết', N'Học sinh cố tình bỏ tiết học', '2023-09-18 10:00:00', '2023-09-18', NULL, 'APPROVED'),
(3, 1, 43, 9, NULL, N'Mặc không đúng quy định', N'Học sinh không tuân thủ quy định về trang phục', '2023-09-20 08:00:00', '2023-09-20', NULL, 'APPROVED'),
(13, 1, 45, 3, 2, N'Phụ kiện không phù hợp', N'Học sinh đeo phụ kiện không được phép bởi nhà trường', '2023-09-22 08:15:00', '2023-09-22', NULL, 'APPROVED'),
(13, 1, 23, 9, 2, N'Quay cóp', N'Học sinh bị bắt quay cóp trong kỳ kiểm tra/thi', '2023-09-25 10:00:00', '2023-09-25', NULL, 'APPROVED'),
(3, 1, 22, 10, NULL, N'Đạo văn', N'Học sinh nộp bài có nội dung đạo văn', '2023-09-27', '2023-09-27', NULL, 'APPROVED'),

-- Class 2 Violations
(14, 2, 6, 11, 3, N'Gây ồn ào, mất trật tự', N'Học sinh mất trật tự trong giờ học.', '2023-09-01 12:00:00', '2023-09-01', NULL, 'APPROVED'),
(14, 2, 13, 13, 3, N'Uống rượu, hút thuốc, sử dụng chất kích thích gây nghiện', N'Học sinh sử dụng Uống rượu, hút thuốc, sử dụng chất kích thích gây nghiện', '2023-09-02 11:00:00', '2023-09-02', NULL, 'APPROVED'),
(3, 2, 4, 20, NULL, N'Cờ bạc', N'Học sinh Cờ bạc trong môi trường học tập', '2023-09-03 09:00:00', '2023-09-03', NULL, 'APPROVED'),
(3, 2, 1, 20, NULL, N'Nghỉ học không phép', N'Học sinh vắng mặt không có lý do.', '2023-09-04 09:00:00', '2023-09-04', NULL, 'APPROVED'),
(14, 2, 23, 14, 3, N'Sử dụng tài liệu trong lúc thi', N'Học sinh Sử dụng tài liệu trong lúc thi', '2023-09-05 08:45:00', '2023-09-05', NULL, 'APPROVED'),
(14, 2, 3, 14, 4, N'Bỏ tiết/trốn tiết', N'Học sinh cố tình bỏ tiết học.', '2023-09-15 10:00:00', '2023-09-15', NULL, 'APPROVED'),
(3, 2, 29, 17, NULL, N'Gây sự, đánh nhau', N'Học sinh Gây sự, đánh nhau', '2023-09-18 08:00:00', '2023-09-18', NULL, 'APPROVED'),
(14, 2, 34, 17, 4, N'Xả rác bừa bãi', N'Học sinh Xả rác bừa bãi', '2023-09-20 08:15:00', '2023-09-20', NULL, 'APPROVED'),
(14, 2, 23, 19, 4, N'Quay cóp', N'Học sinh bị bắt quay cóp trong kỳ kiểm tra/thi.', '2023-09-25 10:00:00', '2023-09-25', NULL, 'APPROVED'),
(14, 2, 40, 19, 4, N'Trộm cắp tài sản của bạn học, nhà trường', N'Học sinh Trộm cắp tài sản của bạn học, nhà trường', '2023-09-30 14:00:00', '2023-09-30', NULL, 'APPROVED'),

-- Class 3 Violations
(3, 3, 44, 21, NULL, N'Thay đổi về đồng phục', N'Học sinh tự ý Thay đổi về đồng phục', '2023-09-01 10:30:00', '2023-09-01', NULL, 'APPROVED'),
(3, 3, 31, 25, NULL, N'Lưu hành văn hóa phẩm đồi trụy', N'Học sinh Lưu hành văn hóa phẩm đồi trụy', '2023-09-03 11:00:00', '2023-09-03', NULL, 'APPROVED'),
(15, 3, 29, 29, 5, N'Gây sự, đánh nhau', N'Học sinh Gây sự, đánh nhau', '2023-09-05 09:00:00', '2023-09-05', NULL, 'APPROVED'),
(15, 3, 1, 30, 5, N'Nghỉ học không phép', N'Học sinh vắng mặt không có lý do.', '2023-09-07 09:00:00', '2023-09-07', NULL, 'APPROVED'),
(15, 3, 25, 26, 5, N'Nói tục, chửi thề', N'Học sinh Nói tục, chửi thề', '2023-09-09 08:45:00', '2023-09-09', NULL, 'APPROVED'),
(15, 3, 3, 22, 6, N'Bỏ tiết/trốn tiết', N'Học sinh cố tình bỏ tiết học.', '2023-09-15 10:00:00', '2023-09-15', NULL, 'APPROVED'),
(3, 3, 7, 22, NULL, N'Leo rào, trèo tường', N'Học sinh Leo rào, trèo tường', '2023-09-18 08:00:00', '2023-09-18', NULL, 'APPROVED'),
(3, 3, 10, 29, NULL, N'Đưa người lạ mặt vào trường', N'Học sinh Đưa người lạ mặt vào trường', '2023-09-20 08:15:00', '2023-09-20', NULL, 'APPROVED'),
(15, 3, 11, 25, 6, N'Mang điện thoại, tư trang quý vào trường', N'Học sinh Mang điện thoại, tư trang quý vào trường', '2023-09-25 10:00:00', '2023-09-25', NULL, 'APPROVED'),
(15, 3, 12, 24, 6, N'Vi phạm luật giao thông', N'Học sinh Vi phạm luật giao thông', '2023-09-30 14:00:00', '2023-09-30', NULL, 'APPROVED'),

-- Class 4 Violations
(16, 4, 6, 40, 7, N'Nói chuyện riêng', N'Học sinh nói chuyện trong giờ học.', '2023-09-01 12:00:00', '2023-09-01', NULL, 'APPROVED'),
(16, 4, 24, 35, 7, N'Ngôn ngữ không phù hợp', N'Học sinh sử dụng ngôn ngữ không phù hợp.', '2023-09-03 11:00:00', '2023-09-03', NULL, 'APPROVED'),
(16, 4, 4, 38, 7, N'Không tuân theo lịch sinh hoạt chung', N'Học sinh không tuân theo lịch sinh hoạt chung của nhà trường.', '2023-09-05 09:00:00', '2023-09-05', NULL, 'APPROVED'),
(3, 4, 1, 39, NULL, N'Nghỉ học không phép', N'Học sinh vắng mặt không có lý do.', '2023-09-07 09:00:00', '2023-09-07', NULL, 'APPROVED'),
(3, 4, 2, 33, NULL, N'Đi học trễ', N'Học sinh đi học trễ.', '2023-09-09 08:45:00', '2023-09-09', NULL, 'APPROVED'),
(3, 4, 3, 33, NULL, N'Bỏ tiết/trốn tiết', N'Học sinh cố tình bỏ tiết học.', '2023-09-15 10:00:00', '2023-09-15', NULL, 'APPROVED'),
(3, 4, 43, 31, NULL, N'Mặc không đúng quy định', N'Học sinh không tuân thủ quy định về trang phục.', '2023-09-18 08:00:00', '2023-09-18', NULL, 'APPROVED'),
(3, 4, 45, 26, NULL, N'Phụ kiện không phù hợp', N'Học sinh đeo phụ kiện không được phép bởi nhà trường.', '2023-09-21 08:15:00', '2023-09-21', NULL, 'APPROVED'),
(16, 4, 23, 38, 8, N'Quay cóp', N'Học sinh bị bắt quay cóp trong kỳ kiểm tra/thi.', '2023-09-25 10:00:00', '2023-09-25', NULL, 'APPROVED'),
(16, 4, 22, 34, 8, N'Đạo văn', N'Học sinh nộp bài có nội dung đạo văn.', '2023-09-30 14:00:00', '2023-09-30', NULL, 'APPROVED'),

-- ClassID 19, StudentInClassID 181 -> 190
(33, 19, 59, 182, 37,  N'Không tham gia buổi chào cờ', N'Không có mặt trong buổi chào cờ hàng tuần.', '2023-09-05', '2023-09-05', NULL, 'APPROVED'),
(33, 19, 60, 181, 37, N'Vắng mặt không phép', N'Vắng mặt không có lý do chính đáng hoặc không có sự cho phép.', '2023-09-10', '2023-09-10', NULL, 'APPROVED'),
(33, 19, 61, 189, 37, N'Đi trễ', N'Đến lớp hoặc các hoạt động của trường muộn.', '2023-09-15', '2023-09-15', NULL, 'APPROVED'),
(33, 19, 62, 187, 37, N'Không tham gia sinh hoạt ngoại khóa', N'Không tham gia các hoạt động ngoại khóa bắt buộc.', '2023-09-20', '2023-09-20', NULL, 'APPROVED'),
(33, 19, 63, 183, 37, N'Không có mặt trong giờ học buổi chiều', N'Vắng mặt không có lý do trong giờ học buổi chiều.', '2023-09-25', '2023-09-25', NULL, 'APPROVED'),
(33, 19, 64, 187, 38, N'Vắng mặt trong giờ thể dục', N'Không tham gia giờ thể dục mà không có lý do chính đáng.', '2023-09-30', '2023-09-30', NULL, 'APPROVED'),
(33, 19, 65, 184, 38, N'Gây ồn ào trong lớp học', N'Làm ồn, gây mất trật tự trong giờ học.', '2023-10-05', '2023-10-05', NULL, 'APPROVED'),
(33, 19, 66, 185, 38, N'Đánh nhau', N'Tham gia vào các vụ ẩu đả, bạo lực.', '2023-10-10', '2023-10-10', NULL, 'APPROVED'),
(33, 19, 71, 189, 38, N'Gian lận trong thi cử', N'Sử dụng tài liệu, thiết bị gian lận trong kỳ thi.', '2023-10-15', '2023-10-15', NULL, 'APPROVED'),
(33, 19, 77, 188, 38, N'Sử dụng ngôn từ không phù hợp', N'Dùng từ ngữ thô tục, xúc phạm.', '2023-10-20', '2023-10-20', NULL, 'APPROVED'),

-- ClassID 20, StudentInClassID 191 -> 200
(33, 20, 83, 193, 39,  N'Xả rác bừa bãi', N'Không vứt rác đúng nơi quy định', '2023-10-25', '2023-10-25', NULL, 'APPROVED'),
(33, 20, 89, 192, 39, N'Mặc đồng phục không đúng quy định', N'Không tuân thủ quy định về đồng phục', '2023-10-30', '2023-10-30', NULL, 'APPROVED'),
(33, 20, 59, 193, 39, N'Không tham gia buổi chào cờ', N'Không có mặt trong buổi chào cờ hàng tuần', '2023-11-05', '2023-11-05', NULL, 'APPROVED'),
(33, 20, 60, 198, 39, N'Vắng mặt không phép', N'Vắng mặt không có lý do chính đáng hoặc không có sự cho phép', '2023-11-10', '2023-11-10', NULL, 'APPROVED'),
(33, 20, 61, 191, 39, N'Đi trễ', N'Đến lớp hoặc các hoạt động của trường muộn', '2023-11-15', '2023-11-15', NULL, 'APPROVED'),
(33, 20, 62, 194, 40, N'Không tham gia sinh hoạt ngoại khóa', N'Không tham gia các hoạt động ngoại khóa bắt buộc', '2023-11-20', '2023-11-20', NULL, 'APPROVED'),
(33, 20, 63, 199, 40, N'Không có mặt trong giờ học buổi chiều', N'Vắng mặt không có lý do trong giờ học buổi chiều', '2023-11-25', '2023-11-25', NULL, 'APPROVED'),
(33, 20, 64, 195, 40, N'Vắng mặt trong giờ thể dục', N'Không tham gia giờ thể dục mà không có lý do chính đáng', '2023-11-30', '2023-11-30', NULL, 'APPROVED'),
(33, 20, 65, 194, 40, N'Gây ồn ào trong lớp học', N'Làm ồn, gây mất trật tự trong giờ học', '2023-12-05', '2023-12-05', NULL, 'APPROVED'),
(33, 20, 66, 196, 40, N'Đánh nhau', N'Tham gia vào các vụ ẩu đả, bạo lực', '2023-12-10', '2023-12-10', NULL, 'APPROVED'),

-- ClassID 21, StudentInClassID 201 -> 210
(33, 21, 71, 210, 41, N'Gian lận trong thi cử', N'Sử dụng tài liệu, thiết bị gian lận trong kỳ thi', '2023-12-15', '2023-12-15', NULL, 'APPROVED'),
(33, 21, 77, 202, 41, N'Sử dụng ngôn từ không phù hợp', N'Dùng từ ngữ thô tục, xúc phạm', '2023-12-20', '2023-12-20', NULL, 'APPROVED'),
(33, 21, 83, 204, 41, N'Xả rác bừa bãi', N'Không vứt rác đúng nơi quy định', '2023-12-25', '2023-12-25', NULL, 'APPROVED'),
(33, 21, 89, 204, 41, N'Mặc đồng phục không đúng quy định', N'Không tuân thủ quy định về đồng phục', '2023-12-30', '2023-12-30', NULL, 'APPROVED'),
(33, 21, 59, 202, 41, N'Không tham gia buổi chào cờ', N'Không có mặt trong buổi chào cờ hàng tuần', '2024-01-05', '2024-01-05', NULL, 'APPROVED'),
(33, 21, 60, 207, 42, N'Vắng mặt không phép', N'Vắng mặt không có lý do chính đáng hoặc không có sự cho phép', '2024-01-10', '2024-01-10', NULL, 'APPROVED'),
(33, 21, 61, 209, 42, N'Đi trễ', N'Đến lớp hoặc các hoạt động của trường muộn', '2024-01-15', '2024-01-15', NULL, 'APPROVED'),
(33, 21, 62, 208, 42, N'Không tham gia sinh hoạt ngoại khóa', N'Không tham gia các hoạt động ngoại khóa bắt buộc', '2024-01-20', '2024-01-20', NULL, 'APPROVED'),
(33, 21, 63, 209, 42, N'Không có mặt trong giờ học buổi chiều', N'Vắng mặt không có lý do trong giờ học buổi chiều', '2024-01-25', '2024-01-25', NULL, 'APPROVED'),
(33, 21, 64, 210, 42, N'Vắng mặt trong giờ thể dục', N'Không tham gia giờ thể dục mà không có lý do chính đáng', '2024-01-30', '2024-01-30', NULL, 'APPROVED'),

-- Class 10 Violations
(22, 10, 6, 91, 19, N'Xả rác bừa bãi', N'Học sinh xả rác bừa bãi', '2024-08-28 10:30:00', '2024-08-28', NULL, 'PENDING'),
(22, 10, 24, 96, 19,  N'Ngôn ngữ không phù hợp', N'Học sinh sử dụng ngôn ngữ không phù hợp', '2024-08-28 11:00:00', '2024-08-28', NULL, 'PENDING'),
(22, 10, 4, 100, 19, N'Không tuân theo lịch sinh hoạt chung', N'Học sinh không tuân theo lịch sinh hoạt chung của nhà trường', '2024-08-28 09:00:00', '2024-08-28', NULL, 'PENDING'),

(3, 10, 2, 92, NULL, N'Nghỉ học có phép/không phép', N'Học sinh nghỉ học không phép', '2024-08-28', '2024-08-28', NULL, 'APPROVED'),
(3, 10, 2, 94, NULL, N'Nghỉ học có phép/không phép', N'Học sinh nghỉ học không phép', '2024-08-28', '2024-08-28', NULL, 'APPROVED'),

-- Class 11 Violations
(23, 11, 35, 102, 21, N'Mang đồ ăn vào trong lớp', N'Học sinh mang đồ ăn vào trong lớp', '2024-08-28 09:00:00', '2024-08-28', NULL, 'PENDING'),
(23, 11, 2, 105, 21, N'Đi học trễ', N'Học sinh đi học trễ.', '2024-08-27 08:45:00', '2024-08-28', NULL, 'PENDING'),

(3, 11, 2, 108, NULL, N'Nghỉ học có phép/không phép', N'Học sinh nghỉ học không phép', '2024-08-28', '2024-08-28', NULL, 'APPROVED'),

-- Class 12 Violations
(24, 12, 29, 113, 23, N'Gây sự, đánh nhau', N'Học sinh gây sự, đánh nhau', '2024-08-28 10:00:00', '2024-08-28', NULL, 'PENDING'),
(24, 12, 43, 119, 23, N'Mặc không đúng quy định', N'Học sinh không tuân thủ quy định về trang phục', '2024-08-28 08:00:00', '2024-08-28', NULL, 'PENDING'),
(24, 12, 45, 116, 23, N'Phụ kiện không phù hợp', N'Học sinh đeo phụ kiện không được phép bởi nhà trường', '2024-08-28 08:15:00', '2024-08-28', NULL, 'PENDING'),

-- Class 13 Violations
(25, 13, 25, 127, 25, N'Nói tục, chửi thề', N'Học sinh nói tục, chửi thề', '2024-08-28 10:00:00', '2024-08-28', NULL, 'PENDING'),
(25, 13, 8, 124, 25, N'Đi vệ sinh sai nơi quy định', N'Học sinh đi vệ sinh sai nơi quy định', '2024-08-28', '2024-08-28', NULL, 'PENDING'),

-- Class 14 Violations
(26, 14, 6, 131, 27, N'Xả rác bừa bãi', N'Học sinh xả rác bừa bãi', '2024-08-28 10:30:00', '2024-08-28', NULL, 'PENDING'),
(26, 14, 24, 139, 27,  N'Ngôn ngữ không phù hợp', N'Học sinh sử dụng ngôn ngữ không phù hợp', '2024-08-28 11:00:00', '2024-08-28', NULL, 'PENDING'),
(26, 14, 4, 136, 27, N'Không tuân theo lịch sinh hoạt chung', N'Học sinh không tuân theo lịch sinh hoạt chung của nhà trường', '2024-08-28 09:00:00', '2024-08-28', NULL, 'PENDING'),
(26, 14, 2, 137, 27, N'Đi học trễ', N'Học sinh đi học trễ.', '2024-08-28 08:45:00', '2024-08-28', NULL, 'PENDING'),

(61, 14, 2, 134, NULL, N'Nghỉ học có phép/không phép', N'Học sinh nghỉ học không phép', '2024-08-28', '2024-08-28', NULL, 'APPROVED'),

-- Class 15 Violations
(27, 15, 3, 144, 29, N'Bỏ tiết/trốn tiết', N'Học sinh trốn tiết', '2024-08-28 09:00:00', '2024-08-28', NULL, 'PENDING'),
(27, 15, 2, 141, 29, N'Đi học trễ', N'Học sinh đi học trễ.', '2024-08-28 08:45:00', '2024-08-28', NULL, 'PENDING'),

-- Class 16 Violations
(28, 16, 7, 153, 31, N'Leo rào, trèo tường', N'Học sinh leo rào, trèo tường', '2024-08-28 10:00:00', '2024-08-28', NULL, 'PENDING'),
(28, 16, 43, 159, 31, N'Mặc không đúng quy định', N'Học sinh không tuân thủ quy định về trang phục', '2024-08-28 08:00:00', '2024-08-28', NULL, 'PENDING'),
(28, 16, 45, 160, 31, N'Phụ kiện không phù hợp', N'Học sinh đeo phụ kiện không được phép bởi nhà trường', '2024-08-28 08:15:00', '2024-08-28', NULL, 'PENDING'),

(62, 16, 2, 155, NULL, N'Nghỉ học có phép/không phép', N'Học sinh nghỉ học không phép', '2024-08-28', '2024-08-28', NULL, 'APPROVED'),
(62, 16, 2, 157, NULL, N'Nghỉ học có phép/không phép', N'Học sinh nghỉ học không phép', '2024-08-28', '2024-08-28', NULL, 'APPROVED'),

-- Class 17 Violations
(29, 17, 24, 162, 33, N'Vô lễ với thầy cô giáo', N'Học sinh vô lễ với thầy cô giáo', '2024-08-28 10:00:00', '2024-08-28', NULL, 'PENDING'),
(29, 17, 13, 168, 33, N'Uống rượu, hút thuốc, sử dụng chất kích thích gây nghiện', N'Học sinh sử dụng chất kích thích trong trường học', '2024-08-28', '2024-08-28', NULL, 'PENDING'),

-- Class 18 Violations
(30, 18, 14, 178, 35, N'Cờ bạc', N'Học sinh tổ chức đánh bạc trong trường học', '2024-08-28 10:00:00', '2024-08-28', NULL, 'PENDING'),
(30, 18, 6, 177, 35, N'Gây ồn ào, mất trật tự', N'Học sinh gây ồn ào mất trật tự trong giờ học', '2024-08-28', '2024-08-28', NULL, 'PENDING');


-- Chèn 40 bản ghi mẫu vào bảng Penalty
INSERT INTO [SchoolRules].[dbo].[Penalty] ([SchoolID], [Code], [Name] , [Level], [Description], [Status])
VALUES
(1, 'PTT001', N'Cảnh cáo', 1, N'Cảnh cáo bằng lời hoặc văn bản', 'ACTIVE'),
(1, 'PTT002', N'Phạt lao động', 2, N'Yêu cầu học sinh tham gia các hoạt động lao động công ích', 'ACTIVE'),
(1, 'PTT003', N'Phạt viết bài kiểm điểm', 3, N'Yêu cầu học sinh viết bài kiểm điểm', 'ACTIVE'),
(1, 'PTT004', N'Phạt đình chỉ', 4, N'Đình chỉ học tập trong một khoảng thời gian', 'ACTIVE'),
(1, 'PTT005', N'Phạt đuổi học', 5, N'Đuổi học tạm thời hoặc vĩnh viễn', 'ACTIVE'),

(2, 'PTA001', N'Cảnh cáo', 1, N'Cảnh cáo bằng lời hoặc văn bản', 'ACTIVE'),
(2, 'PTA002', N'Phạt viết bài kiểm điểm', 2, N'Yêu cầu học sinh viết bài kiểm điểm', 'ACTIVE'),
(2, 'PTA003', N'Phạt cấm tham gia hoạt động ngoại khóa', 3, N'Cấm học sinh tham gia các hoạt động ngoại khóa', 'ACTIVE'),
(2, 'PTA004', N'Tước quyền thi đua', 4, N'Tước quyền tham gia các hoạt động thi đua trong một thời gian nhất định', 'ACTIVE'),
(2, 'PTA005', N'Phạt lao động', 5, N'Yêu cầu học sinh tham gia các hoạt động lao động công ích', 'ACTIVE'),
(2, 'PTA006', N'Phạt bồi thường', 6, N'Yêu cầu học sinh bồi thường thiệt hại về tài sản', 'ACTIVE'),
(2, 'PTA007', N'Phạt tạm dừng hỗ trợ học bổng', 7, N'Tạm dừng các hỗ trợ học bổng cho học sinh vi phạm', 'ACTIVE'),
(2, 'PTA008', N'Phạt đình chỉ', 8, N'Đình chỉ học tập trong một khoảng thời gian', 'ACTIVE'),
(2, 'PTA009', N'Phạt đuổi học', 9, N'Đuổi học tạm thời hoặc vĩnh viễn', 'ACTIVE');



-- Chèn 40 bản ghi mẫu vào bảng Discipline
INSERT INTO [SchoolRules].[dbo].[Discipline] ([ViolationID], [PennaltyID], [Description], [StartDate], [EndDate], [Status])
VALUES
(1, 1, N'Nói chuyện riêng', '2023-09-10', '2023-09-12', 'DONE'),
(2, 2, N'Ngôn ngữ không phù hợp', '2023-09-15', '2023-09-18', 'DONE'),
(3, 2, N'Không tuân theo lịch sinh hoạt chung', '2023-09-10', '2023-09-12', 'DONE'),
(4, 3, N'Nghỉ học không phép', '2023-09-05', '2023-09-08', 'DONE'),
(5, 2, N'Đi học trễ', '2023-09-10', '2023-09-13', 'DONE'),
(6, 3, N'Bỏ tiết/trốn tiết', '2023-09-18', '2023-09-21', 'DONE'),
(7, 2, N'Mặc không đúng quy định', '2023-09-20', '2023-09-22', 'DONE'),
(8, 1, N'Phụ kiện không phù hợp', '2023-09-22', '2023-09-23', 'DONE'),
(9, 4, N'Quay cóp', '2023-09-25', '2023-09-28', 'DONE'),
(10, 3, N'Đạo văn', '2023-09-27', '2023-09-29', 'DONE'),

(11, 1, N'Gây ồn ào, mất trật tự', '2023-09-01', '2023-09-04', 'DONE'),
(12, 4, N'Uống rượu, hút thuốc, sử dụng chất kích thích gây nghiện', '2023-09-02', '2023-09-04', 'DONE'),
(13, 4, N'Cờ bạc', '2023-09-03', '2023-09-05', 'DONE'),
(14, 3, N'Nghỉ học không phép', '2023-09-04', '2023-09-06', 'DONE'),
(15, 4, N'Sử dụng tài liệu trong lúc thi', '2023-09-05', '2023-09-06', 'DONE'),
(16, 3, N'Bỏ tiết/trốn tiết', '2023-09-15', '2023-09-17', 'DONE'),
(17, 2, N'Gây sự, đánh nhau', '2023-09-18', '2023-09-21', 'DONE'),
(18, 2, N'Xả rác bừa bãi', '2023-09-20', '2023-09-22', 'DONE'),
(19, 4, N'Quay cóp', '2023-09-25', '2023-09-28', 'DONE'),
(20, 3, N'Trộm cắp tài sản của bạn học, nhà trường', '2023-09-30', '2023-10-02', 'DONE'),

(21, 1, N'Thay đổi về đồng phục', '2023-09-01', '2023-09-04', 'DONE'),
(22, 3, N'Lưu hành văn hóa phẩm đồi trụy', '2023-09-03', '2023-09-05', 'DONE'),
(23, 4, N'Gây sự, đánh nhau', '2023-09-05', '2023-09-08', 'DONE'),
(24, 3, N'Nghỉ học không phép', '2023-09-07', '2023-09-10', 'DONE'),
(25, 1, N'Nói tục, chửi thề', '2023-09-09', '2023-09-11', 'DONE'),
(26, 2, N'Bỏ tiết/trốn tiết', '2023-09-17', '2023-09-17', 'DONE'),
(27, 2, N'Leo rào, trèo tường', '2023-09-18', '2023-09-21', 'DONE'),
(28, 3, N'Đưa người lạ mặt vào trường', '2023-09-20', '2023-09-22', 'DONE'),
(29, 1, N'Mang điện thoại, tư trang quý vào trường', '2023-09-25', '2023-09-28', 'DONE'),
(30, 3, N'Vi phạm luật giao thông', '2023-09-30', '2023-10-02', 'DONE'),

(31, 1, N'Nói chuyện riêng', '2023-09-01', '2023-09-04', 'DONE'),
(32, 2, N'Ngôn ngữ không phù hợp', '2023-09-03', '2023-09-05', 'DONE'),
(33, 3, N'Không tuân theo lịch sinh hoạt chung', '2023-09-05', '2023-09-08', 'DONE'),
(34, 4, N'Nghỉ học không phép', '2023-09-07', '2023-09-10', 'DONE'),
(35, 5, N'Đi học trễ', '2023-09-09', '2023-09-11', 'DONE'),
(36, 1, N'Bỏ tiết/trốn tiết', '2023-09-15', '2023-09-17', 'DONE'),
(37, 2, N'Mặc không đúng quy định', '2023-09-18', '2023-09-21', 'DONE'),
(38, 3, N'Phụ kiện không phù hợp', '2023-09-22', '2023-09-22', 'DONE'),
(39, 4, N'Quay cóp', '2023-09-25', '2023-09-28', 'DONE'),
(40, 5, N'Đạo văn', '2023-09-30', '2023-10-02', 'DONE'),

(41, 6, N'Không tham gia buổi chào cờ', '2023-09-06', '2023-09-10', 'DONE'),
(42, 7, N'Vắng mặt không phép', '2023-09-10', '2023-09-10', 'DONE'),
(43, 8, N'Đi trễ', '2023-09-15', '2023-09-20', 'DONE'),
(44, 9, N'Không tham gia sinh hoạt ngoại khóa', '2023-09-20', '2023-09-25', 'DONE'),
(45, 10, N'Không có mặt trong giờ học buổi chiều', '2023-09-25', '2023-09-30', 'DONE'),
(46, 11, N'Vắng mặt trong giờ thể dục', '2023-09-30', '2023-10-05', 'DONE'),
(47, 12, N'Gây ồn ào trong lớp học', '2023-10-05', '2023-10-10', 'DONE'),
(48, 10, N'Đánh nhau', '2023-10-10', '2023-10-15', 'DONE'),
(49, 13, N'Gian lận trong thi cử', '2023-10-15', '2023-10-20', 'DONE'),
(50, 6, N'Sử dụng ngôn từ không phù hợp', '2023-10-20', '2023-10-25', 'DONE'),

(51, 7, N'Xả rác bừa bãi', '2023-10-25', '2023-10-30', 'DONE'),
(52, 8, N'Mặc đồng phục không đúng quy định', '2023-10-30', '2023-11-3', 'DONE'),
(53, 9, N'Không tham gia buổi chào cờ', '2023-11-05', '2023-11-10', 'DONE'),
(54, 10, N'Vắng mặt không phép', '2023-11-10', '2023-11-15', 'DONE'),
(55, 11, N'Đi trễ', '2023-11-15', '2023-11-20', 'DONE'),
(56, 12, N'Không tham gia sinh hoạt ngoại khóa', '2023-11-20', '2023-11-25', 'DONE'),
(57, 13, N'Không có mặt trong giờ học buổi chiều', '2023-11-25', '2023-11-30', 'DONE'),
(58, 6, N'Vắng mặt trong giờ thể dục', '2023-11-30', '2023-12-7', 'DONE'),
(59, 7, N'Gây ồn ào trong lớp học', '2023-12-05', '2023-12-07', 'DONE'),
(60, 8, N'Đánh nhau', '2023-12-10', '2023-12-14', 'DONE'),

(61, 9, N'Gian lận trong thi cử', '2023-12-15', '2023-12-25', 'DONE'),
(62, 10, N'Sử dụng ngôn từ không phù hợp', '2023-12-20', '2023-12-26', 'DONE'),
(63, 11, N'Xả rác bừa bãi', '2023-12-25', '2023-12-27', 'PENDING'),
(64, 12, N'Mặc đồng phục không đúng quy định', '2023-12-30', '2023-01-03', 'DONE'),
(65, 13, N'Không tham gia buổi chào cờ', '2024-01-05', '2024-01-05', 'DONE'),
(66, 6, N'Vắng mặt không phép', '2024-01-10', '2024-01-10', 'DONE'),
(67, 7, N'Đi trễ', '2024-01-15', '2024-01-15', 'DONE'),
(68, 8, N'Không tham gia sinh hoạt ngoại khóa', '2024-01-20', '2024-01-20', 'DONE'),
(69, 9, N'Không có mặt trong giờ học buổi chiều', '2024-01-25', '2024-01-25', 'DONE'),
(70, 10, N'Vắng mặt trong giờ thể dục', '2024-01-30', '2024-01-30', 'DONE'),

(74, 1, N'Nghỉ học có phép/không phép', '2024-08-28', '2024-09-04', 'PENDING'),
(75, 1, N'Nghỉ học có phép/không phép', '2024-08-28', '2024-09-04', 'PENDING'),
(78, 1, N'Nghỉ học có phép/không phép', '2024-08-28', '2024-09-04', 'PENDING'),
(88, 1, N'Nghỉ học có phép/không phép', '2024-08-28', '2024-09-04', 'PENDING'),
(94, 1, N'Nghỉ học có phép/không phép', '2024-08-28', '2024-09-04', 'PENDING'),
(95, 1, N'Nghỉ học có phép/không phép', '2024-08-28', '2024-09-04', 'PENDING');

-- Chèn 58 bản ghi mẫu vào bảng ViolationConfig
INSERT INTO [SchoolRules].[dbo].[ViolationConfig] ([ViolationTypeID], [MinusPoints], [Description], [Status])
VALUES
-- Vi phạm chuyên cần
(1, 5, N'Không tuân thủ quy định về sự hiện diện', 'ACTIVE'),
(2, 5, N'Không tuân thủ quy định về sự hiện diện', 'ACTIVE'),
(3, 10, N'Không tuân thủ quy định về sự hiện diện', 'ACTIVE'),
(4, 10, N'Không tuân thủ quy định về sự hiện diện', 'ACTIVE'),
-- Vi phạm nề nếp
(5, 5, N'Không tuân thủ quy định về trật tự, nề nếp', 'ACTIVE'),
(6, 5, N'Không tuân thủ quy định về trật tự, nề nếp', 'ACTIVE'),
(7, 10, N'Không tuân thủ quy định về trật tự, nề nếp', 'ACTIVE'),
(8, 10, N'Không tuân thủ quy định về trật tự, nề nếp', 'ACTIVE'),
(9, 10, N'Không tuân thủ quy định về trật tự, nề nếp', 'ACTIVE'),
(10, 20, N'Không tuân thủ quy định về trật tự, nề nếp', 'ACTIVE'),
(11, 10, N'Không tuân thủ quy định về trật tự, nề nếp', 'ACTIVE'),
(12, 20, N'Không tuân thủ quy định về trật tự, nề nếp', 'ACTIVE'),
(13, 30, N'Không tuân thủ quy định về trật tự, nề nếp', 'ACTIVE'),
(14, 30, N'Không tuân thủ quy định về trật tự, nề nếp', 'ACTIVE'),
(15, 5, N'Không tuân thủ quy định về trật tự, nề nếp', 'ACTIVE'),
(16, 5, N'Không tuân thủ quy định về trật tự, nề nếp', 'ACTIVE'),
(17, 5, N'Không tuân thủ quy định về trật tự, nề nếp', 'ACTIVE'),
(18, 5, N'Không tuân thủ quy định về trật tự, nề nếp', 'ACTIVE'),
-- Vi phạm học tập - thi cử
(19, 5, N'Gian lận hoặc không trung thực trong quá trình học tập và thi cử', 'ACTIVE'),
(20, 10, N'Gian lận hoặc không trung thực trong quá trình học tập và thi cử', 'ACTIVE'),
(21, 5, N'Gian lận hoặc không trung thực trong quá trình học tập và thi cử', 'ACTIVE'),
(22, 20, N'Gian lận hoặc không trung thực trong quá trình học tập và thi cử', 'ACTIVE'),
(23, 30, N'Gian lận hoặc không trung thực trong quá trình học tập và thi cử', 'ACTIVE'),
-- Vi phạm đạo đức
(24, 30, N'Hành vi không đúng mực, trái với đạo đức và giá trị của nhà trường', 'ACTIVE'),
(25, 15, N'Hành vi không đúng mực, trái với đạo đức và giá trị của nhà trường', 'ACTIVE'),
(26, 20, N'Hành vi không đúng mực, trái với đạo đức và giá trị của nhà trường', 'ACTIVE'),
(27, 20, N'Hành vi không đúng mực, trái với đạo đức và giá trị của nhà trường', 'ACTIVE'),
(28, 30, N'Hành vi không đúng mực, trái với đạo đức và giá trị của nhà trường', 'ACTIVE'),
(29, 30, N'Hành vi không đúng mực, trái với đạo đức và giá trị của nhà trường', 'ACTIVE'),
(30, 20, N'Hành vi không đúng mực, trái với đạo đức và giá trị của nhà trường', 'ACTIVE'),
(31, 30, N'Hành vi không đúng mực, trái với đạo đức và giá trị của nhà trường', 'ACTIVE'),
(32, 30, N'Hành vi không đúng mực, trái với đạo đức và giá trị của nhà trường', 'ACTIVE'),
(33, 20, N'Hành vi không đúng mực, trái với đạo đức và giá trị của nhà trường', 'ACTIVE'),
-- Vi phạm môi trường và tài sản chung
(34, 15, N'Gây hại hoặc làm mất trật tự môi trường học tập và tài sản chung', 'ACTIVE'),
(35, 10, N'Gây hại hoặc làm mất trật tự môi trường học tập và tài sản chung', 'ACTIVE'),
(36, 20, N'Gây hại hoặc làm mất trật tự môi trường học tập và tài sản chung', 'ACTIVE'),
(37, 15, N'Gây hại hoặc làm mất trật tự môi trường học tập và tài sản chung', 'ACTIVE'),
(38, 10, N'Gây hại hoặc làm mất trật tự môi trường học tập và tài sản chung', 'ACTIVE'),
(39, 20, N'Gây hại hoặc làm mất trật tự môi trường học tập và tài sản chung', 'ACTIVE'),
(40, 30, N'Gây hại hoặc làm mất trật tự môi trường học tập và tài sản chung', 'ACTIVE'),
(41, 30, N'Gây hại hoặc làm mất trật tự môi trường học tập và tài sản chung', 'ACTIVE'),
-- Vi phạm tác phong
(42, 10, N'Không tuân thủ quy định về tác phong', 'ACTIVE'),
(43, 10, N'Không tuân thủ quy định về tác phong', 'ACTIVE'),
(44, 10, N'Không tuân thủ quy định về tác phong', 'ACTIVE'),
(45, 10, N'Không tuân thủ quy định về tác phong', 'ACTIVE'),
(46, 10, N'Không tuân thủ quy định về tác phong', 'ACTIVE'),
(47, 10, N'Không tuân thủ quy định về tác phong', 'ACTIVE'),
(48, 10, N'Không tuân thủ quy định về tác phong', 'ACTIVE'),
(49, 10, N'Không tuân thủ quy định về tác phong', 'ACTIVE'),
(50, 20, N'Không tuân thủ quy định về tác phong', 'ACTIVE'),
(51, 15, N'Không tuân thủ quy định về tác phong', 'ACTIVE'),
(52, 20, N'Không tuân thủ quy định về tác phong', 'ACTIVE'),
(53, 15, N'Không tuân thủ quy định về tác phong', 'ACTIVE'),
(54, 20, N'Không tuân thủ quy định về tác phong', 'ACTIVE'),
(55, 10, N'Không tuân thủ quy định về tác phong', 'ACTIVE'),
(56, 10, N'Không tuân thủ quy định về tác phong', 'ACTIVE'),
(57, 15, N'Không tuân thủ quy định về tác phong', 'ACTIVE'),
(58, 15, N'Không tuân thủ quy định về tác phong', 'ACTIVE'),

-- ViolationConfig for ViolationGroup 'VGBA001'
(59, 5, N'Không tham gia buổi chào cờ hàng tuần', 'ACTIVE'),
(60, 10, N'Vắng mặt không có lý do chính đáng hoặc không có sự cho phép', 'ACTIVE'),
(61, 3, N'Đến lớp hoặc các hoạt động của trường muộn', 'ACTIVE'),
(62, 5, N'Không tham gia các hoạt động ngoại khóa bắt buộc', 'ACTIVE'),
(63, 7, N'Vắng mặt không có lý do trong giờ học buổi chiều', 'ACTIVE'),
(64, 7, N'Không tham gia giờ thể dục mà không có lý do chính đáng', 'ACTIVE'),

-- ViolationConfig for ViolationGroup 'VGBA002'
(65, 5, N'Làm ồn, gây mất trật tự trong giờ học', 'ACTIVE'),
(66, 20, N'Tham gia vào các vụ ẩu đả, bạo lực', 'ACTIVE'),
(67, 2, N'Không tuân thủ quy định xếp hàng trong các hoạt động chung', 'ACTIVE'),
(68, 5, N'Sử dụng điện thoại trong giờ học', 'ACTIVE'),
(69, 7, N'Gây mất trật tự trong các buổi lễ của trường', 'ACTIVE'),
(70, 5, N'Làm ồn hoặc gây mất trật tự trong thư viện', 'ACTIVE'),

-- ViolationConfig for ViolationGroup 'VGBA003'
(71, 15, N'Sử dụng tài liệu, thiết bị gian lận trong kỳ thi', 'ACTIVE'),
(72, 10, N'Chép bài từ bạn trong các bài kiểm tra, bài thi', 'ACTIVE'),
(73, 5, N'Không hoàn thành bài tập về nhà được giao', 'ACTIVE'),
(74, 10, N'Bị truất bài trong kỳ thi vì hành vi không đúng mực', 'ACTIVE'),
(75, 7, N'Truy cập vào các trang web không liên quan đến học tập', 'ACTIVE'),
(76, 5, N'Không đóng góp hoặc hoàn thành phần bài tập nhóm', 'ACTIVE'),

-- ViolationConfig for ViolationGroup 'VGBA004'
(77, 5, N'Dùng từ ngữ thô tục, xúc phạm', 'ACTIVE'),
(78, 10, N'Không trung thực trong các hoạt động của đoàn thể', 'ACTIVE'),
(79, 3, N'Treo bảng quảng cáo không đúng nơi quy định', 'ACTIVE'),
(80, 10, N'Không trung thực trong việc xin điểm cao', 'ACTIVE'),
(81, 7, N'Làm bài tập hoặc kiểm tra hộ bạn', 'ACTIVE'),
(82, 10, N'Có hành vi trái với đạo đức và quy chuẩn của nhà trường', 'ACTIVE'),

-- ViolationConfig for ViolationGroup 'VGBA005'
(83, 3, N'Không vứt rác đúng nơi quy định', 'ACTIVE'),
(84, 10, N'Gây hư hỏng tài sản của trường', 'ACTIVE'),
(85, 5, N'Sử dụng tài sản của trường không đúng mục đích', 'ACTIVE'),
(86, 5, N'Không tham gia vào các hoạt động bảo vệ môi trường của trường', 'ACTIVE'),
(87, 7, N'Sử dụng tài nguyên của trường mà không được phép', 'ACTIVE'),
(88, 10, N'Không trung thực trong các hoạt động xã hội', 'ACTIVE'),

-- ViolationConfig for ViolationGroup 'VGBA006'
(89, 2, N'Không tuân thủ quy định về đồng phục', 'ACTIVE'),
(90, 2, N'Không đeo bảng tên khi đến trường', 'ACTIVE'),
(91, 3, N'Mặc trang phục không phù hợp trong trường', 'ACTIVE'),
(92, 3, N'Không mặc đúng quy định trang phục thể dục', 'ACTIVE'),
(93, 5, N'Hành vi thiếu tôn trọng đối với giáo viên', 'ACTIVE'),
(94, 5, N'Không đội mũ bảo hiểm khi đi xe máy hoặc xe đạp điện', 'ACTIVE');



-- Chèn 2 bản ghi mẫu vào bảng Package
INSERT INTO [SchoolRules].[dbo].[Package] ([Name], [Description], [Price], [Status])
VALUES
(N'Gói Thường', N'Gói thường phù hợp cho các trường quy mô vừa và nhỏ, số lượng vi phạm và học sinh nằm ở mức tiêu chuẩn, nếu quy mô nhà trường có thể mở rộng trong tương lai hãy cân nhắc đăng ký gói Vip', 2000, 'ACTIVE');

-- Chèn 2 bản ghi mẫu vào bảng Order
INSERT INTO [SchoolRules].[dbo].[Order] ([UserID], [PackageID], [OrderCode], [Description], [Total], [AmountPaid], [AmountRemaining], [CounterAccountBankName], [CounterAccountNumber], [CounterAccountName], [Date], [Status])
VALUES
(1, 1, '996807', 'Thanh toán cho Gói Thường', 2000, 2000, 0, NULL, NULL, NULL, '2023-09-02', 'PAID'),
(1, 1, '743501', 'Thanh toán cho Gói Thường', 2000, 2000, 0, NULL, NULL, NULL, '2024-08-06', 'PAID'),
(31, 1, '598406', 'Thanh toán cho Gói Thường', 2000, 2000, 0, NULL, NULL, NULL, '2024-09-02', 'PAID'),
(31, 1, '282534', 'Thanh toán cho Gói Thường', 2000, 2000, 0, NULL, NULL, NULL, '2024-08-02', 'PAID');


-- Chèn 3 bản ghi mẫu vào bảng RegisteredSchool
INSERT INTO [SchoolRules].[dbo].[RegisteredSchool] ([SchoolID], [RegisteredDate], [Description], [Status])
VALUES
(1, '2023-08-30', N'Trường đã hoàn tất quy trình đăng ký và bắt đầu sử dụng hệ thống', 'ACTIVE'),
(2, '2023-08-30', N'Trường đã hoàn tất quy trình đăng ký và bắt đầu sử dụng hệ thống', 'ACTIVE');


-- Chèn 16 bản ghi mẫu vào bảng YearPackage
INSERT INTO [SchoolRules].[dbo].[YearPackage] ([SchoolYearID], [PackageID], [Status])
VALUES
    -- Dữ liệu cho năm học 2023
    (1, 1, 'EXPIRED'),

    -- Dữ liệu cho năm học 2024
    (2, 1, 'VALID'),

    -- Dữ liệu cho năm học 2023
    (3, 1, 'EXPIRED'),

    -- Dữ liệu cho năm học 2024
    (4, 1, 'VALID');


-- Chèn 12 bản ghi mẫu vào bảng ImageURL
INSERT INTO [SchoolRules].[dbo].[ImageURL] ([ViolationID], [PublicID], [URL], [Name], [Description])
VALUES
    -- Liên kết ảnh cho các vi phạm của lớp 1
    (1, NULL, 'http://example.com/images/violation1.jpg', N'Ảnh vi phạm Nói chuyện riêng', N'Ảnh minh họa vi phạm Nói chuyện riêng của học sinh.'),
    (2, NULL, 'http://example.com/images/violation2.jpg', N'Ảnh vi phạm Ngôn ngữ không phù hợp', N'Ảnh minh họa vi phạm Ngôn ngữ không phù hợp của học sinh.'),
    (3, NULL, 'http://example.com/images/violation3.jpg', N'Ảnh vi phạm Không tuân theo chỉ dẫn', N'Ảnh minh họa vi phạm Không tuân theo chỉ dẫn của học sinh.'),
    (4, NULL, 'http://example.com/images/violation4.jpg', N'Ảnh vi phạm Nghỉ học không phép', N'Ảnh minh họa vi phạm Nghỉ học không phép của học sinh.'),
    (5, NULL, 'http://example.com/images/violation5.jpg', N'Ảnh vi phạm Đi học trễ', N'Ảnh minh họa vi phạm Đi học trễ của học sinh.'),
    (6, NULL, 'http://example.com/images/violation6.jpg', N'Ảnh vi phạm Bỏ tiết/trốn tiết', N'Ảnh minh họa vi phạm Bỏ tiết/trốn tiết của học sinh.'),
    (7, NULL, 'http://example.com/images/violation7.jpg', N'Ảnh vi phạm Mặc không đúng quy định', N'Ảnh minh họa vi phạm Mặc không đúng quy định của học sinh.'),
    (8, NULL, 'http://example.com/images/violation8.jpg', N'Ảnh vi phạm Phụ kiện không phù hợp', N'Ảnh minh họa vi phạm Phụ kiện không phù hợp của học sinh.'),
    (9, NULL, 'http://example.com/images/violation9.jpg', N'Ảnh vi phạm Quay cóp', N'Ảnh minh họa vi phạm Quay cóp của học sinh.'),
    (10, NULL, 'http://example.com/images/violation10.jpg', N'Ảnh vi phạm Đạo văn', N'Ảnh minh họa vi phạm Đạo văn của học sinh.'),

    -- Liên kết ảnh cho các vi phạm của lớp 2
    (11, NULL, 'http://example.com/images/violation11.jpg', N'Ảnh vi phạm Gây ồn ào, mất trật tự', N'Ảnh minh họa vi phạm Gây ồn ào, mất trật tự của học sinh.'),
    (12, NULL, 'http://example.com/images/violation12.jpg', N'Ảnh vi phạm Uống rượu, hút thuốc, sử dụng chất kích thích gây nghiện', N'Ảnh minh họa vi phạm sử dụng chất cấm của học sinh.'),
    (13, NULL, 'http://example.com/images/violation13.jpg', N'Ảnh vi phạm Cờ bạc', N'Ảnh minh họa vi phạm tụ tập đánh bài của học sinh.'),
    (14, NULL, 'http://example.com/images/violation14.jpg', N'Ảnh vi phạm Nghỉ học không phép', N'Ảnh minh họa vi phạm Nghỉ học không phép của học sinh.'),
    (15, NULL, 'http://example.com/images/violation15.jpg', N'Ảnh vi phạm Sử dụng tài liệu trong lúc thi', N'Ảnh minh họa vi phạm Sử dụng tài liệu trong lúc thi của học sinh.'),
    (16, NULL, 'http://example.com/images/violation16.jpg', N'Ảnh vi phạm Bỏ tiết/trốn tiết', N'Ảnh minh họa vi phạm Bỏ tiết/trốn tiết của học sinh.'),
    (17, NULL, 'http://example.com/images/violation17.jpg', N'Ảnh vi phạm Gây sự, đánh nhau', N'Ảnh minh họa vi phạm tụ tập gây sự, đánh nhau của học sinh.'),
    (18, NULL, 'http://example.com/images/violation18.jpg', N'Ảnh vi phạm Xả rác bừa bãi', N'Ảnh minh họa vi phạm Xả rác bừa bãi của học sinh.'),
    (19, NULL, 'http://example.com/images/violation19.jpg', N'Ảnh vi phạm Quay cóp', N'Ảnh minh họa vi phạm Quay cóp của học sinh.'),
    (20, NULL, 'http://example.com/images/violation20.jpg', N'Ảnh vi phạmTrộm cắp tài sản của bạn học, nhà trường', N'Ảnh minh họa vi Trộm cắp tài sản của bạn học, nhà trường của học sinh.'),

    -- Liên kết ảnh cho các vi phạm của lớp 3
    (21, NULL, 'http://example.com/images/violation21.jpg', N'Ảnh vi phạm Thay đổi về đồng phục', N'Ảnh minh họa vi phạm Thay đổi về đồng phục của học sinh.'),
    (22, NULL, 'http://example.com/images/violation22.jpg', N'Ảnh vi phạm Lưu hành văn hóa phẩm đồi trụy', N'Ảnh minh họa vi phạm Lưu hành văn hóa phẩm đồi trụy của học sinh.'),
    (23, NULL, 'http://example.com/images/violation23.jpg', N'Ảnh vi phạm Gây sự, đánh nhau', N'Ảnh minh họa vi phạm Gây sự, đánh nhau của học sinh.'),
    (24, NULL ,'http://example.com/images/violation24.jpg', N'Ảnh vi phạm Nghỉ học không phép', N'Ảnh minh họa vi phạm Nghỉ học không phép của học sinh.'),
    (25, NULL ,'http://example.com/images/violation25.jpg', N'Ảnh vi phạm Nói tục, chửi thề', N'Ảnh minh họa vi phạm Nói tục, chửi thề của học sinh.'),
    (26, NULL, 'http://example.com/images/violation26.jpg', N'Ảnh vi phạm Bỏ tiết/trốn tiết', N'Ảnh minh họa vi phạm Bỏ tiết/trốn tiết của học sinh.'),
    (27, NULL, 'http://example.com/images/violation27.jpg', N'Ảnh vi phạm Leo rào, trèo tường', N'Ảnh minh họa vi phạm Leo rào, trèo tường của học sinh.'),
    (28, NULL, 'http://example.com/images/violation28.jpg', N'Ảnh vi phạm Đưa người lạ mặt vào trường', N'Ảnh minh họa vi phạm Đưa người lạ mặt vào trường của học sinh.'),
    (29, NULL, 'http://example.com/images/violation29.jpg', N'Ảnh vi phạm Mang điện thoại, tư trang quý vào trường', N'Ảnh minh họa vi phạm Mang điện thoại, tư trang quý vào trường của học sinh.'),
    (30, NULL,'http://example.com/images/violation30.jpg', N'Ảnh vi phạm Vi phạm luật giao thông', N'Ảnh minh họa vi phạm Vi phạm luật giao thông của học sinh.'),

    -- Liên kết ảnh cho các vi phạm của lớp 4
    (31, NULL, 'http://example.com/images/violation31.jpg', N'Ảnh vi phạm Nói chuyện riêng', N'Ảnh minh họa vi phạm Nói chuyện riêng của học sinh.'),
    (32, NULL, 'http://example.com/images/violation32.jpg', N'Ảnh vi phạm Ngôn ngữ không phù hợp', N'Ảnh minh họa vi phạm Ngôn ngữ không phù hợp của học sinh.'),
    (33, NULL, 'http://example.com/images/violation33.jpg', N'Ảnh vi phạm Không tuân theo chỉ dẫn', N'Ảnh minh họa vi phạm Không tuân theo chỉ dẫn của học sinh.'),
    (34, NULL, 'http://example.com/images/violation34.jpg', N'Ảnh vi phạm Nghỉ học không phép', N'Ảnh minh họa vi phạm Nghỉ học không phép của học sinh.'),
    (35, NULL, 'http://example.com/images/violation35.jpg', N'Ảnh vi phạm Đi học trễ', N'Ảnh minh họa vi phạm Đi học trễ của học sinh.'),
    (36, NULL, 'http://example.com/images/violation36.jpg', N'Ảnh vi phạm Bỏ tiết/trốn tiết', N'Ảnh minh họa vi phạm Bỏ tiết/trốn tiết của học sinh.'),
    (37, NULL, 'http://example.com/images/violation37.jpg', N'Ảnh vi phạm Mặc không đúng quy định', N'Ảnh minh họa vi phạm Mặc không đúng quy định của học sinh.'),
    (38, NULL, 'http://example.com/images/violation38.jpg', N'Ảnh vi phạm Phụ kiện không phù hợp', N'Ảnh minh họa vi phạm Phụ kiện không phù hợp của học sinh.'),
    (39, NULL, 'http://example.com/images/violation39.jpg', N'Ảnh vi phạm Quay cóp', N'Ảnh minh họa vi phạm Quay cóp của học sinh.'),
    (40, NULL, 'http://example.com/images/violation40.jpg', N'Ảnh vi phạm Đạo văn', N'Ảnh minh họa vi phạm Đạo văn của học sinh.'),

	-----------------------------------------------------------------------------------------------------------------------------------------------------------------
    (71, NULL, 'https://baotayninh.vn/image/fckeditor/upload/2017/20170712/images/Rac_thai_IMG_4250.jpg', N'Ảnh vi phạm xả rác bừa bãi', N'Ảnh minh họa vi phạm xả rác bừa bãi của học sinh'),
    (72, NULL, 'https://th.bing.com/th/id/OIP.tYZUpnJhu5zgOI3goZOIoQHaDt?rs=1&pid=ImgDetMain', N'Ảnh vi phạm Ngôn ngữ không phù hợp', N'Ảnh minh họa vi phạm Ngôn ngữ không phù hợp của học sinh'),
    (73, NULL, 'https://khoinguonsangtao.vn/wp-content/uploads/2022/08/hinh-nen-que-huong-tuoi-tho-tron-hoc.jpeg', N'Ảnh vi phạm Không tuân theo lịch sinh hoạt chung', N'Ảnh minh họa vi phạm Không tuân theo lịch sinh hoạt chung'),
    (74, NULL, 'http://example.com/images/violation34.jpg', N'Ảnh vi phạm Nghỉ học không phép', N'Ảnh minh họa vi phạm Nghỉ học không phép của học sinh'),
    (75, NULL, 'http://example.com/images/violation35.jpg', N'Ảnh vi phạm Học sinh nghỉ học không phép', N'Ảnh minh họa vi phạm Học sinh nghỉ học không phép của học sinh.'),
    (76, NULL, 'https://th.bing.com/th/id/OIP.AFvJZDVG9rEYMyX7LBeHWgHaFk?rs=1&pid=ImgDetMain', N'Ảnh vi phạm Mang đồ ăn vào trong lớp', N'Ảnh minh họa vi phạm mang đồ ăn vào trong lớp của học sinh.'),
    (77, NULL, 'https://th.bing.com/th/id/R.7f17728311689a8368d3edcec251676a?rik=iOuPsBM3UvCWyw&riu=http%3a%2f%2fmedia.doisongphapluat.com%2f688%2f2020%2f1%2f20%2ftreo-tuong-vao-truong-nhung-ha-canh-nham-cho-nam-sinh-nhan-cai-ket-do-khoc-do-cuoi.jpg&ehk=h64KFOL57cnRKblDEll2I6lXdQmuOfsQMnaM4oOc2UE%3d&risl=&pid=ImgRaw&r=0', N'Ảnh vi phạm đi học trễ', N'Ảnh minh họa vi phạm đi học trễ của học sinh.'),
	(78, NULL, 'http://example.com/images/violation37.jpg', N'Ảnh vi phạm Học sinh nghỉ học không phép', N'Ảnh minh họa vi phạm Học sinh nghỉ học không phép'),
    (79, NULL, 'https://danviet.mediacdn.vn/296231569849192448/2022/11/11/a48bf34f2e94e8cab185-1668146536666587176546-16681532086831445341092.jpeg', N'Ảnh vi phạm Gây sự, đánh nhau', N'Ảnh minh họa vi phạm Gây sự, đánh nhau của học sinh.'),
    (80, NULL, 'https://th.bing.com/th/id/OIP.XtLAK7IGgP8yDb1WCjdZQgHaE8?rs=1&pid=ImgDetMain', N'Ảnh vi phạm Mặc không đúng quy định', N'Ảnh minh họa vi phạm Mặc không đúng quy định'),
    (81, NULL, 'https://th.bing.com/th/id/R.4bf26103f9b2112d0ab2e969bf3e7ff8?rik=XtMKI4TZG9cRhg&pid=ImgRaw&r=0', N'Ảnh vi phạm Phụ kiện không phù hợp', N'Ảnh minh họa vi phạm Phụ kiện không phù hợp'),

    (82, NULL, 'https://th.bing.com/th/id/OIP.MSfp9M0UCCpN3EO6BOZACwHaEK?rs=1&pid=ImgDetMain', N'Ảnh vi phạm Nói tục, chửi thề', N'Ảnh minh họa vi phạm Nói tục, chửi thề'),
    (83, NULL, 'https://th.bing.com/th/id/OIP.fwHFkRYU4VGc9eUBwGubjwHaEo?rs=1&pid=ImgDetMain', N'Ảnh vi phạm Đi vệ sinh sai nơi quy định', N'Ảnh minh họa vi phạm Đi vệ sinh sai nơi quy định của học sinh.'),
    (84, NULL, 'https://baotayninh.vn/image/fckeditor/upload/2017/20170712/images/Rac_thai_IMG_4250.jpg', N'Ảnh vi phạm xả rác bừa bãi', N'Ảnh minh họa vi phạm xả rác bừa bãi của học sinh.'),
    (85, NULL, 'https://toplist.vn/images/800px/bai-van-nghi-luan-ve-hien-tuong-noi-tuc-chui-the-trong-gioi-tre-hien-nay-so-2-603307.jpg', N'Ảnh vi phạm Ngôn ngữ không phù hợp', N'Ảnh minh họa vi phạm Ngôn ngữ không phù hợp'),
    (86, NULL, 'https://khoinguonsangtao.vn/wp-content/uploads/2022/08/hinh-nen-que-huong-tuoi-tho-tron-hoc.jpeg', N'Ảnh vi phạm Không tuân theo lịch sinh hoạt chung', N'Ảnh minh họa vi phạm Không tuân theo lịch sinh hoạt chung của học sinh.'),
    (87, NULL, 'https://th.bing.com/th/id/R.7f17728311689a8368d3edcec251676a?rik=iOuPsBM3UvCWyw&riu=http%3a%2f%2fmedia.doisongphapluat.com%2f688%2f2020%2f1%2f20%2ftreo-tuong-vao-truong-nhung-ha-canh-nham-cho-nam-sinh-nhan-cai-ket-do-khoc-do-cuoi.jpg&ehk=h64KFOL57cnRKblDEll2I6lXdQmuOfsQMnaM4oOc2UE%3d&risl=&pid=ImgRaw&r=0', N'Ảnh vi phạm Đi học trễ', N'Ảnh minh họa vi phạm Đi học trễ của học sinh.'),
    (88, NULL, 'http://example.com/images/violation37.jpg', N'Ảnh vi phạm Học sinh nghỉ học không phép', N'Ảnh minh họa vi phạm Học sinh nghỉ học không phép'),
    (89, NULL, 'https://dudoanketquaxoso.com/images/2016/02/04/Ca/1_1454550167_tron-hoc.jpg', N'Ảnh vi phạm bỏ tiết/trốn tiết', N'Ảnh minh họa vi phạm bỏ tiết/trốn tiết'),
    (90, NULL, 'https://th.bing.com/th/id/R.7f17728311689a8368d3edcec251676a?rik=iOuPsBM3UvCWyw&riu=http%3a%2f%2fmedia.doisongphapluat.com%2f688%2f2020%2f1%2f20%2ftreo-tuong-vao-truong-nhung-ha-canh-nham-cho-nam-sinh-nhan-cai-ket-do-khoc-do-cuoi.jpg&ehk=h64KFOL57cnRKblDEll2I6lXdQmuOfsQMnaM4oOc2UE%3d&risl=&pid=ImgRaw&r=0', N'Ảnh vi phạm Đi học trễ', N'Ảnh minh họa vi phạm Đi học trễ của học sinh.'),
    (91, NULL, 'https://dudoanketquaxoso.com/images/2016/02/04/Ca/1_1454550167_tron-hoc.jpg', N'Ảnh vi phạm Leo rào, trèo tường', N'Ảnh minh họa vi phạm Leo rào, trèo tường của học sinh.'),

    (92, NULL, 'https://th.bing.com/th/id/OIP.XtLAK7IGgP8yDb1WCjdZQgHaE8?rs=1&pid=ImgDetMain', N'Ảnh vi phạm Mặc không đúng quy định', N'Ảnh minh họa vi phạm Mặc không đúng quy định của học sinh.'),
    (93, NULL, 'https://th.bing.com/th/id/R.4bf26103f9b2112d0ab2e969bf3e7ff8?rik=XtMKI4TZG9cRhg&pid=ImgRaw&r=0', N'Ảnh vi phạm Phụ kiện không phù hợp', N'Ảnh minh họa vi phạm Phụ kiện không phù hợp của học sinh.'),
    (94, NULL, 'http://example.com/images/violation33.jpg', N'Ảnh vi phạm Nghỉ học có phép/không phép', N'Ảnh minh họa vi phạm Nghỉ học có phép/không phép của học sinh.'),
    (95, NULL, 'http://example.com/images/violation34.jpg', N'Ảnh vi phạm Nghỉ học không phép', N'Ảnh minh họa vi phạm Nghỉ học không phép của học sinh.'),
    (96, NULL, 'https://imgs.vietnamnet.vn/Images/2017/05/11/17/20170511173421-hoc-sinh.jpg', N'Ảnh vi phạm Vô lễ với thầy cô giáo', N'Ảnh minh họa vi phạm Vô lễ với thầy cô giáo'),
    (97, NULL, 'https://th.bing.com/th/id/R.1c273bc1f301e9f3956b3596909d6400?rik=ubQohmEstKzjeA&riu=http%3a%2f%2fmedia.xanhx.vn%2fstorage%2fnewsportal%2f2021%2f4%2f16%2f8603%2fMa-Tuy.jpeg&ehk=bbgz9Y%2fKbZ8Qbovr27eBBY3tWsfFhmsf2FJ7h9YTQvc%3d&risl=&pid=ImgRaw&r=0', N'Ảnh vi phạm Uống rượu, hút thuốc, sử dụng chất kích thích gây nghiện', N'Ảnh minh họa vi phạm Uống rượu, hút thuốc, sử dụng chất kích thích gây nghiện của học sinh.'),
    (98, NULL, 'https://th.bing.com/th/id/OIP.BAA9MyJF93dkN3kUIZ87zgHaEK?rs=1&pid=ImgDetMain', N'Ảnh vi phạm Cờ bạc', N'Ảnh minh họa vi phạm Cờ bạc của học sinh.'),
    (99, NULL, 'https://th.bing.com/th/id/OIP.FI8IUKzsf41XlLd75361NAAAAA?rs=1&pid=ImgDetMain', N'Ảnh vi phạm Gây ồn ào, mất trật tự', N'Ảnh minh họa vi phạm Gây ồn ào, mất trật tự của học sinh.');


-- Chèn 12 bản ghi mẫu vào bảng Evaluation
INSERT INTO [SchoolRules].[dbo].[Evaluation] ([ClassID], [Description], [From], [To], [Points], [Status])
VALUES
-- Class 1 Evaluations
(1, N'Điểm thi đua tuần 1 tháng 9', '2023-09-04', '2023-09-10', 72, 'INACTIVE'),
(1, N'Điểm thi đua tuần 2 tháng 9', '2023-09-11', '2023-09-17', 83, 'INACTIVE'),
(1, N'Điểm thi đua tuần 1 tháng 10', '2023-10-02', '2023-10-08', 78, 'INACTIVE'),
(1, N'Điểm thi đua tuần 2 tháng 10', '2023-10-09', '2023-10-15', 85, 'INACTIVE'),

-- Class 2 Evaluations
(2, N'Điểm thi đua tuần 1 tháng 9', '2023-09-04', '2023-09-10', 78, 'INACTIVE'),
(2, N'Điểm thi đua tuần 2 tháng 9', '2023-09-11', '2023-09-17', 88, 'INACTIVE'),
(2, N'Điểm thi đua tuần 1 tháng 10', '2023-10-02', '2023-10-08', 98, 'INACTIVE'),
(2, N'Điểm thi đua tuần 2 tháng 10', '2023-10-09', '2023-10-15', 64, 'INACTIVE'),

-- Class 3 Evaluations
(3, N'Điểm thi đua tuần 1 tháng 9', '2023-09-04', '2023-09-10', 40, 'INACTIVE'),
(3, N'Điểm thi đua tuần 2 tháng 9', '2023-09-11', '2023-09-17', 60, 'INACTIVE'),
(3, N'Điểm thi đua tuần 1 tháng 10', '2023-10-02', '2023-10-08', 72, 'INACTIVE'),
(3, N'Điểm thi đua tuần 2 tháng 10', '2023-10-09', '2023-10-15', 66, 'INACTIVE'),

-- Class 4 Evaluations
(4, N'Điểm thi đua tuần 1 tháng 9', '2023-09-04', '2023-09-10', 85, 'INACTIVE'),
(4, N'Điểm thi đua tuần 2 tháng 9', '2023-09-11', '2023-09-17', 77, 'INACTIVE'),
(4, N'Điểm thi đua tuần 1 tháng 10', '2023-10-02', '2023-10-08', 78, 'INACTIVE'),
(4, N'Điểm thi đua tuần 2 tháng 10', '2023-10-09', '2023-10-15', 90, 'INACTIVE'),

-- Class 5 Evaluations
(5, N'Điểm thi đua tuần 1 tháng 9', '2023-09-04', '2023-09-10', 55, 'INACTIVE'),
(5, N'Điểm thi đua tuần 2 tháng 9', '2023-09-11', '2023-09-17', 67, 'INACTIVE'),
(5, N'Điểm thi đua tuần 1 tháng 10', '2023-10-02', '2023-10-08', 78, 'INACTIVE'),
(5, N'Điểm thi đua tuần 2 tháng 10', '2023-10-09', '2023-10-15', 80, 'INACTIVE'),

-- Class 6 Evaluations
(6, N'Điểm thi đua tuần 1 tháng 9', '2023-09-04', '2023-09-10', 45, 'INACTIVE'),
(6, N'Điểm thi đua tuần 2 tháng 9', '2023-09-11', '2023-09-17', 87, 'INACTIVE'),
(6, N'Điểm thi đua tuần 1 tháng 10', '2023-10-02', '2023-10-08', 88, 'INACTIVE'),
(6, N'Điểm thi đua tuần 2 tháng 10', '2023-10-09', '2023-10-15', 80, 'INACTIVE'),

-- Class 7 Evaluations
(7, N'Điểm thi đua tuần 1 tháng 9', '2023-09-04', '2023-09-10', 77, 'INACTIVE'),
(7, N'Điểm thi đua tuần 2 tháng 9', '2023-09-11', '2023-09-17', 84, 'INACTIVE'),
(7, N'Điểm thi đua tuần 1 tháng 10', '2023-10-02', '2023-10-08', 83, 'INACTIVE'),
(7, N'Điểm thi đua tuần 2 tháng 10', '2023-10-09', '2023-10-15', 70, 'INACTIVE'),

-- Class 8 Evaluations
(8, N'Điểm thi đua tuần 1 tháng 9', '2023-09-04', '2023-09-10', 84, 'INACTIVE'),
(8, N'Điểm thi đua tuần 2 tháng 9', '2023-09-11', '2023-09-17', 75, 'INACTIVE'),
(8, N'Điểm thi đua tuần 1 tháng 10', '2023-10-02', '2023-10-08', 82, 'INACTIVE'),
(8, N'Điểm thi đua tuần 2 tháng 10', '2023-10-09', '2023-10-15', 68, 'INACTIVE'),

-- Class 9 Evaluations
(9, N'Điểm thi đua tuần 1 tháng 9', '2023-09-04', '2023-09-10', 95, 'INACTIVE'),
(9, N'Điểm thi đua tuần 2 tháng 9', '2023-09-11', '2023-09-17', 87, 'INACTIVE'),
(9, N'Điểm thi đua tuần 1 tháng 10', '2023-10-02', '2023-10-08', 77, 'INACTIVE'),
(9, N'Điểm thi đua tuần 2 tháng 10', '2023-10-09', '2023-10-15', 74, 'INACTIVE'),

(10, N'Điểm thi đua tuần 1 tháng 8', '2024-08-05', '2023-08-11', 96, 'ACTIVE'),
(11, N'Điểm thi đua tuần 1 tháng 8', '2024-08-05', '2023-08-11', 70, 'ACTIVE'),
(12, N'Điểm thi đua tuần 1 tháng 8', '2024-08-05', '2023-08-11', 84, 'ACTIVE'),
(13, N'Điểm thi đua tuần 1 tháng 8', '2024-08-05', '2023-08-11', 88, 'ACTIVE'),
(14, N'Điểm thi đua tuần 1 tháng 8', '2024-08-05', '2023-08-11', 65, 'ACTIVE'),
(15, N'Điểm thi đua tuần 1 tháng 8', '2024-08-05', '2023-08-11', 75, 'ACTIVE'),
(16, N'Điểm thi đua tuần 1 tháng 8', '2024-08-05', '2023-08-11', 100, 'ACTIVE'),
(17, N'Điểm thi đua tuần 1 tháng 8', '2024-08-05', '2023-08-11', 90, 'ACTIVE'),
(18, N'Điểm thi đua tuần 1 tháng 8', '2024-08-05', '2023-08-11', 80, 'ACTIVE'),

(10, N'Điểm thi đua tuần 2 tháng 8', '2024-08-12', '2023-08-18', 90, 'ACTIVE'),
(11, N'Điểm thi đua tuần 2 tháng 8', '2024-08-12', '2023-08-18', 85, 'ACTIVE'),
(12, N'Điểm thi đua tuần 2 tháng 8', '2024-08-12', '2023-08-18', 75, 'ACTIVE'),
(13, N'Điểm thi đua tuần 2 tháng 8', '2024-08-12', '2023-08-18', 80, 'ACTIVE'),
(14, N'Điểm thi đua tuần 2 tháng 8', '2024-08-12', '2023-08-18', 90, 'ACTIVE'),
(15, N'Điểm thi đua tuần 2 tháng 8', '2024-08-12', '2023-08-18', 95, 'ACTIVE'),
(16, N'Điểm thi đua tuần 2 tháng 8', '2024-08-12', '2023-08-18', 85, 'ACTIVE'),
(17, N'Điểm thi đua tuần 2 tháng 8', '2024-08-12', '2023-08-18', 75, 'ACTIVE'),
(18, N'Điểm thi đua tuần 2 tháng 8', '2024-08-12', '2023-08-18', 60, 'ACTIVE');