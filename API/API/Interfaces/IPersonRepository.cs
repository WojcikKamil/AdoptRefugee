using API.Entities;

namespace API.Interfaces
{
    public interface IPersonRepository
    {
        void Update(AppUser appUser);
        Task<AppUser> GetByEmail(string userName);
        void AddData(Person person);
        void AddComrades(Comrades comrades);
        Task<AppUser> GetFamily(string userName);
    }
}
