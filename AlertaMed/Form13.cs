using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Forms;

namespace AlertaMed
{
    public partial class Form13 : Form
    {
        private string textoGuardado = "";
        private string textoGuardado2 = "";

        private string textoGuardado3 = "";
        public Form13()
        {
            InitializeComponent();
        }
        public Form13(string texto)
        {
            InitializeComponent();
            LbLNP.Text = texto;
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.StartPosition = FormStartPosition.Manual;
            form1.Location = this.Location;
            form1.Size = this.Size;
            form1.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textoGuardado = textBox2.Text;

            MessageBox.Show("Remédios guardados com sucesso!", "Aviso");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textoGuardado2 = textBox3.Text;

            MessageBox.Show("Doses guardadas com sucesso!", "Aviso");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textoGuardado3 = textBox4.Text;

            MessageBox.Show("Doses guardadas com sucesso!", "Aviso");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form14 form14 = new Form14(textoGuardado, textoGuardado2, textoGuardado3, LbLNP.Text);
            form14.StartPosition = FormStartPosition.Manual;
            form14.Location = this.Location;
            form14.Size = this.Size;
            form14.Show();
            this.Close();
        }

        private void LbLNP_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form12 form12 = new Form12();
            form12.StartPosition = FormStartPosition.Manual;
            form12.Location = this.Location;
            form12.Size = this.Size;
            form12.Show();
            this.Close();
        }
    }
}
