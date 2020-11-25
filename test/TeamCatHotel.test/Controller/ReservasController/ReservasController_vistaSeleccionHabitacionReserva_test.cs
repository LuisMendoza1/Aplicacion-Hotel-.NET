using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamCatHotel.Controllers;
using TeamCatHotel.Data;
using TeamCatHotel.Models;
using TeamCatHotel.Models.ResevarViewModels;
using Xunit;

namespace TeamCatHotel.test.Controller
{
    public class ReservasController_vistaSeleccionHabitacionReserva_test
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
        public ReservasController_vistaSeleccionHabitacionReserva_test()
        {
            _contextOptions = CreateNewContextOptions();
            context = new ApplicationDbContext(_contextOptions);
            // Insert seed data into the database using one instance of the context
            Cliente cliente1 = (new Cliente { idPersona = 1, nombre = "Prueba1", apellidos = "Prueba1 Prueba1", telefono = "666666666", correoElectronico = "prueba1@prueba1.com", nif = "12345678A", numeroTarjeta = "4444333322221111" });
            Cliente cliente2 = (new Cliente { idPersona = 2, nombre = "Prueba2", apellidos = "Prueba2 Prueba2", telefono = "777777777", correoElectronico = "prueba2@prueba2.com", nif = "87654321B", numeroTarjeta = "4444333322221111" });
            Descuento descuento1 = new Descuento { idDescuento = 1, fechaInicio = new DateTime(2016, 11, 20), fechaFin = new DateTime(2016, 11, 29) };
            Descuento descuento2 = new Descuento { idDescuento = 2, fechaInicio = new DateTime(2016, 11, 27), fechaFin = new DateTime(2016, 11, 28) };
            Habitacion habitacion1 = new Habitacion { precio = 20 };
            Habitacion habitacion2 = new Habitacion { precio = 40 };
            Reserva reserva1 = new Reserva { fechaInicio = new DateTime(2016, 11, 25), fechaFin = new DateTime(2016, 11, 27) };
            Reserva reserva2 = new Reserva { fechaInicio = new DateTime(2016, 11, 10), fechaFin = new DateTime(2016, 11, 12) };
            ReservaHabitacion reservaHabitacion1 = new ReservaHabitacion();
            ReservaHabitacion reservaHabitacion2 = new ReservaHabitacion();

           


            reservaHabitacion1.Reserva = reserva1;
            reservaHabitacion1.Habitacion = habitacion1;
            reservaHabitacion2.Reserva = reserva2;
            reservaHabitacion2.Habitacion = habitacion2;

            context.Cliente.Add(cliente1);
            context.Cliente.Add(cliente2);
            context.Descuento.Add(descuento1);
            context.Descuento.Add(descuento2);
            context.Habitacion.Add(habitacion1);
            context.Habitacion.Add(habitacion2);
            context.Reserva.Add(reserva1);
            context.Reserva.Add(reserva2);
            context.ReservaHabitacion.Add(reservaHabitacion1);
            context.ReservaHabitacion.Add(reservaHabitacion2);

            context.SaveChanges();
        }

        [Fact]
        public async Task vistaSeleccionHabitacionReserva_WithHabitacionesDisponibles()
        {
            // Arrange
            using (context)
            {
                var controller = new ReservasController(context);
                DatosHabitacionReservaViewModel expectedModel = new DatosHabitacionReservaViewModel
                {
                    idPersona = 2,
                    nombre = "Prueba2",
                    apellidos = "Prueba2 Prueba2",
                    nif = "87654321B",
                    comentarios = "patata",
                    fechaFin = new DateTime(2016, 11, 26),
                    fechaInicio = new DateTime(2016, 11, 24),
                    regimenComida = 1,
                    Descuentos = new Descuento[1] { new Descuento { idDescuento = 1, fechaInicio = new DateTime(2016, 11, 20), fechaFin = new DateTime(2016, 11, 29) } },
                    Habitaciones = new Habitacion[1] { new Habitacion { numero = 2, precio = 40 } }
                };

                DatosHabitacionReservaViewModel entrada = new DatosHabitacionReservaViewModel
                {
                    idPersona = 2,
                    nombre = "Prueba2",
                    apellidos = "Prueba2 Prueba2",
                    nif = "87654321B",
                    comentarios = "patata",
                    fechaFin = new DateTime(2016, 11, 26),
                    fechaInicio = new DateTime(2016, 11, 24),
                    regimenComida = 1
                };

                // Act
                var result = await controller.vistaSeleccionHabitacionReserva(entrada);
                //Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                DatosHabitacionReservaViewModel model = (result as ViewResult).Model as DatosHabitacionReservaViewModel;

                //Comprobación del listado de descuentos disponibles
                List<Descuento> expectDescuentos = expectedModel.Descuentos.ToList();
                List<Descuento> modelDescuentos = model.Descuentos.ToList();
                Assert.Equal(expectDescuentos, modelDescuentos, Comparer.Get<Descuento>((p1, p2) => p1.idDescuento == p2.idDescuento));

                //Comprobación del listado de habitaciones disponibles
                List<Habitacion> expectHabitaciones = expectedModel.Habitaciones.ToList();
                List<Habitacion> modelHabitaciones = model.Habitaciones.ToList();
                Assert.Equal(expectHabitaciones, modelHabitaciones, Comparer.Get<Habitacion>((p1, p2) => p1.numero == p2.numero));

                //Comprobación de los otros datos del objeto model
                Assert.Equal(expectedModel, model, Comparer.Get<DatosHabitacionReservaViewModel>((p1, p2) => p1.idPersona == p2.idPersona && p1.comentarios == p2.comentarios && p1.fechaInicio == p2.fechaInicio && p1.fechaFin == p2.fechaFin && p1.regimenComida == p2.regimenComida));

            }
        }
    }
}
