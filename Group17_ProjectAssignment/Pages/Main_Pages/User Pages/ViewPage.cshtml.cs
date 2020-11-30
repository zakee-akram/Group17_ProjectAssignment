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
        
        [BindProperty(SupportsGet = true)]
        public string Category { get; set; }
        public List<string> Categories { get; set; } = new List<string> { "GraphicsCard","Cpu","PowerSupply","Motherboard","Ram" };


        public void OnGet()
        {

            DBString dB = new DBString();
            string ConnectionString = dB.ConString();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * from Products ";
                if (!string.IsNullOrEmpty(Category))
                {
                    command.CommandText += " WHERE Category = @Cat";
                    command.Parameters.AddWithValue("@Cat", Convert.ToString(Category));
                }

                SqlDataReader reader = command.ExecuteReader(); //SqlDataReader is used to read record from a table

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
