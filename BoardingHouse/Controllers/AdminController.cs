using Service;
using System.Web.Mvc;
using System.Web.Security;
using ViewModels;


namespace BoardingHouse.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserService _userService;

        public AdminController()
        {
            _userService = new UserService();
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Json(users);
        }

        [HttpPost]
        public JsonResult GetAllRents()
        {
            var rents = _userService.GetAllRents();
            return Json(rents);
        }

        [HttpPost]
        public JsonResult GetFloors()
        {
            var floors = _userService.GetFloors();
            return Json(floors);
        }

        [HttpPost]
        public JsonResult AddUser(UserViewModel user)
        {
            _userService.AddUser(user);
            return Json(true);
        }

        [HttpPost]
        public JsonResult MakeAReservation(ReservationViewModel reservation)
        {
            _userService.MakeAReservation(reservation);
            return Json(true);
        }

        [HttpPost]
        public JsonResult SearchFreeFloors(ReservationViewModel model)
        {
            var result = _userService.SearchFreeFloors(model);
            return Json(result);
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return Json(Url.Action("Index", "Home"));
        }
    }
}