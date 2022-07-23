using System.ComponentModel.DataAnnotations;

namespace SachseRentalsApi.Entities 
{
    public class Admin
    {
        [Key]
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public Admin(){}
        
        public Admin(string usr, string pwd) {
            Username = usr;
            Password = pwd;
        }
    }
}