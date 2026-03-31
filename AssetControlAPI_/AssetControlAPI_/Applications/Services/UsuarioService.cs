using AssetControlAPI_.Applications.DTOs.AreaDTO;
using AssetControlAPI_.Applications.DTOs.UsuarioDTO;
using AssetControlAPI_.Domains;
using AssetControlAPI_.Interface;

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

    }
}
