using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Net6CoreMvcAuthorize.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login()
        {
            //kontrol edilecek verilerin kontrol edilmesi gerek

            List<string> roles = new List<string>();

            roles.Add("admin");
            roles.Add("ui");
            roles.Add("product.add");

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"Oğuzhan Küçükyamaç"),
            };

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties();

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            return RedirectToAction("Index","Home");
        }
    }
}
