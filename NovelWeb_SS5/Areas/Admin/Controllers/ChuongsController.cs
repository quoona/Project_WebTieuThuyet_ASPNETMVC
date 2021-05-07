using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NovelWeb_SS5.Models;

namespace NovelWeb_SS5.Areas.Admin.Controllers
{
    public class ChuongsController : Controller
    {
        private DbModelsNovelWeb db = new DbModelsNovelWeb();

        // GET: Admin/Chuongs
        public ActionResult Index()
        {
            var chuongs = db.Chuongs.Include(c => c.Truyen);
            return View(chuongs.ToList());
        }

        // GET: Admin/Chuongs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chuong chuong = db.Chuongs.Find(id);
            if (chuong == null)
            {
                return HttpNotFound();
            }
            return View(chuong);
        }

        // GET: Admin/Chuongs/Create
        public ActionResult Create(int id)
        {
            //ViewBag.idTruyen = new SelectList(db.Truyens, "idTruyen", "tenTruyen");
            ViewBag.idTruyen = db.Truyens.Where(x => x.idTruyen == id).FirstOrDefault();
            return View();
        }

        // POST: Admin/Chuongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "idChuong,soChuong,tenChuong,noiDung,idTruyen")] Chuong chuong)
        {
            if (ModelState.IsValid)
            {
                db.Chuongs.Add(chuong);
                db.SaveChanges();
                ViewBag.idTruyen = chuong.idTruyen;
                return RedirectToAction("Index", "CrudTruyens");
            }

            return View(chuong);
        }

        // GET: Admin/Chuongs/Edit/5

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chuong chuong = db.Chuongs.Find(id);
            if (chuong == null)
            {
                return HttpNotFound();
            }
            ViewBag.idTruyen = new SelectList(db.Truyens, "idTruyen", "tenTruyen", chuong.idTruyen);
            return View(chuong);
        }

        // POST: Admin/Chuongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idChuong,soChuong,tenChuong,noiDung,idTruyen")] Chuong chuong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chuong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idTruyen = new SelectList(db.Truyens, "idTruyen", "tenTruyen", chuong.idTruyen);
            return View(chuong);
        }

        // GET: Admin/Chuongs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chuong chuong = db.Chuongs.Where(x => x.idChuong == id).FirstOrDefault();
            if (chuong == null)
            {
                return HttpNotFound();
            }
            return View(chuong);
        }

        // POST: Admin/Chuongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Chuong chuong = db.Chuongs.Find(id);
            db.Chuongs.Remove(chuong);
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