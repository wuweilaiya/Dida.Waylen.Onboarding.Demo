namespace Dida.Waylen.Onboarding.Demo.Service.Open.Data.EntityConfigurations;

public class RoomEntityTypeConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("Room");
        builder.PropertyFullAuditedEntity();
        builder.HasIndex(e => e.Type);
        builder.HasIndex(e => e.BedType);
        builder.Property(e => e.Type).HasComment("房型");
        builder.Property(e => e.TypeDescription).HasComment("房型描述");
        builder.Property(e => e.BedType).HasComment("床型");
        builder.Property(e => e.BedTypeDescription).HasComment("床型描述");
        builder.Property(e => e.Description).HasMaxLength(1000).HasComment("房间描述");
        builder.OwnsOne(e => e.Image, e => e.PropertyImage());
    }
}
