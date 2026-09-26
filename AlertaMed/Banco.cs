using Npgsql;

namespace AlertaMed
{
    public static class Banco
    {
        public static string ConnString =
<<<<<<< HEAD
            "Host=localhost;Port=5432;Username=postgres;Password=admin;Database=alertamed";
=======
            "Host=localhost;Port=5432;Username=postgres;Password=pgadmin;Database=alertamed";
>>>>>>> 89ea95c1bff83eaaaf44d8b7db40a0afe5812bfa

        public static NpgsqlConnection Abrir()
        {
            var conn = new NpgsqlConnection(ConnString);
            conn.Open();
            return conn;
        }
    }
}