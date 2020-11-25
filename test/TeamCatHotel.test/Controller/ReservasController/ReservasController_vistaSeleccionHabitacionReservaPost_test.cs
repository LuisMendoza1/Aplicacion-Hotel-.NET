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
    public class ReservasController_vistaSeleccionHabitacionReservaPost_test
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
        public ReservasController_vistaSeleccionHabitacionReservaPost_test()
        {
            _contextOptions = CreateNewContextOptions();
            context = new ApplicationDbContext(_contextOptions);
            // Insert seed data into the database using one instance of the context
            Cliente cliente1 = (new Cliente { idPersona = 1, nombre = "Prueba1", apellidos = "Prueba1 Prueba1", telefono = "666666666", correoElectronico = "prueba1@prueba1.com", nif = "12345678A", numeroTarjeta = "4444333322221111" });
            Cliente cliente2 = (new Cliente { idPersona = 2, nombre = "Prueba2", apellidos = "Prueba2 Prueba2", telefono = "777777777", correoElectronico = "prueba2@prueba2.com", nif = "87654321B", numeroTarjeta = "4444333322221111" });
            Descuento descuento1 = new Descuento { idDescuento = 1, fechaInicio = new DateTime(2016, 11, 25), fechaFin = new DateTime(2016, 11, 29) };
            Descuento descuento2 = new Descuento { idDescuento = 2, fechaInicio = new DateTime(2016, 11, 27), fechaFin = new DateTime(2016, 11, 28) };
            Habitacion habitacion1 = new Habitacion { numero = 1, precio = 20 };
            Habitacion habitacion2 = new Habitacion { numero = 2, precio = 40 };
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
        public async Task vistaSeleccionHabitacionReservaPost_ModelIsNotValid()
        {
            // Arrange
            using (context)
            {
                var controller = new ReservasController(context);
                controller.ModelState.AddModelError("habitacionesSeleccionadas", "Required");
                DatosHabitacionReservaViewModel entrada = new DatosHabitacionReservaViewModel
                {
                    idPersona = 2,
                    nombre = "Prueba2",
                    apellidos = "Prueba2 Prueba2",
                    nif = "87654321B",
                    comentarios = "patata",
                    fechaFin = new DateTime(2016, 11, 26),
                    fechaInicio = new DateTime(2016, 11, 24),
                    regimenComida = 1,
                    Descuentos = new Descuento[1] { new Descuento { idDescuento = 2 } },
                    Habitaciones = new Habitacion[1] { new Habitacion { numero = 2 } },
                };

                // Act
                var result = await controller.vistaSeleccionHabitacionReservaPost(entrada);
                //Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                DatosHabitacionReservaViewModel model = (result as ViewResult).Model as DatosHabitacionReservaViewModel;
                Assert.Equal(entrada, model, Comparer.Get<DatosHabitacionReservaViewModel>((p1, p2) => p1 == p2));

            }
        }

        [Fact]
        public async Task vistaSeleccionHabitacionReservaPost_ModelIsValid()
        {
            // Arrange
            using (context)
            {
                var controller = new ReservasController(context);
                // ** LO QUE TIENE LA BBDD
                Cliente cliente2 = (new Cliente { idPersona = 2, nombre = "Prueba2", apellidos = "Prueba2 Prueba2", telefono = "777777777", correoElectronico = "prueba2@prueba2.com", nif = "87654321B", numeroTarjeta = "4444333322221111" });
                Habitacion habitacion1 = new Habitacion { numero = 1, precio = 20 };
                Habitacion habitacion2 = new Habitacion { numero = 2, precio = 40 };
                Reserva reserva1 = new Reserva { idReserva = 1, fechaInicio = new DateTime(2016, 11, 25), fechaFin = new DateTime(2016, 11, 27) };
                Reserva reserva2 = new Reserva { idReserva = 2, fechaInicio = new DateTime(2016, 11, 10), fechaFin = new DateTime(2016, 11, 12) };
                ReservaHabitacion reservaHabitacion1 = new ReservaHabitacion { idReservaHabitacion = 1 };
                ReservaHabitacion reservaHabitacion2 = new ReservaHabitacion { idReservaHabitacion = 2 };
                Descuento descuento1 = new Descuento { idDescuento = 1, fechaInicio = new DateTime(2016, 11, 25), fechaFin = new DateTime(2016, 11, 29) };


                reservaHabitacion1.Reserva = reserva1;
                reservaHabitacion1.Habitacion = habitacion1;
                reservaHabitacion2.Reserva = reserva2;
                reservaHabitacion2.Habitacion = habitacion2;
                // ** LO QUE TIENE LA BBDD


                //Reserva nueva:
                Reserva nuevaReserva = new Reserva {
                    idReserva = 3,
                    comentarios = "patata",
                    fechaFin = new DateTime(2016, 11, 26),
                    fechaInicio = new DateTime(2016, 11, 24),
                    regimenComida = 1,
                    Descuento = descuento1,
                    fechaRealizacion = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),
                    estado = 0,
                    Cliente = cliente2,
                    ReservaHabitacion = new ReservaHabitacion[1] { new ReservaHabitacion { idReservaHabitacion = 3, Habitacion = habitacion2 } }.ToList()
                };
                List<Reserva> expectModelReserva = new Reserva[3] { reserva1, reserva2, nuevaReserva}.ToList();

                List<ReservaHabitacion> expectModelReservaHabitacion = new ReservaHabitacion[3] { reservaHabitacion1, reservaHabitacion2, new ReservaHabitacion {
                    idReservaHabitacion = 3,
                    Habitacion = habitacion2,
                    Reserva = nuevaReserva
                } }.ToList();

                DatosHabitacionReservaViewModel entrada = new DatosHabitacionReservaViewModel
                {
                    idPersona = 2,
                    nombre = "Prueba2",
                    apellidos = "Prueba2 Prueba2",
                    nif = "87654321B",
                    comentarios = "patata",
                    fechaFin = new DateTime(2016, 11, 26),
                    fechaInicio = new DateTime(2016, 11, 24),
                    regimenComida = 1,
                    Descuentos = new Descuento[1] { new Descuento { idDescuento = 2 } },
                    Habitaciones = new Habitacion[1] { new Habitacion { numero = 2 } },
                    descuentoSeleccionado = "1",
                    habitacionesSeleccionadas = new String[]{"2"}
                };

                // Act
                var result = await controller.vistaSeleccionHabitacionReservaPost(entrada);
                //Assert
                var viewResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal(viewResult.ActionName, "Index");
                Assert.Equal(viewResult.ControllerName, "Clientes");

                //ReservaCreada
                List<Reserva> model1 = context.Reserva.ToList();
                Assert.Equal(expectModelReserva, model1, Comparer.Get<Reserva>((p1, p2) => p1.idReserva == p2.idReserva &&
                                                                                            p1.estado == p2.estado && p1.regimenComida == p2.regimenComida && p1.comentarios == p2.comentarios &&
                                                                                            p1.fechaFin == p2.fechaFin && p1.fechaInicio == p2.fechaInicio && p1.fechaRealizacion == p2.fechaRealizacion));
                
                //LineaReservaCreada
                List<ReservaHabitacion> model2 = context.ReservaHabitacion.ToList();
                Assert.Equal(expectModelReservaHabitacion, model2, Comparer.Get<ReservaHabitacion>((p1, p2) => p1.idReservaHabitacion == p2.idReservaHabitacion));
                
            }
        }
        
    }
}
