using JaVisitei.Brasil.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace JaVisitei.Brasil.Data.Base
{
    public partial class DbJaVisiteiBrasilContext : DbContext
    {
        public DbJaVisiteiBrasilContext(DbContextOptions<DbJaVisiteiBrasilContext> options)
            : base(options)
        { }

        public virtual DbSet<ArquipelagoEntity> Arquipelagos { get; set; }
        public virtual DbSet<EstadoEntity> Estados { get; set; }
        public virtual DbSet<IlhaEntity> Ilhas { get; set; }
        public virtual DbSet<MesorregiaoEntity> Mesorregiaos { get; set; }
        public virtual DbSet<MicrorregiaoEntity> Microrregiaos { get; set; }
        public virtual DbSet<MunicipioEntity> Municipios { get; set; }
        public virtual DbSet<PaisEntity> Paises { get; set; }
        public virtual DbSet<UsuarioEntity> Usuarios { get; set; }
        public virtual DbSet<TipoRegiaoEntity> TipoRegioes { get; set; }
        public virtual DbSet<VisitaEntity> Visitas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VisitaEntity>(entity =>
            {
                entity.ToTable("TbVisita");

                entity.HasIndex(e => e.IdUsuario, "Fk_TbVisitaIdUsuario_TbUsuarioId");
                entity.HasIndex(e => e.IdTipoRegiao, "Fk_TbVisitaIdTipoRegiao_TbTipoRegiaoId");

                entity.HasIndex(e => new { e.Id, e.IdUsuario, e.IdTipoRegiao, e.IdRegiao }, "Ix_TbVisita_IdUsuarioIdTipoRegiaoIdRegiao");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.IdUsuario)
                    .IsRequired();

                entity.Property(e => e.IdTipoRegiao)
                    .IsRequired();

                entity.Property(e => e.IdRegiao)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.Cor)
                    .HasMaxLength(6);

                entity.Property(e => e.Data)
                    .IsRequired();

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Visitas)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_TbVisitaIdUsuario_TbUsuarioId");

                entity.HasOne(d => d.IdTipoRegiaoNavigation)
                    .WithMany(p => p.Visitas)
                    .HasForeignKey(d => d.IdTipoRegiao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_TbVisitaIdTipoRegiao_TbTipoRegiaoId");
            });

            modelBuilder.Entity<TipoRegiaoEntity>(entity =>
            {
                entity.ToTable("TbTipoRegiao");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(60);
            });

            modelBuilder.Entity<UsuarioEntity>(entity =>
            {
                entity.ToTable("TbUsuario");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.Sobrenome)
                    .HasMaxLength(200);

                entity.Property(e => e.NomeUsuario)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<ArquipelagoEntity>(entity =>
            {
                entity.ToTable("TbArquipelago");

                entity.HasIndex(e => e.IdMesorregiao, "Fk_TbArquipelagoIdMesorregiao_TbMesorregiaoId");

                entity.HasIndex(e => new { e.Id, e.IdMesorregiao }, "Ix_IdArquipelago_IdMesorregiao");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.IdMesorregiao)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdMesorregiaoNavigation)
                    .WithMany(p => p.Arquipelagos)
                    .HasForeignKey(d => d.IdMesorregiao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_TbArquipelagoIdMesorregiao_TbMesorregiaoId");
            });

            modelBuilder.Entity<EstadoEntity>(entity =>
            {
                entity.ToTable("TbEstado");

                entity.HasIndex(e => e.IdPais, "Fk_TbEstadoIdPais_TbPaisId");

                entity.HasIndex(e => new { e.Id, e.IdPais }, "Ix_IdEstado_IdPais");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Desenho)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.IdPais)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdPaisNavigation)
                    .WithMany(p => p.Estados)
                    .HasForeignKey(d => d.IdPais)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_TbEstadoIdPais_TbPaisId");
            });

            modelBuilder.Entity<IlhaEntity>(entity =>
            {
                entity.ToTable("TbIlha");

                entity.HasIndex(e => e.IdArquipelago, "Fk_TbIlhaIdArquipelago_TbArquipelagoId");

                entity.HasIndex(e => new { e.Id, e.IdArquipelago }, "Ix_IdIlha_IdArquipelago");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Desenho)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.IdArquipelago)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdArquipelagoNavigation)
                    .WithMany(p => p.Ilhas)
                    .HasForeignKey(d => d.IdArquipelago)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_TbIlhaIdArquipelago_TbArquipelagoId");
            });

            modelBuilder.Entity<MesorregiaoEntity>(entity =>
            {
                entity.ToTable("TbMesorregiao");

                entity.HasIndex(e => e.IdEstado, "Fk_TbMesorregiaoIdEstado_TdEstadoId");

                entity.HasIndex(e => new { e.Id, e.IdEstado }, "Ix_IdMesorregiao_IdEstado");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Desenho)
                    .IsRequired()
                    .HasMaxLength(1700);

                entity.Property(e => e.IdEstado)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Mesorregiaos)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_TbMesorregiaoIdEstado_TdEstadoId");
            });

            modelBuilder.Entity<MicrorregiaoEntity>(entity =>
            {
                entity.ToTable("TbMicrorregiao");

                entity.HasIndex(e => e.IdMesorregiao, "Fk_TbMicrorregiaoIdMesorregiao_TbMesorregiaoId");

                entity.HasIndex(e => new { e.Id, e.IdMesorregiao }, "Ix_IdMicrorregiao_IdMesorregiao");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Desenho)
                    .IsRequired()
                    .HasMaxLength(1400);

                entity.Property(e => e.IdMesorregiao)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdMesorregiaoNavigation)
                    .WithMany(p => p.Microrregiaos)
                    .HasForeignKey(d => d.IdMesorregiao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_TbMicrorregiaoIdMesorregiao_TbMesorregiaoId");
            });

            modelBuilder.Entity<MunicipioEntity>(entity =>
            {
                entity.ToTable("TbMunicipio");

                entity.HasIndex(e => e.IdMicrorregiao, "Fk_TbMunicipioIdMicrorregiao_TbMicrorregiaoId");

                entity.HasIndex(e => new { e.Id, e.IdMicrorregiao }, "Ix_IdMunicipio_IdMicrorregiao");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Desenho)
                    .IsRequired()
                    .HasMaxLength(800);

                entity.Property(e => e.IdMicrorregiao)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdMicrorregiaoNavigation)
                    .WithMany(p => p.Municipios)
                    .HasForeignKey(d => d.IdMicrorregiao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_TbMunicipioIdMicrorregiao_TbMicrorregiaoId");
            });

            modelBuilder.Entity<PaisEntity>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
