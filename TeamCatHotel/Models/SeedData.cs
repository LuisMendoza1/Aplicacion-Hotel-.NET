using TeamCatHotel.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using TeamCatHotel.Models;
using TeamCatHotel.Enums;

namespace DevelopmentProject.Models
{
    public static class SeedData
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            using (var _context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any movies.
                UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(_context);

                Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager =
                    serviceProvider.GetService<Microsoft.AspNetCore.Identity.UserManager<ApplicationUser>>();

                Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> roleManager =
                    serviceProvider.GetService<Microsoft.AspNetCore.Identity.RoleManager<IdentityRole>>();


                ApplicationUser userToInsert;
                string adminRole = "admin";
                string waiterRole = "waiter";
                string receptionistRole = "receptionist";
                string warehouseMgRole = "warehouseMg";
                Factura factura;
                Reserva reserva;
                Habitacion habitacion;
                ReservaHabitacion rh;

                if (!_context.Roles.Any<IdentityRole>(r => r.Name == adminRole))
                {
                    var result = await roleManager.CreateAsync(new IdentityRole(adminRole));
                }

                if (!_context.Roles.Any<IdentityRole>(r => r.Name == waiterRole))
                {
                    var result = await roleManager.CreateAsync(new IdentityRole(waiterRole));
                }

                if (!_context.Roles.Any<IdentityRole>(r => r.Name == receptionistRole))
                {
                    var result = await roleManager.CreateAsync(new IdentityRole(receptionistRole));
                }

                if (!_context.Roles.Any<IdentityRole>(r => r.Name == warehouseMgRole))
                {
                    var result = await roleManager.CreateAsync(new IdentityRole(warehouseMgRole));
                }

                // _context.SaveChanges();

                if (!_context.Users.Any(u => u.UserName == "elena@uclm.com"))
                {
                    userToInsert = new ApplicationUser { UserName = "elena@uclm.com", PhoneNumber = "0797697898", Email = "elena@uclm.com" };
                    var resultu = await userManager.CreateAsync(userToInsert, "Password@123");
                    var resultr = await userManager.AddToRoleAsync(userToInsert, adminRole);
                }

                if (!_context.Users.Any(u => u.UserName == "gregorio@uclm.com"))
                {
                    userToInsert = new ApplicationUser { UserName = "gregorio@uclm.com", PhoneNumber = "0797697898", Email = "gregorio@uclm.com" };
                    var resultu = await userManager.CreateAsync(userToInsert, "Password@123");
                    var resultr = await userManager.AddToRoleAsync(userToInsert, adminRole);
                }

                if (!_context.Users.Any(u => u.UserName == "admin@hotel.fbk"))
                {
                    userToInsert = new ApplicationUser { UserName = "admin@hotel.fbk", PhoneNumber = "8719638231", Email = "admin@hotel.fbk" };
                    var resultu = await userManager.CreateAsync(userToInsert, "Password@123");
                    var resultr = await userManager.AddToRoleAsync(userToInsert, adminRole);
                }

                if (!_context.Users.Any(u => u.UserName == "camarero@hotel.fbk"))
                {
                    userToInsert = new ApplicationUser { UserName = "camarero@hotel.fbk", PhoneNumber = "0797697898", Email = "camarero@hotel.fbk" };
                    var resultu = await userManager.CreateAsync(userToInsert, "Password@123");
                    var resultr = await userManager.AddToRoleAsync(userToInsert, waiterRole);
                }

                if (!_context.Users.Any(u => u.UserName == "recepcionista@hotel.fbk"))
                {
                    userToInsert = new ApplicationUser { UserName = "recepcionista@hotel.fbk", PhoneNumber = "0797697898", Email = "recepcionista@hotel.fbk" };
                    var resultu = await userManager.CreateAsync(userToInsert, "Password@123");
                    var resultr = await userManager.AddToRoleAsync(userToInsert, receptionistRole);
                }

                if (!_context.Users.Any(u => u.UserName == "ealmacen@hotel.fbk"))
                {
                    userToInsert = new ApplicationUser { UserName = "ealmacen@hotel.fbk", PhoneNumber = "0797697898", Email = "ealmacen@hotel.fbk" };
                    var resultu = await userManager.CreateAsync(userToInsert, "Password@123");
                    var resultr = await userManager.AddToRoleAsync(userToInsert, warehouseMgRole);
                }

                Menu menu;

                if (!_context.Menu.Any(m => m.idServicio == 1))
                {
                    menu = new Menu
                    {
                        descripcion = "Desayuno equilibrado",
                        nombre = "Desayuno",
                        horaInicio = new DateTime(2016, 1, 1, 9, 0, 0),
                        horaFin = new DateTime(2016, 1, 1, 11, 0, 0),
                        precio = 11f
                    };
                    _context.Menu.Add(menu);
                }

                if (!_context.Menu.Any(m => m.idServicio == 2))
                {
                    menu = new Menu
                    {
                        descripcion = "Comida equilibrada",
                        nombre = "Comida",
                        horaInicio = new DateTime(2016, 1, 1, 13, 0, 0),
                        horaFin = new DateTime(2016, 1, 1, 16, 0, 0),
                        precio = 12f
                    };
                    _context.Menu.Add(menu);
                }

                if (!_context.Menu.Any(m => m.idServicio == 3))
                {
                    menu = new Menu
                    {
                        descripcion = "Cena equilibrada",
                        nombre = "Cena",
                        horaInicio = new DateTime(2016, 1, 1, 19, 0, 0),
                        horaFin = new DateTime(2016, 1, 1, 22, 0, 0),
                        precio = 13f
                    };
                    _context.Menu.Add(menu);
                }

                Descuento descuento;
                if(!_context.Descuento.Any(d => d.idDescuento == 1))
                {
                    descuento = new Descuento
                    {
                        nombre = "Descuento1",
                        descripcion = "Wapo",
                        porcentaje = 25,
                        fechaInicio = new DateTime(2007, 1, 1, 19, 0, 0),
                        fechaFin = new DateTime(2030, 1, 1, 19, 0, 0)
                    };
                    _context.Add(descuento);
                }

                if (!_context.Descuento.Any(d => d.idDescuento == 2))
                {
                    descuento = new Descuento
                    {
                        nombre = "Descuento2",
                        descripcion = "Trisquelion",
                        porcentaje = 50,
                        fechaInicio = new DateTime(2017, 12, 1, 19, 0, 0),
                        fechaFin = new DateTime(2030, 1, 1, 19, 0, 0)
                    };
                    _context.Add(descuento);
                }

                Cliente cliente;
                if (!_context.Cliente.Any(d => d.idPersona == 1))
                {
                    cliente = new Cliente
                    {
                        nombre = "Eustaquio",
                        apellidos = "Habichuela",
                        codigoPostal = "16004",
                        correoElectronico = "agallas@cn.com",
                        direccion = "Casa en las afueras",
                        localidad = "Aquimismo",
                        nif = "12345678K",
                        numeroTarjeta = "1234567890123456",
                        pais = "Aquimismostan",
                        provincia = "Aquimismo",
                        telefono = "612345678"
                    };
                    _context.Add(cliente);
                }

                if (!_context.Cliente.Any(d => d.idPersona == 2))
                {
                    cliente = new Cliente
                    {
                        nombre = "Muriel",
                        apellidos = "Habichuela",
                        codigoPostal = "16004",
                        correoElectronico = "agallas2@cn.com",
                        direccion = "Casa en las afueras",
                        localidad = "Aquimismo",
                        nif = "12345678N",
                        numeroTarjeta = "1234567890123459",
                        pais = "Aquimismostan",
                        provincia = "Aquimismo",
                        telefono = "612345679"
                    };
                    _context.Add(cliente);
                }
                _context.SaveChanges();

                if (!_context.Reserva.Any(r => r.idReserva == 1) &&
                    !_context.Factura.Any(f => f.idFactura == 1) &&
                    !_context.Habitacion.Any(h => h.numero == 1) &&
                    !_context.ReservaHabitacion.Any(reh => reh.idReservaHabitacion == 1))
                {
                    factura = new Factura
                    {
                        abono = false
                    };

                    cliente = _context.Cliente.FirstOrDefault(c => c.idPersona == 1);
                    reserva = new Reserva
                    {
                        fechaRealizacion = DateTime.Today.AddDays(-6),
                        fechaInicio = DateTime.Today.AddDays(-5),
                        fechaFin = DateTime.Today.AddDays(5),
                        regimenComida = Regimen.MEDIA,
                        Cliente = cliente,
                        comentarios = "Reserva eustaquia",
                        Factura = factura
                    };

                    habitacion = new Habitacion
                    {
                        descripcion = "Habitacion doble",
                        aforo = 2,
                        precio = 90f,
                        localizacion = "Primera planta",
                        ocupada = true
                    };

                    rh = new ReservaHabitacion
                    {
                        Habitacion = habitacion,
                        Reserva = reserva
                    };

                    _context.Factura.Add(factura);
                    _context.Reserva.Add(reserva);
                    _context.Habitacion.Add(habitacion);
                    _context.ReservaHabitacion.Add(rh);
                    _context.SaveChanges();

                    factura = _context.Factura.FirstOrDefault(f => f.idFactura == 1);
                    reserva = _context.Reserva.FirstOrDefault(f => f.idReserva == 1);
                    factura.Reserva = reserva;
                    _context.Update(factura);
                }

                if (!_context.Reserva.Any(r => r.idReserva == 2) &&
                    !_context.Factura.Any(f => f.idFactura == 2) &&
                    !_context.Habitacion.Any(h => h.numero == 2) &&
                    !_context.ReservaHabitacion.Any(reh => reh.idReservaHabitacion == 2))
                {
                    factura = new Factura
                    {
                        abono = false
                    };

                    cliente = _context.Cliente.FirstOrDefault(c => c.idPersona == 2);
                    reserva = new Reserva
                    {
                        fechaRealizacion = DateTime.Today.AddDays(-6),
                        fechaInicio = DateTime.Today.AddDays(-5),
                        fechaFin = DateTime.Today.AddDays(5),
                        regimenComida = Regimen.COMPLETA,
                        Cliente = cliente,
                        comentarios = "Reserva murielida",
                        Factura = factura
                    };

                    habitacion = new Habitacion
                    {
                        descripcion = "Habitacion para cuatro",
                        aforo = 4,
                        precio = 100f,
                        localizacion = "Primera planta",
                        ocupada = true
                    };

                    rh = new ReservaHabitacion
                    {
                        Habitacion = habitacion,
                        Reserva = reserva
                    };

                    _context.Factura.Add(factura);
                    _context.Reserva.Add(reserva);
                    _context.Habitacion.Add(habitacion);
                    _context.ReservaHabitacion.Add(rh);
                    _context.SaveChanges();

                    factura = _context.Factura.FirstOrDefault(f => f.idFactura == 2);
                    reserva = _context.Reserva.FirstOrDefault(f => f.idReserva == 2);
                    factura.Reserva = reserva;
                    _context.Update(factura);
                }
                _context.SaveChanges();

                if (!_context.Habitacion.Any(h => h.numero == 3))
                {
                    habitacion = new Habitacion
                    {
                        descripcion = "Habitacion para cuatro",
                        aforo = 4,
                        precio = 100f,
                        localizacion = "Primera planta",
                        ocupada = false
                    };
                    _context.Add(habitacion);
                }

                Servicio servicio;
                if (!_context.Servicio.Any(s => s.idServicio == 4))
                {
                    servicio = new Servicio
                    {
                        descripcion = "Sauna",
                        precio = 15f,
                        nombre = "Sauna"
                    };
                    _context.Add(servicio);
                }

                if (!_context.Servicio.Any(s => s.idServicio == 5))
                {
                    servicio = new Servicio
                    {
                        descripcion = "Masaje",
                        precio = 40f,
                        nombre = "Masaje"
                    };
                    _context.Add(servicio);
                }

                Producto producto;
                if (!_context.Producto.Any(p => p.idProducto == 1))
                {
                    producto = new Producto
                    {
                        nombre = "Toalla",
                        cantidad = 3,
                        categoria = "Higiene",
                        descripcion = "Toallas",
                        limiteMinimo = 1,
                        localizador = "A11111111",
                        precio = 5f
                    };
                    _context.Add(producto);
                }

                if (!_context.Producto.Any(p => p.idProducto == 2))
                {
                    producto = new Producto
                    {
                        nombre = "Sabana",
                        cantidad = 2,
                        categoria = "Habitacion",
                        descripcion = "Sabanas",
                        limiteMinimo = 1,
                        localizador = "B22222222",
                        precio = 10f
                    };
                    _context.Add(producto);
                }

                if (!_context.Producto.Any(p => p.idProducto == 3))
                {
                    producto = new Producto
                    {
                        nombre = "Jabon",
                        cantidad = 3,
                        categoria = "Higiene",
                        descripcion = "Jabones",
                        limiteMinimo = 1,
                        localizador = "C33333333",
                        precio = 15f
                    };
                    _context.Add(producto);
                }

                if (!_context.Producto.Any(p => p.idProducto == 4))
                {
                    producto = new Producto
                    {
                        nombre = "Cuberteria",
                        cantidad = 4,
                        categoria = "Restaurante",
                        descripcion = "Cubiertos",
                        limiteMinimo = 1,
                        localizador = "D44444444",
                        precio = 50f
                    };
                    _context.Add(producto);
                }
                _context.SaveChanges();

                Proveedor proveedor;
                Persona contacto;
                if (!_context.Proveedor.Any(p => p.idProveedor == 1))
                {
                    contacto = _context.Cliente.FirstOrDefault(c => c.idPersona == 1);
                    proveedor = new Proveedor
                    {
                        nombre = "PedroSL",
                        cif = "11111111A",
                        correoElectronico = "proveedor1@patata.com",
                        direccion = "CalleUno", 
                        localidad = "Valencia",
                        pais = "Spain",
                        numeroCuenta = "NumeroCuentaUno",
                        provincia = "Valencia",
                        Persona = contacto
                    };
                    _context.Add(proveedor);
                }

                if (!_context.Proveedor.Any(p => p.idProveedor == 2))
                {
                    contacto = _context.Cliente.FirstOrDefault(c => c.idPersona == 2);
                    proveedor = new Proveedor
                    {
                        nombre = "JoseSL",
                        cif = "22222222B",
                        correoElectronico = "proveedor2@patata.com",
                        direccion = "CalleDos",
                        localidad = "Madrid",
                        pais = "Spain",
                        numeroCuenta = "NumeroCuentaDos",
                        provincia = "Madrid",
                        Persona = contacto
                    };
                    _context.Add(proveedor);
                }

                if (!_context.Proveedor.Any(p => p.idProveedor == 3))
                {
                    contacto = _context.Cliente.FirstOrDefault(c => c.idPersona == 3);
                    proveedor = new Proveedor
                    {
                        nombre = "FranciscoSL",
                        cif = "33333333C",
                        correoElectronico = "proveedor3@patata.com",
                        direccion = "CalleTres",
                        localidad = "Barcelona",
                        pais = "Spain",
                        numeroCuenta = "NumeroCuentaTres",
                        provincia = "Barcelona",
                        Persona = contacto
                    };
                    _context.Add(proveedor);
                }
                _context.SaveChanges();

                ProductoProveedor propro;
                if (!_context.ProductoProveedor.Any(pp => pp.idProductoProveedor == 1))
                {
                    producto = _context.Producto.FirstOrDefault(p => p.idProducto == 1);
                    proveedor = _context.Proveedor.FirstOrDefault(p => p.idProveedor == 1);

                    propro = new ProductoProveedor
                    {
                        precio = 5f,
                        Producto = producto,
                        Proveedor = proveedor
                    };
                    _context.Add(propro);
                }

                if (!_context.ProductoProveedor.Any(pp => pp.idProductoProveedor == 2))
                {
                    producto = _context.Producto.FirstOrDefault(p => p.idProducto == 2);
                    proveedor = _context.Proveedor.FirstOrDefault(p => p.idProveedor == 1);

                    propro = new ProductoProveedor
                    {
                        precio = 10f,
                        Producto = producto,
                        Proveedor = proveedor
                    };
                    _context.Add(propro);
                }

                if (!_context.ProductoProveedor.Any(pp => pp.idProductoProveedor == 3))
                {
                    producto = _context.Producto.FirstOrDefault(p => p.idProducto == 3);
                    proveedor = _context.Proveedor.FirstOrDefault(p => p.idProveedor == 1);

                    propro = new ProductoProveedor
                    {
                        precio = 15f,
                        Producto = producto,
                        Proveedor = proveedor
                    };
                    _context.Add(propro);
                }

                if (!_context.ProductoProveedor.Any(pp => pp.idProductoProveedor == 4))
                {
                    producto = _context.Producto.FirstOrDefault(p => p.idProducto == 3);
                    proveedor = _context.Proveedor.FirstOrDefault(p => p.idProveedor == 2);

                    propro = new ProductoProveedor
                    {
                        precio = 15f,
                        Producto = producto,
                        Proveedor = proveedor
                    };
                    _context.Add(propro);
                }

                if (!_context.ProductoProveedor.Any(pp => pp.idProductoProveedor == 5))
                {
                    producto = _context.Producto.FirstOrDefault(p => p.idProducto == 4);
                    proveedor = _context.Proveedor.FirstOrDefault(p => p.idProveedor == 2);

                    propro = new ProductoProveedor
                    {
                        precio = 50f,
                        Producto = producto,
                        Proveedor = proveedor
                    };
                    _context.Add(propro);
                }

                if(!_context.Reserva.Any(r => r.idReserva == 3))
                {
                    cliente = _context.Cliente.FirstOrDefault(c => c.idPersona == 1);
                    reserva = new Reserva
                    {
                        fechaRealizacion = DateTime.Today,
                        fechaInicio = DateTime.Today.AddDays(2),
                        fechaFin = DateTime.Today.AddDays(5),
                        regimenComida = Regimen.MEDIA,
                        Cliente = cliente,
                        comentarios = "Reserva tercera",
                    };

                    habitacion = _context.Habitacion.FirstOrDefault(h => h.numero == 3);

                    rh = new ReservaHabitacion
                    {
                        Habitacion = habitacion,
                        Reserva = reserva
                    };

                    _context.Reserva.Add(reserva);
                    _context.ReservaHabitacion.Add(rh);
                }

                await _context.SaveChangesAsync();
                //_context.SaveChanges();
            }
        }
    }
}