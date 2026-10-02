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

            try
            {
                using (NpgsqlConnection conn = Banco.Abrir())
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    "SELECT id_prescricao, nome_paciente, remedios, doses, horarios FROM public.prescricao", conn))
                using (NpgsqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        prescricoes.Add((
                            rd.GetInt32(0),
                            rd.GetString(1),
                            rd.GetString(2),
                            rd.GetString(3),
                            rd.GetString(4)));
                    }
                }
            }
            catch
            {
                return; // sem conexão com o banco nesse ciclo, tenta de novo daqui 30s
            }

            foreach (var p in prescricoes)
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
            // toca o som algumas vezes pra chamar mais atenção
            for (int i = 0; i < 3; i++)
                SystemSounds.Exclamation.Play();

            MessageBox.Show(
                $"Paciente: {paciente}\nRemédio(s): {remedios}\nDose(s): {doses}\nHorário: {horario}",
                "⏰ Hora do remédio!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}