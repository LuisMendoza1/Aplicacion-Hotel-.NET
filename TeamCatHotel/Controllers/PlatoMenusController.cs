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
    public class PlatoMenusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlatoMenusController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: PlatoMenus
        public async Task<IActionResult> Index()
        {
            return View(await _context.PlatoMenu.ToListAsync());
        }

        // GET: PlatoMenus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platoMenu = await _context.PlatoMenu.SingleOrDefaultAsync(m => m.idPlatoMenu == id);
            if (platoMenu == null)
            {
                return NotFound();
            }

            return View(platoMenu);
        }

        // GET: PlatoMenus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PlatoMenus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idPlatoMenu")] PlatoMenu platoMenu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(platoMenu);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(platoMenu);
        }

        // GET: PlatoMenus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platoMenu = await _context.PlatoMenu.SingleOrDefaultAsync(m => m.idPlatoMenu == id);
            if (platoMenu == null)
            {
                return NotFound();
            }
            return View(platoMenu);
        }

        // POST: PlatoMenus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idPlatoMenu")] PlatoMenu platoMenu)
        {
            if (id != platoMenu.idPlatoMenu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(platoMenu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlatoMenuExists(platoMenu.idPlatoMenu))
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
            return View(platoMenu);
        }

        // GET: PlatoMenus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platoMenu = await _context.PlatoMenu.SingleOrDefaultAsync(m => m.idPlatoMenu == id);
            if (platoMenu == null)
            {
                return NotFound();
            }

            return View(platoMenu);
        }

        // POST: PlatoMenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var platoMenu = await _context.PlatoMenu.SingleOrDefaultAsync(m => m.idPlatoMenu == id);
            _context.PlatoMenu.Remove(platoMenu);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PlatoMenuExists(int id)
        {
            return _context.PlatoMenu.Any(e => e.idPlatoMenu == id);
        }
    }
}
