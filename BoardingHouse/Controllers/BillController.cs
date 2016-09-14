using System;
using System.Web.Mvc;
using Service;
using ViewModels;

namespace BoardingHouse.Controllers
{
    public class BillController : Controller
    {
        private readonly BillService _billService;

        public BillController()
        {
            _billService = new BillService();
        }

        [HttpGet]
        public FileResult GenerateBill()//BillViewModel model
        {
            byte[] content = _billService.GenerateBill(new BillViewModel());

            return new FileContentResult(content, "application/pdf");
        }
    }
}