using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group17_ProjectAssignment.Model
{
    public class ProductModel
    {
        //For Table Registration
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string SalePrice { get; set; }
        public string Category { get; set; }
        //For Table Stock
        public string StockIdNumber { get; set; }
        public string SerialIdNumber { get; set; }
        public string PurchasePrice { get; set; }
        public string Amount { get; set; }
    }
}
