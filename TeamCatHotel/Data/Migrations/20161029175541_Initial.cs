using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TeamCatHotel.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Descuento",
                columns: table => new
                {
                    idDescuento = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    descripcion = table.Column<string>(maxLength: 200, nullable: false),
                    fechaFin = table.Column<DateTime>(nullable: false),
                    fechaInicio = table.Column<DateTime>(nullable: false),
                    nombre = table.Column<string>(maxLength: 100, nullable: false),
                    porcentaje = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Descuento", x => x.idDescuento);
                });

            migrationBuilder.CreateTable(
                name: "Habitacion",
                columns: table => new
                {
                    numero = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    aforo = table.Column<int>(nullable: false),
                    descripcion = table.Column<string>(maxLength: 200, nullable: false),
                    localizacion = table.Column<string>(maxLength: 100, nullable: false),
                    ocupada = table.Column<bool>(nullable: false),
                    precio = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habitacion", x => x.numero);
                });

            migrationBuilder.CreateTable(
                name: "Plato",
                columns: table => new
                {
                    idPlato = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plato", x => x.idPlato);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    idProducto = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    cantidad = table.Column<int>(nullable: false),
                    categoria = table.Column<string>(maxLength: 60, nullable: false),
                    descripcion = table.Column<string>(maxLength: 60, nullable: true),
                    limiteMinimo = table.Column<int>(nullable: false),
                    localizador = table.Column<string>(nullable: false),
                    nombre = table.Column<string>(maxLength: 60, nullable: false),
                    precio = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.idProducto);
                });

            migrationBuilder.CreateTable(
                name: "Servicio",
                columns: table => new
                {
                    idServicio = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Discriminator = table.Column<string>(nullable: false),
                    descripcion = table.Column<string>(maxLength: 200, nullable: true),
                    nombre = table.Column<string>(maxLength: 200, nullable: true),
                    precio = table.Column<float>(nullable: false),
                    horario = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicio", x => x.idServicio);
                });

            migrationBuilder.CreateTable(
                name: "PlatoMenu",
                columns: table => new
                {
                    idPlatoMenu = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    idMenu = table.Column<int>(nullable: true),
                    idPlato = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatoMenu", x => x.idPlatoMenu);
                    table.ForeignKey(
                        name: "FK_PlatoMenu_Servicio_idMenu",
                        column: x => x.idMenu,
                        principalTable: "Servicio",
                        principalColumn: "idServicio",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlatoMenu_Plato_idPlato",
                        column: x => x.idPlato,
                        principalTable: "Plato",
                        principalColumn: "idPlato",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reserva",
                columns: table => new
                {
                    idReserva = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    comentarios = table.Column<string>(maxLength: 150, nullable: true),
                    estado = table.Column<int>(nullable: false),
                    fechaFin = table.Column<DateTime>(nullable: false),
                    fechaInicio = table.Column<DateTime>(nullable: false),
                    fechaRealizacion = table.Column<DateTime>(nullable: false),
                    idCliente = table.Column<int>(nullable: true),
                    idDescuento = table.Column<int>(nullable: true),
                    idFactura = table.Column<int>(nullable: true),
                    regimenComida = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserva", x => x.idReserva);
                    table.ForeignKey(
                        name: "FK_Reserva_Descuento_idDescuento",
                        column: x => x.idDescuento,
                        principalTable: "Descuento",
                        principalColumn: "idDescuento",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Factura",
                columns: table => new
                {
                    idFactura = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    abono = table.Column<bool>(nullable: false),
                    fechaEmision = table.Column<DateTime>(nullable: false),
                    fechaPago = table.Column<DateTime>(nullable: false),
                    idReserva = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factura", x => x.idFactura);
                    table.ForeignKey(
                        name: "FK_Factura_Reserva_idReserva",
                        column: x => x.idReserva,
                        principalTable: "Reserva",
                        principalColumn: "idReserva",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReservaHabitacion",
                columns: table => new
                {
                    idReservaHabitacion = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    idHabitacion = table.Column<int>(nullable: true),
                    idReserva = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservaHabitacion", x => x.idReservaHabitacion);
                    table.ForeignKey(
                        name: "FK_ReservaHabitacion_Habitacion_idHabitacion",
                        column: x => x.idHabitacion,
                        principalTable: "Habitacion",
                        principalColumn: "numero",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservaHabitacion_Reserva_idReserva",
                        column: x => x.idReserva,
                        principalTable: "Reserva",
                        principalColumn: "idReserva",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LineaFactura",
                columns: table => new
                {
                    idLineaFactura = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    idFactura = table.Column<int>(nullable: true),
                    idReservaServicio = table.Column<int>(nullable: true),
                    precio = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineaFactura", x => x.idLineaFactura);
                    table.ForeignKey(
                        name: "FK_LineaFactura_Factura_idFactura",
                        column: x => x.idFactura,
                        principalTable: "Factura",
                        principalColumn: "idFactura",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReservaServicio",
                columns: table => new
                {
                    idReservaServicio = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    fechaFin = table.Column<DateTime>(nullable: false),
                    fechaInicio = table.Column<DateTime>(nullable: false),
                    idLineaFactura = table.Column<int>(nullable: true),
                    idReserva = table.Column<int>(nullable: true),
                    idServicio = table.Column<int>(nullable: true),
                    numero = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservaServicio", x => x.idReservaServicio);
                    table.ForeignKey(
                        name: "FK_ReservaServicio_LineaFactura_idLineaFactura",
                        column: x => x.idLineaFactura,
                        principalTable: "LineaFactura",
                        principalColumn: "idLineaFactura",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservaServicio_Servicio_idReserva",
                        column: x => x.idReserva,
                        principalTable: "Servicio",
                        principalColumn: "idServicio",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservaServicio_Servicio_idServicio",
                        column: x => x.idServicio,
                        principalTable: "Servicio",
                        principalColumn: "idServicio",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservaServicio_Habitacion_numero",
                        column: x => x.numero,
                        principalTable: "Habitacion",
                        principalColumn: "numero",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LineaPedido",
                columns: table => new
                {
                    idLineaPedido = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    cantidad = table.Column<int>(nullable: false),
                    idPedido = table.Column<int>(nullable: true),
                    idProducto = table.Column<int>(nullable: true),
                    precio = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineaPedido", x => x.idLineaPedido);
                    table.ForeignKey(
                        name: "FK_LineaPedido_Producto_idProducto",
                        column: x => x.idProducto,
                        principalTable: "Producto",
                        principalColumn: "idProducto",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Proveedor",
                columns: table => new
                {
                    idProveedor = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    cif = table.Column<string>(maxLength: 20, nullable: false),
                    correoElectronico = table.Column<string>(nullable: true),
                    direccion = table.Column<string>(maxLength: 60, nullable: false),
                    idPersona = table.Column<int>(nullable: true),
                    localidad = table.Column<string>(nullable: false),
                    nombre = table.Column<string>(maxLength: 60, nullable: false),
                    numeroCuenta = table.Column<string>(maxLength: 60, nullable: false),
                    pais = table.Column<string>(maxLength: 30, nullable: false),
                    provincia = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedor", x => x.idProveedor);
                });

            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    idPedido = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    fechaEmision = table.Column<DateTime>(nullable: false),
                    fechaRecepcion = table.Column<DateTime>(nullable: false),
                    idProveedor = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.idPedido);
                    table.ForeignKey(
                        name: "FK_Pedido_Proveedor_idProveedor",
                        column: x => x.idProveedor,
                        principalTable: "Proveedor",
                        principalColumn: "idProveedor",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    idPersona = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Discriminator = table.Column<string>(nullable: false),
                    apellidos = table.Column<string>(maxLength: 50, nullable: false),
                    codigoPostal = table.Column<string>(nullable: true),
                    correoElectronico = table.Column<string>(nullable: false),
                    direccion = table.Column<string>(maxLength: 50, nullable: true),
                    idProveedor = table.Column<int>(nullable: true),
                    localidad = table.Column<string>(maxLength: 50, nullable: true),
                    nif = table.Column<string>(nullable: false),
                    nombre = table.Column<string>(maxLength: 50, nullable: false),
                    pais = table.Column<string>(maxLength: 50, nullable: true),
                    provincia = table.Column<string>(maxLength: 50, nullable: true),
                    telefono = table.Column<int>(nullable: false),
                    numeroTarjeta = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => x.idPersona);
                    table.ForeignKey(
                        name: "FK_Persona_Proveedor_idProveedor",
                        column: x => x.idProveedor,
                        principalTable: "Proveedor",
                        principalColumn: "idProveedor",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductoProveedor",
                columns: table => new
                {
                    idProductoProveedor = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    idProducto = table.Column<int>(nullable: true),
                    idProveedor = table.Column<int>(nullable: true),
                    precio = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoProveedor", x => x.idProductoProveedor);
                    table.ForeignKey(
                        name: "FK_ProductoProveedor_Producto_idProducto",
                        column: x => x.idProducto,
                        principalTable: "Producto",
                        principalColumn: "idProducto",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductoProveedor_Proveedor_idProveedor",
                        column: x => x.idProveedor,
                        principalTable: "Proveedor",
                        principalColumn: "idProveedor",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Factura_idReserva",
                table: "Factura",
                column: "idReserva");

            migrationBuilder.CreateIndex(
                name: "IX_LineaFactura_idFactura",
                table: "LineaFactura",
                column: "idFactura");

            migrationBuilder.CreateIndex(
                name: "IX_LineaFactura_idReservaServicio",
                table: "LineaFactura",
                column: "idReservaServicio",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LineaPedido_idPedido",
                table: "LineaPedido",
                column: "idPedido");

            migrationBuilder.CreateIndex(
                name: "IX_LineaPedido_idProducto",
                table: "LineaPedido",
                column: "idProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_idProveedor",
                table: "Pedido",
                column: "idProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_idProveedor",
                table: "Persona",
                column: "idProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_PlatoMenu_idMenu",
                table: "PlatoMenu",
                column: "idMenu");

            migrationBuilder.CreateIndex(
                name: "IX_PlatoMenu_idPlato",
                table: "PlatoMenu",
                column: "idPlato");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoProveedor_idProducto",
                table: "ProductoProveedor",
                column: "idProducto");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoProveedor_idProveedor",
                table: "ProductoProveedor",
                column: "idProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Proveedor_idPersona",
                table: "Proveedor",
                column: "idPersona",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_idCliente",
                table: "Reserva",
                column: "idCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_idDescuento",
                table: "Reserva",
                column: "idDescuento");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_idFactura",
                table: "Reserva",
                column: "idFactura",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReservaHabitacion_idHabitacion",
                table: "ReservaHabitacion",
                column: "idHabitacion");

            migrationBuilder.CreateIndex(
                name: "IX_ReservaHabitacion_idReserva",
                table: "ReservaHabitacion",
                column: "idReserva");

            migrationBuilder.CreateIndex(
                name: "IX_ReservaServicio_idLineaFactura",
                table: "ReservaServicio",
                column: "idLineaFactura");

            migrationBuilder.CreateIndex(
                name: "IX_ReservaServicio_idReserva",
                table: "ReservaServicio",
                column: "idReserva");

            migrationBuilder.CreateIndex(
                name: "IX_ReservaServicio_idServicio",
                table: "ReservaServicio",
                column: "idServicio");

            migrationBuilder.CreateIndex(
                name: "IX_ReservaServicio_numero",
                table: "ReservaServicio",
                column: "numero");

            migrationBuilder.AddForeignKey(
                name: "FK_Reserva_Factura_idFactura",
                table: "Reserva",
                column: "idFactura",
                principalTable: "Factura",
                principalColumn: "idFactura",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reserva_Persona_idCliente",
                table: "Reserva",
                column: "idCliente",
                principalTable: "Persona",
                principalColumn: "idPersona",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LineaFactura_ReservaServicio_idReservaServicio",
                table: "LineaFactura",
                column: "idReservaServicio",
                principalTable: "ReservaServicio",
                principalColumn: "idReservaServicio",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LineaPedido_Pedido_idPedido",
                table: "LineaPedido",
                column: "idPedido",
                principalTable: "Pedido",
                principalColumn: "idPedido",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Proveedor_Persona_idPersona",
                table: "Proveedor",
                column: "idPersona",
                principalTable: "Persona",
                principalColumn: "idPersona",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Factura_Reserva_idReserva",
                table: "Factura");

            migrationBuilder.DropForeignKey(
                name: "FK_LineaFactura_Factura_idFactura",
                table: "LineaFactura");

            migrationBuilder.DropForeignKey(
                name: "FK_LineaFactura_ReservaServicio_idReservaServicio",
                table: "LineaFactura");

            migrationBuilder.DropForeignKey(
                name: "FK_Persona_Proveedor_idProveedor",
                table: "Persona");

            migrationBuilder.DropTable(
                name: "LineaPedido");

            migrationBuilder.DropTable(
                name: "PlatoMenu");

            migrationBuilder.DropTable(
                name: "ProductoProveedor");

            migrationBuilder.DropTable(
                name: "ReservaHabitacion");

            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropTable(
                name: "Plato");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Reserva");

            migrationBuilder.DropTable(
                name: "Descuento");

            migrationBuilder.DropTable(
                name: "Factura");

            migrationBuilder.DropTable(
                name: "ReservaServicio");

            migrationBuilder.DropTable(
                name: "LineaFactura");

            migrationBuilder.DropTable(
                name: "Servicio");

            migrationBuilder.DropTable(
                name: "Habitacion");

            migrationBuilder.DropTable(
                name: "Proveedor");

            migrationBuilder.DropTable(
                name: "Persona");
        }
    }
}
