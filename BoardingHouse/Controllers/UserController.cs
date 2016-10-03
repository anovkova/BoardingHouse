using Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
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

                var user = _userService.GetLoginUser(decryptCookie.Name);

                return Json(user);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult GetCurrentRentByUser()
        {
            try
            {
                var httpCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                if (httpCookie == null) return null;

                var decryptCookie = FormsAuthentication.Decrypt(httpCookie.Value);
                if (decryptCookie == null) return null;

                var user = _userService.GetLoginUser(decryptCookie.Name);

                return Json(_userService.GetCurrentRentByUser(user));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult GetAllRentByUser()
        {
            try
            {
                var httpCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                if (httpCookie == null) return null;

                var decryptCookie = FormsAuthentication.Decrypt(httpCookie.Value);
                if (decryptCookie == null) return null;

                var user = _userService.GetLoginUser(decryptCookie.Name);

                return Json(_userService.GetAllRentByUser(user));
            
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public void UploadPicture()
        {
            try
            {
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    if (file != null && file.ContentLength <= 52428800)
                    {
                        BinaryReader b = new BinaryReader(file.InputStream);
                        byte[] binData = b.ReadBytes(file.ContentLength);

                        var httpCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                        var decryptCookie = FormsAuthentication.Decrypt(httpCookie.Value);

                        var user = _userService.GetLoginUser(decryptCookie.Name);
                        _userService.UploadPicture(user, binData);




                        //JavaScriptSerializer serializer = new JavaScriptSerializer();
                        //serializer.MaxJsonLength = Int32.MaxValue;
                        //AssetDeclarationViewModel assetDeclarationId = serializer.Deserialize<AssetDeclarationViewModel>(assetDeclaration);

                        //AssetDeclarationService service = new AssetDeclarationService();
                        //service.UpdateAdSignitureDocument(binData, assetDeclarationId.Id);


                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentOutOfRangeException("Documentot ne e prikacen");
            }
          
        }
    }
}