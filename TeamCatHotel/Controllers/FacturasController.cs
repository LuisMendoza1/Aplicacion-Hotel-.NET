using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamCatHotel.Data;
using TeamCatHotel.Models;

namespace TeamCatHotel.Controllers
{
    public class FacturasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FacturasController(ApplicationDbContext context)
        {
            List<Reserva> reservas = context.Reserva.ToList();
            List<Factura> facturas = context.Factura.ToList();
            _context = context;    
        }

        // GET: Facturas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Factura.ToListAsync());
        }

        // GET: Facturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Factura.SingleOrDefaultAsync(m => m.idFactura == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // GET: Facturas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Facturas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idFactura,abono,fechaEmision,fechaPago")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(factura);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(factura);
        }

        // GET: Facturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Factura.SingleOrDefaultAsync(m => m.idFactura == id);
            if (factura == null)
            {
                return NotFound();
            }
            return View(factura);
        }

        // POST: Facturas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idFactura,abono,fechaEmision,fechaPago")] Factura factura)
        {
            if (id != factura.idFactura)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(factura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaExists(factura.idFactura))
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
            return View(factura);
        }

        // GET: Facturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Factura.SingleOrDefaultAsync(m => m.idFactura == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var factura = await _context.Factura.SingleOrDefaultAsync(m => m.idFactura == id);
            _context.Factura.Remove(factura);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool FacturaExists(int id)
        {
            return _context.Factura.Any(e => e.idFactura == id);
        }

        public int getNumeroFactura(int numHabitacion, DateTime fechaIni, DateTime fechaFin)
        {
            var factura = _context.Factura.SingleOrDefault(m => m.Reserva.fechaInicio <= fechaIni && m.Reserva.fechaFin >= fechaFin
            && m.Reserva.ReservaHabitacion.SingleOrDefault(n => n.Reserva.idReserva == m.Reserva.idReserva).Habitacion.numero == numHabitacion);
            //&& m.Reserva.ReservaHabitacion.SingleOrDefault(n => n.Habitacion.numero == numHabitacion).Habitacion.numero == numHabitacion);
            //var factura = _context.Factura.SingleOrDefault(m => m.Reserva.fechaInicio == fechaIni && m.Reserva.fechaFin == fechaFin);
            return factura.idFactura;
        }

        /**
         * Autor: Hernan Indibil de la Cruz Calvo
         * El metodo GetFactura devuelve la factura asociada a una reserva.
         */
        public async Task<Factura> GetFactura(Reserva reserva)
        {
            if (reserva == null || reserva.Factura == null)
            {
                return null;
            }
            return await _context.Factura.SingleOrDefaultAsync<Factura>(f => f.idFactura == reserva.Factura.idFactura);
        }
    }
}
