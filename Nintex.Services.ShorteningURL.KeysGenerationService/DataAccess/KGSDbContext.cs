using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Nintex.Services.ShorteningURL.KeysGenerationService.DataAccess
{
    public partial class KGSDbContext : DbContext
    {
        public KGSDbContext()
        {
        }

        public KGSDbContext(DbContextOptions<KGSDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Kgsround> Kgsrounds { get; set; }
       
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

            modelBuilder.Entity<Kgsround>(entity =>
            {
                entity.HasKey(e => e.RoundId);

                entity.ToTable("KGSRounds");

                entity.Property(e => e.RoundId)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.FromKey)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.LastCounter)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RoundDate).HasColumnType("datetime");

                entity.Property(e => e.ToKey)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);
            });            

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
