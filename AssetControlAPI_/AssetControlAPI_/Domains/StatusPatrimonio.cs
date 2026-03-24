using System;
using System.Collections.Generic;

namespace AssetControlAPI_.Domains;

public partial class StatusPatrimonio
{
    public Guid StatusPatrimonioId { get; set; }

    public string StatusPatrimonio1 { get; set; } = null!;

    public virtual ICollection<LogPatrimonio> LogPatrimonio { get; set; } = new List<LogPatrimonio>();

    public virtual ICollection<Patrimonio> Patrimonio { get; set; } = new List<Patrimonio>();
}
