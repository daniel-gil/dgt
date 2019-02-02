using DGT.Data.Abstract;
using DGT.Models;

namespace DGT.Data.Repositories
{
    public class InfractionRepository : EntityBaseRepository<Infraction>, IInfractionRepository
    {
        public InfractionRepository(DgtDbContext context)
            : base(context)
        { }
    }
}
