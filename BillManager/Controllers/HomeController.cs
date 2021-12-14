using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BillManager.Models;
using System.Data.SQLite;


namespace BillManager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var bill = new BillEntry();


            return View(bill);
        }

        [HttpPost]
        public ActionResult Index(BillEntry entry)
        {
            BillEntry.addBill(entry.Name, entry.Amount, entry.DueDate);
            BillEntry.SendMessage(String.Format("New bill: {0}\nAmount: ${1} each\nDue date: {2}", entry.Name, Math.Round(entry.Amount / 2, 2), entry.DueDate.ToString("MM/dd/yyyy")));

            return View();
        }

        public ActionResult History()
        {
            return View(BillEntry.GetBillEntries());
        }
    }

}