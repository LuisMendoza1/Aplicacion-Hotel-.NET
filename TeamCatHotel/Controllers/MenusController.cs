using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamCatHotel.Data;
using TeamCatHotel.Models;
using TeamCatHotel.Enums;
using Microsoft.AspNetCore.Authorization;
using TeamCatHotel.Services;

namespace TeamCatHotel.Controllers
{
    [Authorize(Roles = "admin,waiter")]
    public class MenusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MenusController(ApplicationDbContext context)
        {
            List<Reserva> reservas = context.Reserva.ToList();
            List<Habitacion> habitaciones = context.Habitacion.ToList();
            List<Factura> facturas = context.Factura.ToList();
            List<Cliente> clientes = context.Cliente.ToList();
            _context = context;    
        }

        // GET: Menus
        public async Task<IActionResult> Index()
        {
            return View(await _context.Menu.ToListAsync());
        }

        // GET: Menus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menu.SingleOrDefaultAsync(m => m.idServicio == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // GET: Menus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Menus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idServicio,descripcion,nombre,precio,horaInicio,horaFin")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menu);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(menu);
        }

        // GET: Menus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menu.SingleOrDefaultAsync(m => m.idServicio == id);
            if (menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }

        // POST: Menus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idServicio,descripcion,nombre,precio,horaInicio,horaFin")] Menu menu)
        {
            if (id != menu.idServicio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuExists(menu.idServicio))
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
            return View(menu);
        }

        // GET: Menus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menu.SingleOrDefaultAsync(m => m.idServicio == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menu = await _context.Menu.SingleOrDefaultAsync(m => m.idServicio == id);
            _context.Menu.Remove(menu);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MenuExists(int id)
        {
            return _context.Menu.Any(e => e.idServicio == id);
        }

        /**
         * Autor: Hernan Indibil de la Cruz Calvo
         * El metodo GetRegimen devuelve el regimen asociado a un menu.
         */
        public int GetRegimen(Menu menu)
        {
            if (menu == null || menu.nombre == null)
            {
                return Regimen.INVALIDO;
            }
            DateTime hi = menu.horaInicio;
            DateTime hf = menu.horaFin;

            int horai = hi.TimeOfDay.Hours;
            int horaf = hf.TimeOfDay.Hours;

            if (horai >= horaf)
            {
                return Regimen.INVALIDO;
            }
            else if (horai >= 13 && horaf <= 16)
            {
                return Regimen.COMPLETA;
            }

            return Regimen.MEDIA;
        }

        /**
         * Autor: Hernan Indibil de la Cruz Calvo
         * El metodo ContratarMenu vuelve a cargar la vista si la comida ya esta pagada,
         * y carga la vista de seleccion de comensales si no lo esta. En caso de
         * no existir una reserva para dicha habitación, retornará igualmente a
         * la vista.
         */

        [HttpGet]
        public IActionResult ContratarMenu(ResultMenuViewModel remvm)
        {
            ViewBag.Menus = new SelectList(_context.Menu, "idServicio", "nombre", remvm.menu);
            ViewData["menuId"] = 0;
            return View(remvm);
        }

        [HttpPost]
        public async Task<IActionResult> ContratarMenu(int menuId, int nHabitacion, int menuIdOld, int nHabitacionOld)
        {
            Menu menu = _context.Menu.FirstOrDefault(m => m.idServicio == menuId);

            ResultMenuViewModel remvm = new ResultMenuViewModel
            {
                menu = menu
            };

            int regimen = GetRegimen(menu);
            if (regimen == Regimen.INVALIDO)
            {
                ViewBag.Menus = new SelectList(_context.Menu, "idServicio", "nombre", remvm.menu);
                remvm.result = "Error: Invalid menu";
                ViewData["menuId"] = 0;
                return View("ContratarMenu", remvm);
            }

            DateTime hora = SystemTime.Now();
            if (menu.horaFin.TimeOfDay.CompareTo(hora.TimeOfDay) < 0 || menu.horaInicio.TimeOfDay.CompareTo(hora.TimeOfDay) > 0)
            {
                ViewBag.Menus = new SelectList(_context.Menu, "idServicio", "nombre", remvm.menu);
                remvm.result = "Error: Out of time";
                ViewData["menuId"] = 0;
                return View("ContratarMenu", remvm);
            }

            ReservasController rc = new ReservasController(_context);
            ReservaHabitacionsController rhc = new ReservaHabitacionsController(_context);
            Reserva r = await rhc.GetReserva(nHabitacion);
            bool paid = rc.ComidaIncluida(r, regimen);

            if (r == null)
            {
                remvm.result = "Error: Reserva not found";
                ViewBag.Menus = new SelectList(_context.Menu, "idServicio", "nombre", remvm.menu);
                ViewData["menuId"] = 0;
                return View("ContratarMenu", remvm);
            }

            String nCliente = r.Cliente.nombre + " " + r.Cliente.apellidos;
            if (menuId != menuIdOld || nHabitacion != nHabitacionOld)
            {
                remvm.result = "Cliente: " + nCliente + '\n' + "Pulse de nuevo contratar para confirmar.";
                ViewData["menuId"] = menuId;
                ViewData["nHabitacion"] = nHabitacion;
                ViewData["nCliente"] = nCliente;
                ViewBag.Menus = new SelectList(_context.Menu, "idServicio", "nombre", remvm.menu);
                return View("ContratarMenu", remvm);
            }

            if (paid)
            {
                remvm.result = "Success";
                ViewBag.Menus = new SelectList(_context.Menu, "idServicio", "nombre", remvm.menu);
                ViewData["menuId"] = 0;
                return View("ContratarMenu", remvm);
            }

            ReservaMenuViewModel rmvm = new ReservaMenuViewModel();
            rmvm.nHabitacion = nHabitacion;
            rmvm.idServicio = menu.idServicio;
            rmvm.nCliente = nCliente;
            return RedirectToAction("SeleccionComensales", rmvm);
        }

        [HttpGet]
        public IActionResult SeleccionComensales(ReservaMenuViewModel rmvm)
        {
            return View(rmvm);
        }

        /**
         * Autor: Hernan Indibil de la Cruz Calvo
         * El metodo SeleccionComensales carga a la cuenta del cliente
         * el precio del menu seleccionado para los comensales
         * seleccionados y retorna a la vista de contratar menu.
         */
        [HttpPost]
        public async Task<IActionResult> SeleccionComensales(int nComensales, int nHabitacion, int idServicio, string nCliente)
        {
            ReservaMenuViewModel rmvm = new ReservaMenuViewModel { nHabitacion = nHabitacion, idServicio = idServicio, nCliente = nCliente };
            Menu menu = _context.Menu.FirstOrDefault(m => m.idServicio == idServicio);
            Habitacion habitacion = await _context.Habitacion.SingleOrDefaultAsync<Habitacion>(h => h.numero == nHabitacion);
            string result = "Failure";
            ResultMenuViewModel remvm = new ResultMenuViewModel
            {
                menu = menu,
                result = result
            };

            if (menu == null || habitacion == null)
            {
                return RedirectToAction("ContratarMenu", remvm);
            }
            if (nComensales <= 0)
            {
                return View("SeleccionComensales", rmvm);
            }

            FacturasController fc = new FacturasController(_context);
            ReservaHabitacionsController rhc = new ReservaHabitacionsController(_context);

            Reserva reserva = await rhc.GetReserva(nHabitacion);
            Factura factura = await fc.GetFactura(reserva);

            LineaFacturasController lfc = new LineaFacturasController(_context);

            result = "Error: couldn't get factura";
            if(factura != null)
            {
                bool chk = await lfc.CrearLineaMenu(menu, nComensales, factura, habitacion);
                if (!chk)
                {
                    result = "Error: out of time";
                }
                else
                {
                    result = "Success";
                }
            }

            remvm.result = result;
            return RedirectToAction("ContratarMenu", remvm);
        }
    }
}
