CREATE OR ALTER PROC sp_KhachHang_Insert
    @TenKH NVARCHAR(50),
    @DienThoaiKH VARCHAR(10),
    @EmailKH VARCHAR(255),
    @DiaChiKH NVARCHAR(255)
AS
BEGIN
    DECLARE @MaKH VARCHAR(10) = 'KH' + RIGHT('000' + CAST((SELECT COUNT(*) + 1 FROM KhachHang) AS VARCHAR(3)), 3);
    INSERT INTO KhachHang VALUES (@MaKH, @TenKH, @DienThoaiKH, @EmailKH, @DiaChiKH);
END
GO

CREATE OR ALTER PROC sp_KhachHang_Update
    @MaKH VARCHAR(10),
    @TenKH NVARCHAR(50),
    @DienThoaiKH VARCHAR(10),
    @EmailKH VARCHAR(255),
    @DiaChiKH NVARCHAR(255)
AS
BEGIN
    UPDATE KhachHang
    SET TenKH = @TenKH,
        DienThoaiKH = @DienThoaiKH,
        EmailKH = @EmailKH,
        DiaChiKH = @DiaChiKH
    WHERE MaKH = @MaKH;
END
GO

CREATE OR ALTER PROC sp_KhachHang_GetAll AS SELECT * FROM KhachHang; 
GO
CREATE OR ALTER PROC sp_KhachHang_GetById @MaKH VARCHAR(10) AS SELECT * FROM KhachHang WHERE MaKH = @MaKH;
GO
CREATE OR ALTER PROC sp_KhachHang_Delete @MaKH VARCHAR(10) AS DELETE FROM KhachHang WHERE MaKH = @MaKH;
GO

