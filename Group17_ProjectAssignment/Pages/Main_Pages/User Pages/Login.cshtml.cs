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
    public class LoginModel : PageModel
    {
        [BindProperty]
        public UsersModel user { get; set; }
        public string message { get; set; }
        public string sessionId { get; set; }
     
     
    public IActionResult OnPost()
        {    
            DBString dB = new DBString();
            string ConnectionString = dB.ConString();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            Console.WriteLine(user.FirstName);
            Console.WriteLine(user.SecondName);
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT UserName, FirstName, Role FROM UserTable WHERE UserName = @UNam AND Password = @Pass";
                command.Parameters.AddWithValue("@UNam", user.UserName);
                command.Parameters.AddWithValue("@Pass", user.Password);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    user.FirstName = reader.GetString(0);
                    user.SecondName = reader.GetString(1);
                    user.Role = reader.GetString(2);
                }
            }

            if (!string.IsNullOrEmpty(user.FirstName))
            {
                sessionId = HttpContext.Session.Id;
                HttpContext.Session.SetString("sessionId", sessionId);
                HttpContext.Session.SetString("username", user.FirstName);
                HttpContext.Session.SetString("fname", user.FirstName);
                HttpContext.Session.SetString("Role", user.Role);

                if (user.Role == "Employee")
                {
                    return RedirectToPage("/Main_Pages/User Pages/WelcomeUser");
                }
                else
                {
                    return RedirectToPage("/Main_Pages/Admin Pages/WelcomeAdmin");
                }


            }
            else
            {
                message = "Invalid Username and Password!";
                return Page();
            }


        }

    }
}
