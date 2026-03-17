using EventEaseWeb.Data;
using EventEaseWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EventEaseWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Dashboard()
        {
            // Count records for dashboard cards
            ViewBag.TotalBookings = _context.Bookings.Count();
            ViewBag.TotalVenues = _context.Venues.Count();
            ViewBag.TotalEvents = _context.Events.Count();
            ViewBag.TotalCustomers = _context.Users.Count();

            // Capture TempData for display once
            ViewBag.LoginMessage = TempData["Success"];
            TempData.Remove("Success"); // remove it immediately

            return View();
        }
        // GET: Admin/AddClient
        public IActionResult AddClient()
        {
            var newClient = new UserModel
            {
                Role = "Client" // Default role set to Client
            };
            return View(newClient);
        }

        // POST: Admin/AddClient
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddClient(UserModel model)
        {
            if (ModelState.IsValid)
            {
                model.Role = "Client"; // Ensure role is always Client
                _context.Users.Add(model);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Client added successfully!";
                return RedirectToAction("Clients");
            }

            TempData["Error"] = "Failed to add client. Please check the form.";
            return View(model);
        }
        // Fetch clients from DB
        public async Task<IActionResult> Clients()
        {
            var clients = await _context.Users
                .OrderBy(c => c.UserID)
                .ToListAsync();

            return View(clients);
        }
        // GET: Admin/ClientDetails/5
        public async Task<IActionResult> ClientDetails(int id)
        {
            var client = await _context.Users.FirstOrDefaultAsync(u => u.UserID == id);
            if (client == null)
                return NotFound();

            return View(client);
        }

        // GET: Admin/EditClient/5
        public async Task<IActionResult> EditClient(int id)
        {
            var client = await _context.Users.FindAsync(id);
            if (client == null)
                return NotFound();

            return View(client);
        }

        // POST: Admin/EditClient/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditClient(int id, UserModel model)
        {
            if (id != model.UserID)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Client updated successfully!";
                    return RedirectToAction(nameof(ClientDetails), new { id = model.UserID });
                }
                catch (DbUpdateException)
                {
                    TempData["Error"] = "Error updating client. Please try again.";
                }
            }

            return View(model);
        }

        // POST: Admin/DeleteClient/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _context.Users.FindAsync(id);
            if (client == null)
                return NotFound();

            try
            {
                _context.Users.Remove(client);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Client deleted successfully!";
                return RedirectToAction(nameof(Clients));
            }
            catch
            {
                TempData["Error"] = "Error deleting client. Please try again.";
                return RedirectToAction(nameof(ClientDetails), new { id });
            }
        }     

        // GET: Admin/Venues
        public async Task<IActionResult> Venues()
        {
            var venues = await _context.Venues
                .OrderBy(v => v.VenueID)
                .ToListAsync();
            return View(venues);
        }

        // GET: Admin/EditVenue/5
        // GET: Admin/VenueDetails/5
        public async Task<IActionResult> VenueDetails(int id)
        {
            var venue = await _context.Venues.FindAsync(id);
            if (venue == null)
                return NotFound();

            return View(venue);
        }

        // GET: Admin/AddVenue
        public IActionResult AddVenue()
        {
            return View();
        }

        // POST: Admin/AddVenue
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVenue(VenueModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Venues.Add(model);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Venue added successfully!";
                return RedirectToAction("Venues");
            }
            TempData["Error"] = "Failed to add venue. Please check the form.";
            return View(model);
        }

        // GET: Admin/EditVenue/5
        public async Task<IActionResult> EditVenue(int id)
        {
            var venue = await _context.Venues.FindAsync(id);
            if (venue == null) return NotFound();
            return View(venue);
        }

        // POST: Admin/EditVenue/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditVenue(int id, VenueModel model)
        {
            if (id != model.VenueID) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Venue updated successfully!";
                    return RedirectToAction("VenueDetails", new { id = model.VenueID });
                }
                catch (DbUpdateException)
                {
                    TempData["Error"] = "Error updating venue. Please try again.";
                }
            }
            return View(model);
        }

        // POST: Admin/DeleteVenue/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVenue(int id)
        {
            var venue = await _context.Venues.FindAsync(id);
            if (venue == null) return NotFound();

            try
            {
                _context.Venues.Remove(venue);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Venue deleted successfully!";
                return RedirectToAction("Venues");
            }
            catch
            {
                TempData["Error"] = "Error deleting venue. Please try again.";
                return RedirectToAction("VenueDetails", new { id });
            }
        }


        // GET: Admin/Events
        public async Task<IActionResult> Events()
        {
            var events = await _context.Events
                .Include(e => e.Venue) // to show venue name if needed
                .OrderBy(e => e.EventID)
                .ToListAsync();
            return View(events);
        }

        // GET: Admin/EventDetails/5
        public async Task<IActionResult> EventDetails(int id)
        {
            var ev = await _context.Events
                .Include(e => e.Venue)
                .FirstOrDefaultAsync(e => e.EventID == id);

            if (ev == null) return NotFound();
            return View(ev);
        }


        // GET: Admin/AddEvent
        public IActionResult AddEvent()
        {
            ViewBag.Venues = new SelectList(_context.Venues.OrderBy(v => v.VenueName), "VenueID", "VenueName");

            return View(new EventModel { EventDate = DateTime.Today });
        }

        // POST: Admin/AddEvent
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEvent(EventModel model)
        {
            // Re-populate dropdown to preserve selection
            ViewBag.Venues = new SelectList(_context.Venues.OrderBy(v => v.VenueName), "VenueID", "VenueName", model.VenueID);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _context.Events.Add(model);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Event added successfully!";
            return RedirectToAction("Events");
        }

        // GET: Admin/EditEvent/5
        public async Task<IActionResult> EditEvent(int id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return NotFound();
            ViewBag.Venues = _context.Venues.ToList();
            return View(ev);
        }

        // POST: Admin/EditEvent/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEvent(int id, EventModel model)
        {
            if (id != model.EventID) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Event updated successfully!";
                    return RedirectToAction("EventDetails", new { id = model.EventID });
                }
                catch (DbUpdateException)
                {
                    TempData["Error"] = "Error updating event. Please try again.";
                }
            }
            ViewBag.Venues = _context.Venues.ToList();
            return View(model);
        }

        // POST: Admin/DeleteEvent/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return NotFound();

            try
            {
                _context.Events.Remove(ev);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Event deleted successfully!";
                return RedirectToAction("Events");
            }
            catch
            {
                TempData["Error"] = "Error deleting event. Please try again.";
                return RedirectToAction("EventDetails", new { id });
            }
        }
        public async Task<IActionResult> Bookings()
        {
            var bookings = await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.Venue)
                .Include(b => b.User)
                .OrderBy(b => b.BookingID)
                .ToListAsync();

            return View(bookings);
        }
        // GET: Admin/BookingDetails/5
        public async Task<IActionResult> BookingDetails(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.Venue)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.BookingID == id);

            if (booking == null) return NotFound();

            return View(booking);
        }
        public IActionResult AddBooking()
        {
            ViewBag.Events = new SelectList(_context.Events, "EventID", "EventName");
            ViewBag.Venues = new SelectList(_context.Venues, "VenueID", "VenueName");

            ViewBag.Clients = new SelectList(
                _context.Users.Where(u => u.Role == "Client"),
                "UserID",
                "FirstName"
            );

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBooking(BookingModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Bookings.Add(model);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Booking added successfully!";
                return RedirectToAction("Bookings");
            }

            // Reload dropdowns if validation fails
            ViewBag.Events = new SelectList(_context.Events, "EventID", "EventName", model.Event.EventID);
            ViewBag.Venues = new SelectList(_context.Venues, "VenueID", "VenueName", model.Venue.VenueID);
            ViewBag.Clients = new SelectList(_context.Users.Where(u => u.Role == "Client"), "UserID", "FirstName", model.User.UserID);

            return View(model);
        }
        public async Task<IActionResult> EditBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
                return NotFound();

            ViewBag.Events = new SelectList(_context.Events, "EventID", "EventName");
            ViewBag.Venues = new SelectList(_context.Venues, "VenueID", "VenueName");
            ViewBag.Clients = new SelectList(_context.Users.Where(u => u.Role == "Client"), "UserID", "FirstName");

            return View(booking);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBooking(int id, BookingModel model)
        {
            if (id != model.BookingID)
                return BadRequest();

            if (ModelState.IsValid)
            {
                _context.Update(model);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Booking updated!";
                return RedirectToAction("BookingDetails", new { id = model.BookingID });
            }

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
                return NotFound();

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Booking deleted!";
            return RedirectToAction("Bookings");
        }







    }
}
