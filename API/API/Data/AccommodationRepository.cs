using API.DTO;
using API.Entities;
using API.Interfaces;
using AutoMapper;
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
           _context.Accommodations.Add(accommodation);
        }

        public async Task<Accommodation> GetAccommodation(int id)
        {
            return _context.Accommodations
                .Include(p => p.Photos)
                .FirstOrDefault(x => x.BenefactorID == id);
        }

        public void upddate(Accommodation accommodation)
        {
            throw new NotImplementedException();
        }
    }
}
