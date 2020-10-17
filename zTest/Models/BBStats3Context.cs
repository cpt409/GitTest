using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace zTest.Models
{
    public partial class BBStats3Context : DbContext
    {
        public BBStats3Context()
        {
        }

        public BBStats3Context(DbContextOptions<BBStats3Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Buffs> Buffs { get; set; }
        public virtual DbSet<PitcherBuff> PitcherBuff { get; set; }
        public virtual DbSet<Pitchers> Pitchers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\;Database=BBStats3;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Buffs>(entity =>
            {
                entity.HasKey(e => e.BuffId)
                    .HasName("pk_buffId");

                entity.HasIndex(e => e.BuffName)
                    .HasName("uk_buffName")
                    .IsUnique();

                entity.Property(e => e.BuffId).HasColumnName("buffId");

                entity.Property(e => e.BuffName)
                    .IsRequired()
                    .HasColumnName("buffName")
                    .HasMaxLength(80)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PitcherBuff>(entity =>
            {
                entity.HasKey(e => new { e.PitcherId, e.BuffId })
                    .HasName("pk_pitcherBuff");

                entity.Property(e => e.PitcherId).HasColumnName("pitcherId");

                entity.Property(e => e.BuffId).HasColumnName("buffId");
            });

            modelBuilder.Entity<Pitchers>(entity =>
            {
                entity.HasKey(e => e.PitcherId)
                    .HasName("pk_pitcherId");

                entity.Property(e => e.PitcherId).HasColumnName("pitcherId");

                entity.Property(e => e.PitcherName)
                    .IsRequired()
                    .HasColumnName("pitcherName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TeamName)
                    .IsRequired()
                    .HasColumnName("teamName")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.VarBb9).HasColumnName("varBB9");

                entity.Property(e => e.VarEra).HasColumnName("varERA");

                entity.Property(e => e.VarFb9).HasColumnName("varFB9");

                entity.Property(e => e.VarGbp).HasColumnName("varGBP");

                entity.Property(e => e.VarIps).HasColumnName("varIPS");

                entity.Property(e => e.VarKo9).HasColumnName("varKO9");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
