using DGT.Data.Abstract;
using DGT.Models;
using System.Collections.Generic;

namespace DGT.Services
{
    public interface IVehicleInfractionService
    {
        // IEnumerable<VehicleInfraction> GetVehiclesInfractions();
        // Vehicle GetVehicle(string id);
        // void CreateVehicle(Vehicle vehicle);
        void RegisterInfraction(Vehicle vehicle, Infraction infraction);
        void SaveVehicle();
    }

    public class VehicleInfractionService : IVehicleInfractionService
    {
        private readonly IVehicleRepository vehicleRepository;

        public VehicleInfractionService(IVehicleRepository vehicleRepository)
        {
            this.vehicleRepository = vehicleRepository;
        }

        /*
        public void CreateVehicle(Vehicle vehicle)
        {
            vehicleRepository.Add(vehicle);
            SaveVehicle();
        }

        public Vehicle GetVehicle(string id)
        {
           return vehicleRepository.GetSingle(s => s.Id == id);
        }

        public IEnumerable<Vehicle> GetVehicles()
        {
            return vehicleRepository.GetAll();
        }

    */
        public void RegisterInfraction(Vehicle vehicle, Infraction infraction)
        {

        }

        public void SaveVehicle()
        {
            vehicleRepository.Commit();
        }
    }
}
