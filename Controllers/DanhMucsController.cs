using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebNews.Models;

namespace WebNews.Controllers
{
	public class DanhMucsController : Controller
	{
		private Model1 db = new Model1();

		// GET: DanhMucs
		public ActionResult Index()
		{
			return View(db.DanhMuc.ToList());
		}

		// GET: DanhMucs/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			DanhMuc danhMuc = db.DanhMuc.Find(id);
			if (danhMuc == null)
			{
				return HttpNotFound();
			}
			return View(danhMuc);
		}

		// GET: DanhMucs/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: DanhMucs/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		// POST: DanhMucs/Create
		[HttpPost]
		[ValidateInput(false)]
		public ActionResult Create([Bind(Include = "Id,TenDanhMuc,MoTa,NgayTao,Anh")] DanhMuc danhMuc)
		{
			if (ModelState.IsValid)
			{
				// Bước 1: Giải mã HTML Entities
				string decodedMoTa = HttpUtility.HtmlDecode(danhMuc.MoTa);

				// Bước 2: Loại bỏ các thẻ HTML
				danhMuc.MoTa = StripHtmlTags(decodedMoTa);

				// Lưu dữ liệu vào cơ sở dữ liệu
				db.DanhMuc.Add(danhMuc);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(danhMuc);
		}

		// Hàm loại bỏ các thẻ HTML
		private string StripHtmlTags(string input)
		{
			if (string.IsNullOrEmpty(input)) return string.Empty;

			// Sử dụng Regex để loại bỏ thẻ HTML
			return Regex.Replace(input, "<.*?>", string.Empty);
		}



		// GET: DanhMucs/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			DanhMuc danhMuc = db.DanhMuc.Find(id);
			if (danhMuc == null)
			{
				return HttpNotFound();
			}
			return View(danhMuc);
		}

		// POST: DanhMucs/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateInput(false)]
		public ActionResult Edit([Bind(Include = "Id,TenDanhMuc,MoTa,NgayTao,Anh")] DanhMuc danhMuc, HttpPostedFileBase Anhbia)
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
					danhMuc.Anh = fileName;
				}
				// Bước 1: Giải mã HTML Entities
				string decodedMoTa = HttpUtility.HtmlDecode(danhMuc.MoTa);

				// Bước 2: Loại bỏ các thẻ HTML
				danhMuc.MoTa = StripHtmlTags(decodedMoTa);

				db.Entry(danhMuc).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(danhMuc);
		}


		// GET: DanhMucs/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			DanhMuc danhMuc = db.DanhMuc.Find(id);
			if (danhMuc == null)
			{
				return HttpNotFound();
			}
			
			return View(danhMuc);
		}

		// POST: DanhMucs/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			DanhMuc danhMuc = db.DanhMuc.Find(id);
			db.DanhMuc.Remove(danhMuc);
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
