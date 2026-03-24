using System;
using System.Collections.Generic;

namespace AssetControlAPI_.Domains;

public partial class TipoAlteracao
{
    public Guid TipoAlteracaoId { get; set; }

    public string NomeAlteracao { get; set; } = null!;

    public virtual ICollection<LogPatrimonio> LogPatrimonio { get; set; } = new List<LogPatrimonio>();
}
