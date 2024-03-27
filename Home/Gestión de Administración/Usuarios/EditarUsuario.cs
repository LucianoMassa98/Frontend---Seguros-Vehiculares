using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
namespace MycGroupApp
{
    public partial class EditarUsuario : Form
    {
        Form formAnterior;
        string idUsuario;
        public EditarUsuario(ref Form x, string idUsuario)
        {
            
            InitializeComponent();
            formAnterior = x;
            x.Visible = false;
            this.idUsuario = idUsuario;
            get();
        }

        public void get()
        {

            string url = Router.Usuario + "/" + idUsuario;


            using (var client = new HttpClient())
            {

                try
                {
                    client.DefaultRequestHeaders.Clear();
                    var response = client.GetAsync(url).Result;
                    var res = response.Content.ReadAsStringAsync().Result;



                    UsuarioViewModel x = JsonConvert.DeserializeObject<UsuarioViewModel>(res.ToString());


                    textBox1.Text = x.username;
                    textBox5.Text = x.password;
                    numericUpDown1.Value = x.tipo;
                    textBox2.Text = x.nombre;
                    dateTimePicker1.Value = x.nacimiento;
                    textBox8.Text = x.celular.ToString();
                    textBox3.Text = x.apellido;
                    textBox6.Text = x.direccion;
                    textBox7.Text = x.imagen;
                    textBox4.Text = x.email;
                    textBox9.Text = x.id.ToString();



                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }






            }



        }
        public void patch()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string url = Router.Usuario + "/" + idUsuario;

                    client.DefaultRequestHeaders.Clear();

                    string parametros = "{ 'username':'" + textBox1.Text + "' , 'password':'" + textBox5.Text + "' ,'tipo':'" + numericUpDown1.Value.ToString() + "' ,'nombre':'" + textBox2.Text + "' ,'apellido':'" + textBox3.Text + "' ,'celular':'" + textBox8.Text + "' ,'direccion':'" + textBox6.Text + "' ,'email':'" + textBox4.Text + "' ,'nacimiento':'" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' ";

                    if (textBox7.Text!="")
                    {
                        parametros += ",'imagen':'" + textBox7.Text + "' }";
                    }
                    else
                    {
                        parametros += "}";

                    }

                    JObject j = JObject.Parse(parametros);

                    var httpContent = new StringContent(j.ToString(), Encoding.UTF8, "application/json");

                    var response = client.PutAsync(url, httpContent).Result;

                    var res = response.Content.ReadAsStringAsync().Result;


                    MessageBox.Show(res.ToString());
                }
                catch (Exception err) { MessageBox.Show(err.Message); }

            }
        }
        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            patch();
            Form x = this;
            formAnterior.Visible = true;
            x.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form x = this;
            formAnterior.Visible = true;
            x.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
