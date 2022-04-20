using API.Entities;
using API.Interfaces;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using API.DTO;
using Data_Access_Layer.Entities;

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

        public void AddBenefactorData(Benefactor benefactor)
        {
            _context.Benefactors?.Add(benefactor);
        }

        public void AddComrades(Comrades comrades)
        {
            _context.Comrades?.Add(comrades);
        }

        public void AddRefugeeData(Refugee refugee)
        {
            _context.Refugees?.Add(refugee);
        }

        public async Task<AppUser> GetByEmail(string userName)
        {
            return await _context.Users
                .SingleOrDefaultAsync(x => x.UserName == userName);
        }

        public async Task<AppUser> GetById(int id)
        {
            return await _context.Users
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public void Update(AppUser appUser)
        {
            _context.Entry(appUser).State = EntityState.Modified;
        }
    }
}
