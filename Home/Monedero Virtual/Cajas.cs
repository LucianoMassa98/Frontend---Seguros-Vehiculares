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
    public partial class Cajas : Form
    {
        Form formAnterior;
        UsuarioViewModel usu;
        public Cajas(ref Form x, object usu)
        {
            InitializeComponent();
            formAnterior = x;
            getOficinas();
            this.usu = (UsuarioViewModel)usu;
        }
        public void getOficinas()
        {
            string fechaDesde = dateTimePicker1.Value.ToString("yyyy-MM-dd") + "T00%3A00%3A00";
            string fechaHasta = dateTimePicker2.Value.ToString("yyyy-MM-dd") + "T23%3A59%3A59";


            string query = "?desde=" + fechaDesde + "&hasta=" + fechaHasta;

            string url = Router.Oficina + query;
            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Clear();
                    var response = client.GetAsync(url).Result;
                    var res = response.Content.ReadAsStringAsync().Result;
                    List<OficinaViewModel> list = JsonConvert.DeserializeObject<List<OficinaViewModel>>(res.ToString());

                    /*label1.Text = "ID: " + ofi.id;
                    label2.Text = "Fecha de alta: " + ofi.createdAt;
                    label3.Text = "Nombre: " + ofi.nombre;
                    label6.Text = "Celular: " + ofi.celular;
                    label16.Text = "$ " + ofi.Saldo().ToString();
                    dataGridView1.DataSource = ofi.operaciones;
                    dataGridView2.DataSource = ofi.movimientos;*/


                    dataGridView1.Rows.Clear();

                    foreach (OficinaViewModel x in list)
                    {
                        dataGridView1.Rows.Add(x.id, x.nombre, x.direccion, x.Saldo().ToString(), x.Gastos().ToString(), x.Venta().ToString());

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
            getOficinas();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PDF.listadoDe(dataGridView1, "Lista de cajas", $"periodo desde {dateTimePicker1.Value.ToString()} - hasta {dateTimePicker2.Value.ToString()}");
        }
    }
}
