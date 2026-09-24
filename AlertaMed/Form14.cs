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
    public partial class Form14 : Form
    {
        public string textoGuardado { get; private set; } = "";

        // Construtor padrão (usado pelo Designer)
        public Form14()
        {
            InitializeComponent();
        }

        // Novo construtor que recebe os valores do Form13
        public Form14(string remedios, string doses1, string doses2, string nomePaciente)
        {
            InitializeComponent();

            // Atribua cada string recebida ao TextBox correto no Form14
            textBox1.Text = nomePaciente;
            textBox2.Text = remedios;
            textBox3.Text = doses1;
            textBox4.Text = doses2;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.StartPosition = FormStartPosition.Manual;
            form1.Location = this.Location;
            form1.Size = this.Size;
            form1.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textoGuardado = textBox1.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form13 form13 = new Form13();
            form13.StartPosition = FormStartPosition.Manual;
            form13.Location = this.Location;
            form13.Size = this.Size;
            form13.Show();
            this.Hide();
        }
    }
}