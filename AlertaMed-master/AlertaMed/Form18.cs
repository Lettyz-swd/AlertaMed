using System;
using System.Linq;
using System.Windows.Forms;
using Npgsql;

namespace AlertaMed
{
    public partial class Form18 : Form
    {
        private string nomePaciente = "";

        public Form18()
        {
            InitializeComponent();
            this.Load += Form18_Load;
        }

        // Construtor usado quando vem do Form12, já com o nome do paciente
        public Form18(string texto) : this()
        {
            nomePaciente = texto;
        }

        private void Form18_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nomePaciente))
            {
                textBox2.Text = nomePaciente;
            }

            CarregarTecnicos();
        }

        // =========================================================
        // Navegação
        // =========================================================
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.StartPosition = FormStartPosition.Manual;
            form1.Location = this.Location;
            form1.Show();
            this.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Form16 form16 = new Form16();
            form16.StartPosition = FormStartPosition.Manual;
            form16.Location = this.Location;
            form16.Show();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // TODO: troque pela tela de onde o usuário costuma chegar até aqui,
            // caso não seja simplesmente fechar/voltar a tela anterior.
            this.Close();
        }

        // =========================================================
        // Hover da barra lateral
        // =========================================================
        private void button1_Enter(object sender, EventArgs e) => button1.Image = Properties.Resources.botão_inicio_2;
        private void button1_Leave(object sender, EventArgs e) => button1.Image = Properties.Resources.botão_inicio_normal;

        private void button10_Enter(object sender, EventArgs e) => button10.Image = Properties.Resources.botão_configurações;
        private void button10_Leave(object sender, EventArgs e) => button10.Image = Properties.Resources.botão_configurações_normal;

        private void button6_Enter(object sender, EventArgs e) => button6.Image = Properties.Resources.botão_voltar_cadastro_selecionado;
        private void button6_Leave(object sender, EventArgs e) => button6.Image = Properties.Resources.botão_voltar_cadastro;

        // =========================================================
        // Hover dos botões de ação (com troca da imagem de fundo)
        // =========================================================
        private void button3_Enter(object sender, EventArgs e)
        {
            button3.Image = Properties.Resources.botao_adicionar_horarios_selecionado;
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_preescrição_bt_remedios_selecionado;
        }

        private void button3_Leave(object sender, EventArgs e)
        {
            button3.Image = Properties.Resources.botao_adicionar_horarios_normal;
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_prescrição_normal;
        }

        private void button4_Enter(object sender, EventArgs e)
        {
            button4.Image = Properties.Resources.botao_adicionar_doses_selecionado;
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_prescrição_bt_doses_selecionado;
        }

        private void button4_Leave(object sender, EventArgs e)
        {
            button4.Image = Properties.Resources.botao_adicionar_doses_normal;
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_prescrição_normal;
        }

        private void button5_Enter(object sender, EventArgs e)
        {
            button5.Image = Properties.Resources.botao_adicionar_horarios_selecionado;
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_prescrição_bt_horarios_selecionado;
        }

        private void button5_Leave(object sender, EventArgs e)
        {
            button5.Image = Properties.Resources.botao_adicionar_horarios_normal;
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_prescrição_normal;
        }

        private void button2_Enter(object sender, EventArgs e)
        {
            button2.Image = Properties.Resources.botao_cadastrar_preescrição_selecionado_2;
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_preescrição_bt_cadastrar_selecionado;
        }

        private void button2_Leave(object sender, EventArgs e)
        {
            button2.Image = Properties.Resources.botao_cadastrar_preescrição_normal_2;
            pictureBox1.Image = Properties.Resources.Tela_cadastrar_prescrição_normal;
        }

        // =========================================================
        // Placeholders dos campos de texto
        // =========================================================
        private void textBox2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "Digite o nome do paciente")
                textBox2.Clear();
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
                textBox2.Text = "Digite o nome do paciente";
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "Digite os remédios")
                textBox3.Clear();
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text))
                textBox3.Text = "Digite os remédios";
        }

        private void textBox5_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == "Digite as doses")
                textBox5.Clear();
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox5.Text))
                textBox5.Text = "Digite as doses";
        }

        // =========================================================
        // Técnico Responsável (carregado a partir de quem se
        // cadastrou pelo Form8 - tabela solicitacao_entrada)
        // =========================================================
        private void CarregarTecnicos()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Selecione o técnico");

            try
            {
                using (NpgsqlConnection conn = Banco.Abrir())
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    "SELECT DISTINCT nome_solicitante FROM public.solicitacao_entrada ORDER BY nome_solicitante", conn))
                using (NpgsqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                        comboBox1.Items.Add(rd.GetString(0));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Não foi possível carregar os técnicos.\n\n" + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            comboBox1.SelectedIndex = 0;
        }

        // =========================================================
        // Remédios
        // =========================================================
        private void button3_Click(object sender, EventArgs e)
        {
            string valor = textBox3.Text.Trim();
            if (string.IsNullOrEmpty(valor) || valor == "Digite os remédios")
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

        // =========================================================
        // Doses
        // =========================================================
        private void button4_Click(object sender, EventArgs e)
        {
            string valor = textBox5.Text.Trim();
            if (string.IsNullOrEmpty(valor) || valor == "Digite as doses")
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

        // =========================================================
        // Horários (MaskedTextBox, máscara 00:00)
        // =========================================================
        private void HorarioCampo_Enter(object sender, EventArgs e)
        {
            ((MaskedTextBox)sender).SelectAll();
        }

        private void HorarioCampo_Leave(object sender, EventArgs e)
        {
            var campo = (MaskedTextBox)sender;

            if (!campo.MaskCompleted)
            {
                campo.Clear();
                return;
            }

            bool valido = System.Text.RegularExpressions.Regex.IsMatch(
                campo.Text,
                @"^([01]\d|2[0-3]):[0-5]\d$");

            if (!valido)
            {
                MessageBox.Show("Horário inválido. Use um horário entre 00:00 e 23:59.",
                                "Valor inválido",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                campo.Focus();
                campo.SelectAll();
            }
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

        // =========================================================
        // Utilitários das listas (Remédios / Doses / Horários)
        // =========================================================
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

        // =========================================================
        // Concluído! Cadastrar Prescrição
        // =========================================================
        private void button2_Click(object sender, EventArgs e)
        {
            string nomePacienteAtual = textBox2.Text.Trim();
            if (string.IsNullOrEmpty(nomePacienteAtual) || nomePacienteAtual == "Digite o nome do paciente")
            {
                MessageBox.Show("Digite o nome do paciente.");
                textBox2.Focus();
                return;
            }

            if (comboBox1.SelectedIndex <= 0)
            {
                MessageBox.Show("Selecione o técnico responsável.");
                comboBox1.Focus();
                return;
            }
            string tecnico = comboBox1.SelectedItem.ToString();

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

            try
            {
                using (NpgsqlConnection conn = Banco.Abrir())
                using (NpgsqlCommand cmd = new NpgsqlCommand(@"
                    INSERT INTO public.prescricao
                        (nome_paciente, tecnico_responsavel, remedios, doses, horarios)
                    VALUES (@paciente, @tecnico, @remedios, @doses, @horarios)", conn))
                {
                    cmd.Parameters.AddWithValue("paciente", nomePacienteAtual);
                    cmd.Parameters.AddWithValue("tecnico", tecnico);
                    cmd.Parameters.AddWithValue("remedios", textBox4.Text);
                    cmd.Parameters.AddWithValue("doses", textBox6.Text);
                    cmd.Parameters.AddWithValue("horarios", textBox10.Text);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar a prescrição:\n\n" + ex.Message);
                MessageBox.Show("Erro ao cadastrar a prescrição:\n\n" + ex.Message);
                return; // não navega se o salvamento falhou
            }

            MessageBox.Show("Prescrição cadastrada com sucesso!");

            Form21 form21 = new Form21();
            form21.StartPosition = FormStartPosition.Manual;
            form21.Location = this.Location;
            form21.Show();
            this.Close();
        }
    }
}