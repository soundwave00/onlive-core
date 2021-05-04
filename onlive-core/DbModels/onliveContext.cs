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

        public virtual DbSet<Events> Events { get; set; }
        public virtual DbSet<Jports> Jports { get; set; }
        public virtual DbSet<Sessions> Sessions { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("SERVER=localhost;DATABASE=onlive;UID=root;PASSWORD=root;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Events>(entity =>
            {
                entity.ToTable("EVENTS");

                entity.HasIndex(e => e.Port)
                    .HasName("FK_JPORTS_EVENTS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(128);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(64);

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.Port).HasColumnName("PORT");

                entity.Property(e => e.Running)
                    .HasColumnName("RUNNING")
                    .HasColumnType("bit(1)");

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

                entity.HasOne(d => d.PortNavigation)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.Port)
                    .HasConstraintName("FK_JPORTS_EVENTS");
            });

            modelBuilder.Entity<Jports>(entity =>
            {
                entity.HasKey(e => e.Port)
                    .HasName("PRIMARY");

                entity.ToTable("JPORTS");

                entity.Property(e => e.Port).HasColumnName("PORT");

                entity.Property(e => e.Running)
                    .HasColumnName("RUNNING")
                    .HasColumnType("bit(1)");
            });

            modelBuilder.Entity<Sessions>(entity =>
            {
                entity.ToTable("SESSIONS");

                entity.HasIndex(e => e.Username)
                    .HasName("FK_USERS_SESSIONS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CodToken)
                    .IsRequired()
                    .HasColumnName("COD_TOKEN")
                    .HasMaxLength(16);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("USERNAME")
                    .HasMaxLength(16);

				entity.Property(e => e.DateStart)
					.IsRequired()
					.HasColumnName("DATE_START")
					.HasColumnType("datetime");

				entity.Property(e => e.DateExp)
					.IsRequired()
					.HasColumnName("DATE_EXP")
					.HasColumnType("datetime");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USERS_SESSIONS");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PRIMARY");

                entity.ToTable("USERS");

                entity.Property(e => e.Username)
                    .HasColumnName("USERNAME")
                    .HasMaxLength(16);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("EMAIL")
                    .HasMaxLength(32);

                entity.Property(e => e.IsActive)
                    .HasColumnName("IS_ACTIVE")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(32);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("PASSWORD")
                    .HasMaxLength(256);

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasColumnName("SALT")
                    .HasMaxLength(16);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("SURNAME")
                    .HasMaxLength(32);

				entity.Property(e => e.DateCreate)
					.IsRequired()
					.HasColumnName("DATE_CREATE")
					.HasColumnType("datetime");

				entity.Property(e => e.DateDelete)
					.HasColumnName("DATE_DELETE")
					.HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
