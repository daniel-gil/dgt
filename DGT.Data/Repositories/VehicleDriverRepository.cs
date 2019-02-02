using DGT.Data.Abstract;
using DGT.Models;

namespace DGT.Data.Repositories
{
    public class VehicleDriverRepository : EntityBaseRepository<VehicleDriver>, IVehicleDriverRepository
    {
        public VehicleDriverRepository(DgtDbContext context)
            : base(context)
        { }
    }
}
