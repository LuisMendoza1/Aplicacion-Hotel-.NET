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
    public class ServiciosControllerListServices_test
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

        public ServiciosControllerListServices_test()
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
        public async Task ListServicesID_Ocupada()
        {
            // Arrange
            using (context)
            {

                Habitacion hab = await context.Habitacion.SingleOrDefaultAsync<Habitacion>(m => m.numero == 1);
                ICollection<Servicio> listServices = await context.Servicio.ToListAsync();
                ListSolicitarServicioViewModel expectedModel = new ListSolicitarServicioViewModel { numeroHab = hab.numero, ListaServicios = listServices, ocupada = hab.ocupada };

                var controller = new ServiciosController(context);
                // Act
                var result = await controller.ListServices(1);
                //Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                ListSolicitarServicioViewModel model = (result as ViewResult).Model as ListSolicitarServicioViewModel;
                var expectedListServices = model.ListaServicios.ToList();
                Assert.Equal(expectedModel, model, Comparer.Get<ListSolicitarServicioViewModel>((p1, p2) => p1.numeroHab == p2.numeroHab && p1.ocupada == p2.ocupada));
                Assert.Equal(expectedListServices, listServices, Comparer.Get<Servicio>((p1, p2) => p1.idServicio == p2.idServicio));
            }

        }

        [Fact]
        public async Task ListServices_NoOcupada()
        {
            // Arrange
            using (context)
            {
                var controller = new ServiciosController(context);
                // Act
                var result = await controller.ListServices(2);
                //Assert
                var viewResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal(viewResult.ControllerName, "Habitacions");
                Assert.Equal(viewResult.ActionName, "Index");
            }

        }

        /* Variante si se devuelve una vista en lugar de una redireccion al meotodo index
         [Fact]
         public async Task CreateHabID_Ocupada()
        {
            // Arrange
            using (context)
            {
                IEnumerable<Servicio> expectedModel = new Servicio[2] { new Servicio { idServicio = 1, nombre="1",precio=5, descripcion="efg"},
                                                                            new Servicio { idServicio = 2, nombre="2", precio=5, descripcion="abc"}};
                var controller = new ServiciosController(context);
                // Act
                var result = await controller.Create(1);
                //Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.IsType<ViewResult>(viewResult);
                IEnumerable<Servicio> model = (result as ViewResult).Model as IEnumerable<Servicio>;
                Assert.Equal(expectedModel, model, Comparer.Get<Servicio>((p1, p2) => p1.idServicio == p2.idServicio));
            }

        }
         */
    }
}
