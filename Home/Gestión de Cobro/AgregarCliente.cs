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
    public partial class AgregarCliente: Form
    {
        Form formAnterior;
        public AgregarCliente(ref Form x)
        {
            
            InitializeComponent();
            formAnterior = x;
            x.Visible = false;
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
        public bool post(string parametros)
        {

            using (var client = new HttpClient())
            {
                try
                {
                    string url = Router.Cliente;

                    client.DefaultRequestHeaders.Clear();

                   

                    JObject j = JObject.Parse(parametros);

                    var httpContent = new StringContent(j.ToString(), Encoding.UTF8, "application/json");

                   
                    HttpResponseMessage response = client.PostAsync(url, httpContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        // La solicitud fue exitosa, puedes procesar la respuesta aquí si es necesario.
                        MessageBox.Show("Operación completada correctamente.", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    else
                    {
                        // La solicitud no fue exitosa, manejar el error
                        var res = response.Content.ReadAsStringAsync().Result;
                        //string jsonResponse = await response.Content.ReadAsStringAsync();
                        ErrorResponse errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(res.ToString());

                        // Muestra el mensaje de error en un cuadro de diálogo
                        MessageBox.Show(Traduccion.traducir(errorResponse.message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    return true;
                   
                }
                catch (Exception err) { MessageBox.Show(err.Message); return false; }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (verificarComboBoxs())
            {
                string parametros = "{ 'nombre':'" + textBox1.Text + "' , 'apellido':'" + textBox2.Text + "','dni':'" + textBox3.Text + "' , 'celular':'" + textBox5.Text + "' , 'direccion':'" + textBox9.Text + "' , 'email':'" + textBox4.Text + "' , 'sexo':'" + comboBox4.Text + "' , 'provincia':'" + comboBox1.Text + "' , 'tipoConsumidor':'" + comboBox2.Text + "' , 'tipoPersona':'" + comboBox3.Text + "' , 'codPostal':'" + textBox8.Text + "' , 'cuit':'" + textBox6.Text + "' ";

                if (textBox7.Text != "")
                {
                    parametros += ", 'imagen':'" + textBox7.Text + "' }";
                }
                else
                {
                    parametros += "}";
                }
                bool band= post(parametros);
                if (band)
                {
                    Form x = this;
                    formAnterior.Visible = true;
                    x.Close();

                }
                
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
