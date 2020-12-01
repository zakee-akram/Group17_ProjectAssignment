using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group17_ProjectAssignment.Model
{
    public class ProductModel
    {
        //For Table Registration
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
       
    }
}
