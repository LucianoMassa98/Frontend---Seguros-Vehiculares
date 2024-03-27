using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MycGroupApp
{
    internal class UsuarioViewModel
    {
       
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public int tipo { get; set; }
        public double celular { get; set; }

        public string direccion { get; set; }

        public string email { get; set; }

        public string imagen { get; set; }

        public DateTime nacimiento{ get; set; }
        public DateTime createdAt { get; set; }

        public bool estado { get; set; }
    }
}
