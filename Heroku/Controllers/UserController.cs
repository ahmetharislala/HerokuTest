using Heroku.Context;
using Heroku.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Heroku.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public  IActionResult Index()
        {
            bool anyuser= _context.Users.Any(x=>x.UserName=="harisbey");
            if (!anyuser)
            {
                _context.Users.Add(new User { UserName="harisbey",Password="Gencfenerli21"});
                _context.SaveChanges();
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(User request)
        {
            User user =_context.Users.FirstOrDefault(x=>x.UserName== request.UserName&&x.Password== request.Password);
            if (user==null) 
            {
                return Json(new { success = false, message = "Kullanıcı adı veya şifre hatalıdır." });

            }
            #region Authentication


            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var claimsIdentity = new ClaimsIdentity(claims, "login");

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            #endregion

            return Json(new { success = true, message = "Giriş başarılı!" });
        }
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }
    }
}
