using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Ynacc.Wage.Dal;

namespace Ynacc.Wage.db
{
    public partial class PersonnelWorkContext : DbContext
    {
        public PersonnelWorkContext()
        {
        }

        public PersonnelWorkContext(DbContextOptions<PersonnelWorkContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PersonnelWork> PersonnelWorks { get; set; } = null!;

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
            modelBuilder.Entity<PersonnelWork>(entity =>
            {
                entity.HasKey(e => e.Pid)
                    .HasName("PK_Personnel_work");

                entity.ToTable("personnel_work");

                entity.Property(e => e.Pid)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("pid");

                entity.Property(e => e.Borndate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("borndate");

                entity.Property(e => e.Deptid)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("deptid");

                entity.Property(e => e.Educationid)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("educationid");

                entity.Property(e => e.Fullspell)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("fullspell");

                entity.Property(e => e.Joindate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("joindate");

                entity.Property(e => e.Nationid)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("nationid");

                entity.Property(e => e.Pname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Psex)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("psex");

                entity.Property(e => e.Remark)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("remark");

                entity.Property(e => e.Simspell)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("simspell");

                entity.Property(e => e.Workdate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("workdate");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
