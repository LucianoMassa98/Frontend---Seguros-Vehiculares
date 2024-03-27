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
    public partial class EditarServicioValor : Form
    {
       
        public EditarServicioValor()
        {
            
            InitializeComponent();
            
            get();
        }
        public void put(string precioId, string data)
        {
            
                using (var client = new HttpClient())
                {
                    try
                    {
                        string url = Router.Precio + "/:"+precioId;

                        client.DefaultRequestHeaders.Clear();


                        // string parametros = "{ 'servicioId':'" + idServicio + "', 'desde':'" + dateTimePicker1.Value.ToString("MM/dd/yyyy") + "','hasta':'" + dateTimePicker2.Value.ToString("MM/dd/yyyy") + "','valor':'" + textBox1.Text + "','carroceria':'" + comboBox3.Text + "','tipo':'" + comboBox4.Text + "'}";


                        JObject j = JObject.Parse(data);

                        var httpContent = new StringContent(j.ToString(), Encoding.UTF8, "application/json");

                        var response = client.PutAsync(url, httpContent).Result;

                        var res = response.Content.ReadAsStringAsync().Result;


                        MessageBox.Show(res.ToString());


                    }
                    catch (Exception err) { MessageBox.Show(err.Message); }



                }

            


        }
        public void get()
        {
            string urlCliente = Router.Precio + "/" ;
            using (var client = new HttpClient())
            {

                try
                {
                    client.DefaultRequestHeaders.Clear();
                    var response = client.GetAsync(urlCliente).Result;
                    var res = response.Content.ReadAsStringAsync().Result;



                    List<PrecioViewModel> servX = JsonConvert.DeserializeObject<List<PrecioViewModel>>(res.ToString());


                    dataGridView1.DataSource = servX;
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }






            }

        }
        public void post(string data)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string url = Router.Precio + "/";

                    client.DefaultRequestHeaders.Clear();




                    JObject j = JObject.Parse(data);

                    var httpContent = new StringContent(j.ToString(), Encoding.UTF8, "application/json");

                    var response = client.PostAsync(url, httpContent).Result;

                    var res = response.Content.ReadAsStringAsync().Result;


                    MessageBox.Show(res.ToString());


                }
                catch (Exception err) { MessageBox.Show(err.Message); }



            }
        }
        public void delete(string servicioValorId)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string url = Router.Precio + "/" + servicioValorId;

                    client.DefaultRequestHeaders.Clear();


                    var response = client.DeleteAsync(url).Result;
                    var res = response.Content.ReadAsStringAsync().Result;
                    MessageBox.Show(res.ToString());


                }
                catch (Exception err) { MessageBox.Show(err.Message); }


            }
        }
        
        
        public bool checkInputsPost()
        {
            if (!comboBox3.Items.Contains(comboBox3.Text)) { MessageBox.Show("Debe seleccionar carrocería correctamente"); return false; }
            if (!comboBox4.Items.Contains(comboBox4.Text)) { MessageBox.Show("Debe seleccionar tipo correctamente"); return false; }

            return true;
        }

        public bool checkInputsPut()
        {
            if (!comboBox1.Items.Contains(comboBox1.Text)) { MessageBox.Show("Debe seleccionar carrocería correctamente"); return false; }
            if (!comboBox2.Items.Contains(comboBox2.Text)) { MessageBox.Show("Debe seleccionar tipo correctamente"); return false; }

            return true;
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (checkInputsPost()) {
                string data = "{ 'desde':'" + dateTimePicker1.Value.ToString("MM/dd/yyyy") + "','hasta':'" + dateTimePicker2.Value.ToString("MM/dd/yyyy") + "','valor':'" + textBox1.Text + "','carroceria':'" + comboBox3.Text + "','tipo':'" + comboBox4.Text + "'}";
                post(data);
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //eliminar valor servicio

            if (dataGridView1.RowCount > 0)
            {
                int i = dataGridView1.CurrentRow.Index;

                string precioId = dataGridView1.Rows[i].Cells[0].Value.ToString();
                delete(precioId);
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0 && checkInputsPut())
            {

                int i = dataGridView1.CurrentRow.Index;
                string precioId = dataGridView1.Rows[i].Cells[0].Value.ToString();
                string data = "{ 'desde':'" + dateTimePicker4.Value.ToString("MM/dd/yyyy") + "','hasta':'" + dateTimePicker3.Value.ToString("MM/dd/yyyy") + "','valor':'" + textBox2.Text + "','carroceria':'" + comboBox1.Text + "','tipo':'" + comboBox2.Text + "'}";

                put(precioId, data);

            }
            
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

       

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {
            int ind = dataGridView1.CurrentCell.RowIndex;

            if(dataGridView1.RowCount > 0 )
            {
                try
                {
                    dateTimePicker4.Value = DateTime.Parse(dataGridView1.Rows[ind].Cells[2].Value.ToString());
                    dateTimePicker3.Value = DateTime.Parse(dataGridView1.Rows[ind].Cells[3].Value.ToString());
                    textBox2.Text = dataGridView1.Rows[ind].Cells[1].Value.ToString();
                    comboBox1.Text = dataGridView1.Rows[ind].Cells[5].Value.ToString();
                    comboBox2.Text = dataGridView1.Rows[ind].Cells[4].Value.ToString();
                } catch (Exception) { }
               
            }
        }
    }
}
