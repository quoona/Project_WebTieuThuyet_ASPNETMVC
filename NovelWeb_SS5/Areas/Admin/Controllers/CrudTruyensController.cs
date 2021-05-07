using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NovelWeb_SS5.Models;
using PagedList;

namespace NovelWeb_SS5.Areas.Admin.Controllers
{
    public class CrudTruyensController : Controller
    {
        private DbModelsNovelWeb db = new DbModelsNovelWeb();

        // GET: Admin/Truyens
        public ActionResult Index(int page = 1, int pageSize = 5)
        {
            List<Truyen> truyens = db.Truyens.Include(t => t.TheLoai).ToList();
            PagedList<Truyen> model = new PagedList<Truyen>(truyens, page, pageSize);
            return View(model);
        }

        // GET: Admin/Truyens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Truyen truyen = db.Truyens.Find(id);
            if (truyen == null)
            {
                return HttpNotFound();
            }
            return View(truyen);
        }

        // GET: Admin/Truyens/Create
        public ActionResult Create()
        {
            ViewBag.idTL = new SelectList(db.TheLoais, "idTL", "tenLoai");
            return View();
        }

        // POST: Admin/Truyens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "idTruyen" +
            ",idTL" +
            ",tenTruyen" +
            ",tacGia" +
            ",tinhTrang" +

            ",tomTat" +
            ",ngayDang")]
            Truyen truyen
            , HttpPostedFileBase uploadhinh)
        {
            if (ModelState.IsValid)
            {
                if (uploadhinh != null && uploadhinh.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(uploadhinh.FileName);
                    string path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/images"), fileName);
                    uploadhinh.SaveAs(path);
                    truyen.anhDaidien = fileName;

                    db.Truyens.Add(truyen);

                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            ViewBag.idTL = new SelectList(db.TheLoais, "idTL", "tenLoai", truyen.idTL);
            return View(truyen);
        }

        // GET: Admin/Truyens/Edit/5
        [ValidateInput(false)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Truyen truyen = db.Truyens.Find(id);
            if (truyen == null)
            {
                return HttpNotFound();
            }
            ViewBag.idTL = new SelectList(db.TheLoais, "idTL", "tenLoai", truyen.idTL);
            return View(truyen);
        }

        // POST: Admin/Truyens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "idTruyen,idTL,tenTruyen,tacGia,tinhTrang,anhDaidien,tomTat,ngayDang")] Truyen truyen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(truyen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idTL = new SelectList(db.TheLoais, "idTL", "tenLoai", truyen.idTL);
            return View(truyen);
        }

        // GET: Admin/Truyens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Truyen truyen = db.Truyens.Find(id);
            if (truyen == null)
            {
                return HttpNotFound();
            }
            return View(truyen);
        }

        // POST: Admin/Truyens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Truyen truyen = db.Truyens.Find(id);
            db.Truyens.Remove(truyen);
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

        public ActionResult DanhSachChuong(int id)
        {
            DbModelsNovelWeb context = new DbModelsNovelWeb();

            List<Chuong> chuongs = context.Chuongs.ToList();
            var q = from x in chuongs
                    where x.idTruyen == id
                    select x;

            return PartialView(q);
        }

        public ActionResult ChiTietChuong(int id)
        {
            DbModelsNovelWeb context = new DbModelsNovelWeb();

            Chuong chuong = context.Chuongs.FirstOrDefault(x => x.idChuong == id);

            return PartialView(chuong);
        }

        public ActionResult PageListTruyen(int page = 1, int pageSize = 2)
        {
            List<Truyen> truyens = db.Truyens.ToList();
            PagedList<Truyen> model = new PagedList<Truyen>(truyens, page, pageSize);
            return View(model);
        }

        public ActionResult CreateChuongs()
        {
            ViewBag.idTruyen = new SelectList(db.Truyens, "idTruyen", "tenTruyen");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateChuongs([Bind(Include = "idChuong,soChuong,tenChuong,noiDung,idTruyen")] Chuong chuong)
        {
            if (ModelState.IsValid)
            {
                db.Chuongs.Add(chuong);
                db.SaveChanges();

                return RedirectToAction("Index", "CrudTruyens");
            }
            //Xem lại chỗ này ????????????????
            ViewBag.idTruyen = new SelectList(db.Truyens, "idTruyen", "tenTruyen", chuong.idTruyen);
            return View(chuong);
        }

        public ActionResult SearchTruyens(string searching)
        {
            List<Truyen> truyens = db.Truyens.Where(x => x.tenTruyen.Contains(searching) | searching == null).ToList();
            return PartialView(truyens);
        }

        public ActionResult ChuongTheoTruyen(int id)
        {
            Chuong chuong = db.Chuongs.Where(x => x.idTruyen == id).FirstOrDefault();

            return View(chuong);
        }
    }
}