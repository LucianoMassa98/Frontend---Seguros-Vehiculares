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
    public partial class Servicios : Form
    {
        Form formAnterior;
        UsuarioViewModel usu;
        public Servicios(ref Form x, object usu)
        {
            InitializeComponent();
            formAnterior = x;
            get();
            this.usu = (UsuarioViewModel)usu;
        }
        public void get()
        {

            string urlCliente = Router.Servicio;

            dynamic s;
            using (var client = new HttpClient())
            {

                try
                {
                    client.DefaultRequestHeaders.Clear();
                    var response = client.GetAsync(urlCliente).Result;
                    var res = response.Content.ReadAsStringAsync().Result;



                    List<ServicioViewModel> list = JsonConvert.DeserializeObject<List<ServicioViewModel>>(res.ToString());
                    dataGridView1.DataSource = list;
                    foreach (ServicioViewModel x in list)
                    {
                        comboBox1.Items.Add(x.nombre);
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
            new AgregarServicio(ref formAnterior).Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string l = label1.Text.Split(':')[1];

            if (l != "") { new EditarServicio(ref formAnterior, l).Show(); }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int n = dataGridView1.RowCount;
            int i = 0;

            while ((i < n) && (dataGridView1.Rows[i].Cells[1].Value.ToString() != comboBox1.Text)) { i++; }

            if (i < n)
            {
                dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[1];
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int ind = dataGridView1.CurrentCell.RowIndex;

            label1.Text = "Id: " + dataGridView1.Rows[ind].Cells[0].Value.ToString();
            label3.Text = "Fecha de alta: " + dataGridView1.Rows[ind].Cells[2].Value.ToString();
            label4.Text = "Nombre: " + dataGridView1.Rows[ind].Cells[1].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PDF.listadoDe(dataGridView1, "Lista de servicios","");
        }
    }
}
