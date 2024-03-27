using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net.Http;
namespace MycGroupApp
{
    public partial class Caja : Form
    {
        Form formAnterior;
        UsuarioViewModel usu;
        public Caja(ref Form x, object usu)
        {
            InitializeComponent();
            formAnterior = x;
            getOficina();
            this.usu = (UsuarioViewModel)usu;
        }
        public void getOficina()
        {
            string fechaDesde = dateTimePicker1.Value.ToString("yyyy-MM-dd") + "T00%3A00%3A00";
            string fechaHasta = dateTimePicker2.Value.ToString("yyyy-MM-dd") + "T23%3A59%3A59";


            string query = "?desde=" + fechaDesde + "&hasta=" + fechaHasta;

            string oficinaId = Router.OficinaLocal;
            string url = Router.Oficina + "/" + oficinaId + query;
            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Clear();
                    var response = client.GetAsync(url).Result;
                    var res = response.Content.ReadAsStringAsync().Result;
                    OficinaViewModel ofi = JsonConvert.DeserializeObject<OficinaViewModel>(res.ToString());

                    label1.Text = "ID: " + ofi.id;
                    label3.Text = "Fecha de alta: " + ofi.createdAt;
                    label4.Text = "Nombre: " + ofi.nombre;
                    label5.Text = "Celular: " + ofi.celular;
                    label10.Text = "$ " + ofi.Saldo().ToString();

                    dataGridView1.Rows.Clear();
                    dataGridView2.Rows.Clear();

                    foreach (OperacionViewModel x in ofi.operaciones)
                    {
                        dataGridView1.Rows.Add(x.id, x.usuario.nombre + " " + x.usuario.apellido, x.cliente.nombre + " " + x.cliente.apellido, x.servicio.nombre, x.valor, x.createdAt.AddHours(-3));
                    }
                    foreach (MovimientoViewModel x in ofi.movimientos)
                    {
                        dataGridView2.Rows.Add(x.id, x.usuario.nombre + " " + x.usuario.apellido, x.descripcion, x.valor, x.createdAt.AddHours(-3));
                    }


                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }

            }


        }
        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new AgregarOficina(ref formAnterior).Show();
        }


       

        private void button5_Click(object sender, EventArgs e)
        {
            new OperarCaja(ref formAnterior, usu).Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            getOficina();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PDF.listadoDe(dataGridView1, "Lista de operaciones", $"periodo desde {dateTimePicker1.Value.ToString()} - hasta {dateTimePicker2.Value.ToString()}");
            PDF.listadoDe(dataGridView2, "Lista de movimientos", $"periodo desde {dateTimePicker1.Value.ToString()} - hasta {dateTimePicker2.Value.ToString()}");
        }
    }
}
