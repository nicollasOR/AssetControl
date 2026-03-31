using AssetControlAPI_.Applications.DTOs.EndereçoDTO;
using AssetControlAPI_.Applications.Regras;
using AssetControlAPI_.Domains;
using AssetControlAPI_.Exceptions;
using AssetControlAPI_.Repository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AssetControlAPI_.Applications.Services
{
    public class EnderecoService
    {
        private readonly IEnderecoRepository _repository;
        public EnderecoService(IEnderecoRepository repository) => _repository = repository;

        public List<LerEnderecoDTO> Listar()
        {
            List<Endereco> Enderecos = _repository.Listar();

            List<LerEnderecoDTO> listarDTO = Enderecos.Select(varAux => new LerEnderecoDTO
            { 
              Logradoura = varAux.Logradoura,
              bairroId = varAux.BairroId,
              CEP = varAux.CEP,
              Complemento = varAux.Complemento,
              enderecoId = varAux.EnderecoId
            }).ToList();

            return listarDTO;
        }

        public LerEnderecoDTO BuscarPorId(Guid enderecoId)
        {
            Endereco? endereco = _repository.BuscarPorId(enderecoId);
            if (endereco == null)
                throw new DomainException("não existe este endereço");

            LerEnderecoDTO listarDTO = new LerEnderecoDTO
            {
                enderecoId = endereco.EnderecoId,
                CEP =endereco.CEP,
                Complemento = endereco.Complemento,
                bairroId = endereco.BairroId,
                Logradoura = endereco.Logradoura

            };

            return listarDTO;
        }

        public LerEnderecoDTO BuscarPorLogradouroENumero(string logradouro, int? numero, Guid bairroId)
        {
            Endereco? enderecoBairroId = _repository.BuscarPorLogradouroENumero(logradouro, numero, bairroId);
            if (enderecoBairroId == null)
                throw new DomainException("Este endereço não existe");

            LerEnderecoDTO listarDTO = new LerEnderecoDTO
            {
                enderecoId = enderecoBairroId.EnderecoId,
                CEP = enderecoBairroId.CEP,
                Logradoura = enderecoBairroId.Logradoura,
                Complemento = enderecoBairroId.Complemento,
                bairroId = enderecoBairroId.BairroId
            };

            return listarDTO;

        }

        public void Adicionar(CriarEnderecoDTO criarDTO)
        {
            ValidarCriacaoDTO.ValidarNome(criarDTO.Logradoura);
            Endereco? enderecoBanco = _repository.BuscarPorLogradouroENumero(criarDTO.Logradoura, criarDTO.numero, criarDTO.bairroId);

            if (enderecoBanco != null)
                throw new DomainException("Endereço já existe");

            Endereco enderecoAux = new Endereco
            {
                Numero = criarDTO.numero,
                BairroId = criarDTO.bairroId,
                CEP = criarDTO.CEP,
                Complemento = criarDTO.Complemento,
                Logradoura = criarDTO.Logradoura,

            };

            _repository.Adicionar(enderecoAux);

            //if(endereco != null && endereco.Bairro != criarDTO.bairroId)
        
        }

        public void Atualizar(Guid bairroId, CriarEnderecoDTO criarDTO)
        {
            ValidarCriacaoDTO.ValidarNome(criarDTO.Logradoura);

            Endereco enderecoBanco = _repository.BuscarPorLogradouroENumero(criarDTO.Logradoura, criarDTO.numero, criarDTO.bairroId);
            Endereco enderecoBairroGuid = _repository.BuscarPorId(bairroId);
            if (enderecoBairroGuid == null)
                throw new DomainException("Não existe este bairro");

            if (enderecoBanco != null && enderecoBanco.BairroId != criarDTO.bairroId)
                throw new DomainException("Já existe um Endereço cadastrada com esse nome nesse bairro.");

            enderecoBairroGuid.Logradoura = criarDTO.Logradoura;
            enderecoBairroGuid.CEP = criarDTO.CEP;
            enderecoBairroGuid.Numero = criarDTO.numero;
            enderecoBairroGuid.Complemento = criarDTO.Complemento;

            _repository.Atualizar(enderecoBairroGuid);

        }

    }
}
