using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Fuel.Models;

public partial class CarburantContext : DbContext
{
    public CarburantContext()
    {
    }

    public CarburantContext(DbContextOptions<CarburantContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Commune> Communes { get; set; }

    public virtual DbSet<Histo> Histos { get; set; }

    public virtual DbSet<PrixCarburantCommune> PrixCarburantCommunes { get; set; }

    public virtual DbSet<PrixCarburantFrance> PrixCarburantFrance { get; set; }

    public virtual DbSet<TypesCarburant> TypesCarburants { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Database=carburant;User Id=postgres;Password=bpsen;Port=5432");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Commune>(entity =>
        {
            entity.HasKey(e => e.IdCommune).HasName("Communes_pkey");

            entity.Property(e => e.IdCommune)
                .UseIdentityAlwaysColumn()
                .HasColumnName("Id_commune");
            entity.Property(e => e.CodePostal)
                .HasColumnType("character varying")
                .HasColumnName("Code_postal");
            entity.Property(e => e.Ville).HasColumnType("character varying");
        });

        modelBuilder.Entity<Histo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("histo_pkey");

            entity.ToTable("histo");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Sp95).HasColumnName("SP95");
            entity.Property(e => e.Sp98).HasColumnName("SP98");
        });

        modelBuilder.Entity<PrixCarburantCommune>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.IdCarburant).HasColumnName("Id_carburant");
            entity.Property(e => e.IdCommune).HasColumnName("Id_commune");
            entity.Property(e => e.IdPrix)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn()
                .HasColumnName("Id_prix");

            entity.HasOne(d => d.IdCarburantNavigation).WithMany()
                .HasForeignKey(d => d.IdCarburant)
                .HasConstraintName("fk_carburant");

            entity.HasOne(d => d.IdCommuneNavigation).WithMany()
                .HasForeignKey(d => d.IdCommune)
                .HasConstraintName("fk_communes");
        });

        modelBuilder.Entity<PrixCarburantFrance>(entity =>
        {
            entity.HasKey(e => e.IdPrix).HasName("PrixCarburantFrance_pkey");

            entity.ToTable("PrixCarburantFrance");

            entity.Property(e => e.IdPrix)
                .UseIdentityAlwaysColumn()
                .HasColumnName("Id_prix");
            entity.Property(e => e.IdCarburant).HasColumnName("Id_carburant");

            entity.HasOne(d => d.IdCarburantNavigation).WithMany(p => p.PrixCarburantFrances)
                .HasForeignKey(d => d.IdCarburant)
                .HasConstraintName("fk_typeCarburant");
        });

        modelBuilder.Entity<TypesCarburant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TypeCarburant_pkey");

            entity.ToTable("TypesCarburant");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Type).HasColumnType("character varying");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
