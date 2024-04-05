using AirlineBookingSystem.Data;
using AirlineBookingSystem.Models.ViewModels;
using AirlineBookingSystem.Models;
using AirlineBookingSystem.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using AirlineBookingSystem.Repository;
using Microsoft.AspNetCore.Authorization;

namespace AirlineBookingSystem.Areas.Customer.Controllers
{
     [Area("Customer")]    
    public class BookingController : Controller
    {
        private readonly IUnitofWork _unitOfWork;
        private readonly ApplicationDbContext _context;

        public BookingController(IUnitofWork unitOfWork, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public IActionResult Index(int flightId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            var bookingVM = new BookingVM
            {
                FlightCategoryList = _unitOfWork.FlightCategory.GetAll()
                    .Select(fc => new SelectListItem
                    {
                        Text = fc.Name,
                        Value = $"{fc.FlightCategoryId}|{fc.Price}"
                    }),
                Booking = new Booking()
            };

            // Set the received FlightId to the Booking model
            bookingVM.Booking.FlightId = flightId;

            var defaultCategory = _unitOfWork.FlightCategory.GetAll().FirstOrDefault();
            if (defaultCategory != null)
            {
                bookingVM.Booking.FlightCategoryId = defaultCategory.FlightCategoryId;
            }

            return View(bookingVM);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(BookingVM bookingVM)
        {          
            var booking = new Booking
            {
                FlightId = bookingVM.Booking.FlightId,
                PassengerName = bookingVM.Booking.PassengerName,
                PassengerEmail = bookingVM.Booking.PassengerEmail,
                AdultCount = bookingVM.Booking.AdultCount,
                ChildCount = bookingVM.Booking.ChildCount,
                InfantsCount = bookingVM.Booking.InfantsCount,
                NumberOfSeats = bookingVM.Booking.NumberOfSeats,
                TotalPrice = bookingVM.Booking.TotalPrice,
                FlightCategoryId = bookingVM.Booking.FlightCategoryId
            };

           
            _unitOfWork.Booking.Add(booking);
            _unitOfWork.Save();

            return RedirectToAction("Confirm", new { bookingId = booking.BookingId });
        }
        public IActionResult Confirm(int bookingId)
        {
            var booking = _unitOfWork.Booking.Get(bookingId);
            if (booking == null)
            {
                
                return RedirectToAction("Index");
            }

           
            ViewBag.BookingId = booking.BookingId;
            ViewBag.Message = "Your ticket is confirmed. Thank you!";

            return View();
        }
    }
  
}

