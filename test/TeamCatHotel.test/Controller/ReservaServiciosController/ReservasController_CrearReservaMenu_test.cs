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

namespace TeamCatHotel.test.Controller
{
    public class ReservaServiciosController_CrearReservaMenu_test
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
        public ReservaServiciosController_CrearReservaMenu_test()
        {
            _contextOptions = CreateNewContextOptions();
            context = new ApplicationDbContext(_contextOptions);
        }

        [Fact]
        public void CrearReservaMenu_MenuIsNull()
        {
            // Arrange
            using (context)
            {
                var controller = new ReservaServiciosController(context);
                Habitacion habitacion = new Habitacion();
                LineaFactura linea = new LineaFactura();
                DateTime hora = new DateTime(2016, 10, 20);
                // Act
                ReservaServicio result = controller.CrearReservaMenu(null, habitacion, linea, hora);
                //Assert
                Assert.Null(result);
            }
        }

        [Fact]
        public void CrearReservaMenu_HabitacionIsNull()
        {
            // Arrange
            using (context)
            {
                var controller = new ReservaServiciosController(context);
                Menu menu = new Menu();
                LineaFactura linea = new LineaFactura();
                DateTime hora = new DateTime(2016, 10, 20);
                // Act
                ReservaServicio result = controller.CrearReservaMenu(menu, null, linea, hora);
                //Assert
                Assert.Null(result);
            }
        }

        [Fact]
        public void CrearReservaMenu_LineaIsNull()
        {
            // Arrange
            using (context)
            {
                var controller = new ReservaServiciosController(context);
                Menu menu = new Menu();
                Habitacion habitacion = new Habitacion();
                DateTime hora = new DateTime(2016, 10, 20);
                // Act
                ReservaServicio result = controller.CrearReservaMenu(menu, habitacion, null, hora);
                //Assert
                Assert.Null(result);
            }
        }

        [Fact]
        public void CrearReservaMenu_TooLate()
        {
            // Arrange
            using (context)
            {
                var controller = new ReservaServiciosController(context);
                Menu menu = new Menu
                {
                    idServicio = 1,
                    horaInicio = new DateTime(2001, 12, 13, 13, 00, 00),
                    horaFin = new DateTime(2001, 12, 13, 16, 00, 00)
                };
                Habitacion habitacion = new Habitacion { numero = 101 };
                LineaFactura linea = new LineaFactura { idLineaFactura = 1 };
                DateTime hora = new DateTime(2016, 10, 20, 16, 00, 01);
                // Act
                ReservaServicio result = controller.CrearReservaMenu(menu, habitacion, linea, hora);
                //Assert
                Assert.Null(result);
            }
        }

        [Fact]
        public void CrearReservaMenu_TooSoon()
        {
            // Arrange
            using (context)
            {
                var controller = new ReservaServiciosController(context);
                Menu menu = new Menu
                {
                    idServicio = 1,
                    horaInicio = new DateTime(2001, 12, 13, 13, 00, 00),
                    horaFin = new DateTime(2001, 12, 13, 16, 00, 00)
                };
                Habitacion habitacion = new Habitacion { numero = 101 };
                LineaFactura linea = new LineaFactura { idLineaFactura = 1 };
                DateTime hora = new DateTime(2016, 10, 20, 12, 59, 59);
                // Act
                ReservaServicio result = controller.CrearReservaMenu(menu, habitacion, linea, hora);
                //Assert
                Assert.Null(result);
            }
        }

        [Fact]
        public void CrearReservaMenu_InTime()
        {
            // Arrange
            using (context)
            {
                var controller = new ReservaServiciosController(context);
                Menu menu = new Menu
                {
                    idServicio = 1,
                    horaInicio = new DateTime(2001, 12, 13, 13, 00, 00),
                    horaFin = new DateTime(2001, 12, 13, 16, 00, 00)
                };
                Habitacion habitacion = new Habitacion { numero = 101 };
                LineaFactura linea = new LineaFactura { idLineaFactura = 1 };
                DateTime hora = new DateTime(2016, 10, 20, 13, 00, 01);
                ReservaServicio exResult = new ReservaServicio
                {
                    Habitacion = habitacion,
                    fechaInicio = new DateTime(2016, 10, 20, 13, 00, 00),
                    fechaFin = new DateTime(2016, 10, 20, 16, 00, 00),
                    LineaFactura = linea,
                    Servicio = menu
                };
                // Act
                ReservaServicio result = controller.CrearReservaMenu(menu, habitacion, linea, hora);
                //Assert
                Assert.IsType<ReservaServicio>(result);
                Assert.Equal(exResult,
                            result,
                            Comparer.Get<ReservaServicio>((rs1, rs2) => rs1.Habitacion.numero == rs2.Habitacion.numero &&
                                                         rs1.LineaFactura.idLineaFactura == rs2.LineaFactura.idLineaFactura &&
                                                         rs1.Servicio.idServicio == rs2.Servicio.idServicio &&
                                                         rs1.fechaInicio.Equals(rs2.fechaInicio) &&
                                                         rs1.fechaFin.Equals(rs2.fechaFin)));
            }
        }
    }
}
