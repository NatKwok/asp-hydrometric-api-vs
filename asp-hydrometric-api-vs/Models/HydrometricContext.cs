using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace asp_hydrometric_api_vs.Models;

public partial class HydrometricContext : DbContext
{
    public HydrometricContext()
    {
    }

    public HydrometricContext(DbContextOptions<HydrometricContext> options)
        : base(options)
    {
    }

    public virtual DbSet<HydrometricAnnualPeak> HydrometricAnnualPeaks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=^ytrO524FD;Database=hydrometric", x => x.UseNetTopologySuite());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("postgis");

        modelBuilder.Entity<HydrometricAnnualPeak>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("hydrometric-annual-peaks_pkey");

            entity.ToTable("hydrometric-annual-peaks");

            entity.Property(e => e.Id)
                .HasColumnType("character varying")
                .HasColumnName("id");
            entity.Property(e => e.DataTypeEn)
                .HasColumnType("character varying")
                .HasColumnName("DATA_TYPE_EN");
            entity.Property(e => e.DataTypeFr)
                .HasColumnType("character varying")
                .HasColumnName("DATA_TYPE_FR");
            entity.Property(e => e.Date)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("DATE");
            entity.Property(e => e.Geom)
                .HasColumnType("geometry(Point,4326)")
                .HasColumnName("geom");
            entity.Property(e => e.Identifier)
                .HasColumnType("character varying")
                .HasColumnName("IDENTIFIER");
            entity.Property(e => e.Peak).HasColumnName("PEAK");
            entity.Property(e => e.PeakCodeEn)
                .HasColumnType("character varying")
                .HasColumnName("PEAK_CODE_EN");
            entity.Property(e => e.PeakCodeFr)
                .HasColumnType("character varying")
                .HasColumnName("PEAK_CODE_FR");
            entity.Property(e => e.ProvTerrStateLoc)
                .HasColumnType("character varying")
                .HasColumnName("PROV_TERR_STATE_LOC");
            entity.Property(e => e.StationName)
                .HasColumnType("character varying")
                .HasColumnName("STATION_NAME");
            entity.Property(e => e.StationNumber)
                .HasColumnType("character varying")
                .HasColumnName("STATION_NUMBER");
            entity.Property(e => e.SymbolEn)
                .HasColumnType("character varying")
                .HasColumnName("SYMBOL_EN");
            entity.Property(e => e.SymbolFr)
                .HasColumnType("character varying")
                .HasColumnName("SYMBOL_FR");
            entity.Property(e => e.TimezoneOffset)
                .HasColumnType("character varying")
                .HasColumnName("TIMEZONE_OFFSET");
            entity.Property(e => e.UnitsEn)
                .HasColumnType("character varying")
                .HasColumnName("UNITS_EN");
            entity.Property(e => e.UnitsFr)
                .HasColumnType("character varying")
                .HasColumnName("UNITS_FR");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
