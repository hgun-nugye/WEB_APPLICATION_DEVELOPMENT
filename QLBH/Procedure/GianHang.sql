USE Huong_Nguyen_Thi_Thanh_6513124_DB_QLBH;
GO
-- Insert
CREATE OR ALTER PROC sp_GianHang_Insert
    @TenGH NVARCHAR(100),
    @MoTaGH NVARCHAR(255),
    @DienThoaiGH VARCHAR(15),
    @EmailGH NVARCHAR(100),
    @DiaChiGH NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra trùng tên gian hàng
    IF EXISTS (SELECT 1 FROM GianHang WHERE TenGH = @TenGH)
    BEGIN
        RAISERROR(N'Tên gian hàng đã tồn tại!', 16, 1);
        RETURN;
    END;

    -- Kiểm tra trùng số điện thoại
    IF EXISTS (SELECT 1 FROM GianHang WHERE DienThoaiGH = @DienThoaiGH)
    BEGIN
        RAISERROR(N'Số điện thoại gian hàng đã tồn tại!', 16, 1);
        RETURN;
    END;

    DECLARE @Count INT = (SELECT COUNT(*) + 1 FROM GianHang);
    DECLARE @MaGH VARCHAR(10) = 'GH' + RIGHT('00000000' + CAST(@Count AS VARCHAR(8)), 8);

    INSERT INTO GianHang (MaGH, TenGH, MoTaGH, NgayTao, DienThoaiGH, EmailGH, DiaChiGH)
    VALUES (@MaGH, @TenGH, @MoTaGH, GETDATE(), @DienThoaiGH, @EmailGH, @DiaChiGH);
END;
GO

-- Update
CREATE OR ALTER PROC sp_GianHang_Update
    @MaGH VARCHAR(10),
    @TenGH NVARCHAR(100),
    @MoTaGH NVARCHAR(255),
    @DienThoaiGH VARCHAR(15),
    @EmailGH NVARCHAR(100),
    @DiaChiGH NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM GianHang WHERE MaGH = @MaGH)
    BEGIN
        RAISERROR(N'Mã gian hàng không tồn tại!', 16, 1);
        RETURN;
    END;

    IF EXISTS (SELECT 1 FROM GianHang WHERE TenGH = @TenGH AND MaGH <> @MaGH)
    BEGIN
        RAISERROR(N'Tên gian hàng đã tồn tại!', 16, 1);
        RETURN;
    END;

    IF EXISTS (SELECT 1 FROM GianHang WHERE DienThoaiGH = @DienThoaiGH AND MaGH <> @MaGH)
    BEGIN
        RAISERROR(N'Số điện thoại gian hàng đã tồn tại!', 16, 1);
        RETURN;
    END;

    UPDATE GianHang
    SET 
        TenGH = @TenGH,
        MoTaGH = @MoTaGH,
        DienThoaiGH = @DienThoaiGH,
        EmailGH = @EmailGH,
        DiaChiGH = @DiaChiGH
    WHERE MaGH = @MaGH;
END;
GO

-- Get all
CREATE OR ALTER PROC sp_GianHang_GetAll
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM GianHang
END;
GO

-- Get by ID
CREATE OR ALTER PROC sp_GianHang_GetByID
    @MaGH VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM GianHang WHERE MaGH = @MaGH;
END;
GO

-- Delete
CREATE OR ALTER PROC sp_GianHang_Delete
    @MaGH VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM GianHang WHERE MaGH = @MaGH)
    BEGIN
        RAISERROR(N'Mã gian hàng không tồn tại!', 16, 1);
        RETURN;
    END;

    DELETE FROM GianHang WHERE MaGH = @MaGH;
END;
GO
exec sp_GianHang_GetAll