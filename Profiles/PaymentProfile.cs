using AutoMapper;

namespace SachseRentalsApi.Profiles
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<Models.PaymentDto, Entities.Payment>();
            CreateMap<Entities.Payment, Models.PaymentDto>();
        }
    }

}
