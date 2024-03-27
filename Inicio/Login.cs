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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        public void post(string username, string password)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string url = Router.Login + "/signIn";

                    client.DefaultRequestHeaders.Clear();

                    string parametros = "{ 'username':'" + username + "' , 'password':'" + password + "' ,'oficinaId':'" + Router.OficinaLocal + "'}";

                    


                    JObject j = JObject.Parse(parametros);

                    var httpContent = new StringContent(j.ToString(), Encoding.UTF8, "application/json");

                    var response = client.PostAsync(url, httpContent).Result;

                    var res = response.Content.ReadAsStringAsync().Result;


                    
                    UsuarioViewModel usu = JsonConvert.DeserializeObject<UsuarioViewModel>(res.ToString());
                    if (usu.tipo > 0)
                    {
                        textBox1.Text = textBox2.Text = "";
                        Form k = this;
                        new Home(ref k, usu).Show();
                    }
                    else
                    {
                       
                        MessageBox.Show("Acceso denegado!!! usuario/contraseña incorrecta/s");
                        MessageBox.Show(res.ToString());
                    }


                }
                catch (Exception err) { MessageBox.Show(err.Message); }

            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            Form k = this;

            switch (Router.IndexOficinaLocal())
            {
                case -1: { new MensajeAviso(ref k).Show(); break; }
                case 0: { new SeleccionarOficinaLocal(ref k).Show(); break; }
            }

            if (Router.EstadoUso == false) { new MensajeBienvenida(ref k).Show(); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            post(textBox1.Text, textBox2.Text);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) { textBox2.Focus(); }
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) { button1.Focus(); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form k = this; 

            new AgregarUsuario(ref k, true).Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = !textBox2.UseSystemPasswordChar;
            if (!textBox2.UseSystemPasswordChar) { button3.Text = "O"; } else { button3.Text = "M"; }
        }
        
    }
}
