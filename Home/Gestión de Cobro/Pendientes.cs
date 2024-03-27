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
    public partial class Pendientes : Form
    {
        UsuarioViewModel usu;
        List<OperacionViewModel> operaciones;
        Form formAnterior;
        public Pendientes(ref Form x, object usu)
        {
            InitializeComponent();
            this.formAnterior = formAnterior;
            this.usu = (UsuarioViewModel)usu;
            get();
        }
        public void get()
        {
            string query = "?pendiente=true";

            string url = Router.Operacion + query;
            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Clear();
                    var response = client.GetAsync(url).Result;
                    var res = response.Content.ReadAsStringAsync().Result;

                    operaciones = JsonConvert.DeserializeObject<List<OperacionViewModel>>(res.ToString());


                    dataGridView1.Rows.Clear();

                    foreach (OperacionViewModel x in operaciones)
                    {
                        dataGridView1.Rows.Add(x.id, x.cliente.nombre, x.cliente.celular, x.servicio.nombre, x.clienteVehiculo.vehiculo.nombre, x.clienteVehiculo.patente, x.hasta.AddHours(-3));
                        comboBox1.Items.Add(x.cliente.dni.ToString());
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }

            }


        }
        
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int ind = dataGridView1.CurrentCell.RowIndex;
         }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (operaciones.Count > 0)
            {
                int i = dataGridView1.CurrentRow.Index;

                new RenovarClienteServicio(ref formAnterior, usu,operaciones[i].id, operaciones[i].cliente).Show();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form k = this;
            formAnterior.Visible = true;
            k.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = dataGridView1.Rows[comboBox1.SelectedIndex].Cells[1];

        }
    }
}
