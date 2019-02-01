using DGT.Data.Infrastructure;
using DGT.Models;

namespace DGT.Data.Repositories
{
    public class DriverRepository : RepositoryBase<Driver>, IDriverRepository
    {
        public DriverRepository(IDbFactory dbFactory, string conString)
            : base(dbFactory, conString)
        { }
    }

    public interface IDriverRepository : IRepository<Driver>
    {

    }
}
