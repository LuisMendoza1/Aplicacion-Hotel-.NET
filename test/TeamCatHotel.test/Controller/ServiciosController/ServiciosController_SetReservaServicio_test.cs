using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamCatHotel.Controllers;
using TeamCatHotel.Data;
using TeamCatHotel.Models;
using TeamCatHotel.Models.SolicitaServicioViewModels;
using Xunit;

namespace TeamCatHotel.test.Controller
{
    public class ServiciosController_SetReservaServicio_test
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

        public ServiciosController_SetReservaServicio_test()
        {
            _contextOptions = CreateNewContextOptions();
            context = new ApplicationDbContext(_contextOptions);
            // Insert seed data into the database using one instance of the context
            context.Servicio.Add(new Servicio { idServicio = 1, nombre = "1", precio = 5, descripcion = "efg" });
            context.Servicio.Add(new Servicio { idServicio = 2, nombre = "2", precio = 5, descripcion = "abc" });
            context.Habitacion.Add(new Habitacion { numero = 1, ocupada = true, aforo = 1, descripcion = "", localizacion = "", precio = 1 });
            context.Habitacion.Add(new Habitacion { numero = 2, ocupada = false, aforo = 1, descripcion = "", localizacion = "", precio = 1 });
            context.SaveChanges();

        }

        [Fact]
        public async Task SetReservaServicio_ModelIsValid()
        {
            // Arrange
            using (context)
            {

                SelectedServiceViewModel selserViewModel = new SelectedServiceViewModel { idServicio = 1, numeroHab = 1 };
                SelectedServiceViewModel expectedModel = new SelectedServiceViewModel { idServicio = 1, numeroHab = 1 };
                var controller = new ServiciosController(context);
                // Act
                var result = await controller.SetReservaServicio(selserViewModel);
                //Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                SelectedServiceViewModel model = (result as ViewResult).Model as SelectedServiceViewModel;
                Assert.Equal(expectedModel, model, Comparer.Get<SelectedServiceViewModel>((p1, p2) => p1.idServicio == p2.idServicio));
            }

        }
        [Fact]
        public async Task SetReservaServicio_ModelIsNotValid()
        {
            // Arrange
            using (context)
            {

                SelectedServiceViewModel selserViewModel = new SelectedServiceViewModel { idServicio = 1};

                var controller = new ServiciosController(context);
                controller.ModelState.AddModelError("numeroHab", "requerido");
                // Act
                var result = await controller.SetReservaServicio(selserViewModel);
                //Assert
                var viewResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal(viewResult.ActionName, "ListServices");
            }

        }
    }
}
