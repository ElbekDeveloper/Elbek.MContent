using Elbek.MContent.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Elbek.MContent.DataAccess {
  public class MContentContext : DbContext {
    public MContentContext([ NotNullAttribute ] DbContextOptions options)
        : base(options) {}
    public virtual DbSet<Author> Authors {
      get;
      set;
    }
    public virtual DbSet<Content> Contents {
      get;
      set;
    }
    public virtual DbSet<ContentAuthors> ContentAuthors {
      get;
      set;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      modelBuilder.ApplyConfigurationsFromAssembly(
          Assembly.GetExecutingAssembly());
    }
  }
}
