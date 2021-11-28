using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkelbimuSvetaine.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SkelbimuSvetaine.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ld1_gynimasContext _context;

        public HomeController(ILogger<HomeController> logger, ld1_gynimasContext context)
        {
            _logger = logger;
            _context = context;
        }
  

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles ="Admin")]
        public IActionResult Secured()
        {
            return View();
        }

        [HttpGet("login")]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Validate(string username, string password, string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            foreach (User a in _context.Users) // laikina, nes loopins per visus user
            {
                if (username == a.Username && password == a.Password)
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim("username", username));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, username));
                    claims.Add(new Claim(ClaimTypes.Name, username));
                    //claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    if (returnUrl == null)
                        return Redirect("/");
                    return Redirect(returnUrl);
                }
            }
            TempData["Error"] = "Klaida. Vartotojo vardas arba slaptažodis neteisingas";
            return View("login");
        }

        [Authorize]
        public async Task<IActionResult> Logout(string returnUrl)
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        [HttpGet("denied")]
        public IActionResult Denied()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
