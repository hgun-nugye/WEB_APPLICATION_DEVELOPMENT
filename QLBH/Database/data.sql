USE Huong_Nguyen_Thi_Thanh_6513124_DB_QLBH;
GO

-- 1. Tỉnh
EXEC sp_Tinh_Insert N'Hà Nội';
EXEC sp_Tinh_Insert N'Hồ Chí Minh';
EXEC sp_Tinh_Insert N'Đà Nẵng';
GO

-- 2. Xã
EXEC sp_Xa_Insert N'Phường Bến NGH000000é', 2;
EXEC sp_Xa_Insert N'Phường Cầu Ông Lãnh', 2;
EXEC sp_Xa_Insert N'Phường Tràng Tiền', 1;
EXEC sp_Xa_Insert N'Phường Hải Châu 1', 3;
GO

-- 3. Nhóm sản phẩm
EXEC sp_NhomSP_Insert N'Mỹ phẩm chăm sóc da';
EXEC sp_NhomSP_Insert N'Mỹ phẩm trang điểm';
EXEC sp_NhomSP_Insert N'Sản phẩm chăm sóc tóc';
EXEC sp_NhomSP_Insert N'Sản phẩm chăm sóc da';
GO

-- 4. Loại sản phẩm
EXEC sp_LoaiSP_Insert N'Sữa rửa mặt', 'NSP0000001';
EXEC sp_LoaiSP_Insert N'Kem chống nắng', 'NSP0000001';
EXEC sp_LoaiSP_Insert N'Son môi', 'NSP0000002';
EXEC sp_LoaiSP_Insert N'Dầu gội', 'NSP0000003';
EXEC sp_LoaiSP_Insert N'Tẩy trang', 'NSP0000004';
GO

-- 5. Nhà cung cấp 
EXEC sp_NhaCC_Insert N'L''Oréal Việt Nam', '0909123456', 'contact@loreal.vn', N'Quận 1, TP. Hồ Chí Minh';
EXEC sp_NhaCC_Insert N'Unilever Việt Nam', '0909988776', 'support@unilever.vn',N'Quận 7, TP. Hồ Chí Minh';
EXEC sp_NhaCC_Insert N'Senka Japan', '0911222333', 'info@senka.jp',N'Tokyo, Nhật Bản';
GO


-- 6. Gian hàng
EXEC sp_GianHang_Insert 
    N'L''Oréal Official Store', 
    N'Gian hàng chính hãng L''Oréal tại Việt Nam', 
    '0901234567', 
    'loreal@store.vn', 
    N'123 Đường Nguyễn Trãi, Quận 5, TP. Hồ Chí Minh';

EXEC sp_GianHang_Insert 
    N'Unilever Official Store', 
    N'Gian hàng chính hãng Unilever', 
    '0909988776', 
    'unilever@store.vn', 
    N'45 Đường Cộng Hòa, Quận Tân Bình, TP. Hồ Chí Minh';

EXEC sp_GianHang_Insert 
    N'Senka Japan Store', 
    N'Sản phẩm chăm sóc da từ Nhật Bản', 
    '0911222333', 
    'senka@store.jp', 
    N'Tokyo, Nhật Bản';

GO

-- 7. Sản phẩm
EXEC sp_SanPham_Insert N'Sữa rửa mặt Senka Perfect Whip', 120000, N'Làm sạch sâu, tạo bọt mịn', N'senka_whip.jpg', 'LSP0000001', 'GH00000003';
EXEC sp_SanPham_Insert N'Kem chống nắng L''Oréal UV Defender', 250000, N'Chống nắng SPF50+, dưỡng da', N'loreal_uv.jpg', 'LSP0000002', 'GH00000001';
EXEC sp_SanPham_Insert N'Son lì Maybelline Matte Ink', 220000, N'Màu chuẩn, lâu trôi', N'matte_ink.jpg', 'LSP0000003', 'GH00000001';
EXEC sp_SanPham_Insert N'Dầu gội Dove phục hồi hư tổn', 180000, N'Phục hồi tóc yếu gãy rụng', N'dove_repair.jpg', 'LSP0000004', 'GH00000002';
GO

-- 8. Khách hàng
EXEC sp_KhachHang_Insert N'Nguyễn Thị Mai', '0909000011', 'mai.nguyen@gmail.com', N'12 Nguyễn Huệ, Q.1, TP.HCM';
EXEC sp_KhachHang_Insert N'Trần Văn Nam', '0909345678', 'namtv@gmail.com', N'20 Lý Tự Trọng, Q.1, TP.HCM';
GO

-- Chi tiết mua hàng và Đơn mua hàng
-- Tạo biến table chứa danh sách chi tiết mua hàng
DECLARE @CTMH CTMH_List;

-- Thêm nhiều sản phẩm vào danh sách
INSERT INTO @CTMH (MaSP, SLM, DGM)
VALUES 
('SP00000001', 10, 120000),
('SP00000002', 20, 180000),
('SP00000003', 15, 200000);

-- Gọi procedure 
EXEC sp_DonMuaHang_Insert 
     @NgayMH = '2025-10-20',
     @MaNCC = 'NCC0000002',
     @ChiTiet = @CTMH;
GO

-- Tạo danh sách sản phẩm bán
DECLARE @CTBH CTBH_List;

INSERT INTO @CTBH (MaSP, SLB, DGB)
VALUES
('SP00000001', 2, 150000),
('SP00000002', 1, 180000),
('SP00000003', 5, 120000);

-- Gọi procedure
EXEC sp_DonBanHang_Insert 
     @NgayBH = '2025-10-20',
     @MaKH = 'KH00000001',
     @ChiTiet = @CTBH;
GO

