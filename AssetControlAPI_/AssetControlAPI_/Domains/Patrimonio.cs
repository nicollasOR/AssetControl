using System;
using System.Collections.Generic;

namespace AssetControlAPI_.Domains;

public partial class Patrimonio
{
    public Guid PatrimonioId { get; set; }

    public string Denominacao { get; set; } = null!;

    public decimal Valor { get; set; }

    public string NumeroSerie { get; set; } = null!;

    public string? Imagem { get; set; }

    public Guid LocalizacaoId { get; set; }

    public Guid TipoPatrimonioId { get; set; }

    public Guid StatusPatrimonioId { get; set; }

    public virtual Localizacao Localizacao { get; set; } = null!;

    public virtual ICollection<LogPatrimonio> LogPatrimonio { get; set; } = new List<LogPatrimonio>();

    public virtual ICollection<SolicitacaoTransferencia> SolicitacaoTransferencia { get; set; } = new List<SolicitacaoTransferencia>();

    public virtual StatusPatrimonio StatusPatrimonio { get; set; } = null!;

    public virtual TipoPatrimonio TipoPatrimonio { get; set; } = null!;
}
