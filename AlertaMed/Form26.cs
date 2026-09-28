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
    public partial class Form26 : Form
    {
        // Textos de exemplo que aparecem dentro das caixas
        private const string PH_TIPO = "Ex: Hospital, Casa de Repouso";
        private const string PH_NOME = "Nome da instituição";
        private const string PH_LOCAL = "Ex: Bairro, Rua";
        private const string PH_BIO = "Ex: Vagas Abertas";
        private const string PH_EMAIL = "Digite o e-mail";

        // Instituição que está sendo editada
        private int _idInstituicao;

        public Form26()
        {
            InitializeComponent();
        }

        // Devolve o texto digitado, ou "" se ainda estiver o texto de exemplo
        private static string Valor(Control caixa, string textoExemplo)
        {
            string t = caixa.Text.Trim();
            return t == textoExemplo ? "" : t;
        }

        // ---------- Carregamento da tela ----------

        private void Form26_Load(object sender, EventArgs e)
        {
            try
            {
                // Só o dono fica nessa tela
                if (!Sessao.EhDono())
                {
                    MessageBox.Show("Apenas o dono da instituição pode editar as informações dela.",
                                    "Acesso restrito", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    VoltarParaPerfis();
                    return;
                }

                _idInstituicao = ObterIdInstituicao();
                if (_idInstituicao == 0)
                {
                    MessageBox.Show("Não encontramos a sua instituição.");
                    VoltarParaPerfis();
                    return;
                }

                using (NpgsqlConnection conn = Banco.Abrir())
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    @"SELECT nome, tipo, email, localizacao, biografia
                      FROM public.instituicao WHERE id_instituicao = @i", conn))
                {
                    cmd.Parameters.AddWithValue("@i", _idInstituicao);
                    using (NpgsqlDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            Preencher(textBox2, rd.IsDBNull(0) ? null : rd.GetString(0)); // nome
                            Preencher(textBox1, rd.IsDBNull(1) ? null : rd.GetString(1)); // tipo
                            Preencher(textBox5, rd.IsDBNull(2) ? null : rd.GetString(2)); // e-mail
                            Preencher(textBox3, rd.IsDBNull(3) ? null : rd.GetString(3)); // localização
                            Preencher(textBox4, rd.IsDBNull(4) ? null : rd.GetString(4)); // biografia
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível carregar a instituição:\n\n" + ex.Message);
            }
        }

        // Instituição ativa na sessão; se não houver, a de que a pessoa é dona
        private int ObterIdInstituicao()
        {
            if (Sessao.EmInstituicao) return Sessao.IdInstituicao;

            using (NpgsqlConnection conn = Banco.Abrir())
            using (NpgsqlCommand cmd = new NpgsqlCommand(
                @"SELECT id_instituicao FROM public.membro_instituicao
                  WHERE id_usuario = @u AND papel = 'dono' LIMIT 1", conn))
            {
                cmd.Parameters.AddWithValue("@u", Sessao.IdUsuario);
                object r = cmd.ExecuteScalar();
                return r == null ? 0 : Convert.ToInt32(r);
            }
        }

        // Só troca o texto de exemplo se o banco tiver valor
        private static void Preencher(TextBox caixa, string valor)
        {
            if (!string.IsNullOrWhiteSpace(valor))
                caixa.Text = valor;
        }

        private void VoltarParaPerfis()
        {
            Form25 form25 = new Form25();
            form25.StartPosition = FormStartPosition.Manual;
            form25.Location = this.Location;
            form25.Size = this.Size;
            form25.Show();
            BeginInvoke(new Action(Close));
        }

        // ---------- Efeitos visuais dos botões ----------

        private void button4_Enter(object sender, EventArgs e)
        {
            button4.Image = Properties.Resources.botão_inicio_2;
        }

        private void button4_Leave(object sender, EventArgs e)
        {
            button4.Image = Properties.Resources.botão_inicio_normal;
        }

        private void button5_Enter(object sender, EventArgs e)
        {
            button5.Image = Properties.Resources.botão_configurações;
        }

        private void button5_Leave(object sender, EventArgs e)
        {
            button5.Image = Properties.Resources.botão_configurações_normal;
        }

        private void button1_Enter(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.botao_concluir_selecionado;
            pictureBox1.Image = Properties.Resources.Tela_perfil_instituiçao_bt_selecionado;
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.botao_concluir_normal;
            pictureBox1.Image = Properties.Resources.Tela_perfil_instituiçao;
        }

        private void button6_Enter(object sender, EventArgs e)
        {
            button6.Image = Properties.Resources.botao_voltar_tela_perfil_selecionado1;
        }

        private void button6_Leave(object sender, EventArgs e)
        {
            button6.Image = Properties.Resources.botao_voltar_tela_perfil_normal;
        }

        // ---------- Navegação ----------

        // Configurações
        private void button5_Click(object sender, EventArgs e)
        {
            Form16 form16 = new Form16();
            form16.StartPosition = FormStartPosition.Manual;
            form16.Location = this.Location;
            form16.Size = this.Size;
            form16.Show();
            this.Close();
        }

        // Início
        private void button4_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.StartPosition = FormStartPosition.Manual;
            form1.Location = this.Location;
            form1.Size = this.Size;
            form1.Show();
            this.Close();
        }

        // Voltar: volta para a tela de escolha de perfil
        private void button6_Click(object sender, EventArgs e)
        {
            Form25 form25 = new Form25();
            form25.StartPosition = FormStartPosition.Manual;
            form25.Location = this.Location;
            form25.Size = this.Size;
            form25.Show();
            this.Close();
        }

        // ---------- Concluir: salva as alterações ----------

        private void button1_Click(object sender, EventArgs e)
        {
            string nome = Valor(textBox2, PH_NOME);
            string tipo = Valor(textBox1, PH_TIPO);
            string local = Valor(textBox3, PH_LOCAL);
            string bio = Valor(textBox4, PH_BIO);
            string email = Valor(textBox5, PH_EMAIL).ToLower();

            if (nome.Length < 2)
            {
                MessageBox.Show("Digite o nome da instituição.");
                textBox2.Focus();
                return;
            }

            if (tipo == "")
            {
                MessageBox.Show("Digite o tipo da instituição.");
                textBox1.Focus();
                return;
            }

            if (email == "" || !email.Contains("@") || !email.Contains(".") || email.Contains(" "))
            {
                MessageBox.Show("Digite um e-mail válido.");
                textBox5.Focus();
                return;
            }

            try
            {
                // Confere de novo, por segurança, que a pessoa ainda é dona
                if (!Sessao.EhDono() || _idInstituicao == 0)
                {
                    MessageBox.Show("Apenas o dono da instituição pode editar as informações dela.");
                    return;
                }

                using (NpgsqlConnection conn = Banco.Abrir())
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    @"UPDATE public.instituicao
                      SET nome = @nome, tipo = @tipo, email = @email,
                          localizacao = @local, biografia = @bio
                      WHERE id_instituicao = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@tipo", tipo);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@local", local == "" ? (object)DBNull.Value : local);
                    cmd.Parameters.AddWithValue("@bio", bio == "" ? (object)DBNull.Value : bio);
                    cmd.Parameters.AddWithValue("@id", _idInstituicao);
                    cmd.ExecuteNonQuery();
                }

                // Mantém o nome atualizado na sessão
                Sessao.EntrarNaInstituicao(_idInstituicao, nome);

                MessageBox.Show("Informações da instituição salvas!");
            }
            catch (PostgresException ex)
            {
                if (ex.SqlState == "23505")
                    MessageBox.Show("Já existe outra instituição com esse e-mail.");
                else
                    MessageBox.Show("Erro ao salvar:\n\n" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar:\n\n" + ex.Message);
            }
            Form25 form25 = new Form25();
            form25.StartPosition = FormStartPosition.Manual;
            form25.Location = this.Location;
            form25.Size = this.Size;
            form25.Show();
            this.Close();
        }

        // ---------- Textos de exemplo (placeholders) ----------

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == PH_TIPO) textBox1.Text = "";
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text)) textBox1.Text = PH_TIPO;
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == PH_NOME) textBox2.Text = "";
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text)) textBox2.Text = PH_NOME;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == PH_LOCAL) textBox3.Text = "";
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text)) textBox3.Text = PH_LOCAL;
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == PH_BIO) textBox4.Text = "";
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox4.Text)) textBox4.Text = PH_BIO;
        }

        private void textBox5_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == PH_EMAIL) textBox5.Text = "";
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox5.Text)) textBox5.Text = PH_EMAIL;
        }
    }
}