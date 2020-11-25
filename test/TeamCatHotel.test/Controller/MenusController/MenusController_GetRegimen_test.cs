using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamCatHotel.Data;
using TeamCatHotel.Models;
using TeamCatHotel.Controllers;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using TeamCatHotel.Enums;

namespace TeamCatHotel.test.Controller
{
    public class MenusController_GetRegimen_test
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
        public MenusController_GetRegimen_test()
        {
            _contextOptions = CreateNewContextOptions();
            context = new ApplicationDbContext(_contextOptions);
        }

        [Fact]
        public void GetRegimen_WithoutMenu()
        {
            // Arrange
            Menu menu = null;
            int exRegimen = Regimen.INVALIDO;
            MenusController mc = new MenusController(context);

            // Act
            int regimen = mc.GetRegimen(menu);

            // Assert
            Assert.Equal(exRegimen, regimen);
        }

        [Fact]
        public void GetRegimen_Invalid()
        {
            // Arrange
            Menu menu = new Menu();
            menu.horaInicio = new DateTime(2016, 1, 1, 19, 0, 0);
            menu.horaFin = new DateTime(2016, 1, 1, 11, 0, 0);
            int exRegimen = Regimen.INVALIDO;
            MenusController mc = new MenusController(context);

            // Act
            int regimen = mc.GetRegimen(menu);

            // Assert
            Assert.Equal(exRegimen, regimen);
        }

        [Fact]
        public void GetRegimen_HalfBoard()
        {
            // Arrange
            Menu menu = new Menu();
            menu.horaInicio = new DateTime(2016, 1, 1, 9, 0, 0);
            menu.horaFin = new DateTime(2016, 1, 1, 11, 0, 0);
            menu.nombre = "TestMP";
            int exRegimen = Regimen.MEDIA;
            MenusController mc = new MenusController(context);

            // Act
            int regimen = mc.GetRegimen(menu);

            // Assert
            Assert.Equal(exRegimen, regimen);
        }

        [Fact]
        public void GetRegimen_FullBoard()
        {
            // Arrange
            Menu menu = new Menu();
            menu.horaInicio = new DateTime(2016, 1, 1, 13, 0, 0);
            menu.horaFin = new DateTime(2016, 1, 1, 16, 0, 0);
            menu.nombre = "TestPC";
            int exRegimen = Regimen.COMPLETA;
            MenusController mc = new MenusController(context);

            // Act
            int regimen = mc.GetRegimen(menu);

            // Assert
            Assert.Equal(exRegimen, regimen);
        }
    }
}
