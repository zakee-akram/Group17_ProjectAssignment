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
    public class Product_UpdateModel : PageModel
    {
        [BindProperty]
        public ProductModel Product { get; set; }
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



        public IActionResult OnGet(int? id)
        {

            UserName = HttpContext.Session.GetString(Session1);
            FirstName = HttpContext.Session.GetString(Session2);
            sessionId = HttpContext.Session.GetString(Session3);
            Role = HttpContext.Session.GetString(Session4);

            if (string.IsNullOrEmpty(UserName) | string.IsNullOrEmpty(FirstName) | string.IsNullOrEmpty(sessionId) | !(Role == "Admin"))
            {

                return RedirectToPage("/Main_Pages/User Pages/Login");
            }
         

            DBString dB = new DBString();
            string ConnectionString = dB.ConString();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();


            Product = new ProductModel();
            Stock = new StockModel();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "SELECT * FROM Products WHERE SerialNumber = @SNum";

                command.Parameters.AddWithValue("@SNum", id);
                Console.WriteLine("@The SerialNumber " + id);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Product.SerialNumber = reader.GetString(0); //getting the first field from the table
                    Product.Name = reader.GetString(1); //getting the second field from the table
                    Product.Company = reader.GetString(2); //getting the third field from the table
                    Product.SalePrice = reader.GetString(3);
                    Product.Category = reader.GetString(4);
                }
                reader.Close();
            }

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "SELECT * FROM Stock WHERE SerialNumber = @SNum";
                command.Parameters.AddWithValue("@SNum", id);
                Console.WriteLine("@The SerialNumber " + id);

                SqlDataReader readerr = command.ExecuteReader();

                while (readerr.Read())
                {
                    Stock.StockIdNumber = readerr.GetInt32(0); //getting the first field from the table
                    Stock.SerialIdNumber = readerr.GetString(1); //getting the second field from the table
                    Stock.PurchasePrice = readerr.GetInt32(2); //getting the third field from the table
                    Stock.Amount = readerr.GetInt32(3);
                }
            }
            conn.Close();
            return Page();

        }
        public IActionResult OnPost()
        {
            DBString dB = new DBString();
            string ConnectionString = dB.ConString();

            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();

            Console.WriteLine("Product SerialNumber : " + Product.SerialNumber);
            Console.WriteLine("Product Name : " + Product.Name);
            Console.WriteLine("Product Company : " + Product.Company);
            Console.WriteLine("Product SalePrice : " + Product.SalePrice);
            Console.WriteLine("Product Category : " + Product.Category);
            Console.WriteLine("Stock Id Number :" + Stock.StockIdNumber);
            Console.WriteLine("Purchase Price :" + Stock.PurchasePrice);
            Console.WriteLine("Amount :" + Stock.Amount);
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "UPDATE Products SET Name = @Name, Company = @Com, SalePrice = @SPri, Category = @Cat WHERE SerialNumber = @SNum";

                command.Parameters.AddWithValue("@SNum", Product.SerialNumber);
                command.Parameters.AddWithValue("@Name", Product.Name);
                command.Parameters.AddWithValue("@Com", Product.Company);
                command.Parameters.AddWithValue("@SPri", Product.SalePrice);
                command.Parameters.AddWithValue("@Cat", Product.Category);

                command.ExecuteNonQuery();
            }
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "UPDATE Stock SET PurchasePrice = @Pri, Amount = @Amn, StockIdNumber = @Sin WHERE SerialNumber = @Snum";
                command.Parameters.AddWithValue("@Pri", Stock.PurchasePrice);
                command.Parameters.AddWithValue("@Amn", Stock.Amount);
                command.Parameters.AddWithValue("@Sin", Stock.StockIdNumber);
                command.Parameters.AddWithValue("@Snum", Product.SerialNumber);
                command.ExecuteNonQuery();
                conn.Close();

            }
            return RedirectToPage("/Index");




        }
 
        }
    }

