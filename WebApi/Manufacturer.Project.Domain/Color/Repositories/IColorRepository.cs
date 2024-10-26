namespace Manufacturer.Project.Domain.Repositories
{
    using Manufacturer.Project.Domain.Models;
    using Manufacturer.Project.Domain.Infra;

    public interface IColorRepository : IRepository<Color, long>
    {
    }
}