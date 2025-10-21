-- Insert
CREATE OR ALTER PROC sp_NhaCC_Insert
(
    @TenNCC NVARCHAR(100),
    @DienThoaiNCC VARCHAR(15),
    @EmailNCC VARCHAR(100),
    @DiaChiNCC NVARCHAR(255)
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Count INT;
    DECLARE @MaNCC VARCHAR(10);

    SELECT @Count = COUNT(*) + 1 FROM NhaCC;
    SET @MaNCC = 'NCC' + RIGHT('0000000' + CAST(@Count AS VARCHAR(7)), 7);

    INSERT INTO NhaCC (MaNCC, TenNCC, DienThoaiNCC, EmailNCC, DiaChiNCC)
    VALUES (@MaNCC, @TenNCC, @DienThoaiNCC, @EmailNCC, @DiaChiNCC);
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

    IF NOT EXISTS (SELECT 1 FROM NhaCC WHERE MaNCC = @MaNCC)
    BEGIN
        RAISERROR(N'Không tồn tại nhà cung cấp có mã này.', 16, 1);
        RETURN;
    END

    UPDATE NhaCC
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

    IF NOT EXISTS (SELECT 1 FROM NhaCC WHERE MaNCC = @MaNCC)
    BEGIN
        RAISERROR(N'Không tồn tại nhà cung cấp có mã này.', 16, 1);
        RETURN;
    END

    DELETE FROM NhaCC WHERE MaNCC = @MaNCC;
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
    FROM NhaCC
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
    FROM NhaCC
    WHERE MaNCC = @MaNCC;
END;
GO


