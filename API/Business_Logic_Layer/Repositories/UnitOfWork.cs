using API.Interfaces;
using AutoMapper;
using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Repositories;

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

        public IAccommodationRepository AccommodationRepository => new AccommodationRepository(_context, _mapper);

        public IRefugeeRepository RefugeeRepository => new RefugeeRepository(_context, _mapper);

        public IBenefactorRepository BenefactorRepository => new BenefactorRepository(_context, _mapper);

        public IRequestRepository RequestRepository => new RequestRepository(_context, _mapper);

        public async Task<bool> Done()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
