using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
namespace MycGroupApp
{
    public partial class ImprimirComprobante : Form
    {
        OperacionViewModel operacion;
        ClienteViewModel cliente;
        public ImprimirComprobante(object operacion, object cliente)
        {
            InitializeComponent();

            this.operacion = (OperacionViewModel)operacion;
            this.cliente = (ClienteViewModel)cliente;

            if (this.operacion.hasta >= DateTime.Now)
            {
                panel16.Visible = true;
            }

            label1.Text = label18.Text = label27.Text = "Fecha:"+DateTime.Now.ToString("dd/MM/yyyy");
            label2.Text = label17.Text = label26.Text = this.cliente.nombre + " " + this.cliente.apellido;
            label4.Text = label15.Text = label24.Text = this.operacion.cuotas.ToString();
            label5.Text = label14.Text = label23.Text = this.operacion.hasta.ToString("dd/MM/yyy");
            label8.Text = label11.Text = label20.Text = "Desde: " + this.operacion.desde.ToString("dd/MM/yyyy") + " Hasta: " + this.operacion.hasta.ToString("dd/MM/yyyy");

            try {
                label3.Text = label16.Text = label25.Text = this.operacion.clienteVehiculo.patente.ToString();
                label6.Text = label13.Text = label22.Text = this.operacion.poliza.ToString();
                label7.Text = label12.Text = label21.Text = this.operacion.endoso.ToString();

                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");

                // Aplica el formato de moneda al valor double
                string formattedAmount = this.operacion.valor.ToString("C", culture);
                label9.Text = label10.Text = label19.Text = formattedAmount;
            } catch (Exception err) { MessageBox.Show(err.Message); }
            
           


        }
        //guardar y enviar
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = button2.Visible = buttonCancelar.Visible = false;
            Form k = this;
            // Definir las coordenadas y el tamaño del área a capturar
            int x = k.Location.X+10; // Coordenada X del área
            int y = k.Location.Y+35; // Coordenada Y del área
            int width = k.Width-10; // Ancho del área
            int height = k.Height-30; // Altura del área

            // Capturar la parte específica de la pantalla
            Bitmap screenshot = CaptureArea(x, y, width, height);

            // Guardar la captura en un archivo (en este caso, en el escritorio)
            //  string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\captura_pantalla.png";
            string filePath = Router.Captura;
            screenshot.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);

            button1.Visible = button2.Visible = buttonCancelar.Visible = true;

            PDF.CrearRemitoOperacion("Remito-"+label2.Text+".pdf",true);
        }

        public static Bitmap CaptureArea(int x, int y, int width, int height)
        {
            // Crear un Bitmap con el tamaño del área a capturar
            Bitmap screenshot = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            // Crear un objeto de gráficos a partir del Bitmap
            using (Graphics graphics = Graphics.FromImage(screenshot))
            {
                // Copiar el contenido de la pantalla en el área especificada al Bitmap
                graphics.CopyFromScreen(x, y, 0, 0, new Size(width, height), CopyPixelOperation.SourceCopy);
            }

            return screenshot;
        }

        private void ImprimirComprobante_Load(object sender, EventArgs e)
        {

        }

        //imprimir
        private  void button2_Click(object sender, EventArgs e)
        {
            button1.Visible = button2.Visible = buttonCancelar.Visible = false;
            
            Form k = this;
            // Definir las coordenadas y el tamaño del área a capturar
            int x = k.Location.X + 10; // Coordenada X del área
            int y = k.Location.Y + 35; // Coordenada Y del área
            int width = k.Width - 10; // Ancho del área
            int height = k.Height - 45; // Altura del área

            // Capturar la parte específica de la pantalla
            Bitmap screenshot = CaptureArea(x, y, width, height);

            // Guardar la captura en un archivo (en este caso, en el escritorio)
            //  string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\captura_pantalla.png";
            string filePath = Router.Captura;
            screenshot.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);

            button1.Visible = button2.Visible = buttonCancelar.Visible = true;

            PDF.CrearRemitoOperacion("Remito-" + label2.Text + ".pdf", false);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label62_Click(object sender, EventArgs e)
        {

        }
    }
}
