using System;
using System.Collections.Generic;

namespace AssetControlAPI_.Domains;

public partial class Localizacao
{
    public Guid LocalizacaoId { get; set; }

    public string NomeLocalizacao { get; set; } = null!;

    public int LocalizacaoSAP { get; set; }

    public string DescricaoSAP { get; set; } = null!;

    public bool? Ativo { get; set; }

    public Guid AreaId { get; set; }

    public virtual Area Area { get; set; } = null!;

    public virtual ICollection<LogPatrimonio> LogPatrimonio { get; set; } = new List<LogPatrimonio>();

    public virtual ICollection<Patrimonio> Patrimonio { get; set; } = new List<Patrimonio>();

    public virtual ICollection<SolicitacaoTransferencia> SolicitacaoTransferencia { get; set; } = new List<SolicitacaoTransferencia>();

    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
}
