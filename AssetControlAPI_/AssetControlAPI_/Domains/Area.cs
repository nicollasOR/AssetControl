using System;
using System.Collections.Generic;

namespace AssetControlAPI_.Domains;

public partial class Area
{
    public Guid AreaId { get; set; }

    public string NomeArea { get; set; } = null!;

    public virtual ICollection<Localizacao> Localizacao { get; set; } = new List<Localizacao>();
}
