namespace Manufacturer.Project.Domain.Models
{
    using System;
    using Manufacturer.Project.Domain.Infra;

    public class Truck : BaseEntity<long>, IEntityModified
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Truck"/> class.
        /// </summary>
        public Truck()
        {
            Created = DateTime.Now;
        }

        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }
        public string ChassisCode { get; set; }
        public int ManufacturerYear { get; set; }
        public long IdTruckType { get; set; }
        public long IdColor { get; set; }
        public long IdPlantOptions { get; set; }
        public TruckType TruckType { get; set; }
        public PlantOptions PlantOptions { get; set; }
        public Color Color { get; set; }
    }
}
