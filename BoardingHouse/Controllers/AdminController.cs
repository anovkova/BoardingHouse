using Mappers;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ViewModels;


namespace BoardingHouse.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
           return View();
        }

        [HttpPost]
        public JsonResult GetAllUsers()
        {
            try
            {
                UserService service = new UserService();
                var users = service.getAllUsers();
                return Json(users);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult GetAllRents()
        {
            try
            {
                UserService service = new UserService();
                var rents = service.GetAllRents();
                return Json(rents);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        [HttpPost]
        public JsonResult GetFloors()
        {
            try
            {
                UserService service = new UserService();
                var floors = service.GetFloors();

                return Json(floors);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult GetFloorsByNumOfFloor(FloorViewModel floor)
        {
            try
            {
                UserService service = new UserService();
                var floors = service.GetFloorsByNumOfFloor(floor);

                return Json(floors);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult ActiveRents(List<RoomViewModel> rooms)
        {
            try
            {
                UserService service = new UserService();
                var validRooms = service.ActiveRents(rooms);

                return Json(validRooms);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult AddUser(UserViewModel user)
        {
            try
            {
                UserService service = new UserService();
                service.AddUser(user);

                return Json(true);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult UpdateUser(UserViewModel user)
        {
            try
            {
                UserService service = new UserService();
                var users = service.UpdateUser(user);

                return Json(users);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult GetFreeRoom(ReservationViewModel reservation)
        {
            try
            {
                UserService service = new UserService();
                var rooms = service.GetFreeRoom(reservation);

                return Json(rooms);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public bool makeAReservation(ReservationViewModel reservation)
        {
            try
            {
                UserService service = new UserService();
                return service.makeAReservation(reservation);
               
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return Json(Url.Action("Index", "Home"));
        }

    }
}