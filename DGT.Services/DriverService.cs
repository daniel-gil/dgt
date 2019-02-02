using DGT.Data.Abstract;
using DGT.Models;
using System.Collections.Generic;

namespace DGT.Services
{
    public interface IDriverService
    {
        IEnumerable<Driver> GetDrivers();
        Driver GetDriver(string id);
        void CreateDriver(Driver driver);
        void SaveDriver();
    }

    public class DriverService : IDriverService
    {
        private readonly IDriverRepository driverRepository;

        public DriverService(IDriverRepository driverRepository)
        {
            this.driverRepository = driverRepository;
        }

        public void CreateDriver(Driver driver)
        {
            driverRepository.Add(driver);
            SaveDriver();
        }

        public Driver GetDriver(string id)
        {
           return driverRepository.GetSingle(s => s.Id == id);
        }

        public IEnumerable<Driver> GetDrivers()
        {
            return driverRepository.GetAll();
        }

        public void SaveDriver()
        {
            driverRepository.Commit();
        }
    }
}
