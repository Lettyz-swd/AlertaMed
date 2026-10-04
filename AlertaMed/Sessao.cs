using System;
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

            GerenciadorAlarmes.Atualizar();   // recarrega os alarmes da conta que entrou
        }

        // Chame depois do Entrar, quando a pessoa entra em uma instituição
        public static void EntrarNaInstituicao(int idInstituicao, string nomeInstituicao)
        {
            IdInstituicao = idInstituicao;
            NomeInstituicao = nomeInstituicao;

            GerenciadorAlarmes.Atualizar();   // agora vale a instituição
        }

        public static void Sair()
        {
            IdUsuario = 0;
            Nome = null;
            IdInstituicao = 0;
            NomeInstituicao = null;

            GerenciadorAlarmes.Atualizar();   // esvazia os alarmes
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

        // ===============================================================
        // PRESCRIÇÕES POR CONTA
        //
        //  - Dentro de uma instituição (EmInstituicao): só as prescrições daquela instituição.
        //  - Uso pessoal (sem instituição): só as do próprio usuário, que não são de instituição.
        //  - Ninguém logado: nada.
        // ===============================================================

        // Pedaço de WHERE para consultas em public.prescricao
        public static string CondicaoDono
        {
            get
            {
                if (!Logado) return "1 = 0";
                if (EmInstituicao) return "id_instituicao = @i";
                return "id_usuario = @u AND id_instituicao IS NULL";
            }
        }

        // Preenche os parâmetros usados pela CondicaoDono
        public static void AplicarParametro(NpgsqlCommand cmd)
        {
            if (!Logado) return;

            if (EmInstituicao)
                cmd.Parameters.AddWithValue("i", IdInstituicao);
            else
                cmd.Parameters.AddWithValue("u", IdUsuario);
        }

        // Para o INSERT em public.prescricao: use as colunas id_usuario e id_instituicao
        // com os valores @id_usuario e @id_instituicao e chame este método.
        //  - id_usuario    = quem cadastrou (sempre preenchido)
        //  - id_instituicao = só preenchido quando o cadastro é feito dentro de uma instituição
        public static void AplicarDonoNoInsert(NpgsqlCommand cmd)
        {
            cmd.Parameters.Add(new NpgsqlParameter("@id_usuario", NpgsqlTypes.NpgsqlDbType.Integer)
            {
                Value = IdUsuario
            });

            cmd.Parameters.Add(new NpgsqlParameter("@id_instituicao", NpgsqlTypes.NpgsqlDbType.Integer)
            {
                Value = EmInstituicao ? (object)IdInstituicao : DBNull.Value
            });
        }
    }
}