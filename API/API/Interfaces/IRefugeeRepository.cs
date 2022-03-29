using API.DTO;
using API.Entities;

namespace API.Interfaces
{
    public interface IRefugeeRepository
    {
        Task<IEnumerable<AccommodationDTO>> GetEmptyAccommodations();
    }
}
