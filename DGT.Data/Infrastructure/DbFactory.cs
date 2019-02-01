using Microsoft.EntityFrameworkCore;

namespace DGT.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        DgtDbContext dbContext;

        public DgtDbContext Init(DbContextOptions<DgtDbContext> options)
        {
            return dbContext ?? (dbContext = new DgtDbContext(options));
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
