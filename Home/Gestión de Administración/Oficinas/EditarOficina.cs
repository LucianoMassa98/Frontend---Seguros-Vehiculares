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
    public partial class EditarOficina : Form
    {
        Form formAnterior;
        string idOficina;
        public EditarOficina(ref Form x, string idOficina)
        {
            
            InitializeComponent();
            formAnterior = x;
            this.idOficina = idOficina;
            x.Visible = false;
            get();
        }
        public void get()
        {

            string url = Router.Oficina + "/" + idOficina;


            using (var client = new HttpClient())
            {

                try
                {
                    client.DefaultRequestHeaders.Clear();
                    var response = client.GetAsync(url).Result;
                    var res = response.Content.ReadAsStringAsync().Result;



                    OficinaViewModel x = JsonConvert.DeserializeObject<OficinaViewModel>(res.ToString());

                    textBox4.Text = x.id.ToString();
                    textBox1.Text = x.nombre;
                    textBox2.Text = x.direccion;
                    textBox3.Text = x.celular;



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
                string url = Router.Oficina + "/" + idOficina;

                client.DefaultRequestHeaders.Clear();

                string parametros = "{ 'nombre':'" + textBox1.Text + "', 'celular':'" + textBox3.Text + "', 'direccion':'" + textBox2.Text + "'}";


                JObject j = JObject.Parse(parametros);

                var httpContent = new StringContent(j.ToString(), Encoding.UTF8, "application/json");

                var response = client.PutAsync(url, httpContent).Result;

                var res = response.Content.ReadAsStringAsync().Result;


                MessageBox.Show(res.ToString());

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
