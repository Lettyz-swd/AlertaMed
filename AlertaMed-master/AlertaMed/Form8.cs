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
    public partial class Form8 : Form

    {
      
        private const string PH_NOME = "Digite o Nome Completo";
        private const string PH_EMAIL = "Digite o seu E-mail";
        private const string PH_MSG = "Digite sua mensagem";
        private const string TXT_EMAIL_AUTO = "Preenchido automaticamente";
        private const string TXT_SELECIONE = "Selecione a instituição";

       
        private class InstituicaoItem
        {
            public int Id;
            public string Nome;
            public string Email;

            public override string ToString()
            {
                return Nome;
            }
        }

        public Form8()
        {
            InitializeComponent();
            this.MaximizeBox = false; 
            
            Color corFundo = textBox4.BackColor;
            textBox4.ReadOnly = true;
            textBox4.BackColor = corFundo;
            textBox4.TabStop = false;
            textBox4.Text = TXT_EMAIL_AUTO;

          
            cmbInstituicao.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbInstituicao.SelectedIndexChanged += cmbInstituicao_SelectedIndexChanged;
            CarregarInstituicoes();
        }

        private void CarregarInstituicoes()
        {
            cmbInstituicao.Items.Clear();
            cmbInstituicao.Items.Add(new InstituicaoItem { Id = 0, Nome = TXT_SELECIONE, Email = "" });

            try
            {
                using (NpgsqlConnection conn = Banco.Abrir())
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    "SELECT id_instituicao, nome, email FROM public.instituicao ORDER BY nome", conn))
                using (NpgsqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        cmbInstituicao.Items.Add(new InstituicaoItem
                        {
                            Id = rd.GetInt32(0),
                            Nome = rd.GetString(1),
                            Email = rd.GetString(2)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Não foi possível carregar as instituições.\n\n" + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            cmbInstituicao.SelectedIndex = 0;
        }

        private void cmbInstituicao_SelectedIndexChanged(object sender, EventArgs e)
        {
            InstituicaoItem item = cmbInstituicao.SelectedItem as InstituicaoItem;

            if (item == null || item.Id == 0)
            {
                textBox4.Text = TXT_EMAIL_AUTO;
            }
            else
            {
                textBox4.Text = item.Email;
            }
        }

        
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
            form1.Show();
            this.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Form16(this).Show();
            this.Hide(); ;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool temDados =
                Valor(textBox1, PH_NOME) != "" ||
                Valor(textBox2, PH_EMAIL) != "" ||
                Valor(textBox5, PH_MSG) != "" ||
                cmbInstituicao.SelectedIndex > 0;

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
            form5.Show();
            this.Hide();

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
            pictureBox1.Image = Properties.Resources.Tela_de_entrar_inst__botão_solicitar_selecionado;
            button1.Image = Properties.Resources.botão_solicitar_selecionado;
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Tela_de_entrar_inst__botao_solicitar_normal;
            button1.Image = Properties.Resources.botão_solicitar_normal;
        }

        private void button7_Enter(object sender, EventArgs e)
        {

        }

        private void button7_Leave(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.StartPosition = FormStartPosition.Manual;
            form2.Location = this.Location;
            form2.Show();
            this.Hide();
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            string nome = Valor(textBox1, PH_NOME);
            string email = Valor(textBox2, PH_EMAIL).ToLower();
            string mensagem = Valor(textBox5, PH_MSG);
            InstituicaoItem inst = cmbInstituicao.SelectedItem as InstituicaoItem;

          
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

          
            if (string.IsNullOrEmpty(email))
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

            
            if (cmbInstituicao.Items.Count <= 1)
            {
                MessageBox.Show("Nenhuma instituição cadastrada ainda.");
                return;
            }

            if (inst == null || inst.Id == 0)
            {
                MessageBox.Show("Selecione uma instituição.");
                cmbInstituicao.Focus();
                return;
            }

            try
            {
                int idUsuario = 0;

                if (Sessao.Logado)
                {
                    
                    idUsuario = Sessao.IdUsuario;
                }
                else
                {
                  
                    using (NpgsqlConnection conn = Banco.Abrir())
                    using (NpgsqlCommand cmd = new NpgsqlCommand(
                        "SELECT id_usuario, nome FROM public.usuario WHERE lower(email) = lower(@email)", conn))
                    {
                        cmd.Parameters.AddWithValue("@email", email);
                        using (NpgsqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                idUsuario = rd.GetInt32(0);
                                nome = rd.GetString(1); 
                            }
                        }
                    }

                    if (idUsuario == 0)
                    {
                        using (NpgsqlConnection conn = Banco.Abrir())
                        using (NpgsqlCommand cmd = new NpgsqlCommand(
                            @"INSERT INTO public.usuario (nome, email, senha)
                              VALUES (@nome, @email, @senha)
                              RETURNING id_usuario", conn))
                        {
                            cmd.Parameters.AddWithValue("@nome", nome);
                            cmd.Parameters.AddWithValue("@email", email);
                           
                            cmd.Parameters.AddWithValue("@senha", Senha.Gerar(Guid.NewGuid().ToString("N")));
                            idUsuario = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                    }

                   
                    Sessao.Entrar(idUsuario, nome);
                }

                
                using (NpgsqlConnection conn = Banco.Abrir())
                using (NpgsqlTransaction tx = conn.BeginTransaction())
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand(
                        @"INSERT INTO public.membro_instituicao (id_instituicao, id_usuario, papel)
                          VALUES (@inst, @usr, 'membro')
                          ON CONFLICT (id_instituicao, id_usuario) DO NOTHING", conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@inst", inst.Id);
                        cmd.Parameters.AddWithValue("@usr", idUsuario);
                        cmd.ExecuteNonQuery();
                    }

                    using (NpgsqlCommand cmd = new NpgsqlCommand(
                        @"INSERT INTO public.solicitacao_entrada
                              (id_instituicao, nome_solicitante, email_solicitante, mensagem, status)
                          VALUES (@inst, @nome, @email, @msg, 'aprovada')", conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@inst", inst.Id);
                        cmd.Parameters.AddWithValue("@nome", nome);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@msg",
                            mensagem == "" ? (object)DBNull.Value : mensagem);
                        cmd.ExecuteNonQuery();
                    }

                    tx.Commit();
                }

                
                Sessao.EntrarNaInstituicao(inst.Id, inst.Nome);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao entrar na instituição:\n\n" + ex.Message);
                return;
            }

            Form6 form6 = new Form6();
            form6.StartPosition = FormStartPosition.Manual;
            form6.Location = this.Location;
            form6.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

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
            if (textBox2.Text == "Digite o seu E-mail")
            {
                textBox2.Clear();
            }
        }

        private void textBox5_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == "Digite sua mensagem")
            {
                textBox5.Clear();
            }
        }

        
        private void textBox3_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_Click(object sender, EventArgs e)
        {

        }

     
        private void textBox1_Leave(object sender, EventArgs e)
        {

        }

        private void textBox2_Leave(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_Leave(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}