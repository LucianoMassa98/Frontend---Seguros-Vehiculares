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
    public partial class AgregarUsuario : Form
    {
        Form formAnterior;
        public AgregarUsuario(ref Form x)
        {
            
            InitializeComponent();
            formAnterior = x;
            x.Visible = false;
        }
        public AgregarUsuario(ref Form x, bool band)
        {

            InitializeComponent();
            formAnterior = x;
            x.Visible = false;

            numericUpDown1.Value = 2;
            numericUpDown1.Enabled = false;
        }
        public bool checkInputs()
        {
            return true;
        }
        public bool post()
        {
            if (true)
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        string url = Router.Usuario;

                        client.DefaultRequestHeaders.Clear();

                        string parametros = "{ 'username':'" + textBox1.Text + "' , 'password':'" + textBox5.Text + "' ,'tipo':'" + numericUpDown1.Value.ToString() + "' ,'nombre':'" + textBox2.Text + "' ,'apellido':'" + textBox3.Text + "'  ,'celular':'" + textBox8.Text + "' ,'direccion':'" + textBox6.Text + "' ,'email':'" + textBox4.Text + "' ,'imagen':'" + textBox7.Text + "'   ,'nacimiento':'" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "'  ,'dni':'" + textBox9.Text + "'           }";
                        string parametros1 = "{'username':'" + textBox1.Text + "' , 'password':'" + textBox5.Text + "' ,'tipo':'" + numericUpDown1.Value.ToString() + "' ,'nombre':'" + textBox2.Text + "' ,'apellido':'" + textBox3.Text + "'  ,'celular':'" + textBox8.Text + "' ,'direccion':'" + textBox6.Text + "' ,'email':'" + textBox4.Text + "' ,'nacimiento':'" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "'  ,'dni':'" + textBox9.Text + "'";
                        if (textBox7.Text != "")
                        {
                            parametros1 += ",'imagen':'" + textBox7.Text + "'}";
                        }
                        else{
                            parametros1 += "}";
                        }
                        
                        


                        JObject j = JObject.Parse(parametros1);

                        var httpContent = new StringContent(j.ToString(), Encoding.UTF8, "application/json");

                        var response = client.PostAsync(url, httpContent).Result;

                        var res = response.Content.ReadAsStringAsync().Result;


                        MessageBox.Show(res.ToString());
                    }
                    catch (Exception err) {
                        
                        MessageBox.Show(err.Message);
                        return false;
                    }

                }
            }

            return true;
        }
        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (post())
            {
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
