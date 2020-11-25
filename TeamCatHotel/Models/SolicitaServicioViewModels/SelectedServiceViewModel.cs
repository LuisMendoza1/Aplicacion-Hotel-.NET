using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TeamCatHotel.Models.SolicitaServicioViewModels
{
    public class SelectedServiceViewModel
    {
        // SERV
        [StringLength(200, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z0-9]+[a-zA-Z0-9''-'\s]*$")]
        [Display(Name = "Servicio: ")]
        public virtual string nombre
        {
            get;
            set;
        }
        [DataType(DataType.Currency)]
        public virtual float precio
        {
            get;
            set;
        }
        [Required]
        public virtual int idServicio
        {
            get;
            set;
        }
        // HAB
        [Required]
        [Display(Name = "Habitacion: ")]
        public virtual int numeroHab
        {
            get;
            set;
        }
        // RS

        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha inicio: ")]
        public virtual DateTime fechaInicio
        {
            get;
            set;
        }

        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha fin: ")]
        public virtual DateTime fechaFin
        {
            get;
            set;
        }

    }
}
