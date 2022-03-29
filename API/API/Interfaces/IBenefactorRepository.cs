using API.DTO;

namespace API.Interfaces
{
    public interface IBenefactorRepository
    {
        Task<IEnumerable<PersonDTO>> GetHomelessRefuges();
        Task<IEnumerable<PersonDTO>> GetFamily(int id);
    }
}
