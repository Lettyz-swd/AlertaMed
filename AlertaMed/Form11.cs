using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Npgsql;

namespace AlertaMed
{
    public partial class Form11 : Form
    {
        // Textos de exemplo que ficam dentro dos campos (propriedade Text no designer).
        // O login trata esses textos como campo vazio.
        private const string PH_EMAIL = "Digite seu E-mail";
        private const string PH_SENHA = "Digite a Senha";

        public Form11()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private bool Vazio(string texto, string placeholder)
        {
            return string.IsNullOrWhiteSpace(texto) || texto == placeholder;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form10 form10 = new Form10();

            form10.StartPosition = FormStartPosition.Manual;
            form10.Location = this.Location;
            form10.Size = this.Size;

            form10.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();

            form1.StartPosition = FormStartPosition.Manual;
            form1.Location = this.Location;
            form1.Size = this.Size;

            form1.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool temDados = !Vazio(txtEmail.Text, PH_EMAIL)
                         || !Vazio(txtSenha.Text, PH_SENHA);

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

            Form1 form1 = new Form1();
            form1.StartPosition = FormStartPosition.Manual;
            form1.Location = this.Location;
            form1.Size = this.Size;
            form1.Show();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (txtSenha.PasswordChar == '\0')
            {
                txtSenha.PasswordChar = '●';
                button6.Image = Properties.Resources.botão_olho_riscado;
            }
            else
            {
                txtSenha.PasswordChar = '\0';
                button6.Image = Properties.Resources.botão_olho_;
            }
        }

        // O designer chama este método pelo nome button1_Enter
        private void button1_Enter(object sender, EventArgs e)
        {
            btnEntrar.Image = Properties.Resources.botao_entrar_selecionado;
            pictureBox1.Image = Properties.Resources.Tela_entrar_usuario_botao_entrar_selecionado;
        }

        // O designer chama este método pelo nome button1_Leave
        private void button1_Leave(object sender, EventArgs e)
        {
            btnEntrar.Image = Properties.Resources.botao_entrar_normal;
            pictureBox1.Image = Properties.Resources.Tela_entrar_usuario_uso_pessoal;
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

        // O designer chama este método pelo nome button1_Click
        private void button1_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string senha = txtSenha.Text;

            if (Vazio(email, PH_EMAIL) || Vazio(senha, PH_SENHA))
            {
                MessageBox.Show("Preencha o e-mail e a senha.");
                return;
            }

            bool entrou = false;

            try
            {
                using (var conn = Banco.Abrir())
                {
                    // Busca o id, o nome e a senha guardada (embaralhada) do usuário
                    string sql = "SELECT id_usuario, nome, senha FROM usuario WHERE email = @email";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("email", email);

                        using (var leitor = cmd.ExecuteReader())
                        {
                            if (leitor.Read() && Senha.Conferir(senha, leitor.GetString(2)))
                            {
                                // Guarda quem entrou para as outras telas usarem
                                Sessao.Entrar(leitor.GetInt32(0), leitor.GetString(1));
                                entrou = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            if (!entrou)
            {
                MessageBox.Show("E-mail ou senha incorretos.");
                return;
            }

            MessageBox.Show("Bem-vindo(a), " + Sessao.Nome + "!");

            // Quando a tela de medicações existir, abra ela aqui
            // (troque FormMedicacoes pelo nome real do formulário):
            //
            // FormMedicacoes tela = new FormMedicacoes();
            // tela.StartPosition = FormStartPosition.Manual;
            // tela.Location = this.Location;
            // tela.Size = this.Size;
            // tela.Show();
            // this.Close();
        }

        // Métodos abaixo: o Designer liga eventos das caixas txtEmail e
        // txtSenha a eles. O "Click" segue o mesmo padrão das outras
        // telas: apaga o texto de exemplo ao clicar.
        private void txtEmail_Click_1(object sender, EventArgs e)
        {
            if (txtEmail.Text == PH_EMAIL)
            {
                txtEmail.Clear();
            }
        }

        private void txtEmail_Leave_1(object sender, EventArgs e)
        {

        }

        private void txtSenha_Click_1(object sender, EventArgs e)
        {
            if (txtSenha.Text == PH_SENHA)
            {
                txtSenha.Clear();
            }
        }

        private void txtSenha_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSenha_Leave_1(object sender, EventArgs e)
        {

        }
    }
}