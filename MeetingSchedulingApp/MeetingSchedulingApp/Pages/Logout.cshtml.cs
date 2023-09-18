using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MeetingSchedulingApp.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync(string returnUrl)
        {
            await HttpContext
                .SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            //if(returnUrl != null) return LocalRedirect(Url.Content("/Accountlogin"));
            return LocalRedirect(Url.Content(returnUrl));
        }
    }
}
