using System.Numerics;
namespace SachseRentalsApi.Models 
{
    public class GuestDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public long Phone { get; set; }
    }
}