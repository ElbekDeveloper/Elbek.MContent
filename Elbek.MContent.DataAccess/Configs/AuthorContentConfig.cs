using Elbek.MContent.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elbek.MContent.DataAccess.Configs
{
    public class AuthorContentConfig : IEntityTypeConfiguration<ContentAuthors>
    {
        public void Configure(EntityTypeBuilder<ContentAuthors> modelBuilder)
        {
            modelBuilder.Property(ac => ac.Id)
                 .HasColumnName("id")
                 .HasColumnType("UNIQUEIDENTIFIER")
                 .IsRequired(true);

            modelBuilder.Property(ac => ac.AuthorId)
                 .HasColumnName("AuthorId")
                 .HasColumnType("UNIQUEIDENTIFIER")
                 .IsRequired(true);

            modelBuilder.Property(ac => ac.ContentId)
                 .HasColumnName("ContentId")
                 .HasColumnType("UNIQUEIDENTIFIER")
                  .IsRequired(true);

            modelBuilder.HasOne(a => a.Author)
                                .WithMany(ac => ac.ContentAuthors)
                                .HasForeignKey(a => a.AuthorId);

            modelBuilder.HasOne(c => c.Content)
                                .WithMany(ac => ac.ContentAuthors)
                                .HasForeignKey(c => c.ContentId);
        }
    }
}
