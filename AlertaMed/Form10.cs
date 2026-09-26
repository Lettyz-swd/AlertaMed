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
    public partial class Form10 : Form
    {
        // Textos de exemplo que ficam dentro dos campos (propriedade Text no designer).
        // O cadastro trata esses textos como campo vazio.
        private const string PH_NOME = "Digite o Nome Completo";
        private const string PH_EMAIL = "Digite seu E-mail";
        private const string PH_SENHA = "Digite a Senha";

        public Form10()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private bool Vazio(string texto, string placeholder)
        {
            return string.IsNullOrWhiteSpace(texto) || texto == placeholder;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

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

        // O designer chama este método pelo nome button1_Enter
        private void button1_Enter(object sender, EventArgs e)
        {
            btnCadastra.Image = Properties.Resources.botão_cadastrar_2_selecionado;
            pictureBox1.Image = Properties.Resources.Tela_cadastro_usuario_botao_cad__selecionado;
        }

        // O designer chama este método pelo nome button1_Leave
        private void button1_Leave(object sender, EventArgs e)
        {
            btnCadastra.Image = Properties.Resources.botão_cadastrar_2;
            pictureBox1.Image = Properties.Resources.Tela_cadastro_usuario;
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool temDados = !Vazio(txtNome.Text, PH_NOME)
                         || !Vazio(txtEmail.Text, PH_EMAIL)
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

        private void button7_Click(object sender, EventArgs e)
        {
            Form11 form11 = new Form11();

            form11.StartPosition = FormStartPosition.Manual;
            form11.Location = this.Location;
            form11.Size = this.Size;

            form11.Show();
            this.Close();
        }

        // O designer chama este método pelo nome btnCadastra_Click
        private void btnCadastra_Click(object sender, EventArgs e)
        {
            string nome = txtNome.Text.Trim();
            string email = txtEmail.Text.Trim();
            string senha = txtSenha.Text;

            if (Vazio(nome, PH_NOME) || Vazio(email, PH_EMAIL) || Vazio(senha, PH_SENHA))
            {
                MessageBox.Show("Preencha todos os campos.");
                return;
            }

            try
            {
                using (var conn = Banco.Abrir())
                {
                    string sql = "INSERT INTO usuario (nome, email, senha) VALUES (@nome, @email, @senha)";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("nome", nome);
                        cmd.Parameters.AddWithValue("email", email);
                        cmd.Parameters.AddWithValue("senha", Senha.Gerar(senha));
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Cadastro realizado!");
            }
            catch (PostgresException ex) when (ex.SqlState == "23505")
            {
                MessageBox.Show("Este e-mail já está cadastrado.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
<<<<<<< HEAD

        private void txtNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNome_Click(object sender, EventArgs e)
        {
            if (txtNome.Text == "Digite o Nome Completo")
            {
                txtNome.Clear();
            }
        }

        private void txtEmail_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text == "Digite seu E-mail")
            {
                txtEmail.Clear();
            }
        }

        private void txtSenha_Click(object sender, EventArgs e)
        {
            if (txtSenha.Text == "Digite a Senha")
            {
                txtSenha.Clear();
            }
        }

        private void txtNome_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                txtNome.Text = "Digite o Nome Completo";
            }
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                txtEmail.Text = "Digite seu E-mail";
            }
        }

        private void txtSenha_Leave(object sender, EventArgs e)
        {
            
        }

        private void txtSenha_Leave_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                txtSenha.Text = "Digite a Senha";
            }
        }
=======
>>>>>>> 89ea95c1bff83eaaaf44d8b7db40a0afe5812bfa
    }
}