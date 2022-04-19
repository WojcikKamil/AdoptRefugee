namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Done();
        IPersonRepository PersonRepository { get; }
        IAccommodationRepository AccommodationRepository { get; }
        IRefugeeRepository RefugeeRepository { get; }
        IBenefactorRepository BenefactorRepository { get; }
    }
}
