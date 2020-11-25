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
    public class ServiciosController_SelectService_test
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

        public ServiciosController_SelectService_test()
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
        public async Task SelectService_ModelIsValid()
        {
            // Arrange
            using (context)
            {

                SelectedServiceViewModel selserViewModel = new SelectedServiceViewModel { idServicio = 1, numeroHab = 1 };
                var controller = new ServiciosController(context);
                // Act
                var result = await controller.SelectService(selserViewModel);
                //Assert
                var viewResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal(viewResult.ActionName, "SetReservaServicio");
            }

        }
        [Fact]
        public async Task SelectService_ModelIsNotValid()
        {
            // Arrange
            using (context)
            {
                SelectedServiceViewModel selserViewModel = new SelectedServiceViewModel{};
                
                var controller = new ServiciosController(context);
                controller.ModelState.AddModelError("idServicio", "requerido");
                controller.ModelState.AddModelError("numeroHab", "requerido");
                // Act
                var result = await controller.SelectService(selserViewModel);
                //Assert
                var viewResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal(viewResult.ActionName, "Index");
                Assert.Equal(viewResult.ControllerName, "Habitacions");
            }

        }
        

    }
}
