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
    public class ReservasController_ComidaIncluida_test
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
        public ReservasController_ComidaIncluida_test()
        {
            _contextOptions = CreateNewContextOptions();
            context = new ApplicationDbContext(_contextOptions);
        }

        [Fact]
        public void ComidaIncluida_ReservaIsNull()
        {
            // Arrange
            using (context)
            {
                var controller = new ReservasController(context);
                bool exResult = false;
                // Act
                bool result = controller.ComidaIncluida(null, Regimen.COMPLETA);
                //Assert
                Assert.IsType<bool>(result);
                Assert.Equal(exResult, result);
            }
        }

        [Fact]
        public void ComidaIncluida_RegimenNotAssigned()
        {
            // Arrange
            using (context)
            {
                var controller = new ReservasController(context);
                bool exResult = false;
                Reserva reserva = new Reserva();

                // Act
                bool result = controller.ComidaIncluida(reserva, Regimen.COMPLETA);

                //Assert
                Assert.IsType<bool>(result);
                Assert.Equal(exResult, result);
            }
        }

        [Fact]
        public void ComidaIncluida_InvalidBoard()
        {
            // Arrange
            using (context)
            {
                var controller = new ReservasController(context);
                bool exResult = false;
                Reserva reserva = new Reserva
                {
                    idReserva = 1,
                    fechaInicio = DateTime.Today.AddDays(-1),
                    fechaFin = DateTime.Today.AddDays(1),
                    regimenComida = Regimen.MEDIA
                };

                // Act
                bool result = controller.ComidaIncluida(reserva, Regimen.INVALIDO);

                //Assert
                Assert.IsType<bool>(result);
                Assert.Equal(exResult, result);
            }
        }

        [Fact]
        public void ComidaIncluida_NotIncluded()
        {
            // Arrange
            using (context)
            {
                var controller = new ReservasController(context);
                bool exResult = false;
                Reserva reserva = new Reserva
                {
                    idReserva = 1,
                    fechaInicio = DateTime.Today.AddDays(-1),
                    fechaFin = DateTime.Today.AddDays(1),
                    regimenComida = Regimen.MEDIA
                };

                // Act
                bool result = controller.ComidaIncluida(reserva, Regimen.COMPLETA);

                //Assert
                Assert.IsType<bool>(result);
                Assert.Equal(exResult, result);
            }
        }

        [Fact]
        public void ComidaIncluida_Included()
        {
            // Arrange
            using (context)
            {
                var controller = new ReservasController(context);
                bool exResult = true;
                Reserva reserva = new Reserva
                {
                    idReserva = 1,
                    fechaInicio = DateTime.Today.AddDays(-1),
                    fechaFin = DateTime.Today.AddDays(1),
                    regimenComida = Regimen.MEDIA
                };

                // Act
                bool result = controller.ComidaIncluida(reserva, Regimen.MEDIA);

                //Assert
                Assert.IsType<bool>(result);
                Assert.Equal(exResult, result);
            }
        }
    }
}
