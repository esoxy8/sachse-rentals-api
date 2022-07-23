using SachseRentalsApi.Entities;
using Microsoft.EntityFrameworkCore;


namespace SachseRentalsApi.Services
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly SachseRentalsDb _dbContext;
        
        public PaymentRepository(SachseRentalsDb dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IEnumerable<Payment>> GetGuestPaymentsAsync(long guestId)
        {
            return await _dbContext.Payments.OrderByDescending(payment => payment.Id).Where(payment => payment.GuestId == guestId).ToListAsync();
        }

        public async Task<Payment?> GetPaymentAsync(long paymentId)
        {
            return await _dbContext.Payments.FirstOrDefaultAsync(payment => payment.Id == paymentId);
        }

        public async Task AddNewPaymentAsync(Payment newPayment)
        {
            await _dbContext.AddAsync<Payment>(newPayment);
        }

        public Task SetPaymentAsReceivedAsync(DateTime time)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePaymentTypeAsync(int paymentType)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _dbContext.SaveChangesAsync() > 0);
        }
    }
}