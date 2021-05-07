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
    public class CrudUsersController : Controller
    {
        private DbModelsNovelWeb db = new DbModelsNovelWeb();

        // GET: Admin/CrudUsers
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.PhanQuyen);
            return View(users.ToList());
        }

        // GET: Admin/CrudUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admin/CrudUsers/Create
        public ActionResult Create()
        {
            ViewBag.idQuyen = new SelectList(db.PhanQuyens, "idQuyen", "loaiQuyen");
            return View();
        }

        // POST: Admin/CrudUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idUser,userName,passWord,hoTen,idQuyen")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idQuyen = new SelectList(db.PhanQuyens, "idQuyen", "loaiQuyen", user.idQuyen);
            return View(user);
        }

        // GET: Admin/CrudUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.idQuyen = new SelectList(db.PhanQuyens, "idQuyen", "loaiQuyen", user.idQuyen);
            return View(user);
        }

        // POST: Admin/CrudUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idUser,userName,passWord,hoTen,idQuyen")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idQuyen = new SelectList(db.PhanQuyens, "idQuyen", "loaiQuyen", user.idQuyen);
            return View(user);
        }

        // GET: Admin/CrudUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/CrudUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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