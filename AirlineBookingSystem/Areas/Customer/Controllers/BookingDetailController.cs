using System.Linq;
using AirlineBookingSystem.Data;
using AirlineBookingSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AirlineBookingSystem.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class BookingDetailController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingDetailController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var bookingsWithFlightDetails = _context.Bookings
                .Select(b => new BookingDetailVM
                {
                    BookingId = b.BookingId,
                    FlightId = b.FlightId,
                    PassengerName = b.PassengerName,
                    PassengerEmail = b.PassengerEmail,
                    NumberOfSeats = b.NumberOfSeats,
                    TotalPrice = b.TotalPrice,
                    FlightNumber = b.Flight.FlightNumber, // Include flight details
                    DepartureAirport = b.Flight.DepartureAirport,
                    DestinationAirport = b.Flight.DestinationAirport,
                    DepartureTime = b.Flight.DepartureTime,
                    ArrivalTime = b.Flight.ArrivalTime
                })
                .ToList();

            return View(bookingsWithFlightDetails);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var booking = _context.Bookings.Find(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.Bookings.Remove(booking);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}
