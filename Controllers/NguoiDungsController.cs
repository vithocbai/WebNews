using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebNews.Models;

namespace WebNews.Controllers
{
    public class NguoiDungsController : Controller
    {
        private Model1 db = new Model1();

		// GET: NguoiDungs/Login
		public ActionResult Login()
		{
			return View();
		}

		// POST: NguoiDungs/Login
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = db.NguoiDung.FirstOrDefault(u => u.TenDangNhap == model.TenDangNhap
														&& u.MatKhau == model.MatKhau);

				if (user != null)
				{
					// Lưu thông tin người dùng vào session
					Session["UserId"] = user.Id;
					Session["Username"] = user.TenDangNhap;
					Session["VaiTro"] = user.VaiTro;

					// Nếu là Admin, chuyển hướng đến trang Admin
					if (user.VaiTro == "Admin")
					{
						return RedirectToAction("Index", "NguoiDungs");
					}

					// Người dùng thường thì về trang Home
					return RedirectToAction("Index", "NguoiDungs");
				}
				else
				{
					ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
				}
			}

			return View(model);
		}

		// GET: NguoiDungs/Logout
		public ActionResult Logout()
		{
			Session.Clear(); // Xóa session
			return RedirectToAction("Login");
		}

		// GET: NguoiDungs
		public ActionResult Index()
        {
            return View(db.NguoiDung.ToList());
        }

        // GET: NguoiDungs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NguoiDung nguoiDung = db.NguoiDung.Find(id);
            if (nguoiDung == null)
            {
                return HttpNotFound();
            }
            return View(nguoiDung);
        }

        // GET: NguoiDungs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NguoiDungs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TenDangNhap,MatKhau,HoTen,Email,VaiTro,NgayTao")] NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                db.NguoiDung.Add(nguoiDung);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nguoiDung);
        }

        // GET: NguoiDungs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NguoiDung nguoiDung = db.NguoiDung.Find(id);
            if (nguoiDung == null)
            {
                return HttpNotFound();
            }
            return View(nguoiDung);
        }

        // POST: NguoiDungs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TenDangNhap,MatKhau,HoTen,Email,VaiTro,NgayTao")] NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nguoiDung).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nguoiDung);
        }

        // GET: NguoiDungs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NguoiDung nguoiDung = db.NguoiDung.Find(id);
            if (nguoiDung == null)
            {
                return HttpNotFound();
            }
            return View(nguoiDung);
        }

        // POST: NguoiDungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NguoiDung nguoiDung = db.NguoiDung.Find(id);
            db.NguoiDung.Remove(nguoiDung);
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
