using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
namespace MycGroupApp
{
    public partial class Home : Form
    {
        Form formAnterior;
        UsuarioViewModel usu;
        public Home(ref Form x, object usu)
        {
            InitializeComponent();
            formAnterior = x;
            x.Visible = false;

            this.usu = (UsuarioViewModel)usu;
            load();
        }
        public void load()
        {
            // Descargar la imagen desde el enlace


            if (this.usu.imagen != null)
            {

                try
                {
                    WebClient webClient = new WebClient();
                    byte[] imagenBytes = webClient.DownloadData(this.usu.imagen);
                    Image imagen = Image.FromStream(new MemoryStream(imagenBytes));
                    panel4.BackgroundImage = imagen;
                    panel4.BackgroundImageLayout = ImageLayout.Stretch;
                }
                catch (Exception err) { }
            }

            label2.Text = this.usu.nombre + " " + this.usu.apellido;
            label3.Text = this.usu.createdAt.ToShortDateString();

            switch (this.usu.tipo)
            {
                case 1: { break; }
                case 2: { button4.Visible = false; break; }
                case 3: { break; }
            }


            if (!usu.estado)
            {
                // operarCaja.show
                Form k = this;
                k.Visible = false;
                new OperarCaja(ref k, usu, "Apertura de caja").Show();
            }
        }
        private void Login_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form k = this;
            new GestionDeCobros(ref k, usu).Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form k = this;
            new MonederoVirtual(ref k, usu).Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form k = this;
            new GestionDeAdministracion(ref k, usu).Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            formAnterior.Visible = true;
            Form k = this;
            k.Close();
        }
    }
}
