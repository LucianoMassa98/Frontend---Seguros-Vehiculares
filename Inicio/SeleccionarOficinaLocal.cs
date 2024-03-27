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
    public partial class SeleccionarOficinaLocal : Form
    {
        Form anterior;
        public SeleccionarOficinaLocal(ref Form anterior)
        {
            InitializeComponent();
            this.anterior = anterior;
            get();
        }
        public void get()
        {

            string urlOficinas = Router.Oficina;

            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Clear();
                    var response = client.GetAsync(urlOficinas).Result;
                    var res = response.Content.ReadAsStringAsync().Result;
                    List<OficinaViewModel> list = JsonConvert.DeserializeObject<List<OficinaViewModel>>(res.ToString());


                    foreach (OficinaViewModel x in list)
                    {
                        comboBox1.Items.Add(x.id + "." + x.nombre + "." + x.direccion);
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
            anterior.Enabled = false;
            DelayedFunction(9000);
            Form k = this;
            k.TopMost = true;
        }
        static async Task DelayedFunction(int millisecondsDelay)
        {
            await Task.Delay(millisecondsDelay);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form k = this;

            anterior.Close();

            k.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string l = "";
            try
            {
                l = comboBox1.Text.Split('.')[0];

            }
            catch (Exception)
            {
                MessageBox.Show("Debe seleccionar una oficina");
            }

            int i = Router.IndexOficinaLocal(l);

            if (i == -1) { MessageBox.Show("Error al seleccionar oficina, llamar al soporte técnico"); }
            else
            {
                anterior.Enabled = true;
                Form k = this;
                k.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Focus();
        }
    }
}
