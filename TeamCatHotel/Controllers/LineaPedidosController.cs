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
    public class LineaPedidosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LineaPedidosController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: LineaPedidos
        public async Task<IActionResult> Index()
        {
            return View(await _context.LineaPedido.ToListAsync());
        }

        // GET: LineaPedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineaPedido = await _context.LineaPedido.SingleOrDefaultAsync(m => m.idLineaPedido == id);
            if (lineaPedido == null)
            {
                return NotFound();
            }

            return View(lineaPedido);
        }

        // GET: LineaPedidos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LineaPedidos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idLineaPedido,cantidad,precio")] CreateLineaPedido createLineaPedido)
        {
            if (ModelState.IsValid)
            {
                LineaPedido lineaPedido = new LineaPedido();
                lineaPedido.cantidad = createLineaPedido.cantidad;
                lineaPedido.precio = createLineaPedido.precio;
                Producto producto = await _context.Producto.Where<Producto>(prod => prod.idProducto == createLineaPedido.idProducto).FirstOrDefaultAsync();
                Pedido pedido = await _context.Pedido.Where<Pedido>(ped => ped.idPedido == createLineaPedido.idPedido).FirstOrDefaultAsync();
                lineaPedido.producto = producto;
                lineaPedido.pedido = pedido;
                _context.Add(lineaPedido);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(createLineaPedido);
        }

        // GET: LineaPedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineaPedido = await _context.LineaPedido.SingleOrDefaultAsync(m => m.idLineaPedido == id);
            if (lineaPedido == null)
            {
                return NotFound();
            }
            return View(lineaPedido);
        }

        // POST: LineaPedidos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idLineaPedido,cantidad,precio")] LineaPedido lineaPedido)
        {
            if (id != lineaPedido.idLineaPedido)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lineaPedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LineaPedidoExists(lineaPedido.idLineaPedido))
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
            return View(lineaPedido);
        }

        // GET: LineaPedidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineaPedido = await _context.LineaPedido.SingleOrDefaultAsync(m => m.idLineaPedido == id);
            if (lineaPedido == null)
            {
                return NotFound();
            }

            return View(lineaPedido);
        }

        // POST: LineaPedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lineaPedido = await _context.LineaPedido.SingleOrDefaultAsync(m => m.idLineaPedido == id);
            _context.LineaPedido.Remove(lineaPedido);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool LineaPedidoExists(int id)
        {
            return _context.LineaPedido.Any(e => e.idLineaPedido == id);
        }
    }
}
