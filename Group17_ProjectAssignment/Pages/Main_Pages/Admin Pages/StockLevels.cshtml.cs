
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Group17_ProjectAssignment.Model;
using System.Data.SqlClient;

namespace Group17_ProjectAssignment.Pages.Main_Pages.Admin_Pages
{
    public class StockLevelsModel : PageModel
    {
        public List<StockModel> Stock { get; set; }

        public void OnGet()
        {

            DBString dB = new DBString();
            string ConnectionString = dB.ConString();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * from Stock ";
                

                SqlDataReader reader = command.ExecuteReader(); //SqlDataReader is used to read record from a table

                Stock = new List<StockModel>(); //this object of list is created to populate all records from the table

                while (reader.Read())
                {
                    StockModel record = new StockModel(); //a local var to hold a record temporarily
                    record.StockIdNumber = reader.GetString(0); //getting the first field from the table
                    record.SerialIdNumber = reader.GetString(1); //getting the second field from the table
                    record.PurchasePrice = reader.GetString(2); //getting the third field from the table
                    record.Amount = reader.GetInt32(3);

                    Stock.Add(record); //adding the single record into the list
                }

                // Call Close when done reading.
                reader.Close();


            }
        }
     
    }
}


