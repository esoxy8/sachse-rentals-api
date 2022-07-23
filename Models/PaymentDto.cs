namespace SachseRentalsApi.Models
{
    public class PaymentDto
    {
        public long GuestId { get; set; }
        public int Type { get; set; }
        public float Amount { get; set; }
        public bool Received { get; set; } = false;
    }
}