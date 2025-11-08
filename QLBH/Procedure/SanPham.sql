USE Huong_Nguyen_Thi_Thanh_6513124_DB_QLBH;
GO
-- ====== INSERT ======
CREATE OR ALTER PROC sp_SanPham_Insert
(
    @TenSP NVARCHAR(50),
    @DonGia MONEY,
    @MoTaSP NVARCHAR(MAX),
    @AnhMH NVARCHAR(50),
    @MaLoai VARCHAR(10),
    @MaGH VARCHAR(10)
)
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra trùng tên sản phẩm trong cùng gian hàng
    IF EXISTS (SELECT 1 FROM SanPham WHERE TenSP = @TenSP AND MaGH = @MaGH)
    BEGIN
        RAISERROR(N'Sản phẩm này đã tồn tại trong gian hàng!', 16, 1);
        RETURN;
    END;

    DECLARE @Count INT = (SELECT COUNT(*) + 1 FROM SanPham);
    DECLARE @MaSP VARCHAR(10) = 'SP' + RIGHT('00000000' + CAST(@Count AS VARCHAR(8)), 8);

    INSERT INTO SanPham(MaSP, TenSP, DonGia, MoTaSP, AnhMH, MaLoai, MaGH)
    VALUES(@MaSP, @TenSP, @DonGia, @MoTaSP, @AnhMH, @MaLoai, @MaGH);
END;
GO

-- ====== UPDATE ======
CREATE OR ALTER PROC sp_SanPham_Update
(
    @MaSP VARCHAR(10),
    @TenSP NVARCHAR(50),
    @DonGia MONEY,
    @MoTaSP NVARCHAR(MAX),
    @AnhMH NVARCHAR(50),
    @MaLoai VARCHAR(10),
    @MaGH VARCHAR(10)
)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM SanPham WHERE MaSP = @MaSP)
    BEGIN
        RAISERROR(N'Mã sản phẩm không tồn tại!', 16, 1);
        RETURN;
    END;

    -- Kiểm tra trùng tên trong cùng gian hàng (ngoại trừ chính nó)
    IF EXISTS (SELECT 1 FROM SanPham WHERE TenSP = @TenSP AND MaGH = @MaGH AND MaSP <> @MaSP)
    BEGIN
        RAISERROR(N'Sản phẩm này đã tồn tại trong gian hàng!', 16, 1);
        RETURN;
    END;

    UPDATE SanPham
    SET TenSP = @TenSP,
        DonGia = @DonGia,
        MoTaSP = @MoTaSP,
        AnhMH = @AnhMH,
        MaLoai = @MaLoai,
        MaGH = @MaGH
    WHERE MaSP = @MaSP;
END;
GO

-- ====== GET ALL ======
CREATE OR ALTER PROC sp_SanPham_GetAll
AS
BEGIN
    SELECT * FROM SanPham;
END
GO

-- ====== GET BY ID ======
CREATE OR ALTER PROC sp_SanPham_GetByID(@MaSP VARCHAR(10))
AS
BEGIN
    SELECT * FROM SanPham WHERE MaSP = @MaSP;
END
GO

-- ====== DELETE ======
CREATE OR ALTER PROC sp_SanPham_Delete(@MaSP VARCHAR(10))
AS
BEGIN
    DELETE FROM SanPham WHERE MaSP = @MaSP;
END
GO

-- ====== SHOW NAME DETAIL ======
CREATE OR ALTER PROC sp_SanPham_GetAll_Detail
AS
BEGIN
    SELECT 
        sp.MaSP,
        sp.TenSP,
        sp.DonGia,
        sp.MoTaSP,
        sp.AnhMH,
        sp.MaLoai,
        lsp.TenLSP AS TenLoai,
        sp.MaGH,
        gh.TenGH AS TenGH
    FROM SanPham sp
    JOIN LoaiSP lsp ON sp.MaLoai = lsp.MaLoai
    JOIN GianHang gh ON sp.MaGH = gh.MaGH;
END;
GO

CREATE OR ALTER PROC sp_SanPham_GetByID_Detail
(
    @MaSP VARCHAR(10)
)
AS
BEGIN
    SELECT 
        sp.MaSP,
        sp.TenSP,
        sp.DonGia,
        sp.MoTaSP,
        sp.AnhMH,
        sp.MaLoai,
        lsp.TenLSP AS TenLoai,
        sp.MaGH,
        gh.TenGH AS TenGH
    FROM SanPham sp
    JOIN LoaiSP lsp ON sp.MaLoai = lsp.MaLoai
    JOIN GianHang gh ON sp.MaGH = gh.MaGH
    WHERE sp.MaSP = @MaSP;
END;
GO

