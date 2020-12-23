using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Nintex.NetCore.Component.ShortUrlStorage.SqlServer.Persistence
{
    public partial class UrlShorteningDbContext : DbContext
    {
        public UrlShorteningDbContext()
        {
        }

        public UrlShorteningDbContext(DbContextOptions<UrlShorteningDbContext> options)
            : base(options)
        {
        }
        
        public virtual DbSet<Url> Urls { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#pragma warning disable CS1030 // #warning directive
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=UrlShorteningDb;Trusted_Connection=True;");
#pragma warning restore CS1030 // #warning directive
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");
            
            modelBuilder.Entity<Url>(entity =>
            {
                entity.HasKey(e => e.KeyId)
                    .HasName("PK_Url");

                entity.HasIndex(e => e.FullUrl, "IX_FullUrl")
                    .IsUnique();

                entity.HasIndex(e => e.ShortKey, "IX_ShortKey")
                    .IsUnique();

                entity.Property(e => e.KeyId)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.FullUrl)
                    .IsRequired()
                    .HasMaxLength(225);

                entity.Property(e => e.LastReadingDate).HasColumnType("datetime");

                entity.Property(e => e.ShortKey)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
