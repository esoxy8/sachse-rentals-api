namespace SachseRentalsApi.Models
{
    public class ReservationDto
    {
        public long GuestId { get; set; }
        public long PaymentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool PaidInFull { get; set; } = false;
    }
}