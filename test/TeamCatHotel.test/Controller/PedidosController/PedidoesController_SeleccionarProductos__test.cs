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
using TeamCatHotel.Models.PedidoViewModel;
using Xunit;

namespace TeamCatHotel.test.Controller
{
    public class PedidoesController_SeleccionarProductos__test
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

        public PedidoesController_SeleccionarProductos__test()
        {
            _contextOptions = CreateNewContextOptions();
            context = new ApplicationDbContext(_contextOptions);
            // Insert seed data into the database using one instance of the context


            Producto producto1 = new Producto
            {
                idProducto = 1,
                nombre = "producto 1",
                precio = 5
            };

            Producto producto2 = new Producto
            {
                idProducto = 2,
                nombre = "producto 2",
                precio = 6
            };
            Producto producto3 = new Producto
            {
                idProducto = 3,
                nombre = "producto 3",
                precio = 5
            };

            context.Producto.Add(producto1);
            context.Producto.Add(producto2);
            context.Producto.Add(producto3);
            context.SaveChanges();
        }
        /*
        [Fact]
        public async Task SeleccionarProductosGET()
        {
            //Arrange
            using (context)
            {
                var controller = new PedidoesController(context);

                SelectProductosViewModel modeloEsperado = new SelectProductosViewModel();
                Producto producto1 = new Producto
                {
                    idProducto = 1,
                    nombre = "producto 1",
                    precio = 5
                };

                Producto producto2 = new Producto
                {
                    idProducto = 2,
                    nombre = "producto 2",
                    precio = 6
                };
                Producto producto3 = new Producto
                {
                    idProducto = 3,
                    nombre = "producto 3",
                    precio = 5
                };
                IEnumerable<Producto> productos = new Producto[3] { producto1, producto2, producto3 };

                modeloEsperado.Productos = productos;

                //Act
                var result = await controller.SeleccionarProductos();
                //Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                SelectProductosViewModel modelo = (result as ViewResult).Model as SelectProductosViewModel;

                Assert.Equal(modeloEsperado.Productos, modelo.Productos
                    , Comparer.Get<Producto>((p1, p2) => p1.idProducto == p2.idProducto && p1.nombre == p2.nombre && p1.cantidad == p2.cantidad && p1.precio == p2.precio));
            }
        }

        [Fact]
        public async Task SeleccionarProductosPOST_ModelIsValid()
        {
            //Arrange
            using (context)
            {
                var controller = new PedidoesController(context);

                // Creamos modelo esperado
                SelectedProductosViewModel selectedProducts = new SelectedProductosViewModel();

                // Creamos objeto SelectProveedoresProductosViewModel con la respuesta de la llamada del controlador
                //Act
                var result = await controller.SeleccionarProductos(selectedProducts);
                //Assert
                var viewResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal(viewResult.ActionName, "SeleccionarProveedores");

                // Comprobamos que el contexto no se ha modificado. Recuperamos todos los productos de la Base de datos
                Producto producto1 = new Producto
                {
                    idProducto = 1,
                    nombre = "producto 1",
                    precio = 5
                };

                Producto producto2 = new Producto
                {
                    idProducto = 2,
                    nombre = "producto 2",
                    precio = 6
                };
                Producto producto3 = new Producto
                {
                    idProducto = 3,
                    nombre = "producto 3",
                    precio = 5
                };
                IEnumerable<Producto> productos = new Producto[3] { producto1, producto2, producto3 };

                List<Producto> productosBaseDatos = await context.Producto.ToListAsync();

                Assert.Equal(productos, productosBaseDatos
                    , Comparer.Get<Producto>((p1, p2) => p1.idProducto == p2.idProducto && p1.nombre == p2.nombre && p1.cantidad == p2.cantidad && p1.precio == p2.precio));

            }
        }

        [Fact]
        public async Task SeleccionarProductosPOST_ModelIsNotValid()
        {
            //Arrange
            using (context)
            {
                var controller = new PedidoesController(context);

                // Creamos modelo esperado
                SelectProductosViewModel modeloEsperado = new SelectProductosViewModel();

                Producto producto1 = new Producto
                {
                    idProducto = 1,
                    nombre = "producto 1",
                    precio = 5
                };

                Producto producto2 = new Producto
                {
                    idProducto = 2,
                    nombre = "producto 2",
                    precio = 6
                };
                Producto producto3 = new Producto
                {
                    idProducto = 3,
                    nombre = "producto 3",
                    precio = 5
                };
                IEnumerable<Producto> productos = new Producto[3] { producto1, producto2, producto3 };

                modeloEsperado.Productos = productos;
                // Creamos objeto SelectProveedoresProductosViewModel con la respuesta de la llamada del controlador
                //Act
                controller.ModelState.AddModelError("ListaProductos", "Required");
                var result = await controller.SeleccionarProductos(null);

                var viewResult = Assert.IsType<ViewResult>(result);
                SelectProductosViewModel modelo = (result as ViewResult).Model as SelectProductosViewModel;

                Assert.Equal(modeloEsperado.Productos, modelo.Productos
                    , Comparer.Get<Producto>((p1, p2) => p1.idProducto == p2.idProducto && p1.nombre == p2.nombre && p1.cantidad == p2.cantidad && p1.precio == p2.precio));
            }
        }*/
    }
}