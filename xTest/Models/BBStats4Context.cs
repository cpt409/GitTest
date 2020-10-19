using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace xTest.Models
{
    public partial class BBStats4Context : DbContext
    {
        public BBStats4Context()
        {
        }

        public BBStats4Context(DbContextOptions<BBStats4Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Buffs> Buffs { get; set; }
        public virtual DbSet<StarterBuff> StarterBuff { get; set; }
        public virtual DbSet<Starters> Starters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\;Database=BBStats4;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Buffs>(entity =>
            {
                entity.HasKey(e => e.BuffId)
                    .HasName("pk_buffs");

                entity.Property(e => e.BuffId).HasColumnName("buffId");

                entity.Property(e => e.BuffName)
                    .IsRequired()
                    .HasColumnName("buffName")
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StarterBuff>(entity =>
            {
                entity.Property(e => e.StarterBuffId).HasColumnName("starterBuffId");

                entity.Property(e => e.BuffId).HasColumnName("buffId");

                entity.Property(e => e.StarterId).HasColumnName("starterId");
            });

            modelBuilder.Entity<Starters>(entity =>
            {
                entity.HasKey(e => e.StarterId)
                    .HasName("pk_starters");

                entity.Property(e => e.StarterId).HasColumnName("starterId");

                entity.Property(e => e.FanId).HasColumnName("fanId");

                entity.Property(e => e.StarterName)
                    .IsRequired()
                    .HasColumnName("starterName")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.TeamName)
                    .IsRequired()
                    .HasColumnName("teamName")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.VarBb9).HasColumnName("varBB9");

                entity.Property(e => e.VarEra).HasColumnName("varERA");

                entity.Property(e => e.VarFip).HasColumnName("varFIP");

                entity.Property(e => e.VarIps).HasColumnName("varIPS");

                entity.Property(e => e.VarKo9).HasColumnName("varKO9");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
