using Npgsql;

namespace AlertaMed
{
    public static class Banco
    {
        public static string ConnString =
            "Host=localhost;Port=5432;Username=postgres;Password=pgadmin;Database=alertamed";

        public static NpgsqlConnection Abrir()
        {
            var conn = new NpgsqlConnection(ConnString);
            conn.Open();
            return conn;
        }
    }
}