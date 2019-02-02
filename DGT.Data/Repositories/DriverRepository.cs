using DGT.Data.Abstract;
using DGT.Models;

namespace DGT.Data.Repositories
{
    public class DriverRepository : EntityBaseRepository<Driver>, IDriverRepository
    {
        public DriverRepository(DgtDbContext context)
            : base(context)
        { }
    }
}
