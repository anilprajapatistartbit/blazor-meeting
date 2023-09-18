using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace MeetingSchedulingApp.Pages
{
    public class LoginModel : PageModel
    {
        public string ReturnUrl { get; set; }
        public async Task<IActionResult> OnGetAsync(string email, string name, int id)
        {
            try
            {
                string returnUrl = Url.Content("~/");
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                var claims = new List<Claim>
                                {
                                    new Claim(ClaimTypes.Email,email),
                                    new Claim(ClaimTypes.Name,name),
                                    new Claim(ClaimTypes.UserData,id.ToString())
                                };
                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    RedirectUri = this.Request.Host.Value,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(2),
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
                return LocalRedirect(returnUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return LocalRedirect("/accountlogin");
            }

        }
    }
}
