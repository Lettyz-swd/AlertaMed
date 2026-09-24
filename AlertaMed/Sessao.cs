namespace AlertaMed
{
    // Guarda quem está logado enquanto o programa está aberto.
    // Como é "static", qualquer tela pode usar: Sessao.IdUsuario, Sessao.Nome,
    // Sessao.IdInstituicao, Sessao.NomeInstituicao
    public static class Sessao
    {
        public static int IdUsuario { get; private set; }
        public static string Nome { get; private set; }

        // Instituição em que a pessoa entrou (0 = nenhuma)
        public static int IdInstituicao { get; private set; }
        public static string NomeInstituicao { get; private set; }

        public static bool Logado
        {
            get { return IdUsuario > 0; }
        }

        public static bool EmInstituicao
        {
            get { return IdInstituicao > 0; }
        }

        // Login de pessoa (limpa qualquer instituição de uma sessão anterior)
        public static void Entrar(int idUsuario, string nome)
        {
            IdUsuario = idUsuario;
            Nome = nome;
            IdInstituicao = 0;
            NomeInstituicao = null;
        }

        // Chame depois do Entrar, quando a pessoa entra em uma instituição
        public static void EntrarNaInstituicao(int idInstituicao, string nomeInstituicao)
        {
            IdInstituicao = idInstituicao;
            NomeInstituicao = nomeInstituicao;
        }

        public static void Sair()
        {
            IdUsuario = 0;
            Nome = null;
            IdInstituicao = 0;
            NomeInstituicao = null;
        }
    }
}