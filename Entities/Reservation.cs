using System.ComponentModel.DataAnnotations;
using SachseRentalsApi.Models;


namespace SachseRentalsApi.Entities;

public class Reservation
{
    [Key]
    public long Id { get; set; }
    
    [Required]
    public int PropertyId { get; set; }

    [Required]
    public long GuestId { get; set; }

    [Required]
    public long PaymentId { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public ReservationStatus Status { get; set; } = ReservationStatus.UPCOMING;

    [Required]
    public bool PaidInFull { get; set; }
}
