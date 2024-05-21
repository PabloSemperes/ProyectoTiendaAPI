using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APINttShop.Models;

public partial class NttshopContext : DbContext
{
    public NttshopContext()
    {
    }

    public NttshopContext(DbContextOptions<NttshopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ManagementUser> ManagementUsers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=NTTD-8GQ34F3\\SQLEXPRESS;user=psemperm;password=DatosPablos95;database=NTTSHOP;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ManagementUser>(entity =>
        {
            entity.HasKey(e => e.PkUser).HasName("PK__Manageme__334B06A94EC7E46A");

            entity.HasIndex(e => e.Login, "UQ__Manageme__5E55825BF5F83D62").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Manageme__A9D105346E95299B").IsUnique();

            entity.Property(e => e.PkUser).HasColumnName("PK_USER");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Language)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Surname1)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Surname2)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.PkUser).HasName("PK__Users__334B06A995570D17");

            entity.HasIndex(e => e.Login, "UQ__Users__5E55825B2E810DFF").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053410CC8741").IsUnique();

            entity.Property(e => e.PkUser).HasColumnName("PK_USER");
            entity.Property(e => e.Adress)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Language)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.PostalCode)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Province)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Surname1)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Surname2)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Town)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
