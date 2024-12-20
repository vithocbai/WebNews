using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNews.Models;

namespace WebNews.Controllers
{
    public class HomeMainController : Controller
    {
		
		private Model1 db = new Model1(); // Kết nối cơ sở dữ liệu

		public ActionResult Index()
		{
			if (Session["Username"] != null)
			{
				ViewBag.Message = $"Chào mừng, {Session["Username"]}!";
			}
			else
			{
				ViewBag.Message = "Bạn chưa đăng nhập!";
			}
			var baiViet = db.BaiViet.ToList();
			return View(baiViet); // Truyền danh sách sản phẩm vào View
		}

		//public ActionResult TrangChu()
		//{
		//	var baiViet = db.BaiViet.ToList();
		//	return View(baiViet);
		//}
		public ActionResult TrangChu()
		{
			// Lấy 3 bài viết ngẫu nhiên từ cơ sở dữ liệu
			var tinPhoBien = db.BaiViet
				.OrderBy(r => Guid.NewGuid()) // Sắp xếp ngẫu nhiên
				.Take(4) // Lấy 3 bài viết ngẫu nhiên
				.Include(b => b.DanhMuc) // Include để lấy thông tin danh mục
				.ToList();

			ViewBag.TinPhoBien = tinPhoBien;

			// Loại bỏ trùng lặp danh mục
			var danhMucList = db.DanhMuc
				.GroupBy(dm => dm.TenDanhMuc)
				.Select(g => g.FirstOrDefault())
				.OrderBy(dm => dm.NgayTao)
				.ToList();

			// Sắp xếp "Thời sự" lên đầu
			danhMucList = danhMucList.OrderBy(dm => dm.TenDanhMuc == "Thời Sự" ? 0 : 1).ToList();
			ViewBag.DanhMucList = danhMucList;

			var baiViet = db.BaiViet.ToList();
			return View(baiViet);

		}

		public ActionResult DanhMuc(int id)
		{
			// Dùng cho footer
			var tinPhoBien = db.BaiViet
				.OrderBy(r => Guid.NewGuid()) // Sắp xếp ngẫu nhiên
				.Take(4) // Lấy 4 bài viết ngẫu nhiên
				.Include(b => b.DanhMuc) // Include để lấy thông tin danh mục
				.ToList();

			ViewBag.TinPhoBien = tinPhoBien;

			// Loại bỏ trùng lặp danh mục
			var danhMucList = db.DanhMuc
				.GroupBy(dm => dm.TenDanhMuc)
				.Select(g => g.FirstOrDefault())
				.OrderBy(dm => dm.NgayTao)
				.ToList();

			// Sắp xếp "Thời sự" lên đầu
			danhMucList = danhMucList.OrderBy(dm => dm.TenDanhMuc == "Thời Sự" ? 0 : 1).ToList();
			ViewBag.DanhMucList = danhMucList;

			// Lấy danh sách bài viết theo DanhMucId
			var baiVietTheoDanhMuc = db.BaiViet
				.Where(b => b.DanhMucId == id)
				.Include(b => b.DanhMuc) // Include để lấy thông tin danh mục
				.ToList();

			
			// Lấy thông tin danh mục hiện tại
			var danhMuc = db.DanhMuc.FirstOrDefault(d => d.Id == id);
			if (danhMuc == null)
			{
				return HttpNotFound(); // Trả về lỗi 404 nếu không tìm thấy danh mục
			}

			ViewBag.TenDanhMuc = danhMuc.TenDanhMuc; // Truyền tên danh mục vào View
			ViewBag.SelectedDanhMucId = id; // Truyền id của danh mục đang được chọn vào ViewBag
			return View(baiVietTheoDanhMuc); // Truyền danh sách bài viết vào View
		}



		public ActionResult LienHe() {

			var tinPhoBien = db.BaiViet
				.OrderBy(r => Guid.NewGuid()) // Sắp xếp ngẫu nhiên
				.Take(4) // Lấy 3 bài viết ngẫu nhiên
				.Include(b => b.DanhMuc) // Include để lấy thông tin danh mục
				.ToList();

			ViewBag.TinPhoBien = tinPhoBien;

			// Loại bỏ trùng lặp danh mục
			var danhMucList = db.DanhMuc
				.GroupBy(dm => dm.TenDanhMuc)
				.Select(g => g.FirstOrDefault())
				.OrderBy(dm => dm.NgayTao)
				.ToList();

			// Sắp xếp "Thời sự" lên đầu
			danhMucList = danhMucList.OrderBy(dm => dm.TenDanhMuc == "Thời Sự" ? 0 : 1).ToList();
			ViewBag.DanhMucList = danhMucList;

			return View();
		}

		// Hiển thị giao diện đăng nhập
		[HttpGet]
		public ActionResult DangNhap()
		{
			return View();
		}

		public ActionResult ChiTiet(int id)
		{
			var tinPhoBien = db.BaiViet
				.OrderBy(r => Guid.NewGuid()) // Sắp xếp ngẫu nhiên
				.Take(4) // Lấy 3 bài viết ngẫu nhiên
				.Include(b => b.DanhMuc) // Include để lấy thông tin danh mục
				.ToList();

			ViewBag.TinPhoBien = tinPhoBien;

			// Loại bỏ trùng lặp danh mục
			var danhMucList = db.DanhMuc
				.GroupBy(dm => dm.TenDanhMuc)
				.Select(g => g.FirstOrDefault())
				.OrderBy(dm => dm.NgayTao)
				.ToList();

			// Sắp xếp "Thời sự" lên đầu
			danhMucList = danhMucList.OrderBy(dm => dm.TenDanhMuc == "Thời Sự" ? 0 : 1).ToList();
			ViewBag.DanhMucList = danhMucList;

			// Tìm bài viết theo ID
			var baiViet = db.BaiViet
				.Include(b => b.DanhMuc) // Lấy thêm thông tin DanhMuc nếu cần
				.FirstOrDefault(b => b.Id == id);

			if (baiViet == null)
			{
				return HttpNotFound(); // Trả về lỗi 404 nếu không tìm thấy bài viết
			}

			return View(baiViet);
		}

	}

}