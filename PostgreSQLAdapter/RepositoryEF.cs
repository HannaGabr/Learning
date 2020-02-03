using System;
using System.Threading.Tasks;
using Jobby.Repository;
using Jobby.Repository.Interfaces;

namespace Jobby.Infra.Persistence.EF
{
    public class RepositoryEF<T>: BaseRepository<T>, IRepository<T> where T : class
    {
        protected readonly JobTrackingContext dbContext;

        public RepositoryEF(JobTrackingContext context)
        {
            dbContext = context;
        }

        public string NextIdentity()
        {
            return GenerateId(Guid.NewGuid().ToString());
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await dbContext.FindAsync<T>(id);
        }

        public void Add(T entity)
        {
            dbContext.Add(entity);
        }

        public void Update(T entity)
        {
            dbContext.Update(entity);
        }

        public async Task RemoveAsync(string id)
        {
            T entity = await GetByIdAsync(id);
            if (entity != null)
            {
                dbContext.Remove(entity);
            }
        }
    }
}
