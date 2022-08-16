using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SachseRentalsApi.Models;
using SachseRentalsApi.Services;

namespace SachseRentalsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PropertyController : ControllerBase
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IMapper _mapper;
    
    public PropertyController(IPropertyRepository propertyRepository, IMapper mapper)
    {
        _propertyRepository = propertyRepository;
        _mapper = mapper;
    }
    
    [HttpGet("{propertyId}")]
    public async Task<IActionResult> GetProperty(long propertyId)
    {
        var propertyEntity = await _propertyRepository.GetPropertyAsync(propertyId);
        if (propertyEntity == null)
        {
            return NotFound($"Property does not exist. Property Id: {propertyId}.");
        }

        return Ok(propertyEntity);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddNewProperty([FromBody] PropertyDto newProperty)
    {
        var newPropertyEntity = _mapper.Map<Entities.Property>(newProperty);
        var propertyId = await _propertyRepository.AddNewPropertyAsync(newPropertyEntity);
        return Created("", $"New Property added. Property Id: {propertyId}.");
    }
}