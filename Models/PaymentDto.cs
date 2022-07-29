namespace SachseRentalsApi.Models
{
    public class PaymentDto
    {
        public long GuestId { get; set; }
        public PaymentType Type { get; set; }
        public float Amount { get; set; }
        public bool Received { get; set; }
    }
}