using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web;

namespace BillManager.Models
{
    public class BillEntry
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Amount is required")]
        public double Amount { get; set; }
        [Required(ErrorMessage = "Due Date is required")]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        public static void addBill(string Name, double Amount, DateTime DueDate)
        {

            using (SQLiteConnection conn = new SQLiteConnection("Data Source =" + HttpContext.Current.Server.MapPath("~/") + "DB/Bills.db; Version = 3; New = false;"))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    string strSql = String.Format("INSERT INTO [Bills] ([Name],[Amount],[DueDate]) VALUES ('{0}', {1}, '{2}')", Name, Amount, DueDate);
                    cmd.CommandText = strSql;
                    cmd.Connection = conn;
                    conn.Open();

                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }
    }
}