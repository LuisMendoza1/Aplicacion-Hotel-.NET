using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamCatHotel.Data;
using TeamCatHotel.Models;
using Microsoft.AspNetCore.Authorization;

namespace TeamCatHotel.Controllers
{
    [Authorize(Roles = "admin,receptionist")]
    public class HabitacionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HabitacionsController(ApplicationDbContext context)
        {
            _context = context;    
        }
        
        // GET: Habitacions
        [HttpGet, ActionName("Index")]
        public async Task<IActionResult> Index(bool? ocupada)
        {
            if (ocupada != null)
            {
                if (ocupada == false)
                {
                    ViewData["ocupada"] = "La habitacion seleccionada debe estar ocupada";
                }
            }
            return View(await _context.Habitacion.ToListAsync());
        }

        // GET: Habitacions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habitacion = await _context.Habitacion.SingleOrDefaultAsync(m => m.numero == id);
            if (habitacion == null)
            {
                return NotFound();
            }

            return View(habitacion);
        }

        // GET: Habitacions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Habitacions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("numero,aforo,descripcion,localizacion,ocupada,precio")] Habitacion habitacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(habitacion);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(habitacion);
        }

        // GET: Habitacions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habitacion = await _context.Habitacion.SingleOrDefaultAsync(m => m.numero == id);
            if (habitacion == null)
            {
                return NotFound();
            }
            return View(habitacion);
        }

        // POST: Habitacions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("numero,aforo,descripcion,localizacion,ocupada,precio")] Habitacion habitacion)
        {
            if (id != habitacion.numero)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(habitacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HabitacionExists(habitacion.numero))
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
            return View(habitacion);
        }

        // GET: Habitacions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habitacion = await _context.Habitacion.SingleOrDefaultAsync(m => m.numero == id);
            if (habitacion == null)
            {
                return NotFound();
            }

            return View(habitacion);
        }

        // POST: Habitacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var habitacion = await _context.Habitacion.SingleOrDefaultAsync(m => m.numero == id);
            _context.Habitacion.Remove(habitacion);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool HabitacionExists(int id)
        {
            return _context.Habitacion.Any(e => e.numero == id);
        }

        
    }
}
