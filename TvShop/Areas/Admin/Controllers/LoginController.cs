using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TvShop.Areas.Admin.Models;
using TvShop.Common;
using TvShop.DAO;

namespace TvShop.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private readonly AccountDAO _accountDAO;
        public LoginController()
        {
            _accountDAO = new AccountDAO();
        }

        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(LoginModel model)
        {
            if (model.userName == "" || model.userName == null)
            {
                ModelState.AddModelError("", "Bạn không được để chống trường này !");
            }
            if (model.passWord == "" || model.passWord == null)
            {
                ModelState.AddModelError("", "Bạn không được để chống trường này !");
            }

            var result = _accountDAO.CheckInforLogin(model.userName, model.passWord);
            if (result != null)
            {
                var userSession = new userLogin();
                userSession.userName = result.username;
                userSession.userID = result.id;
                userSession.loginDate = result.loginDate;

                Session.Add(commonConst.user_session, userSession);
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ModelState.AddModelError("", "Sai thông tin đăng nhập !");
            }
            return View("Index");
        }

        public ActionResult Logout()
        {
            if (Session[commonConst.user_session] != null)
            {
                Session[commonConst.user_session] = null;
            }
            return RedirectToAction("Index", "login");
        }
    }
}