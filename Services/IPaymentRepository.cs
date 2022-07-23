using SachseRentalsApi.Entities;


namespace SachseRentalsApi.Services
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetGuestPaymentsAsync(long guestId);
        Task<Payment?> GetPaymentAsync(long paymentId);
        Task AddNewPaymentAsync(Payment newPayment);
        Task SetPaymentAsReceivedAsync(DateTime time);
        // todo: update this to work with enum
        Task UpdatePaymentTypeAsync(int paymentType);
        Task<bool> SaveChangesAsync();
    }
}