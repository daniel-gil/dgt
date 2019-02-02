using System;
using System.Collections.Generic;
//using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DGT.Data.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace DGT.Data.Infrastructure
{
    public abstract class RepositoryBase<T> where T : class
    {
        #region Properties
        private DgtDbContext dataContext;
        protected readonly DbSet<T> dbSet;

       private string conString;
        //private DbContextOptions<DgtDbContext> options;
        /*

      options.UseSqlServer(Configuration.GetConnectionString("DgtDbContext"),
         sqlServerOptionsAction: sqlOptions =>
         {
             sqlOptions.EnableRetryOnFailure(
             maxRetryCount: 10,
             maxRetryDelay: TimeSpan.FromSeconds(30),
             errorNumbersToAdd: null);
         });
      * */


        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected DgtDbContext DbContext
        {
            get {
               
                    DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<DgtDbContext>();
                    optionsBuilder.UseSqlServer(conString);


                // optionsBuilder.Options
                return dataContext ?? (dataContext = DbFactory.Init(null));
            }
        }
        #endregion

        protected RepositoryBase(IDbFactory dbFactory, string connectionString)
        {
            /*
            if (dataContext == null) {
                DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<DgtDbContext>();
                optionsBuilder.UseSqlServer(connectionString);
                options = optionsBuilder.Options;
            }*/
            conString = connectionString;

            DbFactory = dbFactory;
            dbSet = DbContext.Set<T>();



            /// Configuration.GetConnectionString("DefaultConnection")

            /*
string conString = Microsoft
   .Extensions
   .Configuration
   .ConfigurationExtensions
   .GetConnectionString(this.Configuration, "DefaultConnection");

            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

            options = optionsBuilder.Options;

            */
        }

        /*
        public static IConfigurationRoot GetConfiguration(string path, string environmentName = null, bool addUserSecrets = false)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            if (!String.IsNullOrWhiteSpace(environmentName))
            {
                builder = builder.AddJsonFile($"appsettings.{environmentName}.json", optional: true);
            }

            builder = builder.AddEnvironmentVariables();

            if (addUserSecrets)
            {
                builder.AddUserSecrets(); // requires adding Microsoft.Extensions.Configuration.UserSecrets from NuGet.
            }

            return builder.Build();
        }
        */

        #region Implementation
        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbSet.Remove(obj);
        }

        public virtual T GetById(string id)
        {
            return dbSet.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public async virtual Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            try
            {
                IQueryable<T> tmpData = dbSet.Where(where);
                if (tmpData == null)
                {
                    return null;
                }
                return tmpData.ToList();
            }
            catch (Exception /*ex*/)
            {
                return null;
            }
        }

        public virtual async Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> where)
        {
            try
            {
                IQueryable<T> tmpData = dbSet.Where(where);
                if (tmpData == null)
                {
                    return null;
                }
                IEnumerable<T> list = await tmpData.ToListAsync(); ;
                return list;
            }
            catch (Exception /*ex*/)
            {
                return null;
            }
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).FirstOrDefault<T>();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> where)
        {
            return await dbSet.Where(where).FirstOrDefaultAsync<T>();
        }

        #endregion
    }
}
