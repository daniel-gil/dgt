using DGT.Data.Abstract;
using DGT.Models;
using System.Collections.Generic;

namespace DGT.Services
{
    public interface IInfractionService
    {
        IEnumerable<Infraction> GetInfractions();
        Infraction GetInfraction(string id);
        void CreateInfraction(Infraction infraction);
        void SaveInfraction();
    }

    public class InfractionService : IInfractionService
    {
        private readonly IInfractionRepository infractionRepository;

        public InfractionService(IInfractionRepository infractionRepository)
        {
            this.infractionRepository = infractionRepository;
        }

        public void CreateInfraction(Infraction infraction)
        {
            infractionRepository.Add(infraction);
            SaveInfraction();
        }

        public Infraction GetInfraction(string id)
        {
           return infractionRepository.GetSingle(s => s.Id == id);
        }

        public IEnumerable<Infraction> GetInfractions()
        {
            return infractionRepository.GetAll();
        }

        public void SaveInfraction()
        {
            infractionRepository.Commit();
        }
    }
}
