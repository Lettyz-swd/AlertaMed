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
    public partial class Form19 : Form
    {
        public Form19()
        {
            //design configurado
            InitializeComponent();
        }

        private void button1_Enter(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.botao_selecionar_selecionado;
            pictureBox1.Image = Properties.Resources.Tela_inicial_uso_pessoal_selecionar_prescrição_selecionado;
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.botao_selecionar_normal;
            pictureBox1.Image = Properties.Resources.Tela_inicial_uso_pessoal;
        }

        private void button3_Enter(object sender, EventArgs e)
        {
            button3.Image = Properties.Resources.botao_selecionar_selecionado;
            pictureBox1.Image = Properties.Resources.Tela_inicial_uso_pessoal_bt_selecionar_perfil_selecionado;
        }

        private void button3_Leave(object sender, EventArgs e)
        {
            button3.Image = Properties.Resources.botao_selecionar_normal;
            pictureBox1.Image = Properties.Resources.Tela_inicial_uso_pessoal;
        }

        private void button4_Enter(object sender, EventArgs e)
        {
            button4.Image = Properties.Resources.botão_inicio_2;
        }

        private void button4_Leave(object sender, EventArgs e)
        {
            button4.Image = Properties.Resources.botão_inicio_normal;
        }

        private void button5_Leave(object sender, EventArgs e)
        {
            button5.Image = Properties.Resources.botão_configurações_normal;
        }

        private void button5_Enter(object sender, EventArgs e)
        {
            button5.Image = Properties.Resources.botão_configurações;
        }

        private void button6_Enter(object sender, EventArgs e)
        {
            button6.Image = Properties.Resources.botão_voltar_cadastro_selecionado;
        }

        private void button6_Leave(object sender, EventArgs e)
        {
            button6.Image = Properties.Resources.botão_voltar_cadastro;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form20 form20 = new Form20();
            form20.StartPosition = FormStartPosition.Manual;
            form20.Location = this.Location;
            form20.Size = this.Size;
            form20.Show();
            this.Hide();
        }
        

        private void button7_Click(object sender, EventArgs e)
        {
            Form14 form14 = new Form14();
            form14.StartPosition = FormStartPosition.Manual;
            form14.Location = this.Location;
            form14.Size = this.Size;
            form14.Show();
            this.Hide();
        }

        private void Form19_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form23 form23 = new Form23();
            form23.StartPosition = FormStartPosition.Manual;
            form23.Location = this.Location;
            form23.Size = this.Size;
            form23.Show();
            this.Hide();
        }

        

        private void button4_Click(object sender, EventArgs e)
        {

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

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
