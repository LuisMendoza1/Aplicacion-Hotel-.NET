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
using Xunit;

namespace TeamCatHotel.test.Controller
{
    public class HabitacionsController_Index_test
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

        public HabitacionsController_Index_test()
        {
            _contextOptions = CreateNewContextOptions();
            context = new ApplicationDbContext(_contextOptions);
            // Insert seed data into the database using one instance of the context
            context.Habitacion.Add(new Habitacion { numero = 1, ocupada = true, aforo = 1, descripcion = "", localizacion = "", precio = 1 });
            context.Habitacion.Add(new Habitacion { numero = 2, ocupada = false, aforo = 1, descripcion = "", localizacion = "", precio = 1 });
            context.SaveChanges();

        }

        
        [Fact]
        public async Task IndexOcupadaNull()
        {
            // Arrange
            using (context)
            {
                IEnumerable<Habitacion> expectedModel = new Habitacion[2] { new Habitacion { numero = 1, ocupada = true, aforo = 1, descripcion = "", localizacion = "", precio = 1 },
                                                                            new Habitacion { numero = 2, ocupada = false, aforo = 1, descripcion = "", localizacion = "", precio = 1 }};
                var controller = new HabitacionsController(context);
                // Act
                var result = await controller.Index(null);
                //Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                IEnumerable<Habitacion> model = (result as ViewResult).Model as IEnumerable<Habitacion>;
                Assert.Equal(expectedModel, model, Comparer.Get<Habitacion>((p1, p2) => p1.numero == p2.numero));
            }
        }

        [Fact]
        public async Task IndexOcupadaTrue()
        {
            // Arrange
            using (context)
            {
                IEnumerable<Habitacion> expectedModel = new Habitacion[2] { new Habitacion { numero = 1, ocupada = true, aforo = 1, descripcion = "", localizacion = "", precio = 1 },
                                                                            new Habitacion { numero = 2, ocupada = false, aforo = 1, descripcion = "", localizacion = "", precio = 1 }};
                var controller = new HabitacionsController(context);
                // Act
                var result = await controller.Index(true);
                //Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                IEnumerable<Habitacion> model = (result as ViewResult).Model as IEnumerable<Habitacion>;
                Assert.Equal(expectedModel, model, Comparer.Get<Habitacion>((p1, p2) => p1.numero == p2.numero));
            }
        }

        [Fact]
        public async Task IndexOcupadaFalse()
        {
            // Arrange
            using (context)
            {
                IEnumerable<Habitacion> expectedModel = new Habitacion[2] { new Habitacion { numero = 1, ocupada = true, aforo = 1, descripcion = "", localizacion = "", precio = 1 },
                                                                            new Habitacion { numero = 2, ocupada = false, aforo = 1, descripcion = "", localizacion = "", precio = 1 }};
                var controller = new HabitacionsController(context);
                // Act
                var result = await controller.Index(false);
                //Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                IEnumerable<Habitacion> model = (result as ViewResult).Model as IEnumerable<Habitacion>;
                Assert.Equal(expectedModel, model, Comparer.Get<Habitacion>((p1, p2) => p1.numero == p2.numero));
                Assert.Equal(viewResult.ViewData["ocupada"], "La habitacion seleccionada debe estar ocupada");
            }
        }
    }
}
