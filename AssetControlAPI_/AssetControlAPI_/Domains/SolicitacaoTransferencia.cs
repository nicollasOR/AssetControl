using System;
using System.Collections.Generic;

namespace AssetControlAPI_.Domains;

public partial class SolicitacaoTransferencia
{
    public Guid SolicitacaoTransId { get; set; }

    public DateTime DataSolicitacao { get; set; }

    public DateTime DataResposta { get; set; }

    public string Justificativa { get; set; } = null!;

    public Guid PatrimonioId { get; set; }

    public Guid UsuarioSolicitandoId { get; set; }

    public Guid? UsuarioAprovacaoId { get; set; }

    public Guid LocalizacaoId { get; set; }

    public virtual Localizacao Localizacao { get; set; } = null!;

    public virtual Patrimonio Patrimonio { get; set; } = null!;

    public virtual Usuario? UsuarioAprovacao { get; set; }

    public virtual Usuario UsuarioSolicitando { get; set; } = null!;
}
