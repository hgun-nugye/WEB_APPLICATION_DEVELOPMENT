CREATE OR ALTER PROC sp_GianHang_Insert
    @TenGH NVARCHAR(100),
    @MoTaGH NVARCHAR(255),
    @DienThoaiGH VARCHAR(15),
    @EmailGH VARCHAR(100),
    @DiaChiGH NVARCHAR(200),
    @MaXa TINYINT
AS
BEGIN
    DECLARE @Count INT = (SELECT COUNT(*) + 1 FROM GianHang);
    DECLARE @MaGH VARCHAR(10) = 'GH' + RIGHT('000000000' + CAST(@Count AS VARCHAR(10)), 8 ); 

    INSERT INTO GianHang(MaGH, TenGH, MoTaGH, NgayTao, DienThoaiGH, EmailGH, DiaChiGH, MaXa)
    VALUES(@MaGH, @TenGH, @MoTaGH, GETDATE(), @DienThoaiGH, @EmailGH, @DiaChiGH, @MaXa);
END;
GO

CREATE OR ALTER PROC sp_GianHang_Update
    @MaGH VARCHAR(10),
    @TenGH NVARCHAR(100),
    @MoTaGH NVARCHAR(255),
    @DienThoaiGH VARCHAR(15),
    @EmailGH VARCHAR(100),
    @DiaChiGH NVARCHAR(200),
    @MaXa TINYINT
AS
BEGIN
    UPDATE GianHang
    SET 
        TenGH = @TenGH,
        MoTaGH = @MoTaGH,
        DienThoaiGH = @DienThoaiGH,
        EmailGH = @EmailGH,
        DiaChiGH = @DiaChiGH,
        MaXa = @MaXa
    WHERE MaGH = @MaGH;
END;
GO

CREATE OR ALTER PROC sp_GianHang_GetAll AS SELECT * FROM GianHang; 
GO
CREATE OR ALTER PROC sp_GianHang_GetByID(@MaGH VARCHAR(10)) AS SELECT * FROM GianHang WHERE MaGH=@MaGH;
GO
CREATE OR ALTER PROC sp_GianHang_Delete(@MaGH VARCHAR(10)) AS DELETE FROM GianHang WHERE MaGH=@MaGH;
GO
