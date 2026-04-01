using AssetControlAPI_.Applications.DTOs.AreaDTO;
using AssetControlAPI_.Applications.DTOs.UsuarioDTO;
using AssetControlAPI_.Domains;
using AssetControlAPI_.Interface;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace AssetControlAPI_.Applications.Services
{
    public class UsuarioService
    {

        private readonly IUsuarioRepository _repository;
        public UsuarioService(IUsuarioRepository repository) => _repository = repository;

        public List<ListarUsuarioDTO> Listar()
        {
            List<Usuario> usuarios = _repository.Listar();

            List<ListarUsuarioDTO> listarUsuarios = usuarios.Select(varAux => new ListarUsuarioDTO
            {

                Nome = varAux.Nome,
                CPF = varAux.CPF,
                CargoId = varAux.CargoId,
                Email = varAux.Email,
                CarteiraTrabalho = varAux.CarteiraTrabalho,
                NIF = varAux.NIF,
                RG = varAux.RG,
                EnderecoId = varAux.EnderecoId,
                PrimeiroAcesso = varAux.PrimeiroAcesso,
                Ativo = varAux.Ativo,
                TipoUsuarioId = varAux.TipoUsuarioId

            }).ToList();


            return listarUsuarios;
        }

        public ListarUsuarioDTO BuscarPorId(Guid id)
        {
            Usuario? usuario = _repository.BuscarPorId(id);
            if(usuario == null)
                throw new DomainException("Usuário não encontrado");

                ListarUsuarioDTO dto = new ListarUsuarioDTO
                {
                Nome = usuario.Nome,
                CPF = usuario.CPF,
                CargoId = usuario.CargoId,
                Email = usuario.Email,
                CarteiraTrabalho = usuario.CarteiraTrabalho,
                NIF = usuario.NIF,
                RG = usuario.RG,
                EnderecoId = usuario.EnderecoId,
                PrimeiroAcesso = usuario.PrimeiroAcesso,
                Ativo = usuario.Ativo,
                TipoUsuarioId = usuario.TipoUsuarioId
                };

                return dto;
        }


        public ListarUsuarioDTO BuscarPorNIF(string NIF)
        {
            Usuario? usuario = _repository.BuscarPorNIF(NIF);
            if(usuario == null)
                throw new DomainException("Usuário não encontrado");

                ListarUsuarioDTO dto = new ListarUsuarioDTO
                {
                Nome = usuario.Nome,
                
                CPF = usuario.CPF,
                CargoId = usuario.CargoId,
                Email = usuario.Email,
                CarteiraTrabalho = usuario.CarteiraTrabalho,
                NIF = usuario.NIF,
                RG = usuario.RG,
                EnderecoId = usuario.EnderecoId,
                PrimeiroAcesso = usuario.PrimeiroAcesso,
                Ativo = usuario.Ativo,
                TipoUsuarioId = usuario.TipoUsuarioId
                };

                return dto;
        }

        public ListarUsuarioDTO BuscarPorNome(string nome)
        {
                Usuario? usuario = _repository.BuscarPorNome(nome);
            if(usuario == null)
                throw new DomainException("Usuário não encontrado");

                ListarUsuarioDTO dto = new ListarUsuarioDTO
                {
                Nome = usuario.Nome,
                CPF = usuario.CPF,
                CargoId = usuario.CargoId,
                Email = usuario.Email,
                CarteiraTrabalho = usuario.CarteiraTrabalho,
                NIF = usuario.NIF,
                RG = usuario.RG,
                EnderecoId = usuario.EnderecoId,
                PrimeiroAcesso = usuario.PrimeiroAcesso,
                Ativo = usuario.Ativo,
                TipoUsuarioId = usuario.TipoUsuarioId
                };

                return dto;
        }

        private static byte[] HashSenha(string senha)
        {
            if (string.IsNullOrEmpty(senha))
            {
                throw new DomainException("Senha Obrigatória!");
            }
            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
        }

        public void Adicionar(CriarUsuarioDTO criarDTO)
        {
            ValidarCriacaoDTO.ValidarNome(criarDTO.Nome);
            Usuario? usuario = _repository.BuscarPorId(criarDTO.UsuarioId);
            Usuario? usuarioNIF = _repository.BuscarPorNIF(criarDTO.NIF);
            Usuario? usuarioNome = _repository.BuscarPorNome(criarDTO.Nome);
            if(usuario != null && usuarioNome != null && usuarioNIF != null)
                throw new DomainException("Usuário já existente!");

            return new Usuario
            {
                Nome             = criarDTO.Nome,
                Senha            = HashSenha(criarDTO.Senha),
                CPF              = criarDTO.CPF,
                CargoId          = criarDTO.CargoId,
                Email            = criarDTO.Email,
                CarteiraTrabalho = criarDTO.CarteiraTrabalho,
                NIF              = criarDTO.NIF,
                RG               = criarDTO.RG,
                EnderecoId       = criarDTO.EnderecoId,
                PrimeiroAcesso   = criarDTO.PrimeiroAcesso,
                Ativo            = criarDTO.Ativo,
                TipoUsuarioId    = criarDTO.TipoUsuarioId  
            };
        }

        public void Atualizar(Guid guid, CriarUsuarioDTO criarDTO)
        {
            ValidarCriacaoDTO.ValidarNome(criarDTO.Nome);
            Usuario? usuarioID = _repository.BuscarPorId(id);
            Usuario? usuarioNIF = _repository.BuscarPorNIF(criarDTO.NIF);
            Usuario? usuarioNome = _repository.BuscarPorNome(criarDTO.Nome);
            if(usuarioID == null)
                throw new DomainException("Usuário não existente");

            usuarioID.Nome = criarDTO.Nome;
            usuarioID.CPF = criarDTO.CPF;
            usuarioID.CargoId = criarDTO.CargoId;
            usuarioID.Email = criarDTO.Email;
            usuarioID.CarteiraTrabalho =  criarDTO.CarteiraTrabalho;
            usuarioID.NIF = criarDTO.NIF;
            usuarioID.RG =  criarDTO.RG;
            usuarioID.EnderecoId = criarDTO.EnderecoId;
            usuarioID.PrimeiroAcesso =  criarDTO.PrimeiroAcesso;
            usuarioID.Ativo = criarDTO.Ativo;
            usuarioID.TipoUsuarioId = criarDTO.TipoUsuarioId;

            _repository.Atualizar(usuarioID);
        }

    }
}
