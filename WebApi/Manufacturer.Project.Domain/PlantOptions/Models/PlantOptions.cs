namespace Manufacturer.Project.Domain.Models
{
    using System;
    using Manufacturer.Project.Domain.Infra;

    public class PlantOptions : BaseEntity<long>, IEntityModified
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlantOptions"/> class.
        /// </summary>
        public PlantOptions()
        {
            Created = DateTime.Now;
        }

        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }
        public string Key { get; set; }
        public string DisplayName { get; set; }
    }
}
