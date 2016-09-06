using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using ViewModels;

namespace BoardingHouse.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController()
        {
            _userService = new UserService();
        }
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetLoginUser()
        {
            try
            {
                var httpCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                if (httpCookie == null) return null;

                var decryptCookie = FormsAuthentication.Decrypt(httpCookie.Value);
                if (decryptCookie == null) return null;

                return Json(_userService.GetLoginUser(decryptCookie.Name));
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult GetCurrentRentByUser(UserViewModel user)
        {
            try
            {
                return Json(_userService.GetCurrentRentByUser(user));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult GetAllRentByUser(UserViewModel user)
        {
            try
            {
                return Json(_userService.GetAllRentByUser(user));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult UploadPicture()
        {
            try
            {
                var file = Request.Files[0];
                return Json(true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}