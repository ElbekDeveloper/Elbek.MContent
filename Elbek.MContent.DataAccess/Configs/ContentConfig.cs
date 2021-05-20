using Elbek.MContent.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elbek.MContent.DataAccess.Configs {
  public class ContentConfig : IEntityTypeConfiguration<Content> {
    public void Configure(EntityTypeBuilder<Content> modelBuilder) {
      modelBuilder.Property(c => c.Id)
          .HasColumnName("id")
          .HasColumnType("UNIQUEIDENTIFIER")
          .IsRequired(true);

      modelBuilder.Property(c => c.Title)
          .HasColumnName("Title")
          .HasMaxLength(255)
          .HasColumnType("nvarchar")
          .IsRequired(true);

      modelBuilder.Property(c => c.Type)
          .HasColumnName("Type")
          .HasColumnType("int")
          .IsRequired(true);
    }
  }
}
