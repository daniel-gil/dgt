using System.Threading.Tasks;

namespace DGT.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private DgtDbContext dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public DgtDbContext DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Init(null)); }
        }

        public void Commit()
        {
            //DbContext.Commit();
        }

        public async Task CompleteAsync()
        {
            await DbContext.SaveChangesAsync();
        }

        public bool IsContextValid(ref string strError)
        {
            //System.Collections.Generic.IEnumerable<System.Data.Entity.Validation.DbEntityValidationResult> valErrors = dbContext.GetValidationErrors();

            //if (valErrors == null || ((System.Collections.Generic.List < System.Data.Entity.Validation.DbEntityValidationResult > )valErrors).Count == 0) return true;
            //strError = "";
            //foreach (System.Data.Entity.Validation.DbEntityValidationResult valError in valErrors)
            //{
            //    var errorMessages = valError.ValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);

            //    strError += " | " + valError.ValidationErrors.GetEnumerator().MoveNext.c
            //}
            return false;
        }
    }
}
