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

namespace NovelWeb_SS5.Controllers
{
    public class HomeTruyensController : Controller
    {
        private DbModelsNovelWeb db = new DbModelsNovelWeb();

        // GET: HomeTruyens

        // GET: Admin/Truyens
        public ActionResult Index()
        {
            List<Truyen> truyens = db.Truyens.Include(t => t.TheLoai).ToList();

            return View(truyens);
        }

        public ActionResult TruyenDetails(int? id)
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

        //Tro toi chuong moi + 1
        public ActionResult ChiTietChuongNext(int id)
        {
            DbModelsNovelWeb context = new DbModelsNovelWeb();

            Chuong chuong = context.Chuongs.FirstOrDefault(x => x.idChuong == id + 1);

            return PartialView(chuong);
        }

        public ActionResult PageListTruyen(int page = 1, int pageSize = 2)
        {
            List<Truyen> truyens = db.Truyens.ToList();
            PagedList<Truyen> model = new PagedList<Truyen>(truyens, page, pageSize);
            return View(model);
        }

        public List<Truyen> TruyenMoi(int count)
        {
            return db.Truyens.OrderByDescending(a => a.ngayDang).Take(count).ToList();
        }

        public ActionResult TruyenMoiNhat()
        {
            var truyenmoi = TruyenMoi(4);
            return PartialView(truyenmoi);
        }

        [ChildActionOnly]
        public ActionResult TheLoai()
        {
            List<TheLoai> theloais = db.TheLoais.ToList();
            return PartialView(theloais);
        }

        public ActionResult TruyenTheoTheLoai(int id)
        {
            List<Truyen> truyens = db.Truyens.Where(x => x.idTL == id).ToList();
            return View(truyens);
        }

        public ActionResult SaveTruyens(int idTruyen)
        {
            var savetruyen = new List<Truyen>();
            var truyen = db.Truyens.Find(idTruyen);

            savetruyen.Add(new Truyen()
            {
                tenTruyen = truyen.tenTruyen
            ,
                idTruyen = truyen.idTruyen
            });
            Session["truyenn"] = savetruyen;
            return View();
        }

        public List<Truyen> TruyenXN(int count)
        {
            return db.Truyens.OrderBy(a => a.ngayDang).ThenBy(a => a.idTruyen).Take(count).ToList();
        }

        public ActionResult TruyenXemNhieu()
        {
            var truyenmoi1 = TruyenXN(4);
            return PartialView(truyenmoi1);
        }

        public ActionResult XemNgay(int id)
        {
            Chuong chuongs = db.Chuongs.FirstOrDefault(a => a.idChuong == id);

            return RedirectToAction("ChiTietChuong");
        }

        //Xem ham nayyy

        public ActionResult Sorting(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var truyens = from s in db.Truyens
                          select s;
            switch (sortOrder)
            {
                case "name_desc":
                    truyens = truyens.OrderByDescending(s => s.idTruyen);
                    break;

                case "Date":
                    truyens = truyens.OrderBy(s => s.ngayDang);
                    break;

                case "date_desc":
                    truyens = truyens.OrderByDescending(s => s.ngayDang);
                    break;

                default:
                    truyens = truyens.OrderBy(s => s.idTruyen);
                    break;
            }
            return View(truyens.ToList());
        }

        public ActionResult UserSearchTruyens(string searching)
        {
            List<Truyen> truyens = db.Truyens
               .Where(x => x.tenTruyen.Contains(searching) | searching == null)
               .ToList();

            return PartialView(truyens);
        }

        public ActionResult BreadCrumb(int? id)
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
            return PartialView(truyen);
        }

        public ActionResult Titlesectiontitle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TheLoai theLoai = db.TheLoais.Find(id);
            if (theLoai == null)
            {
                return HttpNotFound();
            }
            return PartialView(theLoai);
        }

        public ActionResult BreadcrumbCate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TheLoai theLoai = db.TheLoais.Find(id);
            if (theLoai == null)
            {
                return HttpNotFound();
            }
            return PartialView(theLoai);
        }
    }
}