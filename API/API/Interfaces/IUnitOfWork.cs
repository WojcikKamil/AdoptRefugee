namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Done();
        IPersonRepository PersonRepository { get; }
    }
}
