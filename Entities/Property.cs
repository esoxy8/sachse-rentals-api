using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SachseRentalsApi.Entities;

public class Property
{
    [Key]
    public long Id { get; set; }
    
    [Required]
    public string Address { get; set; }
    
    [Required]
    public string City { get; set; }
    
    [Required]
    public string State { get; set; }
    
    [Required]
    public string Zip { get; set; }

    public string Country { get; set; } = "United States";
}