using ECommerce.API.Models;
using ECommerce.Shared;

namespace ECommerce.API.Data;

public class DbSeeder
{
    public static async Task SeedItemsAsync(ApiDbContext db)
    {
        var westCoastHipHopTag = new Tag { TagName = "West Coast hip-hop" };
        var consciousRapTag = new Tag { TagName = "Conscious rap" };
        var femaleEmpowermentTag = new Tag { TagName = "Female empowerment" };
        var heartbreakTag = new Tag { TagName = "Heartbreak" };
        
        var sampleItems = new List<Item>
        {
            new()
            {
                Format = ItemFormat.Vinyl, Type = ItemType.Album, Artist = "Kendrick Lamar",
                Name = "Good Kid, m.A.A.d City (10th Anniversary Edition)", Price = 38.00m,
                Genre = "Hip-Hop",
                Tags = new List<Tag>{ westCoastHipHopTag, consciousRapTag }
            },
            
            new()
            {
                Format = ItemFormat.Vinyl, Type = ItemType.Album, Artist = "Lauryn Hill",
                Name = "The Miseducation of Lauryn Hill", Price = 29.98m, 
                Genre = "Neo Soul",
                Tags = new List<Tag>{ femaleEmpowermentTag, heartbreakTag }
            }
        };
        
        db.Items.AddRange(sampleItems);
        await db.SaveChangesAsync();
    }
}