using DGT.Data.Abstract;
using DGT.Models;
using System.Collections.Generic;

namespace DGT.Services
{
    public interface IVehicleDriverService
    {
        IEnumerable<Vehicle> GetVehiclesByDriver(string driverId);
        IEnumerable<Driver> GetDriversByVehicle(string vehicleId);
        string CreateVehicleDriver(VehicleDriver vehicleDriver);
        void SaveVehicle();
    }

    public class VehicleDriverService : IVehicleDriverService
    {
        private readonly IVehicleDriverRepository vehicleDriverRepository;
        private readonly IVehicleService vehicleService;
        private readonly IDriverService driverService;

        public VehicleDriverService(
            IVehicleDriverRepository vehicleDriverRepository,
            IVehicleService vehicleService,
            IDriverService driverService)
        {
            this.vehicleDriverRepository = vehicleDriverRepository;
            this.vehicleService = vehicleService;
            this.driverService = driverService;
        }


        public IEnumerable<Vehicle> GetVehiclesByDriver(string driverId)
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            var list = vehicleDriverRepository.FindBy(s => s.DriverId == driverId);

            foreach (VehicleDriver vehicleDriver in list)
            {
                var vehicle = vehicleService.GetVehicle(vehicleDriver.VehicleId);
                vehicles.Add(vehicle);
            }
            return vehicles;
        }

        public IEnumerable<Driver> GetDriversByVehicle(string vehicleId)
        {
            List<Driver> drivers = new List<Driver>();
            var list = vehicleDriverRepository.FindBy(s => s.VehicleId == vehicleId);

            foreach (VehicleDriver vehicleDriver in list)
            {
                var driver = driverService.GetDriver(vehicleDriver.DriverId);
                drivers.Add(driver);
            }
            return drivers;
        }

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
