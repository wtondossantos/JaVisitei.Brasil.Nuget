using System;
using Microsoft.EntityFrameworkCore;
using JaVisitei.Brasil.Data.Entities;

namespace JaVisitei.Brasil.Data.Base
{
    public partial class DbJaVisiteiBrasilContext : DbContext
    {
        public DbJaVisiteiBrasilContext()
        {
        }

        public DbJaVisiteiBrasilContext(DbContextOptions<DbJaVisiteiBrasilContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Archipelago> Archipelagos { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Island> Islands { get; set; }
        public virtual DbSet<Macroregion> Macroregions { get; set; }
        public virtual DbSet<Microregion> Microregions { get; set; }
        public virtual DbSet<Municipality> Municipalities { get; set; }
        public virtual DbSet<RegionType> RegionTypes { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserManager> UserManagers { get; set; }
        public virtual DbSet<Visit> Visits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Archipelago>(entity =>
            {
                entity.ToTable("Archipelago");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_unicode_ci");

                entity.HasIndex(e => e.MacroregionId, "FK_ArchipelagoMacroregionId_MacroregionId");

                entity.HasIndex(e => new { e.Id, e.MacroregionId }, "IX_ArchipelagoId_ArchipelagoMacroregionId")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 5, 5 });

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.MacroregionId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Macroregion)
                    .WithMany(p => p.Archipelagos)
                    .HasForeignKey(d => d.MacroregionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ArchipelagoMacroregionId_MacroregionId");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_unicode_ci");

                entity.HasIndex(e => e.Name, "UQ_CountryName")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Island>(entity =>
            {
                entity.ToTable("Island");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_unicode_ci");

                entity.HasIndex(e => e.ArchipelagoId, "FK_IslandArchipelagoId_ArchipelagoId");

                entity.HasIndex(e => new { e.Id, e.ArchipelagoId }, "IX_IslandId_IslandArchipelagoId")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 5, 5 });

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.ArchipelagoId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Canvas)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Archipelago)
                    .WithMany(p => p.Islands)
                    .HasForeignKey(d => d.ArchipelagoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IslandArchipelagoId_ArchipelagoId");
            });

            modelBuilder.Entity<Macroregion>(entity =>
            {
                entity.ToTable("Macroregion");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_unicode_ci");

                entity.HasIndex(e => e.StateId, "FK_MacroregionStateId_StateId");

                entity.HasIndex(e => new { e.Id, e.StateId }, "IX_MacroregionId_MacroregionStateId")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 5, 5 });

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Canvas)
                    .IsRequired()
                    .HasMaxLength(1700);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StateId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Macroregions)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_MacroregionStateId_StateId");
            });

            modelBuilder.Entity<Microregion>(entity =>
            {
                entity.ToTable("Microregion");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_unicode_ci");

                entity.HasIndex(e => e.MacroregionId, "FK_MicroregionMacroregionId_MacroregionId");

                entity.HasIndex(e => new { e.Id, e.MacroregionId }, "IX_MicroregionId_MicroregionMacroregionId")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 5, 5 });

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Canvas)
                    .IsRequired()
                    .HasMaxLength(1400);

                entity.Property(e => e.MacroregionId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Macroregion)
                    .WithMany(p => p.Microregions)
                    .HasForeignKey(d => d.MacroregionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MicroregionMacroregionId_MacroregionId");
            });

            modelBuilder.Entity<Municipality>(entity =>
            {
                entity.ToTable("Municipality");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_unicode_ci");

                entity.HasIndex(e => e.MicroregionId, "FK_MunicipalityMicroregionId_MicroregionId");

                entity.HasIndex(e => new { e.Id, e.MicroregionId }, "IX_MunicipalityId_MunicipalityMicroregionId")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 5, 5 });

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Canvas)
                    .IsRequired()
                    .HasMaxLength(800);

                entity.Property(e => e.MicroregionId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Microregion)
                    .WithMany(p => p.Municipalities)
                    .HasForeignKey(d => d.MicroregionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MunicipalityMicroregionId_MicroregionId");
            });

            modelBuilder.Entity<RegionType>(entity =>
            {
                entity.ToTable("RegionType");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_unicode_ci");

                entity.HasIndex(e => e.Name, "UQ_RegionTypeName")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60);
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.ToTable("State");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_unicode_ci");

                entity.HasIndex(e => e.CountryId, "FK_StateCountryId_CountryId");

                entity.HasIndex(e => new { e.Id, e.CountryId }, "IX_StateId_StateCountryId")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 5, 5 });

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Canvas)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.CountryId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.States)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StateCountryId_CountryId");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_unicode_ci");

                entity.HasIndex(e => e.Email, "UQ_UserEmail")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "UQ_UserUsername")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.RegistryDate).HasColumnType("datetime");

                entity.Property(e => e.Surname).HasMaxLength(200);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<UserManager>(entity =>
            {
                entity.ToTable("UserManager");

                entity.HasIndex(e => e.UserId, "FK_UserManagerUserId_UserId");

                entity.Property(e => e.ActivationCode)
                    .IsRequired()
                    .HasMaxLength(6);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserManagers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserManagerUserId_UserId");
            });

            modelBuilder.Entity<Visit>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RegionTypeId, e.RegionId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                entity.ToTable("Visit");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_unicode_ci");

                entity.HasIndex(e => e.RegionTypeId, "FK_VisitRegionTypeId_RegionTypeId");

                entity.HasIndex(e => e.UserId, "FK_VisitUserId_UserId");

                entity.Property(e => e.RegionId).HasMaxLength(60);

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsFixedLength();

                entity.HasOne(d => d.RegionType)
                    .WithMany(p => p.Visits)
                    .HasForeignKey(d => d.RegionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitRegionTypeId_RegionTypeId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Visits)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitUserId_UserId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
