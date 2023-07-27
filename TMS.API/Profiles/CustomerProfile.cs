using AutoMapper;
using TMS.API.Model;
using TMS.API.Model.Dto;

namespace TMS.API.Profiles
{
    public class CustomerProfile : Profile
    {

        public CustomerProfile() {
            CreateMap<Customer, CustomerDTO>().ReverseMap();

        }
    }
}
