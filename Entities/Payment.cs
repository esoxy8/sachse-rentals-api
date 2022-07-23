using System.ComponentModel.DataAnnotations;

namespace SachseRentalsApi.Entities 
{
    public class Payment
    {
        public enum PaymentType {
            CARD,
            CASH,
            CHECK,
            VENMO
        }

        [Key]
        public long Id { get; set; }

        [Required]
        public long GuestId { get; set; }

        [Required]
        public PaymentType Type { get; set; }

        [Required]
        public float Amount { get; set; }

        [Required]
        public DateTime DateSubmitted { get; set; } = DateTime.UtcNow;
        public bool Received { get; set; } = false;
        public DateTime DateReceived { get; set; }
    }
}