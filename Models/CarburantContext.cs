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

    public virtual DbSet<Histo> Histos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Server=localhost;Database=carburant;User Id=postgres;Password=bpsen;Port=5432");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
