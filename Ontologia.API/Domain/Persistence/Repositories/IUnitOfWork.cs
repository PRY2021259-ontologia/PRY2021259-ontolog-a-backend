namespace Ontologia.API.Domain.Persistence.Repositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
