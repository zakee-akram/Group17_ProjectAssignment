using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group17_ProjectAssignment.Pages.Main_Pages
{
    public class LogotModel : PageModel
    {
        public void OnGet()
        {
            HttpContext.Session.Clear();
        }
    }
}
