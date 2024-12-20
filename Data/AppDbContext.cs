using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebNews.Models;

namespace WebNews.Data
{
	public class AppDbContext : DbContext
	{
		public DbSet<BaiViet> BaiViets { get; set; }
		public DbSet<DanhMuc> DanhMucs { get; set; }

		public AppDbContext() : base("DefaultConnection")
		{
		}

	}
}

