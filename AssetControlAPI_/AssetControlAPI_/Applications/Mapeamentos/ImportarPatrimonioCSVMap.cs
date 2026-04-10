using AssetControlAPI_.Applications.DTOs.PatrimonioDTO;
using CsvHelper;
using CsvHelper.Configuration;

namespace AssetControlAPI_.Applications.Mapeamentos
{
    // ClassMap -> é um tradutor de colunas, define como ler o csv
    public class ImportarPatrimonioCSVMap : ClassMap<ImportarPatrimonioCSVDTO>
    {
        // definindo os mapeamentos

        public ImportarPatrimonioCSVMap()
        {

            // Map -> escolhe a propriedade da DTO
            // Name -> diz qual a coluna do CSV para essa propriedade
            Map(numeroPatrimonio => numeroPatrimonio.NumeroPatrimonio).Name("N° invent.");
            Map(nome => nome.Denominacao).Name("Denominação do imobilizado.");
            Map(DataIncorporacao => DataIncorporacao.DataIncorporacao).Name("Dt.incorp.");
            Map(valorAquisicao => valorAquisicao.ValorAquisicao).Name("ValAquis.");
        }
    }
}
