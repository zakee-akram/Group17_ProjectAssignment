using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group17_ProjectAssignment.Model
{
    public class StockModel
    { //For Table Stock
        [Required]
        [Display(Name = "StockIdNumber")]
        public int StockIdNumber { get; set; }
        [Required]
        [Display(Name = "SerialIdNumber")]
        public string SerialIdNumber { get; set; }
        [Required]
        [Display(Name = "PurchasePrice")]
        public int PurchasePrice { get; set; }
        [Required]
        [Display(Name = "Amount")]
        public int Amount { get; set; }

    }
}
