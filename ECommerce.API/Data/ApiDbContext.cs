using Microsoft.EntityFrameworkCore;
using ECommerce.API.Models;
using Microsoft.EntityFrameworkCore.Design;

namespace ECommerce.API.Data;

public class ApiDbContext(DbContextOptions<ApiDbContext> options) : DbContext(options)
{
    public DbSet<Item> Items { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Sale> Sales { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>().HasQueryFilter(i => !i.IsDeleted);
        modelBuilder.Entity<Sale>().HasQueryFilter(s => !s.IsDeleted);
        modelBuilder.Entity<Tag>().HasQueryFilter(t => !t.IsDeleted);
        
        modelBuilder.Entity<Sale>().Ignore(s => s.TotalPrice);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<Enum>()
            .HaveConversion<string>();
    }
}

public class ApiDbContextFactory : IDesignTimeDbContextFactory<ApiDbContext>
{
    public ApiDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApiDbContext>();
        optionsBuilder.UseSqlite(DbConfig.GetConnectionString());
        return new ApiDbContext(optionsBuilder.Options);
    }
}