using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamCatHotel.Models
{
    public class ReservaMenuViewModel
    {
        public virtual int nHabitacion
        {
            get;
            set;
        }

        public virtual string nCliente
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
