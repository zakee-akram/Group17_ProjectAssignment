using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Group17_ProjectAssignment.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group17_ProjectAssignment.Pages.Main_Pages.User_Pages
{
    public class ViewImageModel : PageModel
    {
        public List<ImageModel> ImageRecord { get; set; }

        public void OnGet()
        {
            DBString dB = new DBString();
            string ConnectionString = dB.ConString();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * from ImgData ";


                SqlDataReader reader = command.ExecuteReader(); //SqlDataReader is used to read record from a table

                ImageRecord = new List<ImageModel>(); //this object of list is created to populate all records from the table

                while (reader.Read())
                {
                    ImageModel record = new ImageModel(); //a local var to hold a record temporarily
                    record.SerialNumber = reader.GetString(0); //getting the first field from the table
                    record.User = reader.GetString(1); //getting the third field from the table
                    record.FileName = reader.GetString(2); //getting the second field from the table
                    ImageRecord.Add(record); //adding the single record into the list
                }

                // Call Close when done reading.
                reader.Close();

            }
        }
    }
}
