using DGT.Data.Abstract;
using DGT.Models;
using System.Collections.Generic;

namespace DGT.Services
{
    public interface IVehicleInfractionService
    {
        IEnumerable<VehicleInfraction> GetVehicleInfractions();
        IEnumerable<VehicleInfraction> GetInfractionsByVehicle(string vehicleId);
        VehicleInfraction GetVehicleInfraction(string vehicleInfractionId);
        void RegisterInfraction(VehicleInfraction vehicleInfraction);
        void SaveVehicle();
    }

    public class VehicleInfractionService : IVehicleInfractionService
    {
        private readonly IVehicleInfractionRepository vehicleInfractionRepository;

        public VehicleInfractionService(
             IVehicleInfractionRepository vehicleInfractionRepository)
        {
            this.vehicleInfractionRepository = vehicleInfractionRepository;
        }

        public IEnumerable<VehicleInfraction> GetInfractionsByVehicle(string vehicleId)
        {
            return vehicleInfractionRepository.FindBy(s => s.VehicleId == vehicleId);
        }

        public VehicleInfraction GetVehicleInfraction(string vehicleInfractionId)
        {
            return vehicleInfractionRepository.GetSingle(s => s.Id == vehicleInfractionId);
        }

        public IEnumerable<VehicleInfraction> GetVehicleInfractions()
        {
            return vehicleInfractionRepository.GetAll();
        }


        public void RegisterInfraction(VehicleInfraction vehicleInfraction)
        {
            vehicleInfractionRepository.Add(vehicleInfraction);
            SaveVehicle();
        }

        public void SaveVehicle()
        {
            vehicleInfractionRepository.Commit();
        }
    }
}
