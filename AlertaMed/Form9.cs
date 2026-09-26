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
using Npgsql;

namespace AlertaMed
{
    public partial class Form9 : Form

    {
        // Textos de exemplo que aparecem dentro das caixas de texto
        private const string PH_NOME = "Digite o Nome";
        private const string PH_EMAIL = "Digite o E-mail";
        private const string PH_SENHA = "Digite a Senha";

        public Form9()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

        }

        // Devolve o texto digitado, ou "" se ainda estiver o texto de exemplo
        private static string Valor(Control caixa, string textoExemplo)
        {
            string t = caixa.Text.Trim();
            return t == textoExemplo ? "" : t;
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
            bool temDados =
                Valor(textBox1, PH_NOME) != "" ||
                Valor(textBox2, PH_EMAIL) != "" ||
                (textBox3.Text != "" && textBox3.Text != PH_SENHA);

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
            string nome = Valor(textBox1, PH_NOME);
            string email = Valor(textBox2, PH_EMAIL);
            string senha = textBox3.Text == PH_SENHA ? "" : textBox3.Text;

            // Campos obrigatórios
            if (nome == "")
            {
                MessageBox.Show("Digite o nome da instituição.");
                textBox1.Focus();
                return;
            }

            if (email == "")
            {
                MessageBox.Show("Digite o e-mail da instituição.");
                textBox2.Focus();
                return;
            }

            if (senha == "")
            {
                MessageBox.Show("Digite a senha.");
                textBox3.Focus();
                return;
            }

            int idInstituicao = 0;
            string nomeInstituicao = null;
            string hashSenha = null;
            int idDono = 0;
            string nomeDono = null;
            bool achou = false;

            try
            {
                using (NpgsqlConnection conn = Banco.Abrir())
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    @"SELECT i.id_instituicao, i.nome, i.senha, u.id_usuario, u.nome
                      FROM public.instituicao i
                      LEFT JOIN public.membro_instituicao m
                             ON m.id_instituicao = i.id_instituicao AND m.papel = 'dono'
                      LEFT JOIN public.usuario u
                             ON u.id_usuario = m.id_usuario
                      WHERE lower(i.email) = lower(@email)
                        AND lower(i.nome) = lower(@nome)", conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@nome", nome);

                    using (NpgsqlDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            achou = true;
                            idInstituicao = rd.GetInt32(0);
                            nomeInstituicao = rd.GetString(1);
                            hashSenha = rd.GetString(2);

                            if (!rd.IsDBNull(3))
                            {
                                idDono = rd.GetInt32(3);
                                nomeDono = rd.GetString(4);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao entrar:\n\n" + ex.Message);
                return;
            }

            // Confere a senha (se o hash guardado for inválido, conta como senha errada)
            bool senhaOk = false;

            if (achou)
            {
                try
                {
                    senhaOk = Senha.Conferir(senha, hashSenha);
                }
                catch (Exception)
                {
                    senhaOk = false;
                }
            }

            // Mesma mensagem para qualquer erro, para não revelar o que existe no banco
            if (!achou || !senhaOk)
            {
                MessageBox.Show("Instituição, e-mail ou senha incorretos.");
                textBox3.Focus();
                return;
            }

            // Entrou: guarda na sessão (o dono da instituição e a própria instituição)
            Sessao.Sair();

            if (idDono > 0)
            {
                Sessao.Entrar(idDono, nomeDono);
            }

            Sessao.EntrarNaInstituicao(idInstituicao, nomeInstituicao);

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
    }
}