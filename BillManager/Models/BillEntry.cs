using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

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

        public static IEnumerable<BillEntry> GetBillEntries()
        {
            var billsList = new List<BillEntry>();

            using (SQLiteConnection conn = new SQLiteConnection("Data Source =" + HttpContext.Current.Server.MapPath("~/") + "DB/Bills.db; Version = 3; New = false;"))
            {
                conn.Open();

                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    string strSql = String.Format("SELECT * FROM Bills;");
                    cmd.CommandText = strSql;

                    SQLiteDataReader r = cmd.ExecuteReader();

                    while (r.Read())
                    {
                        billsList.Add(new BillEntry() { Name = Convert.ToString(r["Name"]), Amount = Convert.ToDouble(r["Amount"]), DueDate = Convert.ToDateTime(r["DueDate"]) });
                    }
                }
            }

            return billsList;
        }

        public static void SendMessage(string Message)
        {
            string accountSid = "AC53037ad8d67dea41e4bccc8ec8fd140d";
            string authToken = "72bc0e277f46f5735fc71e978c4a39bb";

            TwilioClient.Init(accountSid, authToken);

            MessageResource.Create(
                body: Message,
                from: new Twilio.Types.PhoneNumber("+19313454922"),
                to: new Twilio.Types.PhoneNumber("+18067771059")
            );

            MessageResource.Create(
                body: Message,
                from: new Twilio.Types.PhoneNumber("+19313454922"),
                to: new Twilio.Types.PhoneNumber("+19089148226")
            );
        }
    }
}