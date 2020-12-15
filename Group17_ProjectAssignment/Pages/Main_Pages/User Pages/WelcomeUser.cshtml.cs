using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group17_ProjectAssignment.Pages.Main_Pages
{
    public class WelcomeUserModel : PageModel
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
            if (string.IsNullOrEmpty(UserName) && string.IsNullOrEmpty(FirstName) && string.IsNullOrEmpty(sessionId))
            {
                return RedirectToPage("/Main_Pages/User Pages/Login");
            }

            if (Role == "Admin")
            {
                return RedirectToPage("/Main_Pages/Admin Pages/WelcomeAdmin");
            }
            return Page();

        }

    }
}