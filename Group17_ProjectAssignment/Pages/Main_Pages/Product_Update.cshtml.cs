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
    public class Product_UpdateModel : PageModel
    {
        [BindProperty]
        public ProductModel Product { get; set; }
        public IActionResult OnGet(int? id)
        {
            string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ComputerShop;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

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
                    Product.SerialNumber = reader.GetString(0); //getting the first field from the table
                    Product.Name = reader.GetString(1); //getting the second field from the table
                    Product.Company = reader.GetString(2); //getting the third field from the table
                    Product.SalePrice = reader.GetString(3);
                    Product.Category = reader.GetString(4);

                }


            }

            conn.Close();

            return Page();

        }
        public IActionResult OnPost()
        {
            DBString dB = new DBString();
            string ConnectionString = dB.ConString();
            //string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ComputerShop;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();

            Console.WriteLine("Product SerialNumber : " + Product.SerialNumber);
            Console.WriteLine("Product Name : " + Product.Name);
            Console.WriteLine("Product Company : " + Product.Company);
            Console.WriteLine("Product SalePrice : " + Product.SalePrice);
            Console.WriteLine("Product Category : " + Product.Category);


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

            conn.Close();

            return RedirectToPage("/Index");
        }



       
    }
}
