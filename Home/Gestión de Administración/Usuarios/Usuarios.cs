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
    public partial class Usuarios : Form
    {
        Form formAnterior;
        public Usuarios(ref Form x)
        {
            InitializeComponent();
            formAnterior = x;
            get();
        }
        public void get()
        {

            string url = Router.Usuario;

            using (var client = new HttpClient())
            {

                try
                {
                    client.DefaultRequestHeaders.Clear();
                    var response = client.GetAsync(url).Result;
                    var res = response.Content.ReadAsStringAsync().Result;



                    List<UsuarioViewModel> list = JsonConvert.DeserializeObject<List<UsuarioViewModel>>(res.ToString());
                    dataGridView1.DataSource = list;
                    foreach (UsuarioViewModel x in list)
                    {
                        comboBox1.Items.Add(x.username);
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
            new AgregarUsuario(ref formAnterior).Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string l = label1.Text.Split(':')[1];

            if (l != "") { new EditarUsuario(ref formAnterior, l).Show(); }
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
            label3.Text = "Fecha de alta: " + dataGridView1.Rows[ind].Cells[11].Value.ToString();
            label4.Text = "UserName: " + dataGridView1.Rows[ind].Cells[3].Value.ToString();
            label5.Text = "Nombre: " + dataGridView1.Rows[ind].Cells[1].Value.ToString();
            label8.Text = "Apellido: " + dataGridView1.Rows[ind].Cells[2].Value.ToString();
            label9.Text = "Tipo: " + dataGridView1.Rows[ind].Cells[5].Value.ToString();
            label10.Text = "Correo: " + dataGridView1.Rows[ind].Cells[8].Value.ToString();
            label11.Text = "Nacimiento: " + dataGridView1.Rows[ind].Cells[10].Value.ToString();
            label12.Text = "Dirección: " + dataGridView1.Rows[ind].Cells[7].Value.ToString();
            label13.Text = "Celular: " + dataGridView1.Rows[ind].Cells[6].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PDF.listadoDe(dataGridView1, "Lista de usuarios","");
        }
    }
}
