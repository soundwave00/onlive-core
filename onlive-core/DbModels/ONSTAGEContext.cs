using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace onlive_core.DbModels
{
    public partial class ONSTAGEContext : DbContext
    {
        public ONSTAGEContext()
        {
        }

        public ONSTAGEContext(DbContextOptions<ONSTAGEContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Events> Events { get; set; }
        public virtual DbSet<EventsGenres> EventsGenres { get; set; }
        public virtual DbSet<FavoritesGroups> FavoritesGroups { get; set; }
        public virtual DbSet<Genres> Genres { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<GroupsGenres> GroupsGenres { get; set; }
        public virtual DbSet<GroupsMembers> GroupsMembers { get; set; }
        public virtual DbSet<GroupsMembersGroupsRoles> GroupsMembersGroupsRoles { get; set; }
        public virtual DbSet<GroupsRoles> GroupsRoles { get; set; }
        public virtual DbSet<Jports> Jports { get; set; }
        public virtual DbSet<MusicRoles> MusicRoles { get; set; }
        public virtual DbSet<Sessions> Sessions { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UsersGenres> UsersGenres { get; set; }
        public virtual DbSet<UsersMusicRoles> UsersMusicRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("SERVER=localhost;DATABASE=ONSTAGE;UID=root;PASSWORD=root;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Events>(entity =>
            {
                entity.ToTable("EVENTS");

                entity.HasIndex(e => e.IdGroups)
                    .HasName("FK_GROUPS_EVENTS");

                entity.HasIndex(e => e.Port)
                    .HasName("FK_JPORTS_EVENTS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(128);

                entity.Property(e => e.IdGroups).HasColumnName("ID_GROUPS");

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

                entity.HasOne(d => d.IdGroupsNavigation)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.IdGroups)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GROUPS_EVENTS");

                entity.HasOne(d => d.PortNavigation)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.Port)
                    .HasConstraintName("FK_JPORTS_EVENTS");
            });

            modelBuilder.Entity<EventsGenres>(entity =>
            {
                entity.HasKey(e => new { e.IdGenres, e.IdEvents })
                    .HasName("PRIMARY");

                entity.ToTable("EVENTS_GENRES");

                entity.HasIndex(e => e.IdEvents)
                    .HasName("FK_EVENTS_EVENTS_GENRES");

                entity.Property(e => e.IdGenres).HasColumnName("ID_GENRES");

                entity.Property(e => e.IdEvents).HasColumnName("ID_EVENTS");

                entity.HasOne(d => d.IdEventsNavigation)
                    .WithMany(p => p.EventsGenres)
                    .HasForeignKey(d => d.IdEvents)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EVENTS_EVENTS_GENRES");

                entity.HasOne(d => d.IdGenresNavigation)
                    .WithMany(p => p.EventsGenres)
                    .HasForeignKey(d => d.IdGenres)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GENRES_EVENTS_GENRES");
            });

            modelBuilder.Entity<FavoritesGroups>(entity =>
            {
                entity.HasKey(e => new { e.Username, e.IdGroups })
                    .HasName("PRIMARY");

                entity.ToTable("FAVORITES_GROUPS");

                entity.HasIndex(e => e.IdGroups)
                    .HasName("FK_GROUPS_FAVORITES_GROUPS");

                entity.Property(e => e.Username)
                    .HasColumnName("USERNAME")
                    .HasMaxLength(16);

                entity.Property(e => e.IdGroups).HasColumnName("ID_GROUPS");

                entity.HasOne(d => d.IdGroupsNavigation)
                    .WithMany(p => p.FavoritesGroups)
                    .HasForeignKey(d => d.IdGroups)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GROUPS_FAVORITES_GROUPS");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.FavoritesGroups)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USERS_FAVORITES_GROUPS");
            });

            modelBuilder.Entity<Genres>(entity =>
            {
                entity.ToTable("GENRES");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Genre)
                    .IsRequired()
                    .HasColumnName("GENRE")
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<Groups>(entity =>
            {
                entity.ToTable("GROUPS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Avatar)
                    .HasColumnName("AVATAR")
                    .HasMaxLength(64);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(128);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(64);
            });

            modelBuilder.Entity<GroupsGenres>(entity =>
            {
                entity.HasKey(e => new { e.IdGroups, e.IdGenres })
                    .HasName("PRIMARY");

                entity.ToTable("GROUPS_GENRES");

                entity.HasIndex(e => e.IdGenres)
                    .HasName("FK_GENRES_GROUPS_GENRES");

                entity.Property(e => e.IdGroups).HasColumnName("ID_GROUPS");

                entity.Property(e => e.IdGenres).HasColumnName("ID_GENRES");

                entity.HasOne(d => d.IdGenresNavigation)
                    .WithMany(p => p.GroupsGenres)
                    .HasForeignKey(d => d.IdGenres)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GENRES_GROUPS_GENRES");

                entity.HasOne(d => d.IdGroupsNavigation)
                    .WithMany(p => p.GroupsGenres)
                    .HasForeignKey(d => d.IdGroups)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GROUPS_GROUPS_GENRES");
            });

            modelBuilder.Entity<GroupsMembers>(entity =>
            {
                entity.HasKey(e => new { e.Username, e.IdGroups })
                    .HasName("PRIMARY");

                entity.ToTable("GROUPS_MEMBERS");

                entity.HasIndex(e => e.IdGroups)
                    .HasName("FK_GROUPS_GROUPS_MEMBERS");

                entity.Property(e => e.Username)
                    .HasColumnName("USERNAME")
                    .HasMaxLength(16);

                entity.Property(e => e.IdGroups).HasColumnName("ID_GROUPS");

                entity.HasOne(d => d.IdGroupsNavigation)
                    .WithMany(p => p.GroupsMembers)
                    .HasForeignKey(d => d.IdGroups)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GROUPS_GROUPS_MEMBERS");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.GroupsMembers)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USERS_GROUPS_MEMBERS");
            });

            modelBuilder.Entity<GroupsMembersGroupsRoles>(entity =>
            {
                entity.HasKey(e => new { e.Username, e.IdGroupsMembers, e.IdGroupsRoles })
                    .HasName("PRIMARY");

                entity.ToTable("GROUPS_MEMBERS_GROUPS_ROLES");

                entity.HasIndex(e => e.IdGroupsRoles)
                    .HasName("FK_GROUPS_ROLES_GROUPS_MEMBERS_GROUPS_ROLES");

                entity.Property(e => e.Username)
                    .HasColumnName("USERNAME")
                    .HasMaxLength(16);

                entity.Property(e => e.IdGroupsMembers).HasColumnName("ID_GROUPS_MEMBERS");

                entity.Property(e => e.IdGroupsRoles).HasColumnName("ID_GROUPS_ROLES");

                entity.HasOne(d => d.IdGroupsRolesNavigation)
                    .WithMany(p => p.GroupsMembersGroupsRoles)
                    .HasForeignKey(d => d.IdGroupsRoles)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GROUPS_ROLES_GROUPS_MEMBERS_GROUPS_ROLES");

                entity.HasOne(d => d.GroupsMembers)
                    .WithMany(p => p.GroupsMembersGroupsRoles)
                    .HasForeignKey(d => new { d.Username, d.IdGroupsMembers })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GROUPS_MEMBERS_GROUPS_MEMBERS_GROUPS_ROLES");
            });

            modelBuilder.Entity<GroupsRoles>(entity =>
            {
                entity.ToTable("GROUPS_ROLES");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Roles)
                    .IsRequired()
                    .HasColumnName("ROLES")
                    .HasMaxLength(16);
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

            modelBuilder.Entity<MusicRoles>(entity =>
            {
                entity.ToTable("MUSIC_ROLES");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Instrument)
                    .IsRequired()
                    .HasColumnName("INSTRUMENT")
                    .HasMaxLength(16);
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

                entity.HasIndex(e => e.Email)
                    .HasName("EMAIL")
                    .IsUnique();

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

            modelBuilder.Entity<UsersGenres>(entity =>
            {
                entity.HasKey(e => new { e.Username, e.IdGenres })
                    .HasName("PRIMARY");

                entity.ToTable("USERS_GENRES");

                entity.HasIndex(e => e.IdGenres)
                    .HasName("FK_GENRES_USERS_GENRES");

                entity.Property(e => e.Username)
                    .HasColumnName("USERNAME")
                    .HasMaxLength(16);

                entity.Property(e => e.IdGenres).HasColumnName("ID_GENRES");

                entity.HasOne(d => d.IdGenresNavigation)
                    .WithMany(p => p.UsersGenres)
                    .HasForeignKey(d => d.IdGenres)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GENRES_USERS_GENRES");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.UsersGenres)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USERS_USERS_GENRES");
            });

            modelBuilder.Entity<UsersMusicRoles>(entity =>
            {
                entity.HasKey(e => new { e.Username, e.IdMusicRoles })
                    .HasName("PRIMARY");

                entity.ToTable("USERS_MUSIC_ROLES");

                entity.HasIndex(e => e.IdMusicRoles)
                    .HasName("FK_MUSIC_ROLES_USERS_MUSIC_ROLES");

                entity.Property(e => e.Username)
                    .HasColumnName("USERNAME")
                    .HasMaxLength(16);

                entity.Property(e => e.IdMusicRoles).HasColumnName("ID_MUSIC_ROLES");

                entity.HasOne(d => d.IdMusicRolesNavigation)
                    .WithMany(p => p.UsersMusicRoles)
                    .HasForeignKey(d => d.IdMusicRoles)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MUSIC_ROLES_USERS_MUSIC_ROLES");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.UsersMusicRoles)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USERS_USERS_MUSIC_ROLES");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
