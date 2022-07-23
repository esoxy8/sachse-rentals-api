using System.Numerics;
using System.ComponentModel.DataAnnotations;


namespace SachseRentalsApi.Entities 
{
    public class Guest
    {
        [Key]
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public long? Phone { get; set; }
        
        public Guest(string firstName, string lastName, string email) {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}