using AutoMapper;
using Backend.Dtos;
using Backend.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Backend.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Device, DeviceDto>().ReverseMap();
            CreateMap<Staff, StaffDto>().ReverseMap();
            CreateMap<Client, ClientDto>().ReverseMap();
        }
    }
}
