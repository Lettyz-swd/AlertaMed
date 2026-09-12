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
    public partial class Form6 : Form

    {

        public Form6()
        {
            InitializeComponent();
        }
        public Form6(Form anterior, Form destino)
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
                    

        private void button1_Enter(object sender, EventArgs e)
        {
            
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            
        }

        private void button2_Enter(object sender, EventArgs e)
        {
            
        }

        private void button2_Leave(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form1 form1= new Form1();
            form1.StartPosition = FormStartPosition.Manual;
            form1.Location = this.Location;
            form1.Size = this.Size;
            form1.Show();
            this.Hide();
        }

        private void button2_Click_1(object sender, EventArgs e)
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

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
               "Você realmente deseja voltar ao início?\n\nVocê sairá desta tela.",
               "Atenção!",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Warning);

            if (resultado == DialogResult.No)
            {
                return;
            }

            Form1 form1 = new Form1();

            form1.StartPosition = FormStartPosition.Manual;
            form1.Location = this.Location;
            form1.Size = this.Size;

            form1.Show();
            this.Close();
        }

        private void button2_Enter_1(object sender, EventArgs e)
        {
            button2.Image = Properties.Resources.botão_inicio_2;
        }

        private void button2_Leave_1(object sender, EventArgs e)
        {
            button2.Image = Properties.Resources.botão_inicio_normal;
        }

        private void button3_Enter(object sender, EventArgs e)
        {
            button3.Image = Properties.Resources.botão_configurações;
        }

        private void button3_Leave(object sender, EventArgs e)
        {
            button3.Image = Properties.Resources.botão_configurações_normal;
        }

        private void button4_Enter(object sender, EventArgs e)
        {
            button4.Image = Properties.Resources.botão_voltar_cadastro_selecionado;
        }

        private void button4_Leave(object sender, EventArgs e)
        {
            button4.Image = Properties.Resources.botão_voltar_cadastro;
        }

        private void button1_Enter_1(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Tela_pedido_de_entrada_bot__ok_selecionado;
            button1.Image = Properties.Resources.botão_ok_selecionado;
            pictureBox1.Image = Properties.Resources.Tela_pedido_de_entrada_bot__ok_selecionado;
        }

        private void button1_Leave_1(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Tela_pedido_de_entrada_bot__ok_normal;
            button1.Image = Properties.Resources.botão_ok_normal;
            pictureBox1.Image = Properties.Resources.Tela_pedido_de_entrada_bot__ok_normal;
        }
    }
}
