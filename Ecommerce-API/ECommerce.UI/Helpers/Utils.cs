using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ECommerce.UI.Helpers;

internal static class Utils
{
    internal static string FormatQueryWithPaginationParams(string baseUrl, int pageNumber, int pageSize,
        string? searchTerm, string? searchGenre)
    {
        var sb = new StringBuilder();
        sb.Append($"{baseUrl}?PageNumber={pageNumber}&PageSize={pageSize}");

        if (!string.IsNullOrWhiteSpace(searchTerm))
            sb.Append($"&SearchTerm={searchTerm}");
        if (!string.IsNullOrWhiteSpace(searchGenre))
            sb.Append($"&Genre={searchGenre}");

        return sb.ToString();
    }

    internal static JsonSerializerOptions GetJsonSerializerOptions()
    {
        return new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };
    }
}