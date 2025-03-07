namespace Dida.Waylen.Onboarding.Demo.Service.Open.Data.EntityConfigurations;

public static class EntityConfigurationExtensions
{
    public static void PropertyFullAuditedEntity<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : FullAuditedEntity<long>
    {
        builder.HasKey(h => h.Id);
        builder.HasIndex(e => e.IsDeleted);
        builder.Property(e => e.IsDeleted).HasDefaultValue(false).HasComment("是否删除");
        builder.Property(e => e.CreateTime).HasColumnType("datetime").IsRequired().HasComment("创建时间");
        builder.Property(e => e.CreateUserId).HasComment("创建者用户ID");
        builder.Property(e => e.UpdateTime).HasColumnType("datetime").HasComment("最后修改时间");
        builder.Property(e => e.UpdateUserId).HasComment("最后修改者用户ID");
        builder.Property(e => e.Id).HasComment("主键");
    }

    public static void PropertyAddress<TEntity>(this OwnedNavigationBuilder<TEntity, Address> address) where TEntity : FullAuditedEntity<long>
    {
        address.Property(e => e.Latitude).IsRequired().HasComment("纬度");
        address.Property(e => e.Longitude).IsRequired().HasComment("经度");
        address.Property(e => e.Country).HasMaxLength(200).IsRequired().HasComment("国家");
        address.Property(e => e.CountryCode).HasMaxLength(200).IsRequired().HasComment("国家编码");
        address.Property(e => e.Province).HasMaxLength(200).IsRequired().HasComment("省份");
        address.Property(e => e.ProvinceCode).HasMaxLength(200).IsRequired().HasComment("省份编码");
        address.Property(e => e.City).HasMaxLength(200).IsRequired().HasComment("城市");
        address.Property(e => e.CityCode).HasMaxLength(200).IsRequired().HasComment("城市编码");
        address.Property(e => e.Region).HasMaxLength(200).IsRequired().HasComment("区县");
        address.Property(e => e.RegionCode).HasMaxLength(200).IsRequired().HasComment("区县编码");
        address.Property(e => e.Street).HasMaxLength(200).IsRequired().HasComment("街道");
        address.Property(e => e.Detail).HasMaxLength(2000).IsRequired().HasComment("详细地址");
        address.Property(e => e.PostalCode).HasMaxLength(20).IsRequired().HasComment("邮政编码");
    }

    public static void PropertyContact<TEntity>(this OwnedNavigationBuilder<TEntity, Contact> contact) where TEntity : FullAuditedEntity<long>
    {
        contact.Property(e => e.PhoneNumber).HasMaxLength(200).IsRequired().HasComment("联系电话");
        contact.Property(e => e.Email).HasMaxLength(200).IsRequired().HasComment("联系邮箱");
        contact.Property(e => e.Website).HasMaxLength(2000).IsRequired().HasComment("网站");
    }

    public static void PropertyImage<TEntity>(this OwnedNavigationBuilder<TEntity, Image> image) where TEntity : FullAuditedEntity<long>
    {
        image.Property(e => e.Url).HasMaxLength(2000).HasComment("主图");
        image.Property(e => e.Description).HasMaxLength(200).HasComment("主图描述");
        image.Property(e => e.Attached).HasConversion(
            v => JsonSerializer.Serialize(v, default(JsonSerializerOptions)),
            v => JsonSerializer.Deserialize<List<ImageDetail>>(v, default(JsonSerializerOptions)) ?? new()
        ).HasComment("附图");
    }
}
