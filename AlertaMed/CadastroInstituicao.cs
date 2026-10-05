namespace AlertaMed
{

    public static class CadastroInstituicao
    {
        public static string Nome;
        public static string Tipo;
        public static string Email;
        public static string SenhaHash;  

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