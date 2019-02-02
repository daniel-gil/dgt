using DGT.Data.Abstract;
using DGT.Models;

namespace DGT.Data.Repositories
{
    public class VehicleInfractionRepository : EntityBaseRepository<VehicleInfraction>, IVehicleInfractionRepository
    {
        public VehicleInfractionRepository(DgtDbContext context)
            : base(context)
        { }
    }
}
