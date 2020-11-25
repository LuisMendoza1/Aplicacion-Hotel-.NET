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
    public class ReservasController_vistaDatosReserva_test
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
        public ReservasController_vistaDatosReserva_test()
        {
            _contextOptions = CreateNewContextOptions();
            context = new ApplicationDbContext(_contextOptions);
            // Insert seed data into the database using one instance of the context
            context.Cliente.Add(new Cliente { idPersona = 1, nombre = "Prueba1", apellidos = "Prueba1 Prueba1", telefono = "666666666", correoElectronico = "prueba1@prueba1.com", nif = "12345678A", numeroTarjeta = "4444333322221111" });
            context.Cliente.Add(new Cliente { idPersona = 2, nombre = "Prueba2", apellidos = "Prueba2 Prueba2", telefono = "777777777", correoElectronico = "prueba2@prueba2.com", nif = "87654321B", numeroTarjeta = "4444333322221111" });
            context.SaveChanges();
        }

        [Fact]
        public async Task vistaDatosReserva()
        {
            // Arrange
            using (context)
            {

                var controller = new ReservasController(context);
                DatosReservaViewModel expectedModel = new DatosReservaViewModel { idPersona = 2, nombre = "Prueba2", apellidos = "Prueba2 Prueba2", nif = "87654321B" };

                // Act
                var result = await controller.vistaDatosReserva(2);
                //Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                DatosReservaViewModel model = (result as ViewResult).Model as DatosReservaViewModel;
                Assert.Equal(expectedModel, model, Comparer.Get<DatosReservaViewModel>((p1, p2) => p1.idPersona == p2.idPersona && p1.nif == p2.nif && p1.nombre == p2.nombre && p1.apellidos == p2.apellidos));

            }
        }



    }
}
