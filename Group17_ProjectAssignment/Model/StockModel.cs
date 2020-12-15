using System.ComponentModel.DataAnnotations;

namespace Group17_ProjectAssignment.Model
{
    //For Stock Managment.
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
