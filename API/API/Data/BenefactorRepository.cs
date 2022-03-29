using API.DTO;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class BenefactorRepository : IBenefactorRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public BenefactorRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonDTO>> GetHomelessRefuges()
        {
            var dupa = await _context.Persons
                .Where(x => x.Status == "Refugee")
                .Include(c => c.Comrades)
                .ToListAsync();
            
            return _mapper.Map<IEnumerable<PersonDTO>>(dupa);
        }

        public async Task<IEnumerable<PersonDTO>> GetFamily(int id)
        {
            var per = await _context.Persons
                .Where(x => x.AppUserId == id)
                .Include(c => c.Comrades)
                .ToListAsync();

            return _mapper.Map<IEnumerable<PersonDTO>>(per);

        }
    }
}
