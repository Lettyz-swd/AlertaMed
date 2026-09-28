using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlertaMed
{
    public partial class Form25 : Form
    {
        public Form25()
        {
            InitializeComponent();
        }

        private void button4_Enter(object sender, EventArgs e)
        {
            button4.Image = Properties.Resources.botão_inicio_2;
        }

        private void button4_Leave(object sender, EventArgs e)
        {
            button4.Image = Properties.Resources.botão_inicio_normal;
        }

        private void button5_Enter(object sender, EventArgs e)
        {
            button5.Image = Properties.Resources.botão_configurações;
        }

        private void button5_Leave(object sender, EventArgs e)
        {
            button5.Image = Properties.Resources.botão_configurações_normal;
        }

        private void button1_Enter(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.botao_selecionar_selecionado;
            pictureBox1.Image = Properties.Resources.Tela_inicial_instituiçao_bt_perfil_selecionar;
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.botao_selecionar_normal;
            pictureBox1.Image = Properties.Resources.Tela_inicial_instituiçao_nova;
        }

        private void button3_Enter(object sender, EventArgs e)
        {
            button3.Image = Properties.Resources.botao_selecionar_selecionado;
            pictureBox1.Image = Properties.Resources.Tela_inicial_instituiçao_bt_instituiçao_selecionar1;
        }

        private void button3_Leave(object sender, EventArgs e)
        {
            button3.Image = Properties.Resources.botao_selecionar_normal;
            pictureBox1.Image = Properties.Resources.Tela_inicial_instituiçao_nova;
        }

        private void button6_Enter(object sender, EventArgs e)
        {
            button6.Image = Properties.Resources.botao_voltar_tela_perfil_selecionado1;
        }

        private void button6_Leave(object sender, EventArgs e)
        {
            button6.Image = Properties.Resources.botao_voltar_tela_perfil_normal;
           
        }

        private void button4_Click(object sender, EventArgs e)
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
            Form26 form26 = new Form26();
            form26.StartPosition = FormStartPosition.Manual;
            form26.Location = this.Location;
            form26.Size = this.Size;
            form26.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form24 form24 = new Form24();
            form24.StartPosition = FormStartPosition.Manual;
            form24.Location = this.Location;
            form24.Size = this.Size;
            form24.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form16 form16 = new Form16();
            form16.StartPosition = FormStartPosition.Manual;
            form16.Location = this.Location;
            form16.Size = this.Size;
            form16.Show();
            this.Close();
        }
    }
}
