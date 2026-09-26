using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AlertaMed
{
    public partial class Form12 : Form
    {
        public Form12()
        {
            InitializeComponent();

            // Nenhum desses eventos estava ligado no designer: fazem o efeito de
            // "marcar um desmarca o outro" (estado civil) e mostram/habilitam os
            // campos "Quantos?"/"Quais?" conforme o Sim/Não escolhido.
            checkBox1.CheckedChanged += checkBox1_CheckedChanged_1;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            checkBox3.CheckedChanged += checkBox3_CheckedChanged;
            checkBox4.CheckedChanged += checkBox4_CheckedChanged;

            checkBox5.CheckedChanged += checkBox5_CheckedChanged;
            checkBox6.CheckedChanged += checkBox6_CheckedChanged;
            checkBox7.CheckedChanged += checkBox7_CheckedChanged;
            checkBox10.CheckedChanged += checkBox10_CheckedChanged;
            checkBox8.CheckedChanged += checkBox8_CheckedChanged;
            checkBox11.CheckedChanged += checkBox11_CheckedChanged;
            checkBox9.CheckedChanged += checkBox9_CheckedChanged;
            checkBox12.CheckedChanged += checkBox12_CheckedChanged;

            lixeira.Click += lixeira_Click;
            lixeira2.Click += lixeira2_Click;
            lixeira3.Click += lixeira3_Click;
            lixeira4.Click += lixeira4_Click;
            lixeira5.Click += lixeira5_Click;
        }

        // ================= Estado civil (só um marcado por vez) =================

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox4.Checked = false;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
            }
        }

        // ================= Tem filhos? (checkBox6 = Sim / checkBox5 = Não) =================

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked) checkBox6.Checked = false;
            AtualizarFilhos();
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked) checkBox5.Checked = false;
            AtualizarFilhos();
        }

        private void AtualizarFilhos()
        {
            textBox7.Enabled = checkBox6.Checked;
            if (!checkBox6.Checked)
            {
                textBox7.Text = "Quantos?";
                textBox7.ForeColor = Color.Gray;
            }
        }

        // ===== Doenças respiratórias? (checkBox10 = Sim / checkBox7 = Não) =====

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked) checkBox10.Checked = false;
            AtualizarRespiratoria();
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox10.Checked) checkBox7.Checked = false;
            AtualizarRespiratoria();
        }

        private void AtualizarRespiratoria()
        {
            textBox8.Enabled = checkBox10.Checked;
            if (!checkBox10.Checked)
            {
                textBox8.Text = "Quais?";
                textBox8.ForeColor = Color.Gray;
            }
        }

        // ==== Doenças cardiovasculares? (checkBox11 = Sim / checkBox8 = Não) ====

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked) checkBox11.Checked = false;
            AtualizarCardiovascular();
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox11.Checked) checkBox8.Checked = false;
            AtualizarCardiovascular();
        }

        private void AtualizarCardiovascular()
        {
            textBox9.Enabled = checkBox11.Checked;
            if (!checkBox11.Checked)
            {
                textBox9.Text = "Quais?";
                textBox9.ForeColor = Color.Gray;
            }
        }

        // ================= Alergias? (checkBox12 = Sim / checkBox9 = Não) =================

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox9.Checked) checkBox12.Checked = false;
            AtualizarAlergia();
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox12.Checked) checkBox9.Checked = false;
            AtualizarAlergia();
        }

        private void AtualizarAlergia()
        {
            textBox10.Enabled = checkBox12.Checked;
            if (!checkBox12.Checked)
            {
                textBox10.Text = "Quais?";
                textBox10.ForeColor = Color.Gray;
            }
        }

        // ================= Lixeiras: limpam o campo ao lado =================

        private void lixeira_Click(object sender, EventArgs e)
        {
            textBox8.Text = "Quais?";
            textBox8.ForeColor = Color.Gray;
        }

        private void lixeira2_Click(object sender, EventArgs e)
        {
            textBox9.Text = "Quais?";
            textBox9.ForeColor = Color.Gray;
        }

        private void lixeira3_Click(object sender, EventArgs e)
        {
            textBox10.Text = "Quais?";
            textBox10.ForeColor = Color.Gray;
        }

        private void lixeira4_Click(object sender, EventArgs e)
        {
            textBox5.Text = "Digite aqui";
            textBox5.ForeColor = Color.Gray;
        }

        private void lixeira5_Click(object sender, EventArgs e)
        {
            textBox6.Text = "Digite aqui";
            textBox6.ForeColor = Color.Gray;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void textBox7_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox7_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (TxTbxNP.Text == "Nome do Paciente" || string.IsNullOrWhiteSpace(TxTbxNP.Text))
            {
                MessageBox.Show("Por favor, digite o nome do paciente.",
                                "Campo obrigatório",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                TxTbxNP.Focus();
                return;
            }

            // Valida o peso antes de continuar
            string textoPeso = textBox4.Text.Replace(",", ".");

            if (textoPeso == "Peso" || string.IsNullOrWhiteSpace(textoPeso) ||
                !double.TryParse(textoPeso, NumberStyles.Any, CultureInfo.InvariantCulture, out double peso))
            {
                MessageBox.Show("Por favor, digite o peso no formato correto (ex: 70.5 ou 70,5).",
                                "Valor inválido",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                textBox4.Focus();
                textBox4.SelectAll();
                return;
            }

            // Valida a idade antes de continuar
            if (textBox2.Text == "Idade" || string.IsNullOrWhiteSpace(textBox2.Text) ||
                !int.TryParse(textBox2.Text, out int idade))
            {
                MessageBox.Show("Por favor, digite a idade no formato correto (apenas números inteiros, ex: 25).",
                                "Valor inválido",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                textBox2.Focus();
                textBox2.SelectAll();
                return;
            }

            // Valida o gênero antes de continuar
            if (comboBox1.SelectedIndex == -1 || comboBox1.Text == "Selecione uma opção")
            {
                MessageBox.Show("Por favor, selecione o gênero do paciente.",
                                "Campo obrigatório",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                comboBox1.Focus();
                return;
            }

            //cadastrar prescrição
            Form13 form13 = new Form13(TxTbxNP.Text);
            form13.StartPosition = FormStartPosition.Manual;
            form13.Location = this.Location;
            form13.Size = this.Size;
            form13.Show();
            this.Close();
        }

        private void TxTbxNP_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void Form12_Load(object sender, EventArgs e)
        {
            textBox4.Text = "Peso";
            textBox4.ForeColor = Color.Gray;

            textBox2.Text = "Idade";
            textBox2.ForeColor = Color.Gray;

            TxTbxNP.Text = "Nome do Paciente";
            TxTbxNP.ForeColor = Color.Gray;

            textBox7.Text = "Quantos?";
            textBox7.ForeColor = Color.Gray;

            textBox8.Text = "Quais?";
            textBox8.ForeColor = Color.Gray;

            textBox9.Text = "Quais?";
            textBox9.ForeColor = Color.Gray;

            textBox10.Text = "Quais?";
            textBox10.ForeColor = Color.Gray;

            textBox10.Enabled = false;
            textBox7.Enabled = false;
            textBox8.Enabled = false;
            textBox9.Enabled = false;

            textBox5.Text = "Digite aqui";
            textBox5.ForeColor = Color.Gray;

            textBox6.Text = "Digite aqui";
            textBox6.ForeColor = Color.Gray;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

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

        private void button1_Enter(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.botão_voltar_cadastro_selecionado;
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.botão_voltar_cadastro;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //botao voltar
            Form15 form15 = new Form15();
            form15.StartPosition = FormStartPosition.Manual;
            form15.Location = this.Location;
            form15.Size = this.Size;
            form15.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //botao inicio
            Form1 form1 = new Form1();
            form1.StartPosition = FormStartPosition.Manual;
            form1.Location = this.Location;
            form1.Size = this.Size;
            form1.Show();
            this.Close();
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

        private void button3_Enter_1(object sender, EventArgs e)
        {

        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "Peso")
            {
                textBox4.Text = "";
                textBox4.ForeColor = Color.Black; // volta a cor normal do texto
            }
        }

        private void textBox4_Validated(object sender, EventArgs e)
        {

        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                textBox4.Text = "Peso";
                textBox4.ForeColor = Color.Gray;
                return;
            }

            string textoPeso = textBox4.Text.Replace(",", ".");

            if (!double.TryParse(textoPeso, NumberStyles.Any, CultureInfo.InvariantCulture, out double peso))
            {
                MessageBox.Show("Por favor, digite o peso no formato correto (ex: 70.5 ou 70,5).",
                                "Valor inválido",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                textBox4.Focus();
                textBox4.SelectAll();
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Idade")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                textBox2.Text = "Idade";
                textBox2.ForeColor = Color.Gray;
                return;
            }

            if (!int.TryParse(textBox2.Text, out int idade))
            {
                MessageBox.Show("Por favor, digite a idade no formato correto (apenas números inteiros, ex: 25).",
                                "Valor inválido",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                textBox2.Focus();
                textBox2.SelectAll();
            }
        }

        private void TxTbxNP_Enter(object sender, EventArgs e)
        {
            if (TxTbxNP.Text == "Nome do Paciente")
            {
                TxTbxNP.Text = "";
                TxTbxNP.ForeColor = Color.Black;
            }
        }

        private void TxTbxNP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxTbxNP.Text))
            {
                TxTbxNP.Text = "Nome do Paciente";
                TxTbxNP.ForeColor = Color.Gray;
            }
        }

        private void textBox7_Enter(object sender, EventArgs e)
        {
            if (textBox7.Text == "Quantos?")
            {
                textBox7.Text = "";
                textBox7.ForeColor = Color.Black;
            }
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox7.Text))
            {
                textBox7.Text = "Quantos?";
                textBox7.ForeColor = Color.Gray;
                return;
            }

            if (!int.TryParse(textBox7.Text, out int quantidade))
            {
                MessageBox.Show("Por favor, digite apenas números inteiros (ex: 2).",
                                "Valor inválido",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                textBox7.Focus();
                textBox7.SelectAll();
            }
        }

        private void textBox8_Enter(object sender, EventArgs e)
        {
            if (textBox8.Text == "Quais?")
            {
                textBox8.Text = "";
                textBox8.ForeColor = Color.Black;
            }
        }

        private void textBox8_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox8.Text))
            {
                textBox8.Text = "Quais?";
                textBox8.ForeColor = Color.Gray;
            }
        }

        private void textBox9_Enter(object sender, EventArgs e)
        {
            if (textBox9.Text == "Quais?")
            {
                textBox9.Text = "";
                textBox9.ForeColor = Color.Black;
            }

        }

        private void textBox9_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox9.Text))
            {
                textBox9.Text = "Quais?";
                textBox9.ForeColor = Color.Gray;
            }
        }

        private void textBox10_Enter(object sender, EventArgs e)
        {
            if (textBox10.Text == "Quais?")
            {
                textBox10.Text = "";
                textBox10.ForeColor = Color.Black;
            }
        }

        private void textBox10_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox10.Text))
            {
                textBox10.Text = "Quais?";
                textBox10.ForeColor = Color.Gray;
            }
        }





        private void textBox5_Enter(object sender, EventArgs e)
        {
            if (textBox5.Text == "Digite aqui")
            {
                textBox5.Text = "";
                textBox5.ForeColor = Color.Black;
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox5.Text))
            {
                textBox5.Text = "Digite aqui";
                textBox5.ForeColor = Color.Gray;
            }
        }

        private void textBox6_Enter(object sender, EventArgs e)
        {
            if (textBox6.Text == "Digite aqui")
            {
                textBox6.Text = "";
                textBox6.ForeColor = Color.Black;
            }
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox6.Text))
            {
                textBox6.Text = "Digite aqui";
                textBox6.ForeColor = Color.Gray;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}