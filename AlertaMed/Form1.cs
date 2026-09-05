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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.StartPosition = FormStartPosition.Manual;
            form3.Location = this.Location;
            form3.Size = this.Size;
            form3.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Enter(object sender, EventArgs e)
        {
            
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Enter(object sender, EventArgs e)
        {
            button4.Image = Properties.Resources.botão_instituição_2;
            button2.Image = Properties.Resources.botão_uso_pessoal_selecionado;
            pictureBox1.Image = Properties.Resources.tela_inicio_nova_botão_uso_pessoal_selecionado;
        }

        private void button2_Leave(object sender, EventArgs e)
        {
            button4.Image = Properties.Resources.botão_instituição_2;
            button2.Image = Properties.Resources.botão_uso_pessoal;
            pictureBox1.Image = Properties.Resources.Tela_inicio_3;
        }

        private void button3_Leave(object sender, EventArgs e)
        {
            button4.Image = Properties.Resources.botão_instituição_2;
            button3.Image = Properties.Resources.botão_sobre;
            pictureBox1.Image = Properties.Resources.Tela_inicio_3;
        }

        private void button3_Enter(object sender, EventArgs e)
        {
            button4.Image = Properties.Resources.botão_instituição_2;
            button2.Image = Properties.Resources.botão_uso_pessoal;
            button3.Image = Properties.Resources.botão_sobre_selecionado;
            pictureBox1.Image = Properties.Resources.tela_inicio_nova_botão_sobre_selecionado;
        }

        private void button4_Enter(object sender, EventArgs e)
        {
            button2.Image = Properties.Resources.botão_uso_pessoal;
            button4.Image = Properties.Resources.botão_instituição_selecionado1;
            pictureBox1.Image = Properties.Resources.tela_inicio_nova_botão_inst_selecionado;
        }

        private void button4_Leave(object sender, EventArgs e)
        {
            button2.Image = Properties.Resources.botão_uso_pessoal;
            button4.Image = Properties.Resources.botão_instituição;
            pictureBox1.Image = Properties.Resources.Tela_inicio_3;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.StartPosition = FormStartPosition.Manual;
            form5.Location = this.Location;
            form5.Size = this.Size;
            form5.Show();
            this.Hide();

        }

        private void button1_Enter_1(object sender, EventArgs e)
        {
            
        }

        private void button1_Leave_1(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Enter_2(object sender, EventArgs e)
        {
            button3.Image = Properties.Resources.botão_sobre;
            button4.Image = Properties.Resources.botão_instituição_2;
            button2.Image = Properties.Resources.botão_uso_pessoal;
            button1.Image = Properties.Resources.botão_uso_profissional_selecionado;
            pictureBox1.Image = Properties.Resources.tela_inicio_nova_botão_profissional_selecionado;
        }

        private void button1_Leave_2(object sender, EventArgs e)
        {
            button3.Image = Properties.Resources.botão_sobre;
            button4.Image = Properties.Resources.botão_instituição_2;
            button2.Image = Properties.Resources.botão_uso_pessoal;
            button1.Image = Properties.Resources.botão_uso_profissional_;
            pictureBox1.Image = Properties.Resources.Tela_inicio_3;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
