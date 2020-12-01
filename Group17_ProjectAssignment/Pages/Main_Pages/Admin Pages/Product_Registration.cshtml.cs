using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Group17_ProjectAssignment.Model;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace Group17_ProjectAssignment.Pages.Main_Pages
{
    public class Product_RegistrationModel : PageModel
    {
        
        [BindProperty]
        //links variable to models
        public ProductModel Products { get; set; }
        [BindProperty]

        public StockModel Stock { get; set; }
      
        public string UserName;
        public const string Session1 = "username";


        public string FirstName;
        public const string Session2 = "fname";

        public string sessionId;
        public const string Session3 = "sessionId";


        public string Role;
        public const string Session4 = "Role";


        public IActionResult OnGet()
        {
            UserName = HttpContext.Session.GetString(Session1);
            FirstName = HttpContext.Session.GetString(Session2);
            sessionId = HttpContext.Session.GetString(Session3);
            Role = HttpContext.Session.GetString(Session4);

            if (string.IsNullOrEmpty(UserName) | string.IsNullOrEmpty(FirstName)| string.IsNullOrEmpty(sessionId) | !(Role == "Admin"))
            {
               
                return RedirectToPage("/Main_Pages/User Pages/Login");
            }
            return Page();

        }

        public IActionResult OnPost()
        {
          

            DBString dB = new DBString();
            string ConnectionString = dB.ConString();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"INSERT INTO Products (SerialNumber, Name, Company, SalePrice, Category) VALUES (@SNum,@Name,@Con,@SPri,@Cat)";
                command.Parameters.AddWithValue("@SNum", Products.SerialNumber);
                command.Parameters.AddWithValue("@Name", Products.Name);
                command.Parameters.AddWithValue("@Con", Products.Company);
                command.Parameters.AddWithValue("@SPri", Products.SalePrice);
                command.Parameters.AddWithValue("@Cat", Products.Category);
                Console.WriteLine(Products.SerialNumber);
                Console.WriteLine(Products.Name);
                Console.WriteLine(Products.Company);
                Console.WriteLine(Products.Category);
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                command.CommandText = @"INSERT INTO Stock (StockIdNumber, SerialNumber, PurchasePrice, Amount) VALUES (@Sidn,@Sedn,@Ppri,@Amnt)";
                command.Parameters.AddWithValue("@Sidn", Stock.StockIdNumber);
                command.Parameters.AddWithValue("@Sedn", Products.SerialNumber);
                command.Parameters.AddWithValue("@Ppri", Stock.PurchasePrice);
                command.Parameters.AddWithValue("@Amnt", Stock.Amount);
                Console.WriteLine(Stock.StockIdNumber);
                Console.WriteLine(Products.SerialNumber);
                Console.WriteLine(Stock.PurchasePrice);
                Console.WriteLine(Stock.Amount);
                command.ExecuteNonQuery();

            }
            return RedirectToPage("/Index");
        }
    }
}
