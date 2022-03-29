using API.Entities;
using API.Interfaces;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using API.DTO;

namespace API.Data
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public PersonRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddComrades(Comrades comrades)
        {
            _context.Comrades.Add(comrades);
        }

        public void AddData(Person person)
        {
            _context.Persons.Add(person);
        }

        public async Task<AppUser> GetByEmail(string userName)
        {
            return await _context.Users
                .SingleOrDefaultAsync(x => x.UserName == userName);
        }

        public void Update(AppUser appUser)
        {
            _context.Entry(appUser).State = EntityState.Modified;
        }
    }
}
