using Model.Dao;
using Model.EF;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        public ActionResult Index(int page = 1, int pageSize = 3)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(page, pageSize);
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            var dao = new UserDao();
            var encryp = Encryptor.MD5Hash(user.Password);
            user.Password = encryp;
            long id = dao.Insert(user);
            if (id > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Thêm mới thất bại.");
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var user = new UserDao().ViewDetail(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            var dao = new UserDao();
            if (!string.IsNullOrEmpty(user.Password))
            {
                var encryp = Encryptor.MD5Hash(user.Password);
                user.Password = encryp;
            }
            bool res = dao.Update(user);
            if (res)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Cập nhật thất bại.");
                return View();
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}