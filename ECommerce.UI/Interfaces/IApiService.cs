namespace ECommerce.UI.Interfaces;

public interface IApiService
{
    public Task<string> GetAsync(string path);
}