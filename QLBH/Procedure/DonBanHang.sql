﻿CREATE TYPE dbo.CTBH_List AS TABLE
(
    MaSP VARCHAR(10),
    SLB INT,
    DGB MONEY
);
GO

CREATE OR ALTER PROC sp_DonBanHang_Insert
(
    @NgayBH DATE,
    @MaKH VARCHAR(10),
    @ChiTiet CTBH_List READONLY
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @MaDBH CHAR(11);
    DECLARE @Count INT;
    DECLARE @Prefix VARCHAR(8);

    -- Tạo mã dạng: BYYMMDD####
    SET @Prefix = 'B' +
                  RIGHT(CAST(YEAR(@NgayBH) AS VARCHAR(4)), 2) +
                  RIGHT('0' + CAST(MONTH(@NgayBH) AS VARCHAR(2)), 2) +
                  RIGHT('0' + CAST(DAY(@NgayBH) AS VARCHAR(2)), 2);

    SELECT @Count = COUNT(*) + 1
    FROM DonBanHang
    WHERE CONVERT(DATE, NgayBH) = @NgayBH;

    SET @MaDBH = @Prefix + RIGHT('0000' + CAST(@Count AS VARCHAR(4)), 4);

    -- Thêm đơn bán hàng
    INSERT INTO DonBanHang(MaDBH, NgayBH, MaKH)
    VALUES (@MaDBH, @NgayBH, @MaKH);

    -- Thêm chi tiết bán hàng
    INSERT INTO CTBH(MaDBH, MaSP, SLB, DGB)
    SELECT @MaDBH, MaSP, SLB, DGB
    FROM @ChiTiet;

    -- Trả về mã đơn mới tạo
    SELECT @MaDBH AS MaDonBanHangMoi;
END;
GO

GO

CREATE OR ALTER PROC sp_DonBanHang_Update
    @MaDBH CHAR(11),
    @NgayBH DATE,
    @MaKH VARCHAR(10)
AS
BEGIN
    UPDATE DonBanHang
    SET NgayBH = @NgayBH,
        MaKH = @MaKH
    WHERE MaDBH = @MaDBH;
END;
GO

CREATE OR ALTER PROC sp_DonBanHang_GetAll AS SELECT * FROM DonBanHang;
GO
CREATE OR ALTER PROC sp_DonBanHang_GetById @MaDBH CHAR(11) AS SELECT * FROM DonBanHang WHERE MaDBH = @MaDBH; 
GO
CREATE OR ALTER PROC sp_DonBanHang_Delete @MaDBH CHAR(11) AS DELETE FROM DonBanHang WHERE MaDBH = @MaDBH; 
GO
