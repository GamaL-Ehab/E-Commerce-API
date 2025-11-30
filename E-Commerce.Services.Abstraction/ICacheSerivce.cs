namespace E_Commerce.Services.Abstraction
{
    public interface ICacheSerivce
    {
        Task SetAsync(string key, object value, TimeSpan duration);
        Task<string> GetAsync(string key);
    }
}
