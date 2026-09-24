using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace AlertaMed
{
    public partial class Form9 : Form

    {

        public Form9()
        {
            InitializeComponent();
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

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

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool temDados = false;

            if ((textBox1.Text.Trim() != "" && textBox1.Text != "Digite seu nome") ||
                (textBox2.Text.Trim() != "" && textBox2.Text != "Digite seu e-mail") ||
                (textBox3.Text.Trim() != "" && textBox3.Text != "Digite sua senha"))
            {
                temDados = true;
            }

            if (temDados)
            {
                DialogResult resultado = MessageBox.Show(
                    "Você realmente deseja voltar?\n\nOs dados preenchidos serão perdidos.",
                    "Atenção!",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (resultado == DialogResult.No)
                {
                    return;
                }
            }

            Form2 form2 = new Form2();

            form2.StartPosition = FormStartPosition.Manual;
            form2.Location = this.Location;
            form2.Size = this.Size;

            form2.Show();
            this.Close();
            
        }
        

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox3.PasswordChar == '\0')
            {
                textBox3.PasswordChar = '●';
                button6.Image = Properties.Resources.botão_olho_riscado;
            }
            else
            {
                textBox3.PasswordChar = '\0';
                button6.Image = Properties.Resources.botão_olho_;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nome = textBox1.Text.Trim();

            // Verifica se está vazio
            if (string.IsNullOrEmpty(nome))
            {
                MessageBox.Show("Digite seu nome.");
                textBox1.Focus();
                return;
            }

            // Verifica se tem números ou símbolos
            if (!nome.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
            {
                MessageBox.Show("O nome não pode conter números ou símbolos.");
                textBox1.Focus();
                return;
            }

            // Se chegou aqui, o nome é válido
            MessageBox.Show("Cadastro realizado com sucesso!");

            Form7 form7 = new Form7();

            form7.StartPosition = FormStartPosition.Manual;
            form7.Location = this.Location;
            form7.Size = this.Size;

            form7.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.StartPosition = FormStartPosition.Manual;
            form2.Location = this.Location;
            form2.Size = this.Size;

            form2.Show();
            this.Close();
        }

        private void button2_Enter(object sender, EventArgs e)
        {
            button2.Image = Properties.Resources.botão_inicio_2;
        }

        private void button2_Leave(object sender, EventArgs e)
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

        private void button6_Enter(object sender, EventArgs e)
        {

        }

        private void button6_Leave(object sender, EventArgs e)
        {

        }

        private void button1_Enter(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.botao_entrar_selecionado;
            pictureBox1.Image = Properties.Resources.Tela_entrar_na_conta_inst__bt_selecionado;
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.botao_entrar_normal;
            pictureBox1.Image = Properties.Resources.Tela_entrar_na_conta_inst_bt__normal;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text == "Digite o Nome")
            {
                textBox1.Clear();
                
            }
        }

        
        

        private void textBox2_Click_1(object sender, EventArgs e)
        {
            if (textBox2.Text == "Digite o E-mail")
            {
                textBox2.Clear();
                
            }
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "Digite a Senha")
            {
                textBox3.Clear();
                
            }
        }
    }
    }
        

        

