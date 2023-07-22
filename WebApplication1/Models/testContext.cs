using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApplication1.Models
{
    public partial class testContext : DbContext
    {
        public testContext()
        {
        }

        public testContext(DbContextOptions<testContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Herobio> Herobios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("server=127.0.0.1;port=3306;database=test;uid=root;pwd=12345678");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("admins");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.City)
                    .HasMaxLength(45)
                    .HasColumnName("city");

                entity.Property(e => e.Username)
                    .HasMaxLength(45)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Herobio>(entity =>
            {
                entity.ToTable("herobio");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Aliases)
                    .HasMaxLength(45)
                    .HasColumnName("aliases");

                entity.Property(e => e.Alignment)
                    .HasMaxLength(45)
                    .HasColumnName("alignment");

                entity.Property(e => e.AlterEgos)
                    .HasMaxLength(45)
                    .HasColumnName("alter-egos");

                entity.Property(e => e.FirstAppearance)
                    .HasMaxLength(45)
                    .HasColumnName("first-appearance");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(45)
                    .HasColumnName("fullname");

                entity.Property(e => e.Heroid).HasColumnName("heroid");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");

                entity.Property(e => e.PlaceOfBirth)
                    .HasMaxLength(45)
                    .HasColumnName("place-of-birth");

                entity.Property(e => e.Publisher)
                    .HasMaxLength(45)
                    .HasColumnName("publisher");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
