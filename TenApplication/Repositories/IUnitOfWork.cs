using TenApplication.Models;

namespace TenApplication.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IJobRepository Jobs { get; }
        Task Save();
    }
}
