using System;
using Npgsql;

namespace AlertaMed
{
    public class Paciente
    {
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string Genero { get; set; }
        public decimal Peso { get; set; }
        public string EstadoCivil { get; set; }
        public bool TemFilhos { get; set; }
        public int? QuantidadeFilhos { get; set; }
        public bool DoencasRespiratorias { get; set; }
        public string QuaisDoencasRespiratorias { get; set; }
        public bool DoencasCardiovasculares { get; set; }
        public string QuaisDoencasCardiovasculares { get; set; }
        public bool TemAlergias { get; set; }
        public string QuaisAlergias { get; set; }
        public string InformacoesExtras { get; set; }
        public string Anotacoes { get; set; }
        public int IdUsuario { get; set; }

       
        public void Salvar()
        {
            using (var conn = Banco.Abrir())
            {
                var cmd = new NpgsqlCommand(@"
                    INSERT INTO paciente
                    (nome, idade, genero, peso, estado_civil, tem_filhos, quantidade_filhos,
                     doencas_respiratorias, quais_doencas_respiratorias,
                     doencas_cardiovasculares, quais_doencas_cardiovasculares,
                     tem_alergias, quais_alergias, informacoes_extras, anotacoes, id_usuario)
                    VALUES
                    (@nome, @idade, @genero, @peso, @estadoCivil, @temFilhos, @qtdFilhos,
                     @doencasResp, @quaisResp, @doencasCard, @quaisCard,
                     @temAlergias, @quaisAlergias, @infoExtras, @anotacoes, @idUsuario)", conn);

                cmd.Parameters.AddWithValue("nome", Nome);
                cmd.Parameters.AddWithValue("idade", Idade);
                cmd.Parameters.AddWithValue("genero", (object)Genero ?? DBNull.Value);
                cmd.Parameters.AddWithValue("peso", Peso);
                cmd.Parameters.AddWithValue("estadoCivil", (object)EstadoCivil ?? DBNull.Value);
                cmd.Parameters.AddWithValue("temFilhos", TemFilhos);
                cmd.Parameters.AddWithValue("qtdFilhos", (object)QuantidadeFilhos ?? DBNull.Value);
                cmd.Parameters.AddWithValue("doencasResp", DoencasRespiratorias);
                cmd.Parameters.AddWithValue("quaisResp", (object)QuaisDoencasRespiratorias ?? DBNull.Value);
                cmd.Parameters.AddWithValue("doencasCard", DoencasCardiovasculares);
                cmd.Parameters.AddWithValue("quaisCard", (object)QuaisDoencasCardiovasculares ?? DBNull.Value);
                cmd.Parameters.AddWithValue("temAlergias", TemAlergias);
                cmd.Parameters.AddWithValue("quaisAlergias", (object)QuaisAlergias ?? DBNull.Value);
                cmd.Parameters.AddWithValue("infoExtras", (object)InformacoesExtras ?? DBNull.Value);
                cmd.Parameters.AddWithValue("anotacoes", (object)Anotacoes ?? DBNull.Value);
                cmd.Parameters.AddWithValue("idUsuario", IdUsuario);

                cmd.ExecuteNonQuery();
            }
        }
    }
}