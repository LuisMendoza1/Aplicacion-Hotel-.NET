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
    public class ReservasController_vistaDatosReservaPost_test
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
        public ReservasController_vistaDatosReservaPost_test()
        {
            _contextOptions = CreateNewContextOptions();
            context = new ApplicationDbContext(_contextOptions);
            // Insert seed data into the database using one instance of the context
            context.Cliente.Add(new Cliente { idPersona = 1, nombre = "Prueba1", apellidos = "Prueba1 Prueba1", telefono = "666666666", correoElectronico = "prueba1@prueba1.com", nif = "12345678A", numeroTarjeta = "4444333322221111" });
            context.Cliente.Add(new Cliente { idPersona = 2, nombre = "Prueba2", apellidos = "Prueba2 Prueba2", telefono = "777777777", correoElectronico = "prueba2@prueba2.com", nif = "87654321B", numeroTarjeta = "4444333322221111" });
            Habitacion habitacion1 = new Habitacion { precio = 20 };
            context.Habitacion.Add( habitacion1);
            Reserva reserva1 = new Reserva { fechaInicio = new DateTime(2018, 01, 01), fechaFin = new DateTime(2018, 02, 01) };
            ReservaHabitacion reservaHabitacion1 = new ReservaHabitacion();
            reservaHabitacion1.Reserva = reserva1;
            reservaHabitacion1.Habitacion = habitacion1;
            context.Reserva.Add(reserva1);
            context.ReservaHabitacion.Add(reservaHabitacion1);
            context.SaveChanges();
        }

        [Fact]
        public async Task vistaDatosReservaPost_ModelIsNotValid()
        {
            // Arrange
            using (context)
            {
                var controller = new ReservasController(context);
                controller.ModelState.AddModelError("fechaInicio", "Required");
                DatosReservaViewModel expectedModel = new DatosReservaViewModel { idPersona = 2};
                // Act
                var result = await controller.vistaDatosReservaPost(new DatosReservaViewModel { idPersona = 2 });
                //Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                DatosReservaViewModel model = (result as ViewResult).Model as DatosReservaViewModel;
                Assert.Equal(expectedModel, model, Comparer.Get<DatosReservaViewModel>((p1, p2) => p1.idPersona == p2.idPersona));

            }
        }

        [Fact]
        public async Task vistaDatosReservaPost_ModelIsValid()
        {
            // Arrange
            using (context)
            {
                ///////
                var controller = new ReservasController(context);
                DatosHabitacionReservaViewModel expectedModel = new DatosHabitacionReservaViewModel { idPersona = 2, nombre = "Prueba2", apellidos = "Prueba2 Prueba2", nif = "87654321B", comentarios = "patata", fechaFin = new DateTime(2016,12,27), fechaInicio = new DateTime(2016,12,25), regimenComida = 1 };
                // Act
                var result = await controller.vistaDatosReservaPost(new DatosReservaViewModel { idPersona = 2, nombre = "Prueba2", apellidos = "Prueba2 Prueba2", nif = "87654321B", comentarios = "patata", fechaFin = new DateTime(2016, 12, 27), fechaInicio = new DateTime(2016, 12, 25), regimenComida = 1 });
                //Assert
                var viewResult = Assert.IsType<RedirectToActionResult>(result);
                viewResult.ActionName = "vistaSeleccionHabitacionReserva";
                DatosHabitacionReservaViewModel resultado = new DatosHabitacionReservaViewModel {
                    idPersona = (int)(result as RedirectToActionResult).RouteValues["idPersona"],
                    nombre = (String)(result as RedirectToActionResult).RouteValues["nombre"],
                    apellidos = (String)(result as RedirectToActionResult).RouteValues["apellidos"],
                    nif = (String)(result as RedirectToActionResult).RouteValues["nif"],
                    fechaInicio = (DateTime)(result as RedirectToActionResult).RouteValues["fechaInicio"],
                    fechaFin = (DateTime)(result as RedirectToActionResult).RouteValues["fechaFin"],
                    comentarios = (String)(result as RedirectToActionResult).RouteValues["comentarios"],
                    regimenComida = (int)(result as RedirectToActionResult).RouteValues["regimenComida"]
                 };

                Assert.Equal(expectedModel, resultado, Comparer.Get<DatosHabitacionReservaViewModel>((p1, p2) => p1.idPersona == p2.idPersona && p1.nif == p2.nif && p1.comentarios == p2.comentarios && p1.fechaFin == p2.fechaFin && p1.fechaInicio == p2.fechaInicio && p1.regimenComida == p2.regimenComida));

            }
        }


        [Fact]
        public async Task vistaDatosReservaPost_ModelIsValid_BadDateBeforeToday()
        {
            // Arrange
            using (context)
            {
                var controller = new ReservasController(context);
                DatosReservaViewModel expectedModel = new DatosReservaViewModel { idPersona = 2 };
                // Act
                var result = await controller.vistaDatosReservaPost(new DatosReservaViewModel { idPersona = 2, nombre = "Prueba2", apellidos = "Prueba2 Prueba2", nif = "87654321B", comentarios = "patata", fechaFin = new DateTime(2016, 12, 27), fechaInicio = new DateTime(2016, 10, 25), regimenComida = 1 });
                //Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                DatosReservaViewModel model = (result as ViewResult).Model as DatosReservaViewModel;
                Assert.Equal(expectedModel, model, Comparer.Get<DatosReservaViewModel>((p1, p2) => p1.idPersona == p2.idPersona));

            }
        }


        [Fact]
        public async Task vistaDatosReservaPost_ModelIsValid_BadDates()
        {
            // Arrange
            using (context)
            {
                var controller = new ReservasController(context);
                DatosReservaViewModel expectedModel = new DatosReservaViewModel { idPersona = 2 };
                // Act
                var result = await controller.vistaDatosReservaPost(new DatosReservaViewModel { idPersona = 2, nombre = "Prueba2", apellidos = "Prueba2 Prueba2", nif = "87654321B", comentarios = "patata", fechaFin = new DateTime(2017, 10, 27), fechaInicio = new DateTime(2017, 12, 25), regimenComida = 1 });
                //Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                DatosReservaViewModel model = (result as ViewResult).Model as DatosReservaViewModel;
                Assert.Equal(expectedModel, model, Comparer.Get<DatosReservaViewModel>((p1, p2) => p1.idPersona == p2.idPersona));

            }
        }

        [Fact]
        public async Task vistaDatosReservaPost_ModelIsValid_NotRooms()
        {
            // Arrange
            using (context)
            {
                var controller = new ReservasController(context);
                DatosReservaViewModel expectedModel = new DatosReservaViewModel { idPersona = 2 };
                // Act
                var result = await controller.vistaDatosReservaPost(new DatosReservaViewModel { idPersona = 2, nombre = "Prueba2", apellidos = "Prueba2 Prueba2", nif = "87654321B", comentarios = "patata", fechaFin = new DateTime(2018, 01, 20), fechaInicio = new DateTime(2018, 01, 10), regimenComida = 1 });
                //Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                DatosReservaViewModel model = (result as ViewResult).Model as DatosReservaViewModel;
                Assert.Equal(expectedModel, model, Comparer.Get<DatosReservaViewModel>((p1, p2) => p1.idPersona == p2.idPersona));

            }
        }


    }
}
