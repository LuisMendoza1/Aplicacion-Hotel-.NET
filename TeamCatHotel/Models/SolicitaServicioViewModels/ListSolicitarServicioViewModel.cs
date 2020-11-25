using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TeamCatHotel.Models.SolicitaServicioViewModels
{
    public class ListSolicitarServicioViewModel
    {
        
        [Display(Name = "Habitacion: ")]
        public virtual int numeroHab
        {
            get;
            set;
        }

        public virtual bool ocupada
        {
            get;
            set;
        }

        public virtual ICollection<Servicio> ListaServicios
        {
            get;
            set;
        }

        public virtual int idServicio
        {
            get;
            set;
        }
    }
}
