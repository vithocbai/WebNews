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
    public class ThongKesController : Controller
    {
        private Model1 db = new Model1();

        // GET: ThongKes
        public ActionResult Index()
        {
            var thongKe = db.ThongKe.Include(t => t.BaiViet);
            return View(thongKe.ToList());
        }

        // GET: ThongKes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongKe thongKe = db.ThongKe.Find(id);
            if (thongKe == null)
            {
                return HttpNotFound();
            }
            return View(thongKe);
        }

        // GET: ThongKes/Create
        public ActionResult Create()
        {
            ViewBag.BaiVietId = new SelectList(db.BaiViet, "Id", "TieuDe");
            return View();
        }

        // POST: ThongKes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BaiVietId,LuotXem,LuotThich,NgayCapNhat")] ThongKe thongKe)
        {
            if (ModelState.IsValid)
            {
                db.ThongKe.Add(thongKe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BaiVietId = new SelectList(db.BaiViet, "Id", "TieuDe", thongKe.BaiVietId);
            return View(thongKe);
        }

        // GET: ThongKes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongKe thongKe = db.ThongKe.Find(id);
            if (thongKe == null)
            {
                return HttpNotFound();
            }
            ViewBag.BaiVietId = new SelectList(db.BaiViet, "Id", "TieuDe", thongKe.BaiVietId);
            return View(thongKe);
        }

        // POST: ThongKes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BaiVietId,LuotXem,LuotThich,NgayCapNhat")] ThongKe thongKe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thongKe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BaiVietId = new SelectList(db.BaiViet, "Id", "TieuDe", thongKe.BaiVietId);
            return View(thongKe);
        }

        // GET: ThongKes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongKe thongKe = db.ThongKe.Find(id);
            if (thongKe == null)
            {
                return HttpNotFound();
            }
            return View(thongKe);
        }

        // POST: ThongKes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ThongKe thongKe = db.ThongKe.Find(id);
            db.ThongKe.Remove(thongKe);
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
