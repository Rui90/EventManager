using System.Linq;
using AutoMapper;
using EventManager.Domain;
using EventManager.Domain.Identity;
using EventManager.WebApi.ViewModels;

namespace EventManager.WebApi.Mappers
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

            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<User, UserLoginViewModel>().ReverseMap();
        }
    }
}
