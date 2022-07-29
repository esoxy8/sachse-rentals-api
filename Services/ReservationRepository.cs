using Microsoft.EntityFrameworkCore;
using SachseRentalsApi.Entities;
using SachseRentalsApi.Models;

namespace SachseRentalsApi.Services;

public class ReservationRepository : IReservationRepository
{
    private readonly SachseRentalsDb _dbContext;
        
    public ReservationRepository(SachseRentalsDb dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Reservation?> GetReservationAsync(long reservationId)
    {
        return await _dbContext.Reservations.FirstOrDefaultAsync(r => r.Id == reservationId);
    }

    public async Task<Reservation?> GetNearestUpcomingReservationAsync(long guestId)
    {
        return await _dbContext.Reservations
            .Where(r => r.GuestId == guestId)
            .Where(r => r.Status == ReservationStatus.UPCOMING)
            .OrderBy(r => r.StartDate)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Reservation>> GetAllGuestBookingsAsync(long guestId)
    {
        return await _dbContext.Reservations.Where(r => r.GuestId == guestId).ToListAsync();
    }

    public async Task<long> AddNewReservationAsync(Reservation reservation)
    {
        try
        {
            await _dbContext.AddAsync(reservation);
            await SaveChangesAsync();
            return reservation.Id;
        }
        catch (Exception e)
        {
            throw new DbUpdateException("Error saving reservation to the database.", e);
        }
    }

    public async Task UpdateReservationStatusAsync(Reservation reservation, ReservationStatus reservationStatus)
    {
        try
        {
            reservation.Status = reservationStatus;
            await SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new DbUpdateException("Error updating reservation status to the database.", e);
        }
        
    }

    public async Task UpdateReservationPaidInFullStatusAsync(Reservation reservation)
    {
        try
        {
            reservation.PaidInFull = true;
            await SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new DbUpdateException("Error updating the reservation's paid in full status to the database.");
        }
    }
    
    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}