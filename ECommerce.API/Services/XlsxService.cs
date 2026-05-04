using ClosedXML.Excel;
using ECommerce.Shared;
using ECommerce.API.Interfaces;
using ECommerce.API.Models;

namespace ECommerce.API.Services;

public class XlsxService(IConfiguration configuration) : IImportService
{
    private readonly string _filePath = Environment.ExpandEnvironmentVariables(configuration["Seeding:FilePath"] ?? throw new ArgumentNullException(nameof(configuration), message: "Could not find Seeding:FilePath in appSettings.json."));
    
    public SeedData GetSeedData()
    {
        using var workbook = new XLWorkbook(_filePath);
        
        var tags = GetTagsFromXlsx(workbook);
        var items = GetItemsFromXlsx(tags, workbook);
        var sales  = GetSalesFromXlsx(items, workbook);

        return new SeedData
        {
            Tags = tags,
            Items = items,
            Sales = sales
        };
    }
    
    //------ Extractor Methods -------
    private List<Tag> GetTagsFromXlsx(XLWorkbook workbook)
    {
        var range = GetRange(workbook, "Tags");
        var headers = GetHeaderDictionary(range);
        var rows = range.RowsUsed().Skip(1);
        
        return rows.Select(row => new Tag
        {
            TagName = row.Cell(headers["name"]).GetValue<string>()
        }).ToList();
    }
    
    private List<Item> GetItemsFromXlsx(List<Tag> tags, XLWorkbook workbook)
    {
        var range = GetRange(workbook, "Items");
        var headers = GetHeaderDictionary(range);

        var rows = range.RowsUsed().Skip(1);
        
        return rows.Select(row =>
        {
            var tagNames = row.Cell(headers["tags"]).GetValue<string>()
                .Split(',')
                .Select(n => n.Trim());
            
            var matchedTags = tags
                .Where(t => tagNames.Contains(t.TagName))
                .ToList();
            
            return new Item
            {
                Name = row.Cell(headers["name"]).GetValue<string>(),
                Artist = row.Cell(headers["artist"]).GetValue<string>(),
                Price = row.Cell(headers["price"]).GetValue<decimal>(),
                Type = row.Cell(headers["type"]).GetValue<ItemType>(),
                Format = row.Cell(headers["format"]).GetValue<ItemFormat>(),
                Genre = row.Cell(headers["genre"]).GetValue<string>(),
                Tags = matchedTags
            };
        }).ToList();
    }

    private List<Sale> GetSalesFromXlsx(List<Item> items, XLWorkbook workbook)
    {
        var range = GetRange(workbook, "Sales");
        var headers = GetHeaderDictionary(range);
        var rows = range.RowsUsed().Skip(1);

        return rows.Select(row =>
        {
            var itemEntries = row.Cell(headers["sale"]).GetValue<string>()
                .Split(',')
                .Select(entry => entry.Trim().Split(':'))
                .ToList();

            var matchedItems = itemEntries.Select(parts =>
            {
                var name = parts[0].Trim();
                var quantity = int.Parse(parts[1].Trim());
                var item = items.FirstOrDefault(i => i.Name == name);

                return new SaleItem
                {
                    Item = item!,
                    Quantity = quantity
                };
            }).Where(si => si.Item != null!).ToList();

            return new Sale
            {
                SoldItems = matchedItems
            };
        }).ToList();
    }

    //------- Helper Methods -------
    private IXLRange GetRange(XLWorkbook workbook, string worksheetName)
    {
        var sheet = workbook.Worksheet(worksheetName);
        return sheet.RangeUsed() ?? throw new InvalidOperationException($"Extracted a null range for worksheet name {worksheetName}");
    }

    private Dictionary<string, int> GetHeaderDictionary(IXLRange range)
    {
        var headerRow = range.RowsUsed().First();
        return headerRow.Cells()
            .ToDictionary(
                c => c.GetValue<string>().Trim().ToLower(),
                c => c.Address.ColumnNumber);
    }
}