using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamCatHotel.Data;
using TeamCatHotel.Models;
using TeamCatHotel.Models.SolicitaServicioViewModels;
using System.Collections;
using Microsoft.AspNetCore.Authorization;

namespace TeamCatHotel.Controllers
{
    [Authorize(Roles = "admin,receptionist")]
    public class ServiciosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServiciosController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Servicios
        [HttpGet, ActionName("Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Servicio.ToListAsync());
        }

        // GET: Servicios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicio.SingleOrDefaultAsync(m => m.idServicio == id);
            if (servicio == null)
            {
                return NotFound();
            }

            return View(servicio);
        }

        // GET: Servicios/Create
        public IActionResult Create()
        {
            return View();
        }



        // POST: Servicios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idServicio,descripcion,nombre,precio")] Servicio servicio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(servicio);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(servicio);
        }

        // GET: Servicios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicio.SingleOrDefaultAsync(m => m.idServicio == id);
            if (servicio == null)
            {
                return NotFound();
            }
            return View(servicio);
        }

        // POST: Servicios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idServicio,descripcion,nombre,precio")] Servicio servicio)
        {
            if (id != servicio.idServicio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servicio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicioExists(servicio.idServicio))
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
            return View(servicio);
        }

        // GET: Servicios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicio.SingleOrDefaultAsync(m => m.idServicio == id);
            if (servicio == null)
            {
                return NotFound();
            }

            return View(servicio);
        }

        // POST: Servicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var servicio = await _context.Servicio.SingleOrDefaultAsync(m => m.idServicio == id);
            _context.Servicio.Remove(servicio);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ServicioExists(int id)
        {
            return _context.Servicio.Any(e => e.idServicio == id);
        }
        // GET: Obtiene una lista de servicios aplicables a una habitacion
        [HttpGet,ActionName("ListServices")]
        public async Task<IActionResult> ListServices(int nHab)
        {
            Habitacion hab = await _context.Habitacion.SingleOrDefaultAsync<Habitacion>(m => m.numero == nHab);
            ICollection<Servicio> listServices = await _context.Servicio.ToListAsync(); 
            ListSolicitarServicioViewModel ls = new ListSolicitarServicioViewModel { numeroHab = nHab, ListaServicios = listServices, ocupada = hab.ocupada };
            if (hab.ocupada)
            {
                //return RedirectToAction("Index",ls);
                //return View(await _context.Servicio.ToListAsync()); // en lugar de index
               // ViewBag.ls = await _context.Servicio.ToListAsync();
                return View(ls);
            }
            else
            {
                ModelState.AddModelError("", $"La habitacion {hab.numero} no esta ocupada");
                return RedirectToAction("Index", "Habitacions", new { ocupada = false});
            }
        }

        // POST: Envia la informacion del servicio que se desea reservar y llama a otro metodo para introducir los datos de la reserva
        [HttpPost, ActionName("SelectService")]
        public async Task<IActionResult> SelectService([Bind("numeroHab, idServicio")] SelectedServiceViewModel selectedservvm)
        {
            
            if (ModelState.IsValid)
            {
                int idServ = selectedservvm.idServicio;
                int nhab = selectedservvm.numeroHab;
                Servicio serv = _context.Servicio.SingleOrDefault(m => m.idServicio == idServ);
                SelectedServiceViewModel selectedservViewModel = new SelectedServiceViewModel { nombre = serv.nombre, precio = serv.precio,  idServicio = idServ, numeroHab = nhab, fechaFin = System.DateTime.Now, fechaInicio = System.DateTime.Now};
                ICollection<Servicio> listServices = await _context.Servicio.ToListAsync();
                ListSolicitarServicioViewModel ls = new ListSolicitarServicioViewModel { numeroHab = nhab, ListaServicios = listServices };
            
                return RedirectToAction("SetReservaServicio", selectedservViewModel);
            }
            else
            {
                return RedirectToAction("Index", "Habitacions");
            }
        }

        // GET: Genera una vista para introducir los datos necesarios para contratar el servicio
        [HttpGet]
        public async Task<IActionResult> SetReservaServicio([Bind("precio, nombre, idServicio, numeroHab")] SelectedServiceViewModel selectedservViewModel)
        {
            if (ModelState.IsValid)
            {
                return View(selectedservViewModel);
            }
            else
            {
                return RedirectToAction("ListServices", new { nHab = selectedservViewModel.numeroHab });
            }
        }

        // POST: Confirma la reserva del servicio
        [HttpPost, ActionName("ConfirmResService")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ConfirmResService([Bind("numeroHab, idServicio, fechaInicio, fechaFin")] SelectedServiceViewModel selectedservViewModel)
        {
            //SelectedServiceViewModel selectedservViewModel = new SelectedServiceViewModel { idServicio = idServ, numeroHab = nHab, fechaInicio = fechaIni, fechaFin = fechaFin };
            if (ModelState.IsValid)
            {
                _context.ReservaServicio.Add(new ReservaServicio
                {
                    Servicio = _context.Servicio.SingleOrDefault(m => m.idServicio == selectedservViewModel.idServicio),
                    Habitacion = _context.Habitacion.SingleOrDefault(m => m.numero == selectedservViewModel.numeroHab),
                    fechaInicio = selectedservViewModel.fechaInicio,
                    fechaFin = selectedservViewModel.fechaFin
                });
                _context.SaveChanges();
                return RedirectToAction("ListServices", new { nHab = selectedservViewModel.numeroHab });
            }
            else
            {
                return View(selectedservViewModel);
            }
        }

    }
}
