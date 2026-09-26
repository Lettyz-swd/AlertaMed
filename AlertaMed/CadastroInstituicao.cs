namespace AlertaMed
{
    // Guarda os dados da instituição enquanto o usuário passa do Form2 para o Form4.
    // Nada é gravado no banco aqui: a gravação acontece só no Form4, quando o dono
    // clica em Cadastrar (assim ninguém cria uma instituição sem dono).
    public static class CadastroInstituicao
    {
        public static string Nome;
        public static string Tipo;
        public static string Email;
        public static string SenhaHash;   // já em hash, nunca a senha pura

        public static bool Preenchido
        {
            get
            {
                return !string.IsNullOrEmpty(Nome)
                    && !string.IsNullOrEmpty(Tipo)
                    && !string.IsNullOrEmpty(Email)
                    && !string.IsNullOrEmpty(SenhaHash);
            }
        }

        public static void Limpar()
        {
            Nome = null;
            Tipo = null;
            Email = null;
            SenhaHash = null;
        }
    }
}