using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MycGroupApp
{
    internal class OperacionViewModel
    {
       
        public int id { get; set; }
        public int usuarioId { get; set; }
        public int clienteId { get; set; }
        public int clienteVehiculoId { get; set; }

        public int servicioId { get; set; }

        public int oficinaId { get; set; }

        public double valor { get; set; }

        public double cuotas { get; set; }

        public DateTime desde { get; set; }

        public DateTime hasta { get; set; }

       
        public double cotizacion { get; set; }
        public double propuesta { get; set; }
        public double poliza { get; set; }
        public double endoso { get; set; }
        public double subrogacion { get; set; }

        public string cedulaVerde { get; set; }
        public DateTime createdAt { get; set; }

        public ClienteViewModel cliente{ get; set; }
        public UsuarioViewModel usuario { get; set; }
        public ClienteVehiculoViewModel clienteVehiculo{ get; set; }
        public ServicioViewModel servicio { get; set; }
    }
}
