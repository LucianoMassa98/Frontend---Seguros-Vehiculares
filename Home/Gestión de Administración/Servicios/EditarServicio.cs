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
using System.Text;
namespace MycGroupApp
{
    public partial class EditarServicio : Form
    {
        Form formAnterior;
        string idServicio;
        public EditarServicio(ref Form x, string idServicio)
        {
            
            InitializeComponent();
            formAnterior = x;
            x.Visible = false;
            this.idServicio = idServicio;
            get();
        }
        public void get()
        {

            string urlCliente = Router.Servicio + "/" + idServicio;

            dynamic s;
            using (var client = new HttpClient())
            {

                try
                {
                    client.DefaultRequestHeaders.Clear();
                    var response = client.GetAsync(urlCliente).Result;
                    var res = response.Content.ReadAsStringAsync().Result;



                    ServicioViewModel servX = JsonConvert.DeserializeObject<ServicioViewModel>(res.ToString());

                    textBox4.Text = servX.id.ToString();
                    textBox1.Text = servX.nombre;

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
                    string url = Router.Servicio + "/" + idServicio;

                    client.DefaultRequestHeaders.Clear();

                    string parametros = "{ 'nombre':'" + textBox1.Text + "'}";


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

        
    }
}
