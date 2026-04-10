using AssetControlAPI_.Applications.DTOs.PatrimonioDTO;
using AssetControlAPI_.Applications.Mapeamentos;
using AssetControlAPI_.Applications.Regras;
using AssetControlAPI_.Domains;
using AssetControlAPI_.Exceptions;
using AssetControlAPI_.Interface;
using CsvHelper;
using CsvHelper.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace AssetControlAPI_.Applications.Services
{
    public class PatrimonioService
    {

        private readonly IPatrimonioRepository _repository;
        public PatrimonioService(IPatrimonioRepository repository) => _repository = repository;

        private static ListarPatrimonioDTO lerDTO(Patrimonio patrimonio)
        {
            return new ListarPatrimonioDTO
            {
                PatrimonioId = patrimonio.PatrimonioId,
                StatusPatrimonioId = patrimonio.StatusPatrimonioId,
                TipoPatrimonioId = patrimonio.TipoPatrimonioId,
                LocalizacaoId = patrimonio.LocalizacaoId,
                Valor = patrimonio.Valor,
                Denominacao = patrimonio.Denominacao,
                Imagem = patrimonio.Imagem,
                NumeroSerie = patrimonio.NumeroSerie
            };
        }

        public List<ListarPatrimonioDTO> Listar()
        {
            List<Patrimonio> patrimonios = _repository.Listar();

            if (patrimonios == null)
                throw new DomainException("Patrimonios não existem");

            List<ListarPatrimonioDTO> listarDTO = patrimonios.Select(varAux => lerDTO(varAux)).ToList();
            //{
            //    StatusPatrimonioId = varAux.StatusPatrimonioId,
            //    TipoPatrimonioId = varAux.TipoPatrimonioId,
            //    PatrimonioId = varAux.PatrimonioId,
            //    Valor = varAux.Valor,
            //    Denominacao = varAux.Denominacao,   
            //    Imagem = varAux.Imagem, 
            //    LocalizacaoId = varAux.LocalizacaoId,   
            //    NumeroSerie = varAux.NumeroSerie
            //}
            //).ToList();

            return listarDTO ;
        }

        public ListarPatrimonioDTO BuscarPorId(Guid id)
        {
            Patrimonio? patrimonio = _repository.BuscarPorId(id);

            if (patrimonio == null)
                throw new DomainException("Patrimônio não existe");

            ListarPatrimonioDTO listarDTO = lerDTO(patrimonio);

            return listarDTO;
        }

        public void Adicionar(IFormFile arquivoCsv, Guid usuarioId)
        {
            if (arquivoCsv == null || arquivoCsv.Length == 0)
                throw new DomainException("Arquivo CSV é obrigatório.");

            Localizacao localizacaoSemLocal = _repository.BuscarPorLocalizacaoNome("Sem local");

            if (localizacaoSemLocal == null)
                throw new DomainException("Localização 'Sem local' não cadastrada.");

            StatusPatrimonio statusAtivo = _repository.BuscarStatusPatrimonioPorNome("Ativo");

            if (statusAtivo == null)
                throw new DomainException("Status 'Ativo' não cadastrado.");

            TipoAlteracao tipoAlteracao = _repository.BuscarTipoAlteracaoPorNome("Atualização de dados");

            if (tipoAlteracao == null)
                throw new DomainException("Tipo de alteração 'Atualização de dados' não cadastrado.");

            List<ImportarPatrimonioCSVDTO> registros;
            //abrir o arquivo csv
            using (var stream = arquivoCsv.OpenReadStream())
                using (var reader = new StreamReader(stream))
                    using (var csv = new CsvReader
                        (reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                        { 
                            //define que o separador é o ;
                            Delimiter = ";",
                            
                            //ignora erros caso o cabecalho nao bata 100%//nao trava as aplicacoes por conta de formatação, vamos tratar os erros depois;
                            HeaderValidated = null,

                            //ignora se tiver faltando algum campo
                            MissingFieldFound = null,
                            
                            //ignora dados quebrados no csv
                            BadDataFound = null,    // 

                            //remove espaços extras automaticamente
                            TrimOptions = TrimOptions.Trim
                        }
                        
                        ))
                        {
                csv.Context.RegisterClassMap<ImportarPatrimonioCSVMap>();
                registros = csv.GetRecords<ImportarPatrimonioCSVDTO>().ToList();                        
                        
                        }
            var erros = new List<string>();
            foreach(var item in registros)
            {
                //se nao tem numero de patrimonio ignora o registro
                if (string.IsNullOrWhiteSpace(item.NumeroPatrimonio))
                    // ignora e vai pro próximo
                    continue;


                //remover espaços extras do número
                string numeroPatrimonio = item.NumeroPatrimonio.Trim();
                if (string.IsNullOrWhiteSpace(numeroPatrimonio))
                {
                    erros.Add($"Patrimônio {numeroPatrimonio} sem denominação");
                    continue;
                }

                string denominacao = item.Denominacao.Trim();

                DateTime? dataIncorporacao = null;
                
                if(!string.IsNullOrWhiteSpace(item.DataIncorporacao))
                {
                    if (DateTime.TryParse(item.DataIncorporacao, new CultureInfo("pt-BR"), DateTimeStyles.None, out DateTime dataConvertida))
                        dataIncorporacao = dataConvertida;
                }
                decimal? valorAquisicao = null;
                if(!string.IsNullOrWhiteSpace(item.ValorAquisicao))
                {
                    //remove separador de milhar e ajusa decimal
                    string valorTexto = item.ValorAquisicao.Replace(".", "").Replace(",", ".");

                    if(decimal.TryParse(valorTexto, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal valorConvertido))
                        valorAquisicao = valorConvertido;

                    ValidarCriacaoDTO.ValidarNumeroPatrimonio(numeroPatrimonio);
                    ValidarCriacaoDTO.ValidarNome(denominacao);

                    bool patrimonioExistente = _repository.BuscarPorNumeroPatrimonio(numeroPatrimonio);

                    if (patrimonioExistente == true)
                    {
                        continue;
                    }

                    Patrimonio patrimonio = new Patrimonio
                    {
                        Denominacao = denominacao,
                        NumeroSerie = numeroPatrimonio,
                        Valor = valorAquisicao,
                        Imagem = null,
                        LocalizacaoId = localizacaoSemLocal.LocalizacaoId,
                        StatusPatrimonioId = statusAtivo.StatusPatrimonioId
                    };

                    _repository.Adicionar(patrimonio);

                    LogPatrimonio log = new LogPatrimonio
                    {
                        DataTransferencia = dataIncorporacao ?? DateTime.Now,
                        TipoAlteracaoId = tipoAlteracao.TipoAlteracaoId,
                        StatusPatrimonioId = patrimonio.StatusPatrimonioId,
                        PatrimonioId = patrimonio.PatrimonioId,
                        UsuarioId = usuarioId,
                        LocalizacaoId = patrimonio.LocalizacaoId
                    };

                    _repository.AdicionarLog(log);
                }



            }
        }

        


        //public ListarPatrimonioDTO BuscarPorNumeroPatrimonio(string numeroPatrimonio, Guid? patrimonioId = null)
        //{
        //    Patrimonio patrimonio = _repository.BuscarPorNumeroPatrimonio(numeroPatrimonio, patrimonioId);
        //    if (patrimonio == null)
        //        throw new DomainException("Patrimônio não existe");

        //    ListarPatrimonioDTO listarDTO = lerDTO(patrimonio);
        //    {
        //PatrimonioId = patrimonio.PatrimonioId,
        //        StatusPatrimonioId = patrimonio.StatusPatrimonioId,
        //        TipoPatrimonioId = patrimonio.TipoPatrimonioId,
        //        LocalizacaoId = patrimonio.LocalizacaoId,
        //        Valor = patrimonio.Valor,
        //        Denominacao = patrimonio.Denominacao,
        //        Imagem = patrimonio.Imagem,
        //        NumeroSerie = patrimonio.NumeroSerie
        //    };

        //    return listarDTO;

        //}

        //public void Adicionar(ListarPatrimonioDTO criarDTO)
        //{
        //    ValidarCriacaoDTO.ValidarNome(criarDTO.Denominacao);
        //    Patrimonio patrimonio = _repository.BuscarPorNumeroPatrimonio(criarDTO.NumeroSerie, criarDTO.PatrimonioId);

        //    if (patrimonio != null)
        //        throw new DomainException("Já existe um patrimonio com este nome e numero");

        //    Patrimonio patrimonioBanco = new Patrimonio
        //    {
        //        StatusPatrimonioId = criarDTO.StatusPatrimonioId,
        //        TipoPatrimonioId = criarDTO.TipoPatrimonioId,
        //        LocalizacaoId = criarDTO.LocalizacaoId,
        //        Valor = criarDTO.Valor,
        //        Denominacao = criarDTO.Denominacao,
        //        Imagem = criarDTO.Imagem,
        //        NumeroSerie = criarDTO.NumeroSerie
        //    };

        //    _repository.Adicionar(patrimonioBanco);
        //}

        //public void Atualizar(Guid guid, CriarPatrimonioDTO criarDTO)
        //{
        //    ValidarCriacaoDTO.ValidarNome(criarDTO.Denominacao);
        //    Patrimonio? patrimonioBanco = _repository.BuscarPorId(guid);
        //    Patrimonio patrimonio = _repository.BuscarPorNumeroPatrimonio(criarDTO.NumeroSerie, guid);

        //    patrimonioBanco.StatusPatrimonioId = criarDTO.StatusPatrimonioId;
        //    patrimonioBanco.TipoPatrimonioId = criarDTO.TipoPatrimonioId;
        //    patrimonioBanco.LocalizacaoId = criarDTO.LocalizacaoId;
        //    patrimonioBanco.Valor = criarDTO.Valor;
        //    patrimonioBanco.Denominacao = criarDTO.Denominacao;
        //    patrimonioBanco.Imagem = criarDTO.Imagem;
        //    patrimonioBanco.NumeroSerie = criarDTO.NumeroSerie;

        //    _repository.Atualizar(patrimonioBanco);
        //}

        //public void AtualizarStatus(Guid guid, AtualizarStatusPatrimonioDTO criarDTO)
        //{
        //    Patrimonio? patrimonioBanco = _repository.BuscarPorId(guid);
        //    Patrimonio patrimonio = _repository.BuscarPorNumeroPatrimonio(criarDTO.NumeroSerie, guid);


        //    patrimonioBanco.StatusPatrimonioId = criarDTO.StatusPatrimonioId;

        //    _repository.Atualizar(patrimonioBanco);
        //}
    }
}
