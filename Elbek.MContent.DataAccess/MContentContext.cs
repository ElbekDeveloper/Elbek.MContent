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
    }
}
