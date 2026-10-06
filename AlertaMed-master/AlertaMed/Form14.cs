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
    public partial class Form14 : Form
    {
        public string textoGuardado { get; private set; } = "";

       
        private TextBox[] colPaciente;
        private TextBox[] colRemedios;
        private TextBox[] colDoses;
        private TextBox[] colHorarios;

      
        private readonly Form _telaAnterior;

     
        public Form14()
        {
            
            InitializeComponent();

            colPaciente = new[] { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6 };
            colRemedios = new[] { textBox7, textBox8, textBox9, textBox10, textBox11, textBox12 };
            colDoses = new[] { textBox13, textBox14, textBox15, textBox16, textBox17, textBox18 };
            colHorarios = new[] { textBox19, textBox20, textBox21, textBox22, textBox23, textBox24 };

            this.Load -= Form14_Load;
            this.Load += Form14_Load;
        }

        
        public Form14(Form telaAnterior) : this()
        {
            _telaAnterior = telaAnterior;
        }

        
        public Form14(string remedios, string doses1, string doses2, string nomePaciente, Form telaAnterior = null)
            : this()
        {
            _telaAnterior = telaAnterior;
        }

        private void Form14_Load(object sender, EventArgs e)
        {
            CarregarHistorico();
        }

        private void CarregarHistorico()
        {
           
            for (int i = 0; i < colPaciente.Length; i++)
            {
                colPaciente[i].Clear();
                colRemedios[i].Clear();
                colDoses[i].Clear();
                colHorarios[i].Clear();
            }

            try
            {
                using (NpgsqlConnection conn = Banco.Abrir())
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    @"SELECT nome_paciente, remedios, doses, horarios
                      FROM public.prescricao
                      WHERE " + Sessao.CondicaoDono + @"
                      ORDER BY data_cadastro DESC
                      LIMIT 6", conn))
                {
                    Sessao.AplicarParametro(cmd);

                    using (NpgsqlDataReader rd = cmd.ExecuteReader())
                    {
                        int linha = 0;
                        while (rd.Read() && linha < colPaciente.Length)
                        {
                            colPaciente[linha].Text = rd.IsDBNull(0) ? "" : rd.GetString(0);
                            colRemedios[linha].Text = rd.IsDBNull(1) ? "" : rd.GetString(1);
                            colDoses[linha].Text = rd.IsDBNull(2) ? "" : rd.GetString(2);
                            colHorarios[linha].Text = rd.IsDBNull(3) ? "" : rd.GetString(3);
                            linha++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Não foi possível carregar o histórico.\n\n" + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
         
            Form1 form1 = new Form1();
            form1.StartPosition = FormStartPosition.Manual;
            form1.Location = this.Location;
            form1.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
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

        private void button10_Enter(object sender, EventArgs e)
        {
            button10.Image = Properties.Resources.botão_configurações;
        }

        private void button10_Leave(object sender, EventArgs e)
        {
            button10.Image = Properties.Resources.botão_configurações_normal;
        }

        private void button6_Enter(object sender, EventArgs e)
        {
            button6.Image = Properties.Resources.botão_voltar_cadastro_selecionado;
        }

        private void button6_Leave(object sender, EventArgs e)
        {
            button6.Image = Properties.Resources.botão_voltar_cadastro;
        }

        private void button2_Enter(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Tela_historico_prescrição_bt_ok_selecionado_1;
            button2.Image = Properties.Resources.botao_ok_voltar_a_tela_selecionado;
            pictureBox1.Image = Properties.Resources.Tela_historico_prescrição_bt_ok_selecionado_1;
        }

        private void button2_Leave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Tela_historico_prescrição_normal_1;
            button2.Image = Properties.Resources.botão_ok_voltar_a_tela_normal;
            pictureBox1.Image = Properties.Resources.Tela_historico_prescrição_normal_1;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
            Form1 form1 = new Form1();
            form1.StartPosition = FormStartPosition.Manual;
            form1.Location = this.Location;
            form1.Show();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            Form destino = _telaAnterior ?? new Form19();

            destino.StartPosition = FormStartPosition.Manual;
            destino.Location = this.Location;
            destino.Show();
            this.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form22 form22 = new Form22();
            form22.StartPosition = FormStartPosition.Manual;
            form22.Location = this.Location;
            form22.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            new Form16(this).Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}