using Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Models;
using Microsoft.EntityFrameworkCore;

namespace Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Services
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		// ======== Khai báo DbSet cho các bảng ========
		public DbSet<NhaCC> NhaCC { get; set; } = null!;
		public DbSet<KhachHang> KhachHang { get; set; } = null!;
		public DbSet<Tinh> Tinh { get; set; } = null!;
		public DbSet<Xa> Xa { get; set; } = null!;
		public DbSet<NhomSP> NhomSP { get; set; } = null!;
		public DbSet<LoaiSP> LoaiSP { get; set; } = null!;
		public DbSet<GianHang> GianHang { get; set; } = null!;
		public DbSet<SanPham> SanPham { get; set; } = null!;

		// Bổ sung thêm các bảng giao dịch
		public DbSet<DonMuaHang> DonMuaHang { get; set; } = null!;
		public DbSet<DonMuaHangDetail> DonMuaHangDetail { get; set; } = null!;
		public DbSet<DonBanHang> DonBanHang { get; set; } = null!;
		public DbSet<DonBanHangDetail> DonBanHangDetail { get; set; } = null!;

		public DbSet<CTMH> CTMH { get; set; } = null!;
		public DbSet<CTBH> CTBH { get; set; } = null!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// ======== Khóa chính ========
			modelBuilder.Entity<NhaCC>().HasKey(n => n.MaNCC);
			modelBuilder.Entity<KhachHang>().HasKey(kh => kh.MaKH);
			modelBuilder.Entity<Tinh>().HasKey(t => t.MaTinh);
			modelBuilder.Entity<Xa>().HasKey(x => x.MaXa);
			modelBuilder.Entity<NhomSP>().HasKey(nsp => nsp.MaNhom);
			modelBuilder.Entity<LoaiSP>().HasKey(lsp => lsp.MaLoai);
			modelBuilder.Entity<GianHang>().HasKey(g => g.MaGH);
			modelBuilder.Entity<SanPham>().HasKey(sp => sp.MaSP);

			// 🆕 Khóa chính mới
			modelBuilder.Entity<DonMuaHang>().HasKey(d => d.MaDMH);
			modelBuilder.Entity<DonBanHang>().HasKey(d => d.MaDBH);

			// 🆕 Khóa chính kép cho chi tiết
			modelBuilder.Entity<CTMH>().HasKey(ct => new { ct.MaDMH, ct.MaSP });
			modelBuilder.Entity<CTBH>().HasKey(ct => new { ct.MaDBH, ct.MaSP });

			// ======== Khóa ngoại: Xa → Tinh ========
			modelBuilder.Entity<Xa>()
				.HasOne(x => x.Tinh)
				.WithMany(t => t.DsXa)
				.HasForeignKey(x => x.MaTinh)
				.OnDelete(DeleteBehavior.Cascade);

			// ======== Khóa ngoại: LoaiSP → NhomSP ========
			modelBuilder.Entity<LoaiSP>()
				.HasOne(lsp => lsp.NhomSP)
				.WithMany(nsp => nsp.LoaiSPs)
				.HasForeignKey(lsp => lsp.MaNhom)
				.OnDelete(DeleteBehavior.Cascade);

			// ======== Khóa ngoại: SanPham → LoaiSP ========
			modelBuilder.Entity<SanPham>()
				.HasOne(sp => sp.LoaiSP)
				.WithMany(lsp => lsp.SanPhams)
				.HasForeignKey(sp => sp.MaLoai)
				.OnDelete(DeleteBehavior.Cascade);

			// ======== Khóa ngoại: SanPham → GianHang ========
			modelBuilder.Entity<SanPham>()
				.HasOne(sp => sp.GianHang)
				.WithMany(g => g.DsSanPham)
				.HasForeignKey(sp => sp.MaGH)
				.OnDelete(DeleteBehavior.Cascade);

			// 🆕 ======== Khóa ngoại: DonMuaHang → NhaCC ========
			modelBuilder.Entity<DonMuaHang>()
				.HasOne(d => d.NhaCC)
				.WithMany(n => n.DonMuaHangs)
				.HasForeignKey(d => d.MaNCC)
				.OnDelete(DeleteBehavior.Cascade);

			// 🆕 ======== Khóa ngoại: DonBanHang → KhachHang ========
			modelBuilder.Entity<DonBanHang>()
				.HasOne(d => d.KhachHang)
				.WithMany(kh => kh.DonBanHangs)
				.HasForeignKey(d => d.MaKH)
				.OnDelete(DeleteBehavior.Cascade);

			// 🆕 ======== Khóa ngoại: CTMH → DonMuaHang và SanPham ========
			modelBuilder.Entity<CTMH>()
				.HasOne(ct => ct.DonMuaHang)
				.WithMany(d => d.CTMHs)
				.HasForeignKey(ct => ct.MaDMH)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<CTMH>()
				.HasOne(ct => ct.SanPham)
				.WithMany(sp => sp.CTMHs)
				.HasForeignKey(ct => ct.MaSP)
				.OnDelete(DeleteBehavior.Cascade);

			// 🆕 ======== Khóa ngoại: CTBH → DonBanHang và SanPham ========
			modelBuilder.Entity<CTBH>()
				.HasOne(ct => ct.DonBanHang)
				.WithMany(d => d.CTBHs)
				.HasForeignKey(ct => ct.MaDBH)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<CTBH>()
				.HasOne(ct => ct.SanPham)
				.WithMany(sp => sp.CTBHs)
				.HasForeignKey(ct => ct.MaSP)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
