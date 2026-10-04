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
    public partial class Form14 : Form
    {
        public string textoGuardado { get; private set; } = "";

        // tela de onde o usuário veio (para o botão Voltar)
        private readonly Form _telaAnterior;

        // Construtor padrão (usado pelo Designer)
        public Form14()
        {
            //design configurado
            InitializeComponent();
        }

        // Use este nos outros forms: new Form14(this)
        public Form14(Form telaAnterior) : this()
        {
            _telaAnterior = telaAnterior;
        }

        // Construtor que recebe os valores do Form13 (e, opcionalmente, a tela anterior)
        public Form14(string remedios, string doses1, string doses2, string nomePaciente, Form telaAnterior = null)
            : this()
        {
            _telaAnterior = telaAnterior;

            // Atribua cada string recebida ao TextBox correto no Form14
            textBox1.Text = nomePaciente;
            textBox2.Text = remedios;
            textBox3.Text = doses1;
            textBox4.Text = doses2;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //inicio
            Form1 form1 = new Form1();
            form1.StartPosition = FormStartPosition.Manual;
            form1.Location = this.Location;
            form1.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button1_Enter(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.botão_inicio_2;
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.botão_inicio_normal;
        }

        private void button10_Enter(object sender, EventArgs e)
        {
            button10.Image = Properties.Resources.botão_configurações;
        }

        private void button10_Leave(object sender, EventArgs e)
        {
            button10.Image = Properties.Resources.botão_configurações_normal;
        }

        private void button6_Enter(object sender, EventArgs e)
        {
            button6.Image = Properties.Resources.botão_voltar_cadastro_selecionado;
        }

        private void button6_Leave(object sender, EventArgs e)
        {
            button6.Image = Properties.Resources.botão_voltar_cadastro;
        }

        private void button2_Enter(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Tela_historico_prescrição_bt_ok_selecionado_1;
            button2.Image = Properties.Resources.botao_ok_voltar_a_tela_selecionado;
            pictureBox1.Image = Properties.Resources.Tela_historico_prescrição_bt_ok_selecionado_1;
        }

        private void button2_Leave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Tela_historico_prescrição_normal_1;
            button2.Image = Properties.Resources.botão_ok_voltar_a_tela_normal;
            pictureBox1.Image = Properties.Resources.Tela_historico_prescrição_normal_1;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //inicio
            Form1 form1 = new Form1();
            form1.StartPosition = FormStartPosition.Manual;
            form1.Location = this.Location;
            form1.Show();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //botao voltar
            // volta para quem abriu o Form14 (Form13, Form19...); se ninguém informou, usa o Form19
            Form destino = _telaAnterior ?? new Form19();

            destino.StartPosition = FormStartPosition.Manual;
            destino.Location = this.Location;
            destino.Show();
            this.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form22 form22 = new Form22();
            form22.StartPosition = FormStartPosition.Manual;
            form22.Location = this.Location;
            form22.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            new Form16(this).Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}