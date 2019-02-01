using DGT.Data.Infrastructure;
using DGT.Data.Repositories;
using DGT.Models;
using System;
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
        private readonly IUnitOfWork unitOfWork;

        public DriverService(IDriverRepository driverRepository,
                             IUnitOfWork unitOfWork)
        {
            this.driverRepository = driverRepository;
            this.unitOfWork = unitOfWork;
        }

        public void CreateDriver(Driver driver)
        {
            driverRepository.Add(driver);
        }

        public Driver GetDriver(string id)
        {
           return driverRepository.GetById(id);
        }

        public IEnumerable<Driver> GetDrivers()
        {
            return driverRepository.GetAll();
        }

        public void SaveDriver()
        {
            unitOfWork.Commit();
        }
    }
}
