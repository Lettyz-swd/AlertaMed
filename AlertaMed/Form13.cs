using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Forms;

namespace AlertaMed
{
    public partial class Form13 : Form
    {
        private string textoGuardado = "";
        private string textoGuardado2 = "";

        private string textoGuardado3 = "";
        public Form13()
        {
            InitializeComponent();
        }
        public Form13(string texto)
        {
            InitializeComponent();
            
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //inicio
            Form1 form1 = new Form1();
            form1.StartPosition = FormStartPosition.Manual;
            form1.Location = this.Location;
            form1.Size = this.Size;
            form1.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //add remedios
            textoGuardado = textBox2.Text;

            MessageBox.Show("Remédios guardados com sucesso!", "Aviso");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //add doses
            textoGuardado2 = textBox3.Text;

            MessageBox.Show("Doses guardadas com sucesso!", "Aviso");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //add horario
            textoGuardado3 = textBox4.Text;

            MessageBox.Show("Horários salvos com sucesso!", "Aviso");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //cadastrar paciente
            Form14 form14 = new Form14();
            form14.StartPosition = FormStartPosition.Manual;
            form14.Location = this.Location;
            form14.Size = this.Size;
            form14.Show();
            this.Close();
        }

        private void LbLNP_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            //voltar
            Form12 form12 = new Form12();
            form12.StartPosition = FormStartPosition.Manual;
            form12.Location = this.Location;
            form12.Size = this.Size;
            form12.Show();
            this.Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
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

        private void button6_Enter(object sender, EventArgs e)
        {
            button6.Image = Properties.Resources.botão_voltar_cadastro_selecionado;
        }

        private void button6_Leave(object sender, EventArgs e)
        {
            button6.Image = Properties.Resources.botão_voltar_cadastro;
        }

        private void button10_Enter(object sender, EventArgs e)
        {
            button10.Image = Properties.Resources.botão_configurações;
        }

        private void button10_Leave(object sender, EventArgs e)
        {
            button10.Image = Properties.Resources.botão_configurações_normal;
        }

        private void button3_Enter(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_preescrição_bt_remedios_selecionado;
            button3.Image = Properties.Resources.botao_adicionar_remedios_selecionado;
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_preescrição_bt_remedios_selecionado;
        }

        private void button3_Leave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_prescrição_normal;
            button3.Image = Properties.Resources.botao_remedios_normal;
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_prescrição_normal;
        }

        private void button4_Enter(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_prescrição_bt_doses_selecionado;
            button4.Image = Properties.Resources.botao_adicionar_doses_selecionado;
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_prescrição_bt_doses_selecionado;
        }

        private void button4_Leave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_prescrição_normal;
            button4.Image = Properties.Resources.botao_adicionar_doses_normal;
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_prescrição_normal;
        }

        private void button5_Enter(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_prescrição_bt_horarios_selecionado;
            button5.Image = Properties.Resources.botao_adicionar_horarios_selecionado;
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_prescrição_bt_horarios_selecionado;
        }

        private void button5_Leave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_prescrição_normal;
            button5.Image = Properties.Resources.botao_adicionar_horarios_normal;
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_prescrição_normal;
        }

        private void button2_Enter(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_preescrição_bt_cadastrar_selecionado;
            button2.Image = Properties.Resources.botao_cadastrar_preescrição_selecionado_2;
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_preescrição_bt_cadastrar_selecionado;
        }

        private void button2_Leave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_prescrição_normal;
            button2.Image = Properties.Resources.botao_cadastrar_preescrição_normal_2;
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_prescrição_normal;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "Digite o nome do paciente")
            {
                textBox2.Clear();
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                textBox2.Text = "Digite o nome do paciente";
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "Digite o nome do Técnico Responsável")
            {
                textBox1.Clear();
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {

        }

        private void textBox1_Leave_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = "Digite o nome do Técnico Responsável";
            }
        }
        }
    }
