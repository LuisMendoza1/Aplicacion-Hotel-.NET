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

namespace TeamCatHotel.Controllers
{
    public class ProductoProveedorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductoProveedorsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: ProductoProveedors
        public async Task<IActionResult> Index()
        {
            List<Producto> productosTotales = _context.Producto.ToList();
            List<Proveedor> proveedoresTotales = _context.Proveedor.ToList();
            return View(await _context.ProductoProveedor.ToListAsync());
        }

        // GET: ProductoProveedors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            List<Producto> productosTotales = _context.Producto.ToList();
            List<Proveedor> proveedoresTotales = _context.Proveedor.ToList();
            if (id == null)
            {
                return NotFound();
            }

            var productoProveedor = await _context.ProductoProveedor.SingleOrDefaultAsync(m => m.idProductoProveedor == id);
            if (productoProveedor == null)
            {
                return NotFound();
            }

            return View(productoProveedor);
        }

        // GET: ProductoProveedors/Create
        public IActionResult Create()
        {
            List<Producto> productosTotales =  _context.Producto.ToList();
            List<Proveedor> proveedoresTotales = _context.Proveedor.ToList();
            CreateProductoProveedor vista = new CreateProductoProveedor();
            vista.Productos = productosTotales;
            vista.proveedores = proveedoresTotales;
            return View(vista);
        }

        // POST: ProductoProveedors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idProductoProveedor,precio,idProducto,idProveedor")] CreateProductoProveedor createProductoProveedor)
        {
            if (ModelState.IsValid)
            {
                ProductoProveedor productoProveedor = new ProductoProveedor();
                productoProveedor.precio = createProductoProveedor.precio;

                Producto producto = await _context.Producto.Where<Producto>(prod => prod.idProducto == createProductoProveedor.idProducto).FirstOrDefaultAsync();
                Proveedor proveedor = await _context.Proveedor.Where<Proveedor>(prov => prov.idProveedor == createProductoProveedor.idProveedor).FirstOrDefaultAsync();
               
                productoProveedor.Producto = producto;
                productoProveedor.Proveedor = proveedor;
                _context.Add(productoProveedor);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(createProductoProveedor);
        }

        // GET: ProductoProveedors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productoProveedor = await _context.ProductoProveedor.SingleOrDefaultAsync(m => m.idProductoProveedor == id);
            if (productoProveedor == null)
            {
                return NotFound();
            }
            return View(productoProveedor);
        }

        // POST: ProductoProveedors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idProductoProveedor,precio")] ProductoProveedor productoProveedor)
        {
            if (id != productoProveedor.idProductoProveedor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productoProveedor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoProveedorExists(productoProveedor.idProductoProveedor))
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
            return View(productoProveedor);
        }

        // GET: ProductoProveedors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productoProveedor = await _context.ProductoProveedor.SingleOrDefaultAsync(m => m.idProductoProveedor == id);
            if (productoProveedor == null)
            {
                return NotFound();
            }

            return View(productoProveedor);
        }

        // POST: ProductoProveedors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productoProveedor = await _context.ProductoProveedor.SingleOrDefaultAsync(m => m.idProductoProveedor == id);
            _context.ProductoProveedor.Remove(productoProveedor);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProductoProveedorExists(int id)
        {
            return _context.ProductoProveedor.Any(e => e.idProductoProveedor == id);
        }

       
       
    }
}
