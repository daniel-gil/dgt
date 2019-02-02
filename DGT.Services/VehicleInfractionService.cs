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
        int RegisterInfraction(VehicleInfraction vehicleInfraction);
        void SaveVehicle();
    }

    public class VehicleInfractionService : IVehicleInfractionService
    {
        private readonly IVehicleInfractionRepository vehicleInfractionRepository;
        private readonly IVehicleService vehicleService;
        private readonly IDriverService driverService;
        private readonly IInfractionService infractionService;

        public VehicleInfractionService(
             IVehicleInfractionRepository vehicleInfractionRepository,
             IVehicleService vehicleService,
             IDriverService driverService,
             IInfractionService infractionService)
        {
            this.vehicleInfractionRepository = vehicleInfractionRepository;
            this.vehicleService = vehicleService;
            this.driverService = driverService;
            this.infractionService = infractionService;
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


        public int RegisterInfraction(VehicleInfraction vehicleInfraction)
        {
            // first register a new infraction linked to the vehicle
            vehicleInfractionRepository.Add(vehicleInfraction);
            SaveVehicle();

            // second, retrieve the infraction to know the penalty points
            var infraction = infractionService.GetInfraction(vehicleInfraction.InfractionId);
           
            // and finally update the driver points
            var driver = driverService.GetDriver(vehicleInfraction.DriverId);
            if (driver.Points > infraction.PointsToDiscount)
            {
                driver.Points -= infraction.PointsToDiscount;
            }
            else
            {
                driver.Points = 0;
            }
            driverService.UpdateDriver(driver);

            return driver.Points;
        }

        public void SaveVehicle()
        {
            vehicleInfractionRepository.Commit();
        }
    }
}
