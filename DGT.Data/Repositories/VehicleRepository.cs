using DGT.Data.Abstract;
using DGT.Models;

namespace DGT.Data.Repositories
{
    public class VehicleRepository : EntityBaseRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(DgtDbContext context)
            : base(context)
        { }
    }
}
