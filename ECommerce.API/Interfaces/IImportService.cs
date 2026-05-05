using ECommerce.API.Models;

namespace ECommerce.API.Interfaces;

public interface IImportService
{
    public SeedData GetSeedData();
}