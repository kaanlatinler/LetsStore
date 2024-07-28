using AutoMapper;
using LetsStore.DTO;
using LetsStore.Models;
using LetsStore.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LetsStore.Controllers
{
    public class AuthController : Controller
    {
        private readonly LetsStoreContext _context;
        private readonly IMapper _mapper;

        public AuthController(LetsStoreContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.UserEmail == model.UserEmail && u.UserPassword == model.UserPassword);

                if (user == null)
                {
                    return NotFound("Wrong Password or Email!!");
                }

                List<Claim> claims = new List<Claim>() {
                        new Claim(ClaimTypes.NameIdentifier, model.UserEmail),
                        new Claim(ClaimTypes.Role, "User"),
                        new Claim(ClaimTypes.Name, user.UserId.ToString())

                    };

                ClaimsIdentity CI = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties prop = new AuthenticationProperties()
                {
                    AllowRefresh = true
                };

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(CI), prop);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                return View();
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserDTO model)
        {
            try
            {
                Guid userID = Guid.NewGuid();

                var newUser = _mapper.Map<User>(model);
                newUser.UserId = userID;
                _context.Users.Add(newUser);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                return View();
            }
        }
    }
}
