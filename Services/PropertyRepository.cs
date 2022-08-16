using Microsoft.EntityFrameworkCore;
using SachseRentalsApi.Entities;

namespace SachseRentalsApi.Services;

public class PropertyRepository : IPropertyRepository
{
    private readonly SachseRentalsDb _dbContext;
    
    public PropertyRepository(SachseRentalsDb dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Property?> GetPropertyAsync(long propertyId)
    {
        return await _dbContext.Properties.FirstOrDefaultAsync(p => p.Id == propertyId);
    }

    public async Task<long> AddNewPropertyAsync(Property property)
    {
        try
        {
            await _dbContext.AddAsync(property);
            await SaveChangesAsync();
            return property.Id;
        }
        catch (Exception e)
        {
            throw new DbUpdateException("Error saving property to the database.", e);
        }
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}