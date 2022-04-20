using API.DTO;
using API.Entities;
using API.Helpers;
using X.PagedList;

namespace API.Interfaces
{
    public interface IRefugeeRepository
    {
        Task<IPagedList> GetEmptyAccommodations(Paging paging = null!, FilteringProperties filter = null!);

        Task<IEnumerable<AccommodationDTO>> GetAccommodationByBenefactorId(int SenderBenefactorId);
    }
}
