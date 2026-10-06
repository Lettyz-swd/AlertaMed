using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlertaMed
{
    public partial class Form27 : Form
    {
        private System.Windows.Forms.Timer timerContador;
        private DateTime horaAbertura;
        private string horarioOriginal;
        private SoundPlayer somAlarme;

        public Form27()
        {
            InitializeComponent();
        }

       
        public Form27(string paciente, string remedio, string doses, string horario) : this()
        {
            label4.Text = paciente;
            label2.Text = remedio;
            label3.Text = doses;

            horarioOriginal = horario;
            horaAbertura = DateTime.Now;
            label5.Text = $"{horarioOriginal}   •   00:00";

            timerContador = new System.Windows.Forms.Timer();
            timerContador.Interval = 1000;
            timerContador.Tick += TimerContador_Tick;
            timerContador.Start();

            
            somAlarme = new SoundPlayer(Properties.Resources.som_alarme); 
            somAlarme.PlayLooping();

            
            this.FormClosed += (s, e) =>
            {
                timerContador.Stop();
                somAlarme.Stop();
            };
        }

        private void TimerContador_Tick(object sender, EventArgs e)
        {
            TimeSpan decorrido = DateTime.Now - horaAbertura;
            label5.Text = $"{horarioOriginal}   •   {decorrido:mm\\:ss}";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            somAlarme.Stop();
            this.Close();
        }

        private void Form27_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }
    }
}