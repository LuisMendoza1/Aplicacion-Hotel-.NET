/*using Microsoft.AspNetCore.Mvc;
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
    public class PedidoesController_SeleccionarProveedores__test
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
        public PedidoesController_SeleccionarProveedores__test()
        {
            _contextOptions = CreateNewContextOptions();
            context = new ApplicationDbContext(_contextOptions);
            // Insert seed data into the database using one instance of the context


            Producto producto1 = new Producto
            {
                idProducto = 1,
                nombre = "producto 1",
                precio = 5 };

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
            Producto producto4 = new Producto
            {
                idProducto = 4,
                nombre = "producto 4",
                precio = 5
            };

            Proveedor proveedor1 = new Proveedor
            {
                idProveedor = 1,
                cif = "123",
                correoElectronico = "123@",
                direccion = "calle1",
            };
            Proveedor proveedor2 = new Proveedor
            {
                idProveedor = 2,
                cif = "321",
                correoElectronico = "142@",
                direccion = "calle2",
            };
            Proveedor proveedor3 = new Proveedor
            {
                idProveedor = 3,
                cif = "333",
                correoElectronico = "143@",
                direccion = "calle3",
            };

            ProductoProveedor prodprov1 = new ProductoProveedor
            {
                idProductoProveedor = 1,
                Producto = producto1,
                Proveedor = proveedor1
            };
            ProductoProveedor prodprov2 = new ProductoProveedor
            {
                idProductoProveedor = 2,
                Producto = producto2,
                Proveedor = proveedor1
            };
            ProductoProveedor prodprov3 = new ProductoProveedor
            {
                idProductoProveedor = 3,
                Producto = producto3,
                Proveedor = proveedor3
            };
            ProductoProveedor prodprov4 = new ProductoProveedor
            {
                idProductoProveedor = 4,
                Producto = producto4,
                Proveedor = proveedor3
            };
            context.Producto.Add(producto1);
            context.Producto.Add(producto2);
            context.Producto.Add(producto3);
            context.Producto.Add(producto4);
            context.Proveedor.Add(proveedor1);
            context.Proveedor.Add(proveedor2);
            context.Proveedor.Add(proveedor3);
            context.ProductoProveedor.Add(prodprov1);
            context.ProductoProveedor.Add(prodprov2);
            context.ProductoProveedor.Add(prodprov3);
            context.ProductoProveedor.Add(prodprov4);

            context.SaveChanges();

        }
        
        [Fact]
        public async Task SeleccionarProveedoresGET()
        {
            //Arrange
            using (context)
            {
                //Para seleccionar los proveedores necesitamos los productos seleccionados, por eso inicializamos todos los valores.
                var controller = new PedidoesController(context);

                Producto producto1 = new Producto
                {
                    idProducto = 1,
                    nombre = "producto 1",
                    cantidad = 4,
                    precio = 5
                };

                Producto producto2 = new Producto
                {
                    idProducto = 2,
                    nombre = "producto 2",
                    cantidad = 2,
                    precio = 6
                };
                Proveedor proveedor1 = new Proveedor
                {
                    idProveedor = 1,
                    cif = "123",
                    correoElectronico = "123@",
                    direccion = "calle1",
                };
                Proveedor proveedor2 = new Proveedor
                {
                    idProveedor = 2,
                    cif = "321",
                    correoElectronico = "142@",
                    direccion = "calle2",
                };

                List<Producto> productosSeleccionados = new List<Producto>();
                productosSeleccionados.Add(producto1);
                productosSeleccionados.Add(producto2);

                int[] cantidades = new int[2];
                cantidades[0] = 4;
                cantidades[1] = 3;

                List<Proveedor> proveedoresObtenidos = new List<Proveedor>();
                proveedoresObtenidos.Add(proveedor1);

                List<LineaPedido> lineasPedido = new List<LineaPedido>();
                int indice = 0;
                foreach (Producto productoSeleccionado in productosSeleccionados)
                {
                    LineaPedido lineaPedido = new LineaPedido();
                    int cantidad = cantidades[indice];
                    lineaPedido.cantidad = cantidad;
                    lineaPedido.precio = productoSeleccionado.precio * cantidad;
                    lineaPedido.producto = productoSeleccionado;
                    lineasPedido.Add(lineaPedido); //vamos añadiendo cada linea a mi lista de lineasPedido
                    indice++;
                }

                // Creamos modelo esperado
                SelectProveedoresProductosViewModel modeloEsperado = new SelectProveedoresProductosViewModel();

                modeloEsperado.lineasPedido = lineasPedido;
                modeloEsperado.proveedores = proveedoresObtenidos;


                // Creamos objeto SelectedProductosViewModel para llamar al Controlador
                SelectedProductosViewModel selectedProducts = new SelectedProductosViewModel();
                int[] idsProductosSeleccionados = new int[2];
                indice = 0;
                foreach (Producto producto in productosSeleccionados)
                {
                    idsProductosSeleccionados[indice] = producto.idProducto;
                    indice++;
                }
                selectedProducts.IdsProductosSeleccionados = idsProductosSeleccionados;
                selectedProducts.listaCantidades = cantidades;

                // Creamos objeto SelectProveedoresProductosViewModel con la respuesta de la llamada del controlador
                //Act
                var result = await controller.SeleccionarProveedores(selectedProducts);
                //Assert
                var viewResult = Assert.IsType<ViewResult>(result);

                SelectProveedoresProductosViewModel modelo = (result as ViewResult).Model as SelectProveedoresProductosViewModel;

                Assert.Equal(modeloEsperado.proveedores.LongCount(), modelo.proveedores.LongCount());
                Assert.Equal(modeloEsperado.lineasPedido.LongCount(), modelo.lineasPedido.LongCount());

                Assert.Equal(modeloEsperado.proveedores, modelo.proveedores, Comparer.Get<Proveedor>((p1, p2) => p1.idProveedor == p2.idProveedor && p1.cif == p2.cif && p1.correoElectronico == p2.correoElectronico && p1.direccion == p2.direccion));
                Assert.Equal(modeloEsperado.lineasPedido, modelo.lineasPedido, Comparer.Get<LineaPedido>((p1, p2) => p1.producto.idProducto == p2.producto.idProducto && p1.cantidad == p2.cantidad && p1.precio == p2.precio));
            }
        }

        [Fact]
        public async Task SeleccionarProveedoresPOST_ModelIsNotValid()
        {
            //Arrange
            using (context) {
                //Para seleccionar los proveedores necesitamos los productos seleccionados, por eso inicializamos todos los valores.
                var controller = new PedidoesController(context);

                Producto producto1 = new Producto
                {
                    idProducto = 1,
                    nombre = "producto 1",
                    cantidad = 4,
                    precio = 5
                };

                Producto producto2 = new Producto
                {
                    idProducto = 2,
                    nombre = "producto 2",
                    cantidad = 2,
                    precio = 6
                };
                Proveedor proveedor1 = new Proveedor
                {
                    idProveedor = 1,
                    cif = "123",
                    correoElectronico = "123@",
                    direccion = "calle1",
                };
                Proveedor proveedor2 = new Proveedor
                {
                    idProveedor = 2,
                    cif = "321",
                    correoElectronico = "142@",
                    direccion = "calle2",
                };

                List<Producto> productosSeleccionados = new List<Producto>();
                productosSeleccionados.Add(producto1);
                productosSeleccionados.Add(producto2);

                int[] cantidades = new int[2];
                cantidades[0] = 4;
                cantidades[1] = 3;

                List<Proveedor> proveedoresObtenidos = new List<Proveedor>();
                proveedoresObtenidos.Add(proveedor1);

                List<LineaPedido> lineasPedido = new List<LineaPedido>();
                int indice = 0;
                foreach (Producto productoSeleccionado in productosSeleccionados)
                {
                    LineaPedido lineaPedido = new LineaPedido();
                    int cantidad = cantidades[indice];
                    lineaPedido.cantidad = cantidad;
                    lineaPedido.precio = productoSeleccionado.precio * cantidad;
                    lineaPedido.producto = productoSeleccionado;
                    lineasPedido.Add(lineaPedido); //vamos añadiendo cada linea a mi lista de lineasPedido
                    indice++;
                }           
                // Creamos objeto SelectedProductosViewModel para llamar al Controlador
                SelectedProductosViewModel selectedProducts = new SelectedProductosViewModel();
                int[] idsProductosSeleccionados = new int[2];
                indice = 0;
                foreach (Producto producto in productosSeleccionados)
                {
                    idsProductosSeleccionados[indice] = producto.idProducto;
                    indice++;
                }
                // Creamos objeto SelectProveedoresProductosViewModel con la respuesta de la llamada del controlador
                //Act
                SelectedProveedorProductosViewModel proveedoresSeleccionadosVacio = new SelectedProveedorProductosViewModel();
                proveedoresSeleccionadosVacio.listaCantidades = cantidades;
                proveedoresSeleccionadosVacio.listaIdsProductosSeleccionados = idsProductosSeleccionados;

                controller.ModelState.AddModelError("IdProveedor", "Required");
                var result = await controller.SeleccionarProveedores(proveedoresSeleccionadosVacio);
                //Assert
                var viewResult = Assert.IsType<ViewResult>(result);

                SelectProveedoresProductosViewModel modelo = (result as ViewResult).Model as SelectProveedoresProductosViewModel;

                Assert.Equal(proveedoresObtenidos.LongCount(), modelo.proveedores.LongCount());
                Assert.Equal(lineasPedido.LongCount(), modelo.lineasPedido.LongCount());

                Assert.Equal(proveedoresObtenidos, modelo.proveedores, Comparer.Get<Proveedor>((p1, p2) => p1.idProveedor == p2.idProveedor && p1.cif == p2.cif && p1.correoElectronico == p2.correoElectronico && p1.direccion == p2.direccion));
                Assert.Equal(lineasPedido, modelo.lineasPedido, Comparer.Get<LineaPedido>((p1, p2) => p1.producto.idProducto == p2.producto.idProducto && p1.cantidad == p2.cantidad && p1.precio == p2.precio));
            }
        }


        [Fact]
        public async Task SeleccionarProveedoresPOST_ModelIsValid()
        {
            //Arrange
            using (context)
            {
                //Para seleccionar los proveedores necesitamos los productos seleccionados, por eso inicializamos todos los valores.
                var controller = new PedidoesController(context);

                Producto producto1 = new Producto
                {
                    idProducto = 1,
                    nombre = "producto 1",
                    cantidad = 4,
                    precio = 5
                };

                Producto producto2 = new Producto
                {
                    idProducto = 2,
                    nombre = "producto 2",
                    cantidad = 2,
                    precio = 6
                };
                Proveedor proveedor1 = new Proveedor
                {
                    idProveedor = 1,
                    cif = "123",
                    correoElectronico = "123@",
                    direccion = "calle1",
                };
      
                List<Producto> productosSeleccionados = new List<Producto>();
                productosSeleccionados.Add(producto1);
                productosSeleccionados.Add(producto2);

                int[] cantidades = new int[2];
                cantidades[0] = 4;
                cantidades[1] = 3;

                List<Proveedor> proveedoresObtenidos = new List<Proveedor>();
                proveedoresObtenidos.Add(proveedor1);

                List<LineaPedido> lineasPedido = new List<LineaPedido>();
                int indice = 0;
                foreach (Producto productoSeleccionado in productosSeleccionados)
                {
                    LineaPedido lineaPedido = new LineaPedido();
                    int cantidad = cantidades[indice];
                    lineaPedido.cantidad = cantidad;
                    lineaPedido.precio = productoSeleccionado.precio * cantidad;
                    lineaPedido.producto = productoSeleccionado;
                    lineasPedido.Add(lineaPedido); //vamos añadiendo cada linea a mi lista de lineasPedido
                    indice++;
                }
                // Creamos objeto SelectedProductosViewModel para llamar al Controlador
                SelectedProductosViewModel selectedProducts = new SelectedProductosViewModel();
                int[] idsProductosSeleccionados = new int[2];
                indice = 0;
                foreach (Producto producto in productosSeleccionados)
                {
                    idsProductosSeleccionados[indice] = producto.idProducto;
                    indice++;
                }
              
                SelectedProveedorProductosViewModel proveedorSeleccionado = new SelectedProveedorProductosViewModel();
                proveedorSeleccionado.listaCantidades = cantidades;
                proveedorSeleccionado.listaIdsProductosSeleccionados = idsProductosSeleccionados;
                proveedorSeleccionado.idProveedor = proveedor1.idProveedor;

                var result = await controller.SeleccionarProveedores(proveedorSeleccionado);

                var viewResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal(viewResult.ActionName, "MostrarResumen");


            }
        }
    }
}
*/