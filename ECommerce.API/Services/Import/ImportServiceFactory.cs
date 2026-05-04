using ECommerce.API.Interfaces.Import;

namespace ECommerce.API.Services.Import;

public class ImportServiceFactory(IConfiguration configuration, IServiceProvider provider)
{
    public IImportService Create()
    {
        var filePath = configuration["Seeding:FilePath"] ??
                       throw new ArgumentNullException(
                           nameof(configuration), "Could not extract Seeding:FilePath from appSettings.json");
        var extension = Path.GetExtension(filePath).ToLower();

        return extension switch
        {
            ".xlsx" => provider.GetRequiredService<XlsxService>(),
            _ => throw new NotSupportedException($"The file extension {extension} is not supported.")
        };
    }
}