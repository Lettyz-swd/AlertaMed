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
        // evita que o código dispare os eventos dos checkboxes ao marcar/desmarcar
        private bool atualizando = false;

        // tela de onde o usuário veio (para o botão Voltar)
        private readonly Form _telaAnterior;

        // true quando saímos para o Início (nesse caso não reabre a tela anterior)
        private bool _saindoParaOutraTela = false;

        // Construtor vazio: o Designer do Visual Studio precisa dele
        public Form16()
        {
            InitializeComponent();

            // Mapa dos checkboxes na tela:
            //   checkBox1 = Ativar notificações     checkBox2 = Desativar notificações
            //   checkBox4 = Ativar som              checkBox3 = Desativar som
            atualizando = true;
            checkBox1.Checked = GerenciadorAlarmes.NotificacoesAtivas;
            checkBox2.Checked = !GerenciadorAlarmes.NotificacoesAtivas;
            checkBox4.Checked = GerenciadorAlarmes.SomAtivo;
            checkBox3.Checked = !GerenciadorAlarmes.SomAtivo;
            atualizando = false;

            // Liga os eventos (o "-=" antes evita ligar duas vezes)
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

        // Use este construtor nos outros forms: new Form16(this).Show();
        public Form16(Form telaAnterior) : this()
        {
            _telaAnterior = telaAnterior;
        }

        // ---------------------------------------------------------------
        // Checkboxes: cada par funciona como "ativar" / "desativar".
        // Marcar um desmarca o outro e nunca ficam os dois vazios.
        // ---------------------------------------------------------------
        private void AtualizarPar(CheckBox escolhido, CheckBox oposto, bool valor, Action<bool> salvar)
        {
            if (atualizando) return;

            atualizando = true;
            escolhido.Checked = true;   // se tentou desmarcar, mantém marcado
            oposto.Checked = false;
            atualizando = false;

            salvar(valor);
        }

        // Notificações
        // (o nome com _1 é o que o Form16.Designer.cs já usa para o checkBox1)
        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
            => AtualizarPar(checkBox1, checkBox2, true, v => GerenciadorAlarmes.NotificacoesAtivas = v);

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
            => AtualizarPar(checkBox2, checkBox1, false, v => GerenciadorAlarmes.NotificacoesAtivas = v);

        // Som: checkBox4 = "Ativar", checkBox3 = "Desativar"
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
            => AtualizarPar(checkBox4, checkBox3, true, v => GerenciadorAlarmes.SomAtivo = v);

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
            => AtualizarPar(checkBox3, checkBox4, false, v => GerenciadorAlarmes.SomAtivo = v);

        // ---------------------------------------------------------------
        // Efeito dos botões (imagem normal / selecionada)
        // ---------------------------------------------------------------
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

        // ---------------------------------------------------------------
        // Navegação
        // ---------------------------------------------------------------

        // Início
        private void button2_Click(object sender, EventArgs e)
        {
            _saindoParaOutraTela = true;

            Form1 form1 = new Form1();
            form1.StartPosition = FormStartPosition.Manual;
            form1.Location = this.Location;
            form1.Show();
            this.Close();
        }

        // Configurações (já estamos nela)
        private void button3_Click(object sender, EventArgs e)
        {
        }

        // Voltar: fecha esta tela e o FormClosed reabre a tela anterior
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form16_Load(object sender, EventArgs e)
        {
        }

        // Roda sempre que o form fecha (botão Voltar ou o X da janela)
        private void Form16_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_saindoParaOutraTela) return;

            if (_telaAnterior != null && !_telaAnterior.IsDisposed)
                _telaAnterior.Show();
            else
                new Form19().Show();   // plano B, caso o Form16 seja aberto sem informar a tela anterior
        }
    }
}