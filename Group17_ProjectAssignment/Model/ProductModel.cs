using System.ComponentModel.DataAnnotations;

namespace Group17_ProjectAssignment.Model
{   //For registrating products. 
    public class ProductModel
    {
        [Required]
        [Display(Name = "SerialNumber")]
        [RegularExpression(@"^[0-9]*$")]
        public string SerialNumber { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Company")]
        public string Company { get; set; }
        [Required]
        [Display(Name = "SalePrice")]
        [RegularExpression(@"^[0-9]*$")]
        public string SalePrice { get; set; }
        [Required]
        [Display(Name = "Category")]
        public string Category { get; set; }

    }
}
