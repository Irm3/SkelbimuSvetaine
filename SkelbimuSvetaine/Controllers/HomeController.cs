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
using SkelbimuSvetaine.Security;

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

            var a = _context.Users.SingleOrDefault(x => x.Username.Equals(username));

            if (a != null)
            {
                bool validate = Hashing.Validate(password, a.Password);
                if (validate == true)
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
            
            TempData["Error"] = "Klaida. Vartotojo vardas arba slaptažodis neteisingas.";
            return View("login");
        }

        [HttpPost("register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Username, Password, Phone, Email, Icon, Miestas")] User user)
        {         
            try
            {
                if (_context.Users.Any(x => x.Username == user.Username))
                {
                    TempData["Error"] = "Klaida. Toks vartotojo vardas jau naudojamas.";
                    return View("register");
                }

                if (_context.Users.Any(x => x.Email == user.Email))
                {
                    TempData["Error"] = "Klaida. Toks el. paštas jau naudojamas.";
                    return View("register");
                }

                if (ModelState.IsValid)
                {
                    user.Password = Hashing.Hash(user.Password);
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                ModelState.AddModelError("", "Nepavyko išsaugoti pakeitimų, jei vis dar kyla problema susisiekite su administratoriumi");
            }
            return View(User);

        }

        [HttpGet("register")]
        public ActionResult Register()
        {
            User user = new User();
            return View(user);
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
