using System.Threading.Tasks;

namespace Jobby.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        string NextIdentity();

        void Add(T entity);
        Task<T> GetByIdAsync(string id);
        Task RemoveAsync(string id);
        void Update(T entity);
    }
}
