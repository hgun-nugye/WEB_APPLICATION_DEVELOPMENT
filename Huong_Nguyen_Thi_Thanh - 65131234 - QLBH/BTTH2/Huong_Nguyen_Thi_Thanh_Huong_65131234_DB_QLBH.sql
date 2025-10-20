CREATE DATABASE Huong_Nguyen_Thi_Thanh_6513124_DB_QLBH
GO
CREATE TABLE NhaCC (
    MaNCC VARCHAR(10) PRIMARY KEY NOT NULL,
    TenNCC NVARCHAR(100) NOT NULL,
    DienThoaiNCC VARCHAR(15) NOT NULL,
    EmailNCC VARCHAR(100) NULL,
    DiaChiNCC NVARCHAR(200) NOT NULL
);
GO
INSERT INTO NhaCC (MaNCC, TenNCC, DienThoaiNCC, EmailNCC, DiaChiNCC)
VALUES
('NCC001', N'Công ty TNHH Thiên Long', '0909123456', 'lienhe@thienlong.vn', N'123 Lê Lợi, Quận 1, TP.HCM'),
('NCC002', N'Công ty Cổ phần Văn Phòng Phẩm Hòa Bình', '0912233445', 'info@hoabinhco.com', N'45 Trần Hưng Đạo, Hà Nội');

