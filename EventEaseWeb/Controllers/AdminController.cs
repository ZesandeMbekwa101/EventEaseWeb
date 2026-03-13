using Microsoft.AspNetCore.Mvc;

namespace EventEaseWeb.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult Clients()
        {
            return View();
        }
        public IActionResult Venues()
        {
            return View();
        }
        public IActionResult Events()
        {
            return View();
        }
        public IActionResult Bookings()
        {
            return View();
        }
    }
}
