using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Group17_ProjectAssignment.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        [BindProperty]
        public ImageModel ImgDetails { get; set; }
        public readonly IWebHostEnvironment _env;

        public string UserName;
        public const string Session1 = "username";


        public string FirstName;
        public const string Session2 = "fname";

        public string sessionId;
        public const string Session3 = "sessionId";


        public string Role;
        public const string Session4 = "Role";
  
        public Product_DeleteModel(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IActionResult OnGet(string? id)
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
            ImgDetails = new ImageModel();
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



            conn.Open();
            ImgDetails = new ImageModel();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "SELECT * FROM ImgData WHERE SerialNumber = @SNum";
                command.Parameters.AddWithValue("@SNum", id);
                Console.WriteLine("@The SerialNumber " + id);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ImgDetails.FileName = reader.GetString(2);
                    ImgDetails.User = reader.GetString(1);
                }
                Console.WriteLine("File name : " + ImgDetails.FileName);


            }

            return Page();
        }


        public IActionResult OnPost()
        {
            if (!string.IsNullOrEmpty(ImgDetails.FileName) && !string.IsNullOrEmpty(ImgDetails.User))
            {
                deletePicture(ImgDetails.User, ImgDetails.FileName);
            }
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
        public void deletePicture(string userr, string FileName)
        {
            Console.WriteLine("Record Id : " + userr);
            Console.WriteLine("File Name : " + FileName);
           
            DBString dB = new DBString();
            string ConnectionString = dB.ConString();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"DELETE FROM ImgData WHERE Username = @Id";
                command.Parameters.AddWithValue("@Id", userr);
                command.ExecuteNonQuery();
            }
            conn.Close();

            Console.WriteLine(FileName);
            string RetrieveImage = Path.Combine(_env.WebRootPath, "Files", FileName);
            System.IO.File.Delete(RetrieveImage);
            Console.WriteLine("File has been deleted");



        }
    }
}
