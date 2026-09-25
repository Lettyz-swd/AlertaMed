using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AlertaMed
{
    public partial class Form15 : Form
    {
        public Form15()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form14 form14 = new Form14();
            form14.StartPosition = FormStartPosition.Manual;
            form14.Location = this.Location;
            form14.Size = this.Size;
            form14.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //começar a cadastrar prescrição
            Form12 form12 = new Form12();
            form12.StartPosition = FormStartPosition.Manual;
            form12.Location = this.Location;
            form12.Size = this.Size;
            form12.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //inicio
            Form1 form1 = new Form1();
            form1.StartPosition = FormStartPosition.Manual;
            form1.Location = this.Location;
            form1.Size = this.Size;
            form1.Show();
            this.Hide();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //voltar
            Form7 form7 = new Form7();
            form7.StartPosition = FormStartPosition.Manual;
            form7.Location = this.Location;
            form7.Size = this.Size;
            form7.Show();
            this.Hide();
        }
    }
}
