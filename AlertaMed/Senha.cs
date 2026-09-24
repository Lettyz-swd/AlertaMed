using System;
using System.Security.Cryptography;

namespace AlertaMed
{
    public static class Senha
    {
        public static string Gerar(string senha)
        {
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
                rng.GetBytes(salt);
            byte[] hash = new Rfc2898DeriveBytes(senha, salt, 100000).GetBytes(32);
            return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
        }

        public static bool Conferir(string senha, string armazenada)
        {
            string[] partes = armazenada.Split(':');
            byte[] salt = Convert.FromBase64String(partes[0]);
            byte[] hashSalvo = Convert.FromBase64String(partes[1]);
            byte[] hashNovo = new Rfc2898DeriveBytes(senha, salt, 100000).GetBytes(32);
            if (hashSalvo.Length != hashNovo.Length) return false;
            int dif = 0;
            for (int i = 0; i < hashSalvo.Length; i++) dif |= hashSalvo[i] ^ hashNovo[i];
            return dif == 0;
        }
    }
}