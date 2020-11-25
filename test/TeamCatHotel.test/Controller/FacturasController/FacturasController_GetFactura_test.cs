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
    public class FacturasController_GetFactura_test
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
        public FacturasController_GetFactura_test()
        {
            _contextOptions = CreateNewContextOptions();
            context = new ApplicationDbContext(_contextOptions);
            Reserva reserva = new Reserva
            {
                idReserva = 1
            };
            Factura factura = new Factura
            {
                idFactura = 1,
                Reserva = reserva
            };
            reserva.Factura = factura;
            context.Reserva.Add(reserva);
            context.Factura.Add(factura);
            context.SaveChanges();
        }

        [Fact]
        public async Task GetFactura_ReservaIsNull()
        {
            // Arrange
            using (context)
            {
                var controller = new FacturasController(context);
                // Act
                Factura result = await controller.GetFactura(null);
                //Assert
                Assert.Null(result);
            }
        }

        [Fact]
        public async Task GetFactura_ReservaNotAssociated()
        {
            // Arrange
            using (context)
            {
                var controller = new FacturasController(context);
                Reserva reserva = new Reserva
                {
                    idReserva = 1
                };

                // Act
                Factura result = await controller.GetFactura(reserva);

                //Assert
                Assert.Null(result);
            }
        }

        [Fact]
        public async Task GetFactura_NotFound()
        {
            // Arrange
            using (context)
            {
                var controller = new FacturasController(context);
                Reserva reserva = new Reserva
                {
                    idReserva = 1,
                };
                Factura factura = new Factura
                {
                    idFactura = 2,
                    Reserva = reserva
                };
                reserva.Factura = factura;

                // Act
                Factura result = await controller.GetFactura(reserva);

                //Assert
                Assert.Null(result);
            }
        }

        [Fact]
        public async Task GetFactura_Found()
        {
            // Arrange
            using (context)
            {
                var controller = new FacturasController(context);
                Reserva reserva = new Reserva
                {
                    idReserva = 1
                };
                Factura exResult = new Factura
                {
                    idFactura = 1,
                    Reserva = reserva
                };
                reserva.Factura = exResult;

                // Act
                Factura result = await controller.GetFactura(reserva);

                //Assert
                Assert.IsType<Factura>(result);
                Assert.Equal(exResult, result, Comparer.Get<Factura>((f1, f2) => f1.idFactura == f2.idFactura));
            }
        }
    }
}
