using AssetControlAPI_.Exceptions;

namespace AssetControlAPI_.Applications.Regras
{
    public class ValidarCriacaoDTO
    {

        public static void ValidarNome(string nome)
        {
            if (nome == null || string.IsNullOrEmpty(nome))
            {
                throw new DomainException("Nome é obrigatório");
            }
        }

        public static void ValidarEstado(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
            {
                throw new DomainException("Estado é obrigatório.");
            }
        }

        //public static void Adicionar

    }
}
