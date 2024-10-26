namespace Manufacturer.Project.Core.Mapping
{
    using Manufacturer.Project.Domain.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TrackerEnabledDbContext.Common.Configuration;

    /// <summary>
    /// Mapping class of the <see cref="TruckType"/> class.
    /// </summary>
    public class TruckTypeClassMap : IEntityTypeConfiguration<TruckType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TruckType"/> class.
        /// </summary>
        /// <param name="builder">entity type builder.</param>
        public void Configure(EntityTypeBuilder<TruckType> builder)
        {
            ////builder.HasQueryFilter(e => e.Deleted == null);

            builder.ToTable("TruckType");
            builder.HasKey(e => new { e.Id });

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Created)
                .HasColumnName("Created")
                .IsRequired();

            builder.Property(x => x.Modified)
                .HasColumnName("Modified");

            builder.Property(x => x.Deleted)
                .HasColumnName("Deleted");

            builder.Property(x => x.Type)
                .HasColumnName("Type")
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(50);
        }
    }
}
