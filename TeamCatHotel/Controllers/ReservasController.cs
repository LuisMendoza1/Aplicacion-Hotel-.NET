using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamCatHotel.Data;
using TeamCatHotel.Models;
using TeamCatHotel.Models.ResevarViewModels;
using TeamCatHotel.Enums;
using System.Collections;
using Microsoft.AspNetCore.Authorization;

namespace TeamCatHotel.Controllers
{
    [Authorize(Roles = "admin,receptionist")]
    public class ReservasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reservas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reserva.ToListAsync());
        }

        // GET: Reservas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva.SingleOrDefaultAsync(m => m.idReserva == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Reservas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idReserva,comentarios,estado,fechaFin,fechaInicio,fechaRealizacion,regimenComida")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(reserva);
        }

        // GET: Reservas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva.SingleOrDefaultAsync(m => m.idReserva == id);
            if (reserva == null)
            {
                return NotFound();
            }
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idReserva,comentarios,estado,fechaFin,fechaInicio,fechaRealizacion,regimenComida")] Reserva reserva)
        {
            if (id != reserva.idReserva)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.idReserva))
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
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva.SingleOrDefaultAsync(m => m.idReserva == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var reserva = await _context.Reserva.SingleOrDefaultAsync(m => m.idReserva == id);
            IEnumerable<ReservaHabitacion> reservasHabitacion = _context.ReservaHabitacion.Where(rh => rh.Reserva.idReserva == id).ToList();
            foreach (ReservaHabitacion reservasHabitacionSeleccionada in reservasHabitacion)
            {
                _context.ReservaHabitacion.Remove(reservasHabitacionSeleccionada);
            }

            _context.Reserva.Remove(reserva);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ReservaExists(int id)
        {
            return _context.Reserva.Any(e => e.idReserva == id);
        }

        /*
        // Post: vistaDatosReservaPostCliente - Método de acción del botón crear reserva en Details - Alejandro Moya
        // Cogemos los datos del cliente y lo vamos rescatando
        [HttpPost, ActionName("vistaDatosReserva")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> vistaDatosReservaPostCliente(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                return View(cliente);
            }

            return RedirectToAction("Index", "ClientesController", "");
        }

            */

        // Se encarga de crear la vista para introducir los datos de la reserva, en este caso, solo queremos crear la vista con los datos del cliente.
        public async Task<IActionResult> vistaDatosReserva(int id)
        {
            var cliente = await _context.Cliente.SingleOrDefaultAsync(m => m.idPersona == id);
            DatosReservaViewModel datosReservaSoloCliente = new DatosReservaViewModel { idPersona = cliente.idPersona, nombre = cliente.nombre, apellidos = cliente.apellidos, nif = cliente.nif, fechaInicio=DateTime.Now, fechaFin=DateTime.Now};
            return View(datosReservaSoloCliente);

        }

        // Post: vistaDatosReservaPost - Método de acción del botón crear reserva en Details - Alejandro Moya
        // Con los datos de la reserva, procedemos al llamar al metodo GET para crear la vista de seleccion de habitaciones y descuentos
        [HttpPost, ActionName("vistaDatosReserva")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> vistaDatosReservaPost([Bind("idPersona,nombre,apellidos,nif,fechaInicio,fechaFin,regimenComida,comentarios")] DatosReservaViewModel datosReserva)
        {
            if (ModelState.IsValid)
            {
                DatosHabitacionReservaViewModel datosReservasProceso = new DatosHabitacionReservaViewModel {
                idPersona = datosReserva.idPersona,
                nombre = datosReserva.nombre,
                apellidos = datosReserva.apellidos,
                nif = datosReserva.nif,
                fechaFin = datosReserva.fechaFin,
                fechaInicio = datosReserva.fechaInicio,
                comentarios = datosReserva.comentarios,
                regimenComida = datosReserva.regimenComida
            };

                if (datosReserva.fechaFin.Subtract(datosReserva.fechaInicio) <= TimeSpan.Zero) {
                    ModelState.AddModelError("", $"Oops, posiblemente ha confundido la Fecha de Inicio con la Fecha de Fin");
                    return View(datosReserva);
                }
                if (DateTime.Today.CompareTo(datosReserva.fechaInicio.Date) > 0) {
                    ModelState.AddModelError("", $"Oops, las fechas introducidas no pueden ser anteriores a la fecha actual");
                    return View(datosReserva);
                }

                // Habitaciones disponibles
                IEnumerable<int> reservasMalas = _context.Reserva.Where(rh => (datosReserva.fechaInicio.Subtract(rh.fechaInicio) >= TimeSpan.Zero && rh.fechaFin.Subtract(datosReserva.fechaInicio) >= TimeSpan.Zero) ||
                                                                              (rh.fechaInicio.Subtract(datosReserva.fechaInicio) >= TimeSpan.Zero && datosReserva.fechaFin.Subtract(rh.fechaInicio) > TimeSpan.Zero) ||
                                                                              (rh.fechaFin.Subtract(datosReserva.fechaInicio) > TimeSpan.Zero && datosReserva.fechaFin.Subtract(rh.fechaFin) >= TimeSpan.Zero)).AsEnumerable().Select(s => s.idReserva).ToList();

                IEnumerable<int> habmalas = _context.ReservaHabitacion.Where(h => reservasMalas.Contains(h.Reserva.idReserva)).Select(s => s.Habitacion.numero).ToList();
                IEnumerable<Habitacion> Habitaciones = _context.Habitacion.Where<Habitacion>(h => !habmalas.Contains(h.numero)).ToList();


                // Si no hay habitaciones, devolvemos la vista de DatosReserva para que cambie las fechas seleccionadas.
                if (!Habitaciones.Any())
                {
                    ModelState.AddModelError("", $"No hay habitaciones disponibles con las fechas dadas");
                    return View(datosReserva);
                }
                return RedirectToAction("vistaSeleccionHabitacionReserva", datosReservasProceso);
            }
            else
            {
                ModelState.AddModelError("", $"Por favor, escriba las fechas en el formato correcto aaaa-mm-dd");
                return View(datosReserva);
            }


        }

        // Se encarga de crear la vista para seleccionar las habitaciones y los descuentos disponibles.
        public async Task<IActionResult> vistaSeleccionHabitacionReserva([Bind("idPersona,nombre,apellidos,nif,fechaInicio,fechaFin,regimenComida,comentarios,descuentoSeleccionado,habitacionesSeleccionadas")] DatosHabitacionReservaViewModel datosReserva)
        {
            //Obtenemos los descuentos disponibles
            datosReserva.Descuentos = _context.Descuento.Where(m => (datosReserva.fechaInicio.Subtract(m.fechaInicio) >= TimeSpan.Zero) && m.fechaFin.Subtract(datosReserva.fechaInicio) >= TimeSpan.Zero).ToList();


            // Habitaciones disponibles
            IEnumerable<int> reservasMalas = _context.Reserva.Where(rh => (datosReserva.fechaInicio.Subtract(rh.fechaInicio) >= TimeSpan.Zero && rh.fechaFin.Subtract(datosReserva.fechaInicio) >= TimeSpan.Zero) ||
                                                                          (rh.fechaInicio.Subtract(datosReserva.fechaInicio) >= TimeSpan.Zero && datosReserva.fechaFin.Subtract(rh.fechaInicio) > TimeSpan.Zero) ||
                                                                          (rh.fechaFin.Subtract(datosReserva.fechaInicio) > TimeSpan.Zero && datosReserva.fechaFin.Subtract(rh.fechaFin) >= TimeSpan.Zero)).AsEnumerable().Select(s => s.idReserva).ToList();

            IEnumerable<int> habmalas = _context.ReservaHabitacion.Where(h => reservasMalas.Contains(h.Reserva.idReserva)).Select(s => s.Habitacion.numero).ToList();
            datosReserva.Habitaciones =  _context.Habitacion.Where<Habitacion>(h => !habmalas.Contains(h.numero)).ToList();

            foreach (Habitacion habitacionSeleccionada in datosReserva.Habitaciones)
            {
                habitacionSeleccionada.precio = habitacionSeleccionada.precio * (float)datosReserva.fechaFin.Subtract(datosReserva.fechaInicio).TotalDays;
            }
            
            
            return View(datosReserva);
        }

        // Post: vistaSeleccionHabitacionReservaPost - Alejandro Moya
        // Guardamos la reserva en la BBDD
        [HttpPost, ActionName("vistaSeleccionHabitacionReserva")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> vistaSeleccionHabitacionReservaPost(DatosHabitacionReservaViewModel datosReserva)
        {
            if (ModelState.IsValid)
            {
                Reserva nuevaReserva = new Reserva {
                    fechaInicio = datosReserva.fechaInicio,
                    fechaFin = datosReserva.fechaFin,
                    regimenComida = datosReserva.regimenComida,
                    comentarios = datosReserva.comentarios
                };

                //Obtenemos el objeto cliente de la BBDD y se lo asignamos al cliente.
                nuevaReserva.Cliente = await _context.Cliente.SingleOrDefaultAsync(m => m.idPersona == datosReserva.idPersona);

                //Asignamos el descuento seleccionado a la reserva, si no hay descuento, será null
                if (datosReserva.descuentoSeleccionado != null) {
                    nuevaReserva.Descuento = await _context.Descuento.SingleOrDefaultAsync(m => m.idDescuento == int.Parse(datosReserva.descuentoSeleccionado)); ;
                }
                


                // Indicamos su fecha de realización, asi como el estado de la reserva
                nuevaReserva.fechaRealizacion = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                nuevaReserva.estado = 0;

                
                // Almacenamos la reserva junto con las habitaciones reservadas
                foreach (string habitacionSeleccionada in datosReserva.habitacionesSeleccionadas)
                {
                    ReservaHabitacion lReservaHabitacion = new ReservaHabitacion();
                    lReservaHabitacion.Reserva = nuevaReserva;
                    lReservaHabitacion.Habitacion = _context.Habitacion.First<Habitacion>(m => m.numero.Equals(int.Parse(habitacionSeleccionada)));
                    _context.ReservaHabitacion.Add(lReservaHabitacion);
                }

                
                // Añadimos la reserva y confirmamos cambios
                _context.Reserva.Add(nuevaReserva);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Clientes");
            }

            else
            {
                ModelState.AddModelError("", $"No ha seleccionado ninguna habitacion");
                //Obtenemos los descuentos disponibles
                datosReserva.Descuentos = _context.Descuento.Where(m => (datosReserva.fechaInicio.Subtract(m.fechaInicio) >= TimeSpan.Zero) && m.fechaFin.Subtract(datosReserva.fechaInicio) >= TimeSpan.Zero).ToList();


                // Habitaciones disponibles
                IEnumerable<int> reservasMalas = _context.Reserva.Where(rh => (datosReserva.fechaInicio.Subtract(rh.fechaInicio) >= TimeSpan.Zero && rh.fechaFin.Subtract(datosReserva.fechaInicio) >= TimeSpan.Zero) ||
                                                                              (rh.fechaInicio.Subtract(datosReserva.fechaInicio) >= TimeSpan.Zero && datosReserva.fechaFin.Subtract(rh.fechaInicio) > TimeSpan.Zero) ||
                                                                              (rh.fechaFin.Subtract(datosReserva.fechaInicio) > TimeSpan.Zero && datosReserva.fechaFin.Subtract(rh.fechaFin) >= TimeSpan.Zero)).AsEnumerable().Select(s => s.idReserva).ToList();

                IEnumerable<int> habmalas = _context.ReservaHabitacion.Where<ReservaHabitacion>(h => reservasMalas.Contains(h.Reserva.idReserva)).Select(s => s.Habitacion.numero).ToList();
                datosReserva.Habitaciones = _context.Habitacion.Where<Habitacion>(h => !habmalas.Contains(h.numero)).ToList();

                foreach (Habitacion habitacionSeleccionada in datosReserva.Habitaciones)
                {
                    habitacionSeleccionada.precio = habitacionSeleccionada.precio * (float)datosReserva.fechaFin.Subtract(datosReserva.fechaInicio).TotalDays;
                }

                return View(datosReserva);
            }
        }

        /**
         * Autor: Hernan Indibil de la Cruz Calvo
         * El metodo ComidaIncluida indica si cierta habitación tiene ya pagada una comida que es cubierta por cierto regimen.
         */
        public bool ComidaIncluida(Reserva reserva, int regimen)
        {
            return regimen != Regimen.INVALIDO && reserva != null && reserva.regimenComida >= regimen;
        }
    }
}
