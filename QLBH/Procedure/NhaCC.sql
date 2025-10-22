USE Huong_Nguyen_Thi_Thanh_6513124_DB_QLBH;
GO

-- Tạo/ghi đè thủ tục
CREATE OR ALTER PROC dbo.sp_NhaCC_Insert
(
    @TenNCC NVARCHAR(100),
    @DienThoaiNCC VARCHAR(15),
    @EmailNCC VARCHAR(100),
    @DiaChiNCC NVARCHAR(255)
)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1 FROM dbo.NhaCC 
        WHERE (TenNCC = @TenNCC AND DienThoaiNCC = @DienThoaiNCC)
           OR (EmailNCC = @EmailNCC)
    )
    BEGIN
        RAISERROR(N'Nhà cung cấp đã tồn tại (trùng tên, điện thoại hoặc email).', 16, 1);
        RETURN;
    END;

    DECLARE @NextID INT = (
        SELECT ISNULL(MAX(CAST(SUBSTRING(MaNCC, 4, 7) AS INT)), 0) + 1
        FROM dbo.NhaCC
    );
    DECLARE @MaNCC VARCHAR(10) = 'NCC' + RIGHT('0000000' + CAST(@NextID AS VARCHAR(7)), 7);

    INSERT INTO dbo.NhaCC (MaNCC, TenNCC, DienThoaiNCC, EmailNCC, DiaChiNCC)
    VALUES (@MaNCC, @TenNCC, @DienThoaiNCC, @EmailNCC, @DiaChiNCC);

    PRINT 'Đã thêm ' + @MaNCC;
END;
GO

-- Update
CREATE OR ALTER PROC sp_NhaCC_Update
(
    @MaNCC VARCHAR(10),
    @TenNCC NVARCHAR(100),
    @DienThoaiNCC VARCHAR(15),
    @EmailNCC VARCHAR(100),
    @DiaChiNCC NVARCHAR(255)
)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM dbo.NhaCC WHERE MaNCC <> @MaNCC AND ((TenNCC = @TenNCC AND DienThoaiNCC = @DienThoaiNCC)
           OR (EmailNCC = @EmailNCC)))
    BEGIN
        RAISERROR(N'Đã tồn tại Nhà cung cấp!', 16, 1);
        RETURN;
    END;

    UPDATE dbo.NhaCC
    SET 
        TenNCC = @TenNCC,
        DienThoaiNCC = @DienThoaiNCC,
        EmailNCC = @EmailNCC,
        DiaChiNCC = @DiaChiNCC
    WHERE MaNCC = @MaNCC;
END;
GO

-- Delete
CREATE OR ALTER PROC sp_NhaCC_Delete
(
    @MaNCC VARCHAR(10)
)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM dbo.NhaCC WHERE MaNCC = @MaNCC)
    BEGIN
        RAISERROR(N'Không tồn tại nhà cung cấp có mã này.', 16, 1);
        RETURN;
    END;

    DELETE FROM dbo.NhaCC WHERE MaNCC = @MaNCC;
END;
GO

-- Get all
CREATE OR ALTER PROC sp_NhaCC_GetAll
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        MaNCC,
        TenNCC,
        DienThoaiNCC,
        EmailNCC,
        DiaChiNCC
    FROM dbo.NhaCC
    ORDER BY MaNCC;
END;
GO

-- Get by ID
CREATE OR ALTER PROC sp_NhaCC_GetByID
(
    @MaNCC VARCHAR(10)
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        MaNCC,
        TenNCC,
        DienThoaiNCC,
        EmailNCC,
        DiaChiNCC
    FROM dbo.NhaCC
    WHERE MaNCC = @MaNCC;
END;
GO
