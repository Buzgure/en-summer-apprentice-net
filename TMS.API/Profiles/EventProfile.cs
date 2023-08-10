using AutoMapper;
using System.Diagnostics.Tracing;
using TMS.API.Model;
using TMS.API.Model.Dto;

namespace TMS.API.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile() {
            CreateMap<Event, EventDTO>().ForMember(dest => dest.EventType, opt => opt.MapFrom(src => src.EventType.EventTypeName))
                .ForMember(dest => dest.Venue, opt => opt.MapFrom(src => src.Venue.Location)).ForMember(dest => dest.TicketCategory, opt => opt.MapFrom(src => src.TicketCategories.Where(tc => tc.EventId == src.EventId).Select(tc => new TicketCategoryDTO
                {
                    TicketCategoryId = tc.TicketCategoryId,
                    EventId = tc.EventId,
                    Description = tc.Description,
                    Price = tc.Price
                    
                }).ToList()));

        
        }
    }
}
