using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlertaMed
{
    public partial class Form24 : Form
    {
        // ---------- Placeholders (texto de exemplo nos campos) ----------
        private const string PH_NOME = "Digite seu nome";
        private const string PH_GENERO = "Digite seu gênero";
        private const string PH_BIO = "Digite sua biografia";

        // Devolve o texto do campo, ou "" se ele ainda estiver com o placeholder / vazio
        private string Valor(TextBox campo, string placeholder)
        {
            string texto = campo.Text.Trim();
            return (texto == placeholder || string.IsNullOrWhiteSpace(texto)) ? "" : texto;
        }

        public Form24()
        {
            InitializeComponent();
        }

        // ---------- Carregamento da tela ----------

        private void Form24_Load(object sender, EventArgs e)
        {
            if (Sessao.Logado && !string.IsNullOrWhiteSpace(Sessao.Nome))
                textBox1.Text = Sessao.Nome;

            string papel = null;
            try
            {
                papel = Sessao.ObterPapel();
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possível carregar o cargo. Verifique a conexão com o banco.");
            }

            if (papel == "dono")
                textBox3.Text = "Dono da instituição";
            else if (papel == "membro")
                textBox3.Text = "Técnico";
            else
                textBox3.Text = "Sem instituição";

            textBox3.ReadOnly = true;         // o cargo vem do banco, a pessoa não digita
            textBox3.BackColor = Color.White; // ReadOnly deixa a caixa cinza por padrão

            // Busca gênero e biografia salvos no banco e preenche os campos
            if (Sessao.Logado)
            {
                try
                {
                    using (NpgsqlConnection conn = Banco.Abrir())
                    using (NpgsqlCommand cmd = new NpgsqlCommand(
                        "SELECT genero, biografia FROM public.usuario WHERE id_usuario = @u", conn))
                    {
                        cmd.Parameters.AddWithValue("@u", Sessao.IdUsuario);

                        using (NpgsqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                string generoSalvo = rd.IsDBNull(0) ? "" : rd.GetString(0);
                                string bioSalva = rd.IsDBNull(1) ? "" : rd.GetString(1);

                                textBox2.Text = string.IsNullOrWhiteSpace(generoSalvo) ? PH_GENERO : generoSalvo;
                                textBox4.Text = string.IsNullOrWhiteSpace(bioSalva) ? PH_BIO : bioSalva;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Não foi possível carregar gênero/biografia.\n\n" + ex.Message);
                }
            }
        }

        // ---------- Efeitos visuais dos botões ----------

        private void button1_Enter(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.botao_concluir_selecionado;
            pictureBox1.Image = Properties.Resources.Tela_Personalizar_perfil_Gerente_instituiçao_bt_selecionado;
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.botao_concluir_normal;
            pictureBox1.Image = Properties.Resources.Tela_Personalizar_perfil_Gerente_instituiçao;
        }

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

        private void button3_Enter(object sender, EventArgs e)
        {
            button3.Image = Properties.Resources.botao_voltar_redondo_selecionado1;
            pictureBox1.Image = Properties.Resources.Personalizar_perfil_Gerente_instituiçao_bt_voltar_selecionado;
        }

        private void button3_Leave(object sender, EventArgs e)
        {
            button3.Image = Properties.Resources.botao_voltar_redondo_normal1;
            pictureBox1.Image = Properties.Resources.Tela_Personalizar_perfil_Gerente_instituiçao;
        }

        // ---------- Navegação ----------

        // Voltar: volta para a tela de escolha de perfil
        private void button3_Click(object sender, EventArgs e)
        {
            Form25 form25 = new Form25();
            form25.StartPosition = FormStartPosition.Manual;
            form25.Location = this.Location;
            form25.Show();
            this.Close();
        }

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
            Form16 form16 = new Form16();
            form16.StartPosition = FormStartPosition.Manual;
            form16.Location = this.Location;
            form16.Show();
            this.Close();
        }

        // Concluir
        private void button1_Click(object sender, EventArgs e)
        {
            string nome = Valor(textBox1, PH_NOME);
            string genero = Valor(textBox2, PH_GENERO);
            string bio = Valor(textBox4, PH_BIO); // corrigido: era textBox3 (cargo), agora é textBox4 (biografia)

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
            Form25 form25 = new Form25();
            form25.StartPosition = FormStartPosition.Manual;
            form25.Location = this.Location;
            form25.Show();
            this.Close();
        }

        // ---------- Textos de exemplo (placeholders) ----------

        // Mantido vazio de propósito: o Leave já cuida do texto padrão.
        // Não apague o método, senão o Designer dá erro de compilação.
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = "Digite seu nome";
            }
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "Digite seu gênero")
            {
                textBox2.Text = "";
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                textBox2.Text = "Digite seu gênero";
            }
        }

        // O cargo é somente leitura, mas os métodos ficam para não quebrar o Designer.
        private void textBox3_Click(object sender, EventArgs e)
        {
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "Digite sua biografia")
            {
                textBox4.Text = "";
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                textBox4.Text = "Digite sua biografia";
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }
    }
}