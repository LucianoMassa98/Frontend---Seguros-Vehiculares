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
    public partial class Vehiculos : Form
    {
        Form formAnterior;
        public Vehiculos(ref Form x)
        {
            InitializeComponent();
            formAnterior = x;
            get();
        }
        public void get()
        {

            string url = Router.Vehiculo;

            using (var client = new HttpClient())
            {

                try
                {
                    client.DefaultRequestHeaders.Clear();
                    var response = client.GetAsync(url).Result;
                    var res = response.Content.ReadAsStringAsync().Result;



                    List<VehiculoViewModel> list = JsonConvert.DeserializeObject<List<VehiculoViewModel>>(res.ToString());
                    dataGridView1.Rows.Clear();
                    foreach (VehiculoViewModel x in list)
                    {
                        comboBox1.Items.Add(x.nombre);
                        dataGridView1.Rows.Add(x.id, x.nombre, x.fabricante, x.createdAt.ToString());
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
            new AgregarVehiculo(ref formAnterior).Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string l = label1.Text.Split(':')[1];

            if (l != "")
            {
                new EditarVehiculo(ref formAnterior, l).Show();

            }
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
            try
            {
                if (ind>=0)
                {
                    label1.Text = "Id: " + dataGridView1.Rows[ind].Cells[0].Value.ToString();
                    label3.Text = "Fecha de alta: " + dataGridView1.Rows[ind].Cells[3].Value.ToString();
                    label4.Text = "Marca: " + dataGridView1.Rows[ind].Cells[1].Value.ToString();
                    label5.Text = "Modelo: " + dataGridView1.Rows[ind].Cells[2].Value.ToString();
                }
               
            }
            catch (Exception) { }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            PDF.listadoDe(dataGridView1, "Lista de vehículos","");
        }
    }
}
