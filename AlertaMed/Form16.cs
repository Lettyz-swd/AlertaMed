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
    public partial class Form16 : Form
    {
       
        private bool atualizando = false;

      
        private readonly Form _telaAnterior;

     
        private bool _saindoParaOutraTela = false;

        
        public Form16()
        {
            InitializeComponent();

            
            atualizando = true;
            checkBox1.Checked = GerenciadorAlarmes.NotificacoesAtivas;
            checkBox2.Checked = !GerenciadorAlarmes.NotificacoesAtivas;
            checkBox4.Checked = GerenciadorAlarmes.SomAtivo;
            checkBox3.Checked = !GerenciadorAlarmes.SomAtivo;
            atualizando = false;

       
            checkBox1.CheckedChanged -= checkBox1_CheckedChanged_1;
            checkBox2.CheckedChanged -= checkBox2_CheckedChanged;
            checkBox3.CheckedChanged -= checkBox3_CheckedChanged;
            checkBox4.CheckedChanged -= checkBox4_CheckedChanged;
            this.FormClosed -= Form16_FormClosed;

            checkBox1.CheckedChanged += checkBox1_CheckedChanged_1;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            checkBox3.CheckedChanged += checkBox3_CheckedChanged;
            checkBox4.CheckedChanged += checkBox4_CheckedChanged;
            this.FormClosed += Form16_FormClosed;
        }


        public Form16(Form telaAnterior) : this()
        {
            _telaAnterior = telaAnterior;
        }

        
      
        private void AtualizarPar(CheckBox escolhido, CheckBox oposto, bool valor, Action<bool> salvar)
        {
            if (atualizando) return;

            atualizando = true;
            escolhido.Checked = true;  
            oposto.Checked = false;
            atualizando = false;

            salvar(valor);
        }

       
        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
            => AtualizarPar(checkBox1, checkBox2, true, v => GerenciadorAlarmes.NotificacoesAtivas = v);

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
            => AtualizarPar(checkBox2, checkBox1, false, v => GerenciadorAlarmes.NotificacoesAtivas = v);

        
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
            => AtualizarPar(checkBox4, checkBox3, true, v => GerenciadorAlarmes.SomAtivo = v);

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
            => AtualizarPar(checkBox3, checkBox4, false, v => GerenciadorAlarmes.SomAtivo = v);

   
        private void button2_Enter(object sender, EventArgs e)
        {
            button2.Image = Properties.Resources.botão_inicio_2;
        }

        private void button2_Leave(object sender, EventArgs e)
        {
            button2.Image = Properties.Resources.botão_inicio_normal;
        }

        private void button4_Enter(object sender, EventArgs e)
        {
            button4.Image = Properties.Resources.botão_voltar_cadastro_selecionado;
        }

        private void button4_Leave(object sender, EventArgs e)
        {
            button4.Image = Properties.Resources.botão_voltar_cadastro;
        }

        private void button3_Enter(object sender, EventArgs e)
        {
            button3.Image = Properties.Resources.botão_configurações;
        }

        private void button3_Leave(object sender, EventArgs e)
        {
            button3.Image = Properties.Resources.botão_configurações_normal;
        }

       
        private void button2_Click(object sender, EventArgs e)
        {
            _saindoParaOutraTela = true;

            Form1 form1 = new Form1();
            form1.StartPosition = FormStartPosition.Manual;
            form1.Location = this.Location;
            form1.Show();
            this.Close();
        }

      
        private void button3_Click(object sender, EventArgs e)
        {
        }

        
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form16_Load(object sender, EventArgs e)
        {
        }

     
        private void Form16_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_saindoParaOutraTela) return;

            if (_telaAnterior != null && !_telaAnterior.IsDisposed)
                _telaAnterior.Show();
            else
                new Form19().Show();  
        }
    }
}