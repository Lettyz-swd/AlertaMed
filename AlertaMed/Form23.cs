using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace AlertaMed
{
    public partial class Form23 : Form
    {
        // Textos de exemplo que aparecem dentro das caixas
        private const string PH_NOME = "Digite seu nome";
        private const string PH_GENERO = "Digite seu gênero";
        private const string PH_BIO = "Digite sua biografia";

        public Form23()
        {
            InitializeComponent();
            this.Load += Form23_Load;
        }

        // Devolve o texto digitado, ou "" se ainda estiver o texto de exemplo
        private static string Valor(Control caixa, string textoExemplo)
        {
            string t = caixa.Text.Trim();
            return t == textoExemplo ? "" : t;
        }

        // Só troca o texto de exemplo se o banco tiver valor
        private static void Preencher(TextBox caixa, string valor)
        {
            if (!string.IsNullOrWhiteSpace(valor))
                caixa.Text = valor;
        }

        // ---------- Carregamento da tela ----------

        private void Form23_Load(object sender, EventArgs e)
        {
            // Garante os textos de exemplo (o do gênero estava escrito diferente no designer)
            textBox1.Text = PH_NOME;
            textBox2.Text = PH_GENERO;
            textBox3.Text = PH_BIO;

            if (!Sessao.Logado) return;

            // Mostra pelo menos o nome da sessão, mesmo se o banco falhar
            Preencher(textBox1, Sessao.Nome);

            try
            {
                using (NpgsqlConnection conn = Banco.Abrir())
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    "SELECT nome, genero, biografia FROM public.usuario WHERE id_usuario = @u", conn))
                {
                    cmd.Parameters.AddWithValue("@u", Sessao.IdUsuario);
                    using (NpgsqlDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            Preencher(textBox1, rd.IsDBNull(0) ? null : rd.GetString(0));
                            Preencher(textBox2, rd.IsDBNull(1) ? null : rd.GetString(1));
                            Preencher(textBox3, rd.IsDBNull(2) ? null : rd.GetString(2));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível carregar seu perfil:\n\n" + ex.Message);
            }
        }

        // ---------- Efeitos visuais dos botões ----------

        private void button2_Enter(object sender, EventArgs e)
        {
            button2.Image = Properties.Resources.botão_inicio_2;
        }

        private void button2_Leave(object sender, EventArgs e)
        {
            button2.Image = Properties.Resources.botão_inicio_normal;
        }

        private void button10_Enter(object sender, EventArgs e)
        {
            button10.Image = Properties.Resources.botão_configurações;
        }

        private void button10_Leave(object sender, EventArgs e)
        {
            button10.Image = Properties.Resources.botão_configurações_normal;
        }

        private void button1_Enter(object sender, EventArgs e)
        {
           
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            
        }

        private void button3_Enter(object sender, EventArgs e)
        {
        }

        private void button3_Leave(object sender, EventArgs e)
        {
        }

        // ---------- Navegação ----------

        // Início
        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.StartPosition = FormStartPosition.Manual;
            form1.Location = this.Location;
            form1.Show();
            this.Close();
        }

        // Configurações
        private void button10_Click(object sender, EventArgs e)
        {
            new Form16(this).Show();
            this.Hide();
        }

        // Seta do canto: volta para a tela de uso pessoal (antes ia para o Form25, da instituição)
        private void button3_Click(object sender, EventArgs e)
        {
            Form19 form19 = new Form19();
            form19.StartPosition = FormStartPosition.Manual;
            form19.Location = this.Location;
            form19.Show();
            this.Close();
        }

        // ---------- Concluir: salva o perfil ----------

        private void button1_Click(object sender, EventArgs e)
        {
            string nome = Valor(textBox1, PH_NOME);
            string genero = Valor(textBox2, PH_GENERO);
            string bio = Valor(textBox3, PH_BIO);

            if (!Sessao.Logado)
            {
                MessageBox.Show("Entre na sua conta para salvar o perfil.");
                return;
            }

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

            try
            {
                using (NpgsqlConnection conn = Banco.Abrir())
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    @"UPDATE public.usuario
                      SET nome = @nome, genero = @genero, biografia = @bio
                      WHERE id_usuario = @u", conn))
                {
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@genero", genero == "" ? (object)DBNull.Value : genero);
                    cmd.Parameters.AddWithValue("@bio", bio == "" ? (object)DBNull.Value : bio);
                    cmd.Parameters.AddWithValue("@u", Sessao.IdUsuario);
                    cmd.ExecuteNonQuery();
                }

                // Atualiza o nome na sessão sem perder a instituição em que a pessoa está
                int idInst = Sessao.IdInstituicao;
                string nomeInst = Sessao.NomeInstituicao;
                Sessao.Entrar(Sessao.IdUsuario, nome);
                if (idInst > 0) Sessao.EntrarNaInstituicao(idInst, nomeInst);

                MessageBox.Show("Perfil salvo!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar o perfil:\n\n" + ex.Message);
            }
            Form19 form19 = new Form19();
            form19.StartPosition = FormStartPosition.Manual;
            form19.Location = this.Location;
            form19.Show();
            this.Close();
        }

        // ---------- Textos de exemplo (placeholders) ----------

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == PH_NOME) textBox1.Text = "";
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text)) textBox1.Text = PH_NOME;
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == PH_GENERO) textBox2.Text = "";
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text)) textBox2.Text = PH_GENERO;
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == PH_BIO) textBox3.Text = "";
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text)) textBox3.Text = PH_BIO;
        }
    }
}