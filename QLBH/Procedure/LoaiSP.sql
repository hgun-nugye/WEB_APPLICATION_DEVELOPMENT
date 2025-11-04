USE Huong_Nguyen_Thi_Thanh_6513124_DB_QLBH;
GO
-- Insert
CREATE OR ALTER PROC sp_LoaiSP_Insert
(
    @TenLSP NVARCHAR(50),
    @MaNhom VARCHAR(10)
)
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra nhóm có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM NhomSP WHERE MaNhom = @MaNhom)
    BEGIN
        RAISERROR(N'Mã nhóm sản phẩm không tồn tại!', 16, 1);
        RETURN;
    END;

    -- Kiểm tra trùng tên loại sản phẩm trong cùng nhóm
    IF EXISTS (SELECT 1 FROM LoaiSP WHERE TenLSP = @TenLSP AND MaNhom = @MaNhom)
    BEGIN
        RAISERROR(N'Tên loại sản phẩm đã tồn tại trong nhóm này!', 16, 1);
        RETURN;
    END;

    DECLARE @Count INT = (SELECT COUNT(*) + 1 FROM LoaiSP);
    DECLARE @MaLoai VARCHAR(10) = 'LSP' + RIGHT('0000000' + CAST(@Count AS VARCHAR(7)), 7);

    INSERT INTO LoaiSP (MaLoai, TenLSP, MaNhom)
    VALUES (@MaLoai, @TenLSP, @MaNhom);
END;
GO

-- Update
CREATE OR ALTER PROC sp_LoaiSP_Update
(
    @MaLoai VARCHAR(10),
    @TenLSP NVARCHAR(50),
    @MaNhom VARCHAR(10)
)
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra mã loại có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM LoaiSP WHERE MaLoai = @MaLoai)
    BEGIN
        RAISERROR(N'Mã loại sản phẩm không tồn tại!', 16, 1);
        RETURN;
    END;

    -- Kiểm tra nhóm có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM NhomSP WHERE MaNhom = @MaNhom)
    BEGIN
        RAISERROR(N'Mã nhóm sản phẩm không tồn tại!', 16, 1);
        RETURN;
    END;

    -- Kiểm tra trùng tên trong cùng nhóm (trừ chính bản ghi đang sửa)
    IF EXISTS (SELECT 1 FROM LoaiSP WHERE TenLSP = @TenLSP AND MaNhom = @MaNhom AND MaLoai <> @MaLoai)
    BEGIN
        RAISERROR(N'Tên loại sản phẩm đã tồn tại trong nhóm này!', 16, 1);
        RETURN;
    END;

    UPDATE LoaiSP
    SET TenLSP = @TenLSP,
        MaNhom = @MaNhom
    WHERE MaLoai = @MaLoai;
END;
GO

CREATE OR ALTER PROC sp_LoaiSP_GetAll AS SELECT * FROM LoaiSP; 
GO
CREATE OR ALTER PROC sp_LoaiSP_GetByID(@MaLoai VARCHAR(10)) AS SELECT * FROM LoaiSP WHERE MaLoai=@MaLoai; 
GO
CREATE OR ALTER PROC sp_LoaiSP_Delete(@MaLoai VARCHAR(10)) AS DELETE FROM LoaiSP WHERE MaLoai=@MaLoai; 
GO

CREATE OR ALTER PROC sp_LoaiNhomSP_GetAll AS SELECT L.MaLoai, L.TenLSP, L.MaNhom, N.TenNhom FROM LoaiSP L JOIN NhomSP N ON L.MaNhom=N.MaNhom;
EXEC sp_LoaiNhomSP_GetAll