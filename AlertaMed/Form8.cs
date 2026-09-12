using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace AlertaMed
{
    public partial class Form8 : Form

    {
        //codigo para fonte personalizada
        private PrivateFontCollection fontes = new PrivateFontCollection();
        private Font fonteMontserrat;
        public Form8()
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
            textBox4.Font = fonteMontserrat;
            textBox5.Font = fonteMontserrat;
        }

        private void button2_Click(object sender, EventArgs e)
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

        }

        private void button4_Click(object sender, EventArgs e)
        {
            
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
            pictureBox1.Image = Properties.Resources.Tela_de_entrar_inst__botão_solicitar_selecionado;
            button1.Image = Properties.Resources.botão_solicitar_selecionado;
            pictureBox1.Image = Properties.Resources.Tela_de_entrar_inst__botão_solicitar_selecionado;
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Tela_de_entrar_inst__botao_solicitar_normal;
            button1.Image = Properties.Resources.botão_solicitar_normal;
            pictureBox1.Image = Properties.Resources.Tela_de_entrar_inst__botao_solicitar_normal;
        }

        private void button7_Enter(object sender, EventArgs e)
        {

        }

        private void button7_Leave(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.StartPosition = FormStartPosition.Manual;
            form2.Location = this.Location;
            form2.Size = this.Size;
            form2.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.StartPosition = FormStartPosition.Manual;
            form6.Location = this.Location;
            form6.Size = this.Size;
            form6.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
