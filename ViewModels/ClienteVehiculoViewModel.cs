using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MycGroupApp
{
    internal class ClienteVehiculoViewModel
    {
       
        public int id { get; set; }
        public int clienteId { get; set; }
        public int vehiculoId { get; set; }
        public string origen { get; set; }
        public string patente { get; set; }

        public int año { get; set; }

        public string motor { get; set; }

        public string combustible { get; set; }

        public string carroceria { get; set; }
        public string tipo { get; set; }

        public string chasis { get; set; }

        public string color { get; set; }

        public string prendario { get; set; }

        public DateTime createdAt { get; set; }

        public VehiculoViewModel vehiculo { get; set; }
    }
}
