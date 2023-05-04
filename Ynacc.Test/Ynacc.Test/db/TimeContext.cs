using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Ynacc.Wage.Dal;

namespace Ynacc.Wage.db
{
    public partial class TimeContext : DbContext
    {
        public TimeContext()
        {
        }

        public TimeContext(DbContextOptions<TimeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdminTime> AdminTimes { get; set; } = null!;

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
            modelBuilder.Entity<AdminTime>(entity =>
            {
                entity.HasKey(e => e.Idx);

                entity.ToTable("Admin_time");

                entity.Property(e => e.Idx)
                    .ValueGeneratedNever()
                    .HasColumnName("idx");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
