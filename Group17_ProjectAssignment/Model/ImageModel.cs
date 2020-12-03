using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group17_ProjectAssignment.Model
{
    public class ImageModel
    {
        [Required]
        [Display(Name = "FileName")]
        public string FileName { get; set; }
        [Required]
        [Display(Name = "User")]
        public string User { get; set; }
        [Required]
        [Display(Name = "Product")]
        public string SerialNumber { get; set; }

    }
}
