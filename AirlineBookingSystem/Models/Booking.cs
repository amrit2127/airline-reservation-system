using System.ComponentModel.DataAnnotations;

namespace AirlineBookingSystem.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        [Required]
        public int FlightId { get; set; }

        public Flight Flight { get; set; }

        [Required]
        public string PassengerName { get; set; }

        [Required]
        public string PassengerEmail { get; set; }        

        [Required]
        public int NumberOfSeats { get; set; }
        [Required]
        public int AdultCount { get; set; }
        public int? ChildCount { get; set; }
        public int? InfantsCount { get; set; }

        public double TotalPrice { get; set; }
        [Required]
        public int FlightCategoryId { get; set; }
        public FlightCategory FlightCategory { get; set; }

    }
}
