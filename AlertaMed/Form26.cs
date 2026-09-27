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
    public partial class Form26 : Form
    {
        public Form26()
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
            button1.Image = Properties.Resources.botao_concluir_selecionado;
            pictureBox1.Image = Properties.Resources.Tela_perfil_instituiçao_bt_selecionado;
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.botao_concluir_normal;
            pictureBox1.Image = Properties.Resources.Tela_perfil_instituiçao;
        }

        private void button6_Enter(object sender, EventArgs e)
        {
            button6.Image = Properties.Resources.botao_voltar_tela_perfil_selecionado1;
        }

        private void button6_Leave(object sender, EventArgs e)
        {
            button6.Image = Properties.Resources.botao_voltar_tela_perfil_normal;
        }
    }
}
