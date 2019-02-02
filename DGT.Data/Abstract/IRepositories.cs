using DGT.Models;

namespace DGT.Data.Abstract
{
    public interface IDriverRepository : IEntityBaseRepository<Driver> { }

    public interface IVehicleRepository : IEntityBaseRepository<Vehicle> { }

    public interface IInfractionRepository : IEntityBaseRepository<Infraction> { }

    public interface IVehicleInfractionRepository : IEntityBaseRepository<VehicleInfraction> { }

    public interface IVehicleDriverRepository : IEntityBaseRepository<VehicleDriver> { }
    
}
