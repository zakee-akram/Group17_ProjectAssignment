using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Group17_ProjectAssignment.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace Group17_ProjectAssignment.Pages.Main_Pages
{
    public class Product_DeleteModel : PageModel
    {
        [BindProperty]
        public ProductModel Product { get; set; }
        [BindProperty]
        public StockModel Stock { get; set; }
        public IActionResult OnGet(string? id)
        {
            
            DBString dB = new DBString();
            string ConnectionString = dB.ConString();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            Product = new ProductModel();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "SELECT * FROM Products WHERE SerialNumber = @SNum";
                command.Parameters.AddWithValue("@SNum", id);
                Console.WriteLine("@The SerialNumber " + id);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Product.SerialNumber = reader.GetString(0); 
                    Product.Name = reader.GetString(1); 
                    Product.Company = reader.GetString(2); 
                    Product.SalePrice = reader.GetString(3);
                    Product.Category = reader.GetString(4);
                }
            }
            conn.Close();
            conn.Open();
            Stock = new StockModel();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "SELECT * FROM Stock WHERE SerialNumber = @SNum";
                command.Parameters.AddWithValue("@SNum", id);
                Console.WriteLine("@The SerialNumber " + id);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Stock.StockIdNumber = reader.GetInt32(0);
                    Stock.SerialIdNumber = reader.GetString(1);
                    Stock.PurchasePrice = reader.GetInt32(2);
                    Stock.Amount = reader.GetInt32(3);
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
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "DELETE Stock WHERE SerialNumber = @SNum";
                command.Parameters.AddWithValue("@SNum", Product.SerialNumber);
                command.ExecuteNonQuery();
                command.CommandText = "DELETE Products WHERE SerialNumber = @SNum";
                command.ExecuteNonQuery();
            }
            conn.Close();
            return RedirectToPage("/Index");
        }
    }
}
