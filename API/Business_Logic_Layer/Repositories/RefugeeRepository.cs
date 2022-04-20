using API.DTO;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace API.Data
{
    public class RefugeeRepository : IRefugeeRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public RefugeeRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccommodationDTO>> GetAccommodationByBenefactorId(int SenderBenefactorId)
        {
            var acc = await _context.Accommodations
                .Where(x => x.BenefactorAppUserID == SenderBenefactorId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<AccommodationDTO>>(acc);
        }

        public async Task<IPagedList> GetEmptyAccommodations(Paging paging = null!, FilteringProperties filter = null!)
        {
            List<AccommodationDTO> query = _context.Accommodations!
                .Where(x => x.RefugeeID == 0)
                .Include(p => p.Photos)
                .Select(m => _mapper.Map<AccommodationDTO>(m))
                .ToList();

            if (filter != null)
            {
                query = query.Where(m =>
                m.City!.Contains(filter.ByCity) && m.NumOfBeds! >= filter.ByNumOfBeds
                && m.NumOfRooms! >= filter.ByNumOfRooms)
                .ToList();
            }

            return await query.ToPagedListAsync(paging.PageNumber, paging.PageSize);
        }
    }
}
