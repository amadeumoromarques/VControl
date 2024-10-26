namespace Manufacturer.Project.Core.Mapping
{
    using Manufacturer.Project.Domain.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TrackerEnabledDbContext.Common.Configuration;

    /// <summary>
    /// Mapping class of the <see cref="Truck"/> class.
    /// </summary>
    public class TruckClassMap : IEntityTypeConfiguration<Truck>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Truck"/> class.
        /// </summary>
        /// <param name="builder">entity type builder.</param>
        public void Configure(EntityTypeBuilder<Truck> builder)
        {
            builder.HasQueryFilter(e => e.Deleted == null);

            builder.ToTable("Truck");
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

            builder.Property(x => x.ChassisCode)
                .HasColumnName("ChassisCode")
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(200);

            builder.Property(x => x.ManufacturerYear)
                .HasColumnName("ManufacturerYear")
                .IsRequired();

            builder.Property(x => x.IdColor)
                .HasColumnName("IdColor")
                .IsRequired();

            builder.Property(x => x.IdTruckType)
                .HasColumnName("IdTruckType")
                .IsRequired();

            builder.Property(x => x.IdPlantOptions)
                .HasColumnName("IdPlantOptions")
                .IsRequired();

            builder.HasOne(f => f.Color)
                .WithMany()
                .HasForeignKey(f => f.IdColor)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Truck_Color");

            builder.HasOne(f => f.TruckType)
                .WithMany()
                .HasForeignKey(f => f.IdTruckType)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Truck_TruckType");

            builder.HasOne(f => f.PlantOptions)
                .WithMany()
                .HasForeignKey(f => f.IdPlantOptions)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Truck_PlantOptions");
        }
    }
}
