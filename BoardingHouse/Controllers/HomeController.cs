using Service;
using System.Web.Mvc;
using ViewModels;
using System.Web.Security;

namespace BoardingHouse.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserService _userService;

        public HomeController()
        {
            _userService = new UserService();
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryTokens]
        public ActionResult Login(UserViewModel user)
        {
            var loginUser = _userService.Login(user);

            if (loginUser != null)
            {
                FormsAuthentication.SetAuthCookie(user.Email, true);

                if (loginUser.Role.Id == (int)RoleEnum.Admin)
                    return Json(Url.Action("Index", "Admin"));

                return Json(Url.Action("Index", "User"));
            }

            return null;
        }
    }
}