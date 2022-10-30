using MemesApi.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace MemesApi.Db
{
    public class MemeContext: DbContext
    {

        public MemeContext() : base() { }
        public MemeContext(DbContextOptions<MemeContext> options) : base(options) { }

        public DbSet<MemeFile> Files { get; set; } 
        public DbSet<Estimate> Estimates { get; set; }
        public DbSet<FileMeta> Metas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<MemeFile>(file =>
            {
                file.HasKey(f => f.Id);
                file.HasOne(f => f.Meta)
                    .WithOne()
                    .HasForeignKey<MemeFile>(f => f.MetaId);
            });

            modelBuilder.Entity<Estimate>(est =>
            {
                est.HasKey(e => e.Id);
                est.HasIndex(e => e.FileId);
                est.HasIndex(e => e.ClientId);

                est.HasOne(e => e.File)
                    .WithMany(f => f.Estimates)
                    .HasForeignKey(e => e.FileId);
            });

            modelBuilder.Entity<FileMeta>(meta =>
            {
                meta.HasKey(m => m.Id);
            });
        }
    }
}
