using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebNews.Data;
using WebNews.Models;
using System.Data.Entity;
using System.Text.RegularExpressions;
using System.IO;

namespace WebNews.Controllers
{
	public class BaiVietsController : Controller
	{
		private Model1 db = new Model1();


		// GET: BaiViets
		public ActionResult Index()
		{
		

			// Lấy toàn bộ danh sách bài viết để truyền qua View
			var baiViet = db.BaiViet.Include(b => b.DanhMuc).Include(b => b.NguoiDung);
			return View(baiViet.ToList());

		}

		// GET: BaiViets/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			BaiViet baiViet = db.BaiViet.Find(id);
			if (baiViet == null)
			{
				return HttpNotFound();
			}
			return View(baiViet);
		}

		public ActionResult Create()
		{
			// Lấy danh sách danh mục không trùng lặp
			ViewBag.DanhMucId = new SelectList(
				db.DanhMuc
					.GroupBy(d => d.TenDanhMuc) // Nhóm theo TenDanhMuc để loại bỏ trùng
					.Select(g => g.FirstOrDefault()) // Chọn một mục đầu tiên trong nhóm
					.Select(d => new { d.Id, d.TenDanhMuc }), // Dựng dữ liệu
				"Id",
				"TenDanhMuc"
			);

			ViewBag.NguoiDungId = new SelectList(db.NguoiDung, "Id", "TenDangNhap");
			return View();
		}


		// POST: BaiViets/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateInput(false)]
		public ActionResult Create([Bind(Include = "Id,TieuDe,NoiDung,NgayDang,TrangThai,NguoiDungId,DanhMucId,Anh")] BaiViet baiViet)
		{
			if (ModelState.IsValid)
			{
				//// Bước 1: Giải mã HTML Entities
				//string decodedMoTa = HttpUtility.HtmlDecode(baiViet.NoiDung);

				//// Bước 2: Loại bỏ các thẻ HTML
				//baiViet.NoiDung = StripHtmlTags(decodedMoTa);

				db.BaiViet.Add(baiViet);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			ViewBag.DanhMucId = new SelectList(db.DanhMuc, "Id", "TenDanhMuc", baiViet.DanhMucId);
			ViewBag.NguoiDungId = new SelectList(db.NguoiDung, "Id", "TenDangNhap", baiViet.NguoiDungId);
			return View(baiViet);
		}
		// Hàm loại bỏ các thẻ HTML
		private string StripHtmlTags(string input)
		{
			if (string.IsNullOrEmpty(input)) return string.Empty;

			// Sử dụng Regex để loại bỏ thẻ HTML
			return Regex.Replace(input, "<.*?>", string.Empty);
		}
		// GET: BaiViets/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			BaiViet baiViet = db.BaiViet.Find(id);
			if (baiViet == null)
			{
				return HttpNotFound();
			}

			// Lấy danh sách danh mục không trùng lặp
			var danhMucList = db.DanhMuc
				.GroupBy(d => d.TenDanhMuc) // Nhóm theo TenDanhMuc để loại bỏ trùng
				.Select(g => g.FirstOrDefault()) // Chọn mục đầu tiên trong nhóm
				.Select(d => new { d.Id, d.TenDanhMuc }).ToList();

			// Gửi danh sách Danh Mục vào ViewBag.DanhMucId với giá trị đã chọn
			ViewBag.DanhMucId = new SelectList(danhMucList, "Id", "TenDanhMuc", baiViet.DanhMucId);

			// Gửi danh sách Người Dùng vào ViewBag.NguoiDungId với giá trị đã chọn
			ViewBag.NguoiDungId = new SelectList(db.NguoiDung, "Id", "TenDangNhap", baiViet.NguoiDungId);

			return View(baiViet);
		}



		// POST: BaiViets/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateInput(false)]
		public ActionResult Edit([Bind(Include = "Id,TieuDe,NoiDung,NgayDang,TrangThai,NguoiDungId,DanhMucId,Anh")] BaiViet baiViet, HttpPostedFileBase Anhbia)
		{
			if (ModelState.IsValid)
			{
				if (Anhbia != null && Anhbia.ContentLength > 0)
				{
					string fileName = Path.GetFileName(Anhbia.FileName);
					string path = Path.Combine(Server.MapPath("~/Hinhsanpham/"), fileName);

					// Lưu file
					Anhbia.SaveAs(path);

					// Gán tên file
					baiViet.Anh = fileName;
				}

				//// Bước 1: Giải mã HTML Entities
				//string decodedMoTa = HttpUtility.HtmlDecode(baiViet.NoiDung);

				//// Bước 2: Loại bỏ các thẻ HTML
				//baiViet.NoiDung = StripHtmlTags(decodedMoTa);

				db.Entry(baiViet).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewBag.DanhMucId = new SelectList(db.DanhMuc, "Id", "TenDanhMuc", baiViet.DanhMucId);
			ViewBag.NguoiDungId = new SelectList(db.NguoiDung, "Id", "TenDangNhap", baiViet.NguoiDungId);
			return View(baiViet);
		}

		// GET: BaiViets/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			BaiViet baiViet = db.BaiViet.Find(id);
			if (baiViet == null)
			{
				return HttpNotFound();
			}
			return View(baiViet);
		}

		// POST: BaiViets/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			BaiViet baiViet = db.BaiViet.Find(id);
			db.BaiViet.Remove(baiViet);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
