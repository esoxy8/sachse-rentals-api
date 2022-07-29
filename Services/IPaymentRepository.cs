using SachseRentalsApi.Entities;
using SachseRentalsApi.Models;


namespace SachseRentalsApi.Services
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetGuestPaymentsAsync(long guestId);
        Task<Payment?> GetPaymentAsync(long paymentId);
        Task<long> AddNewPaymentAsync(Payment newPayment);
        Task SetPaymentAsReceivedAsync(Payment paymentEntity);
        Task UpdatePaymentTypeAsync(Payment paymentEntity, PaymentType paymentType);
        Task SaveChangesAsync();
    }
}