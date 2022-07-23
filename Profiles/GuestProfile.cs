using AutoMapper;

namespace SachseRentalsApi.Profiles
{
    public class GuestProfile : Profile
    {
        public GuestProfile()
        {
            CreateMap<Models.GuestDto, Entities.Guest>();
            CreateMap<Entities.Guest, Models.GuestDto>();
        }
    }

}
