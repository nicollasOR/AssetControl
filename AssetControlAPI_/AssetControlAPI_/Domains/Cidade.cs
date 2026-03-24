using System;
using System.Collections.Generic;

namespace AssetControlAPI_.Domains;

public partial class Cidade
{
    public Guid CidadeId { get; set; }

    public string? NomeCidade { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<Bairro> Bairro { get; set; } = new List<Bairro>();

    public virtual ICollection<Endereco> Endereco { get; set; } = new List<Endereco>();
}
