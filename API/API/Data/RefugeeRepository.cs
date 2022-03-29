using API.DTO;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IEnumerable<AccommodationDTO>> GetEmptyAccommodations()
        {
            var dupa = await _context.Accommodations
                .Where(x => x.RefugeeID == 0)
                .Include(p => p.Photos)
                .ToListAsync();

            return _mapper.Map<IEnumerable<AccommodationDTO>>(dupa);
        }
    }
}
