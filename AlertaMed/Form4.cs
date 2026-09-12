using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AlertaMed
{
    public partial class Form4 : Form
    {
        //codigo para fonte personalizada
        private PrivateFontCollection fontes = new PrivateFontCollection();
        private Font fonteMontserrat;

        //codigo para conseguir mudar o fundo do datetime 
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);

        public Form4()
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
            //botões para ficar com fonte personalizada
            fonteMontserrat = new Font(fontes.Families[0], 12);

            textBox1.Font = fonteMontserrat;
            textBox2.Font = fonteMontserrat;
            textBox3.Font = fonteMontserrat;

            
            button5.Image = Properties.Resources.botão_olho_;

            //personalização ativa do datetime
            SetWindowTheme(dateTimePicker1.Handle, "", ""); // libera BackColor/ForeColor
            dateTimePicker1.BackColor = ColorTranslator.FromHtml("#F9FDFE"); 
            dateTimePicker1.ForeColor = Color.FromArgb(50, 50, 50); // combine com a cor de texto dos outros campos

            dateTimePicker1.MinDate = new DateTime(1900, 1, 1);
            dateTimePicker1.MaxDate = DateTime.Today.AddYears(-18);
            dateTimePicker1.Value = DateTime.Today.AddYears(-18);

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

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool temDados = false;

            if ((textBox1.Text.Trim() != "" && textBox1.Text != "Digite seu nome") ||
                (textBox2.Text.Trim() != "" && textBox2.Text != "Digite seu e-mail") ||
                (textBox3.Text.Trim() != "" && textBox3.Text != "Digite sua senha") ||
                dateTimePicker1.Value.Date != DateTime.Today)
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

            Form2 form2 = new Form2();

            form2.StartPosition = FormStartPosition.Manual;
            form2.Location = this.Location;
            form2.Size = this.Size;

            form2.Show();
            this.Close();

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

        private void button4_Enter(object sender, EventArgs e)
        {
            button4.Image = Properties.Resources.botão_voltar_cadastro_selecionado;
        }

        private void button4_Leave(object sender, EventArgs e)
        {
            button4.Image = Properties.Resources.botão_voltar_cadastro;
        }

        private void button1_Enter(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.botão_cadastrar_2_selecionado;
            pictureBox1.Image = Properties.Resources.Tela_de_continuar_o_cadastro_selecionado_2;
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.botão_cadastrar_2;
            pictureBox1.Image = Properties.Resources.Tela_de_continuar_o_cadastro_normal_2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7();
            form7.StartPosition = FormStartPosition.Manual;
            form7.Location = this.Location;
            form7.Size = this.Size;
            form7.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //configurar 
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //configurar 
        }
    }
}
