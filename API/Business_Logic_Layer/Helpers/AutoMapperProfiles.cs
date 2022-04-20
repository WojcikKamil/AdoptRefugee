using API.DTO;
using API.Entities;
using AutoMapper;
using Business_Logic_Layer.DTO;
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
            CreateMap<AppUser, AccommodationDTO>();
            CreateMap<Person, PersonDTO>();
            CreateMap<Refugee, ComradesDTO>();
            CreateMap<Benefactor, Comrades>();
            CreateMap<PersonDTO, ComradesDTO>();
            CreateMap<Comrades, ComradesDTO>();
            CreateMap<Accommodation, AccommodationDTO>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(
                    src => src.Photos!.FirstOrDefault()!.Url));
            CreateMap<Request, SendRequestDTO>();
            CreateMap<Photo, PhotoDTO>();
            CreateMap<Request, DisplayRequestDTO>();
            CreateMap<Accommodation, DisplayAccommodationDTO>();
            CreateMap<Accommodation, ConfirmAccommodationDTO>();
            CreateMap<ConfirmAccommodationDTO, Accommodation>();
        }
    }
}
