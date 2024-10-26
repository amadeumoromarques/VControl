namespace Manufacturer.Project.Domain.Infra
{
    public interface IIdentifiable<IDType>
    {
        IDType Id { get; set; }
    }
}