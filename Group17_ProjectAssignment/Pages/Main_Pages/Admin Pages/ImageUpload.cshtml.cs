using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Group17_ProjectAssignment.Model;
using System.IO;

namespace Group17_ProjectAssignment.Pages.Main_Pages.Admin_Pages
{
    public class ImageUploadModel : PageModel
    {


        [BindProperty]
        public IFormFile imgUpload { get; set; }

        public readonly IWebHostEnvironment webEnv;


        public ImageUploadModel(IWebHostEnvironment env)
        {
            webEnv = env;
        }


        public IActionResult OnPost()
        {
            var FileToUpload = Path.Combine(webEnv.WebRootPath, "Files", imgUpload.FileName);//this variable consists of file path
            Console.WriteLine("File Name : " + FileToUpload);

            using (var FStream = new FileStream(FileToUpload, FileMode.Create))
            {
                imgUpload.CopyTo(FStream);//copy the file into FStream variable
            }

            return RedirectToPage("/index");
        }


        public void OnGet()
        {

        }
    }
}
