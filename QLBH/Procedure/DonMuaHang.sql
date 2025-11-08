CREATE TYPE dbo.CTMH_List AS TABLE
(
    MaSP VARCHAR(10),
    SLM INT,
    DGM MONEY
);
GO

CREATE OR ALTER PROC sp_DonMuaHang_Insert
(
    @NgayMH DATE,
    @MaNCC VARCHAR(10),
    @ChiTiet CTMH_List READONLY 
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @MaDMH CHAR(11);
    DECLARE @Count INT;
    DECLARE @Prefix VARCHAR(8);

    -- Tạo mã MYYMMDD#### (vd: M2510190001)
    SET @Prefix = 'M' +
                  RIGHT(CAST(YEAR(@NgayMH) AS VARCHAR(4)), 2) +
                  RIGHT('0' + CAST(MONTH(@NgayMH) AS VARCHAR(2)), 2) +
                  RIGHT('0' + CAST(DAY(@NgayMH) AS VARCHAR(2)), 2);

    SELECT @Count = COUNT(*) + 1
    FROM DonMuaHang
    WHERE CONVERT(DATE, NgayMH) = @NgayMH;

    SET @MaDMH = @Prefix + RIGHT('0000' + CAST(@Count AS VARCHAR(4)), 4);

    -- Thêm đơn mua hàng
    INSERT INTO DonMuaHang(MaDMH, NgayMH, MaNCC)
    VALUES (@MaDMH, @NgayMH, @MaNCC);

    -- Thêm chi tiết mua hàng từ danh sách truyền vào
    INSERT INTO CTMH(MaDMH, MaSP, SLM, DGM)
    SELECT @MaDMH, MaSP, SLM, DGM
    FROM @ChiTiet;

END;
GO

GO

CREATE OR ALTER PROC sp_DonMuaHang_Update
    @MaDMH CHAR(11),
    @NgayMH DATE,
    @MaNCC VARCHAR(10)
AS
BEGIN
    UPDATE DonMuaHang
    SET NgayMH = @NgayMH,
        MaNCC = @MaNCC
    WHERE MaDMH = @MaDMH;
END
GO

CREATE OR ALTER PROC sp_DonMuaHang_GetAll AS SELECT * FROM DonMuaHang;
GO
CREATE OR ALTER PROC sp_DonMuaHang_GetAll_Detail AS SELECT D.MaDMH, D.NgayMH, D.MaNCC, N.TenNCC AS TenNCC FROM DonMuaHang D join NhaCC N ON N.MaNCC=D.MaNCC;
GO
CREATE OR ALTER PROC sp_DonMuaHang_GetById @MaDMH CHAR(11) AS SELECT * FROM DonMuaHang WHERE MaDMH = @MaDMH;
GO
CREATE OR ALTER PROC sp_DonMuaHang_GetById_Detail @MaDMH CHAR(11) 
	AS SELECT D.MaDMH, D.NgayMH, D.MaNCC, N.TenNCC AS TenNCC
	FROM DonMuaHang D join NhaCC N ON N.MaNCC=D.MaNCC 
	WHERE MaDMH = @MaDMH;
GO
CREATE OR ALTER PROC sp_DonMuaHang_Delete @MaDMH CHAR(11) AS DELETE FROM DonMuaHang WHERE MaDMH = @MaDMH;
GO
