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
using TeamCatHotel.Services;

namespace TeamCatHotel.test.Controller
{
    public class MenusController_ContratarMenu_test
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
        public MenusController_ContratarMenu_test()
        {
            _contextOptions = CreateNewContextOptions();
            context = new ApplicationDbContext(_contextOptions);
            Reserva reserva = new Reserva
            {
                idReserva = 1,
                fechaInicio = new DateTime(2016, 10, 19, 12, 00, 00),
                fechaFin = new DateTime(2016, 10, 21, 12, 00, 00),
                regimenComida = Regimen.MEDIA
            };
            Habitacion habitacion = new Habitacion { numero = 100 };
            context.Reserva.Add(reserva);
            context.Habitacion.Add(habitacion);
            context.ReservaHabitacion.Add(new ReservaHabitacion
            {
                idReservaHabitacion = 1,
                Habitacion = habitacion,
                Reserva = reserva
            });


            Menu menuMP = new Menu
            {
                idServicio = 1,
                nombre = "TestMP",
                horaInicio = new DateTime(2016, 1, 1, 9, 0, 0),
                horaFin = new DateTime(2016, 1, 1, 11, 0, 0)
            };

            Menu menuPC = new Menu
            {
                idServicio = 2,
                nombre = "TestPC",
                horaInicio = new DateTime(2016, 1, 1, 13, 0, 0),
                horaFin = new DateTime(2016, 1, 1, 16, 0, 0)
            };

            context.Menu.Add(menuMP);
            context.Menu.Add(menuPC);

            context.SaveChanges();
        }

        [Fact]
        public async Task ContratarMenu_InvalidMenu()
        {
            //Arrange
            using (context)
            {
                var controller = new MenusController(context);
                ReservaMenuViewModel rmvm = new ReservaMenuViewModel();
                Menu menu = new Menu();

                ResultMenuViewModel remvm = new ResultMenuViewModel
                {
                    menu = menu,
                    result = "Error: Invalid menu"
                };

                SystemTime.Now = () => new DateTime(2016, 10, 20, 12, 00, 00);

                // Act
                var result = await controller.ContratarMenu(menu.idServicio, 100, menu.idServicio, 100);

                //Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.Equal("ContratarMenu", viewResult.ViewName);
                var mResult = Assert.IsType<ResultMenuViewModel>(viewResult.Model);
                Assert.Null(mResult.menu);
                Assert.Equal(remvm.result, mResult.result);
            }
        }

        [Fact]
        public async Task ContratarMenu_RoomNotFound()
        {
            //Arrange
            using (context)
            {
                var controller = new MenusController(context);
                ReservaMenuViewModel rmvm = new ReservaMenuViewModel();

                Menu menu = new Menu
                {
                    idServicio = 1,
                    nombre = "TestMP",
                    horaInicio = new DateTime(2016, 1, 1, 9, 0, 0),
                    horaFin = new DateTime(2016, 1, 1, 11, 0, 0)
                };

                ResultMenuViewModel remvm = new ResultMenuViewModel
                {
                    menu = menu,
                    result = "Error: Reserva not found"
                };

                SystemTime.Now = () => new DateTime(2016, 10, 20, 12, 00, 00);

                // Act
                var result = await controller.ContratarMenu(menu.idServicio, 101, menu.idServicio, 101);

                //Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.Equal("ContratarMenu", viewResult.ViewName);
                var mResult = Assert.IsType<ResultMenuViewModel>(viewResult.Model);
                Assert.Equal(remvm, mResult, Comparer.Get<ResultMenuViewModel>((rm1, rm2) => rm1.result == rm2.result && rm1.menu.idServicio == rm2.menu.idServicio));
            }
        }

        [Fact]
        public async Task ContratarMenu_NotPaid()
        {
            //Arrange
            using (context)
            {
                var controller = new MenusController(context);
                ReservaMenuViewModel exResult = new ReservaMenuViewModel();

                Menu menu = new Menu
                {
                    idServicio = 2,
                    nombre = "TestPC",
                    horaInicio = new DateTime(2016, 1, 1, 13, 0, 0),
                    horaFin = new DateTime(2016, 1, 1, 16, 0, 0)
                };

                exResult.nHabitacion = 100;
                exResult.idServicio = menu.idServicio;

                SystemTime.Now = () => new DateTime(2016, 10, 20, 12, 00, 00);

                // Act
                var result = await controller.ContratarMenu(menu.idServicio, 100, menu.idServicio, 100);

                //Assert
                var viewResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("SeleccionComensales", viewResult.ActionName);
                ReservaMenuViewModel vmResult = new ReservaMenuViewModel();
                List<string> keys = new List<string>();
                string aux;
                int i = 0;
                foreach (var key in viewResult.RouteValues.Keys)
                {
                    aux = Assert.IsType<string>(key);
                    Assert.Equal(aux, "key", Comparer.Get<string>((s1, s2) => s1 == "nHabitacion" || s1 == "idServicio"));
                    keys.Add(aux);
                    i++;
                }
                Assert.Equal(i, 2);

                i = 0;
                foreach (var c in viewResult.RouteValues.Values)
                {
                    aux = keys[i];
                    if (aux == "nHabitacion")
                    {
                        i++;
                        vmResult.nHabitacion = Assert.IsType<int>(c);
                    }
                    else if (aux == "idServicio")
                    {
                        i++;
                        vmResult.idServicio = Assert.IsType<int>(c);
                    }
                    else
                    {
                        i++;
                    }
                }

                Assert.Equal(i, 2);

                Assert.Equal(exResult, vmResult, Comparer.Get<ReservaMenuViewModel>((v1, v2) => v1.nHabitacion == v2.nHabitacion && v1.idServicio == v2.idServicio));
            }
        }

        [Fact]
        public async Task ContratarMenu_Paid()
        {
            //Arrange
            using (context)
            {
                var controller = new MenusController(context);
                Menu menu = new Menu
                {
                    idServicio = 1,
                    nombre = "TestMP",
                    horaInicio = new DateTime(2016, 1, 1, 9, 0, 0),
                    horaFin = new DateTime(2016, 1, 1, 11, 0, 0)
                };

                ResultMenuViewModel remvm = new ResultMenuViewModel
                {
                    menu = menu,
                    result = "Success"
                };

                SystemTime.Now = () => new DateTime(2016, 10, 20, 12, 00, 00);

                // Act
                var result = await controller.ContratarMenu(menu.idServicio, 100, menu.idServicio, 100);

                //Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.Equal("ContratarMenu", viewResult.ViewName);
                var mResult = Assert.IsType<ResultMenuViewModel>(viewResult.Model);
                Assert.Equal(remvm, mResult, Comparer.Get<ResultMenuViewModel>((rm1, rm2) => rm1.result == rm2.result && rm1.menu.idServicio == rm2.menu.idServicio));
            }
        }
    }
}
