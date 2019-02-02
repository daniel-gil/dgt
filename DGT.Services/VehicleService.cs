using DGT.Data.Abstract;
using DGT.Models;
using System.Collections.Generic;

namespace DGT.Services
{
    public interface IVehicleService
    {
        IEnumerable<Vehicle> GetVehicles();
        Vehicle GetVehicle(string id);
        void CreateVehicle(Vehicle vehicle);
        void SaveVehicle();
    }

    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            this.vehicleRepository = vehicleRepository;
        }

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

        public void SaveVehicle()
        {
            vehicleRepository.Commit();
        }
    }
}
