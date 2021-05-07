using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NovelWeb_SS5.Models;

namespace NovelWeb_SS5.Controllers
{
    public class LoginUserController : Controller
    {
        // GET: LoginUser
        private DbModelsNovelWeb db = new DbModelsNovelWeb();

        // GET: Admin/Login
        public ActionResult IndexUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorise_User(User user)
        {
            if (ModelState.IsValid)
            {
                var userDetail = db.Users
                .Where(x => x.userName == user.userName && x.passWord == user.passWord)
                .FirstOrDefault();

                if (userDetail == null)
                {
                    ViewBag.ErrorMessage = "Sai tài khoản hoặc mật khẩu";

                    return View("IndexUser", user);
                }
                else if (userDetail != null && userDetail.idQuyen == 1)
                {
                    Session["UserId"] = user.idUser;
                    Session["UserName"] = user.userName;
                    Session["UserHoTen"] = user.hoTen;
                    return RedirectToAction("Index_Admin", "CrudComments");
                }
                else
                {
                    Session["UserId"] = user.idUser;
                    Session["UserName"] = user.userName;
                    Session["UserHoTen"] = user.hoTen;
                    return RedirectToAction("Index", "Comment");
                }
            }
            else
            {
                ViewBag.error = "Đăng nhập thất bại, sai tài khoản hoặc mật khẩu";
                return View("IndexUser", user);
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

        public ActionResult CreateUser()
        {
            return View();
        }

        // POST: Admin/CrudUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser([Bind(Include = "idUser,userName,passWord,hoTen")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("IndexUser");
            }

            return View(user);
        }
    }
}