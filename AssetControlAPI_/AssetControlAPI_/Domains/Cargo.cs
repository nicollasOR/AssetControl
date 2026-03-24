using System;
using System.Collections.Generic;

namespace AssetControlAPI_.Domains;

public partial class Cargo
{
    public Guid CargoId { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
}
