using EventEaseWeb.Data;
using EventEaseWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventEaseWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(UserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            UserModel user = new UserModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                Role = model.Role
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            TempData["Success"] = "Account created successfully!";
            return RedirectToAction("Login");
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                TempData["Error"] = "Please enter both email and password";
                return View();
            }

            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

                if (user != null)
                {
                    HttpContext.Session.SetString("UserId", user.UserID.ToString());
                    HttpContext.Session.SetString("UserName", $"{user.FirstName} {user.LastName}");
                    HttpContext.Session.SetString("UserRole", "Booking Specialist");

                    TempData["Success"] = $"Welcome, {user.FirstName} {user.LastName}!";
                    return RedirectToAction("Dashboard", "Admin"); // redirect immediately
                }

                TempData["Error"] = "Invalid email or password";
                return View();
            }
            catch
            {
                TempData["Error"] = "Login failed. Please try again.";
                return View();
            }
        }
        // GET: Logout
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["LogoutMessage"] = "You have been logged out successfully";
            return RedirectToAction("Dashboard", "Admin");
        }
    }
}
