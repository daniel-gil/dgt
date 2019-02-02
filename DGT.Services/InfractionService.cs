using DGT.Data.Abstract;
using DGT.Models;
using System.Collections.Generic;
using System.Linq;

namespace DGT.Services
{
    public interface IInfractionService
    {
        IEnumerable<Infraction> GetInfractions();
        Infraction GetInfraction(string id);
        void CreateInfraction(Infraction infraction);
        IEnumerable<Infraction> GetTopInfractions(int top);
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

        public IEnumerable<Infraction> GetTopInfractions(int top)
        {
            /*
            var winners = (from s in dc.Scores
                           from u in dc.Users
                           where (s.result) && (s.attackinguserid == u.userid)
                           group s by u
                            into groups
                           select new
                           {
                               Username = groups.Key.username,
                               Won = groups.Count()
                           }).OrderByDescending(x => x.Seire).Distinct().Take(25).ToList();

    */
            var list = infractionRepository.GetAll()
                                         .GroupBy(i => i.Id);
            /*  var list = (from r in all
                         .GroupBy(i => i.Id)
                         where t.DeliverySelection == true && t.Delivery.SentForDelivery == null
                         orderby t.Delivery.SubmissionDate
                         select t).Take(top);*/
            return null;
        }

        public void SaveInfraction()
        {
            infractionRepository.Commit();
        }
    }
}
