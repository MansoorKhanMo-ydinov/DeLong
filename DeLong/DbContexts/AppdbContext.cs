using DeLong.Entities.Incomes;
using DeLong.Entities.Informs;
using DeLong.Entities.Products;
using DeLong.Entities.Roles;
using DeLong.Entities.Users;
using DeLong.Entities.Warehouses;
using Microsoft.EntityFrameworkCore;

namespace DeLong.DbContexts;

public class AppdbContext:DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Inform> Informs { get; set; }
    public DbSet<Kirim> Kirims { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // PostgreSQL ulanish satrini kiriting
        optionsBuilder.UseNpgsql("Host=LocalHost;Port=5432;Database=MansurjonDb;Username=postgres;Password=mansurjon1607");
    }
}
