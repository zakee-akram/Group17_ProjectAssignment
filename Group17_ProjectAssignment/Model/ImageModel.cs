using System.ComponentModel.DataAnnotations;

namespace Group17_ProjectAssignment.Model
{
    // Model for image data. 4 parameters.
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
