using SachseRentalsApi.Entities;
using Microsoft.EntityFrameworkCore;
using SachseRentalsApi.Models;


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

        public async Task<long> AddNewPaymentAsync(Payment newPayment)
        {
            try
            {
                await _dbContext.AddAsync(newPayment);
                await SaveChangesAsync();
                return newPayment.Id;
            }
            catch (Exception e)
            {
                throw new DbUpdateException($"An error occurred adding payment. Payment Id: {newPayment.Id}.", e);
            }
        }

        public async Task SetPaymentAsReceivedAsync(Payment paymentEntity)
        {
            try
            {
                paymentEntity.Received = true;
                paymentEntity.DateReceived = DateTime.UtcNow;
                await SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new DbUpdateException($"An error occurred updating the payment received status. Payment Id: {paymentEntity.Id}", e);
            }
        }

        public async Task UpdatePaymentTypeAsync(Payment paymentEntity, PaymentType paymentType)
        {
            try
            {
                paymentEntity.Type = paymentType;
                await SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new DbUpdateException($"An error occurred updating the payment type. Payment Id: {paymentEntity.Id}.", e);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}