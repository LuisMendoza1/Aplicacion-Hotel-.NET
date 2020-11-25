using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamCatHotel.Data;
using TeamCatHotel.Models;
using TeamCatHotel.Controllers;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using TeamCatHotel.Enums;
using TeamCatHotel.Services;

namespace TeamCatHotel.test.Controller
{
    public class LineaFacturasController_CrearLineaMenu_test
    {
        private static DbContextOptions<ApplicationDbContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh
            // InMemorydatabaseinstance.
            var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();
            // Create a new options instance telling the context to use an
            // InMemorydatabase and the new service provider.
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase().UseInternalServiceProvider(serviceProvider);
            return builder.Options;
        }

        private DbContextOptions<ApplicationDbContext> _contextOptions;
        private ApplicationDbContext context;
        public LineaFacturasController_CrearLineaMenu_test()
        {
            _contextOptions = CreateNewContextOptions();
            context = new ApplicationDbContext(_contextOptions);
            Factura factura = new Factura { idFactura = 1 };
            context.Factura.Add(factura);
            context.SaveChanges();
        }

        [Fact]
        public async Task CrearLineaMenu_MenuIsNull()
        {
            // Arrange
            using (context)
            {
                var controller = new LineaFacturasController(context);
                Menu menu = null;
                Habitacion habitacion = new Habitacion();
                Factura factura = new Factura();
                int cantidad = 1;
                bool exResult = false;
                // Act
                bool result = await controller.CrearLineaMenu(menu, cantidad, factura, habitacion);
                //Assert
                Assert.IsType<bool>(result);
                Assert.Equal(exResult, result);
            }
        }

        [Fact]
        public async Task CrearLineaMenu_CantidadIsInvalid()
        {
            // Arrange
            using (context)
            {
                var controller = new LineaFacturasController(context);
                Menu menu = new Menu();
                Habitacion habitacion = new Habitacion();
                Factura factura = new Factura();
                int cantidad = -1;
                bool exResult = false;
                // Act
                bool result = await controller.CrearLineaMenu(menu, cantidad, factura, habitacion);
                //Assert
                Assert.IsType<bool>(result);
                Assert.Equal(exResult, result);
            }
        }

        [Fact]
        public async Task CrearLineaMenu_FacturaIsNull()
        {
            // Arrange
            using (context)
            {
                var controller = new LineaFacturasController(context);
                Menu menu = new Menu();
                Habitacion habitacion = new Habitacion();
                Factura factura = null;
                int cantidad = 1;
                bool exResult = false;
                // Act
                bool result = await controller.CrearLineaMenu(menu, cantidad, factura, habitacion);
                //Assert
                Assert.IsType<bool>(result);
                Assert.Equal(exResult, result);
            }
        }

        [Fact]
        public async Task CrearLineaMenu_HabitacionIsNull()
        {
            // Arrange
            using (context)
            {
                var controller = new LineaFacturasController(context);
                Menu menu = new Menu();
                Habitacion habitacion = null;
                Factura factura = new Factura();
                int cantidad = 1;
                bool exResult = false;
                // Act
                bool result = await controller.CrearLineaMenu(menu, cantidad, factura, habitacion);
                //Assert
                Assert.IsType<bool>(result);
                Assert.Equal(exResult, result);
            }
        }

        [Fact]
        public async Task CrearLineaMenu_FailedCreatingReservaServicio()
        {
            // Arrange
            using (context)
            {
                var controller = new LineaFacturasController(context);
                Menu menu = new Menu
                {
                    idServicio = 1,
                    horaInicio = new DateTime(2001, 12, 13, 13, 00, 00),
                    horaFin = new DateTime(2001, 12, 13, 16, 00, 00)
                };
                int cantidad = 1;
                Factura factura = new Factura { idFactura = 1 };
                Habitacion habitacion = new Habitacion { numero = 101 };
                bool exResult = false;
                SystemTime.Now = () => new DateTime(2016, 10, 13, 12, 00, 00);
                // Act
                bool result = await controller.CrearLineaMenu(menu, cantidad, factura, habitacion);
                //Assert
                Assert.IsType<bool>(result);
                Assert.Equal(exResult, result);
            }
        }

        [Fact]
        public async Task CrearLineaMenu_Success()
        {
            // Arrange
            using (context)
            {
                var controller = new LineaFacturasController(context);
                Menu menu = new Menu
                {
                    idServicio = 1,
                    horaInicio = new DateTime(2001, 12, 13, 13, 00, 00),
                    horaFin = new DateTime(2001, 12, 13, 16, 00, 00),
                    precio = 10F
                };
                int cantidad = 1;
                Factura factura = new Factura { idFactura = 1 };
                Habitacion habitacion = new Habitacion { numero = 101 };
                bool exResult = true;
                LineaFactura exLF = new LineaFactura { idLineaFactura = 1, Factura = factura, precio = menu.precio };
                ReservaServicio exRS = new ReservaServicio
                {
                    idReservaServicio = 1,
                    Habitacion = habitacion,
                    fechaInicio = new DateTime(2016, 10, 20, 13, 00, 00),
                    fechaFin = new DateTime(2016, 10, 20, 16, 00, 00),
                    LineaFactura = exLF,
                    Servicio = menu
                };
                exLF.ReservaServicio = exRS;
                IEnumerable<LineaFactura> exLFModel = new List<LineaFactura> { exLF };
                IEnumerable<ReservaServicio> exRSModel = new List<ReservaServicio> { exRS };

                SystemTime.Now = () => new DateTime(2016, 10, 20, 14, 00, 00);
                // Act
                bool result = await controller.CrearLineaMenu(menu, cantidad, factura, habitacion);
                //Assert
                Assert.IsType<bool>(result);
                Assert.Equal(exResult, result);

                IEnumerable<ReservaServicio> rsModel = context.ReservaServicio.ToList<ReservaServicio>();
                IEnumerable<LineaFactura> lfModel = context.LineaFactura.ToList<LineaFactura>();
                Assert.Equal(exRSModel,
                            rsModel,
                            Comparer.Get<ReservaServicio>((rs1, rs2) => rs1.idReservaServicio == rs2.idReservaServicio &&
                                                         rs1.Habitacion.numero == rs2.Habitacion.numero &&
                                                         rs1.LineaFactura.idLineaFactura == rs2.LineaFactura.idLineaFactura &&
                                                         rs1.Servicio.idServicio == rs2.Servicio.idServicio &&
                                                         rs1.fechaInicio.Equals(rs2.fechaInicio) &&
                                                         rs1.fechaFin.Equals(rs2.fechaFin)));
                Assert.Equal(exLFModel,
                            lfModel,
                            Comparer.Get<LineaFactura>((lf1, lf2) => lf1.idLineaFactura == lf2.idLineaFactura &&
                                                         lf1.precio == lf2.precio &&
                                                         lf1.Factura.idFactura == lf2.Factura.idFactura &&
                                                         lf1.ReservaServicio.idReservaServicio == lf2.ReservaServicio.idReservaServicio
                                                         ));
            }
        }
    }
}
