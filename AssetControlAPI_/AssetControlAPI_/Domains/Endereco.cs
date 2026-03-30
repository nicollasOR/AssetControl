using System;
using System.Collections.Generic;

namespace AssetControlAPI_.Domains;

public partial class Endereco
{
    public Guid EnderecoId { get; set; }

    public string Logradoura { get; set; } = null!;

    public int Numero { get; set; }

    public string? Complemento { get; set; }

    public string CEP { get; set; } = null!;

    public Guid BairroId { get; set; }

    public virtual Bairro Bairro { get; set; } = null!;

    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
}
