using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Ynacc.Wage.Dal;

namespace Ynacc.Wage.db
{
    public partial class LabourLateContext : DbContext
    {
        public LabourLateContext()
        {
        }

        public LabourLateContext(DbContextOptions<LabourLateContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ImpWorkOtherlate> ImpWorkOtherlates { get; set; } = null!;
        public virtual DbSet<ImpLate03> ImpLate03s { get; set; } = null!;
        public virtual DbSet<StaticsB> StaticsBs { get; set; } = null!;
        public virtual DbSet<audit03> audit03s { get; set; } = null!;

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
            modelBuilder.Entity<ImpWorkOtherlate>(entity =>
            {
                entity.HasKey(e => new { e.Deptid, e.Pid, e.Latetype, e.Year, e.Month, e.Expyear, e.Expmonth, e.Startdate });

                entity.ToTable("imp_work_otherlate");

                entity.Property(e => e.Deptid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("deptid");

                entity.Property(e => e.Pid)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("pid");

                entity.Property(e => e.Latetype)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("latetype");

                entity.Property(e => e.Year).HasColumnName("year");

                entity.Property(e => e.Month).HasColumnName("month");

                entity.Property(e => e.Expyear).HasColumnName("expyear");

                entity.Property(e => e.Expmonth).HasColumnName("expmonth");

                entity.Property(e => e.Startdate).HasColumnName("startdate");

                entity.Property(e => e.Addamount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("addamount");

                entity.Property(e => e.Addpoint)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("addpoint");

                entity.Property(e => e.Auditer)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("auditer");

                entity.Property(e => e.Checker)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("checker");

                entity.Property(e => e.Enddate).HasColumnName("enddate");

                entity.Property(e => e.FlagAudit).HasColumnName("flag_audit");

                entity.Property(e => e.FlagCheck).HasColumnName("flag_check");

                entity.Property(e => e.FlagCommit).HasColumnName("flag_commit");

                entity.Property(e => e.Latedays1).HasColumnName("latedays1");

                entity.Property(e => e.Latedays2).HasColumnName("latedays2");

                entity.Property(e => e.Maker)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maker");

                entity.Property(e => e.Pname)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Remark)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("remark");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
