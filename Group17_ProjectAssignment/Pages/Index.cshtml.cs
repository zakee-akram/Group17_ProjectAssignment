using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Group17_ProjectAssignment.Model;
using System.Data.SqlClient;


namespace Group17_ProjectAssignment.Pages
{
    public class IndexModel : PageModel
    {
        public List<ProductModel> Products;
        public List<StockModel> Stock;
        public List<ProductModel> TopProduct;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public int AmtProduct;
        public double AvgSalePrice;
        public double AvgPurchasePrice;
        public double AvgProfit;
        public string highest;
       // public int AmtProduct;
        //public double AvgSalePrice;
       // public double AvgPurchasePrice;
        public void OnGet()
        {
            DBString dB = new DBString();
            string ConnectionString = dB.ConString();
            SqlConnection conn = new SqlConnection(ConnectionString);

            conn.Open();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * FROM Products ";

                Products = new List<ProductModel>(); //this object of list is created to populate all records from the table
                SqlDataReader reader = command.ExecuteReader(); //SqlDataReader is used to read record from a table
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
                reader.Close();
            }


            SqlConnection connn = new SqlConnection(ConnectionString);
            connn.Open();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * from Stock ";


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

          //  connn.close();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * from Products ORDER By SalePrice ";


                SqlDataReader reader = command.ExecuteReader(); //SqlDataReader is used to read record from a table

                TopProduct = new List<ProductModel>(); //this object of list is created to populate all records from the table

                while (reader.Read())
                {
                    ProductModel record = new ProductModel(); //a local var to hold a record temporarily
                    record.SerialNumber = reader.GetString(0); //getting the first field from the table
                    record.SalePrice = reader.GetString(3); //getting the third field from the table
                    TopProduct.Add(record); //adding the single record into the list
                }

                // Call Close when done reading.
                reader.Close();

            }

            /////amm times sale.
            /////amd times purchase
            /////minus each other
            for (int i = 0; i<Stock.Count;i++) {
                ; //this object of list is created to populate all records from the table
                using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;            
                command.Parameters.AddWithValue("@SNum", Stock[i].SerialIdNumber);
                Console.WriteLine("@The SerialNumber " + Stock[i].SerialIdNumber);
                    //SqlDataReader reader = command.ExecuteReader(); //SqlDataReader is used to read record from a table
                    command.CommandText = @"SELECT Count (Amount) from Stock";
                    AmtProduct = (Int32)command.ExecuteScalar();
                    command.CommandText = @"SELECT AVG(CAST(SalePrice as FLOAT)) from Products";
                    AvgSalePrice= (double)command.ExecuteScalar();
                    command.CommandText = @"SELECT AVG(CAST(PurchasePrice as FLOAT)) from Stock";
                    AvgPurchasePrice =(double)command.ExecuteScalar();
                    AvgProfit =  AvgSalePrice - AvgPurchasePrice ;
                }


            }

    }
    }

}