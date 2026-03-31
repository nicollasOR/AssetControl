using AssetControlAPI_.Interface;
using AssetControlAPI_.Domains;
using AssetControlAPI_.Exceptions;
using AssetControlAPI_.Applications.DTOs.TipoUsuarioDTO;
using AssetControlAPI_.Applications.Regras;


namespace AssetControlAPI_.Applications.Services
{
    public class TipoUsuarioService
    {

        private readonly ITipoUsuarioRepository _repository;
        public TipoUsuarioService(ITipoUsuarioRepository repository) => _repository = repository;

        public List<ListarTipoUsuarioDTO> Listar()
        {
            List<TipoUsuario> tipoUsuario = _repository.Listar();

            if (tipoUsuario == null)
                throw new DomainException("Tipo não encontrado");

            List<ListarTipoUsuarioDTO> tipoUsuarioBanco = tipoUsuario.Select(construtorAux => new ListarTipoUsuarioDTO
            {
                TipoUsuarioId = construtorAux.TipoUsuarioId,
                nomeTipoUsuario = construtorAux.Nome
            }).ToList();

            return tipoUsuarioBanco;
        }

        public ListarTipoUsuarioDTO ObterPorId(Guid id)
        {
            TipoUsuario usuario = _repository.ObterPorId(id);

            ListarTipoUsuarioDTO listarUsuario = new ListarTipoUsuarioDTO
            {
                nomeTipoUsuario = usuario.Nome,
                TipoUsuarioId = usuario.TipoUsuarioId
            };

            return listarUsuario;
        }

        public ListarTipoUsuarioDTO ObterPorNome(string nome)
        {
            TipoUsuario usuario = _repository.ObterPorNome(nome);
            if (usuario == null)
                throw new DomainException("Tipo não existe");
            ListarTipoUsuarioDTO listarUsuario = new ListarTipoUsuarioDTO
            {
                nomeTipoUsuario = usuario.Nome,
                TipoUsuarioId = usuario.TipoUsuarioId
            };
            return listarUsuario;
        }

        public void Adicionar(CriarTipoUsuarioDTO criarDTO)
        {
            ValidarCriacaoDTO.ValidarNome(criarDTO.nomeTipoUsuario);

            TipoUsuario tipoUsuario = _repository.ObterPorNome(criarDTO.nomeTipoUsuario);

            if (tipoUsuario != null)
                throw new DomainException("Já existe com este nome, tipo usuario");
            TipoUsuario novaDTO = new TipoUsuario
            {
                Nome = criarDTO.nomeTipoUsuario,
            };

            _repository.Adicionar(novaDTO);


        }

        public void Atualizar(Guid id, CriarTipoUsuarioDTO criarDTO)
        {
            ValidarCriacaoDTO.ValidarEstado(criarDTO.nomeTipoUsuario);
            TipoUsuario tipoUsuario = _repository.ObterPorNome(criarDTO.nomeTipoUsuario);
            TipoUsuario tipoUsuarioBanco = _repository.ObterPorId(id);
            if (tipoUsuarioBanco == null)
                throw new DomainException("TipoUsuário não existe");

            if (tipoUsuario != null)
                throw new DomainException("Já existe este tipoUsuário cadastrado com este nome");

            tipoUsuarioBanco.Nome = criarDTO.nomeTipoUsuario;

            _repository.Atualizar(tipoUsuarioBanco);


        }
    }
}
