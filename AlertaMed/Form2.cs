using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace AlertaMed
{
    public partial class Form2 : Form
    {
        private PrivateFontCollection fontes = new PrivateFontCollection();
        private Font fonteMontserrat;

        public Form2()
        {
            InitializeComponent();
            Assembly assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream("AlertaMed.Montserrat-Arabic Regular.ttf"))
            {
                byte[] dados = new byte[stream.Length];
                stream.Read(dados, 0, dados.Length);

                IntPtr memoria = Marshal.AllocCoTaskMem(dados.Length);

                try
                {
                    Marshal.Copy(dados, 0, memoria, dados.Length);
                    fontes.AddMemoryFont(memoria, dados.Length);
                }
                finally
                {
                    Marshal.FreeCoTaskMem(memoria);
                }
            }

            fonteMontserrat = new Font(fontes.Families[0], 12);

            textBox1.Font = fonteMontserrat;
            textBox2.Font = fonteMontserrat;
            textBox3.Font = fonteMontserrat;
            comboBox1.Font = fonteMontserrat;

            button4.Image = Properties.Resources.botão_voltar_cadastro;
            button5.Image = Properties.Resources.botão_olho_;
            button1.Image = Properties.Resources.botão_continuar_normal;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.StartPosition = FormStartPosition.Manual;
            form1.Location = this.Location;
            form1.Size = this.Size;
            form1.Show();
            this.Close();
            button2.Image = Properties.Resources.botão_inicio_3;
            button3.Image = Properties.Resources.botão_configurações_normal;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool temDados = false;

            if ((textBox1.Text.Trim() != "" && textBox1.Text != "Digite seu nome") ||
                (textBox2.Text.Trim() != "" && textBox2.Text != "Digite seu e-mail") ||
                (textBox3.Text.Trim() != "" && textBox3.Text != "Digite sua senha") ||
                comboBox1.SelectedIndex != -1)
            {
                temDados = true;
            }

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
            form5.Size = this.Size;

            form5.Show();
            this.Close();

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.StartPosition = FormStartPosition.Manual;
            form4.Location = this.Location;
            form4.Size = this.Size;
            form4.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Enter(object sender, EventArgs e)
        {
            button4.Image = Properties.Resources.botão_voltar_cadastro_selecionado;
        }

        private void button4_Leave(object sender, EventArgs e)
        {
            button4.Image = Properties.Resources.botão_voltar_cadastro;
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //configurar 
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //configurar 
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (textBox3.PasswordChar == '\0')
            {
                textBox3.PasswordChar = '●';
                button5.Image = Properties.Resources.botão_olho_riscado;
            }
            else
            {
                textBox3.PasswordChar = '\0';
                button5.Image = Properties.Resources.botão_olho_;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void button6_Click_1(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form9 form9 = new Form9();
            form9.StartPosition = FormStartPosition.Manual;
            form9.Location = this.Location;
            form9.Size = this.Size;
            form9.Show();
            this.Hide();
        }

        private void button7_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void button7_Enter(object sender, EventArgs e)
        {
            
        }

        private void button7_Leave(object sender, EventArgs e)
        {
            
        }

        private void button1_Enter(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.botão_continuar_selecionado;
            pictureBox1.Image = Properties.Resources.Tela_botão_continuar_selecionado;

        }

        private void button1_Leave(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.botão_continuar_normal;
            pictureBox1.Image = Properties.Resources.Tela_botão_continuar_normal;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //configurar 
        }
    }
}
