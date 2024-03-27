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
    public partial class EditarCliente : Form
    {
        Form formAnterior;
        string idCliente;
        public EditarCliente(ref Form x, string idCliente)
        {
            
            InitializeComponent();
            formAnterior = x;
            formAnterior.Visible = false;
            this.idCliente = idCliente;
            get();
        }
        public void get()
        {

            string url = Router.Cliente + "/" + idCliente;


            using (var client = new HttpClient())
            {

                try
                {
                    client.DefaultRequestHeaders.Clear();
                    var response = client.GetAsync(url).Result;
                    var res = response.Content.ReadAsStringAsync().Result;



                    ClienteViewModel x = JsonConvert.DeserializeObject<ClienteViewModel>(res.ToString());


                    textBox1.Text = x.nombre;
                    textBox2.Text = x.apellido;
                    textBox3.Text = x.dni.ToString();
                    textBox4.Text = x.email;
                    textBox5.Text = x.celular.ToString();
                    comboBox1.Text = x.provincia;
                    comboBox2.Text = x.tipoConsumidor;
                    comboBox3.Text = x.tipoPersona;
                    textBox6.Text = x.cuit.ToString();
                    comboBox4.Text = x.sexo;
                    textBox7.Text = x.imagen;
                    textBox8.Text = x.codPostal.ToString();
                    textBox9.Text = x.direccion;



                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }






            }



        }
        public bool verificarComboBoxs()
        {
            if (!comboBox1.Items.Contains(comboBox1.Text))
            {
                MessageBox.Show("Debe seleccionar correctamente la provincia del cliente");
                return false;
            }
            if (!comboBox2.Items.Contains(comboBox2.Text))
            {
                MessageBox.Show("Debe seleccionar correctamente el tipo de consumidor del cliente");
                return false;
            }
            if (!comboBox3.Items.Contains(comboBox3.Text))
            {
                MessageBox.Show("Debe seleccionar correctamente el tipo de persona para el cliente");
                return false;
            }
            if (!comboBox4.Items.Contains(comboBox4.Text))
            {
                MessageBox.Show("Debe seleccionar correctamente el sexo de la persona para el cliente");
                return false;
            }



            return true;
        }
        public void patch()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string url = Router.Cliente + "/" + idCliente;

                    client.DefaultRequestHeaders.Clear();

                    string parametros = "{ 'nombre':'" + textBox1.Text + "' , 'apellido':'" + textBox2.Text + "','dni':'" + textBox3.Text + "' , 'celular':'" + textBox5.Text + "' , 'direccion':'" + textBox9.Text + "' , 'email':'" + textBox4.Text + "' , 'sexo':'" + comboBox4.Text + "' , 'provincia':'" + comboBox1.Text + "' , 'tipoConsumidor':'" + comboBox2.Text + "' , 'tipoPersona':'" + comboBox3.Text + "' , 'codPostal':'" + textBox8.Text + "' , 'cuit':'" + textBox6.Text + "'  ";
                    if (textBox7.Text != "")
                    {
                        parametros += ", 'imagen':'" + textBox7.Text + "' }";
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (verificarComboBoxs())
            {
                patch();
                Form x = this;
                formAnterior.Visible = true;
                x.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form x = this;
            formAnterior.Visible = true;
            x.Close();
        }

     
    }
}
