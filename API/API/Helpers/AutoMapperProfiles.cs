using API.DTO;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegisterDTO, AppUser>();
            CreateMap<Person, PersonDTO>();
            CreateMap<Comrades, ComradesDTO>();
        }
    }
}
