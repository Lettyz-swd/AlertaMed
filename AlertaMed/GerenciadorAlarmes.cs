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
<<<<<<< HEAD
        private static System.Windows.Forms.Timer timer;
        private static readonly HashSet<string> jaAlertadosHoje = new HashSet<string>();
        private static DateTime diaAtual = DateTime.Today;

        // Chame isso uma vez, no começo do app (Form1 já cuida disso).
        // Se chamar de novo, não faz nada (evita ficar com vários timers rodando).
        public static void Iniciar()
        {
            if (timer != null) return;

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 30000; // checa a cada 30 segundos
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            // vira o dia -> esquece os alarmes de ontem
            if (DateTime.Today != diaAtual)
            {
                diaAtual = DateTime.Today;
                jaAlertadosHoje.Clear();
            }

            string horaAtual = DateTime.Now.ToString("HH:mm");

            var prescricoes = new List<(int id, string paciente, string remedios, string doses, string horarios)>();
=======
        // ---------------------------------------------------------------
        // Preferências do usuário (usadas pela tela de configurações, Form16)
        //
        // Requer duas entradas em Properties > Settings.settings:
        //   NotificacoesAtivas  (bool, escopo Usuário, valor True)
        //   SomAtivo            (bool, escopo Usuário, valor True)
        // ---------------------------------------------------------------
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

        // ---------------------------------------------------------------
        // Alarmes
        // ---------------------------------------------------------------
        private static System.Windows.Forms.Timer timerChecagem;   // checa o relógio, a cada 1 segundo (preciso)
        private static System.Windows.Forms.Timer timerAtualizaDados; // busca o banco, a cada 60 segundos (leve)

        private static readonly HashSet<string> jaAlertadosHoje = new HashSet<string>();
        private static DateTime diaAtual = DateTime.Today;
        private static string horaAnterior = "";

        private static List<(int id, string paciente, string remedios, string doses, string horarios)> prescricoesCache
            = new List<(int, string, string, string, string)>();

        // Chame isso uma vez, no começo do app (Form1 já cuida disso).
        public static void Iniciar()
        {
            if (timerChecagem != null) return;

            AtualizarCache(); // já carrega uma vez de cara, sem esperar 60s

            timerAtualizaDados = new System.Windows.Forms.Timer();
            timerAtualizaDados.Interval = 60000; // atualiza a lista do banco a cada 60 segundos
            timerAtualizaDados.Tick += (s, e) => AtualizarCache();
            timerAtualizaDados.Start();

            timerChecagem = new System.Windows.Forms.Timer();
            timerChecagem.Interval = 1000; // checa o relógio a cada 1 segundo, bem preciso
            timerChecagem.Tick += TimerChecagem_Tick;
            timerChecagem.Start();
        }

        private static void AtualizarCache()
        {
            var lista = new List<(int id, string paciente, string remedios, string doses, string horarios)>();
>>>>>>> 42b777e08f13232eb430626d064dd3eb42fea8dc

            try
            {
                using (NpgsqlConnection conn = Banco.Abrir())
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    "SELECT id_prescricao, nome_paciente, remedios, doses, horarios FROM public.prescricao", conn))
                using (NpgsqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
<<<<<<< HEAD
                        prescricoes.Add((
=======
                        lista.Add((
>>>>>>> 42b777e08f13232eb430626d064dd3eb42fea8dc
                            rd.GetInt32(0),
                            rd.GetString(1),
                            rd.GetString(2),
                            rd.GetString(3),
                            rd.GetString(4)));
                    }
                }
<<<<<<< HEAD
            }
            catch
            {
                return; // sem conexão com o banco nesse ciclo, tenta de novo daqui 30s
            }

            foreach (var p in prescricoes)
=======

                prescricoesCache = lista; // só troca se deu certo
            }
            catch
            {
                // sem conexão nesse ciclo, mantém o cache antigo e tenta de novo daqui 60s
            }
        }

        private static void TimerChecagem_Tick(object sender, EventArgs e)
        {
            // vira o dia -> esquece os alarmes de ontem
            if (DateTime.Today != diaAtual)
            {
                diaAtual = DateTime.Today;
                jaAlertadosHoje.Clear();
            }

            string horaAtual = DateTime.Now.ToString("HH:mm");

            // só processa quando o minuto realmente mudou, evita checar 60x por nada
            if (horaAtual == horaAnterior) return;
            horaAnterior = horaAtual;

            foreach (var p in prescricoesCache)
>>>>>>> 42b777e08f13232eb430626d064dd3eb42fea8dc
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
<<<<<<< HEAD
            // toca o som algumas vezes pra chamar mais atenção
            for (int i = 0; i < 3; i++)
                SystemSounds.Exclamation.Play();

            MessageBox.Show(
                $"Paciente: {paciente}\nRemédio(s): {remedios}\nDose(s): {doses}\nHorário: {horario}",
                "⏰ Hora do remédio!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
=======
            // Notificações desativadas: não aparece alarme nenhum (nem som)
            if (!NotificacoesAtivas) return;

            // Som desativado: o alarme aparece, mas em silêncio
            if (SomAtivo)
            {
                for (int i = 0; i < 3; i++)
                    SystemSounds.Exclamation.Play();
            }

            Form27 alerta = new Form27(paciente, remedios, doses, horario);
            alerta.StartPosition = FormStartPosition.CenterScreen;
            alerta.ShowDialog();
>>>>>>> 42b777e08f13232eb430626d064dd3eb42fea8dc
        }
    }
}