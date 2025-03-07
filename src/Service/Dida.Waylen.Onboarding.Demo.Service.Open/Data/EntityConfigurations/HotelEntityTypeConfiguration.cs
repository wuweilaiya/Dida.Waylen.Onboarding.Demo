namespace Dida.Waylen.Onboarding.Demo.Service.Open.Data.EntityConfigurations;

public class HotelEntityTypeConfiguration : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        builder.ToTable("Hotel");
        builder.PropertyFullAuditedEntity();
        builder.HasIndex(e => e.Name);
        builder.HasIndex(e => e.HotelStarRating);
        builder.Property(e => e.Name).HasMaxLength(200).IsRequired().HasComment("酒店名称");
        builder.Property(e => e.Description).HasMaxLength(1000).HasComment("酒店描述");
        builder.Property(e => e.HotelStarRating).HasComment("酒店星级");
        builder.OwnsOne(e => e.Address, e => e.PropertyAddress());
        builder.OwnsOne(e => e.Contact, e => e.PropertyContact());
        builder.OwnsOne(e => e.Image, e => e.PropertyImage());

        builder.HasMany(e => e.Rooms).WithOne(x => x.Hotel);
    }
}
