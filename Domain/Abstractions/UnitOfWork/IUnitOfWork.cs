namespace ChatApplication.Domain.Abstractions.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Complete();
    }
}
