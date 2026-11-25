using Microsoft.EntityFrameworkCore;
using AgroManager.Models;


namespace AgroManager.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<PedidoDetalle> PedidoDetalles { get; set; } = null!;
    public DbSet<Pedido> Pedidos { get; set; } = null!;
    public DbSet<MovimientoInventario> MovimientoInventarios  { get; set; } = null!;
    public DbSet<Marca> Marcas { get; set; } = null!;
    public DbSet<Cliente> Clientes  { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<LotePallet> LotePallets { get; set; } = null!;
    public DbSet<EntregaProgramada> EntregasProgramadas { get; set; } = null!;
    public DbSet<Proveedor> Proveedores { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        // User -> configuración de nombre único
        modelBuilder.Entity<User>()
            .Property(u => u.Name)
            .UseCollation("NOCASE");

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Name)
            .IsUnique();
        // Cliente -> Pedido
        modelBuilder.Entity<Pedido>()
            .HasOne(p => p.Cliente)
            .WithMany(c => c.Pedidos)
            .HasForeignKey(p => p.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        // Pedido -> PedidoDetalle
        modelBuilder.Entity<PedidoDetalle>()
            .HasOne(pd => pd.Pedido)
            .WithMany(p => p.PedidoDetalles)
            .HasForeignKey(pd => pd.PedidoId)
            .OnDelete(DeleteBehavior.Cascade);

        // Product -> PedidoDetalle
        modelBuilder.Entity<PedidoDetalle>()
            .HasOne(pd => pd.Product)
            .WithMany(p => p.PedidoDetalles)
            .HasForeignKey(pd => pd.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        // Product -> LotePallet
        modelBuilder.Entity<LotePallet>()
            .HasOne(lp => lp.Product)
            .WithMany(p => p.LotePallets)
            .HasForeignKey(lp => lp.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        // LotePallet -> MovimientoInventario
        modelBuilder.Entity<MovimientoInventario>()
            .HasOne(mi => mi.LotePallet)
            .WithMany(lp => lp.MovimientoInventarios)
            .HasForeignKey(mi => mi.LotePalletId)
            .OnDelete(DeleteBehavior.Restrict);

        // Product -> MovimientoInventario
        modelBuilder.Entity<MovimientoInventario>()
            .HasOne(mi => mi.Product)
            .WithMany(p => p.MovimientoInventarios)
            .HasForeignKey(mi => mi.ProductoId)
            .OnDelete(DeleteBehavior.Restrict);

        // Product -> EntregaProgramada
        modelBuilder.Entity<EntregaProgramada>()
            .HasOne(ep => ep.Product)
            .WithMany(p => p.EntregasProgramadas)
            .HasForeignKey(ep => ep.ProductoId)
            .OnDelete(DeleteBehavior.Restrict);

        // Proveedor -> EntregaProgramada
        modelBuilder.Entity<EntregaProgramada>()
            .HasOne(ep => ep.Proveedor)
            .WithMany(p => p.EntregasProgramadas)
            .HasForeignKey(ep => ep.ProveedorId)
            .OnDelete(DeleteBehavior.Restrict);

        // PedidoDetalle -> MovimientoInventario
        modelBuilder.Entity<MovimientoInventario>()
            .HasOne(mi => mi.PedidoDetalle)
            .WithMany(pd => pd.MovimientoInventarios)
            .HasForeignKey(mi => mi.PedidoDetalleId)
            .OnDelete(DeleteBehavior.Restrict);

        // Category -> Product
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // Marca -> Product
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Marca)
            .WithMany(m => m.Products)
            .HasForeignKey(p => p.MarcaId)
            .OnDelete(DeleteBehavior.Restrict);

        // EntregaProgramada -> MovimientoInventario
        modelBuilder.Entity<MovimientoInventario>()
            .HasOne(mi => mi.EntregaProgramada)
            .WithMany()
            .HasForeignKey(mi => mi.EntregaProgramadaId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // User -> MovimientoInventario
        modelBuilder.Entity<MovimientoInventario>()
            .HasOne(mi => mi.User)
            .WithMany(u => u.MovimientoInventarios)
            .HasForeignKey(mi => mi.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}