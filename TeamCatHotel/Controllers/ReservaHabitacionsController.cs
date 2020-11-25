using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamCatHotel.Data;
using TeamCatHotel.Models;
using TeamCatHotel.Services;

namespace TeamCatHotel.Controllers
{
    public class ReservaHabitacionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservaHabitacionsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: ReservaHabitacions
        public async Task<IActionResult> Index()
        {
            return View(await _context.ReservaHabitacion.ToListAsync());
        }

        // GET: ReservaHabitacions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservaHabitacion = await _context.ReservaHabitacion.SingleOrDefaultAsync(m => m.idReservaHabitacion == id);
            if (reservaHabitacion == null)
            {
                return NotFound();
            }

            return View(reservaHabitacion);
        }

        // GET: ReservaHabitacions/Create
        public IActionResult Create()
        {
            ViewBag.Reservas = new SelectList(_context.Reserva, "idReserva", "idReserva");
            ViewBag.Habitaciones = new SelectList(_context.Habitacion, "numero", "numero");
            return View();
        }

        // POST: ReservaHabitacions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int idR, int nh, ReservaHabitacion reservaHabitacion)
        {
            Habitacion habitacion = _context.Habitacion.FirstOrDefault(h => h.numero == nh);
            Reserva reserva = _context.Reserva.FirstOrDefault(r => r.idReserva == idR);
            reservaHabitacion.Habitacion = habitacion;
            reservaHabitacion.Reserva = reserva;
            if (ModelState.IsValid)
            {
                _context.Add(reservaHabitacion);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(reservaHabitacion);
        }

        // GET: ReservaHabitacions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservaHabitacion = await _context.ReservaHabitacion.SingleOrDefaultAsync(m => m.idReservaHabitacion == id);
            if (reservaHabitacion == null)
            {
                return NotFound();
            }
            return View(reservaHabitacion);
        }

        // POST: ReservaHabitacions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idReservaHabitacion")] ReservaHabitacion reservaHabitacion)
        {
            if (id != reservaHabitacion.idReservaHabitacion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservaHabitacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaHabitacionExists(reservaHabitacion.idReservaHabitacion))
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
            return View(reservaHabitacion);
        }

        // GET: ReservaHabitacions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservaHabitacion = await _context.ReservaHabitacion.SingleOrDefaultAsync(m => m.idReservaHabitacion == id);
            if (reservaHabitacion == null)
            {
                return NotFound();
            }

            return View(reservaHabitacion);
        }

        // POST: ReservaHabitacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservaHabitacion = await _context.ReservaHabitacion.SingleOrDefaultAsync(m => m.idReservaHabitacion == id);
            _context.ReservaHabitacion.Remove(reservaHabitacion);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ReservaHabitacionExists(int id)
        {
            return _context.ReservaHabitacion.Any(e => e.idReservaHabitacion == id);
        }

        /**
         * Autor: Hernan Indibil de la Cruz Calvo
         * El metodo GetReserva devuelve la Reserva asociada a un numero de habitacion en la fecha actual.
         */
        public async Task<Reserva> GetReserva(int nHabitacion)
        {
            var rh = await _context.ReservaHabitacion.SingleOrDefaultAsync(m => m.Habitacion.numero == nHabitacion && m.Reserva.fechaInicio <= SystemTime.Now() && m.Reserva.fechaFin >= SystemTime.Now());
            if (rh == null)
            {
                rh = new ReservaHabitacion();
            }
            return rh.Reserva;
        }
    }
}
