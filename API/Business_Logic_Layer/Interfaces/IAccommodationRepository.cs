using API.DTO;
using API.Entities;

namespace API.Interfaces
{
    public interface IAccommodationRepository
    {
        void upddate(Accommodation accommodation);
        void AddAccommodation(Accommodation accommodation);
        Task<Accommodation> GetAccommodation(int id);
    }
}
