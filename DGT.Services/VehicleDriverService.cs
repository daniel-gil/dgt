using DGT.Data.Abstract;
using DGT.Models;
using System.Collections.Generic;

namespace DGT.Services
{
    public interface IVehicleDriverService
    {
       // IEnumerable<VehicleDriver> GetVehicleDrivers();
       // IEnumerable<VehicleDriver> GetInfractionsByVehicle(string vehicleId);
       // VehicleDriver GetVehicleDriver(string vehicleDriverId);
        string CreateVehicleDriver(VehicleDriver vehicleDriver);
        void SaveVehicle();
    }

    public class VehicleDriverService : IVehicleDriverService
    {
        private readonly IVehicleDriverRepository vehicleDriverRepository;
        private readonly IDriverService driverService;

        public VehicleDriverService(
            IVehicleDriverRepository vehicleDriverRepository,
            IDriverService driverService)
        {
            this.vehicleDriverRepository = vehicleDriverRepository;
            this.driverService = driverService;
        }

        /*
        public IEnumerable<VehicleDriver> GetInfractionsByVehicle(string vehicleId)
        {
            return vehicleDriverRepository.FindBy(s => s.VehicleId == vehicleId);
        }

        public VehicleDriver GetVehicleDriver(string vehicleDriverId)
        {
            return vehicleDriverRepository.GetSingle(s => s.Id == vehicleDriverId);
        }

        public IEnumerable<VehicleDriver> GetVehicleDrivers()
        {
            return vehicleDriverRepository.GetAll();
        }


        public void RegisterInfraction(VehicleDriver vehicleDriver)
        {
            vehicleDriverRepository.Add(vehicleDriver);
            SaveVehicle();
        }
        */

        public string CreateVehicleDriver(VehicleDriver vehicleDriver)
        {
            // check if exists the driver
            var driver = driverService.GetDriver(vehicleDriver.DriverId);
            if (driver == null)
            {
                return "driver not found";
            }

            // check if the driver is allowed to have one more vehicle
            if (driver.NumVehicles >= 10)
            {
                return "this driver has reached the maximal number of vehicles (10)";
            }

            // link the driver to the vehicle 
            vehicleDriverRepository.Add(vehicleDriver);
            SaveVehicle();

            // update the number of vehicles linked to this driver
            driver.NumVehicles++;
            driverService.UpdateDriver(driver);

            return "";
        }

        public void SaveVehicle()
        {
            vehicleDriverRepository.Commit();
        }
    }
}
