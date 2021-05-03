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
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("SERVER=localhost;DATABASE=onlive;UID=root;PASSWORD=;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Events>(entity =>
            {
                entity.ToTable("EVENTS");

                entity.HasIndex(e => e.Port)
                    .HasName("FK_JPORTS_EVENTS");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(128);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(64);

                entity.Property(e => e.Pid)
                    .HasColumnName("PID")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Port)
                    .HasColumnName("PORT")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

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

                entity.Property(e => e.Port)
                    .HasColumnName("PORT")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Running)
                    .HasColumnName("RUNNING")
                    .HasColumnType("bit(1)");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PRIMARY");

                entity.ToTable("USERS");

                entity.Property(e => e.Username)
                    .HasColumnName("USERNAME")
                    .HasMaxLength(16);

                entity.Property(e => e.CodiceToken)
                    .HasColumnName("CODICE_TOKEN")
                    .HasMaxLength(16)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateCreate)
                    .HasColumnName("DATE_CREATE")
                    .HasColumnType("date");

                entity.Property(e => e.DateDelete)
                    .HasColumnName("DATE_DELETE")
                    .HasColumnType("date");

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

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("SURNAME")
                    .HasMaxLength(32);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
