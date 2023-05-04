using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Ynacc.Wage.Dal;

namespace Ynacc.Wage.db
{
    public partial class OvertimeContext : DbContext
    {
        public OvertimeContext()
        {
        }

        public OvertimeContext(DbContextOptions<OvertimeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ImpAddnight> ImpAddnights { get; set; } = null!;
        public virtual DbSet<ImpLate02> ImpLate02s { get; set; } = null!;
        public virtual DbSet<audit02> audit02s { get; set; } = null!;

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
            modelBuilder.Entity<ImpAddnight>(entity =>
            {
                entity.HasKey(e => new { e.Deptid, e.Pid, e.Year, e.Month, e.Expyear, e.Expmonth });

                entity.ToTable("imp_addnight");

                entity.Property(e => e.Deptid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("deptid");

                entity.Property(e => e.Pid)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("pid");

                entity.Property(e => e.Year).HasColumnName("year");

                entity.Property(e => e.Month).HasColumnName("month");

                entity.Property(e => e.Expyear).HasColumnName("expyear");

                entity.Property(e => e.Expmonth).HasColumnName("expmonth");

                entity.Property(e => e.Adddays)
                    .HasColumnType("decimal(9, 2)")
                    .HasColumnName("adddays");

                entity.Property(e => e.Alldaynum)
                    .HasColumnType("decimal(9, 2)")
                    .HasColumnName("alldaynum");

                entity.Property(e => e.Auditer)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("auditer");

                entity.Property(e => e.Checker)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("checker");

                entity.Property(e => e.Earlynum)
                    .HasColumnType("decimal(9, 2)")
                    .HasColumnName("earlynum");

                entity.Property(e => e.FlagAudit).HasColumnName("flag_audit");

                entity.Property(e => e.FlagCheck).HasColumnName("flag_check");

                entity.Property(e => e.FlagCommit).HasColumnName("flag_commit");

                entity.Property(e => e.Getamount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("getamount");

                entity.Property(e => e.Holidays)
                    .HasColumnType("decimal(9, 2)")
                    .HasColumnName("holidays");

                entity.Property(e => e.Maker)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maker");

                entity.Property(e => e.Mealamount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("mealamount");

                entity.Property(e => e.Middlenum)
                    .HasColumnType("decimal(9, 2)")
                    .HasColumnName("middlenum");

                entity.Property(e => e.Nightnum)
                    .HasColumnType("decimal(9, 2)")
                    .HasColumnName("nightnum");

                entity.Property(e => e.Ondutyamount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("ondutyamount");

                entity.Property(e => e.Ondutydays)
                    .HasColumnType("decimal(9, 2)")
                    .HasColumnName("ondutydays");

                entity.Property(e => e.Pname)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("remarks");

                entity.Property(e => e.Remarkss)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("remarkss");

                entity.Property(e => e.Weekenddays)
                    .HasColumnType("decimal(9, 2)")
                    .HasColumnName("weekenddays");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
