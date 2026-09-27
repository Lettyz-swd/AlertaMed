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
    public partial class Form23 : Form
    {
        public Form23()
        {
            InitializeComponent();
        }

        private void button2_Enter(object sender, EventArgs e)
        {
            button2.Image = Properties.Resources.botão_inicio_2;
        }

        private void button2_Leave(object sender, EventArgs e)
        {
            button2.Image = Properties.Resources.botão_inicio_normal;
        }

        private void button10_Enter(object sender, EventArgs e)
        {
            button10.Image = Properties.Resources.botão_configurações;
        }

        private void button10_Leave(object sender, EventArgs e)
        {
            button10.Image = Properties.Resources.botão_configurações_normal;
        }

        private void button1_Enter(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.botao_concluir_selecionado;
            pictureBox1.Image = Properties.Resources.Tela_personalizar_perfil_uso_pessoal_bt_selecionado;
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.botao_concluir_normal;
            pictureBox1.Image = Properties.Resources.Tela_personalizar_perfil_uso_pessoal_normal;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Enter(object sender, EventArgs e)
        {
            button3.Image = Properties.Resources.botao_voltar_redondo_selecionado1;
            pictureBox1.Image = Properties.Resources.Tela_personalizar_perfil_uso_pessoal_bt_voltar_selecionado;
        }

        private void button3_Leave(object sender, EventArgs e)
        {
            button3.Image = Properties.Resources.botao_voltar_redondo_normal1;
            pictureBox1.Image = Properties.Resources.Tela_personalizar_perfil_uso_pessoal_normal;
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }
    }
}
