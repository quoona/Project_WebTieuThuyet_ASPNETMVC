using NovelWeb_SS5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NovelWeb_SS5.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private DbModelsNovelWeb db = new DbModelsNovelWeb();

        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorise(User user)
        {
            if (ModelState.IsValid)
            {
                var userDetail = db.Users
                .Where(x => x.userName == user.userName && x.passWord == user.passWord)
                .FirstOrDefault();

                if (userDetail == null)
                {
                    ViewBag.ErrorMessage = "Sai tài khoản hoặc mật khẩu";

                    return View("Index", user);
                }
                else if (userDetail != null && userDetail.idQuyen == 1)
                {
                    Session["UserId"] = user.idUser;
                    Session["UserName"] = user.userName;
                    Session["UserHoTen"] = user.hoTen;
                    return RedirectToAction("Welcome");
                }
                else if (userDetail != null && userDetail.idQuyen == 2)
                {
                    Session["UserId"] = user.idUser;
                    Session["UserName"] = user.userName;
                    Session["UserHoTen"] = user.hoTen;
                    return RedirectToAction("Index", "HomeTruyens", new { area = "" });
                }
                else
                {
                    ViewBag.error = "Đăng nhập thất bại, sai tài khoản hoặc mật khẩu";
                    return View("Index", user);
                }
            }
            else
            {
                ViewBag.error = "Đăng nhập thất bại, sai tài khoản hoặc mật khẩu";
                return View("Index", user);
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

        public ActionResult Welcome()
        {
            return View();
        }
    }
}