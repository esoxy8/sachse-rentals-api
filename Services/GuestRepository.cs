using SachseRentalsApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace SachseRentalsApi.Services
{
    public class GuestRepository : IGuestRepository
    {
        private readonly SachseRentalsDb _dbContext;

        public GuestRepository(SachseRentalsDb dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Guest>> GetGuestsAsync()
        {
            return await _dbContext.Guests.OrderBy(guest => guest.Id).ToListAsync();
        }
        public async Task<Guest?> GetGuestByEmailAsync(string guestEmail)
        {
            return await _dbContext.Guests.FirstOrDefaultAsync(guest => guest.Email.ToLower() == guestEmail.ToLower());
        }
        public async Task AddNewGuestAsync(Guest newGuest)
        {
            await _dbContext.AddAsync<Guest>(newGuest);
        }
        public Task<Guest?> DeleteGuestAsync(int guestId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _dbContext.SaveChangesAsync() > 0);
        }
    }
}