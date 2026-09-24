namespace AlertaMed
{
    // Guarda quem está logado enquanto o programa está aberto.
    // Como é "static", qualquer tela pode usar: Sessao.IdUsuario, Sessao.Nome
    public static class Sessao
    {
        public static int IdUsuario { get; private set; }
        public static string Nome { get; private set; }

        public static bool Logado
        {
            get { return IdUsuario > 0; }
        }

        public static void Entrar(int idUsuario, string nome)
        {
            IdUsuario = idUsuario;
            Nome = nome;
        }

        public static void Sair()
        {
            IdUsuario = 0;
            Nome = null;
        }
    }
}