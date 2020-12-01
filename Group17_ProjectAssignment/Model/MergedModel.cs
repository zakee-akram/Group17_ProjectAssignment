using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group17_ProjectAssignment.Model
{
    public class MergedModel
    {  //For Table Registration
        [Required]
        [Display(Name = "SerialNumber")]
        public string SerialNumber { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Company")]
        public string Company { get; set; }
        [Required]
        [Display(Name = "SalePrice")]
        public string SalePrice { get; set; }
        [Required]
        [Display(Name = "Category")]
        public string Category { get; set; }
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
