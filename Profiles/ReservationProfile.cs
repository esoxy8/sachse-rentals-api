using AutoMapper;

namespace SachseRentalsApi.Profiles
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<Models.ReservationDto, Entities.Reservation>();
            CreateMap<Entities.Reservation, Models.ReservationDto>();
        }
    }

}
