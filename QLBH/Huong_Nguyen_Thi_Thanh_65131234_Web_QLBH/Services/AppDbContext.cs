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
		public DbSet<Tinh> Tinh { get; set; } = null!;
		public DbSet<Xa> Xa { get; set; } = null!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Thiết lập khóa chính 
			modelBuilder.Entity<NhaCC>().HasKey(n => n.MaNCC);
			modelBuilder.Entity<Tinh>().HasKey(t => t.MaTinh);
			modelBuilder.Entity<Xa>().HasKey(x => x.MaXa);

			// Khóa ngoại: Xa → Tinh
			modelBuilder.Entity<Xa>()
				.HasOne<Tinh>()
				.WithMany()
				.HasForeignKey(x => x.MaTinh)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
