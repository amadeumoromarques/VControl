namespace Manufacturer.Project.Domain.Infra
{
    [Serializable]
    public class BaseEntity<IDType> : IIdentifiable<IDType>, IBaseEntity
    {
        public virtual IDType Id { get; set; }

        object IBaseEntity.Id => Id;
    }
}