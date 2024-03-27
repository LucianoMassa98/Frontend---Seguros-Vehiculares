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
    public partial class AgregarClienteVehiculo : Form
    {
        Form formAnterior;
        ClienteViewModel cli;
        List<VehiculoViewModel> list;
        public AgregarClienteVehiculo(ref Form x, string clienteId)
        {
            
            InitializeComponent();
            formAnterior = x;
            formAnterior.Visible = false;
            getCliente(clienteId);
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



                    list = JsonConvert.DeserializeObject<List<VehiculoViewModel>>(res.ToString());
                    comboBox1.Items.Clear();
                    foreach (VehiculoViewModel x in list)
                    {
                        comboBox1.Items.Add(x.id + "|" + x.fabricante + "|" + x.nombre);
                        comboBox1.Items.Add(x.fabricante + "|" + x.nombre + "|" + x.id);
                        comboBox1.Items.Add(x.nombre + "|" + x.fabricante + "|" + x.id);
                    }

                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }






            }



        }
        public string GetOrigen()
        {
            if (checkBox2.Checked) { return "Importado"; }
            return "Nacional";
        }
        public string GetPrendario()
        {
            if (checkBox3.Checked) { return "Si"; }
            return "No";
        }

        public bool checkInputs()
        {
            if (!comboBox1.Items.Contains(comboBox1.Text)) { MessageBox.Show("Debe seleccionar vehículo correctamente"); return false; }
            if (!comboBox2.Items.Contains(comboBox2.Text)) { MessageBox.Show("Debe seleccionar tipo de combustible correctamente"); return false; }
            if (!comboBox4.Items.Contains(comboBox4.Text)) { MessageBox.Show("Debe seleccionar carrocería correctamente"); return false; }
            if (!comboBox3.Items.Contains(comboBox3.Text)) { MessageBox.Show("Debe seleccionar tipo correctamente"); return false; }

            if (!checkBox1.Checked && !checkBox2.Checked) { MessageBox.Show("Debe seleccionar origen correctamente"); return false; }

            try
            {
                int id = int.Parse(comboBox1.Text.Split('|')[0]);
                
            }
            catch (Exception)
            {

                int id = int.Parse(comboBox1.Text.Split('|')[2]);

                //buscar y seleccionar
                int i = 0;
                while ((i < list.Count) && (list[i].id!= id)) { i++; }

                if (i < list.Count) { comboBox1.Text = list[i].id + "|" + list[i].fabricante + "|" + list[i].nombre; }
                
                //

            }
            return true;
        }
        public void getCliente(string clienteId)
        {

            string url = Router.Cliente + "/" + clienteId;
            using (var client = new HttpClient())
            {

                try
                {
                    client.DefaultRequestHeaders.Clear();
                    var response = client.GetAsync(url).Result;
                    var res = response.Content.ReadAsStringAsync().Result;



                    cli = JsonConvert.DeserializeObject<ClienteViewModel>(res.ToString());

                    foreach (VehiculoViewModel x in cli.items)
                    {
                        dataGridView1.Rows.Add(x.id, x.fabricante, x.nombre, x.ClienteVehiculo.patente,x.ClienteVehiculo.carroceria, x.ClienteVehiculo.tipo,x.ClienteVehiculo.año);
                    }

                    // foreach(ClienteVehiculoViewModel x in cli.vehiculos)

                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }






            }

        }
        public void post()
        {
            if (checkInputs())
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        string url = Router.Cliente + "/vehiculo";

                        client.DefaultRequestHeaders.Clear();

                        string parametros = "{ 'clienteId':'" + cli.id + "' , 'vehiculoId':'" + comboBox1.Text.Split('|')[0] + "','origen':'" + GetOrigen() + "','patente':'" + textBox1.Text + "','año':'" + textBox2.Text + "','motor':'" + textBox4.Text + "','combustible':'" + comboBox2.Text + "','carroceria':'" + comboBox4.Text + "','tipo':'" + comboBox3.Text + "','chasis':'" + textBox11.Text + "','color':'" + textBox3.Text + "','prendario':'" + GetPrendario() + "', }";


                        JObject j = JObject.Parse(parametros);

                        var httpContent = new StringContent(j.ToString(), Encoding.UTF8, "application/json");

                        var response = client.PostAsync(url, httpContent).Result;

                        var res = response.Content.ReadAsStringAsync().Result;


                        MessageBox.Show(res.ToString());
                    }
                    catch (Exception err) { MessageBox.Show(err.Message); }

                }
            }
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Enabled = true;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Form k = this;
            new AgregarVehiculo(ref k).Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = !checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = !checkBox2.Checked;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            get();
        }
    }
}
