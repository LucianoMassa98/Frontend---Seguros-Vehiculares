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
    public partial class Comprobantes : Form
    {
        Form formAnterior;
        UsuarioViewModel usu;
        ClienteViewModel cli;
        public Comprobantes(ref Form x, object usu, int clienteId)
        {
            InitializeComponent();
            formAnterior = x;
            formAnterior.Visible = false;
            getCliente(clienteId);
            this.usu = (UsuarioViewModel)usu;
            load();
        }

        public void getCliente(int id)
        {
            string urlCliente = Router.Cliente+"/"+id;
            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Clear();
                    var response = client.GetAsync(urlCliente).Result;
                    var res = response.Content.ReadAsStringAsync().Result;
                    cli = JsonConvert.DeserializeObject<ClienteViewModel>(res.ToString());
                    

                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }

            }

        }

        public void load()
        {
            foreach (OperacionViewModel x in cli.operaciones)
            {
                string estado = "Activo";

                if (DateTime.Now > x.hasta) { estado = "Vencido"; }
                dataGridView1.Rows.Add(x.id, x.oficinaId, x.servicio.nombre, x.clienteVehiculo.vehiculo.nombre, x.clienteVehiculo.patente, x.createdAt.AddHours(-3), x.hasta.AddHours(-3), estado);
                if (estado == "Vencido")
                {
                    dataGridView1.Rows[dataGridView1.RowCount - 1].DefaultCellStyle.BackColor = Color.LightSalmon;
                }
            }
        }
        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int ind = dataGridView1.CurrentCell.RowIndex;
        }


        private void button3_Click_1(object sender, EventArgs e)
        {
            int ind = dataGridView1.CurrentCell.RowIndex;
            Form k = this;
            new RenovarClienteServicio(ref k, usu, cli.operaciones[ind].id, cli).Show();
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form k = this;
            formAnterior.Visible = true;
            k.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int ind = dataGridView1.CurrentCell.RowIndex;
            new ImprimirComprobante(cli.operaciones[ind], cli).Show();
        }
    }
}
