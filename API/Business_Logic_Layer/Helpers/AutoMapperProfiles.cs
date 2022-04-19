using API.DTO;
using API.Entities;
using AutoMapper;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Entities.models;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegisterDTO, AppUser>();
            CreateMap<AppUser, PersonDTO>();
            CreateMap<Person, PersonDTO>();
            CreateMap<Refugee, ComradesDTO>();
            CreateMap<Benefactor, Comrades>();
            CreateMap<PersonDTO, ComradesDTO>();
            CreateMap<Comrades, ComradesDTO>();
            CreateMap<Accommodation, AccommodationDTO>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(
                    src => src.Photos!.FirstOrDefault()!.Url));
            CreateMap<Photo, PhotoDTO>();
        }
    }
}
