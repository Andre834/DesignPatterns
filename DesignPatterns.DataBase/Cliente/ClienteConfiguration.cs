using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesignPatterns.DataBase.Cliente
{


    public sealed class ClienteConfiguration : IEntityTypeConfiguration<Domain.Models.Cliente>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Cliente> builder)
        {
            builder.ToTable(nameof(Cliente), nameof(Cliente));

            builder.HasKey(entity => entity.Id);

            builder.Property(entity => entity.Id).ValueGeneratedOnAdd().IsRequired();

            builder.OwnsOne(entity => entity.Nome, relationship => relationship.Property(property => property.Value).HasMaxLength(250).IsRequired());

            builder.OwnsOne(entity => entity.Email, relationship => relationship.Property(property => property.Value).HasMaxLength(250).IsRequired());
        }
    }
}
