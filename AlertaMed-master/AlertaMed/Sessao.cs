using System;
using Npgsql;

namespace AlertaMed
{
    
    public static class Sessao
    {
        public static int IdUsuario { get; private set; }
        public static string Nome { get; private set; }

       
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

     
        public static void Entrar(int idUsuario, string nome)
        {
            IdUsuario = idUsuario;
            Nome = nome;
            IdInstituicao = 0;
            NomeInstituicao = null;

            GerenciadorAlarmes.Atualizar();   
        }

        
        public static void EntrarNaInstituicao(int idInstituicao, string nomeInstituicao)
        {
            IdInstituicao = idInstituicao;
            NomeInstituicao = nomeInstituicao;

            GerenciadorAlarmes.Atualizar();   
        }

        public static void Sair()
        {
            IdUsuario = 0;
            Nome = null;
            IdInstituicao = 0;
            NomeInstituicao = null;

            GerenciadorAlarmes.Atualizar();  
        }

       
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

       
        public static string CondicaoDono
        {
            get
            {
                if (!Logado) return "1 = 0";
                if (EmInstituicao) return "id_instituicao = @i";
                return "id_usuario = @u AND id_instituicao IS NULL";
            }
        }

       
        public static void AplicarParametro(NpgsqlCommand cmd)
        {
            if (!Logado) return;

            if (EmInstituicao)
                cmd.Parameters.AddWithValue("i", IdInstituicao);
            else
                cmd.Parameters.AddWithValue("u", IdUsuario);
        }

       
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