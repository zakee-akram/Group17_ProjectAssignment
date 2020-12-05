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
        [RegularExpression(@"^[0-9]*$")]
        public int StockIdNumber { get; set; }
        //[Display(Name = "SerialIdNumber")]
//[RegularExpression(@"^[0-9]*$")]
        public string SerialIdNumber { get; set; }
        [Required]
        [Display(Name = "PurchasePrice")]
        [RegularExpression(@"^[0-9]*$")]
        public int PurchasePrice { get; set; }
        [Required]
        [Display(Name = "Amount")]
        [RegularExpression(@"^[0-9]*$")]
        public int Amount { get; set; }

    }
}
