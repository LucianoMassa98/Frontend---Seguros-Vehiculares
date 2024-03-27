using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MycGroupApp
{
    internal class VehiculoViewModel
    {

        public int id { get; set; }
        public string nombre { get; set; }
        public string fabricante { get; set; } 
        public DateTime createdAt { get; set; }

        public ClienteVehiculoViewModel ClienteVehiculo { get; set; }
    }
}
