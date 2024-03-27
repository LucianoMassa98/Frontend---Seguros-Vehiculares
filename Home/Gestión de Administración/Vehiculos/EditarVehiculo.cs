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
using System.Net.Http;
using Newtonsoft.Json;
namespace MycGroupApp
{
    public partial class EditarVehiculo : Form
    {
        Form formAnterior;
        string idVehiculo;
        public EditarVehiculo(ref Form x, string idVehiculo)
        {
            
            InitializeComponent();
            formAnterior = x;
            x.Visible = false;
            this.idVehiculo = idVehiculo;
            get();
        }
        public bool checkInputs()
        {
            try
            {
                int marca = int.Parse(textBox1.Text);
                return false;
            }
            catch (Exception err)
            {

                try
                {
                    int modelo = int.Parse(textBox2.Text);
                    return false;

                }
                catch (Exception)
                {
                    return true;
                }
                MessageBox.Show("");
            }
            return false;
        }
        public void get()
        {

            string url = Router.Vehiculo + "/" + idVehiculo;


            using (var client = new HttpClient())
            {

                try
                {
                    client.DefaultRequestHeaders.Clear();
                    var response = client.GetAsync(url).Result;
                    var res = response.Content.ReadAsStringAsync().Result;



                    VehiculoViewModel x = JsonConvert.DeserializeObject<VehiculoViewModel>(res.ToString());


                    textBox2.Text = x.nombre;
                    textBox1.Text = x.fabricante;
                    textBox3.Text = x.id.ToString();



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
                    string url = Router.Vehiculo + "/" + idVehiculo;

                    client.DefaultRequestHeaders.Clear();

                    string parametros = "{ 'nombre':'" + textBox2.Text + "' , 'fabricante':'" + textBox1.Text + "'}";


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
            if (checkInputs())
            {
                patch();
                Form x = this;
                formAnterior.Visible = true;
                x.Close();
            }
            else
            {
                MessageBox.Show("Los valores de Marca y Modelo, deben ser alfanumericoss");
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
