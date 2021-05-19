using Elbek.MContent.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elbek.MContent.DataAccess.Configs
{
public class AuthorConfig : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> modelBuilder)
    {
        modelBuilder.Property(a => a.Id)
        .HasColumnName("id")
        .HasColumnType("UNIQUEIDENTIFIER")
        .IsRequired(true);

        modelBuilder.Property(a => a.Name)
        .HasColumnName("Name")
        .HasMaxLength(50)
        .HasColumnType("nvarchar")
        .IsRequired(true);
    }
}
}
