using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BillManager.Models;

namespace BillManager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //var billsList = new List<BillEntry>();

            //BillEntry newBill = new BillEntry
            //{
            //    Name = "Test",
            //    Amount = 10.45,
            //    DueDate = new DateTime(2021, 12, 25)
            //};

            //billsList.Add(newBill);

            var bill = new BillEntry();


            return View(bill);
        }

        [HttpPost]
        public ActionResult Index(BillEntry entry)
        {
            BillEntry.addBill(entry.Name, entry.Amount, entry.DueDate);

            return View();
        }
    }
}