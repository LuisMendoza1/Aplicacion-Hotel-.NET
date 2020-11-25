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
    public class ReservaServiciosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservaServiciosController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: ReservaServicios
        public async Task<IActionResult> Index()
        {
            return View(await _context.ReservaServicio.ToListAsync());
        }

        // GET: ReservaServicios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservaServicio = await _context.ReservaServicio.SingleOrDefaultAsync(m => m.idReservaServicio == id);
            if (reservaServicio == null)
            {
                return NotFound();
            }

            return View(reservaServicio);
        }

        // GET: ReservaServicios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReservaServicios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idReservaServicio,fechaFin,fechaInicio")] ReservaServicio reservaServicio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservaServicio);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(reservaServicio);
        }

        // GET: ReservaServicios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservaServicio = await _context.ReservaServicio.SingleOrDefaultAsync(m => m.idReservaServicio == id);
            if (reservaServicio == null)
            {
                return NotFound();
            }
            return View(reservaServicio);
        }

        // POST: ReservaServicios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idReservaServicio,fechaFin,fechaInicio")] ReservaServicio reservaServicio)
        {
            if (id != reservaServicio.idReservaServicio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservaServicio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaServicioExists(reservaServicio.idReservaServicio))
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
            return View(reservaServicio);
        }

        // GET: ReservaServicios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservaServicio = await _context.ReservaServicio.SingleOrDefaultAsync(m => m.idReservaServicio == id);
            if (reservaServicio == null)
            {
                return NotFound();
            }

            return View(reservaServicio);
        }

        // POST: ReservaServicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservaServicio = await _context.ReservaServicio.SingleOrDefaultAsync(m => m.idReservaServicio == id);
            _context.ReservaServicio.Remove(reservaServicio);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ReservaServicioExists(int id)
        {
            return _context.ReservaServicio.Any(e => e.idReservaServicio == id);
        }

        public void setServReserva(int servId, float precio, DateTime fechaIni, DateTime fechaFin, int numHabitacion)
        {
            ReservaServicio resserv = new ReservaServicio();
            var serv = _context.Servicio.SingleOrDefault(m => m.idServicio == servId);
            resserv.Servicio = serv;
            resserv.fechaInicio = fechaIni;
            resserv.fechaFin = fechaFin;

            LineaFacturasController lf = new LineaFacturasController(_context);
            lf.setLineaFactura(resserv,precio,fechaIni,fechaFin,numHabitacion);

            _context.ReservaServicio.Add(resserv);
        }

        public ReservaServicio CrearReservaMenu(Menu menu, Habitacion habitacion, LineaFactura linea, DateTime hora)
        {
            if (menu == null || linea == null || habitacion == null)
            {
                return null;
            }
            ReservaServicio rs = new ReservaServicio();
            rs.Servicio = menu;
            rs.Habitacion = habitacion;
            rs.LineaFactura = linea;

            rs.fechaInicio = new DateTime(hora.Year, hora.Month, hora.Day, menu.horaInicio.Hour, menu.horaInicio.Minute, menu.horaInicio.Second);
            rs.fechaFin = new DateTime(hora.Year, hora.Month, hora.Day, menu.horaFin.Hour, menu.horaFin.Minute, menu.horaFin.Second);

            if (menu.horaFin.TimeOfDay.CompareTo(hora.TimeOfDay) < 0 || menu.horaInicio.TimeOfDay.CompareTo(hora.TimeOfDay) > 0)
            {
                return null;
            }

            return rs;
        }
    }
}
