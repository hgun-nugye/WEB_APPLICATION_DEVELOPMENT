USE Huong_Nguyen_Thi_Thanh_6513124_DB_QLBH;
GO

-- Insert: nếu KH đã có (trùng SĐT hoặc Email) thì cập nhật lại, nếu chưa thì thêm mới
CREATE OR ALTER PROC sp_KhachHang_Insert
    @TenKH NVARCHAR(50),
    @DienThoaiKH VARCHAR(10),
    @EmailKH VARCHAR(255),
    @DiaChiKH NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @MaKH VARCHAR(10);

    SELECT TOP 1 @MaKH = MaKH
    FROM KhachHang
    WHERE DienThoaiKH = @DienThoaiKH OR EmailKH = @EmailKH;

    IF @MaKH IS NOT NULL
    BEGIN
        UPDATE KhachHang
        SET TenKH = @TenKH,
            DiaChiKH = @DiaChiKH
        WHERE MaKH = @MaKH;

        PRINT N'Khách hàng đã tồn tại — thông tin đã được cập nhật.';
    END
    ELSE
    BEGIN
        DECLARE @NextId INT = ISNULL(
            (SELECT MAX(CAST(SUBSTRING(MaKH, 3, LEN(MaKH)) AS INT)) + 1 FROM KhachHang), 1
        );
        SET @MaKH = 'KH' + RIGHT('00000000' + CAST(@NextId AS VARCHAR(8)), 8);

        INSERT INTO KhachHang(MaKH, TenKH, DienThoaiKH, EmailKH, DiaChiKH)
        VALUES (@MaKH, @TenKH, @DienThoaiKH, @EmailKH, @DiaChiKH);

        PRINT N'Thêm khách hàng mới thành công.';
    END
END;
GO

-- Update: nếu trùng thông tin với KH khác thì báo lỗi
CREATE OR ALTER PROC sp_KhachHang_Update
    @MaKH VARCHAR(10),
    @TenKH NVARCHAR(50),
    @DienThoaiKH VARCHAR(10),
    @EmailKH VARCHAR(255),
    @DiaChiKH NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1 FROM KhachHang
        WHERE MaKH <> @MaKH 
          AND (DienThoaiKH = @DienThoaiKH OR EmailKH = @EmailKH)
    )
    BEGIN
        RAISERROR(N'Thông tin khách hàng đã tồn tại, không thể cập nhật.', 16, 1);
        RETURN;
    END

    UPDATE KhachHang
    SET TenKH = @TenKH,
        DienThoaiKH = @DienThoaiKH,
        EmailKH = @EmailKH,
        DiaChiKH = @DiaChiKH
    WHERE MaKH = @MaKH;

    PRINT N'Cập nhật thông tin khách hàng thành công.';
END;
GO

-- Get all
CREATE OR ALTER PROC sp_KhachHang_GetAll AS SELECT * FROM KhachHang;
GO

-- Get by ID
CREATE OR ALTER PROC sp_KhachHang_GetById @MaKH VARCHAR(10)
AS SELECT * FROM KhachHang WHERE MaKH = @MaKH;
GO

-- Delete
CREATE OR ALTER PROC sp_KhachHang_Delete @MaKH VARCHAR(10)
AS DELETE FROM KhachHang WHERE MaKH = @MaKH;
GO
