using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamCatHotel.Models
{
    public class ResultMenuViewModel
    {
        public virtual string result
        {
            get;
            set;
        }
        public virtual Menu menu
        {
            get;
            set;
        }
    }
}
