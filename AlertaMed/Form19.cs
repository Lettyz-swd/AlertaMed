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
    public partial class Form19 : Form
    {
        public Form19()
        {
            
            InitializeComponent();
        }

  
        private void Abrir(Form destino)
        {
            destino.StartPosition = FormStartPosition.Manual;
            destino.Location = this.Location;
            destino.Show();
            this.Close();
        }

    
        private void AbrirHistorico()
        {
            Form14 historico = new Form14(this);
            historico.StartPosition = FormStartPosition.Manual;
            historico.Location = this.Location;
            historico.Show();
            this.Hide();
        }

    

        private void button1_Enter(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.botao_selecionar_selecionado;
            pictureBox1.Image = Properties.Resources.Tela_inicial_uso_pessoal_selecionar_prescrição_selecionado;
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.botao_selecionar_normal;
            pictureBox1.Image = Properties.Resources.Tela_inicial_uso_pessoal;
        }

        private void button3_Enter(object sender, EventArgs e)
        {
            button3.Image = Properties.Resources.botao_selecionar_selecionado;
            pictureBox1.Image = Properties.Resources.Tela_inicial_uso_pessoal_bt_selecionar_perfil_selecionado;
        }

        private void button3_Leave(object sender, EventArgs e)
        {
            button3.Image = Properties.Resources.botao_selecionar_normal;
            pictureBox1.Image = Properties.Resources.Tela_inicial_uso_pessoal;
        }

        private void button4_Enter(object sender, EventArgs e)
        {
            button4.Image = Properties.Resources.botão_inicio_2;
        }

        private void button4_Leave(object sender, EventArgs e)
        {
            button4.Image = Properties.Resources.botão_inicio_normal;
        }

        private void button5_Leave(object sender, EventArgs e)
        {
            button5.Image = Properties.Resources.botão_configurações_normal;
        }

        private void button5_Enter(object sender, EventArgs e)
        {
            button5.Image = Properties.Resources.botão_configurações;
        }

        private void button6_Enter(object sender, EventArgs e)
        {
            button6.Image = Properties.Resources.botão_voltar_cadastro_selecionado;
        }

        private void button6_Leave(object sender, EventArgs e)
        {
            button6.Image = Properties.Resources.botão_voltar_cadastro;
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            Abrir(new Form20());
        }

       
        private void button3_Click(object sender, EventArgs e)
        {
            Abrir(new Form23());
        }

        
        private void button4_Click(object sender, EventArgs e)
        {
            Abrir(new Form1());
        }

       
        private void button5_Click(object sender, EventArgs e)
        {
            new Form16(this).Show();
            this.Hide();
        }

      
        private void button6_Click(object sender, EventArgs e)
        {
            Abrir(new Form1());
        }

      
        private void button7_Click(object sender, EventArgs e)
        {
            AbrirHistorico();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            AbrirHistorico();
        }

       

        private void Form19_Load(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }
    }
}