namespace Manufacturer.Project.Domain.Models
{
    using System;
    using Manufacturer.Project.Domain.Infra;

    public class Color : BaseEntity<long>, IEntityModified
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> class.
        /// </summary>
        public Color()
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
        /// Gets or sets Name.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets Code.
        /// </summary>
        public virtual string SapCode { get; set; }

        /// <summary>
        /// Gets or sets HexaColor.
        /// </summary>
        public virtual string HexaColor { get; set; }
    }
}
