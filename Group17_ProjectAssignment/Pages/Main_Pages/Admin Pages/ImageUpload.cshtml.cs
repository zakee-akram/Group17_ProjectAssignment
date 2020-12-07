using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Group17_ProjectAssignment.Model;
using System.IO;
using System.Data.SqlClient;

namespace Group17_ProjectAssignment.Pages.Main_Pages.Admin_Pages
{
    public class ImageUploadModel : PageModel
    {
        [BindProperty]
        public ImageModel imgDetail { get; set; }
        [BindProperty]

        public ProductModel Products { get; set; }
        public StockModel Stock { get; set; }


        public string UserName;
        public const string Session1 = "username";


        public string FirstName;
        public const string Session2 = "fname";

        public string sessionId;
        public const string Session3 = "sessionId";


        public string Role;
        public const string Session4 = "Role";

        protected void btnInsert(object sender, EventArgs e)
        {

        }

        public IActionResult OnGet(int? id)
        {
         
            UserName = HttpContext.Session.GetString(Session1);
            FirstName = HttpContext.Session.GetString(Session2);
            sessionId = HttpContext.Session.GetString(Session3);
            Role = HttpContext.Session.GetString(Session4);

            if (string.IsNullOrEmpty(UserName) | string.IsNullOrEmpty(FirstName) | string.IsNullOrEmpty(sessionId))
            {

                return RedirectToPage("/Main_Pages/User Pages/Login");
            }


            DBString dB = new DBString();
            string ConnectionString = dB.ConString();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * from Products WHERE SerialNumber =@SNum";

                command.Parameters.AddWithValue("@SNum", id);
                Console.WriteLine("@The SerialNumber " + id);

                Products = new ProductModel();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Products.SerialNumber = reader.GetString(0);
                    Products.Name = reader.GetString(1);
            
                }
                reader.Close();
            }

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * from Stock WHERE SerialNumber =@SNum";
                command.Parameters.AddWithValue("@SNum", id);
                Console.WriteLine("@The SerialNumber " + id);
                Stock = new StockModel();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Stock.StockIdNumber = reader.GetInt32(0);
                    Stock.Amount = reader.GetInt32(3);
                }
                reader.Close();
            }
            return Page();
   
        }



        [BindProperty]
        public IFormFile imgUpload { get; set; }

        public readonly IWebHostEnvironment webEnv;


        public ImageUploadModel(IWebHostEnvironment env)
        {
            webEnv = env;
        }


        public IActionResult OnPost(int? id)

        {


            var FileToUpload = Path.Combine(webEnv.WebRootPath, "Files", imgUpload.FileName);//this variable consists of file path
            Console.WriteLine("File Name : " + FileToUpload);

            using (var FStream = new FileStream(FileToUpload, FileMode.Create))
            {
                imgUpload.CopyTo(FStream);//copy the file into FStream variable
            }
            UserName = HttpContext.Session.GetString(Session1);


            DBString dB = new DBString();
            string ConnectionString = dB.ConString();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"INSERT INTO ImgData (SerialNumber, FileName, Username) VALUES (@SNum,@Fnam,@Unam)";
                command.Parameters.AddWithValue("@SNum", id);
                command.Parameters.AddWithValue("@Fnam", UserName);
                command.Parameters.AddWithValue("@Unam", imgUpload.FileName);
                Console.WriteLine(Products);
                Console.WriteLine(FileToUpload);
                Console.WriteLine(UserName);
                command.ExecuteNonQuery();
            }
                return RedirectToPage("/index");
        }


       
    }
}
