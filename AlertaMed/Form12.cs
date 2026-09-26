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

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
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

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            textBox10.Enabled = radioButton11.Checked;

            if (!radioButton11.Checked)
            {
                textBox10.Text = "Quais?";
                textBox10.ForeColor = Color.Gray;
            }
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            textBox7.Enabled = radioButton6.Checked;

            if (!radioButton6.Checked)
            {
                textBox7.Text = "Quantos?";
                textBox7.ForeColor = Color.Gray;
            }
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            textBox8.Enabled = radioButton7.Checked;

            if (!radioButton7.Checked)
            {
                textBox8.Text = "Quais?";
                textBox8.ForeColor = Color.Gray;
            }
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            textBox9.Enabled = radioButton9.Checked;

            if (!radioButton9.Checked)
            {
                textBox9.Text = "Quais?";
                textBox9.ForeColor = Color.Gray;
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
