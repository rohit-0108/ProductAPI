using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.ProductName).IsRequired().HasMaxLength(255);
        builder.Property(p => p.CreatedBy).IsRequired().HasMaxLength(100);
        builder.Property(p => p.ModifiedBy).HasMaxLength(100);
    }
}

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("Item");
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Quantity).IsRequired();
        
        builder.HasOne(i => i.Product)
               .WithMany(p => p.Items)
               .HasForeignKey(i => i.ProductId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
