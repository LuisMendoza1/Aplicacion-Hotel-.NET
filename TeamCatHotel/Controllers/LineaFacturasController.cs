using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamCatHotel.Data;
using TeamCatHotel.Models;
using System.Collections.ObjectModel;
using System.Collections;
using TeamCatHotel.Services;

namespace TeamCatHotel.Controllers
{
    public class LineaFacturasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LineaFacturasController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: LineaFacturas
        public async Task<IActionResult> Index()
        {
            return View(await _context.LineaFactura.ToListAsync());
        }

        // GET: LineaFacturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineaFactura = await _context.LineaFactura.SingleOrDefaultAsync(m => m.idLineaFactura == id);
            if (lineaFactura == null)
            {
                return NotFound();
            }

            return View(lineaFactura);
        }

        // GET: LineaFacturas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LineaFacturas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idLineaFactura,precio")] LineaFactura lineaFactura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lineaFactura);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(lineaFactura);
        }

        // GET: LineaFacturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineaFactura = await _context.LineaFactura.SingleOrDefaultAsync(m => m.idLineaFactura == id);
            if (lineaFactura == null)
            {
                return NotFound();
            }
            return View(lineaFactura);
        }

        // POST: LineaFacturas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idLineaFactura,precio")] LineaFactura lineaFactura)
        {
            if (id != lineaFactura.idLineaFactura)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lineaFactura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LineaFacturaExists(lineaFactura.idLineaFactura))
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
            return View(lineaFactura);
        }

        // GET: LineaFacturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineaFactura = await _context.LineaFactura.SingleOrDefaultAsync(m => m.idLineaFactura == id);
            if (lineaFactura == null)
            {
                return NotFound();
            }

            return View(lineaFactura);
        }

        // POST: LineaFacturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lineaFactura = await _context.LineaFactura.SingleOrDefaultAsync(m => m.idLineaFactura == id);
            _context.LineaFactura.Remove(lineaFactura);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool LineaFacturaExists(int id)
        {
            return _context.LineaFactura.Any(e => e.idLineaFactura == id);
        }

        public void setLineaFactura(ReservaServicio resserv, float precio, DateTime fechaIni, DateTime fechaFin, int numHabitacion)
        {
            int idFact;
            LineaFactura lfact = new LineaFactura();
            FacturasController fc = new FacturasController(_context);
            idFact = fc.getNumeroFactura(numHabitacion, fechaIni, fechaFin);
            Factura factura = _context.Factura.SingleOrDefault(m => m.idFactura == idFact);
            
            lfact.precio = precio;
            lfact.ReservaServicio = resserv;
            lfact.Factura = factura;
            factura.LineasFactura.Add(lfact);
            _context.Factura.Update(factura);
            _context.Factura.SingleOrDefault(m => m.idFactura == idFact).LineasFactura.Add(lfact);
            _context.LineaFactura.Add(lfact);
        }

        public async Task<bool> CrearLineaMenu(Menu menu, int cantidad, Factura factura, Habitacion habitacion)
        {
            if (menu == null || cantidad <= 0 || factura == null || habitacion == null)
            {
                return false;
            }

            bool poison = false;
            ReservaServiciosController rsc = new ReservaServiciosController(_context);
            LineaFactura lfaux;
            ReservaServicio rsaux;
            DateTime hora = SystemTime.Now();
            List<LineaFactura> lfs = new List<LineaFactura>();
            List<ReservaServicio> rss = new List<ReservaServicio>();

            for (int i = 0; i < cantidad; i++)
            {
                lfaux = new LineaFactura { precio = menu.precio, Factura = factura };
                _context.LineaFactura.Add(lfaux);
                rsaux = rsc.CrearReservaMenu(menu, habitacion, lfaux, hora);
                if (rsaux == null)
                {
                    poison = true;
                    break;
                }
                lfs.Add(lfaux);
                rss.Add(rsaux);
            }
            
            if(!poison)
            {
                await _context.SaveChangesAsync();
                for (int i = 0; i < cantidad; i++)
                {
                    lfaux = lfs[i];
                    rsaux = rss[i];
                    lfaux.ReservaServicio = rsaux;
                    _context.LineaFactura.Update(lfaux);
                }
                await _context.SaveChangesAsync();
            }
            return !poison;
        }
    }
}
