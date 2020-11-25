using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeamCatHotel.Models.ResevarViewModels
{
    public class DatosHabitacionReservaViewModel
    {
        // DATOS CLIENTE

        [Required]
        [RegularExpression(@"^(\d{8}[A-Z])$")]
        [Display(Name = "NIF:")]
        public virtual string nif
        {
            get;
            set;
        }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [Display(Name = "Nombre:")]
        public virtual string nombre
        {
            get;
            set;
        }


        [Required]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [Display(Name = "Apellidos:")]
        public virtual string apellidos
        {
            get;
            set;
        }

        [Required]
        public virtual int idPersona
        {
            get;
            set;
        }



        // DATOS RESERVA

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha Inicio:")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime fechaInicio
        {
            get;
            set;
        }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha Fin:")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime fechaFin
        {
            get;
            set;
        }

        [StringLength(150, MinimumLength = 5)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [Display(Name = "Comentarios:")]
        public virtual string comentarios
        {
            get;
            set;
        }

        [Required]
        [Display(Name = "Regimen de comidas:")]
        public virtual int regimenComida
        {
            get;
            set;
        }

        [Display(Name = "Seleccione un descuento:")]
        public string descuentoSeleccionado
        {
            get;
            set;
        }

        [Required]
        [Display(Name = "Seleccione las habitaciones requeridas:")]
        public string[] habitacionesSeleccionadas
        {
            get;
            set;
        }


        // DATOS NECESARIOS PARA LA VISTA, LISTADO HABITACIONES Y DESCUENTOS

        public IEnumerable<Habitacion> Habitaciones
        {
            get;
            set;
        }

        public IEnumerable<Descuento> Descuentos
        {
            get;
            set;
        }
    }
}
