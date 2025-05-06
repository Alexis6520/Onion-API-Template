using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EFCore.SqlServer.Configuration
{
    /// <summary>
    /// Configuración para donitas de Krispy Kreme
    /// </summary>
    public class DonutConfig : IEntityTypeConfiguration<Donut>
    {
        public void Configure(EntityTypeBuilder<Donut> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(200)
                .IsRequired(false);

            builder.Property(x => x.Price)
                .HasColumnType("decimal(5,2)");
        }
    }
}
