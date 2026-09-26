using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AlertaMed
{
    public partial class Form12 : Form
    {
        // Textos de exemplo que ficam dentro dos campos (propriedade Text no designer).
        // O formulário trata esses textos como campo vazio.
        private const string PH_NOME = "Digite o nome do paciente";
        private const string PH_IDADE = "Idade";
        private const string PH_QUANTOS = "Quantos?";
        private const string PH_QUAIS = "Quais?";
        private const string PH_INFO_EXTRA = "Digite informações extras";
        private const string PH_GENERO = "Selecione uma opção";

        public Form12()
        {
            InitializeComponent();

            // Esses eventos não estavam ligados no designer: fazem o efeito de
            // "marcar um desmarca o outro" (como um radio button) e mostram/escondem
            // os campos "Quantos?" / "Quais?" conforme o Sim/Não escolhido.
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            checkBox3.CheckedChanged += checkBox3_CheckedChanged;
            checkBox4.CheckedChanged += checkBox4_CheckedChanged;

            checkBox7.CheckedChanged += checkBox7_CheckedChanged;
            checkBox9.CheckedChanged += checkBox9_CheckedChanged;
            checkBox11.CheckedChanged += checkBox11_CheckedChanged;
            checkBox10.CheckedChanged += checkBox10_CheckedChanged;
            checkBox12.CheckedChanged += checkBox12_CheckedChanged;

            lixeira.Click += lixeira_Click;
            lixeira2.Click += lixeira2_Click;
            lixeira3.Click += lixeira3_Click;
            lixeira4.Click += lixeira4_Click;
            lixeira5.Click += lixeira5_Click;

            // Estado inicial: os campos "Quantos?"/"Quais?" só aparecem quando
            // a pessoa marcar "Sim" na pergunta correspondente.
            AtualizarVisibilidadeFilhos();
            AtualizarVisibilidadeRespiratoria();
            AtualizarVisibilidadeCardiovascular();
            AtualizarVisibilidadeAlergia();
        }

        private bool Vazio(string texto, string placeholder)
        {
            return string.IsNullOrWhiteSpace(texto) || texto == placeholder;
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

        // ================= Tem filhos? (checkBox5 = Sim / checkBox6 = Não) =================

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked) checkBox6.Checked = false;
            AtualizarVisibilidadeFilhos();
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked) checkBox5.Checked = false;
            AtualizarVisibilidadeFilhos();
        }

        private void AtualizarVisibilidadeFilhos()
        {
            bool mostrar = checkBox5.Checked;
            textBox7.Visible = mostrar;
            if (!mostrar) textBox7.Text = PH_QUANTOS;
        }

        // ========= Doenças respiratórias? (checkBox8 = Sim / checkBox7 = Não) =========

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked) checkBox8.Checked = false;
            AtualizarVisibilidadeRespiratoria();
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked) checkBox7.Checked = false;
            AtualizarVisibilidadeRespiratoria();
        }

        private void AtualizarVisibilidadeRespiratoria()
        {
            bool mostrar = checkBox8.Checked;
            textBox8.Visible = mostrar;
            lixeira.Visible = mostrar;
            if (!mostrar) textBox8.Text = PH_QUAIS;
        }

        // ====== Doenças cardiovasculares? (checkBox9 = Sim / checkBox11 = Não) ======

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox9.Checked) checkBox11.Checked = false;
            AtualizarVisibilidadeCardiovascular();
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox11.Checked) checkBox9.Checked = false;
            AtualizarVisibilidadeCardiovascular();
        }

        private void AtualizarVisibilidadeCardiovascular()
        {
            bool mostrar = checkBox9.Checked;
            textBox9.Visible = mostrar;
            lixeira2.Visible = mostrar;
            if (!mostrar) textBox9.Text = PH_QUAIS;
        }

        // ================= Alergias? (checkBox10 = Sim / checkBox12 = Não) =================

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox10.Checked) checkBox12.Checked = false;
            AtualizarVisibilidadeAlergia();
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox12.Checked) checkBox10.Checked = false;
            AtualizarVisibilidadeAlergia();
        }

        private void AtualizarVisibilidadeAlergia()
        {
            bool mostrar = checkBox10.Checked;
            textBox10.Visible = mostrar;
            lixeira3.Visible = mostrar;
            if (!mostrar) textBox10.Text = PH_QUAIS;
        }

        // ================= Lixeiras: limpam o campo ao lado =================

        private void lixeira_Click(object sender, EventArgs e)
        {
            textBox8.Text = PH_QUAIS;
        }

        private void lixeira2_Click(object sender, EventArgs e)
        {
            textBox9.Text = PH_QUAIS;
        }

        private void lixeira3_Click(object sender, EventArgs e)
        {
            textBox10.Text = PH_QUAIS;
        }

        private void lixeira4_Click(object sender, EventArgs e)
        {
            textBox5.Text = PH_INFO_EXTRA;
        }

        private void lixeira5_Click(object sender, EventArgs e)
        {
            textBox6.Text = "Digite suas anotações";
        }

        // ================= Validação simples e avançar para a próxima tela =================

        private void button2_Click(object sender, EventArgs e)
        {
            string nome = TxTbxNP.Text.Trim();
            string idadeTexto = textBox2.Text.Trim();

            if (Vazio(nome, PH_NOME))
            {
                MessageBox.Show("Preencha o nome do paciente.", "Atenção!");
                TxTbxNP.Focus();
                return;
            }

            if (Vazio(idadeTexto, PH_IDADE) || !int.TryParse(idadeTexto, out int idade) || idade < 0)
            {
                MessageBox.Show("Preencha a idade do paciente com um número válido.", "Atenção!");
                textBox2.Focus();
                return;
            }

            if (comboBox1.SelectedIndex == -1 || Vazio(comboBox1.Text, PH_GENERO))
            {
                MessageBox.Show("Selecione o gênero do paciente.", "Atenção!");
                comboBox1.Focus();
                return;
            }

            //cadastrar prescrição
            Form13 form13 = new Form13(nome);
            form13.StartPosition = FormStartPosition.Manual;
            form13.Location = this.Location;
            form13.Size = this.Size;
            form13.Show();
            this.Close();
        }

        // ================= Resto do código original (sem alterações) =================

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

        private void TxTbxNP_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void Form12_Load(object sender, EventArgs e)
        {

        }

        private void checkBox9_CheckedChanged_1(object sender, EventArgs e)
        {

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

        private void TxTbxNP_Click(object sender, EventArgs e)
        {
            if (TxTbxNP.Text == PH_NOME)
            {
                TxTbxNP.Clear();
            }
        }

        private void TxTbxNP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxTbxNP.Text))
            {
                TxTbxNP.Text = PH_NOME;
            }
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == PH_IDADE)
            {
                textBox2.Clear();
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                textBox2.Text = PH_IDADE;
            }
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == PH_INFO_EXTRA)
            {
                textBox4.Clear();
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                textBox4.Text = PH_INFO_EXTRA;
            }
        }
        }
    }
