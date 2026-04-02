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
        private static LerEnderecoDTO lerDTO(Endereco endereco)
        {
            return new LerEnderecoDTO
            {
                enderecoId = endereco.EnderecoId,
                Logradoura = endereco.Logradoura,
                Complemento = endereco.Complemento,
                CEP = endereco.CEP,
                bairroId = endereco.BairroId
            };
            
        }
        public List<LerEnderecoDTO> Listar()
        {
            List<Endereco> Enderecos = _repository.Listar();

            List<LerEnderecoDTO> listarDTO = Enderecos.Select(varAux => lerDTO(varAux)).ToList();
            return listarDTO;
        }

        public LerEnderecoDTO BuscarPorId(Guid enderecoId)
        {
            Endereco? endereco = _repository.BuscarPorId(enderecoId);
            if (endereco == null)
                throw new DomainException("não existe este endereço");

            LerEnderecoDTO listarDTO = lerDTO(endereco);

            return listarDTO;
        }

        public Endereco BuscarPorLogradouro(string logradoura, int? numero, Guid bairroId)
        {
            Endereco? endereco = _repository.BuscarPorLogradouro(logradoura, numero, bairroId);
            if (endereco == null)
                throw new DomainException("Endereço não encontrado");

            LerEnderecoDTO lerDTOs = lerDTO(endereco);
            return lerDTOs;
        }


        public LerEnderecoDTO BuscarPorLogradouroENumero(string logradouro, int? numero, Guid bairroId, Guid? enderecoId)
        {
            Endereco? enderecoBairroId = _repository.BuscarPorLogradouroENumero(logradouro, numero, bairroId, enderecoId);
            if (enderecoBairroId == null)
                throw new DomainException("Este endereço não existe");
            LerEnderecoDTO listarDTO = lerDTO(enderecoBairroId);

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
