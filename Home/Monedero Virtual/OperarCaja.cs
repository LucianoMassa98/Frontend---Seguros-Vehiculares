using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
namespace MycGroupApp
{
    public partial class OperarCaja : Form
    {
        Form formAnterior;
        UsuarioViewModel usu;
        public OperarCaja(ref Form x, object usu)
        {
            InitializeComponent();
            formAnterior = x;
            x.Visible = false;
            textBox1.Text = Router.OficinaLocal;
            this.usu = (UsuarioViewModel)usu;
        }
        public OperarCaja(ref Form x, object usu, string descripcion)
        {
            InitializeComponent();
            formAnterior = x;
            formAnterior.Visible = false;
            Form k = this;
            k.TopMost = true;
            textBox1.Text = Router.OficinaLocal;
            this.usu = (UsuarioViewModel)usu;

            comboBox1.SelectedIndex = 0;
            if (comboBox1.Text == descripcion) { comboBox1.Enabled = false; }

            button2.Visible = false;
        }
        public void post()
        {
            if ((comboBox1.SelectedIndex < comboBox1.Items.Count) && (comboBox1.SelectedIndex >= 0))
            {


                using (var client = new HttpClient())
                {
                    try
                    {
                        string url = Router.Movimiento;

                        client.DefaultRequestHeaders.Clear();
                        double valorImporte = 0;
                        if (comboBox1.SelectedIndex > 0)
                        {
                            valorImporte = double.Parse(textBox2.Text) * -1;
                        }
                        else { valorImporte = double.Parse(textBox2.Text); }


                        string parametros = "{ 'usuarioId':'" + usu.id + "' , 'oficinaId':'" + Router.OficinaLocal + "','valor':'" + valorImporte.ToString() + "' , 'descripcion':'" + comboBox1.Text + "' }";


                        JObject j = JObject.Parse(parametros);

                        var httpContent = new StringContent(j.ToString(), Encoding.UTF8, "application/json");

                        var response = client.PostAsync(url, httpContent).Result;

                        var res = response.Content.ReadAsStringAsync().Result;

                    }
                    catch (Exception err) { MessageBox.Show(err.Message); }

                }
            }
            else
            {
                MessageBox.Show("Debe selecionar una descripción válida para el movimiento");
            }


        }
        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            DialogResult resultado = MessageBox.Show("¿Desea continuar?", "Confirmación", MessageBoxButtons.OKCancel);

            if (resultado == DialogResult.OK)
            {
                // El usuario hizo clic en Aceptar
                post();
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Focus();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button1.Focus();
            }
        }
    }
}
