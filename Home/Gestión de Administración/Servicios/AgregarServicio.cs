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
namespace MycGroupApp
{
    public partial class AgregarServicio : Form
    {
        Form formAnterior;
        public AgregarServicio(ref Form x)
        {
            
            InitializeComponent();
            formAnterior = x;
            x.Visible = false;
        }
        public void post()
        {
            using (var client = new HttpClient())
            {
                string url = Router.Servicio;

                client.DefaultRequestHeaders.Clear();

                string parametros = "{ 'nombre':'" + textBox1.Text + "'}";


                JObject j = JObject.Parse(parametros);

                var httpContent = new StringContent(j.ToString(), Encoding.UTF8, "application/json");

                var response = client.PostAsync(url, httpContent).Result;

                var res = response.Content.ReadAsStringAsync().Result;


                MessageBox.Show(res.ToString());

            }
        }
        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            post();
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
