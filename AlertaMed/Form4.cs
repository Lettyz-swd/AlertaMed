using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Npgsql;
using NpgsqlTypes;



namespace AlertaMed
{
    public partial class Form4 : Form
    {
        // Textos de exemplo que aparecem dentro das caixas de texto
        private const string PH_NOME = "Digite o Nome Completo";
        private const string PH_EMAIL = "Digite o E-mail";
        private const string PH_SENHA = "Digite a Senha";

        // Vira true quando a pessoa mexe na data de nascimento
        private bool dataEscolhida = false;

        //codigo para conseguir mudar o fundo do datetime 
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);

        public Form4()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            Assembly assembly = Assembly.GetExecutingAssembly();


            button5.Image = Properties.Resources.botão_olho_;

            //personalização ativa do datetime
            SetWindowTheme(dateTimePicker1.Handle, "", ""); // libera BackColor/ForeColor
            dateTimePicker1.BackColor = ColorTranslator.FromHtml("#F9FDFE");
            dateTimePicker1.ForeColor = Color.FromArgb(50, 50, 50); // combine com a cor de texto dos outros campos

            dateTimePicker1.MinDate = new DateTime(1900, 1, 1);
            dateTimePicker1.MaxDate = DateTime.Today.AddYears(-18);
            dateTimePicker1.Value = DateTime.Today.AddYears(-18);

            // a partir daqui, qualquer mudança na data conta como "a pessoa escolheu"
            dateTimePicker1.ValueChanged += delegate { dataEscolhida = true; };
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

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool temDados =
                Valor(textBox1, PH_NOME) != "" ||
                Valor(textBox2, PH_EMAIL) != "" ||
                (textBox3.Text != "" && textBox3.Text != PH_SENHA) ||
                dataEscolhida;

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

        private void button1_Enter(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.botão_cadastrar_2_selecionado;
            pictureBox1.Image = Properties.Resources.Tela_de_continuar_o_cadastro_selecionado_2;
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.botão_cadastrar_2;
            pictureBox1.Image = Properties.Resources.Tela_de_continuar_o_cadastro_normal_2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nome = Valor(textBox1, PH_NOME);
            string email = Valor(textBox2, PH_EMAIL);
            string senha = textBox3.Text == PH_SENHA ? "" : textBox3.Text;
            DateTime nascimento = dateTimePicker1.Value.Date;

            // Nome do dono
            if (string.IsNullOrEmpty(nome))
            {
                MessageBox.Show("Digite seu nome.");
                textBox1.Focus();
                return;
            }

            if (!nome.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
            {
                MessageBox.Show("O nome não pode conter números ou símbolos.");
                textBox1.Focus();
                return;
            }

            // E-mail
            if (email == "")
            {
                MessageBox.Show("Digite seu e-mail.");
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

            // Data de nascimento (idade mínima: 18 anos)
            if (!dataEscolhida)
            {
                MessageBox.Show("Escolha sua data de nascimento.");
                dateTimePicker1.Focus();
                return;
            }

            if (nascimento > DateTime.Today.AddYears(-18))
            {
                MessageBox.Show("Idade mínima: 18 anos.");
                dateTimePicker1.Focus();
                return;
            }

            // Os dados da instituição vêm do Form2
            if (!CadastroInstituicao.Preenchido)
            {
                MessageBox.Show(
                    "Os dados da instituição não foram encontrados.\n\nVolte e preencha a tela anterior de novo.");
                return;
            }

            int idUsuario = 0;
            string nomeUsuario = nome;
            int idInstituicao = 0;
            string nomeInstituicao = CadastroInstituicao.Nome;

            // Grava tudo numa transação: ou grava dono + instituição + vínculo, ou não grava nada
            try
            {
                using (NpgsqlConnection conn = Banco.Abrir())
                using (NpgsqlTransaction tx = conn.BeginTransaction())
                {
                    // 1) O dono já tem conta de usuário com esse e-mail?
                    bool jaTemConta = false;
                    string hashExistente = null;

                    using (NpgsqlCommand cmd = new NpgsqlCommand(
                        "SELECT id_usuario, nome, senha FROM public.usuario WHERE lower(email) = lower(@email)",
                        conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@email", email);

                        using (NpgsqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                jaTemConta = true;
                                idUsuario = rd.GetInt32(0);
                                nomeUsuario = rd.GetString(1);
                                hashExistente = rd.GetString(2);
                            }
                        }
                    }

                    if (jaTemConta)
                    {
                        // Usa a conta que já existe, se a senha conferir
                        if (!Senha.Conferir(senha, hashExistente))
                        {
                            MessageBox.Show(
                                "Já existe uma conta com esse e-mail, mas a senha não confere.\n\n" +
                                "Use a senha dessa conta ou outro e-mail.");
                            textBox3.Focus();
                            return;
                        }

                        // Completa a data de nascimento se a conta ainda não tinha
                        using (NpgsqlCommand cmd = new NpgsqlCommand(
                            "UPDATE public.usuario SET data_nascimento = @dn " +
                            "WHERE id_usuario = @id AND data_nascimento IS NULL", conn, tx))
                        {
                            cmd.Parameters.Add("@dn", NpgsqlDbType.Date).Value = nascimento;
                            cmd.Parameters.AddWithValue("@id", idUsuario);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // Cria a conta do dono (senha em hash)
                        using (NpgsqlCommand cmd = new NpgsqlCommand(
                            "INSERT INTO public.usuario (nome, email, senha, data_nascimento) " +
                            "VALUES (@nome, @email, @senha, @dn) RETURNING id_usuario", conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@nome", nome);
                            cmd.Parameters.AddWithValue("@email", email);
                            cmd.Parameters.AddWithValue("@senha", Senha.Gerar(senha));
                            cmd.Parameters.Add("@dn", NpgsqlDbType.Date).Value = nascimento;
                            idUsuario = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                    }

                    // 2) Cria a instituição
                    using (NpgsqlCommand cmd = new NpgsqlCommand(
                        "INSERT INTO public.instituicao (nome, tipo, email, senha) " +
                        "VALUES (@nome, @tipo, @email, @senha) RETURNING id_instituicao", conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@nome", CadastroInstituicao.Nome);
                        cmd.Parameters.AddWithValue("@tipo", CadastroInstituicao.Tipo);
                        cmd.Parameters.AddWithValue("@email", CadastroInstituicao.Email);
                        cmd.Parameters.AddWithValue("@senha", CadastroInstituicao.SenhaHash);
                        idInstituicao = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // 3) Liga o dono à instituição
                    using (NpgsqlCommand cmd = new NpgsqlCommand(
                        "INSERT INTO public.membro_instituicao (id_instituicao, id_usuario, papel) " +
                        "VALUES (@inst, @usr, 'dono')", conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@inst", idInstituicao);
                        cmd.Parameters.AddWithValue("@usr", idUsuario);
                        cmd.ExecuteNonQuery();
                    }

                    tx.Commit();
                }
            }
            catch (PostgresException ex)
            {
                if (ex.SqlState == "23505")
                {
                    MessageBox.Show(
                        "Já existe um cadastro com esses dados (e-mail repetido).\n\n" +
                        "Volte e confira o e-mail da instituição.");
                }
                else
                {
                    MessageBox.Show("Erro ao cadastrar:\n\n" + ex.Message);
                }
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar:\n\n" + ex.Message);
                return;
            }

            // Deu certo: limpa os dados temporários e entra como o dono
            CadastroInstituicao.Limpar();
            Sessao.Entrar(idUsuario, nomeUsuario);
            Sessao.EntrarNaInstituicao(idInstituicao, nomeInstituicao);

            Form7 form7 = new Form7();
            form7.StartPosition = FormStartPosition.Manual;
            form7.Location = this.Location;
            form7.Size = this.Size;
            form7.Show();
            this.Hide();


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {



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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //configurar 
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //configurar 
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "Digite o Nome Completo")
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

        // Métodos abaixo: o Designer liga o evento "Leave" a eles.
        // Ficam vazios por enquanto - podem receber validação depois.
        private void textBox1_Leave(object sender, EventArgs e)
        {

        }

        private void textBox2_Leave(object sender, EventArgs e)
        {

        }

        private void textBox3_Leave_1(object sender, EventArgs e)
        {

        }
    }
}