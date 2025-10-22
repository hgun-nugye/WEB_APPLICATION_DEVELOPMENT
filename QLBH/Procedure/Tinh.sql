USE Huong_Nguyen_Thi_Thanh_6513124_DB_QLBH;
GO
-- INSERT
CREATE OR ALTER PROC sp_Tinh_Insert
(
    @TenTinh NVARCHAR(90)
)
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra trùng tên tỉnh
    IF EXISTS (SELECT 1 FROM Tinh WHERE TenTinh = @TenTinh)
    BEGIN
        RAISERROR(N'Tên tỉnh đã tồn tại.', 16, 1);
        RETURN;
    END;

    -- Sinh mã tự động tăng (TinyInt)
    DECLARE @MaTinh TINYINT;
    SELECT @MaTinh = ISNULL(MAX(MaTinh), 0) + 1 FROM Tinh;

    -- Thêm dữ liệu
    INSERT INTO Tinh (MaTinh, TenTinh)
    VALUES (@MaTinh, @TenTinh);
END;
GO

-- UPDATE
CREATE OR ALTER PROC sp_Tinh_Update
(@MaTinh TINYINT, @TenTinh NVARCHAR(90))
AS
BEGIN
	 SET NOCOUNT ON;

    -- Kiểm tra trùng tên tỉnh 
    IF EXISTS (SELECT 1 FROM Tinh WHERE TenTinh = @TenTinh AND MaTinh <> @MaTinh)
    BEGIN
        RAISERROR(N'Tên tỉnh đã tồn tại.', 16, 1);
        RETURN;
    END;

    UPDATE Tinh SET TenTinh = @TenTinh WHERE MaTinh = @MaTinh;
END
GO

-- GET ALL
CREATE OR ALTER PROC sp_Tinh_GetAll
AS
BEGIN
    SELECT * FROM Tinh;
END
GO

-- GET BY ID
CREATE OR ALTER PROC sp_Tinh_GetByID
(@MaTinh TINYINT)
AS
BEGIN
    SELECT * FROM Tinh WHERE MaTinh = @MaTinh;
END
GO

-- DELETE
CREATE OR ALTER PROC sp_Tinh_Delete
(@MaTinh TINYINT)
AS
BEGIN
    DELETE FROM Tinh WHERE MaTinh = @MaTinh;
END
GO
