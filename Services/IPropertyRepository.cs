using SachseRentalsApi.Entities;

namespace SachseRentalsApi.Services;

public interface IPropertyRepository
{
    Task<Property?> GetPropertyAsync(long propertyId);
    Task<long> AddNewPropertyAsync(Property property);
    Task SaveChangesAsync();
}