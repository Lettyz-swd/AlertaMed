using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace AlertaMed
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GerenciadorAlarmes.Iniciar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form10 form10 = new Form10();

            form10.StartPosition = FormStartPosition.Manual;
            form10.Location = this.Location;
            form10.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.StartPosition = FormStartPosition.Manual;
            form3.Location = this.Location;
            form3.Show();
            this.Hide();
        }

        private void button2_Enter(object sender, EventArgs e)
        {
            button4.Image = Properties.Resources.botão_instituição_2;
            button2.Image = Properties.Resources.botão_uso_pessoal_selecionado;
            pictureBox1.Image = Properties.Resources.Tela_inicial_bt_uso_pessoal_clicado;
        }

        private void button2_Leave(object sender, EventArgs e)
        {
            button4.Image = Properties.Resources.botão_instituição_2;
            button2.Image = Properties.Resources.botão_uso_pessoal;
            pictureBox1.Image = Properties.Resources.AlertaMed_Design1;
        }

        private void button3_Leave(object sender, EventArgs e)
        {
            button4.Image = Properties.Resources.botão_instituição_2;
            button3.Image = Properties.Resources.botão_sobre;
            pictureBox1.Image = Properties.Resources.AlertaMed_Design1;
        }

        private void button3_Enter(object sender, EventArgs e)
        {
            button4.Image = Properties.Resources.botão_instituição_2;
            button2.Image = Properties.Resources.botão_uso_pessoal;
            button3.Image = Properties.Resources.botão_sobre_selecionado;
            pictureBox1.Image = Properties.Resources.Tela_inicial_bt_sobre_clicado;
        }

        private void button4_Enter(object sender, EventArgs e)
        {
            button2.Image = Properties.Resources.botão_uso_pessoal;
            button4.Image = Properties.Resources.botão_instituição_selecionado1;
            pictureBox1.Image = Properties.Resources.Tela_inicial_bt_instituicao_clicado;
        }

        private void button4_Leave(object sender, EventArgs e)
        {
            button2.Image = Properties.Resources.botão_uso_pessoal;
            button4.Image = Properties.Resources.botão_instituição;
            pictureBox1.Image = Properties.Resources.AlertaMed_Design1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.StartPosition = FormStartPosition.Manual;
            form5.Location = this.Location;
            form5.Show();
            this.Hide();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}