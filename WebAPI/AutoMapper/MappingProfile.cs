using AutoMapper;
using WebAPI.DataTransferObjects;
using WebAPI.Entities;

namespace WebAPI.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeDtoForUpdate, Employee>().ReverseMap();
            CreateMap<EmployeeDtoForCreate, Employee>();
        }
    }
}
