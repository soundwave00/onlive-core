using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace onlive_core.DbModels
{
    public partial class onliveContext : DbContext
    {
        public onliveContext()
        {
        }

        public onliveContext(DbContextOptions<onliveContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Live> Live { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("SERVER=localhost;DATABASE=onlive;UID=root;PASSWORD=root;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Live>(entity =>
            {
                entity.ToTable("LIVE");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(128);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(64);

				entity.Property(e => e.DateSet)
					.IsRequired()
					.HasColumnName("DATE_SET")
					.HasColumnType("datetime");

				entity.Property(e => e.DateStart)
					.HasColumnName("DATE_START")
					.HasColumnType("datetime");

				entity.Property(e => e.DateStop)
					.HasColumnName("DATE_STOP")
					.HasColumnType("datetime");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.Running)
                    .HasColumnName("RUNNING")
                    .HasColumnType("bit(1)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
