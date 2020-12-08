using Group17_ProjectAssignment.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;

namespace Group17_ProjectAssignment.Pages.Main_Pages
{
    public class CreateAccountModel : PageModel
    {
        [BindProperty]
        //links variable to models
        public UsersModel Users { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            DBString dB = new DBString();
            string ConnectionString = dB.ConString();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"INSERT INTO UserTable (UserName, FirstName, SecondName, Password, Role) VALUES (@UNam, @FNam, @SNam, @Pass, @Role)";
                command.Parameters.AddWithValue("@UNam", Users.UserName);
                command.Parameters.AddWithValue("@FNam", Users.FirstName);
                command.Parameters.AddWithValue("@SNam", Users.SecondName);
                command.Parameters.AddWithValue("@Pass", Users.Password);
                command.Parameters.AddWithValue("@Role", Users.Role);
                Console.WriteLine(Users.FirstName);
                Console.WriteLine(Users.SecondName);
                Console.WriteLine(Users.Password);
                Console.WriteLine(Users.Role);
                command.ExecuteNonQuery();

            }
            return RedirectToPage("/Index");
        }
    }

}
