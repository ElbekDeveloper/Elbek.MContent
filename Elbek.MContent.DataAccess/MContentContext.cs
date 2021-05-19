using Elbek.MContent.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Elbek.MContent.DataAccess
{
    public class MContentContext : DbContext
    {
        public MContentContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<AuthorContent> AuthorContents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
