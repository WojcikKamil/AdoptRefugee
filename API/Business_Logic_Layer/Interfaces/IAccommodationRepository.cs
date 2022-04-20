using API.DTO;
using API.Entities;
using Business_Logic_Layer.DTO;

namespace API.Interfaces
{
    public interface IAccommodationRepository
    {
        void update(Accommodation accommodation);
        void AddAccommodation(Accommodation accommodation);
        Task<Accommodation> GetAccommodation(int id);
        Task<Accommodation> GetOneAccommodation(int id);
    }
}
