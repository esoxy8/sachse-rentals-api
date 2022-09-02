using SachseRentalsApi.Entities;
using SachseRentalsApi.Models;

namespace SachseRentalsApi.Services;

public interface IReservationRepository
{
    Task<Reservation?> GetReservationAsync(long reservationId);
    Task<Reservation?> GetNearestUpcomingReservationAsync(long guestId);
    Task<IEnumerable<Reservation>> GetAllGuestBookingsAsync(long guestId);
    Task<long> AddNewReservationAsync(Reservation reservation);
    Task UpdateReservationStatusAsync(Reservation reservation, ReservationStatus reservationStatus);
    Task UpdateReservationPaidInFullStatusAsync(Reservation reservation);
    Task<IEnumerable<Reservation>> GetUpcomingPropertyReservationsAsync(long propertyId);
    Task SaveChangesAsync();
}