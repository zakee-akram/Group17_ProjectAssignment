using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group17_ProjectAssignment.Pages
{
    public class WelcomeAdminModel : PageModel
    {


        public string UserName;
        public const string Session1 = "username";


        public string FirstName;
        public const string Session2 = "fname";

        public string sessionId;
        public const string Session3 = "sessionId";

        public IActionResult OnGet()
        {
            UserName = HttpContext.Session.GetString(Session1);
            FirstName = HttpContext.Session.GetString(Session2);
            sessionId = HttpContext.Session.GetString(Session3);

            if (string.IsNullOrEmpty(UserName) && string.IsNullOrEmpty(FirstName) && string.IsNullOrEmpty(sessionId))
            {
                return RedirectToPage("/Main_Pages/User Pages/Login");
            }
            return Page();

        }


      
    }
}
