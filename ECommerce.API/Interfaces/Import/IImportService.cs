using ECommerce.API.Models;

namespace ECommerce.API.Interfaces.Import;

public interface IImportService
{
    public SeedData GetSeedData();
}