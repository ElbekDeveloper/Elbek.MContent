using Elbek.MContent.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Elbek.MContent.DataAccess
{
    public class MContentContext : DbContext
    {
        public MContentContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .Property(a => a.Id)
                .HasColumnName("id")
                .HasMaxLength(36)
                .HasColumnType("varchar")
                .IsRequired(true);

            modelBuilder.Entity<Author>()
                .Property(a => a.Name)
                .HasColumnName("Name")
                .HasMaxLength(50)
                .HasColumnType("nvarchar")
                .IsRequired(true);


        }
    }
}
