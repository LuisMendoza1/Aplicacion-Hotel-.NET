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
using Xunit;

namespace TeamCatHotel.test.Controller
{
    public class ClientesController_Index_test
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
        public ClientesController_Index_test()
        {
            _contextOptions = CreateNewContextOptions();
            context = new ApplicationDbContext(_contextOptions);
            // Insert seed data into the database using one instance of the context
            context.Cliente.Add(new Cliente { idPersona = 1, nombre = "Prueba1", apellidos = "Prueba1 Prueba1", telefono = "666666666", correoElectronico = "prueba1@prueba1.com", nif = "12345678A", numeroTarjeta = "4444333322221111" });
            context.Cliente.Add(new Cliente { idPersona = 2, nombre = "Prueba2", apellidos = "Prueba2 Prueba2", telefono = "777777777", correoElectronico = "prueba2@prueba2.com", nif = "87654321B", numeroTarjeta = "4444333322221111" });
            context.SaveChanges();
        }

        [Fact]
        public async Task Index_WithoutNIF()
        {
            // Arrange
            using (context)
            {
                IEnumerable<Cliente> expectedModel = new Cliente[2] { new Cliente { idPersona = 1, nombre = "Prueba1", apellidos = "Prueba1 Prueba1", telefono = "666666666", correoElectronico = "prueba1@prueba1.com", nif = "12345678A", numeroTarjeta = "4444333322221111" } ,
                                                                      new Cliente { idPersona = 2, nombre = "Prueba2", apellidos = "Prueba2 Prueba2", telefono = "777777777", correoElectronico = "prueba2@prueba2.com", nif = "87654321B", numeroTarjeta = "4444333322221111" }  };
                var controller = new ClientesController(context);
                // Act
                var result = await controller.Index("");
                //Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                IEnumerable<Cliente> model = (result as ViewResult).Model as IEnumerable<Cliente>;
                Assert.Equal(expectedModel, model, Comparer.Get<Cliente>((p1, p2) => p1.idPersona == p2.idPersona));
            }
        }

        [Fact]
        public async Task Index_WithNIF()
        {
            // Arrange
            using (context)
            {
                IEnumerable<Cliente> expectedModel = new Cliente[1] {new Cliente { idPersona = 2, nombre = "Prueba2", apellidos = "Prueba2 Prueba2", telefono = "777777777", correoElectronico = "prueba2@prueba2.com", nif = "87654321B", numeroTarjeta = "4444333322221111" }  };
                var controller = new ClientesController(context);
                // Act
                var result = await controller.Index("87654321B");
                //Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                IEnumerable<Cliente> model = (result as ViewResult).Model as IEnumerable<Cliente>;
                Assert.Equal(expectedModel, model, Comparer.Get<Cliente>((p1, p2) => p1.idPersona == p2.idPersona));
            }
        }

    }
}
