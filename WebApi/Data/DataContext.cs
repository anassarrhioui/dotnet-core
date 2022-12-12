using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data;

public class DataContext : DbContext
{

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
    }
    public DbSet<Product> Products { get; set; }
}