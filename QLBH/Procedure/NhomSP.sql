USE Huong_Nguyen_Thi_Thanh_6513124_DB_QLBH;
GO
-- Insert
CREATE OR ALTER PROC sp_NhomSP_Insert
(
    @TenNhom NVARCHAR(100)
)
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra trùng tên nhóm sản phẩm
    IF EXISTS (SELECT 1 FROM NhomSP WHERE TenNhom = @TenNhom)
    BEGIN
        RAISERROR(N'Tên nhóm sản phẩm đã tồn tại!', 16, 1) WITH NOWAIT;
        RETURN;
    END;

    -- Tạo mã nhóm tự động
    DECLARE @Count INT = (SELECT COUNT(*) + 1 FROM NhomSP);
    DECLARE @MaNhom VARCHAR(10) = 'NSP' + RIGHT('0000000' + CAST(@Count AS VARCHAR(7)), 7);

    -- Thêm mới
    INSERT INTO NhomSP (MaNhom, TenNhom)
    VALUES (@MaNhom, @TenNhom);
END
GO


-- Update
CREATE OR ALTER PROC sp_NhomSP_Update
(
    @MaNhom VARCHAR(10),
    @TenNhom NVARCHAR(100)
)
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra tồn tại mã nhóm
    IF NOT EXISTS (SELECT 1 FROM NhomSP WHERE MaNhom = @MaNhom)
    BEGIN
        RAISERROR(N'Mã nhóm sản phẩm không tồn tại!', 16, 1) WITH NOWAIT;
        RETURN;
    END

    -- Kiểm tra trùng tên (trừ chính nó)
    IF EXISTS (SELECT 1 FROM NhomSP WHERE TenNhom = @TenNhom AND MaNhom <> @MaNhom)
    BEGIN
        RAISERROR(N'Tên nhóm sản phẩm đã tồn tại!', 16, 1);
        RETURN;
    END

    -- Cập nhật
    UPDATE NhomSP 
    SET TenNhom = @TenNhom
    WHERE MaNhom = @MaNhom;
END
GO

CREATE OR ALTER PROC sp_NhomSP_GetAll AS SELECT * FROM NhomSP;
GO
CREATE OR ALTER PROC sp_NhomSP_GetByID(@MaNhom VARCHAR(10)) AS SELECT * FROM NhomSP WHERE MaNhom=@MaNhom; 
GO
CREATE OR ALTER PROC sp_NhomSP_Delete(@MaNhom VARCHAR(10)) AS DELETE FROM NhomSP WHERE MaNhom=@MaNhom; 
GO
