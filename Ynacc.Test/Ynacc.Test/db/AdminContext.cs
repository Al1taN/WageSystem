using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Ynacc.Wage.Dal;

namespace Ynacc.Wage.db
{
    public partial class AdminContext : DbContext
    {
        public AdminContext()
        {
        }

        public AdminContext(DbContextOptions<AdminContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<UserData> UserDatas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("data source=192.168.161.173;initial catalog=WAGE_TEST;user id=wageapp;password=9B67B386");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.Pid)
                    .HasName("PK__admin__DD37D91A73C934CC");

                entity.ToTable("admin");

                entity.Property(e => e.Pid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("pid");

                entity.Property(e => e.Dept)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("dept");

                entity.Property(e => e.Pname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Prole)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("prole");

                entity.Property(e => e.Psd)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("psd");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
