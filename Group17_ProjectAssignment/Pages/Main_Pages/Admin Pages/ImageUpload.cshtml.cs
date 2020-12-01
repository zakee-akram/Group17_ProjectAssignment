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
        public string UserName;
        public const string Session1 = "username";


        public string FirstName;
        public const string Session2 = "fname";

        public string sessionId;
        public const string Session3 = "sessionId";


        public string Role;
        public const string Session4 = "Role";


        public IActionResult OnGet()
        {
            UserName = HttpContext.Session.GetString(Session1);
            FirstName = HttpContext.Session.GetString(Session2);
            sessionId = HttpContext.Session.GetString(Session3);
            Role = HttpContext.Session.GetString(Session4);

            if (string.IsNullOrEmpty(UserName) | string.IsNullOrEmpty(FirstName) | string.IsNullOrEmpty(sessionId))
            {

                return RedirectToPage("/Main_Pages/User Pages/Login");
            }
            return Page();

        }

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


       
    }
}
