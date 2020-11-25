using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeamCatHotel.Models;

namespace TeamCatHotel.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Descuento> Descuento { get; set; }

        public DbSet<Persona> Persona { get; set; }

        public DbSet<Servicio> Servicio { get; set; }

        public DbSet<Cliente> Cliente { get; set; }

        public DbSet<Factura> Factura { get; set; }

        public DbSet<Habitacion> Habitacion { get; set; }

        public DbSet<LineaFactura> LineaFactura { get; set; }

        public DbSet<LineaPedido> LineaPedido { get; set; }

        public DbSet<Menu> Menu { get; set; }

        public DbSet<Pedido> Pedido { get; set; }

        public DbSet<Plato> Plato { get; set; }

        public DbSet<PlatoMenu> PlatoMenu { get; set; }

        public DbSet<Producto> Producto { get; set; }

        public DbSet<ProductoProveedor> ProductoProveedor { get; set; }

        public DbSet<Proveedor> Proveedor { get; set; }

        public DbSet<Reserva> Reserva { get; set; }

        public DbSet<ReservaHabitacion> ReservaHabitacion { get; set; }

        public DbSet<ReservaServicio> ReservaServicio { get; set; }
    }
}
