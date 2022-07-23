using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SachseRentalsApi.Models;
using SachseRentalsApi.Services;

namespace SachseRentalsApi.Controllers
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public PaymentController(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public async Task<IActionResult> AddNewPayment([FromBody] PaymentDto newPayment)
        {
            var newPaymentEntity = _mapper.Map<Entities.Payment>(newPayment);
            await _paymentRepository.AddNewPaymentAsync(newPaymentEntity);
            var paymentAdded = await _paymentRepository.SaveChangesAsync();

            // todo: improve this error handling
            if (!paymentAdded) {
                var msg = $"Error logging a record of the payment.";
                return BadRequest(msg);
            }

            return Created("A record of this payment was recorded.", newPayment);
        }

        [HttpGet("{paymentId}")]
        public async Task<IActionResult> GetPayment(long paymentId)
        {
            var paymentEntity = await _paymentRepository.GetPaymentAsync(paymentId);
            if (paymentEntity == null) {
                return NotFound("The payment was not found.");
            }
            return Ok(paymentEntity);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteGuest(string id)
        {
            throw new NotImplementedException();
            // return NoContent();
        }
    }

    [Route("api/payments")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public PaymentsController(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        [HttpGet("{guestId}")]
        public async Task<IActionResult> GetGuestPayments(long guestId)
        {
            var payments = await _paymentRepository.GetGuestPaymentsAsync(guestId);
            return Ok(payments);
        }
    }
}