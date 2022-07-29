using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SachseRentalsApi.Models;
using SachseRentalsApi.Services;

namespace SachseRentalsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{

    private readonly IGuestRepository _guestRepository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMapper _mapper;

    public PaymentController(IPaymentRepository paymentRepository, 
        IGuestRepository guestRepository, IMapper mapper)
    {
        _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
        _guestRepository = guestRepository ?? throw new ArgumentNullException(nameof(guestRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    [HttpPost]
    public async Task<IActionResult> AddNewPayment([FromBody] PaymentDto newPayment)
    {
        // validate payment type
        if ((int)newPayment.Type >= Enum.GetNames(typeof(PaymentType)).Length)
        {
            return BadRequest("Invalid Payment Type");
        }
        
        // validate guest
        var guest = await _guestRepository.GetGuestByIdAsync(newPayment.GuestId);
        if (guest == null)
        {
            return BadRequest($"Cannot add payment for a guest that does not exist. Guest Id: {newPayment.GuestId}.");
        }
        
        var newPaymentEntity = _mapper.Map<Entities.Payment>(newPayment);
        var paymentId = await _paymentRepository.AddNewPaymentAsync(newPaymentEntity);
        return Created($"A record of this payment was recorded. Payment Id: {paymentId}.", newPayment);
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

    [HttpGet("guest/{guestId}")]
    public async Task<IActionResult> GetGuestPayments(long guestId)
    {
        var payments = await _paymentRepository.GetGuestPaymentsAsync(guestId);
        return Ok(payments);
    }
    
    [HttpPut("received/{paymentId}")]
    public async Task<IActionResult> SetPaymentAsReceived(long paymentId)
    {
        var paymentEntity = await _paymentRepository.GetPaymentAsync(paymentId);
        if (paymentEntity == null) {
            return NotFound($"Payment was not found. PaymentId: {paymentId}");
        }

        await _paymentRepository.SetPaymentAsReceivedAsync(paymentEntity);
        return Ok($"Payment received. Payment Id: {paymentId}.");
    }
    
    [HttpPut("type/{paymentId}")]
    public async Task<IActionResult> UpdatePaymentType(long paymentId, PaymentType paymentType)
    {
        if ((int)paymentType >= Enum.GetNames(typeof(PaymentType)).Length)
        {
            return BadRequest("Invalid Payment Type");
        }
        
        var paymentEntity = await _paymentRepository.GetPaymentAsync(paymentId);
        if (paymentEntity == null) {
            return NotFound($"Payment was not found. PaymentId: {paymentId}");
        }

        await _paymentRepository.UpdatePaymentTypeAsync(paymentEntity, paymentType);
        return Ok($"Payment type updated. Payment Id: {paymentId}.");
    }
}
