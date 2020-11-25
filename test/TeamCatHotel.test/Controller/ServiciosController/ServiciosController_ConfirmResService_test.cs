using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TeamCatHotel.Controllers;
using TeamCatHotel.Data;
using TeamCatHotel.Models;
using TeamCatHotel.Models.SolicitaServicioViewModels;
using Xunit;

namespace TeamCatHotel.test.Controller
{
    public class ServiciosController_ConfirmResService_test
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

        DateTime fechaInicio, fechaFin;
        public ServiciosController_ConfirmResService_test()
        {
            _contextOptions = CreateNewContextOptions();
            context = new ApplicationDbContext(_contextOptions);

            Servicio serv = new Servicio
            {
                idServicio = 1,
                nombre = "masajes",
                precio = 20,
                descripcion = ""
            };

            Habitacion hab = new Habitacion
            {
                numero = 1,
                ocupada = true,
                aforo = 1,
                descripcion = "",
                localizacion = "",
                precio = 1
            };

            fechaFin = DateTime.Now.AddDays(1);
            fechaInicio = DateTime.Now.AddHours(1);


            context.Servicio.Add(serv);
            context.Habitacion.Add(hab);
            context.SaveChanges();
        }
        
        [Fact]
        public async Task ConfirmResServiceValid_test()
        {
            using (context)
            {
                ReservaServicio expectedModel = new ReservaServicio
                {
                    idReservaServicio = 1,
                    Servicio = context.Servicio.SingleOrDefault(m=>m.idServicio==1),
                    Habitacion = context.Habitacion.SingleOrDefault(m => m.numero == 1),
                    fechaInicio = fechaInicio,
                    fechaFin = fechaFin
                };

                SelectedServiceViewModel selectedservViewModel = new SelectedServiceViewModel { idServicio = 1, numeroHab = 1, fechaInicio = fechaInicio, fechaFin = fechaFin };

                ServiciosController controller = new ServiciosController(context);
                var result = await controller.ConfirmResService(selectedservViewModel);
                var viewResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal(viewResult.ActionName,"ListServices");
                var model = context.ReservaServicio.SingleOrDefault(m=>m.Habitacion.numero == 1 && m.Servicio.idServicio==1);
                Assert.Equal(expectedModel, model, Comparer.Get<ReservaServicio>((p1, p2) => p1.idReservaServicio == p2.idReservaServicio));
            }
        }

        [Fact]
        public async Task ConfirmResServiceNotValid_test()
        {
            using (context)
            {
                SelectedServiceViewModel selectedservViewModel = new SelectedServiceViewModel { idServicio = 1, numeroHab = 1, fechaInicio = DateTime.Now.AddDays(4), fechaFin = DateTime.Now.AddDays(1) };
                //SelectedServiceViewModel selserViewModel = new SelectedServiceViewModel { idServicio = 1, numeroHab = 1 };
                SelectedServiceViewModel expectedModel = new SelectedServiceViewModel { idServicio = 1, numeroHab = 1 };
                var controller = new ServiciosController(context);
                controller.ModelState.AddModelError("fechaFin", "la fecha de fin debe ser posterior a la fecha de inicio");
                // Act
                var result = await controller.ConfirmResService(selectedservViewModel);
                //Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                SelectedServiceViewModel model = (result as ViewResult).Model as SelectedServiceViewModel;
                Assert.Equal(expectedModel, model, Comparer.Get<SelectedServiceViewModel>((p1, p2) => p1.idServicio == p2.idServicio));
            }
        }
    }
}
