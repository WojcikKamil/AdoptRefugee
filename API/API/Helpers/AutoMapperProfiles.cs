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
            CreateMap<AppUser, PersonDTO>();
            CreateMap<Person, PersonDTO>();
            CreateMap<Person, ComradesDTO>();
            CreateMap<Person, Comrades>();
            CreateMap<PersonDTO, ComradesDTO>();
            CreateMap<Comrades, ComradesDTO>();
            CreateMap<Accommodation, AccommodationDTO>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(
                    src => src.Photos.FirstOrDefault().Url));
            CreateMap<Photo, PhotoDTO>();
        }
    }
}
