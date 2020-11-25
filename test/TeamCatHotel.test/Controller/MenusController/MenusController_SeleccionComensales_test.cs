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
    public class MenusController_SeleccionComensales_test
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
        public MenusController_SeleccionComensales_test()
        {
            _contextOptions = CreateNewContextOptions();
            context = new ApplicationDbContext(_contextOptions);
            Factura factura = new Factura { idFactura = 1 };
            Reserva reserva = new Reserva
            {
                idReserva = 1,
                Factura = factura,
                fechaInicio = new DateTime(2016, 10, 19, 12, 00, 00),
                fechaFin = new DateTime(2016, 10, 21, 12, 00, 00)
            };

            Reserva reserva2 = new Reserva
            {
                idReserva = 2,
                fechaInicio = new DateTime(2016, 10, 19, 12, 00, 00),
                fechaFin = new DateTime(2016, 10, 21, 12, 00, 00)
            };

            Menu menu = new Menu
            {
                idServicio = 1,
                nombre = "TestPC",
                horaInicio = new DateTime(2001, 12, 13, 13, 00, 00),
                horaFin = new DateTime(2001, 12, 13, 16, 00, 00),
                precio = 10F
            };

            factura.Reserva = reserva;
            Habitacion habitacion = new Habitacion { numero = 101 };
            Habitacion habitacion2 = new Habitacion { numero = 102 };
            context.Factura.Add(factura);
            context.Reserva.Add(reserva);
            context.Reserva.Add(reserva2);
            context.Habitacion.Add(habitacion);
            context.Habitacion.Add(habitacion2);
            context.ReservaHabitacion.Add(new ReservaHabitacion { idReservaHabitacion = 1, Habitacion = habitacion, Reserva = reserva });
            context.ReservaHabitacion.Add(new ReservaHabitacion { idReservaHabitacion = 2, Habitacion = habitacion2, Reserva = reserva2 });
            context.Menu.Add(menu);

            context.SaveChanges();
        }

        [Fact]
        public async Task SeleccionComensales_InvalidHabitacion()
        {
            //Arrange
            using (context)
            {
                var controller = new MenusController(context);
                int nComensales = 1;

                Menu menu = new Menu
                {
                    idServicio = 1,
                    nombre = "TestPC",
                    horaInicio = new DateTime(2001, 12, 13, 13, 00, 00),
                    horaFin = new DateTime(2001, 12, 13, 16, 00, 00),
                    precio = 10F
                };

                Habitacion habitacion = new Habitacion { numero = 0 };

                ResultMenuViewModel remvm = new ResultMenuViewModel
                {
                    menu = menu,
                    result = "Failure"
                };

                // Act
                var result = await controller.SeleccionComensales(nComensales, habitacion.numero, menu.idServicio, "");

                //Assert
                var viewResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("ContratarMenu", viewResult.ActionName);
                ResultMenuViewModel vmResult = new ResultMenuViewModel();
                List<string> keys = new List<string>();
                string aux;
                int i = 0;
                foreach (var key in viewResult.RouteValues.Keys)
                {
                    aux = Assert.IsType<string>(key);
                    Assert.Equal(aux, "key", Comparer.Get<string>((s1, s2) => s1 == "result" || s1 == "menu"));
                    keys.Add(aux);
                    i++;
                }
                Assert.Equal(i, 2);

                i = 0;
                foreach (var c in viewResult.RouteValues.Values)
                {
                    aux = keys[i];
                    if (aux == "result")
                    {
                        i++;
                        vmResult.result = Assert.IsType<string>(c);
                    }
                    else if (aux == "menu")
                    {
                        i++;
                        vmResult.menu = Assert.IsType<Menu>(c);
                    }
                    else
                    {
                        i++;
                    }
                }
                Assert.Equal(remvm, vmResult, Comparer.Get<ResultMenuViewModel>((rm1, rm2) => rm1.result == rm2.result && rm1.menu.idServicio == rm2.menu.idServicio));
            }
        }

        [Fact]
        public async Task SeleccionComensales_InvalidMenu()
        {
            //Arrange
            using (context)
            {
                var controller = new MenusController(context);

                Menu menu = new Menu
                {
                    idServicio = 0,
                    nombre = "TestPC",
                    horaInicio = new DateTime(2001, 12, 13, 13, 00, 00),
                    horaFin = new DateTime(2001, 12, 13, 16, 00, 00),
                    precio = 10F
                };

                Habitacion habitacion = new Habitacion { numero = 101 };

                int nComensales = 1;

                ResultMenuViewModel remvm = new ResultMenuViewModel
                {
                    menu = menu,
                    result = "Failure"
                };

                // Act
                var result = await controller.SeleccionComensales(nComensales, habitacion.numero, menu.idServicio, "");

                //Assert
                var viewResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("ContratarMenu", viewResult.ActionName);
                string vmResult = "NOP";
                List<string> keys = new List<string>();
                string aux;
                int i = 0;
                foreach (var key in viewResult.RouteValues.Keys)
                {
                    aux = Assert.IsType<string>(key);
                    Assert.Equal(aux, "key", Comparer.Get<string>((s1, s2) => s1 == "result" || s1 == "menu"));
                    keys.Add(aux);
                    i++;
                }
                Assert.Equal(i, 2);

                i = 0;
                foreach (var c in viewResult.RouteValues.Values)
                {
                    aux = keys[i];
                    if (aux == "result")
                    {
                        i++;
                        vmResult = Assert.IsType<string>(c);
                    }
                    else if (aux == "menu")
                    {
                        i++;
                        Assert.Null(c);
                    }
                    else
                    {
                        i++;
                    }
                }

                Assert.Equal(remvm.result, vmResult);
            }
        }

        [Fact]
        public async Task SeleccionComensales_InvalidNComensales()
        {
            //Arrange
            using (context)
            {
                var controller = new MenusController(context);

                Menu menu = new Menu
                {
                    idServicio = 1,
                    nombre = "TestPC",
                    horaInicio = new DateTime(2001, 12, 13, 13, 00, 00),
                    horaFin = new DateTime(2001, 12, 13, 16, 00, 00),
                    precio = 10F
                };

                Habitacion habitacion = new Habitacion { numero = 101 };

                ReservaMenuViewModel rmvm = new ReservaMenuViewModel
                {
                    idServicio = menu.idServicio,
                    nHabitacion = habitacion.numero
                };

                int nComensales = 0;

                ReservaMenuViewModel exModel = new ReservaMenuViewModel();
                exModel.nHabitacion = habitacion.numero;
                exModel.idServicio = menu.idServicio;

                // Act
                var result = await controller.SeleccionComensales(nComensales, habitacion.numero, menu.idServicio, "");

                //Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.Equal("SeleccionComensales", viewResult.ViewName);
                Assert.Equal(exModel, viewResult.Model as ReservaMenuViewModel, Comparer.Get<ReservaMenuViewModel>((rm1, rm2) => rm1.nHabitacion == rm2.nHabitacion && rm1.idServicio == rm2.idServicio));
            }
        }

        [Fact]
        public async Task SeleccionComensales_FacturaNotFound()
        {
            //Arrange
            using (context)
            {
                var controller = new MenusController(context);

                Menu menu = new Menu
                {
                    idServicio = 1,
                    nombre = "TestPC",
                    horaInicio = new DateTime(2001, 12, 13, 13, 00, 00),
                    horaFin = new DateTime(2001, 12, 13, 16, 00, 00),
                    precio = 10F
                };

                ResultMenuViewModel remvm = new ResultMenuViewModel
                {
                    menu = menu,
                    result = "Error: couldn't get factura"
                };

                int nComensales = 1;

                Habitacion habitacion = new Habitacion { numero = 102 };

                ReservaMenuViewModel rmvm = new ReservaMenuViewModel
                {
                    idServicio = menu.idServicio,
                    nHabitacion = habitacion.numero
                };

                SystemTime.Now = () => new DateTime(2016, 10, 20, 12, 00, 00);

                // Act
                var result = await controller.SeleccionComensales(nComensales, habitacion.numero, menu.idServicio, "");

                //Assert
                var viewResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("ContratarMenu", viewResult.ActionName);
                ResultMenuViewModel vmResult = new ResultMenuViewModel();
                List<string> keys = new List<string>();
                string aux;
                int i = 0;
                foreach (var key in viewResult.RouteValues.Keys)
                {
                    aux = Assert.IsType<string>(key);
                    Assert.Equal(aux, "key", Comparer.Get<string>((s1, s2) => s1 == "result" || s1 == "menu"));
                    keys.Add(aux);
                    i++;
                }
                Assert.Equal(i, 2);

                i = 0;
                foreach (var c in viewResult.RouteValues.Values)
                {
                    aux = keys[i];
                    if (aux == "result")
                    {
                        i++;
                        vmResult.result = Assert.IsType<string>(c);
                    }
                    else if (aux == "menu")
                    {
                        i++;
                        vmResult.menu = Assert.IsType<Menu>(c);
                    }
                    else
                    {
                        i++;
                    }
                }
                Assert.Equal(remvm, vmResult, Comparer.Get<ResultMenuViewModel>((rm1, rm2) => rm1.result == rm2.result && rm1.menu.idServicio == rm2.menu.idServicio));
            }
        }

        [Fact]
        public async Task SeleccionComensales_Failure()
        {
            //Arrange
            using (context)
            {
                var controller = new MenusController(context);

                Menu menu = new Menu
                {
                    idServicio = 1,
                    nombre = "TestPC",
                    horaInicio = new DateTime(2001, 12, 13, 13, 00, 00),
                    horaFin = new DateTime(2001, 12, 13, 16, 00, 00),
                    precio = 10F
                };

                int nComensales = 1;
                int nHabitacion = 101;

                ReservaMenuViewModel rmvm = new ReservaMenuViewModel
                {
                    idServicio = menu.idServicio,
                    nHabitacion = nHabitacion
                };

                ResultMenuViewModel remvm = new ResultMenuViewModel
                {
                    menu = menu,
                    result = "Error: out of time"
                };

                SystemTime.Now = () => new DateTime(2016, 10, 20, 12, 00, 00);

                // Act
                var result = await controller.SeleccionComensales(nComensales, rmvm.nHabitacion, rmvm.idServicio, "");

                //Assert
                var viewResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("ContratarMenu", viewResult.ActionName);
                ResultMenuViewModel vmResult = new ResultMenuViewModel();
                List<string> keys = new List<string>();
                string aux;
                int i = 0;
                foreach (var key in viewResult.RouteValues.Keys)
                {
                    aux = Assert.IsType<string>(key);
                    Assert.Equal(aux, "key", Comparer.Get<string>((s1, s2) => s1 == "result" || s1 == "menu"));
                    keys.Add(aux);
                    i++;
                }
                Assert.Equal(i, 2);

                i = 0;
                foreach (var c in viewResult.RouteValues.Values)
                {
                    aux = keys[i];
                    if (aux == "result")
                    {
                        i++;
                        vmResult.result = Assert.IsType<string>(c);
                    }
                    else if (aux == "menu")
                    {
                        i++;
                        vmResult.menu = Assert.IsType<Menu>(c);
                    }
                    else
                    {
                        i++;
                    }
                }
                Assert.Equal(remvm, vmResult, Comparer.Get<ResultMenuViewModel>((rm1, rm2) => rm1.result == rm2.result && rm1.menu.idServicio == rm2.menu.idServicio));
            }
        }
        
        [Fact]
        public async Task SeleccionComensales_Success()
        {
            //Arrange
            using (context)
            {
                var controller = new MenusController(context);

                Menu menu = new Menu
                {
                    idServicio = 1,
                    nombre = "TestPC",
                    horaInicio = new DateTime(2001, 12, 13, 13, 00, 00),
                    horaFin = new DateTime(2001, 12, 13, 16, 00, 00),
                    precio = 10F
                };

                int nComensales = 1;
                Factura factura = new Factura { idFactura = 1 };
                Reserva reserva = new Reserva { idReserva = 1, Factura = factura };
                factura.Reserva = reserva;
                int nHabitacion = 101;

                ReservaMenuViewModel rmvm = new ReservaMenuViewModel
                {
                    idServicio = menu.idServicio,
                    nHabitacion = nHabitacion
                };

                ResultMenuViewModel remvm = new ResultMenuViewModel
                {
                    menu = menu,
                    result = "Success"
                };

                LineaFactura exLF = new LineaFactura { idLineaFactura = 1, Factura = factura, precio = menu.precio };
                ReservaServicio exRS = new ReservaServicio
                {
                    idReservaServicio = 1,
                    Habitacion = new Habitacion { numero = 101 },
                    fechaInicio = new DateTime(2016, 10, 20, 13, 00, 00),
                    fechaFin = new DateTime(2016, 10, 20, 16, 00, 00),
                    LineaFactura = exLF,
                    Servicio = menu
                };
                exLF.ReservaServicio = exRS;
                IEnumerable<LineaFactura> exLFModel = new List<LineaFactura> { exLF };
                IEnumerable<ReservaServicio> exRSModel = new List<ReservaServicio> { exRS };

                SystemTime.Now = () => new DateTime(2016, 10, 20, 14, 00, 00);

                // Act
                var result = await controller.SeleccionComensales(nComensales, rmvm.nHabitacion, rmvm.idServicio, "");

                //Assert
                var viewResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("ContratarMenu", viewResult.ActionName);
                ResultMenuViewModel vmResult = new ResultMenuViewModel();
                List<string> keys = new List<string>();
                string aux;
                int i = 0;
                foreach (var key in viewResult.RouteValues.Keys)
                {
                    aux = Assert.IsType<string>(key);
                    Assert.Equal(aux, "key", Comparer.Get<string>((s1, s2) => s1 == "result" || s1 == "menu"));
                    keys.Add(aux);
                    i++;
                }
                Assert.Equal(i, 2);

                i = 0;
                foreach (var c in viewResult.RouteValues.Values)
                {
                    aux = keys[i];
                    if (aux == "result")
                    {
                        i++;
                        vmResult.result = Assert.IsType<string>(c);
                    }
                    else if (aux == "menu")
                    {
                        i++;
                        vmResult.menu = Assert.IsType<Menu>(c);
                    }
                    else
                    {
                        i++;
                    }
                }
                Assert.Equal(remvm, vmResult, Comparer.Get<ResultMenuViewModel>((rm1, rm2) => rm1.result == rm2.result && rm1.menu.idServicio == rm2.menu.idServicio));

                IEnumerable<ReservaServicio> rsModel = context.ReservaServicio.ToList<ReservaServicio>();
                IEnumerable<LineaFactura> lfModel = context.LineaFactura.ToList<LineaFactura>();
                Assert.Equal(exRSModel,
                            rsModel,
                            Comparer.Get<ReservaServicio>((rs1, rs2) => rs1.idReservaServicio == rs2.idReservaServicio &&
                                                         rs1.Habitacion.numero == rs2.Habitacion.numero &&
                                                         rs1.LineaFactura.idLineaFactura == rs2.LineaFactura.idLineaFactura &&
                                                         rs1.Servicio.idServicio == rs2.Servicio.idServicio &&
                                                         rs1.fechaInicio.Equals(rs2.fechaInicio) &&
                                                         rs1.fechaFin.Equals(rs2.fechaFin)));
                Assert.Equal(exLFModel,
                            lfModel,
                            Comparer.Get<LineaFactura>((lf1, lf2) => lf1.idLineaFactura == lf2.idLineaFactura &&
                                                         lf1.precio == lf2.precio &&
                                                         lf1.Factura.idFactura == lf2.Factura.idFactura &&
                                                         lf1.ReservaServicio.idReservaServicio == lf2.ReservaServicio.idReservaServicio
                                                         ));
            }
        }
    }
}
