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
    public partial class Form20 : Form
    {
        public Form20()
        {
            InitializeComponent();
        }

        private void button4_Enter(object sender, EventArgs e)
        {
            button4.Image = Properties.Resources.botão_inicio_2;
        }

        private void button4_Leave(object sender, EventArgs e)
        {
            button4.Image = Properties.Resources.botão_inicio_normal;
        }

        private void button3_Enter(object sender, EventArgs e)
        {
            button3.Image = Properties.Resources.botão_configurações;
        }

        private void button3_Leave(object sender, EventArgs e)
        {
            button3.Image = Properties.Resources.botão_configurações_normal;
        }

        private void button1_Enter(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.botão_voltar_cadastro_selecionado;
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.botão_voltar_cadastro;
        }

        private void button2_Enter(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Tela_cadastro_paciente_2_bt_selecionado;
            button2.Image = Properties.Resources.botao_cadastrar_preescrição_selecionado_2;
            pictureBox1.Image = Properties.Resources.Tela_cadastro_paciente_2_bt_selecionado;
        }

        private void button2_Leave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Tela_cadastro_paciente_2_bt_normal;
            button2.Image = Properties.Resources.botao_cadastrar_preescrição_normal_2;
            pictureBox1.Image = Properties.Resources.Tela_cadastro_paciente_2_bt_normal;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // ---- validações básicas ----
            if (string.IsNullOrWhiteSpace(TxTbxNP.Text) || TxTbxNP.Text == "Digite o nome do paciente")
            {
                MessageBox.Show("Digite o nome do paciente.");
                return;
            }

            if (!int.TryParse(textBox2.Text, out int idade))
            {
                MessageBox.Show("Digite uma idade válida.");
                return;
            }

            if (!decimal.TryParse(textBox4.Text, out decimal peso))
            {
                MessageBox.Show("Digite um peso válido.");
                return;
            }

            // ---- estado civil ----
            string estadoCivil = checkBox1.Checked ? "solteiro"
                                : checkBox2.Checked ? "casado"
                                : checkBox3.Checked ? "viuvo"
                                : checkBox4.Checked ? "divorciado"
                                : null;

            // ---- filhos ----
            bool temFilhos = checkBox6.Checked;
            int? qtdFilhos = null;
            if (temFilhos && int.TryParse(textBox7.Text, out int qtd))
                qtdFilhos = qtd;

            // ---- monta o objeto ----
            var paciente = new Paciente
            {
                Nome = TxTbxNP.Text,
                Idade = idade,
                Genero = comboBox1.SelectedItem?.ToString() ?? comboBox1.Text,
                Peso = peso,
                EstadoCivil = estadoCivil,
                TemFilhos = temFilhos,
                QuantidadeFilhos = qtdFilhos,
                DoencasRespiratorias = checkBox10.Checked,
                QuaisDoencasRespiratorias = checkBox10.Checked ? textBox8.Text : null,
                DoencasCardiovasculares = checkBox11.Checked,
                QuaisDoencasCardiovasculares = checkBox11.Checked ? textBox9.Text : null,
                TemAlergias = checkBox12.Checked,
                QuaisAlergias = checkBox12.Checked ? textBox10.Text : null,
                InformacoesExtras = textBox5.Text,
                Anotacoes = textBox6.Text,
                IdUsuario = 1 // TODO: trocar pelo id do usuário logado (sessão)
            };

            try
            {
                paciente.Salvar();
                MessageBox.Show("Paciente cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar paciente: " + ex.Message);
            }
            Form13 form13 = new Form13();
            form13.StartPosition = FormStartPosition.Manual;
            form13.Location = this.Location;
            form13.Size = this.Size;
            form13.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
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
            Form16 form16 = new Form16();
            form16.StartPosition = FormStartPosition.Manual;
            form16.Location = this.Location;
            form16.Size = this.Size;
            form16.Show();
            this.Close();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (TxTbxNP.Text == "Digite o nome do paciente")
            {
                TxTbxNP.Text = "";
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "Digite a idade do paciente")
            {
                textBox3.Text = "";
            }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_Click(object sender, EventArgs e)
        {
            if (textBox11.Text == "Digite o peso do paciente")
            {
                textBox11.Text = "";
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_Click(object sender, EventArgs e)
        {
            if (textBox7.Text == "Quantos?")
            {
                textBox7.Text = "";
            }
        }

        private void textBox8_Click(object sender, EventArgs e)
        {
            if (textBox8.Text == "Quais?")
            {
                textBox8.Text = "";
            }
        }

        private void textBox9_Click(object sender, EventArgs e)
        {
            if (textBox9.Text == "Quais?")
            {
                textBox9.Text = "";
            }
        }

        private void textBox10_Click(object sender, EventArgs e)
        {
            if (textBox10.Text == "Quais?")
            {
                textBox10.Text = "";
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxTbxNP.Text))
            {
                TxTbxNP.Text = "Digite o nome do paciente";
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                textBox3.Text = "Idade";
            }
        }

        private void textBox11_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox11.Text))
            {
                textBox11.Text = "Peso";
            }
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox7.Text))
            {
                textBox7.Text = "Quantos?";
            }
        }

        private void textBox8_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox8.Text))
            {
                textBox8.Text = "Quais?";
            }
        }

        private void textBox9_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox9.Text))
            {
                textBox9.Text = "Quais?";
            }
        }

        private void textBox10_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox10.Text))
            {
                textBox10.Text = "Quais?";
            }
        }

        private void textBox5_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == "Digite informações extras")
            {
                textBox5.Text = "";
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox5.Text))
            {
                textBox5.Text = "Digite informações extras";
            }
        }

        private void textBox6_Click(object sender, EventArgs e)
        {
            if (textBox6.Text == "Digite suas anotações")
            {
                textBox6.Text = "";
            }
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox6.Text))
            {
                textBox6.Text = "Digite suas anotações";
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            return;
        }
    }
}