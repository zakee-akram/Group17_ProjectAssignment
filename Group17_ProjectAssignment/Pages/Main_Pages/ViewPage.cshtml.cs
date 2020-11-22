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
    public class ViewPageModel : PageModel
    {
        public List<ProductModel> Products { get; set; }

        public void OnGet()
        {
            string dbConnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ComputerShop;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(dbConnection);
            conn.Open();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * from Products "; SqlDataReader reader = command.ExecuteReader(); //SqlDataReader is used to read record from a table

                Products = new List<ProductModel>(); //this object of list is created to populate all records from the table

                while (reader.Read())
                {
                    ProductModel record = new ProductModel(); //a local var to hold a record temporarily
                    record.SerialNumber = reader.GetString(0); //getting the first field from the table
                    record.Name = reader.GetString(1); //getting the second field from the table
                    record.Company = reader.GetString(2); //getting the third field from the table
                    record.SalePrice = reader.GetString(3);
                    record.Category = reader.GetString(4);

                    Products.Add(record); //adding the single record into the list
                }

                // Call Close when done reading.
                reader.Close();


            }
        }
    }
}
