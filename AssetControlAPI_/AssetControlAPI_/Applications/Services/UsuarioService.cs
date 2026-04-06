using AssetControlAPI_.Applications.DTOs.AreaDTO;
using AssetControlAPI_.Applications.DTOs.UsuarioDTO;
using AssetControlAPI_.Domains;
using AssetControlAPI_.Interface;
using AssetControlAPI_.Exceptions;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using AssetControlAPI_.Applications.Regras;
using AssetControlAPI_.Applications.Autenticacao;

namespace AssetControlAPI_.Applications.Services
{
    public class UsuarioService
    {

        private readonly IUsuarioRepository _repository;
        public UsuarioService(IUsuarioRepository repository) => _repository = repository;

        private static ListarUsuarioDTO lerDTO(Usuario usuario)
        {
            return new ListarUsuarioDTO
            {
                UsuarioId = usuario.UsuarioId,
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
        }
        public List<ListarUsuarioDTO> Listar()
        {
            List<Usuario> usuarios = _repository.Listar();
            List<ListarUsuarioDTO> listarUsuarios = usuarios.Select(varAux => lerDTO(varAux)).ToList();
            return listarUsuarios;

        }

        public ListarUsuarioDTO BuscarPorId(Guid id)
        {
            Usuario? usuario = _repository.BuscarPorId(id);
            if (usuario == null)
                throw new DomainException("Usuário não encontrado");
            ListarUsuarioDTO listarDTOs = lerDTO(usuario);
            return listarDTOs;
        }


        public ListarUsuarioDTO BuscarPorNIF(string NIF)
        {
            Usuario? usuario = _repository.BuscarPorNIF(NIF);
            if (usuario == null)
                throw new DomainException("Usuário não encontrado");

            ListarUsuarioDTO listarDTOs = lerDTO(usuario);
            return listarDTOs;
        }

        public ListarUsuarioDTO BuscarPorNome(string nome)
        {
            Usuario? usuario = _repository.BuscarPorNome(nome);
            if (usuario == null)
                throw new DomainException("Usuário não encontrado");

            ListarUsuarioDTO listarDTOs = lerDTO(usuario);
            return listarDTOs;
        }


        public ListarUsuarioDTO BuscarPor_ID_NIF_Nome(Guid? id, string NIF, string Nome)
        {
            Usuario? usuario = _repository.BuscarPor_ID_NIF_Nome(id, NIF, Nome);
            if (usuario == null)
                throw new DomainException("Usuário não encontrado");
            ListarUsuarioDTO listarDTOs = lerDTO(usuario);
            return listarDTOs;
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
            ValidarCriacaoDTO.ValidarNIF(criarDTO.NIF); 
            ValidarCriacaoDTO.ValidarCPF(criarDTO.CPF);
            ValidarCriacaoDTO.ValidarEmail(criarDTO.Email);


            Usuario? usuarioDuplicado = _repository.BuscarPor_ID_NIF_Nome(criarDTO.usuarioId, criarDTO.NIF, criarDTO.Nome);
            if (usuarioDuplicado != null)
            {
                if(usuarioDuplicado.NIF == criarDTO.NIF)
                throw new DomainException("Usuário já cadastrado com este NIF");

                if(usuarioDuplicado.CPF == criarDTO.CPF)
                throw new DomainException("Usuário já cadastrado com este CPF!");

                if (usuarioDuplicado.Email.ToLower() == criarDTO.Email.ToLower())
                    throw new DomainException("Usuário já cadastrado com este Email");
            }

            if (!_repository.enderecoExiste(criarDTO.EnderecoId))
                throw new DomainException("Endereço informado não existe");

            if (!_repository.tipoUsuarioExiste(criarDTO.TipoUsuarioId))
                throw new DomainException("O tipo de usuário não existe");

            if (!_repository.cargoExiste(criarDTO.CargoId))
                throw new DomainException("O cargo informado não existe!");

            Usuario usuarioDTO =  new Usuario
            {
                Nome = criarDTO.Nome,
                Senha = CriptografarUsuario.CriptografarSenha(criarDTO.NIF), 
                CPF = criarDTO.CPF,
                CargoId = criarDTO.CargoId,
                Email = criarDTO.Email,
                CarteiraTrabalho = criarDTO.CarteiraTrabalho,
                NIF = criarDTO.NIF,
                RG = criarDTO.RG,
                EnderecoId = criarDTO.EnderecoId,
                TipoUsuarioId = criarDTO.TipoUsuarioId,
                PrimeiroAcesso = true,
                Ativo = true
            };

            _repository.Adicionar(usuarioDTO);
        }

        public void Atualizar(Guid guid, CriarUsuarioDTO criarDTO)
        {

            ValidarCriacaoDTO.ValidarNome(criarDTO.Nome);
            ValidarCriacaoDTO.ValidarNIF(criarDTO.NIF);
            ValidarCriacaoDTO.ValidarCPF(criarDTO.CPF);
            ValidarCriacaoDTO.ValidarEmail(criarDTO.Email);
            Usuario? usuarioBanco = _repository.BuscarPorId(guid);
            if (usuarioBanco == null)
                throw new DomainException("Usuário não existente");

            Usuario? usuarioDuplicado = _repository.BuscarPor_ID_NIF_Nome(criarDTO.usuarioId, criarDTO.NIF, criarDTO.Nome);

            if(usuarioDuplicado != null)
            {
                if (usuarioDuplicado.NIF == criarDTO.NIF)
                    throw new DomainException("Usuário já cadastrado com este NIF");

                if (usuarioDuplicado.CPF == criarDTO.CPF)
                    throw new DomainException("Usuário já cadastrado com este CPF!");

                if (usuarioDuplicado.Email.ToLower() == criarDTO.Email.ToLower())
                    throw new DomainException("Usuário já cadastrado com este Email");
            }

            if (!_repository.enderecoExiste(criarDTO.EnderecoId))
                throw new DomainException("Endereço informado não existe");

            if (!_repository.tipoUsuarioExiste(criarDTO.TipoUsuarioId))
                throw new DomainException("O tipo de usuário não existe");

            if (!_repository.cargoExiste(criarDTO.CargoId))
                throw new DomainException("O cargo informado não existe!");


            usuarioDuplicado.Nome = criarDTO.Nome; 
            usuarioDuplicado.CPF = criarDTO.CPF;
            usuarioDuplicado.CargoId = criarDTO.CargoId;
            usuarioDuplicado.Email = criarDTO.Email;
            usuarioDuplicado.CarteiraTrabalho = criarDTO.CarteiraTrabalho;
            usuarioDuplicado.NIF = criarDTO.NIF;
            usuarioDuplicado.RG = criarDTO.RG;
            usuarioDuplicado.EnderecoId = criarDTO.EnderecoId;
            usuarioDuplicado.TipoUsuarioId = criarDTO.TipoUsuarioId;
            usuarioDuplicado.CargoId = criarDTO.CargoId;

            //usuarioID.PrimeiroAcesso = false;
            //usuarioID.Ativo = criarDTO.Ativo;

            _repository.Atualizar(usuarioDuplicado);
        }

        public void AtualizarStatus(Guid id, AtualizarStatusUsuarioDTO dto)
        {
            Usuario usuarioBanco = _repository.BuscarPorId(id);
            if (usuarioBanco == null)
                throw new DomainException("Usuário nao encontrado");

            usuarioBanco.Ativo = dto.Ativo;
            _repository.AtualizarStatus(usuarioBanco);
        }

    }
}
