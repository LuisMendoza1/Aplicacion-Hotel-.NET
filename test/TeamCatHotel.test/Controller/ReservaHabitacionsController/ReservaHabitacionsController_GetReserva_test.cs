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
    public class ReservaHabitacionsController_GetReserva_test
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
        public ReservaHabitacionsController_GetReserva_test()
        {
            _contextOptions = CreateNewContextOptions();
            context = new ApplicationDbContext(_contextOptions);
            Reserva reserva = new Reserva
            {
                idReserva = 1,
                fechaInicio = new DateTime(2016, 10, 19, 12, 00, 00),
                fechaFin = new DateTime(2016, 10, 21, 12, 00, 00)
            };
            Habitacion habitacion = new Habitacion { numero = 100 };
            context.Reserva.Add(reserva);
            context.Habitacion.Add(habitacion);
            context.ReservaHabitacion.Add(new ReservaHabitacion { idReservaHabitacion = 1,
                                                                  Habitacion = habitacion,
                                                                  Reserva = reserva });
            context.SaveChanges();
        }

        [Fact]
        public async Task GetReserva_RoomNotFound()
        {
            // Arrange
            using (context)
            {
                var controller = new ReservaHabitacionsController(context);

                SystemTime.Now = () => new DateTime(2016, 10, 20, 12, 00, 00);
                // Act
                Reserva result = await controller.GetReserva(1);
                //Assert
                Assert.Null(result);
            }
        }

        [Fact]
        public async Task GetReserva_RoomFound()
        {
            // Arrange
            using (context)
            {
                var controller = new ReservaHabitacionsController(context);
                Reserva exResult = new Reserva
                {
                    idReserva = 1,
                    fechaInicio = DateTime.Today.AddDays(-1),
                    fechaFin = DateTime.Today.AddDays(1)
                };

                SystemTime.Now = () => new DateTime(2016, 10, 20, 12, 00, 00);

                // Act
                Reserva result = await controller.GetReserva(100);

                //Assert
                Assert.IsType<Reserva>(result);
                Assert.Equal(exResult, result, Comparer.Get<Reserva>((r1, r2) => r1.idReserva == r2.idReserva));
            }
        }
    }
}
