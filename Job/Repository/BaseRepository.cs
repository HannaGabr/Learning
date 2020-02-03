namespace Jobby.Repository
{
    public class BaseRepository<T>
    {
        public string GenerateId(string identifier)
        {
            return $"{nameof(T)}-{identifier}";
        }
    }
}
