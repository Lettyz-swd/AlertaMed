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
    public partial class Form20 : Form
    {
        public Form20()
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

        private void button3_Enter(object sender, EventArgs e)
        {
            button3.Image = Properties.Resources.botão_configurações;
        }

        private void button3_Leave(object sender, EventArgs e)
        {
            button3.Image = Properties.Resources.botão_configurações_normal;
        }

        private void button1_Enter(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.botão_voltar_cadastro_selecionado;
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.botão_voltar_cadastro;
        }

        private void button2_Enter(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Tela_cadastro_paciente_2_bt_selecionado;
            button2.Image = Properties.Resources.botao_cadastrar_preescrição_selecionado_2;
            pictureBox1.Image = Properties.Resources.Tela_cadastro_paciente_2_bt_selecionado;
        }

        private void button2_Leave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Tela_cadastro_paciente_2_bt_normal;
            button2.Image = Properties.Resources.botao_cadastrar_preescrição_normal_2;
            pictureBox1.Image = Properties.Resources.Tela_cadastro_paciente_2_bt_normal;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
