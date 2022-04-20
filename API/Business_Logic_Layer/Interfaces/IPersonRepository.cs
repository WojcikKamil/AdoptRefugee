using API.DTO;
using API.Entities;
using Data_Access_Layer.Entities;

namespace API.Interfaces
{
    public interface IPersonRepository
    {
        void Update(AppUser appUser);
        Task<AppUser> GetByEmail(string userName);
        Task<AppUser> GetById(int id);
        void AddRefugeeData(Refugee refugee);
        void AddBenefactorData(Benefactor benefactor);
        void AddComrades(Comrades comrades);
    }
}
