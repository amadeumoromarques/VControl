namespace Manufacturer.Project.Core.Mapping
{
    using Manufacturer.Project.Domain.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TrackerEnabledDbContext.Common.Configuration;

    /// <summary>
    /// Mapping class of the <see cref="Color"/> class.
    /// </summary>
    public class ColorClassMap : IEntityTypeConfiguration<Color>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> class.
        /// </summary>
        /// <param name="builder">entity type builder.</param>
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            ////builder.HasQueryFilter(e => e.Deleted == null);

            builder.ToTable("Color");
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

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(300);

            builder.Property(x => x.SapCode)
                .HasColumnName("SapCode")
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(100);

            builder.Property(x => x.HexaColor)
                .HasColumnName("HexaColor")
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(50);
        }
    }
}
