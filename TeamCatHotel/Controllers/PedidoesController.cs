using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamCatHotel.Data;
using TeamCatHotel.Models;
using TeamCatHotel.Models.PedidoViewModel;
using Microsoft.AspNetCore.Authorization;

namespace TeamCatHotel.Controllers
{
    [Authorize(Roles = "admin,warehouseMg")]
    public class PedidoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PedidoesController(ApplicationDbContext context)
        {
            _context = context;    
        }
        

        // GET: Pedidoes/Create
        // Prepara la vista para mostrar los proveedores
        public async Task<IActionResult> SelectProveedor()
        {
            ProveedoresViewModel selectProveedores = new ProveedoresViewModel();
            selectProveedores.proveedores = await _context.Proveedor.ToListAsync(); //le meto en mi objeto selectProveedores todo el listado de proveedores
            return View(selectProveedores);  // llamo a la vista para que me la devuelva con mi listado de proveedores  
        }


        // POST: Pedidoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SelectProveedor(ProveedoresViewModel proveedorSeleccionadoViewModel) //le paso el proveedor seleccionado de mi viewmodel
        {            
            // Comprobar si el proveedor seleccionado tiene productos

            if (ModelState.IsValid)
            {
                Proveedor proveedorSeleccionado = new Proveedor();
                proveedorSeleccionado.idProveedor = proveedorSeleccionadoViewModel.idProveedor; //dejamos en la variable proveedorSeleccionado el proveedor

                List<Producto> productosAsociados = obtenerProductosDeProveedor(proveedorSeleccionado); //llamamos al metodo para obtener los productos asociados al proveedor

                if (productosAsociados.LongCount() == 0)
                {
                    proveedorSeleccionadoViewModel.proveedores = await _context.Proveedor.ToListAsync(); //le meto en mi objeto selectProveedor los proveedores
                    ModelState.AddModelError("idProveedor", "Error. El proveedor seleccionado no tiene productos. Seleccione uno distinto.");
                    return View(proveedorSeleccionadoViewModel);

                }
                else
                {
                    return RedirectToAction("SelectProductos", proveedorSeleccionadoViewModel); //llamo a la vista de SelectProductos pasandole el ViewModel con el proveedor
                }
            }
            else
            {
                proveedorSeleccionadoViewModel.proveedores = await _context.Proveedor.ToListAsync(); //le meto en mi objeto selectProveedor los proveedores
                // me devuelve la vista con el proveedor seleccionado
                return View(proveedorSeleccionadoViewModel);
            }
           
        }

        //GET:
        // Para crear el objeto que tiene las lineas de pedido (productos y cantidad) y el proveedor para pasárselos a la vista SeleccionarProductos
        public async Task<IActionResult> SelectProductos(ProveedoresViewModel proveedorSeleccionadoViewModel)
        {
            List<Producto> productosTotales = await _context.Producto.ToListAsync(); //lista de todos los productos
            List<Producto> productosAsociados = new List<Producto>(); //lista de productos asociados a un proveedor
            Proveedor proveedorSeleccionado = new Proveedor();
            proveedorSeleccionado.idProveedor = proveedorSeleccionadoViewModel.idProveedor; //dejamos en la variable proveedorSeleccionado el proveedor
            
            productosAsociados = obtenerProductosDeProveedor(proveedorSeleccionado); //llamamos al metodo para obtener los productos asociados al proveedor
  
            ProveedorProductosViewModel selectProveedorProductos = new ProveedorProductosViewModel();
            selectProveedorProductos.productos = productosAsociados;
            var proveedorRecuperado= await _context.Proveedor.Where<Proveedor>(p => p.idProveedor == proveedorSeleccionado.idProveedor).FirstAsync();
            selectProveedorProductos.proveedor = proveedorRecuperado;
            selectProveedorProductos.idProveedor = proveedorRecuperado.idProveedor;
            //en mi objeto selectProveedorProductos dejo las lineasPedido (con los productos,cantidad y precio seleccionados) y el proveedor que los suministra
            if (proveedorSeleccionadoViewModel.noProductosSeleccionados)
            {
                ModelState.AddModelError("idProveedor", "Error. No ha seleccionado productos.");

            }
            if (proveedorSeleccionadoViewModel.noPositivo)
            {
                ModelState.AddModelError("idProveedor", "Error. La cantidad no puede ser menor que 0.");

            }
            if (proveedorSeleccionadoViewModel.mayorCien)
            {
                ModelState.AddModelError("idProveedor", "Error. La cantidad no puede ser mayor que 100.");
            }
            //me devuelve la vista con mi objeto selectProveedorProductos (producto, cantidad, precio seleccionados junto con el proveedor asociado)
            return View(selectProveedorProductos);

        }
   
        //POST:
        // Recoge los datos de la vista Seleccionar Productos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SelectProductos(ProveedorProductosViewModel proveedorProductosSeleccionadosViewModel)
        {
            if (ModelState.IsValid)
            {
                Boolean distintoCero = false;
                Boolean noPositivo = false;
                Boolean mayorCien = false;
                foreach (int cantidad in proveedorProductosSeleccionadosViewModel.listaCantidades)
                {
                    if (cantidad!=0)
                    {
                        distintoCero = true;
                    }
                    if (cantidad < 0) {
                        noPositivo = true;
                    }
                    if (cantidad > 100)
                    {
                        mayorCien = true;
                    }

                }
                if (distintoCero == false)
                {
                    ProveedoresViewModel proveedorSeleccionadoViewModel = new ProveedoresViewModel();

                    proveedorSeleccionadoViewModel.idProveedor = proveedorProductosSeleccionadosViewModel.idProveedor; //dejamos en la variable proveedorSeleccionado el proveedor
                    proveedorSeleccionadoViewModel.noProductosSeleccionados = true;
                    return RedirectToAction("SelectProductos", proveedorSeleccionadoViewModel);
                }

                else if (noPositivo == true)
                {
                    ProveedoresViewModel proveedorSeleccionadoViewModel = new ProveedoresViewModel();

                    proveedorSeleccionadoViewModel.idProveedor = proveedorProductosSeleccionadosViewModel.idProveedor; //dejamos en la variable proveedorSeleccionado el proveedor
                    proveedorSeleccionadoViewModel.noPositivo = true;
                    return RedirectToAction("SelectProductos", proveedorSeleccionadoViewModel);
                }
                else if(mayorCien == true)
                {
                    ProveedoresViewModel proveedorSeleccionadoViewModel = new ProveedoresViewModel();

                    proveedorSeleccionadoViewModel.idProveedor = proveedorProductosSeleccionadosViewModel.idProveedor; //dejamos en la variable proveedorSeleccionado el proveedor
                    proveedorSeleccionadoViewModel.mayorCien = true;
                    return RedirectToAction("SelectProductos", proveedorSeleccionadoViewModel);
                }
            
                else

                {
                    return RedirectToAction("ResumenPedido", proveedorProductosSeleccionadosViewModel);
                }
            }
            else
            {
                ProveedoresViewModel proveedorSeleccionadoViewModel = new ProveedoresViewModel();

                proveedorSeleccionadoViewModel.idProveedor = proveedorProductosSeleccionadosViewModel.idProveedor; //dejamos en la variable proveedorSeleccionado el proveedor
                return RedirectToAction("SelectProductos", proveedorSeleccionadoViewModel);
            }
        }
        // Prepara la vista para mostrar el resumen
     
        public async Task<IActionResult> ResumenPedido(ProveedorProductosViewModel proveedorProductosSeleccionados)
        {
            List<Producto> productos = new List<Producto>();
            foreach (int id in proveedorProductosSeleccionados.IdsProductosSeleccionados)
            {
                Producto producto = await _context.Producto.Where<Producto>(prod => prod.idProducto == id).FirstAsync();
                productos.Add(producto);
            }

            Proveedor proveedor = await _context.Proveedor.Where<Proveedor>(prod => prod.idProveedor == proveedorProductosSeleccionados.idProveedor).FirstAsync(); //obtengo el proveedor seleccionado


            List<LineaPedido> lineasPedido = new List<LineaPedido>();
            int idx = 0;
            foreach (Producto producto in productos)
            {
                int cantidad = (int)proveedorProductosSeleccionados.listaCantidades.GetValue(idx);
                if (cantidad != 0)
                {
                    LineaPedido lineaPedido = new LineaPedido();
                    lineaPedido.cantidad = cantidad; //le añado las cantidades de mi objeto viewmodel
                    lineaPedido.precio = (int)proveedorProductosSeleccionados.listaCantidades.GetValue(idx) * producto.precio; //le añado los precios de mi objeto viewmodel
                    lineaPedido.producto = producto;
                    // voy añadiendo en las lineasPedido con los datos del producto
                    lineasPedido.Add(lineaPedido); //para cada lineaPedido, creo lineasPedido, donde tengo listas de cada producto, listas de precio y listas de cantidad
                }
                    idx++;
            }
            //creo un pedido con las lineas de pedido y el proveedor
            Pedido pedido = new Pedido();
            pedido.LineasPedido = lineasPedido; //le añado en el pedido mi linea de pedido (producto, cantidad, precio)
            pedido.Proveedor = proveedor; //le añado en pedido el proveedor seleccionado
            double precioTotal = 0;
            
            foreach(LineaPedido linea in lineasPedido)
            {
                precioTotal = linea.precio + precioTotal;
            }

            ResumenViewModel resumenPedidoViewModel = new ResumenViewModel(); //el objeto resumenPedidoViewModel tendrá el pedido creado
            resumenPedidoViewModel.pedido = pedido;
            resumenPedidoViewModel.precioTotal = precioTotal;
            //para que llame a la vista resumen pedido pasando el objeto resumenPedioViewModel
            return View(resumenPedidoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResumenPedido(ConfirmResumenViewModel proveedorProductosViewModel)
        {
            if (ModelState.IsValid)
            {
                List<Producto> productos = new List<Producto>();
                foreach (int id in proveedorProductosViewModel.listaIdsProductosSeleccionados)
                {
                    Producto productoAux = _context.Producto.Where<Producto>(prod => prod.idProducto == id).ToList<Producto>().First();
                    productos.Add(productoAux);
                }
                Proveedor proveedor = new Proveedor();
                proveedor = await _context.Proveedor.Where<Proveedor>(prod => prod.idProveedor == proveedorProductosViewModel.idProveedor).FirstAsync(); //obtengo el proveedor seleccionado

                List<LineaPedido> lineasPedido = new List<LineaPedido>();
                int idx = 0;
                foreach (Producto producto in productos)
                {
                    LineaPedido lineaPedido = new LineaPedido();
                    lineaPedido.cantidad = (int)proveedorProductosViewModel.listaCantidades.GetValue(idx); //le añado las cantidades de mi objeto viewmodel
                    lineaPedido.precio = (int)proveedorProductosViewModel.listaCantidades.GetValue(idx) * producto.precio; //le añado los precios de mi objeto viewmodel
                    lineaPedido.producto = producto;
                    // voy añadiendo en las lineasPedido con los datos del producto
                    lineasPedido.Add(lineaPedido); //para cada lineaPedido, creo lineasPedido, donde tengo listas de cada producto, listas de precio y listas de cantidad
                    idx++;
                }
                //creo un pedido con las lineas de pedido y el proveedor
                Pedido pedido = new Pedido();
                pedido.fechaRecepcion = DateTime.Now;
                pedido.fechaEmision = DateTime.Now;

                pedido.LineasPedido = lineasPedido; //le añado en el pedido mi linea de pedido (producto, cantidad, precio)
                pedido.Proveedor = proveedor; //le añado en pedido el proveedor seleccionado


                // Guardamos en Base de datos
                foreach (LineaPedido lineaPedido in lineasPedido)
                {
                    _context.LineaPedido.Add(lineaPedido);
                }
                _context.Pedido.Add(pedido);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        private List<Producto> obtenerProductosDeProveedor(Proveedor proveedor)
        {
            List<Producto> productosTotales = _context.Producto.ToList(); //Para que no se muestren null en ProductoProveedor
            List<Proveedor> proveedoresTotales = _context.Proveedor.ToList(); //Para que no se muestren null en ProductoProveedor
            //Ahora pasamos a conseguir el listado de productos asociados al proveedor en la variable "relaciones"
            List<ProductoProveedor> relaciones = _context.ProductoProveedor.Where<ProductoProveedor>(pp => pp.Proveedor.idProveedor == proveedor.idProveedor).ToList<ProductoProveedor>();
            List<Producto> productos = new List<Producto>();
            foreach (ProductoProveedor relacion in relaciones)
            {
                if(relacion.Producto != null)
                {
                    Producto prod = relacion.Producto;
                    productos.Add(prod);
                }
            }
            // Eliminar los duplicados
            return productos.Distinct().ToList<Producto>();
        }

        /******************
         * AUTOGENERADOS
         * ****************/

        // GET: Pedidoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pedido.ToListAsync());
        }

        // GET: Pedidoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido.SingleOrDefaultAsync(m => m.idPedido == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }


        // GET: Pedidoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido.SingleOrDefaultAsync(m => m.idPedido == id);
            if (pedido == null)
            {
                return NotFound();
            }
            return View(pedido);
        }

        // POST: Pedidoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idPedido,fechaEmision,fechaRecepcion")] Pedido pedido)
        {
            if (id != pedido.idPedido)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.idPedido))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(pedido);
        }

        // GET: Pedidoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido.SingleOrDefaultAsync(m => m.idPedido == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _context.Pedido.SingleOrDefaultAsync(m => m.idPedido == id);
            _context.Pedido.Remove(pedido);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedido.Any(e => e.idPedido == id);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////

/*
        // GET: Pedidoes/Create
        // Prepara la vista para mostrar los productos
        public async Task<IActionResult> SeleccionarProductos()
        {
            SelectProductosViewModel selectProductos = new SelectProductosViewModel();
            selectProductos.Productos = await _context.Producto.ToListAsync(); //le meto en mi objeto selectProductos todo el listado de productos
            return View(selectProductos);  // llamo a la vista para que me la devuelva con mi listado de productos   
        }

        // POST: Pedidoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        // Obtenemos los productos seleccionados desde la vista
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SeleccionarProductos(SelectedProductosViewModel productosSeleccionadosViewModel) //le paso los productos de mi viewmodel
        {
            SelectProductosViewModel selectProductos = new SelectProductosViewModel();
            selectProductos.Productos = await _context.Producto.ToListAsync(); //le meto en mi objeto selectProductos todo el listado de productos

            if (ModelState.IsValid)
            {
                return RedirectToAction("SeleccionarProveedores", productosSeleccionadosViewModel); //llamo a SeleccionarProveedores pasandole selectProveedores
            }
            // me devuelve la vista con los productos totales
            return View(selectProductos);
        }

        //GET:
        // Para crear el objeto que tiene las lineas de pedido (productos y cantidad) y los proveedores para pasárselos a la vista SeleccionarProveedores
        public async Task<IActionResult> SeleccionarProveedores(SelectedProductosViewModel productosSeleccionadosViewModel)
        {
            List<Producto> productosTotales = await _context.Producto.ToListAsync(); //lista de todos los productos
            List<Producto> productosSeleccionados = new List<Producto>(); //lista de los productos seleccionados
            List<Proveedor> proveedores = new List<Proveedor>(); //lista de proveedores asociados a un producto

            // en mi linea pedido guardo la cantidad, el precio y el producto
            List<LineaPedido> lineasPedido = new List<LineaPedido>();

            foreach (int id in productosSeleccionadosViewModel.IdsProductosSeleccionados)
            {
                //vamos añadiendo a la lista de productosSeleccionados los productos que se han seleccionado
                Producto producto = await _context.Producto.Where<Producto>(prod => prod.idProducto == id).FirstAsync();
                productosSeleccionados.Add(producto);
            }
            //para cada producto seleccionado creamos una lineaPedido que contendrá la cantidad, el precio y el producto
            foreach (Producto productoSeleccionado in productosSeleccionados)
            {
                LineaPedido lineaPedido = new LineaPedido();
                int cantidad = productosSeleccionadosViewModel.listaCantidades[productosTotales.IndexOf(productoSeleccionado)];
                lineaPedido.cantidad = cantidad;
                lineaPedido.precio = productoSeleccionado.precio * cantidad;
                lineaPedido.producto = productoSeleccionado;
                lineasPedido.Add(lineaPedido); //vamos añadiendo cada linea a mi lista de lineasPedido
            }

            proveedores = obtenerProveedoresDeProductos(productosSeleccionados); //para obtener los proveedores ligados a el/los productos seleccionados

            SelectProveedoresProductosViewModel selectProveedores = new SelectProveedoresProductosViewModel();
            selectProveedores.lineasPedido = lineasPedido;
            selectProveedores.proveedores = proveedores;
            //en mi objeto selectProveedores (de SelectProveedoresProductosViewModel) dejo las lineasPedido (con los productos,cantida y precio seleccionados) y los proveedores (todos los que suministran el/los producto/s)

            //me devuelve la vista con mi objeto selectProveedores (producto, cantidad, precio seleccionados junto con mi lista de proveedores asociados)
            return View(selectProveedores);
        }

        //POST:
        // Recoge los datos de la vista proveedores (Los mismos que en la primera vista, más el proveedor seleccionado)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SeleccionarProveedores(SelectedProveedorProductosViewModel proveedorProductosViewModel)
        {
            if (ModelState.IsValid)
            {
                List<Producto> productos = new List<Producto>();
                foreach (int id in proveedorProductosViewModel.listaIdsProductosSeleccionados)
                {
                    productos = await _context.Producto.Where<Producto>(prod => prod.idProducto == id).ToListAsync<Producto>();
                }
                Proveedor proveedor = await _context.Proveedor.SingleAsync(m => m.idProveedor == proveedorProductosViewModel.idProveedor); //obtengo el proveedor seleccionado

                List<LineaPedido> lineasPedido = new List<LineaPedido>();
                int idx = 0;
                foreach (Producto producto in productos)
                {
                    LineaPedido lineaPedido = new LineaPedido();
                    lineaPedido.cantidad = (int)proveedorProductosViewModel.listaCantidades.GetValue(idx); //le añado las cantidades de mi objeto viewmodel
                    lineaPedido.precio = (int)proveedorProductosViewModel.listaCantidades.GetValue(idx) * producto.precio; //le añado los precios de mi objeto viewmodel
                    lineaPedido.producto = producto;
                    // voy añadiendo en las lineasPedido con los datos del producto
                    lineasPedido.Add(lineaPedido); //para cada lineaPedido, creo lineasPedido, donde tengo listas de cada producto, listas de precio y listas de cantidad
                }
                //creo un pedido con las lineas de pedido y el proveedor
                Pedido pedido = new Pedido();
                pedido.LineasPedido = lineasPedido; //le añado en el pedido mi linea de pedido (producto, cantidad, precio)
                pedido.Proveedor = proveedor; //le añado en pedido el proveedor seleccionado

                ResumenPedidoViewModel resumenPedidoViewModel = new ResumenPedidoViewModel(); //el objeto resumenPedidoViewModel tendrá el pedido creado
                resumenPedidoViewModel.pedido = pedido;
                //para que llame a la vista resumen pedido pasando el objeto resumenPedioViewModel
                return RedirectToAction("MostrarResumen", resumenPedidoViewModel);
            }
            else
            {
                List<Producto> productosTotales = await _context.Producto.ToListAsync(); //lista de todos los productos
                List<Producto> productosSeleccionados = new List<Producto>(); //lista de los productos seleccionados
                List<Proveedor> proveedores = new List<Proveedor>(); //lista de proveedores asociados a un producto

                // en mi linea pedido guardo la cantidad, el precio y el producto
                List<LineaPedido> lineasPedido = new List<LineaPedido>();

                foreach (int id in proveedorProductosViewModel.listaIdsProductosSeleccionados)
                {
                    //vamos añadiendo a la lista de productosSeleccionados los productos que se han seleccionado
                    Producto producto = await _context.Producto.Where<Producto>(prod => prod.idProducto == id).FirstAsync();
                    productosSeleccionados.Add(producto);
                }
                //para cada producto seleccionado creamos una lineaPedido que contendrá la cantidad, el precio y el producto
                foreach (Producto productoSeleccionado in productosSeleccionados)
                {
                    LineaPedido lineaPedido = new LineaPedido();
                    int cantidad = proveedorProductosViewModel.listaCantidades[productosTotales.IndexOf(productoSeleccionado)];
                    lineaPedido.cantidad = cantidad;
                    lineaPedido.precio = productoSeleccionado.precio * cantidad;
                    lineaPedido.producto = productoSeleccionado;
                    lineasPedido.Add(lineaPedido); //vamos añadiendo cada linea a mi lista de lineasPedido
                }
                proveedores = obtenerProveedoresDeProductos(productosSeleccionados); //para obtener los proveedores ligados a el/los productos seleccionados

                SelectProveedoresProductosViewModel selectProveedores = new SelectProveedoresProductosViewModel();
                selectProveedores.lineasPedido = lineasPedido;
                selectProveedores.proveedores = proveedores;

                return View(selectProveedores);
            }
        }
        // Prepara la vista para mostrar el resumen
        public IActionResult MostrarResumen(ResumenPedidoViewModel resumenPedidoViewModel)
        {
            return View(resumenPedidoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MostrarResumen(SelectedProveedorProductosViewModel proveedorProductosViewModel)
        {
            if (ModelState.IsValid)
            {
                List<Producto> productos = new List<Producto>();
                foreach (int id in proveedorProductosViewModel.listaIdsProductosSeleccionados)
                {
                    Producto productoAux = _context.Producto.Where<Producto>(prod => prod.idProducto == id).ToList<Producto>().First();
                    productos.Add(productoAux);
                }
                Proveedor proveedor = await _context.Proveedor.SingleAsync(m => m.idProveedor == proveedorProductosViewModel.idProveedor); //obtengo el proveedor seleccionado

                List<LineaPedido> lineasPedido = new List<LineaPedido>();
                int idx = 0;
                foreach (Producto producto in productos)
                {
                    LineaPedido lineaPedido = new LineaPedido();
                    lineaPedido.cantidad = (int)proveedorProductosViewModel.listaCantidades.GetValue(idx); //le añado las cantidades de mi objeto viewmodel
                    lineaPedido.precio = (int)proveedorProductosViewModel.listaCantidades.GetValue(idx) * producto.precio; //le añado los precios de mi objeto viewmodel
                    lineaPedido.producto = producto;
                    // voy añadiendo en las lineasPedido con los datos del producto
                    lineasPedido.Add(lineaPedido); //para cada lineaPedido, creo lineasPedido, donde tengo listas de cada producto, listas de precio y listas de cantidad
                    idx++;
                }
                //creo un pedido con las lineas de pedido y el proveedor
                Pedido pedido = new Pedido();
                pedido.LineasPedido = lineasPedido; //le añado en el pedido mi linea de pedido (producto, cantidad, precio)
                pedido.Proveedor = proveedor; //le añado en pedido el proveedor seleccionado

                ResumenPedidoViewModel resumenPedidoViewModel = new ResumenPedidoViewModel(); //el objeto resumenPedidoViewModel tendrá el pedido creado
                resumenPedidoViewModel.pedido = pedido;

                // Guardamos en Base de datos
                foreach (LineaPedido lineaPedido in lineasPedido)
                {
                    _context.LineaPedido.Add(lineaPedido);
                }
                _context.Pedido.Add(pedido);
                _context.SaveChanges();

                return RedirectToAction("ConfirmaGuardado");
            }
            else
            {
                return RedirectToAction("ErrorGuardado");
            }
        }

        // Pagina estática para mostrar confirmación de guardado
        public IActionResult ConfirmaGuardado()
        {
            return View();
        }

        // Pagina estática para mostrar que no se ha podido guardar
        public IActionResult ErrorGuardado()
        {
            return View();
        }

        //GET:
        //Devuelve una lista de proveedores dado un producto asociado 
        private List<Proveedor> obtenerProveedoresDeProducto(Producto producto)
        {
            List<ProductoProveedor> relaciones = _context.ProductoProveedor.Where<ProductoProveedor>(pp => pp.Producto.idProducto == producto.idProducto).ToList<ProductoProveedor>();
            IEnumerable<Proveedor> proveedores = new List<Proveedor>();
            foreach (ProductoProveedor relacion in relaciones)
            {
                List<Proveedor> proveedoresAux = _context.Proveedor.Where<Proveedor>(p => p.idProveedor == relacion.Proveedor.idProveedor).ToList<Proveedor>();
                proveedores = proveedores.Concat(proveedoresAux);
            }
            return proveedores.ToList<Proveedor>();

        }

        //GET:
        //Devuelve una lista de proveedores dada una lista de productos asociados
        private List<Proveedor> obtenerProveedoresDeProductos(List<Producto> productos)
        {
            List<Proveedor> proveedores = new List<Proveedor>();
            List<Proveedor> provaux = new List<Proveedor>();
            foreach (Producto producto in productos) //para cada producto
            {
                provaux = obtenerProveedoresDeProducto(producto); //llamo al metodo de recuperar proveedores por un producto
                if (proveedores.Count == 0) // si proveedores esta vacio
                {
                    proveedores = provaux; //proveedores pasa a ser la lista de provaux obtenida
                }
                else
                {
                    foreach (Proveedor proveedor in proveedores)
                    {
                        if (!provaux.Contains(proveedor)) //si provaux no contiene a proveedor
                        {
                            proveedores.Remove(proveedor); //lo elimino de proveedores
                        }
                    }
                }
            }

            return proveedores;
        }*/
    }
    
}
