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
    public partial class Form16 : Form
    {
        private bool atualizando = false;

        public Form16()
        {
            InitializeComponent();

            // Carrega a preferência salva e liga os eventos dos checkboxes
            atualizando = true;
            checkBox1.Checked = Configuracoes.NotificacoesAtivas;
            checkBox2.Checked = !Configuracoes.NotificacoesAtivas;
            atualizando = false;

            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
        }

        // "Ativar notificações": marcar desmarca o outro; nunca fica sem nenhum marcado
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (atualizando) return;
            atualizando = true;
            checkBox1.Checked = true;      // se tentou desmarcar, mantém marcado
            checkBox2.Checked = false;
            atualizando = false;
            Configuracoes.NotificacoesAtivas = true;
        }

        // "Desativar notificações"
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (atualizando) return;
            atualizando = true;
            checkBox2.Checked = true;
            checkBox1.Checked = false;
            atualizando = false;
            Configuracoes.NotificacoesAtivas = false;
        }



        private void button2_Enter(object sender, EventArgs e)
        {
            button2.Image = Properties.Resources.botão_inicio_2;
        }

        private void button2_Leave(object sender, EventArgs e)
        {
            button2.Image = Properties.Resources.botão_inicio_normal;
        }

        private void button4_Enter(object sender, EventArgs e)
        {
            button4.Image = Properties.Resources.botão_voltar_cadastro_selecionado;
        }

        private void button4_Leave(object sender, EventArgs e)
        {
            button4.Image = Properties.Resources.botão_voltar_cadastro;
        }

        private void button3_Enter(object sender, EventArgs e)
        {
            button3.Image = Properties.Resources.botão_configurações;
        }

        private void button3_Leave(object sender, EventArgs e)
        {
            button3.Image = Properties.Resources.botão_configurações_normal;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.StartPosition = FormStartPosition.Manual;
            form1.Location = this.Location;
            form1.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
             "Você realmente deseja voltar ?",
               "Atenção!",
             MessageBoxButtons.YesNo,
              MessageBoxIcon.Warning);

            if (resultado == DialogResult.Yes)
            {
                Form1 form1 = new Form1();
                form1.StartPosition = FormStartPosition.Manual;
                form1.Location = this.Location;
                form1.Show();
                this.Close();
            }
            else if (resultado == DialogResult.No)
            {
                return;
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult resposta = MessageBox.Show(
             "Você realmente deseja sair da conta?",
             "Sair da conta",
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Question);

            if (resposta == DialogResult.Yes)
            {
                Form1 form1 = new Form1();
                form1.Show();
                this.Hide();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}