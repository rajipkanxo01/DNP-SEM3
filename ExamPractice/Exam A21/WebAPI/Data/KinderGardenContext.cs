using Domains;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Data;

public class KinderGardenContext : DbContext
{
    public DbSet<Child> Childs { get; set; }
    public DbSet<Toy> Toys { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = Exam.db");
    }
}