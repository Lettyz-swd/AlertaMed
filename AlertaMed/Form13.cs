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
    public partial class Form13 : Form
    {
        public Form13()
        {
            InitializeComponent();
        }

        public Form13(string texto)
        {
            InitializeComponent();
            
        }

        private void Form13_Load(object sender, EventArgs e)
        {

        }

        
        private void textBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
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
            string valor = textBox3.Text.Trim();
            if (string.IsNullOrEmpty(valor) || valor == "Digite o remédio")
            {
                MessageBox.Show("Digite o nome do remédio.");
                return;
            }

            AdicionarLinha(textBox4, valor);
            textBox3.Clear();
        }

        private void lixeira5_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
        }

        private void lixeira_Click(object sender, EventArgs e)
        {
            RemoverUltimaLinha(textBox4);
        }

        
        private void button4_Click(object sender, EventArgs e)
        {
            string valor = textBox5.Text.Trim();
            if (string.IsNullOrEmpty(valor) || valor == "Digite a dose")
            {
                MessageBox.Show("Digite a dose.");
                return;
            }

            AdicionarLinha(textBox6, valor);
            textBox5.Clear();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox5.Clear();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            RemoverUltimaLinha(textBox6);
        }

       
        private void HorarioCampo_Enter(object sender, EventArgs e)
        {
            ((MaskedTextBox)sender).SelectAll();
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            ValidarHorario(textBox7);
        }

        private void textBox8_Leave(object sender, EventArgs e)
        {
            ValidarHorario(textBox8);
        }

        private void textBox9_Leave(object sender, EventArgs e)
        {
            ValidarHorario(textBox9);
        }

        private bool ValidarHorario(MaskedTextBox campo)
        {
            if (!campo.MaskCompleted)
            {
                campo.Clear();
                return true; 
            }

            bool valido = System.Text.RegularExpressions.Regex.IsMatch(
                campo.Text,
                @"^([01]\d|2[0-3]):[0-5]\d$");

            if (!valido)
            {
                MessageBox.Show("Por favor, digite o horário no formato correto (ex: 08:30, 14:00).",
                                "Valor inválido",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                campo.Focus();
                campo.SelectAll();
            }

            return valido;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var campos = new[] { textBox7, textBox8, textBox9 };

            var horarios = campos
                .Where(c => c.MaskCompleted)
                .Select(c => c.Text)
                .ToArray();

            if (horarios.Length == 0)
            {
                MessageBox.Show("Preencha ao menos um horário.");
                return;
            }

            AdicionarLinha(textBox10, string.Join(" | ", horarios));

            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            RemoverUltimaLinha(textBox10);
        }

       
        private void AdicionarLinha(TextBox lista, string valor)
        {
            lista.Text = string.IsNullOrEmpty(lista.Text)
                ? valor
                : lista.Text + Environment.NewLine + valor;
        }

        private void RemoverUltimaLinha(TextBox lista)
        {
            var linhas = lista.Text.Split(
                new[] { Environment.NewLine },
                StringSplitOptions.RemoveEmptyEntries);

            lista.Text = linhas.Length <= 1
                ? ""
                : string.Join(Environment.NewLine, linhas.Take(linhas.Length - 1));
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Adicione ao menos um remédio.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox6.Text))
            {
                MessageBox.Show("Adicione ao menos uma dose.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox10.Text))
            {
                MessageBox.Show("Adicione ao menos um horário.");
                return;
            }

            if (!Sessao.Logado)
            {
                MessageBox.Show("Entre na sua conta para cadastrar a prescrição.");
                return;
            }

            try
            {
                using (NpgsqlConnection conn = Banco.Abrir())
                using (NpgsqlCommand cmd = new NpgsqlCommand(@"
                    INSERT INTO public.prescricao
                        (nome_paciente, tecnico_responsavel, remedios, doses, horarios,
                         id_usuario, id_instituicao)
                    VALUES (@paciente, @tecnico, @remedios, @doses, @horarios,
                            @id_usuario, @id_instituicao)", conn))
                {
                    cmd.Parameters.AddWithValue("paciente", Sessao.Nome);
                    cmd.Parameters.AddWithValue("tecnico", "Autocadastro (uso pessoal)");
                    cmd.Parameters.AddWithValue("remedios", textBox4.Text);
                    cmd.Parameters.AddWithValue("doses", textBox6.Text);
                    cmd.Parameters.AddWithValue("horarios", textBox10.Text);

               
                    Sessao.AplicarDonoNoInsert(cmd);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar a prescrição:\n\n" + ex.Message);
                return; 
            }

            GerenciadorAlarmes.Atualizar();

            MessageBox.Show("Prescrição cadastrada com sucesso!");

       
            Form22 form22 = new Form22();
            form22.StartPosition = FormStartPosition.Manual;
            form22.Location = this.Location;
            form22.Show();
            this.Close();
        }

        private void LbLNP_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form12 form12 = new Form12();
            form12.StartPosition = FormStartPosition.Manual;
            form12.Location = this.Location;
            form12.Size = this.Size;
            form12.Show();
            this.Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Enter(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.botão_inicio_2;
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.botão_inicio_normal;
        }

        private void button6_Enter(object sender, EventArgs e)
        {
            button6.Image = Properties.Resources.botão_voltar_cadastro_selecionado;
        }

        private void button6_Leave(object sender, EventArgs e)
        {
            button6.Image = Properties.Resources.botão_voltar_cadastro;
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
            pictureBox1.Image = Properties.Resources.Tela_cadastro_prescrição_uso_pessoal_bt_remedio_selecionado;
            button3.Image = Properties.Resources.botao_adicionar_remedios_selecionado;
        }

        private void button3_Leave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Tela_cadastro_prescrição_uso_pessoal_normal;
            button3.Image = Properties.Resources.botao_remedios_normal;
        }

        private void button4_Enter(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Tela_cadastro_prescrição_uso_pessoal_bt_doses_selecionado;
            button4.Image = Properties.Resources.botao_adicionar_doses_selecionado;
        }

        private void button4_Leave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Tela_cadastro_prescrição_uso_pessoal_normal;
            button4.Image = Properties.Resources.botao_adicionar_doses_normal;
        }

        private void button5_Enter(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Tela_cadastro_prescrição_uso_pessoal_bt_horarios_selecionado;
            button5.Image = Properties.Resources.botao_adicionar_horarios_selecionado;
        }

        private void button5_Leave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Tela_cadastro_prescrição_uso_pessoal_normal;
            button5.Image = Properties.Resources.botao_adicionar_horarios_normal;
        }

        private void button2_Enter(object sender, EventArgs e)
        {
            button2.Image = Properties.Resources.botao_cadastrar_preescrição_selecionado_2;
            pictureBox1.Image = Properties.Resources.Tela_cadastro_prescrição_uso_pessoal_bt_concluido_selecionado;
        }

        private void button2_Leave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Tela_cadastro_prescrição_uso_pessoal_normal;
            button2.Image = Properties.Resources.botao_cadastrar_preescrição_normal_2;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Digite o remédio")
            {
                textBox3.Text = "";
                textBox3.ForeColor = Color.Black;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                textBox3.Text = "Digite o remédio";
                textBox3.ForeColor = Color.Gray;
            }
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            if (textBox5.Text == "Digite a dose")
            {
                textBox5.Text = "";
                textBox5.ForeColor = Color.Black;
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox5.Text))
            {
                textBox5.Text = "Digite a dose";
                textBox5.ForeColor = Color.Gray;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            new Form16(this).Show();
            this.Hide();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}