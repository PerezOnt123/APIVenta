// Contexto Context.cs
using Microsoft.EntityFrameworkCore;

public class VentasContext : DbContext
{
    public DbSet<Venta> Ventas { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseInMemoryDatabase("VentasDB");
    }
}