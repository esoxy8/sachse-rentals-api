using SachseRentalsApi.Entities;


namespace SachseRentalsApi.Services
{
    public interface IGuestRepository
    {
        Task<Guest?> GetGuestByIdAsync(long guestId);
        Task<IEnumerable<Guest>> GetGuestsAsync();
        Task<Guest?> GetGuestByEmailAsync(string guestEmail);
        Task AddNewGuestAsync(Guest newGuest);
        Task<Guest?> DeleteGuestAsync(int guestId);
        Task<bool> SaveChangesAsync();
    }
}