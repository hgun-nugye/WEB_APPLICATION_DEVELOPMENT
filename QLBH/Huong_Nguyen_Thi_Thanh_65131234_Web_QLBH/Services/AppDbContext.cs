using Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Models;
using Microsoft.EntityFrameworkCore;

namespace Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Services
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<NhaCC> NhaCCs { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<NhaCC>().ToTable("NhaCC");
		}
	}
}
