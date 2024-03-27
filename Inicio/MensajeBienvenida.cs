using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MycGroupApp
{
    public partial class MensajeBienvenida : Form
    {
        Form anterior;
        public MensajeBienvenida(ref Form anterior)
        {
            InitializeComponent();
            this.anterior = anterior;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            anterior.Enabled = false;
            DelayedFunction(9000);
            Form k = this;
            k.TopMost = true;
        }
        static async Task DelayedFunction(int millisecondsDelay)
        {
            await Task.Delay(millisecondsDelay);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form k = this;

            anterior.Close();

            k.Close();
        }
    }
}
