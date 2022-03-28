using API.Interfaces;
using AutoMapper;

namespace API.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UnitOfWork(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IPersonRepository PersonRepository => new PersonRepository(_context, _mapper);

        public async Task<bool> Done()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
