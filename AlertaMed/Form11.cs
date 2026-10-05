using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AlertaMed
{
    public partial class Form11 : Form
    {
      
        private const string PH_EMAIL = "Digite seu E-mail";
        private const string PH_SENHA = "Digite a Senha";

        public Form11()
        {
            InitializeComponent();

           
            txtSenha.PasswordChar = '\0';
            button6.Image = Properties.Resources.botão_olho_;
        }

        private bool Vazio(string texto, string placeholder)
        {
            return string.IsNullOrWhiteSpace(texto) || texto == placeholder;
        }

      
        private void AtivarCampo(System.Windows.Forms.TextBox campo, string placeholder)
        {
            if (campo.Text == placeholder)
            {
                campo.Text = "";
                campo.ForeColor = Color.Black;
            }
        }

        private void DesativarCampo(System.Windows.Forms.TextBox campo, string placeholder)
        {
            if (string.IsNullOrWhiteSpace(campo.Text))
            {
                campo.Text = placeholder;
                campo.ForeColor = Color.Gray;
            }
        }

        private void txtEmail_Enter(object sender, EventArgs e)
        {
            AtivarCampo(txtEmail, PH_EMAIL);
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            DesativarCampo(txtEmail, PH_EMAIL);
        }

        private void txtSenha_Enter(object sender, EventArgs e)
        {
            if (txtSenha.Text == PH_SENHA)
            {
                txtSenha.Text = "";
                txtSenha.ForeColor = Color.Black;
            }
            txtSenha.PasswordChar = '●';
            button6.Image = Properties.Resources.botão_olho_riscado;
        }

        private void txtSenha_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                txtSenha.PasswordChar = '\0';
                txtSenha.ForeColor = Color.Gray;
                txtSenha.Text = PH_SENHA;
                button6.Image = Properties.Resources.botão_olho_;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (txtSenha.Text == PH_SENHA)
                return; 

            bool estaMascarado = txtSenha.PasswordChar != '\0';

            txtSenha.PasswordChar = estaMascarado ? '\0' : '●';
            button6.Image = estaMascarado
                ? Properties.Resources.botão_olho_
                : Properties.Resources.botão_olho_riscado;
        }

        private void txtSenha_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form10 form10 = new Form10();
            form10.StartPosition = FormStartPosition.Manual;
            form10.Location = this.Location;
            form10.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.StartPosition = FormStartPosition.Manual;
            form1.Location = this.Location;
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
            form1.Show();
            this.Close();
        }

        private void button1_Enter(object sender, EventArgs e)
        {
            btnEntrar.Image = Properties.Resources.botao_entrar_selecionado;
            pictureBox1.Image = Properties.Resources.Tela_entrar_usuario_botao_entrar_selecionado;
        }

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
                  
                    string sql = "SELECT id_usuario, nome, senha FROM usuario WHERE email = @email";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("email", email);

                        using (var leitor = cmd.ExecuteReader())
                        {
                            if (leitor.Read() && Senha.Conferir(senha, leitor.GetString(2)))
                            {
                              
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

            Form19 form19 = new Form19();
            form19.StartPosition = FormStartPosition.Manual;
            form19.Location = this.Location;
            form19.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Form16(this).Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form11_Load(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtSenha_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
    }
}