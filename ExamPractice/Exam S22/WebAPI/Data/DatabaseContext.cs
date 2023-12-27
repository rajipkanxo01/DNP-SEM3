using Domains;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Album> Albums { get; set; }
    public DbSet<Image> Images { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = Exam.db");
    }
}