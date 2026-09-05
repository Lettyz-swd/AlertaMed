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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            button4.Image = Properties.Resources.botão_criar_uma_conta_;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.StartPosition = FormStartPosition.Manual;
            form2.Location = this.Location;
            form2.Size = this.Size;
            form2.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8();
            form8.StartPosition = FormStartPosition.Manual;
            form8.Location = this.Location;
            form8.Size = this.Size;
            form8.Show();
            this.Hide();
        }

        private void button4_Enter(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Tela_inicio_v2_botão_criar_selecionado;
            button4.Image = Properties.Resources.botão_criar_uma_conta_selecionado;
            pictureBox1.Image = Properties.Resources.Tela_inicio_v2_botão_criar_selecionado;

        }

        private void button4_Leave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Tela_de_inicio_v_2;
            button4.Image = Properties.Resources.botão_criar_uma_conta_;
            pictureBox1.Image = Properties.Resources.Tela_de_inicio_v_2;

        }

        private void button1_Enter(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.botão_entrar_em_uma_instituição_selecionado;
            pictureBox1.Image = Properties.Resources.Tela_inicio_v2_botão_entrar_selecionado;
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.botão_entrar_em_uma_instituição;
            pictureBox1.Image = Properties.Resources.Tela_de_inicio_v_2;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.StartPosition = FormStartPosition.Manual;
            form1.Location = this.Location;
            form1.Size = this.Size;
            form1.Show();
            this.Close();
        }

        private void button2_Enter(object sender, EventArgs e)
        {
            button2.Image = Properties.Resources.botão_voltar_selecionado_na_tela_opc_de_inst_;
          
        }

        private void button2_Leave(object sender, EventArgs e)
        {
            button2.Image = Properties.Resources.botão_voltar_normal_na_tela_opc_de_inst;
        }
    }
}
