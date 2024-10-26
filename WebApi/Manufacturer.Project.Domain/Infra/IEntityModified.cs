namespace Manufacturer.Project.Domain.Infra
{
    public interface IEntityModified
    {
        /// <summary>
        /// Gets or sets Modified.
        /// </summary>
        DateTime? Modified { get; set; }

        /// <summary>
        /// Gets or sets Deleted.
        /// </summary>
        DateTime? Deleted { get; set; }
    }
}