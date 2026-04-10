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

        public static void ValidarPatrimonio(string patrimonio)
        {
            if (string.IsNullOrWhiteSpace(patrimonio))
                throw new DomainException("Nome do patrimônio");
        }

        public static void ValidarLogradoura(string logradouro)
        {
            if (string.IsNullOrWhiteSpace(logradouro))
                throw new DomainException("Nome do Logradouro");
        }

        public static void ValidarNIF(string NIF)
        {
            if (string.IsNullOrWhiteSpace(NIF))
                throw new DomainException("NIF Obrigatório");
        }


        public static void ValidarCPF(string CPF)
        {
            if (string.IsNullOrWhiteSpace(CPF))
                throw new DomainException("CPF Obrigatório");
        }

        public static void ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("Email Obrigatório");
        }

        public static void ValidarSenha(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
                throw new DomainException("Senha é obrigatória");
        }

        public static void ValidarJustificativa(string justificativa)
        {
            if (string.IsNullOrWhiteSpace(justificativa))
            {
                throw new DomainException("Justificativa é obrigatória.");
            }
        }

        public static void ValidarNumeroPatrimonio(string numeroPatrimonio)
        {
            if (string.IsNullOrWhiteSpace(numeroPatrimonio))
                throw new DomainException("Número de patrimônio é obrigatório");
        }




        }
}
