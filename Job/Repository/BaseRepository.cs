namespace Jobby.Repository
{
    public abstract class BaseRepository<T>
    {
        public string GenerateId(string identifier)
        {
            return $"{nameof(T)}-{identifier}";
        }
    }
}
