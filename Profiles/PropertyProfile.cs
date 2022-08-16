using AutoMapper;

namespace SachseRentalsApi.Profiles;

public class PropertyProfile : Profile
{
    public PropertyProfile()
    {
        CreateMap<Models.PropertyDto, Entities.Property>();
        CreateMap<Entities.Property, Models.PropertyDto>();
    }
}