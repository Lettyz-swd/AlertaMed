using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Npgsql;

namespace AlertaMed
{
    public partial class Form2 : Form
    {
        // Textos de exemplo que aparecem dentro das caixas de texto
        private const string PH_NOME = "Digite o Nome";
        private const string PH_EMAIL = "Digite o E-mail";
        private const string PH_SENHA = "Digite a Senha";

        public Form2()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // Tipo da instituição: só escolher, não digitar
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            // Se a lista estiver vazia no Designer, usa estas opções
            if (comboBox1.Items.Count == 0)
            {
                comboBox1.Items.AddRange(new object[]
                {
                    "Hospital",
                    "Clínica",
                    "Casa de repouso",
                    "Escola",
                    "Farmácia",
                    "Outro"
                });
            }
        }

        // Devolve o texto digitado, ou "" se ainda estiver o texto de exemplo
        private static string Valor(Control caixa, string textoExemplo)
        {
            string t = caixa.Text.Trim();
            return t == textoExemplo ? "" : t;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.StartPosition = FormStartPosition.Manual;
            form1.Location = this.Location;
            form1.Size = this.Size;
            form1.Show();
            this.Close();
            button2.Image = Properties.Resources.botão_inicio_3;
            button3.Image = Properties.Resources.botão_configurações_normal;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool temDados =
                Valor(textBox1, PH_NOME) != "" ||
                Valor(textBox2, PH_EMAIL) != "" ||
                (textBox3.Text != "" && textBox3.Text != PH_SENHA) ||
                comboBox1.SelectedIndex != -1;

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

            Form5 form5 = new Form5();

            form5.StartPosition = FormStartPosition.Manual;
            form5.Location = this.Location;
            form5.Size = this.Size;

            form5.Show();
            this.Close();

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nome = Valor(textBox1, PH_NOME);
            string email = Valor(textBox2, PH_EMAIL).ToLower();
            string senha = textBox3.Text == PH_SENHA ? "" : textBox3.Text;

            // Nome da instituição (pode ter números, ex.: "Farmácia 24h")
            if (nome.Length < 2)
            {
                MessageBox.Show("Digite o nome da instituição.");
                textBox1.Focus();
                return;
            }

            // Tipo da instituição
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Escolha o tipo da instituição.");
                comboBox1.Focus();
                return;
            }

            // E-mail
            if (email == "")
            {
                MessageBox.Show("Digite o e-mail da instituição.");
                textBox2.Focus();
                return;
            }

            if (!email.Contains("@") || !email.Contains(".") || email.Contains(" "))
            {
                MessageBox.Show("Digite um e-mail válido.");
                textBox2.Focus();
                return;
            }

            // Senha
            if (senha.Length < 6)
            {
                MessageBox.Show("A senha precisa ter pelo menos 6 caracteres.");
                textBox3.Focus();
                return;
            }

            // Confere agora se já existe instituição com esse e-mail
            // (assim a pessoa descobre antes de preencher os dados do dono)
            try
            {
                using (NpgsqlConnection conn = Banco.Abrir())
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    "SELECT 1 FROM public.instituicao WHERE lower(email) = @email", conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);

                    if (cmd.ExecuteScalar() != null)
                    {
                        MessageBox.Show("Já existe uma instituição cadastrada com esse e-mail.");
                        textBox2.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível verificar o e-mail:\n\n" + ex.Message);
                return;
            }

            // Guarda os dados para o Form4 gravar quando o dono se cadastrar
            CadastroInstituicao.Nome = nome;
            CadastroInstituicao.Tipo = comboBox1.Text.Trim();
            CadastroInstituicao.Email = email;
            CadastroInstituicao.SenhaHash = Senha.Gerar(senha);

            Form4 form4 = new Form4();
            form4.StartPosition = FormStartPosition.Manual;
            form4.Location = this.Location;
            form4.Size = this.Size;
            form4.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Enter(object sender, EventArgs e)
        {
            button4.Image = Properties.Resources.botão_voltar_cadastro_selecionado;
        }

        private void button4_Leave(object sender, EventArgs e)
        {
            button4.Image = Properties.Resources.botão_voltar_cadastro;
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //configurar 
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //configurar 
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (textBox3.PasswordChar == '\0')
            {
                textBox3.PasswordChar = '●';
                button5.Image = Properties.Resources.botão_olho_riscado;
            }
            else
            {
                textBox3.PasswordChar = '\0';
                button5.Image = Properties.Resources.botão_olho_;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form9 form9 = new Form9();
            form9.StartPosition = FormStartPosition.Manual;
            form9.Location = this.Location;
            form9.Size = this.Size;
            form9.Show();
            this.Hide();
        }

        private void button7_MouseEnter(object sender, EventArgs e)
        {

        }

        private void button7_Enter(object sender, EventArgs e)
        {

        }

        private void button7_Leave(object sender, EventArgs e)
        {

        }

        private void button1_Enter(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.botão_continuar_selecionado;
            pictureBox1.Image = Properties.Resources.Tela_botão_continuar_selecionado;

        }

        private void button1_Leave(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.botão_continuar_normal;
            pictureBox1.Image = Properties.Resources.Tela_botão_continuar_normal;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //configurar 
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "Digite o Nome")
            {
                textBox1.Clear();

            }
        }

        private void textBox2_Click(object sender, EventArgs e)
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
<<<<<<< HEAD

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = "Digite o Nome";
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                textBox2.Text = "Digite o E-mail";
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                textBox3.Text = "Digite a Senha";
            }
        }

        private void textBox3_Leave_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                textBox3.Text = "Digite a Senha";
            }
        }
        }
    }
=======
    }
}
>>>>>>> 89ea95c1bff83eaaaf44d8b7db40a0afe5812bfa
