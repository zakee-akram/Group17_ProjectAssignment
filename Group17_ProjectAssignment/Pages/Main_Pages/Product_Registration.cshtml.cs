using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Group17_ProjectAssignment.Model;
using System.Data.SqlClient;

namespace Group17_ProjectAssignment.Pages.Main_Pages
{
    public class Product_RegistrationModel : PageModel
    {
        [BindProperty]
        //links variable to models
        public ProductModel Products { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            string dbConnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ComputerShop;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(dbConnection);
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
                Console.WriteLine(Products.SalePrice);
                Console.WriteLine(Products.Category);
                command.ExecuteNonQuery();

            }
            return RedirectToPage("/Index");
        }
    }
}
