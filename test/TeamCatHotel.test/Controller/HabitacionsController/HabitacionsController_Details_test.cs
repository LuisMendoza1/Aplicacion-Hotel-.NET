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
    
    public class HabitacionsController_Details_test
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

        public HabitacionsController_Details_test()
        {
            _contextOptions = CreateNewContextOptions();
            context = new ApplicationDbContext(_contextOptions);
            // Insert seed data into the database using one instance of the context
            context.Habitacion.Add(new Habitacion { numero = 1, ocupada = true, aforo = 1, descripcion = "", localizacion = "", precio = 1 });
            context.Habitacion.Add(new Habitacion { numero = 2, ocupada = false, aforo = 1, descripcion = "", localizacion = "", precio = 1 });
            context.SaveChanges();

        }


        [Fact]
        public async Task Detail_withoutId()
        {
            // Arrange
            using (context)
            {
                var controller = new HabitacionsController(context);
                // Act
                var result = await controller.Details(null);
                //Assert
                var viewResult = Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public async Task Detail_withIdNoExist()
        {
            // Arrange
            using (context)
            {
                var controller = new HabitacionsController(context);
                // Act
                var result = await controller.Details(20);
                //Assert
                var viewResult = Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public async Task Detail_withIdExist()
        {
            // Arrange
            using (context)
            {
                Habitacion expectedModel = new Habitacion { numero = 1, ocupada = true, aforo = 1, descripcion = "", localizacion = "", precio = 1 };
                var controller = new HabitacionsController(context);

                // Act
                var result = await controller.Details(1);

                //Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                Habitacion model = (result as ViewResult).Model as Habitacion;
                Assert.Equal(expectedModel, model, Comparer.Get<Habitacion>((p1, p2) => p1.numero == p2.numero));
            }
        }

    }
}
