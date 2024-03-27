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
    public partial class RenovarClienteServicio : Form
    {
        Form formAnterior;
        OperacionViewModel operacion;
        UsuarioViewModel usu;
        ClienteViewModel cli;
       
        public RenovarClienteServicio(ref Form x, object usu, int operacionId, object cliente)
        {
            
            InitializeComponent();
            formAnterior = x;
            formAnterior.Visible = false;
            this.usu = (UsuarioViewModel)usu;
            this.cli = (ClienteViewModel)cliente;
            getOperacion(operacionId);
           

        }
        
        public void getOperacion(int operacionId)
        {
            string url = Router.Operacion + "/" + operacionId;
            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Clear();
                    var response = client.GetAsync(url).Result;
                    var res = response.Content.ReadAsStringAsync().Result;
                    this.operacion = JsonConvert.DeserializeObject<OperacionViewModel>(res.ToString());
                    Load();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
        }

        public double calcularPrecio()
        {
            // calcular precio 
                string url = Router.Cliente + "/calcularPrecio/" + operacion.clienteVehiculoId;

                try
                {
                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        var response = client.GetAsync(url).Result;
                        var res = response.Content.ReadAsStringAsync().Result;

                        PrecioViewModel precio = JsonConvert.DeserializeObject<PrecioViewModel>(res.ToString());

                        return precio.valor;
                        

                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }

            return 0;
        }
        public void Load()
        {
            
            textBox1.Text = operacion.clienteVehiculoId + "." + operacion.clienteVehiculo.patente;
            textBox3.Text = operacion.cedulaVerde;
            textBox2.Text = operacion.servicioId + "." + operacion.servicio.nombre;

            textBox11.Text = calcularPrecio().ToString();
            textBox12.Text = (operacion.cuotas+1).ToString();
            textBox10.Text = (operacion.endoso+1).ToString();
            textBox9.Text = operacion.poliza.ToString();
            textBox6.Text = operacion.subrogacion.ToString();
            textBox3.Text = operacion.cedulaVerde;
            textBox7.Text = operacion.cotizacion.ToString();
            textBox8.Text = operacion.propuesta.ToString();

            //comparar si esta vencido
            if (operacion.hasta>DateTime.Now)
            {
                dateTimePicker1.Value = operacion.desde;
                dateTimePicker2.Value = operacion.hasta.AddMonths(1);
            }
            else
            {

                dateTimePicker1.Value = DateTime.Now;
                dateTimePicker2.Value = DateTime.Now.AddMonths(1);
            }
            label16.Text = "Cliente: " + cli.nombre + " " + cli.apellido;

        }
        public bool patch()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string url = Router.Operacion + "/" + this.operacion.id;

                    client.DefaultRequestHeaders.Clear();

                    DateTime fechaActual = DateTime.Now;

                    string parametros = "{ 'usuarioId':'" + usu.id + "' ,'oficinaId':'" + Router.OficinaLocal + "','valor':'" + textBox11.Text + "','cuotas':'" + textBox12.Text + "','desde':'" + dateTimePicker1.Value.ToString("MM/dd/yyyy") + "','hasta':'" + dateTimePicker2.Value.ToString("MM/dd/yyyy") + "','createdAt':'" + fechaActual.AddHours(3).ToString("MM/dd/yyyy") + "'";
                    if (textBox9.Text != "") { parametros += ", 'poliza':'" + textBox9.Text + "' "; }
                    if (textBox10.Text != "") { parametros += ", 'endoso':'" + textBox10.Text + "'"; }
                    if (textBox6.Text != "") { parametros += ", 'subrogacion':'" + textBox6.Text + "' "; }
                    if (textBox3.Text != "") { parametros += ", 'cedulaVerde':'" + textBox3.Text + "' "; }
                    if (textBox7.Text != "") { parametros += ", 'cotizacion':'" + textBox7.Text + "'"; }
                    if (textBox8.Text != "") { parametros += ", 'propuesta':'" + textBox8.Text + "' "; }

                    parametros += "}";

                    JObject j = JObject.Parse(parametros);

                    var httpContent = new StringContent(j.ToString(), Encoding.UTF8, "application/json");

                    var response = client.PutAsync(url, httpContent).Result;

                    var res = response.Content.ReadAsStringAsync().Result;

                    this.operacion = JsonConvert.DeserializeObject<OperacionViewModel>(res.ToString());

                    return true;

                }
                catch (Exception err) { return false; MessageBox.Show(err.Message); }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool band = patch();

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

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
