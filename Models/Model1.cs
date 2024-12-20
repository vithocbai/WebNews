using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebNews.Models
{
	public partial class Model1 : DbContext
	{
		public Model1()
			: base("name=Model1")
		{
		}

		public virtual DbSet<BaiViet> BaiViet { get; set; }
		public virtual DbSet<BinhLuan> BinhLuan { get; set; }
		public virtual DbSet<DanhMuc> DanhMuc { get; set; }
		public virtual DbSet<NguoiDung> NguoiDung { get; set; }
		public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
		public virtual DbSet<ThongKe> ThongKe { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
		}
	}
}
