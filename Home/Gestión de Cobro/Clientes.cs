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
    public partial class Clientes : Form
    {
        Form formAnterior;
        UsuarioViewModel usu;
        public Clientes(ref Form x, object usu)
        {
            InitializeComponent();
            formAnterior = x;

            this.usu = (UsuarioViewModel)usu;
            get();
        }
        public void get()
        {

            string urlCliente = Router.Cliente;

            using (var client = new HttpClient())
            {

                try
                {
                    client.DefaultRequestHeaders.Clear();
                    var response = client.GetAsync(urlCliente).Result;
                    var res = response.Content.ReadAsStringAsync().Result;



                    List<ClienteViewModel> list = JsonConvert.DeserializeObject<List<ClienteViewModel>>(res.ToString());
                    dataGridView1.DataSource = list;


                    foreach (ClienteViewModel x in list)
                    {

                        comboBox1.Items.Add(x.dni);
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
            new AgregarCliente(ref formAnterior).Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string l = label1.Text.Split(':')[1];

            if (l != "") { new EditarCliente(ref formAnterior, l).Show(); }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int n = dataGridView1.RowCount;
            int i = 0;

            while ((i < n) && (dataGridView1.Rows[i].Cells[3].Value.ToString() != comboBox1.Text)) { i++; }

            if (i < n)
            {
                dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[3];
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int ind = dataGridView1.CurrentCell.RowIndex;

            label1.Text = "Id: " + dataGridView1.Rows[ind].Cells[0].Value.ToString();
            label3.Text = "Fecha de alta: " + dataGridView1.Rows[ind].Cells[14].Value.ToString();
            label4.Text = "Nombre: " + dataGridView1.Rows[ind].Cells[1].Value.ToString();
            label5.Text = "D.n.i: " + dataGridView1.Rows[ind].Cells[3].Value.ToString();
            label8.Text = "Correo: " + dataGridView1.Rows[ind].Cells[6].Value.ToString();
            label9.Text = "Celular: " + dataGridView1.Rows[ind].Cells[4].Value.ToString();
            label10.Text = "Provincia: " + dataGridView1.Rows[ind].Cells[9].Value.ToString();
            label11.Text = "Sexo: " + dataGridView1.Rows[ind].Cells[8].Value.ToString();
            label12.Text = "Tipo Persona: " + dataGridView1.Rows[ind].Cells[11].Value.ToString();
            label13.Text = "Tipo Consumidor: " + dataGridView1.Rows[ind].Cells[10].Value.ToString();
            label14.Text = "C. Postal: " + dataGridView1.Rows[ind].Cells[12].Value.ToString();
            label15.Text = "Cuit: " + dataGridView1.Rows[ind].Cells[13].Value.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string l = label1.Text.Split(':')[1];
            new AgregarClienteVehiculo(ref formAnterior, l).Show();

        }

        private void button5_Click(object sender, EventArgs e)
        {

            string l = label1.Text.Split(':')[1];
            new AgregarClienteServicio(ref formAnterior, usu, int.Parse(l)).Show();

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            PDF.listadoDe(dataGridView1, "Lista de clientes","");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string l = label1.Text.Split(':')[1];
            Form x = this;
            new Comprobantes(ref x, usu, int.Parse(l)).Show();
        }
    }
}
