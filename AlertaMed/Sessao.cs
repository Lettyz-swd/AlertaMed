using Npgsql;

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

        // Diz se a pessoa logada é dono da instituição.
        // Se ela já entrou numa instituição, checa aquela; senão, checa se é dono de alguma.
        public static bool EhDono()
        {
            if (!Logado) return false;

            string sql = "SELECT 1 FROM public.membro_instituicao " +
                         "WHERE id_usuario = @u AND papel = 'dono'";
            if (EmInstituicao) sql += " AND id_instituicao = @i";
            sql += " LIMIT 1";

            using (NpgsqlConnection conn = Banco.Abrir())
            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("u", IdUsuario);
                if (EmInstituicao) cmd.Parameters.AddWithValue("i", IdInstituicao);
                return cmd.ExecuteScalar() != null;
            }
        }

        // Devolve "dono", "membro" ou null (se a pessoa não está em nenhuma instituição).
        // Se houver mais de um papel, o de dono tem prioridade.
        public static string ObterPapel()
        {
            if (!Logado) return null;

            string sql = "SELECT papel FROM public.membro_instituicao WHERE id_usuario = @u";
            if (EmInstituicao) sql += " AND id_instituicao = @i";
            sql += " ORDER BY (papel = 'dono') DESC LIMIT 1";

            using (NpgsqlConnection conn = Banco.Abrir())
            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("u", IdUsuario);
                if (EmInstituicao) cmd.Parameters.AddWithValue("i", IdInstituicao);
                return cmd.ExecuteScalar() as string;
            }
        }
    }
}