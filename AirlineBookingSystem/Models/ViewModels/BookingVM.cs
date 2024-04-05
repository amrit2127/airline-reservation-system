using Microsoft.AspNetCore.Mvc.Rendering;

namespace AirlineBookingSystem.Models.ViewModels
{
    public class BookingVM
    {
        public Booking Booking { get; set; }
        public IEnumerable<SelectListItem> FlightCategoryList { get; set; }
        public decimal Price { get; set; } // Add this property
    }
}
