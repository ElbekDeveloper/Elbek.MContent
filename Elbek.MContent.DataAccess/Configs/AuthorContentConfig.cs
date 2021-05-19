using Elbek.MContent.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elbek.MContent.DataAccess.Configs
{
    public class AuthorContentConfig : IEntityTypeConfiguration<AuthorContent>
    {
        public void Configure(EntityTypeBuilder<AuthorContent> modelBuilder)
        {
            modelBuilder.Property(ac => ac.Id)
                 .HasColumnName("id")
                 .HasColumnType("UNIQUEIDENTIFIER");

            modelBuilder.Property(ac => ac.AuthorId)
                 .HasColumnName("AuthorId")
                 .HasColumnType("UNIQUEIDENTIFIER");

            modelBuilder.Property(ac => ac.ContentId)
                 .HasColumnName("ContentId")
                 .HasColumnType("UNIQUEIDENTIFIER");

            modelBuilder.HasOne(a => a.Author)
                                .WithMany(ac => ac.AuthorContents)
                                .HasForeignKey(a => a.AuthorId);

            modelBuilder.HasOne(c => c.Content)
                                .WithMany(ac => ac.ContentAuthors)
                                .HasForeignKey(c => c.ContentId);
        }
    }
}
