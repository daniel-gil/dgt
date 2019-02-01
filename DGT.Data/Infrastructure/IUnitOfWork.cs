using System.Threading.Tasks;

namespace DGT.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
        Task CompleteAsync();
    }
}
