using System;
using System.IO;

namespace AlertaMed
{
    // Guarda as preferências do app (por usuário) num arquivo em %AppData%\AlertaMed.
    // Uso em qualquer tela:  if (Configuracoes.NotificacoesAtivas) { ... }
    public static class Configuracoes
    {
        private static string Arquivo
        {
            get
            {
                string pasta = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "AlertaMed");
                Directory.CreateDirectory(pasta);
                return Path.Combine(pasta, "config_" + Sessao.IdUsuario + ".txt");
            }
        }

        // Padrão: notificações ativadas
        public static bool NotificacoesAtivas
        {
            get { return Ler("notificacoes", true); }
            set { Gravar("notificacoes", value); }
        }

        private static bool Ler(string chave, bool padrao)
        {
            try
            {
                if (!File.Exists(Arquivo)) return padrao;
                foreach (string linha in File.ReadAllLines(Arquivo))
                {
                    string[] p = linha.Split('=');
                    if (p.Length == 2 && p[0] == chave)
                    {
                        bool v;
                        if (bool.TryParse(p[1], out v)) return v;
                    }
                }
            }
            catch { }
            return padrao;
        }

        private static void Gravar(string chave, bool valor)
        {
            try
            {
                File.WriteAllText(Arquivo, chave + "=" + valor);
            }
            catch { }
        }
    }
}
