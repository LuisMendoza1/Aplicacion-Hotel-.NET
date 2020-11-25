using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TeamCatHotel.Data;

namespace TeamCatHotel.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20161029175541_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("TeamCatHotel.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("TeamCatHotel.Models.Descuento", b =>
                {
                    b.Property<int>("idDescuento")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("descripcion")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 200);

                    b.Property<DateTime>("fechaFin");

                    b.Property<DateTime>("fechaInicio");

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int>("porcentaje");

                    b.HasKey("idDescuento");

                    b.ToTable("Descuento");
                });

            modelBuilder.Entity("TeamCatHotel.Models.Factura", b =>
                {
                    b.Property<int>("idFactura")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("abono");

                    b.Property<DateTime>("fechaEmision");

                    b.Property<DateTime>("fechaPago");

                    b.Property<int?>("idReserva");

                    b.HasKey("idFactura");

                    b.HasIndex("idReserva");

                    b.ToTable("Factura");
                });

            modelBuilder.Entity("TeamCatHotel.Models.Habitacion", b =>
                {
                    b.Property<int>("numero")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("aforo");

                    b.Property<string>("descripcion")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 200);

                    b.Property<string>("localizacion")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<bool>("ocupada");

                    b.Property<float>("precio");

                    b.HasKey("numero");

                    b.ToTable("Habitacion");
                });

            modelBuilder.Entity("TeamCatHotel.Models.LineaFactura", b =>
                {
                    b.Property<int>("idLineaFactura")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("idFactura");

                    b.Property<int?>("idReservaServicio");

                    b.Property<float>("precio");

                    b.HasKey("idLineaFactura");

                    b.HasIndex("idFactura");

                    b.HasIndex("idReservaServicio")
                        .IsUnique();

                    b.ToTable("LineaFactura");
                });

            modelBuilder.Entity("TeamCatHotel.Models.LineaPedido", b =>
                {
                    b.Property<int>("idLineaPedido")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("cantidad");

                    b.Property<int?>("idPedido");

                    b.Property<int?>("idProducto");

                    b.Property<float>("precio");

                    b.HasKey("idLineaPedido");

                    b.HasIndex("idPedido");

                    b.HasIndex("idProducto");

                    b.ToTable("LineaPedido");
                });

            modelBuilder.Entity("TeamCatHotel.Models.Pedido", b =>
                {
                    b.Property<int>("idPedido")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("fechaEmision");

                    b.Property<DateTime>("fechaRecepcion");

                    b.Property<int?>("idProveedor");

                    b.HasKey("idPedido");

                    b.HasIndex("idProveedor");

                    b.ToTable("Pedido");
                });

            modelBuilder.Entity("TeamCatHotel.Models.Persona", b =>
                {
                    b.Property<int>("idPersona")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("apellidos")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("codigoPostal");

                    b.Property<string>("correoElectronico")
                        .IsRequired();

                    b.Property<string>("direccion")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<int?>("idProveedor");

                    b.Property<string>("localidad")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("nif")
                        .IsRequired();

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("pais")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("provincia")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<int>("telefono");

                    b.HasKey("idPersona");

                    b.HasIndex("idProveedor");

                    b.ToTable("Persona");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Persona");
                });

            modelBuilder.Entity("TeamCatHotel.Models.Plato", b =>
                {
                    b.Property<int>("idPlato")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("idPlato");

                    b.ToTable("Plato");
                });

            modelBuilder.Entity("TeamCatHotel.Models.PlatoMenu", b =>
                {
                    b.Property<int>("idPlatoMenu")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("idMenu");

                    b.Property<int?>("idPlato");

                    b.HasKey("idPlatoMenu");

                    b.HasIndex("idMenu");

                    b.HasIndex("idPlato");

                    b.ToTable("PlatoMenu");
                });

            modelBuilder.Entity("TeamCatHotel.Models.Producto", b =>
                {
                    b.Property<int>("idProducto")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("cantidad");

                    b.Property<string>("categoria")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 60);

                    b.Property<string>("descripcion")
                        .HasAnnotation("MaxLength", 60);

                    b.Property<int>("limiteMinimo");

                    b.Property<string>("localizador")
                        .IsRequired();

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 60);

                    b.Property<float>("precio");

                    b.HasKey("idProducto");

                    b.ToTable("Producto");
                });

            modelBuilder.Entity("TeamCatHotel.Models.ProductoProveedor", b =>
                {
                    b.Property<int>("idProductoProveedor")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("idProducto");

                    b.Property<int?>("idProveedor");

                    b.Property<float>("precio");

                    b.HasKey("idProductoProveedor");

                    b.HasIndex("idProducto");

                    b.HasIndex("idProveedor");

                    b.ToTable("ProductoProveedor");
                });

            modelBuilder.Entity("TeamCatHotel.Models.Proveedor", b =>
                {
                    b.Property<int>("idProveedor")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("cif")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.Property<string>("correoElectronico");

                    b.Property<string>("direccion")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 60);

                    b.Property<int?>("idPersona");

                    b.Property<string>("localidad")
                        .IsRequired();

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 60);

                    b.Property<string>("numeroCuenta")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 60);

                    b.Property<string>("pais")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 30);

                    b.Property<string>("provincia")
                        .IsRequired();

                    b.HasKey("idProveedor");

                    b.HasIndex("idPersona")
                        .IsUnique();

                    b.ToTable("Proveedor");
                });

            modelBuilder.Entity("TeamCatHotel.Models.Reserva", b =>
                {
                    b.Property<int>("idReserva")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("comentarios")
                        .HasAnnotation("MaxLength", 150);

                    b.Property<int>("estado");

                    b.Property<DateTime>("fechaFin");

                    b.Property<DateTime>("fechaInicio");

                    b.Property<DateTime>("fechaRealizacion");

                    b.Property<int?>("idCliente");

                    b.Property<int?>("idDescuento");

                    b.Property<int?>("idFactura");

                    b.Property<int>("regimenComida");

                    b.HasKey("idReserva");

                    b.HasIndex("idCliente");

                    b.HasIndex("idDescuento");

                    b.HasIndex("idFactura")
                        .IsUnique();

                    b.ToTable("Reserva");
                });

            modelBuilder.Entity("TeamCatHotel.Models.ReservaHabitacion", b =>
                {
                    b.Property<int>("idReservaHabitacion")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("idHabitacion");

                    b.Property<int?>("idReserva");

                    b.HasKey("idReservaHabitacion");

                    b.HasIndex("idHabitacion");

                    b.HasIndex("idReserva");

                    b.ToTable("ReservaHabitacion");
                });

            modelBuilder.Entity("TeamCatHotel.Models.ReservaServicio", b =>
                {
                    b.Property<int>("idReservaServicio")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("fechaFin");

                    b.Property<DateTime>("fechaInicio");

                    b.Property<int?>("idLineaFactura");

                    b.Property<int?>("idReserva");

                    b.Property<int?>("idServicio");

                    b.Property<int?>("numero");

                    b.HasKey("idReservaServicio");

                    b.HasIndex("idLineaFactura");

                    b.HasIndex("idReserva");

                    b.HasIndex("idServicio");

                    b.HasIndex("numero");

                    b.ToTable("ReservaServicio");
                });

            modelBuilder.Entity("TeamCatHotel.Models.Servicio", b =>
                {
                    b.Property<int>("idServicio")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("descripcion")
                        .HasAnnotation("MaxLength", 200);

                    b.Property<string>("nombre")
                        .HasAnnotation("MaxLength", 200);

                    b.Property<float>("precio");

                    b.HasKey("idServicio");

                    b.ToTable("Servicio");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Servicio");
                });

            modelBuilder.Entity("TeamCatHotel.Models.Cliente", b =>
                {
                    b.HasBaseType("TeamCatHotel.Models.Persona");

                    b.Property<string>("numeroTarjeta")
                        .IsRequired();

                    b.ToTable("Cliente");

                    b.HasDiscriminator().HasValue("Cliente");
                });

            modelBuilder.Entity("TeamCatHotel.Models.Menu", b =>
                {
                    b.HasBaseType("TeamCatHotel.Models.Servicio");

                    b.Property<int>("horario");

                    b.ToTable("Menu");

                    b.HasDiscriminator().HasValue("Menu");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("TeamCatHotel.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("TeamCatHotel.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TeamCatHotel.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TeamCatHotel.Models.Factura", b =>
                {
                    b.HasOne("TeamCatHotel.Models.Reserva", "Reserva")
                        .WithMany()
                        .HasForeignKey("idReserva");
                });

            modelBuilder.Entity("TeamCatHotel.Models.LineaFactura", b =>
                {
                    b.HasOne("TeamCatHotel.Models.Factura", "Factura")
                        .WithMany("LineasFactura")
                        .HasForeignKey("idFactura");

                    b.HasOne("TeamCatHotel.Models.ReservaServicio", "ReservaServicio")
                        .WithOne()
                        .HasForeignKey("TeamCatHotel.Models.LineaFactura", "idReservaServicio");
                });

            modelBuilder.Entity("TeamCatHotel.Models.LineaPedido", b =>
                {
                    b.HasOne("TeamCatHotel.Models.Pedido", "Pedido")
                        .WithMany("LineasPedido")
                        .HasForeignKey("idPedido");

                    b.HasOne("TeamCatHotel.Models.Producto", "Producto")
                        .WithMany("LineasPedido")
                        .HasForeignKey("idProducto");
                });

            modelBuilder.Entity("TeamCatHotel.Models.Pedido", b =>
                {
                    b.HasOne("TeamCatHotel.Models.Proveedor", "Proveedor")
                        .WithMany("Pedidos")
                        .HasForeignKey("idProveedor");
                });

            modelBuilder.Entity("TeamCatHotel.Models.Persona", b =>
                {
                    b.HasOne("TeamCatHotel.Models.Proveedor", "Proveedor")
                        .WithMany()
                        .HasForeignKey("idProveedor");
                });

            modelBuilder.Entity("TeamCatHotel.Models.PlatoMenu", b =>
                {
                    b.HasOne("TeamCatHotel.Models.Menu", "Menu")
                        .WithMany("PlatoMenu")
                        .HasForeignKey("idMenu");

                    b.HasOne("TeamCatHotel.Models.Plato", "Plato")
                        .WithMany("PlatoMenu")
                        .HasForeignKey("idPlato");
                });

            modelBuilder.Entity("TeamCatHotel.Models.ProductoProveedor", b =>
                {
                    b.HasOne("TeamCatHotel.Models.Producto", "Producto")
                        .WithMany("ProductoProveedor")
                        .HasForeignKey("idProducto");

                    b.HasOne("TeamCatHotel.Models.Proveedor", "Proveedor")
                        .WithMany("ProductoProveedor")
                        .HasForeignKey("idProveedor");
                });

            modelBuilder.Entity("TeamCatHotel.Models.Proveedor", b =>
                {
                    b.HasOne("TeamCatHotel.Models.Persona", "Persona")
                        .WithOne()
                        .HasForeignKey("TeamCatHotel.Models.Proveedor", "idPersona");
                });

            modelBuilder.Entity("TeamCatHotel.Models.Reserva", b =>
                {
                    b.HasOne("TeamCatHotel.Models.Cliente", "Cliente")
                        .WithMany("Reservas")
                        .HasForeignKey("idCliente");

                    b.HasOne("TeamCatHotel.Models.Descuento", "Descuento")
                        .WithMany("Reservas")
                        .HasForeignKey("idDescuento");

                    b.HasOne("TeamCatHotel.Models.Factura", "Factura")
                        .WithOne()
                        .HasForeignKey("TeamCatHotel.Models.Reserva", "idFactura");
                });

            modelBuilder.Entity("TeamCatHotel.Models.ReservaHabitacion", b =>
                {
                    b.HasOne("TeamCatHotel.Models.Habitacion", "Habitacion")
                        .WithMany("ReservaHabitacion")
                        .HasForeignKey("idHabitacion");

                    b.HasOne("TeamCatHotel.Models.Reserva", "Reserva")
                        .WithMany("ReservaHabitacion")
                        .HasForeignKey("idReserva");
                });

            modelBuilder.Entity("TeamCatHotel.Models.ReservaServicio", b =>
                {
                    b.HasOne("TeamCatHotel.Models.LineaFactura", "LineaFactura")
                        .WithMany()
                        .HasForeignKey("idLineaFactura");

                    b.HasOne("TeamCatHotel.Models.Servicio")
                        .WithMany("ReservasServicio")
                        .HasForeignKey("idReserva");

                    b.HasOne("TeamCatHotel.Models.Servicio", "Servicio")
                        .WithMany()
                        .HasForeignKey("idServicio");

                    b.HasOne("TeamCatHotel.Models.Habitacion", "Habitacion")
                        .WithMany("ReservaServicio")
                        .HasForeignKey("numero");
                });
        }
    }
}
