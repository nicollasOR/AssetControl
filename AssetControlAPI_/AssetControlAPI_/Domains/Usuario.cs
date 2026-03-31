using System;
using System.Collections.Generic;

namespace AssetControlAPI_.Domains;

public partial class Usuario
{
    public Guid UsuarioId { get; set; }

    public string Nome { get; set; } = null!;

    public string? RG { get; set; }

    public string CPF { get; set; } = null!;

    public string CarteiraTrabalho { get; set; } = null!;

    public string Email { get; set; } = null!;

    public byte[] Senha { get; set; } = null!;

    public bool? Ativo { get; set; }

    public Guid EnderecoId { get; set; }

    public Guid CargoId { get; set; }

    public Guid TipoUsuarioId { get; set; }

    public bool PrimeiroAcesso { get; set; }

    public string NIF { get; set; } = null!;

    public virtual Cargo Cargo { get; set; } = null!;

    public virtual Endereco Endereco { get; set; } = null!;

    public virtual ICollection<LogPatrimonio> LogPatrimonio { get; set; } = new List<LogPatrimonio>();

    public virtual ICollection<SolicitacaoTransferencia> SolicitacaoTransferenciaUsuarioAprovacao { get; set; } = new List<SolicitacaoTransferencia>();

    public virtual ICollection<SolicitacaoTransferencia> SolicitacaoTransferenciaUsuarioSolicitando { get; set; } = new List<SolicitacaoTransferencia>();

    public virtual TipoUsuario TipoUsuario { get; set; } = null!;

    public virtual ICollection<Localizacao> Localizacao { get; set; } = new List<Localizacao>();
}
