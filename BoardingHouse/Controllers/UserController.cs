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

                var email = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                UserService service = new UserService();

                return Json(service.GetLoginUser(email));
               
               
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

                var email = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                UserService service = new UserService();

                return Json(service.GetCurrentRentByUser(user));


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
                UserService service = new UserService();
                return Json(service.GetAllRentByUser(user));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    
    }
}