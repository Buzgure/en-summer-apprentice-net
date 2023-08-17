using AutoMapper;
using TMS.API.Model;
using TMS.API.Model.Dto;

namespace TMS.API.Profiles

{
    public class OrderProfile : Profile
    {

        public OrderProfile() {
            
            CreateMap<Order, OrderDTO>().ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.CustomerName)).ForMember(dest => dest.TicketCategoryName, opt => opt.MapFrom(src => src.TicketCategory.Description));
            CreateMap<Order, OrderPatchDTO>().ReverseMap();



        }
    }
}
