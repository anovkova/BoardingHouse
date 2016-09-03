using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;
using System.Web.Security;

namespace BoardingHouse.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]  
        [ValidateAntiForgeryTokens]  
        public ActionResult Login(UserViewModel user)
        {
            UserService service = new UserService();

            var loginUser = service.Login(user);
            if (loginUser != null)
            {
                FormsAuthentication.SetAuthCookie(user.Email,true);
               
                //var test = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                if (loginUser.Role.Title=="admin")
                    return Json(Url.Action("Index", "Admin"));
                else
                    return Json(Url.Action("Index", "User"));
            }
            return null;
            //return Json(Url.Action("Index", "Home"));

        }
    }
}