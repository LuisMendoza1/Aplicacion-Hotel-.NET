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
    public class PlatoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlatoesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Platoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Plato.ToListAsync());
        }

        // GET: Platoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plato = await _context.Plato.SingleOrDefaultAsync(m => m.idPlato == id);
            if (plato == null)
            {
                return NotFound();
            }

            return View(plato);
        }

        // GET: Platoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Platoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idPlato,nombre")] Plato plato)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plato);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(plato);
        }

        // GET: Platoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plato = await _context.Plato.SingleOrDefaultAsync(m => m.idPlato == id);
            if (plato == null)
            {
                return NotFound();
            }
            return View(plato);
        }

        // POST: Platoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idPlato,nombre")] Plato plato)
        {
            if (id != plato.idPlato)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlatoExists(plato.idPlato))
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
            return View(plato);
        }

        // GET: Platoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plato = await _context.Plato.SingleOrDefaultAsync(m => m.idPlato == id);
            if (plato == null)
            {
                return NotFound();
            }

            return View(plato);
        }

        // POST: Platoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plato = await _context.Plato.SingleOrDefaultAsync(m => m.idPlato == id);
            _context.Plato.Remove(plato);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PlatoExists(int id)
        {
            return _context.Plato.Any(e => e.idPlato == id);
        }
    }
}
