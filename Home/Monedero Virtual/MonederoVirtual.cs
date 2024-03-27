﻿using System;
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
    public partial class MonederoVirtual : Form
    {
        private Form activeForm = null;
        Form formAnterior;
        UsuarioViewModel usu;
        public MonederoVirtual(ref Form x, object usu)
        {
            InitializeComponent();
            formAnterior = x;
            formAnterior.Visible = false;
            this.usu = (UsuarioViewModel)usu;

            if (this.usu.tipo == 1)
            {

                button1.Visible = true;
                button2.Visible = false;
            }
            else
            {
                button1.Visible = false;
                button2.Visible = true;
            }
        }
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel2.Controls.Add(childForm);
            panel2.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            formAnterior.Visible = true;
            Form k = this;
            k.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form k = this;
           openChildForm(new Cajas(ref k, usu));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form k = this;
           openChildForm(new Caja(ref k, usu));
        }
    }
}
