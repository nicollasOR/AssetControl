using System;
using System.Collections.Generic;
using AssetControlAPI_.Domains;
using Microsoft.EntityFrameworkCore;

namespace AssetControlAPI_.Contexts;

public partial class AssetDBContext : DbContext
{
    public AssetDBContext()
    {
    }

    public AssetDBContext(DbContextOptions<AssetDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Area> Area { get; set; }

    public virtual DbSet<Bairro> Bairro { get; set; }

    public virtual DbSet<Cargo> Cargo { get; set; }

    public virtual DbSet<Cidade> Cidade { get; set; }

    public virtual DbSet<Endereco> Endereco { get; set; }

    public virtual DbSet<Localizacao> Localizacao { get; set; }

    public virtual DbSet<LogPatrimonio> LogPatrimonio { get; set; }

    public virtual DbSet<Patrimonio> Patrimonio { get; set; }

    public virtual DbSet<SolicitacaoTransferencia> SolicitacaoTransferencia { get; set; }

    public virtual DbSet<StatusPatrimonio> StatusPatrimonio { get; set; }

    public virtual DbSet<StatusTransferencia> StatusTransferencia { get; set; }

    public virtual DbSet<TipoAlteracao> TipoAlteracao { get; set; }

    public virtual DbSet<TipoPatrimonio> TipoPatrimonio { get; set; }

    public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=AssetDB;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.AreaId).HasName("PK__Area__70B82048E55BDA8F");

            entity.Property(e => e.AreaId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeArea).HasMaxLength(50);
        });

        modelBuilder.Entity<Bairro>(entity =>
        {
            entity.HasKey(e => e.BairroId).HasName("PK__Bairro__4A0937C3A017F5F0");

            entity.Property(e => e.BairroId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeBairro)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.Cidade).WithMany(p => p.Bairro)
                .HasForeignKey(d => d.CidadeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("BairroCidade_ID_FK");
        });

        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.CargoId).HasName("PK__Cargo__B4E665CD8DFAEE26");

            entity.Property(e => e.CargoId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Nome)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Cidade>(entity =>
        {
            entity.HasKey(e => e.CidadeId).HasName("PK__Cidade__B680093936A225DF");

            entity.Property(e => e.CidadeId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Estado)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.NomeCidade)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Endereco>(entity =>
        {
            entity.HasKey(e => e.EnderecoId).HasName("PK__Endereco__B9D946CF78A9282F");

            entity.Property(e => e.EnderecoId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CEP)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.Complemento)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Logradoura)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Bairro).WithMany(p => p.Endereco)
                .HasForeignKey(d => d.BairroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("EnderecoBairro_ID_FK");
        });

        modelBuilder.Entity<Localizacao>(entity =>
        {
            entity.HasKey(e => e.LocalizacaoId).HasName("PK__Localiza__83ABDF2AA30A7CE4");

            entity.ToTable(tb => tb.HasTrigger("trg_Localizacao_SoftDelete"));

            entity.Property(e => e.LocalizacaoId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Ativo).HasDefaultValue(true);
            entity.Property(e => e.DescricaoSAP)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NomeLocalizacao)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Area).WithMany(p => p.Localizacao)
                .HasForeignKey(d => d.AreaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LocalizacaoArea_ID");

            entity.HasMany(d => d.Usuario).WithMany(p => p.Localizacao)
                .UsingEntity<Dictionary<string, object>>(
                    "LocalUsuario",
                    r => r.HasOne<Usuario>().WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_LocalUsuario_Usuario_ID"),
                    l => l.HasOne<Localizacao>().WithMany()
                        .HasForeignKey("LocalizacaoId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_LocalUsuario_Localizacao"),
                    j =>
                    {
                        j.HasKey("LocalizacaoId", "UsuarioId").HasName("PK_LocalizacaoUsuario_ID");
                    });
        });

        modelBuilder.Entity<LogPatrimonio>(entity =>
        {
            entity.HasKey(e => e.LogPatrimonioId).HasName("PK__LogPatri__E716D10B41521C10");

            entity.Property(e => e.LogPatrimonioId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DataTransferencia).HasPrecision(0);

            entity.HasOne(d => d.Localizacao).WithMany(p => p.LogPatrimonio)
                .HasForeignKey(d => d.LocalizacaoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Log_Localizacao_ID");

            entity.HasOne(d => d.Patrimonio).WithMany(p => p.LogPatrimonio)
                .HasForeignKey(d => d.PatrimonioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Log_Patrimonio_ID");

            entity.HasOne(d => d.StatusPatrimonio).WithMany(p => p.LogPatrimonio)
                .HasForeignKey(d => d.StatusPatrimonioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Log_StatusPatrimonio_ID");

            entity.HasOne(d => d.TipoAlteracao).WithMany(p => p.LogPatrimonio)
                .HasForeignKey(d => d.TipoAlteracaoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Log_TipoAlteracao_ID");

            entity.HasOne(d => d.Usuario).WithMany(p => p.LogPatrimonio)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Log_Usuario_ID");
        });

        modelBuilder.Entity<Patrimonio>(entity =>
        {
            entity.HasKey(e => e.PatrimonioId).HasName("PK__Patrimon__C5A60BFE4289B974");

            entity.ToTable(tb => tb.HasTrigger("trg_Patrimonio_SoftDelete"));

            entity.HasIndex(e => e.NumeroSerie, "UQ__Patrimon__C5455177A4CC8D65").IsUnique();

            entity.Property(e => e.PatrimonioId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Denominacao).IsUnicode(false);
            entity.Property(e => e.Imagem).IsUnicode(false);
            entity.Property(e => e.NumeroSerie)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Localizacao).WithMany(p => p.Patrimonio)
                .HasForeignKey(d => d.LocalizacaoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatrimonioLocalizacao_ID");

            entity.HasOne(d => d.StatusPatrimonio).WithMany(p => p.Patrimonio)
                .HasForeignKey(d => d.StatusPatrimonioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patrimonio_StatusPatrimonio_ID");

            entity.HasOne(d => d.TipoPatrimonio).WithMany(p => p.Patrimonio)
                .HasForeignKey(d => d.TipoPatrimonioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patrimonio_TipoPatrimonio_ID");
        });

        modelBuilder.Entity<SolicitacaoTransferencia>(entity =>
        {
            entity.HasKey(e => e.SolicitacaoTransId).HasName("PK__Solicita__D015134364337251");

            entity.Property(e => e.SolicitacaoTransId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DataResposta).HasPrecision(0);
            entity.Property(e => e.DataSolicitacao).HasPrecision(0);

            entity.HasOne(d => d.Localizacao).WithMany(p => p.SolicitacaoTransferencia)
                .HasForeignKey(d => d.LocalizacaoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SolicitacaoTransferencia_Localizacao_ID");

            entity.HasOne(d => d.Patrimonio).WithMany(p => p.SolicitacaoTransferencia)
                .HasForeignKey(d => d.PatrimonioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SolicitacaoTransferencia_Patrimonio_ID");

            entity.HasOne(d => d.UsuarioAprovacao).WithMany(p => p.SolicitacaoTransferenciaUsuarioAprovacao)
                .HasForeignKey(d => d.UsuarioAprovacaoId)
                .HasConstraintName("FK_SolicitacaoTransferencia_UsuarioAprovacao_ID");

            entity.HasOne(d => d.UsuarioSolicitando).WithMany(p => p.SolicitacaoTransferenciaUsuarioSolicitando)
                .HasForeignKey(d => d.UsuarioSolicitandoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SolicitacaoTransferencia_UsuarioSolicitacao_ID");
        });

        modelBuilder.Entity<StatusPatrimonio>(entity =>
        {
            entity.HasKey(e => e.StatusPatrimonioId).HasName("PK__StatusPa__B3F336295678D97D");

            entity.Property(e => e.StatusPatrimonioId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.StatusPatrimonio1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("StatusPatrimonio");
        });

        modelBuilder.Entity<StatusTransferencia>(entity =>
        {
            entity.HasKey(e => e.StatusTransferenciaId).HasName("PK__StatusTr__7AA82899B4CF5DB0");

            entity.Property(e => e.StatusTransferenciaId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.StatusTransferencia1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("StatusTransferencia");
        });

        modelBuilder.Entity<TipoAlteracao>(entity =>
        {
            entity.HasKey(e => e.TipoAlteracaoId).HasName("PK__TipoAlte__9BEF4F6D1BFEC5D2");

            entity.Property(e => e.TipoAlteracaoId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeAlteracao).HasMaxLength(50);
        });

        modelBuilder.Entity<TipoPatrimonio>(entity =>
        {
            entity.HasKey(e => e.TipoPatrimonioId).HasName("PK__TipoPatr__4DC9FFB9E94BE10C");

            entity.Property(e => e.TipoPatrimonioId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeTipo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoUsuario>(entity =>
        {
            entity.HasKey(e => e.TipoUsuarioId).HasName("PK__TipoUsua__7F22C722FB8D2545");

            entity.Property(e => e.TipoUsuarioId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Nome)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuario__2B3DE7B895D20A4F");

            entity.ToTable(tb => tb.HasTrigger("trg_Usuario_SoftDelete"));

            entity.HasIndex(e => e.RG, "UQ__Usuario__321537C8EB5AF2B8").IsUnique();

            entity.HasIndex(e => e.CarteiraTrabalho, "UQ__Usuario__6E25BCA2BB525854").IsUnique();

            entity.HasIndex(e => e.CPF, "UQ__Usuario__C1F897311D372A1A").IsUnique();

            entity.Property(e => e.UsuarioId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Ativo).HasDefaultValue(true);
            entity.Property(e => e.CPF).HasMaxLength(12);
            entity.Property(e => e.CarteiraTrabalho)
                .HasMaxLength(14)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Nome).HasMaxLength(100);
            entity.Property(e => e.RG)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Senha).HasMaxLength(32);

            entity.HasOne(d => d.Cargo).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.CargoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsuarioCargo_ID");

            entity.HasOne(d => d.Endereco).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.EnderecoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsuarioEndereco_ID");

            entity.HasOne(d => d.TipoUsuario).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.TipoUsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsuarioTipoUsuario_ID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
