using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Windows.Forms;
using Npgsql;

namespace AlertaMed
{
    public static class GerenciadorAlarmes
    {
       
        public static bool NotificacoesAtivas
        {
            get => Properties.Settings.Default.NotificacoesAtivas;
            set
            {
                Properties.Settings.Default.NotificacoesAtivas = value;
                Properties.Settings.Default.Save();
            }
        }

        public static bool SomAtivo
        {
            get => Properties.Settings.Default.SomAtivo;
            set
            {
                Properties.Settings.Default.SomAtivo = value;
                Properties.Settings.Default.Save();
            }
        }

       
        private static System.Windows.Forms.Timer timerChecagem;   
        private static System.Windows.Forms.Timer timerAtualizaDados; 

        private static readonly HashSet<string> jaAlertadosHoje = new HashSet<string>();
        private static DateTime diaAtual = DateTime.Today;
        private static string horaAnterior = "";

        private static List<(int id, string paciente, string remedios, string doses, string horarios)> prescricoesCache
            = new List<(int, string, string, string, string)>();

       
        public static void Iniciar()
        {
            if (timerChecagem != null) return;

            AtualizarCache(); 

            timerAtualizaDados = new System.Windows.Forms.Timer();
            timerAtualizaDados.Interval = 60000; 
            timerAtualizaDados.Tick += (s, e) => AtualizarCache();
            timerAtualizaDados.Start();

            timerChecagem = new System.Windows.Forms.Timer();
            timerChecagem.Interval = 1000; 
            timerChecagem.Tick += TimerChecagem_Tick;
            timerChecagem.Start();
        }

        
        public static void Atualizar()
        {
            AtualizarCache();
        }

        private static void AtualizarCache()
        {
           
            if (!Sessao.Logado)
            {
                prescricoesCache = new List<(int, string, string, string, string)>();
                return;
            }

            var lista = new List<(int id, string paciente, string remedios, string doses, string horarios)>();

            try
            {
                using (NpgsqlConnection conn = Banco.Abrir())
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    @"SELECT id_prescricao, nome_paciente, remedios, doses, horarios
                      FROM public.prescricao
                      WHERE " + Sessao.CondicaoDono, conn))
                {
                    Sessao.AplicarParametro(cmd);

                    using (NpgsqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add((
                                rd.GetInt32(0),
                                rd.GetString(1),
                                rd.GetString(2),
                                rd.GetString(3),
                                rd.GetString(4)));
                        }
                    }
                }

                prescricoesCache = lista; 
            }
            catch
            {
                
            }
        }

        private static void TimerChecagem_Tick(object sender, EventArgs e)
        {
            
            if (DateTime.Today != diaAtual)
            {
                diaAtual = DateTime.Today;
                jaAlertadosHoje.Clear();
            }

            string horaAtual = DateTime.Now.ToString("HH:mm");

            
            if (horaAtual == horaAnterior) return;
            horaAnterior = horaAtual;

            foreach (var p in prescricoesCache)
            {
                var horariosDaPrescricao = p.horarios
                    .Split(new[] { Environment.NewLine, "|" }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(h => h.Trim())
                    .Where(h => h.Length > 0);

                foreach (var h in horariosDaPrescricao)
                {
                    if (h != horaAtual) continue;

                    string chave = $"{p.id}_{h}_{diaAtual:yyyyMMdd}";
                    if (jaAlertadosHoje.Contains(chave)) continue;

                    jaAlertadosHoje.Add(chave);
                    Disparar(p.paciente, p.remedios, p.doses, h);
                }
            }
        }

        private static void Disparar(string paciente, string remedios, string doses, string horario)
        {
            
            if (!NotificacoesAtivas) return;

           
            if (SomAtivo)
            {
                for (int i = 0; i < 3; i++)
                    SystemSounds.Exclamation.Play();
            }

            Form27 alerta = new Form27(paciente, remedios, doses, horario);
            alerta.StartPosition = FormStartPosition.CenterScreen;
            alerta.ShowDialog();
        }
    }
}