using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SachseRentalsApi.Models;
using SachseRentalsApi.Services;

namespace SachseRentalsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : ControllerBase
    {

        private readonly IGuestRepository _guestRepository;
        private readonly IMapper _mapper;

        public GuestController(IGuestRepository guestRepository, IMapper mapper)
        {
            _guestRepository = guestRepository ?? throw new ArgumentNullException(nameof(guestRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public async Task<IActionResult> AddNewGuest([FromBody] GuestDto newGuest)
        {
            var guestEntity = await _guestRepository.GetGuestByEmailAsync(newGuest.Email);
            if (guestEntity != null) {
                var msg = $"A guest with the email \'{newGuest.Email}\' already exists";
                return BadRequest(msg);
            }

            var newGuestEntity = _mapper.Map<Entities.Guest>(newGuest);
            await _guestRepository.AddNewGuestAsync(newGuestEntity);
            var guestAdded = await _guestRepository.SaveChangesAsync();

            if (!guestAdded) {
                var msg = $"The guest \'{newGuest.Email}\' could not be added.";
                return BadRequest(msg);
            }

            return Created("The guest was successfully added.", newGuest);
        }

        [HttpGet]
        public async Task<IActionResult> GetGuest(string email)
        {
            var guestEntity = await _guestRepository.GetGuestByEmailAsync(email);
            if (guestEntity == null) {
                return NotFound("The guest was not found");
            }
            return Ok(_mapper.Map<GuestDto>(guestEntity));
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteGuest(string id)
        {
            throw new NotImplementedException();
            // return NoContent();
        }
    }
}