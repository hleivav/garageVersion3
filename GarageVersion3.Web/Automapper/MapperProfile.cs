using AutoMapper;
using GarageVersion3.Core;
using GarageVersion3.Web.Models;

namespace GarageVersion3.Web.Automapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Member, MemberIndexViewModel>()
                .ForMember( // funktionen "ForMember" ger variabeln från vyn nrOfVehicles värdet av Vehicles.Count
                dest => dest.NrOfVehicles,
                from => from .MapFrom(s => s.Vehicles.Count));
            CreateMap<Member, MemberCreateViewModel>().ReverseMap();
            CreateMap<Member, MemberEditViewModel>().ReverseMap();
            CreateMap<Member, MemberDetailsViewModel>();
            CreateMap<Member, MemberDeleteViewModel>().ReverseMap();
        }
    }
}
