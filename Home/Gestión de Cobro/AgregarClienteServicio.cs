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
    public partial class AgregarClienteServicio : Form
    {
        Form formAnterior;
        ClienteViewModel cli;
        UsuarioViewModel usu;
        List<ServicioViewModel> listServicios;

        OperacionViewModel operacion=new OperacionViewModel();
        public AgregarClienteServicio(ref Form x, object usu, int clienteId)
        {

            InitializeComponent();
            formAnterior = x;
            formAnterior.Visible = false;
            this.usu = (UsuarioViewModel)usu;
            getCliente(clienteId);
            getServicios();
        }
        public void getServicios()
        {

            string url = Router.Servicio;
            using (var client = new HttpClient())
            {

                try
                {
                    client.DefaultRequestHeaders.Clear();
                    var response = client.GetAsync(url).Result;
                    var res = response.Content.ReadAsStringAsync().Result;



                    listServicios = JsonConvert.DeserializeObject<List<ServicioViewModel>>(res.ToString());
                    comboBox2.Items.Clear();
                    foreach (ServicioViewModel x in listServicios)
                    {
                        comboBox2.Items.Add(x.id + "." + x.nombre);
                    }


                    dateTimePicker2.Value = dateTimePicker2.Value.AddMonths(1);

                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }

        }
        public void getCliente(int id)
        {

            string url = Router.Cliente + "/" + id;

          
            using (var client = new HttpClient())
            {

                try
                {
                    client.DefaultRequestHeaders.Clear();
                    var response = client.GetAsync(url).Result;
                    var res = response.Content.ReadAsStringAsync().Result;
                    cli = JsonConvert.DeserializeObject<ClienteViewModel>(res.ToString());
                    comboBox1.Items.Clear();
                   
                    foreach (VehiculoViewModel x in cli.items)
                    {
                        comboBox1.Items.Add(x.ClienteVehiculo.id + "|" + x.fabricante + "|" + x.nombre);
                        comboBox1.Items.Add(x.fabricante + "|" + x.nombre + "|" + x.ClienteVehiculo.id);
                        comboBox1.Items.Add(x.nombre + "|" + x.fabricante + "|" + x.ClienteVehiculo.id);
                    }


                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }

        }
        public bool post(string data)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string url = Router.Operacion;

                    client.DefaultRequestHeaders.Clear();



                    JObject j = JObject.Parse(data);

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
                    
                }
                catch (Exception err) {  MessageBox.Show(err.Message); return false; }

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

            string data = "{ 'usuarioId':'" + usu.id + "' ,'clienteId':'" + cli.id + "' , 'clienteVehiculoId':'" + findClienteVehiculoId(comboBox1.Text.Split('|')) + "','servicioId':'" + comboBox2.Text.Split('.')[0] + "', 'oficinaId':'" + Router.OficinaLocal + "','valor':'" + textBox11.Text + "','cuotas':'" + textBox12.Text + "','desde':'" + dateTimePicker1.Value.ToString("MM/dd/yyyy") + "','hasta':'" + dateTimePicker2.Value.ToString("MM/dd/yyyy") + "'  ";


            if (textBox9.Text != "") { data += ", 'poliza':'" + textBox9.Text + "' "; }
            if (textBox10.Text != "") { data += ", 'endoso':'" + textBox10.Text + "'"; }
            if (textBox6.Text != "") { data += ", 'subrogacion':'" + textBox6.Text + "' "; }
            if (textBox3.Text != "") { data += ", 'cedulaVerde':'" + textBox3.Text + "' "; }
            if (textBox7.Text != "") { data += ", 'cotizacion':'" + textBox7.Text + "'"; }
            if (textBox8.Text != "") { data += ", 'propuesta':'" + textBox8.Text + "' "; }

            data += "}";
            bool band= post(data);
            if (band)
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


        public int findClienteVehiculoId(string[] text)
        {
            if (text.Count() == 3)
            {
                try
                {
                    int id = int.Parse(text[0]);
                    return id;
                }
                catch (Exception)
                {
                    try
                    {
                        int id = int.Parse(text[2]);
                        return id;
                    }
                    catch (Exception)
                    {
                        return -1;
                    }

                }
                
            }
            return -1;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Enabled = true;
            int clienteVehiculoId = findClienteVehiculoId(comboBox1.Text.Split('|'));

            if (clienteVehiculoId!=-1)
            { // calcular precio 
                string url = Router.Cliente + "/calcularPrecio/" + clienteVehiculoId;

                try
                {
                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        var response = client.GetAsync(url).Result;
                        var res = response.Content.ReadAsStringAsync().Result;
                       
                        PrecioViewModel precio = JsonConvert.DeserializeObject<PrecioViewModel>(res.ToString());
                        
                        textBox11.Text = precio.valor.ToString();
                        if (textBox11.Text=="0"){
                            MessageBox.Show("No hay precio disponible para este vehículo");
                        }

                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
            else
            {
                MessageBox.Show("No ingreso correctamente el vehículo del cliente");
            }
            

        }


        public int findOperacion(int clienteId, int servicioId, int clienteVehiculoId)
        {

            string url = Router.Operacion + "/" + clienteId + "/" + servicioId + "/" + clienteVehiculoId;
           
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    var response = client.GetAsync(url).Result;
                    var res = response.Content.ReadAsStringAsync().Result;
                     operacion = JsonConvert.DeserializeObject<OperacionViewModel>(res.ToString());
                    if (operacion.id>0)
                    {
                        return operacion.id;
                    }
                   
                }
            }
            catch (Exception) { return -1; }

            return -1;
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indClienteVehiculo = findClienteVehiculoId(comboBox1.Text.Split('|'));
            int indServico = int.Parse(comboBox2.Text.Split('.')[0]);
            // textBox11.Text = listServicios[indServico].CalcularValor(cli.items[indClienteVehiculo].ClienteVehiculo).ToString();
            int operacionId = findOperacion(cli.id, indServico, indClienteVehiculo);
            // buscar operacion si existiera. 
            if (operacionId == -1)
            {

                textBox12.Text = "1";
                textBox10.Text = "0";
            }
            else
            {
                DialogResult resultado = MessageBox.Show("¿Desea renovar la covertura del cliente?", "Confirmación", MessageBoxButtons.OKCancel);

                if (resultado == DialogResult.OK)
                {
                    // El usuario hizo clic en Aceptar
                    Form k = this;

                    
                    new RenovarClienteServicio(ref k, usu, operacionId, cli).Show();
                }
                comboBox1.Text = comboBox2.Text = "";


            }


        }

    }
}
