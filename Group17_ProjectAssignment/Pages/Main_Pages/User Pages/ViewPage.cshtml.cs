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
        public List<StockModel> Stock { get; set; }


        [BindProperty(SupportsGet = true)]
        public string Category { get; set; }
        [BindProperty(SupportsGet = true)]
        public string pricef { get; set; }
        public List<string> Categories { get; set; } = new List<string> { "GraphicsCard", "Cpu", "PowerSupply", "Motherboard", "Ram" };
        public List<string> pricefilter { get; set; } = new List<string> {"50","100","250","500","100" }; 

        public void OnGet()
        {

            DBString dB = new DBString();
            string ConnectionString = dB.ConString();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * From Products";
                if (!string.IsNullOrEmpty(Category))
                {
                    command.CommandText += " WHERE Category = @Cat";
                    command.Parameters.AddWithValue("@Cat", Convert.ToString(Category));
                }

                SqlDataReader reader = command.ExecuteReader(); //SqlDataReader is used to read record from a table
                command.CommandText = @"SELECT * FROM Products ";

                Products = new List<ProductModel>(); //this object of list is created to populate all records from the table

                while (reader.Read())
                {
                    ProductModel record = new ProductModel();
                    record.SerialNumber = reader.GetString(0); 
                    record.Name = reader.GetString(1); 
                    record.Company = reader.GetString(2); 
                    record.SalePrice = reader.GetString(3);
                    record.Category = reader.GetString(4);

                    Products.Add(record); //adding the single record into the list
                }
                reader.Close();
            }

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * From Products";
                if (!string.IsNullOrEmpty(pricef))
                {
                    command.CommandText += " WHERE SalePrice BETWEEN 0 AND " + pricef;
                    command.Parameters.AddWithValue("@SPri", Convert.ToString(pricef));
                }

                SqlDataReader reader = command.ExecuteReader(); //SqlDataReader is used to read record from a table
                command.CommandText = @"SELECT * FROM Products ";

                Products = new List<ProductModel>(); //this object of list is created to populate all records from the table

                while (reader.Read())
                {
                    ProductModel record = new ProductModel();
                    record.SerialNumber = reader.GetString(0);
                    record.Name = reader.GetString(1);
                    record.Company = reader.GetString(2);
                    record.SalePrice = reader.GetString(3);
                    record.Category = reader.GetString(4);

                    Products.Add(record); //adding the single record into the list
                }
                reader.Close();
            }

            SqlConnection connn = new SqlConnection(ConnectionString);
            connn.Open();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * from Stock ORDER BY SerialNumber ";


                SqlDataReader reader = command.ExecuteReader(); //SqlDataReader is used to read record from a table

                Stock = new List<StockModel>(); //this object of list is created to populate all records from the table

                while (reader.Read())
                {
                    StockModel record = new StockModel(); //a local var to hold a record temporarily
                    record.StockIdNumber = reader.GetInt32(0); //getting the first field from the table
                    record.SerialIdNumber = reader.GetString(1); //getting the second field from the table
                    record.PurchasePrice = reader.GetInt32(2); //getting the third field from the table
                    record.Amount = reader.GetInt32(3);

                    Stock.Add(record); //adding the single record into the list
                }

                // Call Close when done reading.
                reader.Close();

            }
        }
    }


}

