using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MycGroupApp
{
    internal class MovimientoViewModel
    {
        public int id { get; set; }
        public int usuarioId { get; set; }
        public int oficinaId { get; set; }
        public double valor { get; set; }
        public string descripcion { get; set; }
        public DateTime createdAt { get; set; }
        public UsuarioViewModel usuario { get; set; }
    }
}
