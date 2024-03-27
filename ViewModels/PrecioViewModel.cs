using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MycGroupApp
{
    internal class PrecioViewModel
    {
       
        public int id { get; set; }
        public double valor { get; set; }
        public DateTime desde { get; set; }
        public DateTime hasta { get; set; }
        public string carroceria { get; set; }
        public string tipo { get; set; }
        public DateTime createdAt { get; set; }

    }
}
