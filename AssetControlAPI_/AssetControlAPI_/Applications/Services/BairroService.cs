using AssetControlAPI_.Applications.DTOs.AreaDTO;
using AssetControlAPI_.Applications.DTOs.BairroDTO;
using AssetControlAPI_.Applications.Regras;
using AssetControlAPI_.Domains;
using AssetControlAPI_.Exceptions;
using AssetControlAPI_.Interface;

namespace AssetControlAPI_.Applications.Services
{
    public class BairroService
    {

        private readonly IBairroRepository _repository;
        public BairroService(IBairroRepository repository) => _repository = repository; 
        private static ListarBairroDTO listarDTO(Bairro bairro)
        {
            return new ListarBairroDTO
            {
                bairroId = bairro.BairroId,
                bairroNome = bairro.NomeBairro,
                cidadeId = bairro.CidadeId
            };
        }

        public List<ListarBairroDTO> Listar()
        {

            List<Bairro> Bairro = _repository.Listar();

            if (Bairro == null)
                throw new DomainException("Bairro não existe");

            List<ListarBairroDTO> listarDTOs = Bairro.Select(bairroAux => listarDTO(bairroAux)).ToList(); 
            

            return listarDTOs;
        }

        public ListarBairroDTO ObterPorGuid(Guid id)
        {
            Bairro? bairro = _repository.ObterPorGuid(id);
            if (bairro == null)
                throw new DomainException("Bairro não existe");
            ListarBairroDTO listarDTOs = listarDTO(bairro);


            return listarDTOs;
        }

        public ListarBairroDTO ObterPorNomeEstado(string nomeBairro, Guid cidadeId)
        { 

            Bairro? bairro = _repository.ObterPorNomeEstado(nomeBairro, cidadeId);
            if (bairro == null)
                throw new DomainException("Nome errado");

            ListarBairroDTO listarDTOs = listarDTO(bairro);


            return listarDTOs;

        }

        public void Adicionar(CriarBairroDTO criarDTOs)
        {
            ValidarCriacaoDTO.ValidarNome(criarDTOs.nomeBairro);

            Bairro? bairroBanco = _repository.ObterPorNomeEstado(criarDTOs.nomeBairro, criarDTOs.cidadeId);
            if (bairroBanco == null)
                throw new DomainException("Bairro não encontrado!");

            Bairro criarDTO = new Bairro
            {
               NomeBairro = criarDTOs.nomeBairro

               
            };

            _repository.Adicionar(criarDTO);
        }


        public void Atualizar(Guid guid, CriarBairroDTO criarDTO)
        {
            ValidarCriacaoDTO.ValidarNome(criarDTO.nomeBairro);
            Bairro? bairroBanco = _repository.ObterPorNomeEstado(criarDTO.nomeBairro, criarDTO.cidadeId);

            if (bairroBanco == null)
                throw new DomainException("Bairro não existe");

            bairroBanco.NomeBairro = criarDTO.nomeBairro;

            _repository.Atualizar(bairroBanco);


        }
    }
}
