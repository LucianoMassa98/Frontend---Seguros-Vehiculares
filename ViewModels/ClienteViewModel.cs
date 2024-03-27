using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MycGroupApp
{
    internal class ClienteViewModel
    {
       
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }

        public double dni { get; set; }

        public double celular { get; set; }

        public string direccion { get; set; }

        public string email { get; set; }

        public string imagen { get; set; }

        public string sexo { get; set; }

        public string provincia { get; set; }

        public string tipoConsumidor { get; set; }

        public string tipoPersona { get; set; }

        public double codPostal { get; set; }

        public double cuit { get; set; }
        public DateTime createdAt { get; set; }

        public List<VehiculoViewModel> items { get; set; }

        public List<OperacionViewModel> operaciones { get; set; }

       
    }
}
