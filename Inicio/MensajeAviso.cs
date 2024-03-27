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
    public partial class MensajeAviso : Form
    {
        Form anterior;
        public MensajeAviso(ref Form anterior)
        {
            InitializeComponent();
            this.anterior = anterior;
        }

        private void Login_Load(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form k = this;

            anterior.Close();

            k.Close();
        }
    }
}
