using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SachseRentalsApi.Models;
using SachseRentalsApi.Services;

namespace SachseRentalsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReservationController : ControllerBase
{
    private readonly IGuestRepository _guestRepository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IReservationRepository _reservationRepository;
    private readonly IMapper _mapper;

    public ReservationController(IReservationRepository reservationRepository,
        IPaymentRepository paymentRepository, IGuestRepository guestRepository, IMapper mapper)
    {
        _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
        _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
        _guestRepository = guestRepository ?? throw new ArgumentNullException(nameof(guestRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet("{reservationId}")]
    public async Task<IActionResult> GetReservation(long reservationId)
    {
        var reservationEntity = await _reservationRepository.GetReservationAsync(reservationId);
        if (reservationEntity == null)
        {
            return NotFound($"Reservation does not exist. Reservation Id: {reservationId}.");
        }

        return Ok(reservationEntity);
    }
    
    [HttpGet("guest/{guestId}")]
    public async Task<IActionResult> GetAllGuestReservations(long guestId)
    {
        var guestEntity = await _guestRepository.GetGuestByIdAsync(guestId);
        if (guestEntity == null)
        {
            return NotFound($"Guest not found. Cannot get reservations. Guest Id: {guestId}.");
        }

        var reservations = await _reservationRepository.GetAllGuestBookingsAsync(guestId);
        return Ok(reservations);
    }
    
    [HttpGet("guest/nearest/{guestId}")]
    public async Task<IActionResult> GetGuestNearestUpcomingReservation(long guestId)
    {
        var guestEntity = await _guestRepository.GetGuestByIdAsync(guestId);
        if (guestEntity == null)
        {
            return NotFound($"Guest not found. Cannot get nearest reservation. Guest Id: {guestId}.");
        }

        var nearestReservation = await _reservationRepository.GetNearestUpcomingReservationAsync(guestId);
        if (nearestReservation == null)
        {
            return NotFound($"Reservation not found. The guest has no upcoming reservations. Guest Id: {guestId}.");
        }

        return Ok(nearestReservation);
    }

    [HttpPost]
    public async Task<IActionResult> AddNewReservation([FromBody] ReservationDto newReservation)
    {
        var guestEntity = await _guestRepository.GetGuestByIdAsync(newReservation.GuestId);
        if (guestEntity == null)
        {
            return NotFound($"Guest not found. Cannot add reservation. Guest Id: {newReservation.GuestId}.");
        }
        
        var paymentEntity = await _paymentRepository.GetPaymentAsync(newReservation.PaymentId);
        if (paymentEntity == null)
        {
            return NotFound($"Payment not found. Cannot add reservation. Payment Id: {newReservation.PaymentId}.");
        }

        if (newReservation.StartDate > newReservation.EndDate || newReservation.StartDate < DateTime.UtcNow)
        {
            return BadRequest("Invalid booking date range.");
        }

        var newReservationEntity = _mapper.Map<Entities.Reservation>(newReservation);
        var reservationId = await _reservationRepository.AddNewReservationAsync(newReservationEntity);
        return Created("", $"Reservation set. Reservation Id: {reservationId}.");
    }
    
    [HttpPut("status/{reservationId}")]
    public async Task<IActionResult> UpdateReservationStatus(long reservationId, ReservationStatus reservationStatus)
    {
        if ((int)reservationStatus >= Enum.GetNames(typeof(ReservationStatus)).Length)
        {
            return BadRequest("Invalid reservation status.");
        }
        
        var reservationEntity = await _reservationRepository.GetReservationAsync(reservationId);
        if (reservationEntity == null) {
            return NotFound($"Reservation was not found. Reservation Id: {reservationId}");
        }

        await _reservationRepository.UpdateReservationStatusAsync(reservationEntity, reservationStatus);
        return Ok($"Reservation status updated. Reservation Id: {reservationId}.");
    }
    
    [HttpPut("paid/{reservationId}")]
    public async Task<IActionResult> UpdateReservationPaidStatus(long reservationId)
    {
        var reservationEntity = await _reservationRepository.GetReservationAsync(reservationId);
        if (reservationEntity == null) {
            return NotFound($"Reservation was not found. Reservation Id: {reservationId}");
        }

        await _reservationRepository.UpdateReservationPaidInFullStatusAsync(reservationEntity);
        return Ok($"Reservation paid. Reservation Id: {reservationId}.");
    }
}