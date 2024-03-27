using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MycGroupApp
{
    internal class OficinaViewModel
    {
       
        public int id { get; set; }
        public string nombre { get; set; }
        public string celular { get; set; }
        public string direccion { get; set; }


        public List<MovimientoViewModel> movimientos { get; set; }
        public List<OperacionViewModel> operaciones { get; set; }
        public DateTime createdAt { get; set; }


        public double Saldo()
        {
            double sum = 0;
            foreach(MovimientoViewModel x in this.movimientos)
            {
                sum += x.valor;
            }

            foreach (OperacionViewModel x in this.operaciones)
            {
                sum += x.valor;
            }
            return sum;
        }

        public double Gastos() {
            double sum = 0;
            foreach (MovimientoViewModel x in movimientos) {

                if ((x.descripcion != "Cierre de caja") &&(x.descripcion != "Apertura de caja")) { sum += x.valor; }
            }
            return sum;
        }
        public double Venta() {
            double sum = 0;
            foreach (OperacionViewModel x in operaciones)
            { sum += x.valor; 
            }
            return sum;
        }

    }
}
