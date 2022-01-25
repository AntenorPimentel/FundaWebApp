using AutoMapper;
using Funda.Business.Models;
using Funda.Data.DTO;

namespace Funda.Business.Profiles
{
    public class MakelaarServiceProfile : Profile
    {
        public MakelaarServiceProfile()
        {
            CreateMap<HouseForSalePersistence, MakelaarDetails>()
                .ForMember(dest => dest.MakelaarId, opt => opt.MapFrom(src => src.MakelaarId))
                .ForMember(dest => dest.MakelaarNaam, opt => opt.MapFrom(src => src.MakelaarNaam));
        }
    }
}