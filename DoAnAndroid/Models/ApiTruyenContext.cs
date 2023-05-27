using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DoAnAndroid.Models;

public partial class ApiTruyenContext : DbContext
{
    public ApiTruyenContext()
    {
    }

    public ApiTruyenContext(DbContextOptions<ApiTruyenContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DocGia> DocGia { get; set; }

    public virtual DbSet<Truyen> Truyens { get; set; }

    public virtual DbSet<TuongTac> TuongTacs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=PHUCHIEU;Database=API_TRUYEN;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DocGia>(entity =>
        {
            entity.HasKey(e => e.Username);

            entity.Property(e => e.Username).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Sdt)
                .HasMaxLength(50)
                .HasColumnName("SDT");
            entity.Property(e => e.TenDg)
                .HasMaxLength(50)
                .HasColumnName("TenDG");
        });

        modelBuilder.Entity<Truyen>(entity =>
        {
            entity.HasKey(e => e.MaTruyen);

            entity.ToTable("Truyen");

            entity.Property(e => e.MaTruyen).HasMaxLength(50);
            entity.Property(e => e.NgayXb)
                .HasColumnType("date")
                .HasColumnName("NgayXB");
            entity.Property(e => e.TacGia).HasMaxLength(50);
            entity.Property(e => e.TenTruyen).HasMaxLength(50);
            entity.Property(e => e.TheLoai).HasMaxLength(50);
        });

        modelBuilder.Entity<TuongTac>(entity =>
        {
            entity.HasKey(e => new { e.MaTruyen, e.Username }).HasName("PK_BinhLuan");

            entity.ToTable("TuongTac");

            entity.Property(e => e.MaTruyen).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.MaTruyenNavigation).WithMany(p => p.TuongTacs)
                .HasForeignKey(d => d.MaTruyen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TuongTac_Truyen");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.TuongTacs)
                .HasForeignKey(d => d.Username)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TuongTac_DocGia");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
