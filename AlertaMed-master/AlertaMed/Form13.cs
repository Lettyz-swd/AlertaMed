using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Forms;

namespace AlertaMed
{
    public partial class Form13 : Form
    {
        private string textoGuardado = "";
        private string textoGuardado2 = "";
        private string textoGuardado3 = "";
        private string nomePaciente = "";

        private List<string> listaRemedios = new List<string>();
        private List<string> listaDoses = new List<string>();
        private List<string> listaHorarios = new List<string>();

        public Form13()
        {
            InitializeComponent();
        }

        public Form13(string texto)
        {
            InitializeComponent();
            nomePaciente = texto;
        }

        private void Form13_Load(object sender, EventArgs e)
        {
            // Nome do paciente (travado, vindo do Form12)
            if (!string.IsNullOrWhiteSpace(nomePaciente) && nomePaciente != "Nome do Paciente")
            {
                textBox2.Text = nomePaciente;
                textBox2.ForeColor = Color.Black;
            }
            else
            {
                textBox2.Text = "Nome do Paciente";
                textBox2.ForeColor = Color.Gray;
            }
            textBox2.ReadOnly = true;

            // Técnico responsável (placeholder)
            textBox1.Text = "Digite o nome do técnico responsável";
            textBox1.ForeColor = Color.Gray;

            // Listas grandes (travadas, só o código escreve)
            textBox4.ReadOnly = true;
            textBox6.ReadOnly = true;
            textBox10.ReadOnly = true;

            textBox3.Text = "Digite o remédio";
            textBox3.ForeColor = Color.Gray;
            textBox5.Text = "Digite a dose";
            textBox5.ForeColor = Color.Gray;
        }

        // ---------- textBox1 - Técnico Responsável ----------
        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Digite o nome do técnico responsável")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = "Digite o nome do técnico responsável";
                textBox1.ForeColor = Color.Gray;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //inicio
            Form1 form1 = new Form1();
            form1.StartPosition = FormStartPosition.Manual;
            form1.Location = this.Location;
            form1.Size = this.Size;
            form1.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //add remedios (textBox3 = input, textBox4 = lista grande)
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Digite um remédio antes de adicionar.", "Aviso");
                return;
            }

            listaRemedios.Add(textBox3.Text);
            textBox4.Text = string.Join(" - ", listaRemedios);

            textBox3.Clear();
            MessageBox.Show("Remédios guardados com sucesso!", "Aviso");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //add doses (textBox5 = input, textBox6 = lista grande)
            if (string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Digite uma dose antes de adicionar.", "Aviso");
                return;
            }

            listaDoses.Add(textBox5.Text);
            textBox6.Text = string.Join(" - ", listaDoses);

            textBox5.Clear();
            MessageBox.Show("Doses guardadas com sucesso!", "Aviso");
        }

        

        private void button5_Click(object sender, EventArgs e)
        {
            //add horario (textBox7, textBox8, textBox9 = inputs, textBox10 = lista grande)
            if (!ValidarHorario(textBox7) || !ValidarHorario(textBox8) || !ValidarHorario(textBox9))
            {
                return;
            }

            // Monta o grupo de 3 horários separados por vírgula
            string grupo = $"{textBox7.Text}, {textBox8.Text}, {textBox9.Text}";
            listaHorarios.Add(grupo);

            // Junta os grupos separados por " - "
            textBox10.Text = string.Join(" - ", listaHorarios);

            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            MessageBox.Show("Horários salvos com sucesso!", "Aviso");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //cadastrar paciente
            Form14 form14 = new Form14();
            form14.StartPosition = FormStartPosition.Manual;
            form14.Location = this.Location;
            form14.Size = this.Size;
            form14.Show();
            this.Close();
        }

        private void LbLNP_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            //voltar
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
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_preescrição_bt_remedios_selecionado;
            button3.Image = Properties.Resources.botao_adicionar_remedios_selecionado;
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_preescrição_bt_remedios_selecionado;
        }

        private void button3_Leave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_prescrição_normal;
            button3.Image = Properties.Resources.botao_remedios_normal;
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_prescrição_normal;
        }

        private void button4_Enter(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_prescrição_bt_doses_selecionado;
            button4.Image = Properties.Resources.botao_adicionar_doses_selecionado;
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_prescrição_bt_doses_selecionado;
        }

        private void button4_Leave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_prescrição_normal;
            button4.Image = Properties.Resources.botao_adicionar_doses_normal;
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_prescrição_normal;
        }

        private void button5_Enter(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_prescrição_bt_horarios_selecionado;
            button5.Image = Properties.Resources.botao_adicionar_horarios_selecionado;
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_prescrição_bt_horarios_selecionado;
        }

        private void button5_Leave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_prescrição_normal;
            button5.Image = Properties.Resources.botao_adicionar_horarios_normal;
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_prescrição_normal;
        }

        private void button2_Enter(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_preescrição_bt_cadastrar_selecionado;
            button2.Image = Properties.Resources.botao_cadastrar_preescrição_selecionado_2;
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_preescrição_bt_cadastrar_selecionado;
        }

        private void button2_Leave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_prescrição_normal;
            button2.Image = Properties.Resources.botao_cadastrar_preescrição_normal_2;
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_prescrição_normal;
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

        private void lixeira5_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
        }

        private void lixeira_Click(object sender, EventArgs e)
        {
            listaRemedios.Clear();
            textBox4.Clear();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox5.Clear();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            listaDoses.Clear();
            textBox6.Clear();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            listaHorarios.Clear();
            textBox10.Clear();
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
        private bool ValidarHorario(TextBox campo)
        {
            // Regex: exige exatamente HH:mm, com HH de 00 a 23 e mm de 00 a 59
            bool valido = System.Text.RegularExpressions.Regex.IsMatch(
                campo.Text,
                @"^([01]\d|2[0-3]):[0-5]\d$"
            );

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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}