using Group17_ProjectAssignment.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;
using System.IO;

namespace Group17_ProjectAssignment.Pages.Main_Pages.Admin_Pages
{
    public class ImageUploadModel : PageModel
    {
        //Delcaring variables for page. Referencing models. Session parameters to verify access.
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
        //On page load with id which is serial number from the page it was called from. 
        public IActionResult OnGet(int? id)
        {
            //Get session variables. 
            UserName = HttpContext.Session.GetString(Session1);
            FirstName = HttpContext.Session.GetString(Session2);
            sessionId = HttpContext.Session.GetString(Session3);
            Role = HttpContext.Session.GetString(Session4);
            //Redirect to login page if nobody is logged in. 
            if (string.IsNullOrEmpty(UserName) | string.IsNullOrEmpty(FirstName) | string.IsNullOrEmpty(sessionId))
            {
                return RedirectToPage("/Main_Pages/User Pages/Login");
            }
            //Gets the conenction string from the other class.
            DBString dB = new DBString();
            string ConnectionString = dB.ConString();
            SqlConnection conn = new SqlConnection(ConnectionString);
            //Opens up a sql connection. 
            conn.Open();
            //Loads Product onto page where serial number matches id. Gets only serialnumber and name. 
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * from Products WHERE SerialNumber =@SNum";

                command.Parameters.AddWithValue("@SNum", id);
                Console.WriteLine("@The SerialNumber " + id);
                //New List to store variables. 
                Products = new ProductModel();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Products.SerialNumber = reader.GetString(0);
                    Products.Name = reader.GetString(1);
                }
                reader.Close();
            }
            //load stock onto page too. Gets idnumber and amount. 
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
        //Image upload stuff.
        [BindProperty]
        public IFormFile imgUpload { get; set; }
        public readonly IWebHostEnvironment webEnv;
        public ImageUploadModel(IWebHostEnvironment env)
        {
            webEnv = env;
        }
        //Once button is cliocked onpost runs with id. 
        public IActionResult OnPost(int? id)
        {
            //creates the file path.
            var FileToUpload = Path.Combine(webEnv.WebRootPath, "Files", imgUpload.FileName);
            Console.WriteLine("File Name : " + FileToUpload);
            //Coppys path to fstream which uploads to locaiton. 
            using (var FStream = new FileStream(FileToUpload, FileMode.Create))
            {
                imgUpload.CopyTo(FStream);//copy the file into FStream variable
            }
            //gets username to upload into db. 
            UserName = HttpContext.Session.GetString(Session1);
            DBString dB = new DBString();
            string ConnectionString = dB.ConString();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            //Insert serialnumber filepath and username into database imgUpload. 
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
            //go back to homepage. 
            return RedirectToPage("/index");
        }
    }
}
