using Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Models;
using Microsoft.EntityFrameworkCore;

namespace Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Services
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		// Khai báo DbSet cho các bảng
		public DbSet<NhaCC> NhaCC { get; set; } = null!;
		public DbSet<KhachHang> KhachHang { get; set; } = null!;
		public DbSet<Tinh> Tinh { get; set; } = null!;
		public DbSet<Xa> Xa { get; set; } = null!;
		public DbSet<NhomSP> NhomSP { get; set; } = null!;
		public DbSet<LoaiSP> LoaiSP { get; set; } = null!;
		public DbSet<GianHang> GianHang { get; set; } = null!;
		public DbSet<SanPham> SanPham { get; set; } = null!; 

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// ======== Thiết lập khóa chính ========
			modelBuilder.Entity<NhaCC>().HasKey(n => n.MaNCC);
			modelBuilder.Entity<KhachHang>().HasKey(kh => kh.MaKH);
			modelBuilder.Entity<Tinh>().HasKey(t => t.MaTinh);
			modelBuilder.Entity<Xa>().HasKey(x => x.MaXa);
			modelBuilder.Entity<NhomSP>().HasKey(nsp => nsp.MaNhom);
			modelBuilder.Entity<LoaiSP>().HasKey(lsp => lsp.MaLoai);
			modelBuilder.Entity<GianHang>().HasKey(g => g.MaGH);
			modelBuilder.Entity<SanPham>().HasKey(sp => sp.MaSP); 

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
				.WithMany(lsp =>lsp.SanPhams)
				.HasForeignKey(sp => sp.MaLoai)
				.OnDelete(DeleteBehavior.Cascade);

			// ======== Khóa ngoại: SanPham → GianHang ========
			modelBuilder.Entity<SanPham>()
				.HasOne(sp => sp.GianHang)
				.WithMany(g => g.DsSanPham)
				.HasForeignKey(sp => sp.MaGH)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
