using AutoMapper;
using TMS.API.Model;
using TMS.API.Model.Dto;

namespace TMS.API.Profiles
{
    public class TicketCategoryProfile : Profile
    {
        public TicketCategoryProfile() {
            CreateMap<TicketCategory, TicketCategoryDTO>();
        }
    }
}
