USE Huong_Nguyen_Thi_Thanh_6513124_DB_QLBH;
GO
CREATE OR ALTER PROC sp_Xa_Insert
(
    @TenXa NVARCHAR(90),
	@MaTinh SMALLINT
)
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra trùng tên xã
    IF EXISTS (SELECT 1 FROM Xa WHERE TenXa = @TenXa AND MaTinh=@MaTinh)
    BEGIN
        RAISERROR(N'Xã đã tồn tại.', 16, 1);
        RETURN;
    END;

    -- Sinh mã tự động tăng (TinyInt)
    DECLARE @MaXa TINYINT;
    SELECT @MaXa = ISNULL(MAX(MaXa), 0) + 1 FROM Xa;

    -- Thêm dữ liệu
    INSERT INTO Xa (MaXa, TenXa, MaTinh)
    VALUES (@MaXa, @TenXa, @MaTinh);
END;
GO


CREATE OR ALTER PROC sp_Xa_GetAll AS SELECT * FROM Xa; 
GO
CREATE OR ALTER PROC sp_Xa_GetByID(@MaXa TINYINT) AS SELECT * FROM Xa WHERE MaXa=@MaXa; 
GO
CREATE OR ALTER PROC sp_Xa_Delete(@MaXa TINYINT) AS DELETE FROM Xa WHERE MaXa=@MaXa; 
GO
CREATE OR ALTER PROC sp_Xa_Update
(
    @MaXa TINYINT,
    @TenXa NVARCHAR(90),
    @MaTinh SMALLINT
)
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra trùng tên xã (ngoại trừ chính xã đang sửa)
    IF EXISTS (SELECT 1 FROM Xa WHERE TenXa = @TenXa AND MaXa <> @MaXa)
    BEGIN
        RAISERROR(N'Tên xã đã tồn tại.', 16, 1);
        RETURN;
    END;

    -- Cập nhật thông tin (không sửa MaXa)
    UPDATE Xa
    SET TenXa = @TenXa,
        MaTinh = @MaTinh
    WHERE MaXa = @MaXa;
END;
GO




