using System.Linq;
using AutoMapper;
using ProAgil.Domain;
using ProAgil.WebApi.ViewModels;

namespace ProAgil.WebApi.Mappers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() {
            CreateMap<Event, EventViewModel>()
                .ForMember(dest => dest.GuestsEvents, opt => opt.MapFrom(from => from.GuestsEvents.Select(x => x.Guest).ToList()))
                .ReverseMap();
            CreateMap<Lot, LotViewModel>().ReverseMap();
            CreateMap<SocialNetwork, SocialNetworkViewModel>().ReverseMap();
            CreateMap<Guest, GuestViewModel>()
                .ForMember(dest => dest.Events, opt => opt.MapFrom(from => from.GuestsEvents.Select(x => x.Event).ToList())).ReverseMap();
        }
    }
}
