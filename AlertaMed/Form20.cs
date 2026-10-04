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

            ConfigurarGrupoExclusivo(checkBox1, checkBox2, checkBox3, checkBox4);
            ConfigurarParNaoSim(checkBox5, checkBox6, textBox7, "Quantos?");   // Tem filhos?
            ConfigurarParNaoSim(checkBox7, checkBox10, textBox8, "Quais?");    // Doenças Respiratórias?
            ConfigurarParNaoSim(checkBox8, checkBox11, textBox9, "Quais?");    // Doenças Cardiovasculares?
            ConfigurarParNaoSim(checkBox9, checkBox12, textBox10, "Quais?");   // Alergias?

            lixeira.Click += lixeira_Click;
            lixeira2.Click += lixeira2_Click;
            lixeira3.Click += lixeira3_Click;
            lixeira4.Click += lixeira4_Click;
            lixeira5.Click += lixeira5_Click;
        }

        // ================= Lixeiras: limpam o campo ao lado =================

        private void lixeira_Click(object sender, EventArgs e)
        {
            textBox8.Text = "Quais?";
        }

        private void lixeira2_Click(object sender, EventArgs e)
        {
            textBox9.Text = "Quais?";
        }

        private void lixeira3_Click(object sender, EventArgs e)
        {
            textBox10.Text = "Quais?";
        }

        private void lixeira4_Click(object sender, EventArgs e)
        {
            textBox5.Text = "Digite informações extras";
        }

        private void lixeira5_Click(object sender, EventArgs e)
        {
            textBox6.Text = "Digite suas anotações";
        }

        // Liga um par "Não"/"Sim" a um campo de detalhe: marcar "Não" trava o
        // campo (e limpa); marcar "Sim" destrava. Marcar um desmarca o outro.
        private void ConfigurarParNaoSim(CheckBox chkNao, CheckBox chkSim, TextBox campo, string placeholder)
        {
            chkNao.CheckedChanged += (s, e) =>
            {
                if (chkNao.Checked) chkSim.Checked = false;
                AtualizarBloqueioCampo(chkNao, campo, placeholder);
            };

            chkSim.CheckedChanged += (s, e) =>
            {
                if (chkSim.Checked) chkNao.Checked = false;
                AtualizarBloqueioCampo(chkNao, campo, placeholder);
            };
        }

        private void AtualizarBloqueioCampo(CheckBox chkNao, TextBox campo, string placeholder)
        {
            if (chkNao.Checked)
            {
                campo.Text = placeholder;
                campo.Enabled = false;
            }
            else
            {
                campo.Enabled = true;
                if (string.IsNullOrWhiteSpace(campo.Text))
                    campo.Text = placeholder;
            }
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
            // ---- precisa estar logado: o paciente fica ligado à conta ----
            if (!Sessao.Logado)
            {
                MessageBox.Show("Entre na sua conta para cadastrar o paciente.");
                return;
            }

            // ---- validações básicas ----
            if (string.IsNullOrWhiteSpace(textBox1.Text) || textBox1.Text == "Digite o nome do paciente")
            {
                MessageBox.Show("Digite o nome do paciente.");
                return;
            }

            if (!int.TryParse(textBox3.Text, out int idade))
            {
                MessageBox.Show("Digite uma idade válida.");
                return;
            }

            if (!decimal.TryParse(textBox11.Text, out decimal peso))
            {
                MessageBox.Show("Digite um peso válido.");
                return;
            }

            // ---- gênero (faltava essa validação) ----
            if (comboBox2.SelectedIndex == -1 || comboBox2.Text == "Selecione uma opção" || string.IsNullOrWhiteSpace(comboBox2.Text))
            {
                MessageBox.Show("Selecione o gênero do paciente.");
                comboBox2.Focus();
                return;
            }

            // ---- estado civil (obrigatório marcar um) ----
            if (!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked && !checkBox4.Checked)
            {
                MessageBox.Show("Selecione o estado civil do paciente.");
                return;
            }

            string estadoCivil = checkBox1.Checked ? "solteiro"
                                : checkBox2.Checked ? "casado"
                                : checkBox3.Checked ? "viuvo"
                                : "divorciado";

            // ---- tem filhos? (obrigatório Sim ou Não) ----
            if (!checkBox5.Checked && !checkBox6.Checked)
            {
                MessageBox.Show("Informe se o paciente tem filhos.");
                return;
            }

            // ---- doenças respiratórias? ----
            if (!checkBox7.Checked && !checkBox10.Checked)
            {
                MessageBox.Show("Informe se o paciente tem doenças respiratórias.");
                return;
            }

            // ---- doenças cardiovasculares? ----
            if (!checkBox8.Checked && !checkBox11.Checked)
            {
                MessageBox.Show("Informe se o paciente tem doenças cardiovasculares.");
                return;
            }

            // ---- alergias? ----
            if (!checkBox9.Checked && !checkBox12.Checked)
            {
                MessageBox.Show("Informe se o paciente tem alergias.");
                return;
            }

            // ---- filhos ----
            bool temFilhos = checkBox6.Checked;
            int? qtdFilhos = null;
            if (temFilhos)
            {
                if (!int.TryParse(textBox7.Text, out int qtd))
                {
                    MessageBox.Show("Digite a quantidade de filhos corretamente.");
                    textBox7.Focus();
                    textBox7.SelectAll();
                    return;
                }
                qtdFilhos = qtd;
            }

            // ---- monta o objeto ----
            var paciente = new Paciente
            {
                Nome = textBox1.Text,
                Idade = idade,
                Genero = comboBox2.SelectedItem?.ToString() ?? comboBox2.Text,
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
                IdUsuario = Sessao.IdUsuario   // paciente ligado ao usuário logado
            };

            try
            {
                paciente.Salvar();
                MessageBox.Show("Paciente cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar paciente: " + ex.Message);
                return; // não navega pro Form13 se o salvamento falhou
            }

            Form13 form13 = new Form13();
            form13.StartPosition = FormStartPosition.Manual;
            form13.Location = this.Location;
            form13.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
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
            this.Hide();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "Digite o nome do paciente")
            {
                textBox1.Text = "";
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "Idade")
            {
                textBox3.Text = "";
            }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_Click(object sender, EventArgs e)
        {
            if (textBox11.Text == "Peso")
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
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = "Digite o nome do paciente";
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
            Form19 form19 = new Form19();
            form19.StartPosition = FormStartPosition.Manual;
            form19.Location = this.Location;
            form19.Show();
            this.Close();
        }
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ConfigurarGrupoExclusivo(params CheckBox[] grupo)
        {
            foreach (var chk in grupo)
            {
                chk.CheckedChanged += (s, e) =>
                {
                    if (chk.Checked)
                    {
                        foreach (var outro in grupo)
                        {
                            if (outro != chk)
                                outro.Checked = false;
                        }
                    }
                };
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}