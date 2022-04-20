using API.DTO;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Business_Logic_Layer.DTO;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AccommodationRepository : IAccommodationRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public AccommodationRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddAccommodation(Accommodation accommodation)
        {
           _context?.Accommodations?.Add(accommodation);
        }

        public async Task<Accommodation> GetAccommodation(int id)
        {
            return _context.Accommodations!
                .Include(p => p.Photos)!
                .FirstOrDefault(x => x.BenefactorAppUserID == id)!;
        }

        public async Task<Accommodation> GetOneAccommodation(int id)
        {
            return await _context.Accommodations!
                .SingleOrDefaultAsync(x => x.BenefactorAppUserID == id);
        }

        public void update(Accommodation accommodation)
        {
            _context.Entry(accommodation).State = EntityState.Modified;
        }
    }
}
