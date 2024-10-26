namespace Manufacturer.Project.Domain.Models
{
    using System;
    using Manufacturer.Project.Domain.Infra;

    public class TruckType : BaseEntity<long>, IEntityModified
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TruckType"/> class.
        /// </summary>
        public TruckType()
        {
            Created = DateTime.Now;
        }

        /// <summary>
        /// Gets or sets Created.
        /// </summary>
        public virtual DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets Modified.
        /// </summary>
        public virtual DateTime? Modified { get; set; }

        /// <summary>
        /// Gets or sets Deleted.
        /// </summary>
        public virtual DateTime? Deleted { get; set; }

        /// <summary>
        /// Gets or sets Type.
        /// </summary>
        public virtual string Type { get; set; }

        /////// <summary>
        /////// Gets or sets Field.
        /////// </summary>
        ////public virtual IList<Field> Field { get; set; }
    }
}
