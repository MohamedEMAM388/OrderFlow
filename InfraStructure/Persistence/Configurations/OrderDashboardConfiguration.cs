using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfraStructure.Persistence.Configurations;

public class OrderDashboardConfiguration : IEntityTypeConfiguration<OrderDashboard>
{
    public void Configure(EntityTypeBuilder<OrderDashboard> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CustomerName)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.TotalPrice)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.OrderStatus)
            .HasConversion<string>()
            .HasMaxLength(30);
    }
}